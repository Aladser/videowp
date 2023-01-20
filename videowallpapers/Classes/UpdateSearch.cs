using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;

namespace videowp.Classes
{
    public class UpdateSearch
    {
        readonly BackgroundWorker bw = new BackgroundWorker();

        readonly PlaylistControl playlist;

        public string SharePath{
            get { return sharePath; }
        }
        string sharePath;

        int downtime;

        // \\192.168.1.100\Data\video
        public UpdateSearch(PlaylistControl pl, string sharePath, int downtime=60000)
        {
            bw.DoWork += BW_DoWork;
            bw.WorkerSupportsCancellation = true;

            playlist = pl;
            this.sharePath = sharePath;
            this.downtime = downtime;
        }

        // фоновая задача
        void BW_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (IsShare()) GetFilesFromShare();
                Thread.Sleep(downtime);
            }
        }

        // получить видео из сетевой папки
        public void GetFilesFromShare()
        {
            List<string> srcFiles = GetVideoFromFolder(sharePath, true);
            List<string> dstFiles = GetVideoFromFolder(playlist.playlistFolderPath, true);

            // добавление файлов из сетевой папки
            bool isNewData = false;
            foreach (string srcFilename in srcFiles)
            {
                string findVideo = dstFiles.Find(x => x.Equals(srcFilename));
                if (findVideo == null)
                {
                    isNewData = true;
                    int copyCount = 0;
                    while (true)
                    {
                        // копирование при доступной сетевой папке
                        if (IsShare())
                            File.Copy($"{sharePath}\\{srcFilename}", $"{playlist.playlistFolderPath}\\{srcFilename}");
                        else
                        {
                            Thread.Sleep(60000);
                            if (copyCount > 4) break;
                            copyCount++;
                            continue;
                        }
                        // проверка целостности
                        long srcSize = new FileInfo($"{sharePath}\\{srcFilename}").Length;
                        long dstSize = new FileInfo($"{playlist.playlistFolderPath}\\{srcFilename}").Length;
                        if (srcSize == dstSize) break;
                        if (copyCount > 4) break;
                        copyCount++;
                    }
                }
            }

            // удаление файлов из папки, которых нет в сетевой папке
            foreach (string dstFilename in dstFiles)
            {
                string findVideo = srcFiles.Find(x => x.Equals(dstFilename));
                if (findVideo == null && IsShare())
                {
                    isNewData = true;
                    File.Delete($"{playlist.playlistFolderPath}\\{dstFilename}");
                }
            }
            Program.isNewData = isNewData;
        }

        public void BW_GetFilesFromShare(object sender, DoWorkEventArgs e)
        {
            GetFilesFromShare();
        }

        // установить время простоя
        public void SetDowntime(int period) { this.downtime = period; }

        // установить сетевую папку
        public void SetShare(string path) { sharePath = path; }
        // проверка соединения с шарой
        public bool IsShare() { return Directory.Exists(sharePath); }

        // старт фоновой задачи
        public void Start() { bw.RunWorkerAsync(); }
        // остановка фоновой задачи
        public void Stop() { bw.CancelAsync(); }
        // Возвращает активность фоновой задачи
        public bool IsActive() { return bw.IsBusy; }

        // Получить файлы из папки
        public static List<string> GetVideoFromFolder(string path, bool onlyFilename = false)
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
