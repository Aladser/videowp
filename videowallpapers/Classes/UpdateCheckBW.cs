using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;

namespace videowp.Classes
{
    internal class UpdateCheckBW
    {
        readonly BackgroundWorker bw = new BackgroundWorker();
        public string SharePath
        {
            get { return config.UpdateServer; }
        }
        public bool IsNewData;
        readonly PlaylistControl playlist;
        readonly ConfigControl config;
        readonly int[] times = {1, 30, 60, 120, 240, 480}; // время проверки обновлений

        public UpdateCheckBW(ConfigControl config, PlaylistControl pl)
        {
            bw.DoWork += BW_DoWork;
            bw.WorkerSupportsCancellation = true;

            this.config = config;
            playlist = pl;
        }

        // фоновая задача
        void BW_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (IsShareConnection() && Directory.Exists(playlist.playlistFolderPath)) GetFilesFromShare();
                Thread.Sleep(times[config.UpdateTime]*60000);
            }
        }

        // получить видео из сетевой папки
        public void GetFilesFromShare()
        {
            List<string> srcFiles = VideoFileFunctions.GetVideofilesFromFolder(config.UpdateServer, true);
            List<string> dstFiles = VideoFileFunctions.GetVideofilesFromFolder(playlist.playlistFolderPath, true);
            bool newdata = false;

            // добавление файлов из сетевой папки
            foreach (string srcFilename in srcFiles)
            {
                string findVideo = dstFiles.Find(x => x.Equals(srcFilename));
                if (findVideo == null)
                {
                    newdata = true;
                    int copyCount = 0;
                    while (true)
                    {
                        // копирование при доступной сетевой папке
                        int count = 0;
                        if (IsShareConnection())
                        {
                            string dstPath = $"{playlist.playlistFolderPath}\\{srcFilename}";
                            File.Copy($"{config.UpdateServer}\\{srcFilename}", dstPath);
                            // проверка целостности
                            if(VideoFileFunctions.IsIntegrity(dstPath)) break;
                            else
                            {
                                if (count > 4)
                                {
                                    File.Delete(dstPath);
                                    break;
                                }
                                count++;
                            }
                        }
                        else
                        {
                            Thread.Sleep(60000);
                            if (copyCount > 5) break;
                            copyCount++;
                            continue;
                        }
                    }
                }
            }

            // удаление файлов из папки, которых нет в сетевой папке
            foreach (string dstFilename in dstFiles)
            {
                string findVideo = srcFiles.Find(x => x.Equals(dstFilename));
                if (findVideo == null && IsShareConnection())
                {
                    newdata = true;
                    File.Delete($"{playlist.playlistFolderPath}\\{dstFilename}");
                }
            }
            IsNewData = newdata;
        }

        public void BW_GetFilesFromShare(object sender, DoWorkEventArgs e)
        {
            GetFilesFromShare();
        }

        // установить сетевую папку
        public void SetShare(string path) { config.UpdateServer = path; }

        // проверка соединения с шарой
        public bool IsShareConnection() { return Directory.Exists(config.UpdateServer); }

        // старт фоновой задачи
        public void Start() { bw.RunWorkerAsync(); }

        // остановка фоновой задачи
        public void Stop() { bw.CancelAsync(); }

        // Возвращает активность фоновой задачи
        public bool IsActive() { return bw.IsBusy; }


    }
}
