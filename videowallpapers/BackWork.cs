﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace videowallpapers
{
    internal class BackWork
    {
        readonly BackgroundWorker bw = new BackgroundWorker();
        readonly double[] inactionTime = { 0.05, 1, 3, 5, 10, 15 }; // массив периодов бездействия
        int inactionNumber;
        int inactionInMs;
        long downtime;
        long dwt1, dwt2;
        public List<string> log = new List<string>();

        // процесс Windows
        // Добавление нового плеера 5/6
        string[] procs = { "mpc-hc64", "KMPlayer64", "vlc" };
        int procIndex = 0;
        /// <summary>
        /// Класс фоновой задачи для показа обоев
        /// </summary>
        /// <param name="inActionNumber">время, номер берется из Combobox</param>
        public BackWork(int inactionNumber)
        {
            bw.DoWork += BW_DoWork;
            bw.WorkerSupportsCancellation = true;
            this.inactionNumber = inactionNumber;
            setTimePeriod(inactionNumber);
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
                    continue;
                }
                //  увеличение таймера
                if (downtime<inactionInMs && !isActive)
                {
                    Thread.Sleep(90);
                }
                // таймер закончился
                else if (downtime>=inactionInMs && !isActive)
                {
                    isActive = true;
                    Process.Start((string)e.Argument);
                }
                // пробуждение после запуска приложения
                else if (downtime<inactionInMs && isActive)
                {
                    dwt1 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    isActive = false;
                    Process[] processes = Process.GetProcessesByName(procs[procIndex]);
                    foreach (Process elem in processes)
                        elem.Kill();
                }
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
            // Добавление нового плеера 6/6
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

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(HandleRef hWnd, [In, Out] ref RECT rect);
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

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
                && !proc.Contains(procs[procIndex]) && !proc.Contains("explorer")
                )
            {
                log.Add(proc);
                //Console.WriteLine(proc);
                return true;
            }
            else
                return false;
        }
    }
}
