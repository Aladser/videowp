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
        readonly OpenFileDialog ofd = new OpenFileDialog();
        BackWork backwork;// фоновая задача показа обоев
        public static VideoPlayer player; //текущий видеоплеер

        public MainForm()
        {                  
            InitializeComponent();
            if (File.Exists(Program.shortcut)) autoloaderCheckBox.Checked = true; // проверка автозапуска
            backwork = new BackWork(); // фоновая задача показа обоев
            CenterToScreen();
            timeComboBox.SelectedIndex = Program.cfgdata.period;  // считывание времени бездействия на форму и backwork            
            autoShowCheckBox.Checked = Program.cfgdata.autoshow == 0 ? false : true; // считывание autoshow
            // считывание playerpath
            if (File.Exists(Program.cfgdata.plpath))
            {
                playlistNameLabel.Text = Program.cfgdata.plpath;
                string ext = Path.GetExtension(Program.cfgdata.plpath);
                int index;
                if (VideoPlayer.playerExtensions[VideoPlayer.KMP].Contains(ext))
                    index = 1;
                else if (VideoPlayer.playerExtensions[VideoPlayer.VLC].Contains(ext))
                    index = 2;
                else if (VideoPlayer.playerExtensions[VideoPlayer.LA].Contains(ext))
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
            player = new VideoPlayer(playerComboBox.SelectedIndex, Program.cfgdata.plpath);
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ofd.Filter = player.getActivePlayerFilter();
            // показ обоев после запуска программы
            if (autoShowCheckBox.Checked && !player.getPlaylist().Equals(""))
            {
                onRadioButton.Checked = true;
                backwork.start(player.getPlaylist());
                notifyIcon.Visible = true;
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
                this.Text = "Видеобои 1.8: АКТИВНО";
                notifyIcon.Text = "Видеообои ВКЛ";
                notifyIcon.Visible = true;
                backwork.start( player.getPlaylist() );
                playlistSelectButton.Enabled = false;
            }
            else
            {
                this.Text = "Видеобои 1.8";
                notifyIcon.Text = "Видеообои ВЫКЛ";
                notifyIcon.Visible = false;
                backwork.stop();
                playlistSelectButton.Enabled = true;
            }               
        }
        // Информация о программе
        private void aboutImage_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(aboutImage, "Видеобом 1.8\n(c) Aladser\n2022");
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
            Program.cfgdata.period = timeComboBox.SelectedIndex;
            ConfigStream.Write(Program.cfgpath, Program.cfgdata);
        }
        // Создание-удаление ярлыка
        private void autoLoader_CheckedChanged(object sender, EventArgs e)
        {
            Program.editAutoLoader(autoloaderCheckBox.Checked);
        }
        // переключение видеоплеера
        private void playerComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            backwork.stop();
            player.setPlaylist("");
            player.setActivePlayer(playerComboBox.SelectedIndex);

            playlistNameLabel.Text = "Не выбран плейлист";
            ofd.Filter = VideoPlayer.playerFilters[playerComboBox.SelectedIndex];
            offRadioButton.Checked = true;
            switchPanel.Enabled = false;
        }
        // смена плейлиста
        private void playlistSelectButton_Click(object sender, EventArgs e)
        {
            ofd.InitialDirectory = player.getPlaylist();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            ofd.InitialDirectory = Path.GetDirectoryName(ofd.FileName);
            string ext = Path.GetExtension(ofd.FileName);
            int index=0;
            if (VideoPlayer.playerFilters[VideoPlayer.MPC].Contains(ext))
                index = 0;
            else if (VideoPlayer.playerFilters[VideoPlayer.KMP].Contains(ext))
                index = 1;
            else if (VideoPlayer.playerFilters[VideoPlayer.VLC].Contains(ext))
                index = 2;
            else if (VideoPlayer.playerFilters[VideoPlayer.LA].Contains(ext))
                index = 3;
            else
                return;
            playerComboBox.SelectedIndex = index;
            player.setPlaylist(ofd.FileName);
            playlistNameLabel.Text = ofd.FileName;
            switchPanel.Enabled = true;
            Program.cfgdata.plpath = player.getPlaylist();
            ConfigStream.Write(Program.cfgpath, Program.cfgdata);
        }
        // Открыть приложение после нажатия на иконку в трее
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }
        // Переключение автопоказа обоев
        private void autoShowCheckBoxCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Program.cfgdata.autoshow = autoShowCheckBox.Checked ? 1 : 0;
            ConfigStream.Write(Program.cfgpath, Program.cfgdata);
        }
        // закрытие приложения
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
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
