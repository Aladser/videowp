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
        public string PlaylistFolderPath
        {
            set {
                plFolderPath = value;
                this.WriteToFile();
            }
            get { return plFolderPath; }
        }
        string plFolderPath;

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
        /// <summary>
        /// Путь к шаре с видео
        /// </summary>
        public string Updates
        {
            set
            {
                updates = value;
                this.WriteToFile();
            }
            get { return updates; }
        }
        string updates;

        public ConfigControl()
        {
            if (File.Exists(CONFIG_PATH))
            {
                this.ReadFromFile();
            }
            else
            {
                MessageBox.Show("Конфигурационный файл не найден. Установлены стандартные настройки");
                plFolderPath = "";
                inactonIndex = 0;
                autoshow = 0;
                overwindows = 0;
                updates = "";
                this.WriteToFile();
            }              
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
                plFolderPath = line.Substring(line.IndexOf("= ") + 2);
                if (!Directory.Exists(plFolderPath)) plFolderPath = "";

                line = reader.ReadLine();
                inactonIndex = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));

                line = reader.ReadLine();
                autoshow = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));

                line = reader.ReadLine();
                overwindows = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));

                line = reader.ReadLine();
                updates = line.Substring(line.IndexOf("= ") + 2);
                if (!Directory.Exists(updates)) updates = "";
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            reader.Close();
        }
        /// <summary>
        /// Запись данных в конфиг.файл
        /// </summary>
        void WriteToFile()
        {
            StreamWriter writer = new StreamWriter(CONFIG_PATH, false);
            string text = $"plfolderpath = {plFolderPath}\n";
            text += $"period = {inactonIndex}\n";
            text += $"autoshow = {autoshow}\n";
            text += $"overWindows = {overwindows}\n";
            text += $"updates = {updates}\n";
            writer.WriteLine(text);
            writer.Close();
        }
    }
}
