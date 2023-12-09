using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace videowp.Classes
{
    /// <summary>
    /// Управляет локальным плейлистом
    /// </summary>
    internal class PlaylistControl
    {
        public readonly string PLAYLIST_PATH = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\PL.m3u"; // плейлист
        public string playlistFolderPath;

        public PlaylistControl(string playlistFolderPath)
        {
            // проверка папки с видео
            if (!playlistFolderPath.Equals(""))
                if (!Directory.Exists(playlistFolderPath)) Directory.CreateDirectory(playlistFolderPath);
            else
                this.playlistFolderPath = "";
            this.playlistFolderPath = playlistFolderPath;

            // проверка наличия плейлиста
            if (!File.Exists(PLAYLIST_PATH))
            {
                StreamWriter writer = new StreamWriter(PLAYLIST_PATH, false);
                writer.WriteLine("");
                writer.Close();
            }
        }

        /// <summary>
        /// Добавление и удаление файлов в файле плейлиста из папки
        /// </summary>
        public void CheckFilesInPlaylist()
        {         
            // считывание плейлиста
            StreamReader reader = new StreamReader(PLAYLIST_PATH);
            List<string> plFiles = new List<string>();
            string line;
            while ((line = reader.ReadLine()) != null) { 
                plFiles.Add(line); 
            }
            reader.Close();
            // считывание файлов папки и проверка целостности файлов
            List<string> dirFiles = VideoFileFunctions.GetVideofilesFromFolder(playlistFolderPath);
            foreach (string elem in dirFiles) {
                // если файл копируется сейчас, пропускается
                if (!VideoFileFunctions.IsIntegrity(elem)) {
                    try
                    {
                        File.Delete(elem);
                    } catch(IOException)
                    {
                        continue;
                    }
                }
            }
            // удаление несуществующих файлов из плейлиста
            int i = 0;
            bool isOldFiles = false;
            while (i<plFiles.Count)
            {
                string findElem = dirFiles.Find(el => el.Equals(plFiles[i]));
                if (findElem == null)
                {
                    plFiles.RemoveAt(i);
                    isOldFiles = true;
                }                       
                else
                    i++;
            }
            // добавление новых файлов из папки в плейлист
            foreach (string elem in dirFiles)
            {
                string findElem = plFiles.Find(el => el.Equals(elem));
                if (findElem == null)
                {
                    plFiles.Add(elem);
                    isOldFiles = true;
                }
            }
            // перезапись плейлиста
            if (isOldFiles)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(PLAYLIST_PATH, false);
                    foreach (string elem in plFiles) writer.WriteLine(elem);
                    writer.Close();
                }
                catch (IOException)
                {
                    return;
                }

            }
        }

        // проверка папки на наличие файлов
        public bool IsEmpty(){
            return playlistFolderPath.Equals("") || Directory.GetFiles(playlistFolderPath).Length == 0;
        }
    }

}
