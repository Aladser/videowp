using System;
using System.IO;
using System.Windows.Forms;

namespace videowp
{
    /// <summary>
    /// Конфигурационный файл
    /// </summary>
    public class ConfigControl
    {
        /// <summary>
        /// путь к конфигурационному файлу
        /// </summary>
        readonly string CONFIG_PATH = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\videowp.cfg";

        public string plpath;
        public int period;
        public int autoshow;
        public int overWindows;

        public ConfigControl()
        {
            if (!File.Exists(CONFIG_PATH))
            {
                MessageBox.Show($"Файл {CONFIG_PATH} не найден. Установлены стандартные настройки");
                plpath = "";
                period = 0;
                autoshow = 0;
                overWindows = 0;
                this.Write();
            }
            else
                this.Read();
        }

        /// <summary>
        /// Чтение данных из конфиг.файла
        /// </summary>
        public void Read()
        {
            StreamReader reader = new StreamReader(CONFIG_PATH);
            try
            {
                string line = reader.ReadLine();
                plpath = line.Substring(line.IndexOf("= ") + 2);
                if (!File.Exists(plpath)) plpath = "";

                line = reader.ReadLine();
                period = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));

                line = reader.ReadLine();
                autoshow = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));

                line = reader.ReadLine();
                overWindows = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));         
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            reader.Close();
        }
        /// <summary>
        /// Запись данных в конфиг.файл
        /// </summary>
        public void Write()
        {
            StreamWriter writer = new StreamWriter(CONFIG_PATH, false);
            string text = "playerpath = " + plpath + "\n";
            text += "period = " + period + '\n';
            text += "autoshow = " + autoshow + '\n';
            text += "overWindows = " + overWindows + '\n';
            writer.WriteLine(text);
            writer.Close();
        }
    }
}
