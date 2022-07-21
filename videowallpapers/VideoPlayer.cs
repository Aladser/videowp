using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace videowallpapers
{
    public class VideoPlayer
    {
        // форматы плейлистов
        static string mpcfilter = "MPC плейлист (*.mpcpl;*pls;*asx;*m3u)|*.mpcpl;*pls;*asx;*m3u|Все файлы (*.*)|*.*";
        static string kmpfilter = "KMP плейлист (*.kpl;*pls;*asx;*m3u)|*.kpl;*pls;*asx;*m3u|Все файлы (*.*)|*.*";
        static string vlcfilter = "VLC плейлист (*.xspf;*.m3u;*.m3u8;*.html)|*.xspf;*.m3u;*.m3u8;*.html|Все файлы (*.*)|*.*";
        static string lafilter = "LA плейлист (*.lap;*.m3u)|*.lap;*.m3u|Все файлы (*.*)|*.*";
        public static readonly string[] playerFilters = { mpcfilter, kmpfilter, vlcfilter, lafilter };

        static string[] mpcExtensions = { ".mpcpl", ".pls", ".asx", ".m3u" };
        static string[] kmpExtensions = { ".kpl", ".pls", ".asx", ".m3u" };
        static string[] vlcExtensions = { ".xspf", ".m3u", ".m3u8", ".html" };
        static string[] laExtensions = { ".lap", ".m3u" };
        public static readonly string[][] playerExtensions = { mpcExtensions, kmpExtensions, vlcExtensions, laExtensions };

        string[] playerProcesses = { "mpc-hc64", "KMPlayer64", "vlc", "LA" };
        int procIndex;
        string plpath;

        public VideoPlayer(int index, string pl)
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
    }
}
