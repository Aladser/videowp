using System;
using System.IO;
using System.Windows.Forms;

namespace videowp
{
    /// <summary>
    /// Конфигурация программы
    /// </summary>
    internal class ConfigControl
    {
        /// <summary>
        /// путь к конфигурационному файлу
        /// </summary>
        readonly string CONFIG_PATH = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\videowp.cfg";
        public string PlaylistFolderPath
        {
            set {
                plFolderPath = value;
                this.WriteToFile();
            }
            get { return plFolderPath; }
        }
        /// <summary>
        /// папка плейлиста
        /// </summary>
        string plFolderPath;

        public int InactionIndex
        {
            set {
                inactonIndex = value;
                this.WriteToFile();
            }
            get { return inactonIndex; }
        }
        /// <summary>
        /// индекс времени бездействия
        /// </summary>
        int inactonIndex;

        // получить время бездействия в мс
        public int GetInactionTime()
        {
            double[] inactionTimeNumberList = { 0.01, 1, 3, 5, 10, 15 };
            return (int)(inactionTimeNumberList[inactonIndex] * 60000);
        }

        public int AutoShow
        {
            set {
                autoshow = value;
                this.WriteToFile();
            }
            get { return autoshow; }
        }
        /// <summary>
        /// автозапуск обоев
        /// </summary>
        int autoshow;

        public int OverWindows
        {
            set
            {
                overwindows = value;
                this.WriteToFile();
            }
            get { return overwindows; }
        }
        /// <summary>
        /// поверх всех окон
        /// </summary>
        int overwindows;

        public string UpdateServer
        {
            set
            {
                updateSrv = value;
                this.WriteToFile();
            }
            get { return updateSrv; }
        }
        /// <summary>
        /// Путь к шаре с видео
        /// </summary>
        string updateSrv;
        public int UpdateTime{
            set { 
                updatetime = value;
                this.WriteToFile();
            }
            get { return updatetime; }
        }
        /// <summary>
        /// время проверки обновлений
        /// </summary>
        int updatetime;

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
                updateSrv = "";
                updatetime = 4;
                this.WriteToFile();
            }              
        }

        // Чтение данных из конфиг.файла
        void ReadFromFile()
        {
            StreamReader reader = new StreamReader(CONFIG_PATH);
            try
            {
                // папка плейлиста
                string line = reader.ReadLine();
                plFolderPath = line.Substring(line.IndexOf("= ") + 2);
                if (!plFolderPath.Equals(""))
                {
                    if (!Directory.Exists(plFolderPath))
                    {
                        plFolderPath = "";
                        this.WriteToFile();
                    }
                }
                // время бездейтсвия
                line = reader.ReadLine();
                inactonIndex = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));
                // автопоказ
                line = reader.ReadLine();
                autoshow = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));
                // поверх всех окон
                line = reader.ReadLine();
                overwindows = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));
                // сетевая папка
                line = reader.ReadLine();
                updateSrv = line.Substring(line.IndexOf("= ") + 2);
                if (!updateSrv.Equals(""))
                {
                    if (!Directory.Exists(plFolderPath))
                    {
                        updateSrv = "";
                        this.WriteToFile();
                    }
                }
                // время обновления из сетевой папки видеофайлов
                line = reader.ReadLine();
                updatetime = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            reader.Close();
        }

        // Запись данных в конфиг.файл
        void WriteToFile()
        {
            StreamWriter writer = new StreamWriter(CONFIG_PATH, false);
            string text = $"plfolderpath = {plFolderPath}\n";
            text += $"period = {inactonIndex}\n";
            text += $"autoshow = {autoshow}\n";
            text += $"over_windows = {overwindows}\n";
            text += $"updates = {updateSrv}\n";
            text += $"update_time = {updatetime}\n";
            writer.WriteLine(text);
            writer.Close();
        }
    }
}
