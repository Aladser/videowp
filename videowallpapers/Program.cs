using System;
using System.IO;
using System.Windows.Forms;

namespace videowallpapers
{
    internal static class Program
    {
        public static readonly string cfgpath = Path.GetDirectoryName(Application.ExecutablePath) + "\\config.cfg"; // путь конфига
        public static readonly string logpath = Path.GetDirectoryName(Application.ExecutablePath) + "\\log.txt"; // путь лога
        public static readonly string shortcut = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\videowallpapers.lnk"; // ярлык автозагрузки 
        public static bool isConfigEdited = false; // флаг проверки правки конфиг.файла
        public static ConfigData cfgdata;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Считывание конфигурационного файла
            if (!File.Exists(Program.cfgpath))
            {
                MessageBox.Show("Файл не найден. Установлены стандартные настройки");
                cfgdata = new ConfigData();
                isConfigEdited = true;
            }
            else
                cfgdata = ConfigStream.Read(Program.cfgpath);
            if (cfgdata == null)
            {
                MessageBox.Show("Ошибка чтения конфиг.файла. Установлены стандартные настройки", "", MessageBoxButtons.OK);
                cfgdata = new ConfigData();
                isConfigEdited = true;
            }



            if (System.Diagnostics.Process.GetProcessesByName(Application.ProductName).Length > 1)
            {
                return;
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                new MainForm();
                Application.Run();
            }
        }
    }
}
