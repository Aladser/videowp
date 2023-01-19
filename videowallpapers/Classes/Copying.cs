using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using videowp.Classes;

namespace videowp
{
    internal class Copying
    {
        readonly BackgroundWorker bw = new BackgroundWorker();
        readonly string src;
        readonly string dst;

        public Copying(string srcDir, string dstDir)
        {
            bw.DoWork += BW_DoWork;
            bw.WorkerReportsProgress = true;
            bw.ProgressChanged += new ProgressChangedEventHandler(Bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Bw_RunCompleted);
            src = srcDir;
            dst = dstDir;
        }

        // фоновая задача
        void BW_DoWork(object sender, DoWorkEventArgs e)
        {           
            bool isNewData = false;
            List<string> srcFiles = PlaylistControl.GetVideosFromFolder(src, true);
            List<string> dstFiles = PlaylistControl.GetVideosFromFolder(dst, true);
            // добавление файлов из сетевой папки
            int i = 0;
            int size = srcFiles.Count + dstFiles.Count;
            foreach (string srcFilename in srcFiles)
            {
                string findVideo = dstFiles.Find(x => x.Equals(srcFilename));
                if (findVideo == null)
                {
                    isNewData = true;
                    int copyCount = 0;
                    while (true)
                    {
                        File.Copy($"{src}\\{srcFilename}", $"{dst}\\{srcFilename}");
                        // проверка целостности
                        long srcSize = new FileInfo($"{src}\\{srcFilename}").Length;
                        long dstSize = new FileInfo($"{dst}\\{srcFilename}").Length;
                        if (srcSize == dstSize) break;
                        if (copyCount > 4) break;
                        copyCount++;
                    }
                }
                bw.ReportProgress((i*100)/size);
                i++;
            }
            // удаление файлов из папки плейлиста            
            foreach (string dstFile in dstFiles)
            {
                string findVideo = srcFiles.Find(x => x.Equals(dstFile));
                if (findVideo == null)
                {
                    isNewData = true;
                    File.Delete($"{dst}\\{dstFile}");
                }
                bw.ReportProgress((i * 100) / size);
                i++;
            }
            if (isNewData) Program.plCtrl.CheckFilesInPlaylist();
        }

        void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e){
            if (Program.mainform != null)
            {
                Program.mainform.setNotifyIconText($"Aladser Видеообои (копирование: {e.ProgressPercentage}%)");
            }
        }

        void Bw_RunCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (Program.mainform != null)
            {
                Program.mainform.setNotifyIconText($"Aladser Видеообои (копирование завершено)");
            }
        }
        public void Start() { bw.RunWorkerAsync(); }
        public bool IsActive() { return bw.IsBusy; }
    }
}
