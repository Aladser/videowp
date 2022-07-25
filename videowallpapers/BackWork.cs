using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace videowallpapers
{
    internal class BackWork
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(HandleRef hWnd, [In, Out] ref RECT rect);
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        readonly BackgroundWorker bw = new BackgroundWorker();
        readonly double[] inactionTime = { 0.05, 1, 3, 5, 10, 15 }; // массив периодов бездействия
        int inactionNumber;
        int inactionInMs;
        long downtime;
        long dwt1, dwt2;

        /// <summary>
        /// Класс фоновой задачи показа обоев
        /// </summary>
        /// <param name="inActionNumber">время, номер берется из Combobox</param>
        public BackWork(int inactionNumber)
        {
            bw.DoWork += BW_DoWork;
            bw.WorkerSupportsCancellation = true;
            this.inactionNumber = inactionNumber;
            setTimePeriod(inactionNumber);
        }
        public BackWork()
        {
            bw.DoWork += BW_DoWork;
            bw.WorkerSupportsCancellation = true;
            this.inactionNumber = 0;
            setTimePeriod(0);
        }
        // фоновая задача
        private void BW_DoWork(object sender, DoWorkEventArgs e)
        {
            bool isActive = false;
            dwt1 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            downtime = 0;
            while (true)
            {               
                // послана команда на выключение фоновой задачи
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                // если работает приложение в полном экране
                if (IsForegroundFullScreen())
                {
                    dwt1 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                }
                // таймер закончился
                else if (downtime>=inactionInMs && !isActive)
                {
                    dwt2 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    isActive = true;
                    if (Program.cfgdata.player == 1)
                    {
                        try
                        {
                            Program.mplayerPr.StartInfo.Arguments = " -fixed-vo -zoom -xy " + Program.widthScreen + " -shuffle -loop 0 -playlist " + Program.cfgdata.plpath + " &> /dev/null";
                            Program.mplayerPr.Start();
                        }
                        catch (System.ComponentModel.Win32Exception)
                        {
                            MessageBox.Show("MPlayer не найден. Установить плеер рядом с исполняемым файлом");
                            this.stop();
                            Process.GetCurrentProcess().Kill();
                        }
                    }
                    else
                        Process.Start(MainForm.player.getPlaylist());
                }
                // пробуждение после запуска приложения
                else if (downtime<inactionInMs && isActive)
                {
                    dwt1 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    isActive = false;
                    Process[] processes = Process.GetProcessesByName( MainForm.player.getActivePlayer() );
                    foreach (Process elem in processes)
                        elem.Kill();
                }
                Thread.Sleep(100);
                dwt2 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                downtime = dwt2 - dwt1;
            }
        }
        /// <summary>
        /// старт фоновой задачи
        /// </summary>
        /// <param name="plpath"></param>
        public void start(String plpath)
        {
            bw.RunWorkerAsync(); 
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
            this.inactionNumber = inActionNumber;
            inactionInMs = (int)(inactionTime[inActionNumber] * 60000);
        }
        /// <summary>
        /// возвращает индекс времени для combobox
        /// </summary>
        /// <returns></returns>
        public int getTimePeriod()
        {
            return inactionNumber;
        }
        /// <summary>
        /// Событие об остановке показа обоев
        /// </summary>
        public void stopShowWallpaper()
        {
            dwt1 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }
        // Опеределение запуска фуллэкрана
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        /// <summary>
        /// Поиск другой программы в фуллскрине
        /// </summary>
        /// <returns></returns>
        public bool IsForegroundFullScreen()
        {
            Screen screen = Screen.PrimaryScreen;
            RECT rect = new RECT();
            IntPtr hWnd = (IntPtr)GetForegroundWindow();
            GetWindowRect(new HandleRef(null, hWnd), ref rect);

            uint procId = 0;
            GetWindowThreadProcessId(hWnd, out procId);
            string proc = Process.GetProcessById((int)procId).ToString();
            if (screen.Bounds.Width == (rect.right - rect.left) 
                && screen.Bounds.Height == (rect.bottom - rect.top) 
                && !proc.Contains( MainForm.player.getActivePlayer() ) 
                && !proc.Contains("explorer")
                )
                return true;
            else
                return false;
        }
    }
}
