using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace videowallpapers
{
    internal class BackWork
    {
        uint downtime;
        readonly BackgroundWorker bw = new BackgroundWorker();
        readonly double[] inActonTime = { 0.03, 1, 3, 5, 10, 15 };
        int inActionNumber;
        int inactionInMs; // время простоя
        // процесс Windows
        string[] procs = { "mpc-hc64", "KMPlayer64", "vlc" };
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
        // downtime обнулятся и событий движения мыши и нажатия клавиатуры
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
                // обновление downtime для Win10-11
                if(Environment.OSVersion.ToString() == "Microsoft Windows NT 6.2.9200.0")
                {
                    downtime = GetIdleTime();
                } 
                // закончился таймер ожидания
                if (downtime>=inactionInMs && !isActive)
                {
                    bool isProcess = false;
                    // запрет запуска второго видеоплеера
                    Process[] actprocs = Process.GetProcesses();
                    foreach (Process elemi in Process.GetProcesses())
                    {
                        foreach(string elemj in procs)
                        if (elemi.ToString().Contains(elemj))
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
                else if (downtime >= (inactionInMs+1000) && isActive)
                {
                    continue;
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
                    if (Environment.OSVersion.ToString() != "Microsoft Windows NT 6.2.9200.0")
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
            switch (Path.GetExtension(plpath))
            {
                case ".mpcpl":
                    procIndex = 0;
                    break;
                case ".kpl":
                    procIndex = 1;
                    break;
                default:
                    procIndex = 2;
                    break;
            }
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
        /// Возравращает, работает ли фоновая задача
        /// </summary>
        /// <returns></returns>
        public bool isActive()
        {
            return bw.IsBusy;
        }
        /// <summary>
        /// установка времнеи простоя системы
        /// </summary>
        /// <param name="inActionNumber"></param>
        public void setTimePeriod(int inActionNumber)
        {
            this.inActionNumber = inActionNumber;
            inactionInMs = (int)(inActonTime[inActionNumber] * 60000);
            Console.WriteLine(inactionInMs);
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
        // Таймер бездействия системы. Работает корректно на Вин10/Вин11
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        internal struct LASTINPUTINFO
        {
            public uint cbSize;

            public uint dwTime;
        }
        public static uint GetIdleTime()
        {
            LASTINPUTINFO LastUserAction = new LASTINPUTINFO();
            LastUserAction.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(LastUserAction);
            GetLastInputInfo(ref LastUserAction);
            return ((uint)Environment.TickCount - LastUserAction.dwTime);
        }
    }
}
