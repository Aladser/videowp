using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace videowallpapers
{
    public class VideoPlayerManager
    {
        // форматы плейлистов
        static string mpcfilter = "MPC плейлист (*.mpcpl;*pls;*asx;*m3u)|*.mpcpl;*pls;*asx;*m3u";
        static string mpfilter = "MPlayer плейлист (*.m3u;*.m3u8;*.pls;*.pls;*.xspf)|*.m3u;*.m3u8;*.pls;*.pls;*.xspf";
        static string lafilter = "LA плейлист (*.lap;*.m3u)|*.lap;*.m3u";
        static string kmpfilter = "KMP плейлист (*.kpl;*pls;*asx;*m3u)|*.kpl;*pls;*asx;*m3u";
        static string vlcfilter = "VLC плейлист (*.xspf;*.m3u;*.m3u8;*.html)|*.xspf;*.m3u;*.m3u8;*.html";

        public static readonly string[] playerFilters = { mpcfilter, mpfilter, mpfilter, lafilter, kmpfilter, vlcfilter };

        static string[] mpcExtensions = { ".mpcpl", ".pls", ".asx", ".m3u" };
        static string[] mpExtensions = { ".m3u", ".mpcpl" };
        static string[] laExtensions = { ".lap", ".m3u" };
        static string[] kmpExtensions = { ".kpl", ".pls", ".asx", ".m3u" };
        static string[] vlcExtensions = { ".xspf", ".m3u", ".m3u8", ".html" };

        public static readonly string[][] playerExtensions = { mpcExtensions, mpExtensions, mpExtensions, laExtensions, kmpExtensions, vlcExtensions };

        string[] playerProcesses = { "mpc-hc64", "mplayer", "smplayer", "LA", "KMPlayer64", "vlc" };
        int procIndex;
        string plpath;

        public VideoPlayerManager(int index, string pl)
        {
            procIndex = index;
            plpath = pl;
        }

        public void setActivePlayer(int index)
        {
            procIndex = index;
        }
        public int getActivePlayerNumber()
        {
            return procIndex;
        }
        public string getActivePlayer()
        {
            return playerProcesses[procIndex];
        }
        public void setPlaylist(string pl)
        {
            plpath = pl;
        }
        public string getPlaylist()
        {
            return plpath;
        }
        public string getActivePlayerFilter()
        {
            return playerFilters[procIndex];
        }
    }
}
