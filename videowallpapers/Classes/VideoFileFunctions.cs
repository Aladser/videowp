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
        // проверка на нули последнего процента файла
        public static bool IsIntegrity(string path)
        {
            try
            {
                byte[] byteArr = File.ReadAllBytes(path);
                int percent = (int)(byteArr.Length * 0.01);
                int zeroCount = 0;

                for (int j = byteArr.Length - 1; j > byteArr.Length - percent - 1; j--) if (byteArr[j] == 0) zeroCount++;
                return zeroCount != percent;
            } catch(System.IO.IOException)
            {
                return false;
            }

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
