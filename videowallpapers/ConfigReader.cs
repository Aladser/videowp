using System;
using System.IO;

namespace videowallpapers
{
    internal struct Configuration
    {
        public int period;
        public int autoloader;
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
        public static Configuration Read(string path)
        {
            Configuration rslt;
            StreamReader reader = new StreamReader(path);
            string line = reader.ReadLine();
            rslt.period = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));
            line = reader.ReadLine();
            rslt.autoloader = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));
            line = reader.ReadLine();
            rslt.plpath = line.Substring(line.IndexOf("= ") + 2);
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
            String text;
            text = "period = " + period + "\n";
            text += "autoload = " + autoloader + "\n";
            text += "playerpath = " + plpath + "\n";
            writer.WriteLine(text);
            writer.Close();
        }
    }
}
