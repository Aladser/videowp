using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace videowp.Classes
{

    internal abstract class VideoFileFunctions
    {   
        /// <summary>
        /// Проверка целостности видеофайла
        /// </summary>
        public static bool IsIntegrity(string path)
        {
            byte[] byteArr = File.ReadAllBytes(path);
            int fivePercents = (int)(byteArr.Length * 0.05);
            int count = 0;

            for (int j = byteArr.Length-1; j > -1; j--)
            {
                if (byteArr[j] == 0) count++;
                if (count > fivePercents) return false;
            }
            return true;
        }
        /// <summary>
        /// Получить файлы из папки
        /// </summary>
        /// <param name="path">путь папки</param>
        /// <param name="onlyFilename"> получить только имена файлов?</param>
        /// <returns></returns>
        public static List<string> GetVideofilesFromFolder(string path, bool onlyFilename = false)
        {
            List<string> dirFiles = Directory.GetFiles(path).ToList<string>();
            string ext;
            string[] extList = { ".mp4", ".m4v", ".mkv", ".avi" };
            for (int i = 0; i < dirFiles.Count; i++)
            {
                ext = Path.GetExtension(dirFiles[i]);
                if (!extList.Contains(ext)) dirFiles.RemoveAt(i);
            }
            if (onlyFilename)
                for (int i = 0; i < dirFiles.Count; i++)
                    dirFiles[i] = Path.GetFileName(dirFiles[i]);
            return dirFiles;
        }

    }
}
