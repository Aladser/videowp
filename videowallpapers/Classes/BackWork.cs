﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace videowp
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
        long downtime;
        long dwt1, dwt2;

        /// <summary>
        /// Класс фоновой задачи показа обоев
        /// </summary>
        public BackWork()
        {
            bw.DoWork += BW_DoWork;
            bw.WorkerSupportsCancellation = true;
        }

        /// <summary>
        /// фоновая задача
        /// </summary>
        void BW_DoWork(object sender, DoWorkEventArgs e)
        {
            bool isActive = false;

            long startBWTime = GetTimeNow();
            dwt1 = startBWTime;
            downtime = 0;

            Program.mpvProc.Arguments = $"--playlist={Program.config.PlaylistPath}";

            while (true)
            {               
                // выключение фоновой задачи
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                //поиск другого запущенного приложения в фуллскрине
                if (IsForegroundFullScreen() && Program.config.OverWindows==0)
                {
                    dwt1 = GetTimeNow();
                }
                // запуск обоев
                else if (downtime >= Program.config.GetInactionTime() && !isActive)
                {
                    dwt2 = GetTimeNow();
                    isActive = true;
                    Process.Start(Program.mpvProc);
                }
                // прерывание показа обоев
                else if (downtime < Program.config.GetInactionTime() && isActive)
                {
                    dwt1 = GetTimeNow();
                    isActive = false;
                    foreach (Process proc in Process.GetProcessesByName("mpv")) proc.Kill();
                }
                System.Threading.Thread.Sleep(200);
                dwt2 = GetTimeNow();
                downtime = dwt2 - dwt1;
            }
        }

        /// <summary>
        /// старт фоновой задачи
        /// </summary>
        /// <param name="plpath"></param>
        public void Start(){bw.RunWorkerAsync(); }
        /// <summary>
        /// остановка фоновой задачи
        /// </summary>
        public void Stop(){ bw.CancelAsync();}
        /// <summary>
        /// получить текущее время в мс
        /// </summary>
        /// <returns></returns>
        private long GetTimeNow() {return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;}
        /// <summary>
        /// Возвращает активность фоновой задачи
        /// </summary>
        /// <returns></returns>
        public bool IsActive(){ return bw.IsBusy; }
        /// <summary>
        /// Событие остановки показа обоев
        /// </summary>
        public void StopShowWallpaper(){ dwt1 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;}
        // Определение запуска фуллэкрана
        [StructLayout(LayoutKind.Sequential)]
        struct RECT
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
            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.PrimaryScreen;
            RECT rect = new RECT();
            IntPtr hWnd = (IntPtr)GetForegroundWindow();
            GetWindowRect(new HandleRef(null, hWnd), ref rect);

            uint procId = 0;
            GetWindowThreadProcessId(hWnd, out procId);
            string proc = Process.GetProcessById((int)procId).ToString();
            if (
                screen.Bounds.Width == (rect.right - rect.left) && 
                screen.Bounds.Height == (rect.bottom - rect.top) && 
                !proc.Contains("mpv") && 
                !proc.Contains("explorer")
               )
               return true;
            else
               return false;
        }
    }
}