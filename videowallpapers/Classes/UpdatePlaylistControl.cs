using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace videowp.Classes
{
    internal class UpdatePlaylistControl
    {
        readonly string sharePath;
        readonly string playlistPath;

        public UpdatePlaylistControl(string sharePath, string playlistPath)
        {
            this.sharePath = sharePath;
            this.playlistPath = playlistPath;
        }

        // проверка обновлений
        public void CheckUpdates()
        {
            List<string> shareFilenames = GetVideosFromFolder(sharePath);
            List<string> playlistFilenames = GetVideosFromFolder(Path.GetDirectoryName(playlistPath));
            bool isNewVideos = !(new HashSet<string>(shareFilenames).SetEquals(new HashSet<string>(playlistFilenames)));

            if (isNewVideos)
            {
                for (int i = 0; i < shareFilenames.Count; i++)
                {
                    string isFile = playlistFilenames.Find(x => x.Equals(shareFilenames[i]));
                    if (isFile == null)
                    {
                        string oldpath = $"{sharePath}\\{shareFilenames[i]}";
                        string newpath = $"{Path.GetDirectoryName(playlistPath)}\\{shareFilenames[i]}";
                        File.Copy(oldpath, newpath);
                    }
                }
                string playlist = Path.GetFileName(playlistPath);
                File.Copy($"{sharePath}\\{playlist}", playlistPath, true);
            }
        }

        /// <summary>
        /// получить список видео из папки
        /// </summary>
        private List<string> GetVideosFromFolder(string sharePath)
        {
            List<string> files = Directory.GetFiles(sharePath).ToList<string>();
            for (int i = 0; i < files.Count; i++) files[i] = Path.GetFileName(files[i]);
            int index = files.IndexOf(Path.GetFileName(playlistPath)); 
            files.RemoveAt(index); // удаление файла плейлиста
            return files;
        }
    }
}
