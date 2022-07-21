using System;
using System.IO;

namespace videowallpapers
{
    /// <summary>
    /// Данные конфиг.файла
    /// </summary>
    internal class ConfigData
    {
        public ConfigData()
        {           
            autoshow = 0;
            plpath = "";
            period = 0;
        }
        public int period;
        public int autoshow;
        public string plpath;
    }
    /// <summary>
    /// Класс доступа к конфиг файлу
    /// </summary>
    internal static class ConfigStream
    {
        public const int AUTOSHOW = 0;
        public const int PLAYLIST = 1;
        public const int PERIOD = 2;
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
        /// <param name="period"></param>
        /// <param name="autoshow"></param>
        /// <param name="path"></param>
        public static void Write(string path, int period, bool autoshow, string plpath)
        {
            StreamWriter writer = new StreamWriter(path, false);
            String text = "autoshow = " + (autoshow?"1":"0") + "\n";
            text += "playerpath = " + plpath + "\n";
            text += "period = " + period + "\n";
            writer.WriteLine(text);
            writer.Close();
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
            text += "playerpath = " + data.plpath + "\n";
            text += "period = " + data.period + "\n";
            writer.WriteLine(text);
            writer.Close();
        }
    }
}
