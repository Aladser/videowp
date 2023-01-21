using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace videowp.Classes
{
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

        //добавление и удаление файлов в плейлист из папки
        public void CheckFilesInPlaylist()
        {         
            // считывание плейлиста
            StreamReader reader = new StreamReader(PLAYLIST_PATH);
            List<string> plFiles = new List<string>();
            string line;
            while ((line = reader.ReadLine()) != null) plFiles.Add(line);
            reader.Close();
            // считывание файлов папки
            List<string> dirFiles = UpdateSearchBW.GetVideoFromFolder(playlistFolderPath);
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
            if (isOldFiles)
            {
                StreamWriter writer = new StreamWriter(PLAYLIST_PATH, false);
                foreach (string elem in plFiles) writer.WriteLine(elem);
                writer.Close();
            }
        }

        // проверка папки на наличие файлов
        public bool IsEmpty(){
            return playlistFolderPath.Equals("") ? true : Directory.GetFiles(playlistFolderPath).Length == 0;
        }
    }

}
