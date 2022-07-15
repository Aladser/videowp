using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace videowallpapers
{
    internal class BackWork
    {
        int downtime;
        readonly BackgroundWorker bw = new BackgroundWorker();
        readonly double[] inActonTime = { 0.03, 1, 3, 5, 10, 15 };
        int inActionNumber;
        int inactionInMs; // время простоя
        // процесс Windows
        string[] procs = { "mpc-hc64", "KMPlayer64" };
        int procIndex = 0;
        /// <summary>
        /// Класс фоновой задачи для показа обоев
        /// </summary>
        /// <param name="inActionNumber">время, номер берется из Combobox</param>
        public BackWork(int inActionNumber)
        {
            bw.DoWork += BW_DoWork;
            bw.WorkerSupportsCancellation = true;
            this.inActionNumber = inActionNumber;
            setTimePeriod(inActionNumber);
        }
        // фоновая задача
        private void BW_DoWork(object sender, DoWorkEventArgs e)
        {
            bool isActive = false;
            downtime = 0;
            while (true)
            {
                // послана команда на выключение фоновой задачи
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                // закончился таймер ожидания
                if (downtime >= inactionInMs && !isActive)
                {
                    bool isProcess = false;
                    foreach (Process elem in Process.GetProcesses())
                    {
                        if (elem.ToString().Contains(procs[procIndex]))
                        {
                            isProcess = true;
                            break;
                        }
                    }
                    if (isProcess == true)
                        continue;
                    isActive = true;
                    Process.Start((string)e.Argument);
                }
                // пробуждение после запуска приложения
                else if (downtime < inactionInMs && isActive)
                {
                    isActive = false;
                    Process[] processes = System.Diagnostics.Process.GetProcessesByName(procs[procIndex]);
                    foreach (Process elem in processes)
                        elem.Kill();
                }
                else
                {
                    System.Threading.Thread.Sleep(100);
                    downtime += 100;
                }
            }
        }
        /// <summary>
        /// старт фоновой задачи
        /// </summary>
        /// <param name="plpath"></param>
        public void start(String plpath)
        {
            procIndex = Path.GetExtension(plpath) == ".kpl" ? 1 : 0; 
            bw.RunWorkerAsync(plpath); 
        }
        /// <summary>
        /// остановка фоновой задачи
        /// </summary>
        public void stop()
        {
            bw.CancelAsync();
        }
        /// <summary>
        /// установка времнеи простоя системы
        /// </summary>
        /// <param name="inActionNumber"></param>
        public void setTimePeriod(int inActionNumber)
        {
            this.inActionNumber = inActionNumber;
            inactionInMs = (int)(inActonTime[inActionNumber] * 60000);
        }
        /// <summary>
        /// возвращает индекс времени для combobox
        /// </summary>
        /// <returns></returns>
        public int getTimePeriod()
        {
            return inActionNumber;
        }

        public void stopShowWallpaper()
        {
            downtime = 0;
        }
    }
}
