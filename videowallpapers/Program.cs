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

        public static readonly string SHORTCUT = $"{Environment.GetFolderPath(Environment.SpecialFolder.Startup)}\\videowp.lnk"; // ярлык автозагрузки 
        public static bool isNewData = false; // флаг новых видео

        static ConfigControl config;   // конфигурационный файл  
        static PlaylistControl plCtrl; // управление плейлистом
        static UpdateSearchBW updateSearch; // проверка обновлений плейлиста
        static MainForm mainform;
        static PlayerBW bcgwork;
        
        static readonly string mpvPath = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\mpv\\mpv.exe"; // MPV
        static ProcessStartInfo mpvProc; // MPV процесс в Windows
        static UserActivityHook globalHook; // хук глобального движения мыши или клавиатуры

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
                // создание хука
                globalHook = new UserActivityHook();
                globalHook.KeyPress += (object sender, KeyPressEventArgs e) => Program.bcgwork.StopShowWallpaper(); // нажатие клавиши
                globalHook.OnMouseActivity += (object sender, MouseEventArgs e) => Program.bcgwork.StopShowWallpaper(); // движение мыши
                globalHook.Start(true, true);
                // предотвращение запуска второй копии
                if (Process.GetProcessesByName(Application.ProductName).Length > 1) return;
                // запуск программы
                mpvProc = new ProcessStartInfo(mpvPath, @"");
                config = new ConfigControl();

                plCtrl = new PlaylistControl(config.PlaylistFolderPath);
                plCtrl.CheckFilesInPlaylist();
                bcgwork = new PlayerBW(config, mpvProc, plCtrl);
                updateSearch = new UpdateSearchBW(plCtrl, config.UpdateServer);
                updateSearch.Start();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                mainform = new MainForm(config, bcgwork, plCtrl, updateSearch);
                if (InstanceCheck()) Application.Run();
            }      
        }

        public static bool IsShare(){return updateSearch.IsShare();}
        public static void SetShare(string path) { updateSearch.SetShare(path); }
        // Изменить автозагрузку
        public static void EditAutoLoader(bool isAutoLoader)
        {
            // Создание ярлыка
            if (isAutoLoader)
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

        // Проверка запуска второй копии приложения
        static Mutex InstanceCheckMutex;
        static bool InstanceCheck()
        {
            InstanceCheckMutex = new Mutex(true, "videowp", out bool isNew);
            return isNew;
        }
    }
}
