using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace videowallpapers
{
    public class VideoPlayerManager
    {
        // форматы плейлистов
        static string mpcfilter = "MPC плейлист (*.mpcpl;*pls;*asx;*m3u)|*.mpcpl;*pls;*asx;*m3u";
        static string kmpfilter = "KMP плейлист (*.kpl;*pls;*asx;*m3u)|*.kpl;*pls;*asx;*m3u";
        static string vlcfilter = "VLC плейлист (*.xspf;*.m3u;*.m3u8;*.html)|*.xspf;*.m3u;*.m3u8;*.html";
        static string lafilter = "LA плейлист (*.lap;*.m3u)|*.lap;*.m3u";
        static string smpfilter = "SMP плейлист (*.m3u;*.m3u8;*.pls;*.pls;*.xspf)|*.m3u;*.m3u8;*.pls;*.pls;*.xspf";
        static string mpfilter = "Mplayer плейлист (*.m3u;*.m3u8;*.pls;*.pls;*.xspf)|*.m3u;*.m3u8;*.pls;*.pls;*.xspf";
        public static readonly string[] playerFilters = { mpcfilter, kmpfilter, vlcfilter, lafilter, smpfilter, mpfilter};

        static string[] mpcExtensions = { ".mpcpl", ".pls", ".asx", ".m3u" };
        static string[] kmpExtensions = { ".kpl", ".pls", ".asx", ".m3u" };
        static string[] vlcExtensions = { ".xspf", ".m3u", ".m3u8", ".html" };
        static string[] laExtensions = { ".lap", ".m3u" };
        static string[] smpExtensions = { ".m3u", ".m3u8", ".pls", ".pls", ".xspf" };
        static string[] mpExtensions = { ".m3u", ".mpcpl" };
        public static readonly string[][] playerExtensions = { mpcExtensions, kmpExtensions, vlcExtensions, laExtensions, smpExtensions, mpExtensions };

        string[] playerProcesses = { "mpc-hc64", "KMPlayer64", "vlc", "LA", "smplayer", "mplayer" };
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
