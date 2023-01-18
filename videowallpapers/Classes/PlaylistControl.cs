using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace videowp.Classes
{
    internal class PlaylistControl
    {
        public readonly string PLAYLIST_PATH = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\PL.m3u"; // плейлист
        public string playlistFolderPath;

        public PlaylistControl(string playlistFolderPath)
        {
            this.playlistFolderPath = Directory.Exists(playlistFolderPath) ? playlistFolderPath : "";
        }
        //проверка файлов плейлиста и папки
        public void CheckFilesInFolder()
        {         
            if (Directory.Exists(playlistFolderPath))
            {
                // считывание файлов плейлиста
                StreamReader reader = new StreamReader(PLAYLIST_PATH);
                List<string> plFiles = new List<string>();
                string line;
                while ((line = reader.ReadLine()) != null) plFiles.Add(line);
                reader.Close();
                // считывание файлов папки
                List<string> dirFiles = GetVideosFromFolder(playlistFolderPath);
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
                // коррекция соддержимого папки
                if (isOldFiles)
                {
                    StreamWriter writer = new StreamWriter(PLAYLIST_PATH, false);
                    writer.Write("");
                    writer.Close();
                    writer = new StreamWriter(PLAYLIST_PATH, true);
                    foreach (string elem in plFiles) writer.WriteLine(elem);
                    writer.Close();
                }
            }
        }

        // проверка папки на наличие файлов
        public bool IsEmpty()
        {
            return Directory.GetFiles(playlistFolderPath).Length == 0;
        }

        List<string> GetVideosFromFolder(string path)
        {
            List<string> dirFiles = Directory.GetFiles(playlistFolderPath).ToList<string>();
            string ext;
            string[] extList = {".mp4", ".m4v"};
            for(int i=0; i<dirFiles.Count; i++)
            {
                ext = Path.GetExtension(dirFiles[i]);
                if (!extList.Contains(ext)) dirFiles.RemoveAt(i);
            }
            return dirFiles;
        }
    }

}
