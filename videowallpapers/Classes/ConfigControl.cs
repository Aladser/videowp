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
                this.WriteToFile();
            }
            get { return plpath; }
        }
        string plpath;

        /// <summary>
        /// индекс времени бездействия
        /// </summary>
        public int InactionIndex
        {
            set {
                inactonIndex = value;
                this.WriteToFile();
            }
            get { return inactonIndex; }
        }
        int inactonIndex;
        /// <summary>
        /// получить время бездействия в мс
        /// </summary>
        /// <returns></returns>
        public int GetInactionTime()
        {
            double[] inactionTimeNumberList = { 0.05, 1, 3, 5, 10, 15 };
            return (int)(inactionTimeNumberList[inactonIndex] * 60000);
        }

        /// <summary>
        /// автозапуск обоев
        /// </summary>
        public int AutoShow
        {
            set {
                autoshow = value;
                this.WriteToFile();
            }
            get { return autoshow; }
        }
        int autoshow;
        /// <summary>
        /// повех всех окон
        /// </summary>
        public int OverWindows
        {
            set
            {
                overwindows = value;
                this.WriteToFile();
            }
            get { return overwindows; }
        }
        int overwindows;

        public ConfigControl()
        {
            if (!File.Exists(CONFIG_PATH))
            {
                MessageBox.Show("Конфигурационный файл не найден. Будут установлены стандартные настройки");
                plpath = "";
                inactonIndex = 0;
                autoshow = 0;
                overwindows = 0;
                this.WriteToFile();
            }
            else
                this.ReadFromFile();
        }

        /// <summary>
        /// Чтение данных из конфиг.файла
        /// </summary>
        void ReadFromFile()
        {
            StreamReader reader = new StreamReader(CONFIG_PATH);
            try
            {
                string line = reader.ReadLine();
                plpath = line.Substring(line.IndexOf("= ") + 2);
                if (!File.Exists(plpath)) plpath = "";

                line = reader.ReadLine();
                inactonIndex = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));

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
        void WriteToFile()
        {
            StreamWriter writer = new StreamWriter(CONFIG_PATH, false);
            string text = $"playerpath = {plpath}\n";
            text += $"period = {inactonIndex}\n";
            text += $"autoshow = {autoshow}\n";
            text += $"overWindows = {overwindows}\n";
            writer.WriteLine(text);
            writer.Close();
        }

        
    }
}
