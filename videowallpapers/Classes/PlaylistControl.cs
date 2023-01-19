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
        Copying copyCtrl;

        public PlaylistControl(string playlistFolderPath, string sharePath = "")
        {
            this.playlistFolderPath = Directory.Exists(playlistFolderPath) ? playlistFolderPath : "";
            this.sharePath = sharePath;
            copyCtrl = !sharePath.Equals("") ? new Copying(sharePath, playlistFolderPath) : null;

            // проверка наличия плейлиста
            if (!File.Exists(PLAYLIST_PATH))
            {
                StreamWriter writer = new StreamWriter(PLAYLIST_PATH, false);
                writer.WriteLine("");
                writer.Close();
            }
            else
            {
                if (IsShare()) GetFilesFromShare();
                CheckFilesInPlaylist();
            }
        }

        // установка сетевой папки
        // \\192.168.1.100\Data\video
        public void SetShare(string path) {
            sharePath = path;
            copyCtrl = new Copying(sharePath, playlistFolderPath);
        }

        // проверка соединения с шарой
        public bool IsShare() {
            if (!sharePath.Equals(""))
            {
                return Directory.Exists(sharePath);
            }
            else
                return false;
        }

        // получить видео из сетевой папки
        public void GetFilesFromShare()
        {
            copyCtrl.Start();
        }

        //добавление и удаление файлов в файл плейлиста из папки
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

        // проверка папки на наличие файлов
        public bool IsEmpty()
        {
            if (Directory.Exists(playlistFolderPath))
                return Directory.GetFiles(playlistFolderPath).Length == 0;
            else
                return true;
        }

        // активное копирование
        public bool IsActiveCopying(){return copyCtrl.IsActive();}

        // Получить файлы из папки
        public static List<string> GetVideosFromFolder(string path, bool onlyFilename = false)
        {
            List<string> dirFiles = Directory.GetFiles(path).ToList<string>();
            string ext;
            string[] extList = { ".mp4", ".m4v", ".mkv", ".avi" };
            for (int i = 0; i < dirFiles.Count; i++)
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
