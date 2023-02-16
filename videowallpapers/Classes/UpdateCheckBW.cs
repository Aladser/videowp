using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using videowp.Formes;

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
            bool isEmptyPL = dstFiles.Count == 0;
            // проверка целостности файлов в папке плейлиста
            int i = 0;
            string path;
            while (i < dstFiles.Count)
            {
                path = $"{playlist.playlistFolderPath}\\{dstFiles[i]}";
                if (!VideoFileFunctions.IsIntegrity(path))
                {
                    File.Delete(path);
                    dstFiles.RemoveAt(i);
                }
                else
                    i++;
            }
            // добавление файлов из сетевой папки
            foreach (string srcFilename in srcFiles)
            {
                string findVideo = dstFiles.Find(x => x.Equals(srcFilename));
                if (findVideo == null)
                {
                    newdata = true;
                    File.Copy($"{config.UpdateServer}\\{srcFilename}", $"{playlist.playlistFolderPath}\\{srcFilename}");
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
            // добавление новых видео, если папка плейлиста пуста
            if(isEmptyPL && newdata)
            {
                Program.mainform.CheckFilesOfPlaylist();
            }
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
