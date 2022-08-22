using System;
using System.IO;

namespace videowp
{
    /// <summary>
    /// Данные конфиг.файла
    /// </summary>
    internal class ConfigData
    {
        public string plpath;
        public string player;
        public int period;
        public int autoshow;
        public ConfigData()
        {           
            plpath = "";
            player = "mpv";
            period = 0;
            autoshow = 0;
        }
    }
    /// <summary>
    /// Класс доступа к конфиг файлу
    /// </summary>
    internal static class ConfigStream
    {
        /// <summary>
        /// Чтение данных из конфиг.файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static ConfigData Read(string path)
        {
            ConfigData rslt;
            StreamReader reader = new StreamReader(path);
            try
            {
                rslt = new ConfigData();                
                string line = reader.ReadLine();
                rslt.plpath = line.Substring(line.IndexOf("= ") + 2);
                line = reader.ReadLine();
                rslt.player = line.Substring(line.IndexOf("= ") + 2);
                line = reader.ReadLine();
                rslt.period = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));
                line = reader.ReadLine();
                rslt.autoshow = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));
                if (!File.Exists(rslt.plpath))
                    rslt.plpath = "";
            }
            catch (Exception)
            {
                rslt = null;
            }
            reader.Close();
            return rslt;
        }
        /// <summary>
        /// Запись данных в конфиг.файл
        /// </summary>
        /// <param name="path"> Путь конф.файла </param>
        /// <param name="data"> ConfigFata </param>
        public static void Write(string path, ConfigData data)
        {
            StreamWriter writer = new StreamWriter(path, false);
            string text = "playerpath = " + data.plpath + "\n";
            text += "player = " + data.player + "\n";
            text += "period = " + data.period + "\n";
            text += "autoshow = " + data.autoshow + "\n";
            writer.WriteLine(text);
            writer.Close();
        }
    }
}
