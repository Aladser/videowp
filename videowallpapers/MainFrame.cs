using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace videowallpapers
{
    public partial class MainForm : Form
    {
        UserActivityHook globalHook;// хук глобального движения мыши или клавиатуры
        BackWork backwork;// фоновая задача показа обоев
        readonly string cfgpath = Path.GetDirectoryName(Application.ExecutablePath) + "\\config.cfg"; // путь конфига
        readonly string logpath = Path.GetDirectoryName(Application.ExecutablePath) + "\\log.txt"; // путь лога
        readonly string shortcut= Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\videowallpapers.lnk"; // ярлык автозагрузки 
        readonly OpenFileDialog ofd = new OpenFileDialog();
        bool isConfigEdited = false; // флаго проверки правки конфиг.файла
        VideoPlayer player; //текущий видеоплеер

        public MainForm()
        {           
            if (File.Exists(shortcut)) autoloaderCheckBox.Checked = true; // проверка автозапуска
            backwork = new BackWork(); // фоновая задача показа обоев
            InitializeComponent();
            CenterToScreen();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
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
            if (cfgdata == null)
            {
                MessageBox.Show("Ошибка чтения конфиг.файла. Установлены стандартные настройки", "", MessageBoxButtons.OK);
                cfgdata = new ConfigData();
                isConfigEdited = true;
            }           
            timeComboBox.SelectedIndex = cfgdata.period;  // считывание времени бездействия на форму и backwork            
            autoShowCheckBox.Checked = cfgdata.autoshow == 0 ? false : true; // считывание autoshow
            // считывание playerpath
            if (File.Exists(cfgdata.plpath))
            {
                playlistNameLabel.Text = cfgdata.plpath;
                string ext = Path.GetExtension(cfgdata.plpath);
                int index;
                if (VideoPlayer.playerExtensions[1].Contains(ext))
                    index = 1;
                else if (VideoPlayer.playerExtensions[2].Contains(ext))
                    index = 2;
                else if (VideoPlayer.playerExtensions[3].Contains(ext))
                    index = 3;
                else
                    index = 0;
                playerComboBox.SelectedIndex = index;
            }
            else
            {
                playlistNameLabel.Text = "Не найден плейлист";
                playerComboBox.SelectedIndex = 0;

                switchPanel.Enabled = false;
                playlistSelectButton.Enabled = true;
                offRadioButton.Checked = true;
            }
            player = new VideoPlayer(playerComboBox.SelectedIndex, cfgdata.plpath);
            // показ обоев после запуска программы
            if (autoShowCheckBox.Checked && !player.getPlaylist().Equals(""))
            {
                onRadioButton.Checked = true;
                backwork.start(player.getPlaylist());
            }
            else
            {
                Visible = true;
                offRadioButton.Checked = true;
            }               
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
                this.Text = "Видеобои 1.75: АКТИВНО";
                notifyIcon.Text = "Видеообои ВКЛ";
                notifyIcon.Visible = true;
                backwork.start( player.getPlaylist() );
                playlistSelectButton.Enabled = false;
            }
            else
            {
                this.Text = "Видеобои 1.75";
                notifyIcon.Text = "Видеообои ВЫКЛ";
                notifyIcon.Visible = false;
                backwork.stop();
                playlistSelectButton.Enabled = true;
            }               
        }
        // Информация о программе
        private void aboutImage_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(aboutImage, "Видеобом 1.75\n(c) Aladser\n2022");
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
        // Переключение времени простоя на форме и backwork
        private void TimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            player.setPlaylist("");

            playlistNameLabel.Text = "Не выбран плейлист";
            ofd.Filter = VideoPlayer.playerFilters[playerComboBox.SelectedIndex];
            offRadioButton.Checked = true;
            switchPanel.Enabled = false;
        }
        // смена плейлиста
        private void playlistSelectButton_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            ofd.InitialDirectory = Path.GetDirectoryName(ofd.FileName);
            string ext = Path.GetExtension(ofd.FileName);
            int index=0;
            if (VideoPlayer.playerFilters[0].Contains(ext))
                index = 0;
            else if (VideoPlayer.playerFilters[1].Contains(ext))
                index = 1;
            else if (VideoPlayer.playerFilters[2].Contains(ext))
                index = 2;
            else if (VideoPlayer.playerFilters[3].Contains(ext))
                index = 3;
            else
                return;
            playerComboBox.SelectedIndex = index;
            player.setPlaylist(ofd.FileName);
            playlistNameLabel.Text = ofd.FileName;
            switchPanel.Enabled = true;
            isConfigEdited = true;
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
            isConfigEdited = true;
        }
        // закрытие приложения
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isConfigEdited)
                ConfigStream.Write(cfgpath, timeComboBox.SelectedIndex, autoShowCheckBox.Checked, player.getPlaylist());
            /*
            //лог
            StreamWriter writer = new StreamWriter(logpath, false);
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
