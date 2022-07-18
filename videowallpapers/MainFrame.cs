using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace videowallpapers
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// хук глобального движения мыши или клавиатуры
        /// </summary>
        UserActivityHook globalHook;
        /// <summary>
        /// фоновая задача показа обоев
        /// </summary>
        BackWork backwork;
        readonly string cfgpath = Path.GetDirectoryName(Application.ExecutablePath) + "\\config.cfg"; // путь конфига
        readonly string logpath = Path.GetDirectoryName(Application.ExecutablePath) + "\\log.txt"; // путь лога
        readonly string shortcut= Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\videowallpapers.lnk"; // ярлык автозагрузки 
        // форматы плейлистов
        string mpcfilter = "MPC плейлист (*.mpcpl;*pls;*asx;*m3u)|*.mpcpl;*pls;*asx;*m3u|Все файлы (*.*)|*.*";
        string kmpfilter = "KMP плейлист (*.kpl;*pls;*asx;*m3u)|*.kpl;*pls;*asx;*m3u|Все файлы (*.*)|*.*";
        string vlcfilter = "VLC плейлист (*.xspf;*.m3u;*.m3u8;*.html)|*.xspf;*.m3u;*.m3u8;*.html|Все файлы (*.*)|*.*";
        string[] mpcExt = { ".mpcpl",".pls",".asx",".m3u"};
        string[] kmpExt = { ".kpl",".pls", ".asx", ".m3u"};
        string[] vlcExt = { ".xspf",".m3u",".m3u8",".html"};
        readonly OpenFileDialog ofd = new OpenFileDialog();
        bool isConfigEdited = false; // флаго проверки правки конфиг.файла
        int procIndex = 0; // инжекс проигрывателя в массиве проигрываетелей класса BackWork
        /// <summary>
        /// Путь к плейлисту
        /// </summary>
        string plpath = "";
        /// <summary>
        /// Запуск обоев после запуска программы
        /// </summary>
        int autoloadSaver; // работа после запуска


        public MainForm()
        {
            backwork = new BackWork(0);
            // дизайн
            InitializeComponent();
            this.CenterToScreen();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ofd.Filter = mpcfilter;
            // проверка автозапуска
            if (File.Exists(shortcut))
                autoloaderCheckBox.Checked = true;
            // Считывание конфигурационного файла
            ConfigData cfgdata;
            if (!File.Exists(cfgpath))
            {
                MessageBox.Show("Файл не найден. Установлены стандартные настройки");
                cfgdata = new ConfigData();
                isConfigEdited = true;
            }                
            else
                cfgdata = ConfigStream.Read(cfgpath);
            if(cfgdata == null)
            {
                MessageBox.Show("Ошибка чтения конфиг.файла. Установлены стандартные настройки","", MessageBoxButtons.OK);
                cfgdata = new ConfigData();
                isConfigEdited = true;
            }
            // считывание period
            timeComboBox.SelectedIndex = cfgdata.period;
            backwork.setTimePeriod(cfgdata.period);
            // считывание autoloadSaver
            autoloadSaver = cfgdata.autoload;
            autoloadSaverCheckBox.Checked = cfgdata.autoload==0 ? false : true;
            // считывание playerpath
            // Добавление нового плеера 1/5
            if (File.Exists(cfgdata.plpath))
            {
                playlistNameLabel.Text = cfgdata.plpath;
                plpath = cfgdata.plpath;
                string ext = Path.GetExtension(cfgdata.plpath);
                if (mpcExt.Contains(ext))
                {
                    playerComboBox.SelectedIndex = 0;
                    procIndex = 0;
                }
                else if (kmpExt.Contains(ext))
                {
                    playerComboBox.SelectedIndex = 1;
                    procIndex = 1;
                    ofd.Filter = kmpfilter;
                }
                else if(vlcExt.Contains(ext))
                {
                    playerComboBox.SelectedIndex = 2;
                    procIndex = 2;
                    ofd.Filter = vlcfilter;
                }
                else
                {
                    playerComboBox.SelectedIndex = 0;
                    return;
                }
            }
            else
            {
                playlistNameLabel.Text = "Не найден плейлист";
                playerComboBox.SelectedIndex = 0;
                switchPanel.Enabled = false;
                playlistSelectButton.Enabled = true;
                notifyIcon.Visible = false;
                offRadioButton.Checked = true;
            }
            // показ обоев после запуска программы
            if (autoloadSaver == 1)
            {
                notifyIcon.Visible = true;
                onRadioButton.Checked = true;
            }
            else
                Visible = true;
            // Создание хука
            globalHook = new UserActivityHook();
            globalHook.KeyPress += GlobalKeyPress;
            globalHook.OnMouseActivity += GlobalMouseActivity;
            globalHook.Start(true, true);
        }
        // глобальное нажатие клавиатуры
        public void GlobalKeyPress(object sender, KeyPressEventArgs e)
        {
            backwork.stopShowWallpaper();
        }
        // глобальное движение мыши
        public void GlobalMouseActivity(object sender, MouseEventArgs e)
        {
            backwork.stopShowWallpaper();
        }
        //Включить фоновую задачу
        private void OnRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (onRadioButton.Checked)
            {
                this.Text = "Видеобои 1.72: АКТИВНО";
                notifyIcon.Text = "Видеообои ВКЛ";
                backwork.start(plpath);
                playlistSelectButton.Enabled = false;
            }
            else
            {
                this.Text = "Видеобои 1.72";
                notifyIcon.Text = "Видеообои ВЫКЛ";
                backwork.stop();
                playlistSelectButton.Enabled = true;
            }               
        }
        // Сворачивание в трей
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon.Visible = true;
            }
        }
        // Переключение времени простоя
        private void TimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            offRadioButton.Checked = true;
            backwork.setTimePeriod(timeComboBox.SelectedIndex);
            isConfigEdited = true;
        }
        // Создание-удаление ярлыка
        private void autoLoader_CheckedChanged(object sender, EventArgs e)
        {
            // Создание ярлыка
            if (autoloaderCheckBox.Checked)
            {
                //Windows Script Host Shell Object
                dynamic shell = Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")));
                try
                {
                    var lnk = shell.CreateShortcut(shortcut);
                    try
                    {
                        lnk.TargetPath = Application.ExecutablePath;
                        lnk.IconLocation = "shell32.dll, 1";
                        lnk.Save();
                    }
                    finally
                    {
                        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(lnk);
                    }
                }
                finally
                {
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shell);
                }
            }
            // Удаление ярлыка
            else
                File.Delete(shortcut);
        }
        // переключение видеоплеера
        private void playerComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            backwork.stop();
            offRadioButton.Checked = true;
            switchPanel.Enabled = false;
            playlistNameLabel.Text = "Не выбран плейлист";
            plpath = "";
            // Добавление нового плеера 2/6
            if (playerComboBox.SelectedIndex == 0)
            {
                ofd.Filter = mpcfilter;
                procIndex = 0;
            }
            else if(playerComboBox.SelectedIndex == 1)
            {
                ofd.Filter = kmpfilter;
                procIndex = 1;
            }
            else
            {
                ofd.Filter = vlcfilter;
                procIndex = 2;
            }           
        }
        // смена плейлиста
        private void playlistSelectButton_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            ofd.InitialDirectory = Path.GetDirectoryName(ofd.FileName);
            string ext = Path.GetExtension(ofd.FileName);
            // Добавление нового плеера 3/6
            if (mpcExt.Contains(ext))
            {
                playerComboBox.SelectedIndex = 0;
                procIndex = 0;
                ofd.Filter = mpcfilter;
            }
            else if (kmpExt.Contains(ext))
            {
                playerComboBox.SelectedIndex = 1;
                procIndex = 1;
                ofd.Filter = kmpfilter;
            }
            else if (vlcExt.Contains(ext))
            {
                playerComboBox.SelectedIndex = 2;
                procIndex = 2;
                ofd.Filter = vlcfilter;
            }
            else
                return;
            plpath = ofd.FileName;
            playlistNameLabel.Text = ofd.FileName;
            procIndex = playerComboBox.SelectedIndex;
            switchPanel.Enabled = true;
            isConfigEdited = true;
        }
        // Информация о программе
        private void aboutImage_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(aboutImage, "Видеобом 1.72\n(c) Aladser\n2022");
        }
        // Открыть приложение после нажатия на иконку в трее
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }
        // Переключение автопоказа обоев
        private void autoloaderSaverCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            autoloadSaver = autoloadSaverCheckBox.Checked ? 1 : 0;
            isConfigEdited = true;
        }
        // закрытие приложения
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isConfigEdited)
                ConfigStream.Write(cfgpath, timeComboBox.SelectedIndex, autoloadSaver, plpath);
            /*
            //лог
            StreamWriter writer = new StreamWriter(logpath, false);
            writer.WriteLine("");
            foreach(string elem in backwork.log)
            {
                Console.WriteLine(elem);
                writer.WriteLine(elem + "\n");
            }
            writer.Close();
            */
            // Закрытие или сворачивание приложения
            if (!backwork.isActive())
                Process.GetCurrentProcess().Kill();
            else
            {
                e.Cancel = true;
                Hide();
                notifyIcon.Visible = true;
            }
        }
    }
}
