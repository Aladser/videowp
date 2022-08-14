using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

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
        [DllImport("user32.dll", SetLastError = true)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        [Flags]
        enum MouseFlags
        {
            Move = 0x0001, LeftDown = 0x0002, LeftUp = 0x0004, RightDown = 0x0008,
            RightUp = 0x0010, Absolute = 0x8000
        };

        readonly BackgroundWorker bw = new BackgroundWorker();                      
        readonly double[] inactionTime = { 0.05, 1, 3, 5, 10, 15 }; // массив периодов бездействия
        int inactionNumber;
        int inactionInMs;
        long downtime;
        long dwt1, dwt2;
        ProcessStartInfo command = new ProcessStartInfo(@"cmd.exe", @"");

        /// <summary>
        /// Класс фоновой задачи показа обоев
        /// </summary>
        /// <param name="inActionNumber">время, номер берется из Combobox</param>
        public BackWork(int inactionNumber)
        {
            initialise();
            this.inactionNumber = inactionNumber;
            setTimePeriod(inactionNumber);
        }
        public BackWork()
        {
            initialise();
            this.inactionNumber = 0;
            setTimePeriod(0);
        }
        private void initialise()
        {
            command.WindowStyle = ProcessWindowStyle.Hidden;
            command.RedirectStandardOutput = true;
            command.UseShellExecute = false;
            command.CreateNoWindow = true;
            bw.DoWork += BW_DoWork;
            bw.WorkerSupportsCancellation = true;
        }
        // фоновая задача
        private void BW_DoWork(object sender, DoWorkEventArgs e)
        {
            bool isActive = false;
            dwt1 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            downtime = 0;
            // /C C:\Projects\videowallpapers\videowallpapers\bin\Debug\mpv\mpv.exe --playlist=D:\VideoWP\PL.m3u
            command.Arguments = @"/C " + Program.mpv + " --fs --playlist=" + Program.cfgdata.plpath;
            Console.WriteLine(command.Arguments);
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
                    Process.Start(command);
                }
                // пробуждение после запуска приложения
                else if (downtime<inactionInMs && isActive)
                {
                    dwt1 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    isActive = false;
                    //Process[] processes = Process.GetProcessesByName( "mpv" );
                    //foreach (Process elem in processes)
                    //  elem.Kill();
                    // Двойное нажатие мыши
                    mouse_event((uint)MouseFlags.LeftDown, 0, 0, 0, 0);
                    mouse_event((uint)MouseFlags.LeftUp, 0, 0, 0, 0);
                    mouse_event((uint)MouseFlags.LeftDown, 0, 0, 0, 0);
                    mouse_event((uint)MouseFlags.LeftUp, 0, 0, 0, 0);
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
        public void start()
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
        // Определение запуска фуллэкрана
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
                && !proc.Contains( "mpv" ) 
                && !proc.Contains("explorer")
                )
                return true;
            else
                return false;
        }
    }
}
