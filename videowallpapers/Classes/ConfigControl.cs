using System;
using System.IO;
using System.Windows.Forms;

namespace videowp
{
    // Конфигурационный Control
    public class ConfigControl
    {
        // путь к конфигурационному файлу
        readonly string CONFIG_PATH = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\videowp.cfg";

        // Плейлист
        public string PlaylistFolderPath
        {
            set {
                plFolderPath = value;
                this.WriteToFile();
            }
            get { return plFolderPath; }
        }
        string plFolderPath;

        // индекс времени бездействия
        public int InactionIndex
        {
            set {
                inactonIndex = value;
                this.WriteToFile();
            }
            get { return inactonIndex; }
        }
        int inactonIndex;
        // получить время бездействия в мс
        public int GetInactionTime()
        {
            double[] inactionTimeNumberList = { 0.05, 1, 3, 5, 10, 15 };
            return (int)(inactionTimeNumberList[inactonIndex] * 60000);
        }

        // автозапуск обоев
        public int AutoShow
        {
            set {
                autoshow = value;
                this.WriteToFile();
            }
            get { return autoshow; }
        }
        int autoshow;
        // повех всех окон
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
        // Путь к шаре с видео
        public string UpdateServer
        {
            set
            {
                updateSrv = value;
                this.WriteToFile();
            }
            get { return updateSrv; }
        }
        string updateSrv;

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
                this.WriteToFile();
            }              
        }

        // Чтение данных из конфиг.файла
        void ReadFromFile()
        {
            StreamReader reader = new StreamReader(CONFIG_PATH);
            try
            {
                string line = reader.ReadLine();
                plFolderPath = line.Substring(line.IndexOf("= ") + 2);
                if (!plFolderPath.Equals(""))
                {
                    if (!Directory.Exists(plFolderPath)) Directory.CreateDirectory(plFolderPath);
                }

                line = reader.ReadLine();
                inactonIndex = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));

                line = reader.ReadLine();
                autoshow = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));

                line = reader.ReadLine();
                overwindows = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));

                line = reader.ReadLine();
                updateSrv = line.Substring(line.IndexOf("= ") + 2);
                if (!updateSrv.Equals(""))
                {
                    if (!Directory.Exists(plFolderPath)) Directory.CreateDirectory(updateSrv);
                }
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
            text += $"overWindows = {overwindows}\n";
            text += $"updates = {updateSrv}\n";
            writer.WriteLine(text);
            writer.Close();
        }
    }
}
