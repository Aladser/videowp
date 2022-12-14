﻿using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace videowp
{
    internal static class Program
    {
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        /// <summary>
        /// конфигурационный файл
        /// </summary>
        public static ConfigControl config = new ConfigControl();
        public static readonly string shortcut = $"{Environment.GetFolderPath(Environment.SpecialFolder.Startup)}\\videowp.lnk"; // ярлык автозагрузки 

        public static string mpvPath = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\mpv\\mpv.exe"; // MPV
        public static ProcessStartInfo mpvProc; // MPV процесс в Windows
        public static string filefilter = "видеоплейлисты (*.m3u;*.m3u8;*.pls;*)|*.m3u;*.m3u8;*.pls";

        static UserActivityHook globalHook;// хук глобального движения мыши или клавиатуры
        public static MainForm mainform;
        public static BackWork bcgwork;      
        // Проверка запуска второй копии приложения
        static Mutex InstanceCheckMutex;
        static bool InstanceCheck()
        {
            InstanceCheckMutex = new Mutex(true, "videowp", out bool isNew);
            return isNew;
        }
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()   
        {
            // поиск папки mpv
            if (!File.Exists(mpvPath))
            {
                MessageBox.Show("Папка mpv не найдена. Программа будет закрыта");
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                mpvProc = new ProcessStartInfo(Program.mpvPath, @"");
            }
            // Создание хука
            globalHook = new UserActivityHook();
            globalHook.KeyPress += (object sender, KeyPressEventArgs e) => Program.bcgwork.StopShowWallpaper(); // нажатие клавиши
            globalHook.OnMouseActivity += (object sender, MouseEventArgs e) => Program.bcgwork.StopShowWallpaper(); // движение мыши
            globalHook.Start(true, true);
            if (Process.GetProcessesByName(Application.ProductName).Length > 1) return;// предотвращение запуска второй копии
            // запуск программы
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bcgwork = new BackWork();
            mainform = new MainForm();
            if (InstanceCheck()) Application.Run();
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
    }
}
