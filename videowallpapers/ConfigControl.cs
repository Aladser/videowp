using System;
using System.IO;
using System.Windows.Forms;

namespace videowp
{
    /// <summary>
    /// Конфигурационный Control
    /// </summary>
    public class ConfigControl
    {
        /// <summary>
        /// путь к конфигурационному файлу
        /// </summary>
        readonly string CONFIG_PATH = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\videowp.cfg";
        /// <summary>
        /// путь плейлиста
        /// </summary>
        public string PlaylistPath
        {
            set {
                plpath = value;
                this.writeToFile();
            }
            get { return plpath; }
        }
        private string plpath;
        /// <summary>
        /// Время бездействия
        /// </summary>
        public int InactionNumber
        {
            set {
                period = value;
                this.writeToFile();
            }
            get { return period; }
        }
        private int period;
        /// <summary>
        /// автозапуск обоев
        /// </summary>
        public bool AutoShow
        {
            set {
                autoshow = value ? 1 : 0;
                this.writeToFile();
            }
            get { return autoshow==1 ? true : false; }
        }
        private int autoshow;
        /// <summary>
        /// повех всех окон
        /// </summary>
        public bool OverWindows
        {
            set
            {
                overwindows = value ? 1 : 0;
                this.writeToFile();
            }
            get { return overwindows == 1 ? true : false; }
        }
        private int overwindows;

        public ConfigControl()
        {
            if (!File.Exists(CONFIG_PATH))
            {
                MessageBox.Show($"Файл {CONFIG_PATH} не найден. Будут установлены стандартные настройки");
                plpath = "";
                period = 0;
                autoshow = 0;
                overwindows = 0;
                this.writeToFile();
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
                overwindows = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));         
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
        private void writeToFile()
        {
            StreamWriter writer = new StreamWriter(CONFIG_PATH, false);
            string text = $"playerpath = {plpath}\n";
            text += $"period = {period}\n";
            text += $"autoshow = {autoshow}\n";
            text += $"overWindows = {overwindows}\n";
            writer.WriteLine(text);
            writer.Close();
        }
    }
}
