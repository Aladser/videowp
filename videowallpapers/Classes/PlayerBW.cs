using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using videowp.Classes;

namespace videowp
{
    /// <summary>
    /// Фоновый поток видеоплеера
    /// </summary>
    internal class PlayerBW
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(HandleRef hWnd, [In, Out] ref RECT rect);
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        readonly BackgroundWorker bw = new BackgroundWorker();
        readonly ConfigControl config;
        readonly ProcessStartInfo mpvProc;
        readonly PlaylistControl playlist;
        readonly PlaylistUpdatesBW updateCtrl;
        long downtime;
        long dwt1, dwt2;

        public PlayerBW(ConfigControl config, PlaylistUpdatesBW updateCtrl, ProcessStartInfo mpvProc, PlaylistControl pl)
        {
            this.config = config;
            this.updateCtrl = updateCtrl;
            this.mpvProc = mpvProc;
            this.playlist = pl;
            bw.DoWork += BW_DoWork;
            bw.WorkerSupportsCancellation = true;
        }

        void BW_DoWork(object sender, DoWorkEventArgs e)
        {
            bool isActive = false;

            long startBWTime = GetTimeNow();
            dwt1 = startBWTime;
            downtime = 0;

            mpvProc.Arguments = $"--playlist={playlist.PLAYLIST_PATH}";

            while (true)
            {               
                // выключение фоновой задачи
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                // добавление новых видео
                if(updateCtrl.IsNewData)
                {
                    if (isActive)
                    {
                        foreach (Process proc in Process.GetProcessesByName("mpv")) proc.Kill();
                        playlist.CheckFilesInPlaylist();
                        Process.Start(mpvProc);
                    }
                    else
                        playlist.CheckFilesInPlaylist();
                    updateCtrl.IsNewData = false;
                }
                //поиск другого запущенного приложения в фуллскрине
                if (IsForegroundFullScreen() && config.OverWindows==0)
                {
                    dwt1 = GetTimeNow();
                }
                // запуск обоев
                else if (downtime >= config.GetInactionTime() && !isActive)
                {
                    dwt2 = GetTimeNow();
                    isActive = true;
                    foreach (Process proc in Process.GetProcessesByName("mpv")) proc.Kill(); // убить зависшие процессы
                    Process.Start(mpvProc);
                }
                // прерывание показа обоев
                else if (downtime < config.GetInactionTime() && isActive)
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

        public void Start(){bw.RunWorkerAsync(); }
        public void Stop(){ bw.CancelAsync();}
        public bool IsActive() { return bw.IsBusy; }

        private long GetTimeNow() {return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;}

        public void StopShowWallpaper(){ dwt1 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;}
        // Поиск другой программы в фуллскрине
        [StructLayout(LayoutKind.Sequential)]
        struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

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
