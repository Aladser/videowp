using System;
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
        [DllImport("user32.dll", SetLastError = true)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        readonly BackgroundWorker bw = new BackgroundWorker();                      
        readonly double[] inactionTime = { 0.05, 1, 3, 5, 10, 15 }; // массив периодов бездействия
        int inactionNumber;
        int inactionInMs;
        long downtime;
        long dwt1, dwt2;
        ProcessStartInfo command = new ProcessStartInfo(@"cmd.exe", @"");


        private enum MouseFlags : uint
        {
            MOUSEEVENTF_ABSOLUTE = 0x8000,   // If set, dx and dy contain normalized absolute coordinates between 0 and 65535. The event procedure maps these coordinates onto the display surface. Coordinate (0,0) maps onto the upper-left corner of the display surface, (65535,65535) maps onto the lower-right corner.
            MOUSEEVENTF_LEFTDOWN = 0x0002,   // The left button is down.
            MOUSEEVENTF_LEFTUP = 0x0004,     // The left button is up.
            MOUSEEVENTF_MIDDLEDOWN = 0x0020, // The middle button is down.
            MOUSEEVENTF_MIDDLEUP = 0x0040,   // The middle button is up.
            MOUSEEVENTF_MOVE = 0x0001,       // Movement occurred.
            MOUSEEVENTF_RIGHTDOWN = 0x0008,  // The right button is down.
            MOUSEEVENTF_RIGHTUP = 0x0010,    // The right button is up.
            MOUSEEVENTF_WHEEL = 0x0800,      // The wheel has been moved, if the mouse has a wheel.The amount of movement is specified in dwData
            MOUSEEVENTF_XDOWN = 0x0080,      // An X button was pressed.
            MOUSEEVENTF_XUP = 0x0100,        // An X button was released.
            MOUSEEVENTF_HWHEEL = 0x01000     // The wheel button is tilted.
        }
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
            long startBWTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            dwt1 = startBWTime;
            downtime = 0;
            if(Program.cfgdata.player.Equals("mpv"))
                command.Arguments = @"/C " + Program.mpvPath + " --playlist=" + Program.cfgdata.plpath; // MPV
            else
                command.Arguments = @"/C " + Program.cfgdata.plpath; // VLC
            //Console.WriteLine(command.Arguments);
            while (true)
            {               
                // выключение фоновой задачи
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                // поиск другого запущенного приложения в фуллскрине
                if (IsForegroundFullScreen())
                {
                    dwt1 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                }
                // запуск обоев
                else if (downtime>=inactionInMs && !isActive)
                {
                    dwt2 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    isActive = true;
                    Process.Start(command);
                }
                // прерывание показа обоев
                else if (downtime<inactionInMs && isActive)
                {
                    dwt1 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    isActive = false;
                    Process.GetProcessesByName(Program.cfgdata.player)[0].Kill();
                }
                System.Threading.Thread.Sleep(100);
                dwt2 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                downtime = dwt2 - dwt1;
                // перезапуск обоев каждый час
                if( (dwt2-startBWTime) > 3600000 )
                {
                    if (isActive)
                        Process.GetProcessesByName(Program.cfgdata.player)[0].Kill();
                    startBWTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                }
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
            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.PrimaryScreen;
            RECT rect = new RECT();
            IntPtr hWnd = (IntPtr)GetForegroundWindow();
            GetWindowRect(new HandleRef(null, hWnd), ref rect);

            uint procId = 0;
            GetWindowThreadProcessId(hWnd, out procId);
            string proc = Process.GetProcessById((int)procId).ToString();
            if (screen.Bounds.Width == (rect.right - rect.left) 
                && screen.Bounds.Height == (rect.bottom - rect.top)
                && !proc.Contains(Program.cfgdata.player)
                && !proc.Contains("explorer")
                )
                return true;
            else
                return false;
        }
    }
}
