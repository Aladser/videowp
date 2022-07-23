using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace videowallpapers
{
    internal static class Program
    {
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        public static int widthScreen = Screen.PrimaryScreen.Bounds.Size.Width;
        public static string mplayer = Path.GetDirectoryName(Application.ExecutablePath) + "\\MPlayer\\mplayer.exe";
        public static Process mplayerPr = new Process();
        public static readonly string cfgpath = Path.GetDirectoryName(Application.ExecutablePath) + "\\config.cfg"; // путь конфига
        public static ConfigData cfgdata;
        public static readonly string logpath = Path.GetDirectoryName(Application.ExecutablePath) + "\\log.txt"; // путь лога
        public static readonly string shortcut = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\videowallpapers.lnk"; // ярлык автозагрузки 
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Опеределение настоящей ширины экрана
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);
            float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;
            widthScreen = (int)((float)(Screen.PrimaryScreen.Bounds.Size.Width) * ScreenScalingFactor);
            Console.WriteLine(widthScreen);
            // Считывание конфигурационного файла
            if (!File.Exists(Program.cfgpath))
            {
                MessageBox.Show("Файл не найден. Установлены стандартные настройки");
                cfgdata = new ConfigData();
                ConfigStream.Write(cfgpath, cfgdata);
            }
            else
                cfgdata = ConfigStream.Read(Program.cfgpath);
            if (cfgdata == null)
            {
                MessageBox.Show("Ошибка чтения конфиг.файла. Установлены стандартные настройки", "", MessageBoxButtons.OK);
                cfgdata = new ConfigData();
                ConfigStream.Write(cfgpath, cfgdata);
            }
            // Процесс плеера
            mplayerPr.StartInfo.FileName = Program.mplayer;
            // предотвращение запуска второй копии
            if (System.Diagnostics.Process.GetProcessesByName(Application.ProductName).Length > 1) return;
            // запуск программы
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                new MainForm();
                Application.Run();
            }
        }

        /// <summary>
        /// Изменить автозапуск
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
                        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(lnk);
                    }
                }
                finally
                {
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shell);
                }
            }
            // Удаление ярлыка
            else
                File.Delete(Program.shortcut);
        }
        // Получение настоящей ширины экрана
        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117,
        }
        private static float getScalingFactor()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);

            float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;

            return ScreenScalingFactor;
        }
    }
}
