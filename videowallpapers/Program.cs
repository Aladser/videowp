using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace videowallpapers
{
    internal static class Program
    {
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        public static readonly string shortcut = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\videowallpapers.lnk"; // ярлык автозагрузки 
        public static readonly string cfgpath = Path.GetDirectoryName(Application.ExecutablePath) + "\\config.cfg"; // конфиг
        public static string mpv = Path.GetDirectoryName(Application.ExecutablePath) + "\\mpv\\mpv.exe"; // mpv плеер
        public static string filefilter = "MPV плейлист (*.m3u;*.m3u8;*.pls;*.pls;*.xspf)|*.m3u;*.m3u8;*.pls;*.pls;*.xspf";
        public static ConfigData cfgdata;
        static UserActivityHook globalHook;// хук глобального движения мыши или клавиатуры
        public static MainForm mainform;
        public static BackWork bcgwork;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()   
        {
            Console.WriteLine(Application.ExecutablePath);
            Console.WriteLine(cfgpath);

            // Считывание конфигурационного файла
            if (!File.Exists(Program.cfgpath))
            {
                MessageBox.Show("Файл "+ Program.cfgpath + " не найден. Установлены стандартные настройки");
                cfgdata = new ConfigData();
                ConfigStream.Write(cfgpath, cfgdata);
            }
            else
                cfgdata = ConfigStream.Read(Program.cfgpath);
            if (cfgdata == null)
            {
                MessageBox.Show("Ошибка чтения "+ Program.cfgpath + ". Установлены стандартные настройки", "", MessageBoxButtons.OK);
                cfgdata = new ConfigData();
                ConfigStream.Write(cfgpath, cfgdata);
            }
            if (!File.Exists(mpv))
            {
                MessageBox.Show("Папка mpv не найдена. Программа будет закрыта");
                Process.GetCurrentProcess().Kill();
            }
            // Создание хука
            globalHook = new UserActivityHook();
            globalHook.KeyPress += GlobalKeyPress;
            globalHook.OnMouseActivity += GlobalMouseActivity;
            globalHook.Start(true, true);
            if (Process.GetProcessesByName(Application.ProductName).Length > 1) return;// предотвращение запуска второй копии
            // запуск программы
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bcgwork = new BackWork(cfgdata.period);
            mainform = new MainForm();
            Application.Run();
        }

        /// <summary>
        /// Изменить автозагрузку
        /// </summary>
        /// <param name="isAutoLoader">флаг</param>
        public static void editAutoLoader(bool isAutoLoader)
        {
            // Создание ярлыка
            if (isAutoLoader)
            {
                //Windows Script Host Shell Object
                dynamic shell = Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")));
                try
                {
                    var lnk = shell.CreateShortcut(Program.shortcut);
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
                File.Delete(Program.shortcut);
        }
        // глобальное нажатие клавиатуры
        public static void GlobalKeyPress(object sender, KeyPressEventArgs e)
        {
            Program.bcgwork.stopShowWallpaper();
        }
        // глобальное движение мыши
        public static void GlobalMouseActivity(object sender, MouseEventArgs e)
        {
            Program.bcgwork.stopShowWallpaper();
        }
    }
}
