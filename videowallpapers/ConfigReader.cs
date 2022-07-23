using System;
using System.IO;

namespace videowallpapers
{
    /// <summary>
    /// Данные конфиг.файла
    /// </summary>
    internal class ConfigData
    {
        public int autoshow;
        public int player;
        public string plpath;
        public ConfigData()
        {           
            autoshow = 0;
            player = 0;
            plpath = "";
            period = 0;
        }
        public int period;
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
                rslt.autoshow = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));
                line = reader.ReadLine();
                rslt.player = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));
                line = reader.ReadLine();
                rslt.plpath = line.Substring(line.IndexOf("= ") + 2);
                line = reader.ReadLine();
                rslt.period = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));
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
            String text = "autoshow = " + data.autoshow + "\n";
            text += "player = " + data.player + "\n";
            text += "playerpath = " + data.plpath + "\n";
            text += "period = " + data.period + "\n";
            writer.WriteLine(text);
            writer.Close();
        }
    }
}
