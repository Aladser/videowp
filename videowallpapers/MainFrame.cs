using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace videowallpapers
{
    public partial class MainForm : Form
    {
        
        readonly OpenFileDialog ofd = new OpenFileDialog();
        public static VideoPlayerManager player; //текущий видеоплеер       

        public MainForm()
        {                  
            InitializeComponent();
            CenterToScreen();
            if (File.Exists(Program.shortcut)) autoloaderCheckBox.Checked = true; // проверка автозапуска
            timeComboBox.SelectedIndex = Program.cfgdata.period;  // считывание времени заставки            
            autoShowCheckBox.Checked = Program.cfgdata.autoshow==0 ? false : true; // считывание autoshow
            // считывание playerpath
            if (File.Exists(Program.cfgdata.plpath))
            {
                playlistNameLabel.Text = Program.cfgdata.plpath;
                playerComboBox.SelectedIndex = Program.cfgdata.player;
            }
            else
            {
                playlistNameLabel.Text = "Не найден плейлист";
                playerComboBox.SelectedIndex = 0;
                Program.cfgdata.player = 0;
                ConfigStream.Write(Program.cfgpath, Program.cfgdata);
                switchPanel.Enabled = false;
                playlistSelectButton.Enabled = true;
                offRadioButton.Checked = true;
            }
            player = new VideoPlayerManager(playerComboBox.SelectedIndex, Program.cfgdata.plpath);
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ofd.Filter = player.getActivePlayerFilter();
            // показ обоев после запуска программы
            if (autoShowCheckBox.Checked && !player.getPlaylist().Equals(""))
            {
                onRadioButton.Checked = true;
                Program.bcgwork.start(player.getPlaylist());
                notifyIcon.Visible = true;
            }
            else
            {
                Show();
                offRadioButton.Checked = true;
            }               
        }


        //Включить фоновую задачу
        private void OnRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (onRadioButton.Checked)
            {
                this.Text = "Видеобои 1.88: АКТИВНО";
                notifyIcon.Text = "Видеообои ВКЛ";
                Program.bcgwork.start( player.getPlaylist() );
                playlistSelectButton.Enabled = false;
            }
            else
            {
                this.Text = "Видеобои 1.88";
                notifyIcon.Text = "Видеообои ВЫКЛ";
                Program.bcgwork.stop();
                playlistSelectButton.Enabled = true;
            }               
        }
        // Информация о программе
        private void aboutImage_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(aboutImage, "Видеобом 1.88\n(c) Aladser\n2022");
        }
        // Сворачивание в трей
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            notifyIcon.Visible = true;
            Hide();
        }
        // Переключение времени заставки
        private void TimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.bcgwork.setTimePeriod(timeComboBox.SelectedIndex);
            Program.cfgdata.period = timeComboBox.SelectedIndex;
            ConfigStream.Write(Program.cfgpath, Program.cfgdata);
        }
        // Переключение автозагрузки
        private void autoLoader_CheckedChanged(object sender, EventArgs e)
        {
            Program.editAutoLoader(autoloaderCheckBox.Checked);
        }
        // переключение видеоплеера
        private void playerComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Program.bcgwork.stop();
            player.setPlaylist("");
            Program.cfgdata.plpath = "";
            if (playerComboBox.SelectedIndex == 5)
            {
                if (!File.Exists(Program.mplayer))
                {
                    MessageBox.Show("Проигрыватель MPlayer не найден. Разместите папку видеоплеера рядом с исполняемым файлом");
                    Program.cfgdata.player = 0;
                    playerComboBox.SelectedIndex = 0;
                }                    
            }
            player.setActivePlayer(playerComboBox.SelectedIndex);
            Program.cfgdata.player = playerComboBox.SelectedIndex;
            ConfigStream.Write(Program.cfgpath, Program.cfgdata);

            playlistNameLabel.Text = "Не выбран плейлист";
            ofd.Filter = VideoPlayerManager.playerFilters[playerComboBox.SelectedIndex];
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
            player.setPlaylist(ofd.FileName);
            playlistNameLabel.Text = ofd.FileName;
            switchPanel.Enabled = true;
            Program.cfgdata.plpath = player.getPlaylist();
            ConfigStream.Write(Program.cfgpath, Program.cfgdata);
        }
        // Разворачивание окна
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            notifyIcon.Visible = false;
        }
        // Переключение автопоказа обоев
        private void autoShowCheckBoxCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Program.cfgdata.autoshow = autoShowCheckBox.Checked ? 1 : 0;
            ConfigStream.Write(Program.cfgpath, Program.cfgdata);
        }
        // Скрытие или закрытие программы
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*            
            //лог
            StreamWriter writer = new StreamWriter(Program.logpath, false);
            Console.WriteLine(log);
            writer.WriteLine(log + "\n");
            writer.Close();
            */
            // Закрытие или сворачивание приложения
            if (!Program.bcgwork.isActive())
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
