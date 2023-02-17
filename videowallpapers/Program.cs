using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using videowp.Classes;

namespace videowp
{
    internal static class Program
    {
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
     
        static ConfigControl config;        // конфигурационный файл  
        static PlaylistControl plCtrl;      // управление плейлистом
        static UpdateCheckBW updateCtrl;    // управляет обновленями плейлиста       
        static PlayerBW playerBW;
        public static MainForm mainForm;

        static readonly string mpvPath = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\player\\mpv.exe"; // MPV
        public static readonly string SHORTCUT = $"{Environment.GetFolderPath(Environment.SpecialFolder.Startup)}\\videowp.lnk"; // ярлык автозагрузки 
        static ProcessStartInfo mpvProc; // MPV процесс в Windows
        static UserActivityHook globalHook; // хук глобального движения мыши или клавиатуры

        // ярлык автозагрузки
        public static bool IsAutoLoader
        {
            set
            {
                if (value)
                {
                    //Windows Script Host Shell Object
                    dynamic shell = Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")));
                    try
                    {
                        var lnk = shell.CreateShortcut(Program.SHORTCUT);
                        try
                        {
                            lnk.TargetPath = Application.ExecutablePath;
                            lnk.IconLocation = "shell32.dll, 1";
                            lnk.Save();
                        }
                        finally
                        {
                            Marshal.FinalReleaseComObject(lnk);
                        }
                    }
                    finally
                    {
                        Marshal.FinalReleaseComObject(shell);
                    }
                }
                // Удаление ярлыка
                else
                    File.Delete(Program.SHORTCUT);
            }
        }

        [STAThread]
        static void Main()   
        {
            // проверка папки mpv
            if (!File.Exists(mpvPath))
            {
                MessageBox.Show("Папка mpv не найдена. Невозможен запуск программы");
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                // предотвращение запуска второй копии
                if (Process.GetProcessesByName(Application.ProductName).Length > 1) return;
                // запуск программы
                mpvProc = new ProcessStartInfo(mpvPath, @"");
                config = new ConfigControl();

                plCtrl = new PlaylistControl(config.PlaylistFolderPath);
                if(!plCtrl.IsEmpty()) plCtrl.CheckFilesInPlaylist();
                updateCtrl = new UpdateCheckBW(config, plCtrl);
                playerBW = new PlayerBW(config, updateCtrl, mpvProc, plCtrl);

                // создание хука
                globalHook = new UserActivityHook();
                globalHook.KeyPress += (object sender, KeyPressEventArgs e) => playerBW.StopShowWallpaper(); // нажатие клавиши
                globalHook.OnMouseActivity += (object sender, MouseEventArgs e) => playerBW.StopShowWallpaper(); // движение мыши
                globalHook.Start(true, true);

                if(!plCtrl.playlistFolderPath.Equals("") && !config.UpdateServer.Equals("")) updateCtrl.Start();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                mainForm = new MainForm(config, playerBW, plCtrl, updateCtrl);
                if (InstanceCheck()) Application.Run();               
            }      
        }

        // Проверка запуска второй копии приложения
        static Mutex InstanceCheckMutex;
        static bool InstanceCheck()
        {
            InstanceCheckMutex = new Mutex(true, "videowp", out bool isNew);
            return isNew;
        }
    }
}
