using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace videowallpapers
{
    public partial class MainForm : Form
    {
        UserActivityHook globalHook;
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
        string pathname = ""; // путь плейлиста
        int workafterboot; // работа после запуска
        readonly OpenFileDialog ofd = new OpenFileDialog();
        bool isEdited = false;
        int procIndex = 0; // инжекс проигрывателя в массиве проигрываетелей класса BackWork

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
            StreamReader reader;
            try
            {
                reader = new StreamReader(cfgpath);
                // считывание period
                string line = reader.ReadLine();
                int number = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));
                timeComboBox.SelectedIndex = number;
                backwork.setTimePeriod(number);
                // считывание workafterboot
                line = reader.ReadLine();
                number = Int32.Parse(line.Substring(line.IndexOf("= ") + 2));
                workafterboot = number==0 ? 0 : 1;
                wabCheckBox.Checked = number==0 ? false : true;
                // считывание playerpath
                line = reader.ReadLine();
                line = line.Substring(line.IndexOf("= ") + 2);
                // Добавление нового плеера 1/6
                if (File.Exists(line))
                {
                    playlistNameLabel.Text = line;
                    pathname = line;
                    string ext = Path.GetExtension(line);
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
                        return;
                }
                else
                {
                    playlistNameLabel.Text = File.Exists(line) ? "Неизвестный формат плейлиста" : "Не найден плейлист";
                    playerComboBox.SelectedIndex = 0;
                    switchPanel.Enabled = false;
                    playlistSelectButton.Enabled = true;
                    notifyIcon.Visible = false;
                    offRadioButton.Checked = true;
                }
                reader.Close();
                // начать работу после запуска
                if (workafterboot == 1)
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
            catch (FileNotFoundException)
            {
                var result = MessageBox.Show("Файл настроек не найден. Будут установлены стандартные настройки", "Приложение не запущено", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    StreamWriter writer = new StreamWriter(cfgpath, false);
                    String text = "period = 0\n";
                    text += "workafterboot = 0\n";
                    text += "playerpath = ";
                    writer.WriteLine(text);
                    writer.Close();
                    timeComboBox.SelectedIndex = 0;
                    Visible = true;
                    playlistNameLabel.Text = "Не найден плейлист";
                    playerComboBox.SelectedIndex = 0;
                    switchPanel.Enabled = false;
                    playlistSelectButton.Enabled = true;
                    notifyIcon.Visible = false;
                    offRadioButton.Checked = true;
                }
            }
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
                this.Text = "Видеобои 1.68: АКТИВНО";
                notifyIcon.Text = "Видеообои ВКЛ";
                backwork.start(pathname);
                playlistSelectButton.Enabled = false;
            }
            else
            {
                this.Text = "Видеобои 1.68";
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
            isEdited = true;
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
            pathname = "";
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
        // переключение плеера и плейлиста
        private void playlistSelectButton_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            ofd.InitialDirectory = ofd.FileName.Substring(0, ofd.FileName.Length-Path.GetFileName(ofd.FileName).Length);
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
            pathname = ofd.FileName;
            procIndex = playerComboBox.SelectedIndex;
            playlistNameLabel.Text = pathname;
            switchPanel.Enabled = true;
            isEdited = true;
        }
        // Флаг Работа после запуска
        private void wabCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            workafterboot = wabCheckBox.Checked ? 1 : 0;
            isEdited = true;
        }
        // Информация о программе
        private void aboutImage_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(aboutImage, "Видеобом 1.7\n(c) Aladser\n2022");
        }
        // Открыть приложение после нажатия на иконку в трее
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }
        // закрытие приложения
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StreamWriter writer;
            String text;
            // запись измененных данных
            if (isEdited)
            {
                writer = new StreamWriter(cfgpath, false);
                text = "period = " + timeComboBox.SelectedIndex + "\n";
                text += "workafterboot = " + workafterboot + "\n";
                text += "playerpath = " + pathname + "\n";
                writer.WriteLine(text);
                writer.Close();
            }
            /*лог
            writer = new StreamWriter(logpath, false);
            text = backwork.downtime.ToString();
            writer.WriteLine(text);
            writer.Close();
            */// Закрытие или сворачивание приложения
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
