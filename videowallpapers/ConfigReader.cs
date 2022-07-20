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
            autoload = 0;
            plpath = "";
            period = 0;
        }
        public int period;
        public int autoload;
        public string plpath;
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
                rslt.autoload = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));
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
        /// <param name="autoloader"></param>
        /// <param name="path"></param>
        public static void Write(string path, int period, int autoloader, string plpath)
        {
            StreamWriter writer = new StreamWriter(path, false);
            String text = "autoload = " + autoloader + "\n";
            text += "playerpath = " + plpath + "\n";
            text += "period = " + period + "\n";
            writer.WriteLine(text);
            writer.Close();
        }
    }
}
