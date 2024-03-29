﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using videowp.Formes;

namespace videowp.Classes
{
    /// <summary>
    /// Синхронизирует локальный и сетевой плейлисты
    /// </summary>
    internal class PlaylistUpdatesBW
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

        public PlaylistUpdatesBW(ConfigControl config, PlaylistControl pl)
        {
            bw.DoWork += BW_DoWork;
            bw.WorkerSupportsCancellation = true;

            this.config = config;
            this.playlist = pl;
        }

        // фоновая задача
        void BW_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (IsShareConnection() && Directory.Exists(playlist.playlistFolderPath))
                {
                    GetFilesFromShare();
                }
                Thread.Sleep(times[config.UpdateTime]*60000);
            }
        }

        // получить видео из сетевой папки
        public void GetFilesFromShare(SettingForm setForm = null)
        {
            List<string> srcFiles = VideoFileFunctions.GetVideofilesFromFolder(config.UpdateServer, true); // файлы из сетевой папки
            List<string> localFiles = VideoFileFunctions.GetVideofilesFromFolder(playlist.playlistFolderPath, true); // файлы из папки плейлиста

            IsNewData = false; // новые данные?
            bool isEmptyPlaylist = localFiles.Count == 0; // пустая папка плейлиста?
            // проверка целостности файлов в папке плейлиста
            int i = 0;
            string path;
            while (i < localFiles.Count)
            {
                path = $"{playlist.playlistFolderPath}\\{localFiles[i]}";
                if (!VideoFileFunctions.IsIntegrity(path))
                {
                    File.Delete(path);
                    localFiles.RemoveAt(i);
                }
                else
                    i++;
            }
            // добавление файлов из сетевой папки
            foreach (string srcFilename in srcFiles)
            {
                string findVideo = localFiles.Find(x => x.Equals(srcFilename));
                if (findVideo == null)
                {
                    IsNewData = true;
                    File.Copy($"{config.UpdateServer}\\{srcFilename}", $"{playlist.playlistFolderPath}\\{srcFilename}");
                }
            }
           
            // удаление файлов из папки, которых нет в сетевой папке
            foreach (string dstFilename in localFiles)
            {
                string findVideo = srcFiles.Find(x => x.Equals(dstFilename));
                if (findVideo == null && IsShareConnection())
                {
                    IsNewData = true;
                    File.Delete($"{playlist.playlistFolderPath}\\{dstFilename}");
                }
            }
            // добавление новых видеофайлов, если папка плейлиста пуста
            if (IsNewData) playlist.CheckFilesInPlaylist();
            if(isEmptyPlaylist && IsNewData) Program.mainForm.CheckEmptyPlaylist();

            localFiles = VideoFileFunctions.GetVideofilesFromFolder(playlist.playlistFolderPath, true);
            foreach (string el in localFiles) Console.Write($"{el} ");
            Console.WriteLine();
        }

        // асинхронно получить видео из сетевой папки
        public void BW_GetFilesFromShare(object sender, DoWorkEventArgs e)
        {
            GetFilesFromShare((SettingForm)e.Argument);
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
