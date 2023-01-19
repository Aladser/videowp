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
        string sharePath;

        public PlaylistControl(string playlistFolderPath, string sharePath = "")
        {
            this.playlistFolderPath = Directory.Exists(playlistFolderPath) ? playlistFolderPath : "";
            this.sharePath = sharePath;

            if (!File.Exists(PLAYLIST_PATH))
            {
                StreamWriter writer = new StreamWriter(PLAYLIST_PATH, false);
                writer.WriteLine("");
                writer.Close();
            }
            else
            {
                if (!sharePath.Equals(""))
                {
                    if (Directory.Exists(sharePath))
                    {
                        CompareFilesWithShare();
                    }
                }
                CheckFilesInPlaylist();
            }
        }

        // установка сетевой папки
        public void setShare(string path)
        {
            sharePath = path;
        }

        //добавление и удаление файлов в плейлист из папки
        public void CheckFilesInPlaylist()
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
                if (isOldFiles)
                {
                    StreamWriter writer = new StreamWriter(PLAYLIST_PATH, false);
                    foreach (string elem in plFiles) writer.WriteLine(elem);
                    writer.Close();
                }
            }
        }

        // сравнить папку видео с сетевой папкой
        public void CompareFilesWithShare()
        {
            if(!sharePath.Equals(""))
            {
                List<string> dirFiles = GetVideosFromFolder(playlistFolderPath, true);
                List<string> shareFiles = GetVideosFromFolder(sharePath, true);
                // добавление файлов из сетевой папки
                foreach(string shrVideo in shareFiles)
                {
                    string findVideo = dirFiles.Find(x => x.Equals(shrVideo));
                    if (findVideo == null) File.Copy($"{sharePath}\\{shrVideo}", $"{playlistFolderPath}\\{shrVideo}");
                }
                // удаление файлов из папки плейлиста
                foreach (string dirVideo in dirFiles)
                {
                    string findVideo = shareFiles.Find(x => x.Equals(dirVideo));
                    if (findVideo == null) File.Delete($"{playlistFolderPath}\\{dirVideo}");
                }
            }
        }

        // проверка папки на наличие файлов
        public bool IsEmpty()
        {
            if (Directory.Exists(playlistFolderPath))
                return Directory.GetFiles(playlistFolderPath).Length == 0;
            else
                return true;
        }

        List<string> GetVideosFromFolder(string path, bool onlyFilename = false)
        {
            List<string> dirFiles = Directory.GetFiles(path).ToList<string>();
            string ext;
            string[] extList = {".mp4", ".m4v", ".mkv", ".avi"};
            for(int i=0; i<dirFiles.Count; i++)
            {
                ext = Path.GetExtension(dirFiles[i]);
                if (!extList.Contains(ext)) dirFiles.RemoveAt(i);
            }
            if (onlyFilename)
                for (int i = 0; i < dirFiles.Count; i++) 
                    dirFiles[i] = Path.GetFileName(dirFiles[i]);
            return dirFiles;
        }
    }

}
