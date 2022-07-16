using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

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
        // downtime обнулятся из событий движения мыши и нажатия клавиатуры
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
                if (downtime>=inactionInMs && !IsForegroundFullScreen() && !isActive)
                {
                    isActive = true;
                    Process.Start((string)e.Argument);
                }
                // пробуждение после запуска приложения
                else if ((downtime<inactionInMs || IsForegroundFullScreen()) && isActive)
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
        /// <summary>
        /// Событие об остановке показа обоев
        /// </summary>
        public void stopShowWallpaper()
        {
            downtime = 0;
        }

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
            return IsForegroundFullScreen(null);
        }

        public bool IsForegroundFullScreen(Screen screen)
        {
            if (screen == null)
            {
                screen = Screen.PrimaryScreen;
            }
            RECT rect = new RECT();
            GetWindowRect(new HandleRef(null, GetForegroundWindow()), ref rect);
            IntPtr hWnd = (IntPtr)GetForegroundWindow();

            uint procId = 0;
            GetWindowThreadProcessId(hWnd, out procId);
            var process = System.Diagnostics.Process.GetProcessById((int)procId);

            bool rslt = new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top).Contains(screen.Bounds);
            if ( rslt && !process.ToString().Contains(procs[procIndex]) )
            {
                return true;
            }
            else
                return false;
        }
    }
}
