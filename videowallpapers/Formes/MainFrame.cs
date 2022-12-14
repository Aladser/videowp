using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace videowp
{
    public partial class MainForm : Form
    {
        readonly OpenFileDialog ofd = new OpenFileDialog();
        readonly Bitmap[] switcher = {Properties.Resources.offbtn, Properties.Resources.onbtn}; // переключатель
        int switcherIndex;                                                             // индекс переключателя  

        public MainForm()
        {
            InitializeComponent();
            CenterToScreen();

            if (File.Exists(Program.shortcut)) autoloaderCheckBox.Checked = true;// проверка автозапуска
            timeComboBox.SelectedIndex = Program.config.InactionNumber;          // считывание времени заставки            
            autoShowCheckBox.Checked = Program.config.AutoShow;                  // считывание autoshow
            overWindowCheckBox.Checked = Program.config.OverWindows;             // флаг Поверх всех окон
            // считывание playerpath
            if (File.Exists(Program.config.PlaylistPath))
            {
                playlistNameLabel.Text = Program.config.PlaylistPath;

                if (Program.config.AutoShow)
                {
                    activeSwitchOnSign(true);
                    Program.bcgwork.Start();
                }
                else
                {
                    activeSwitchOnSign(false);
                    this.Show();
                }
            }
            else
            {
                playlistNameLabel.Text = "Не найден плейлист";
                activeSwitchOnSign(false);
                workSwitcher.Enabled = false;
                playlistSelectButton.Enabled = true;
                this.Show();
            }
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ofd.Filter = Program.filefilter;
        }

        // переключить показ обоев
        private void workSwitcher_Click(object sender, EventArgs e)
        {
            switcherIndex = switcherIndex == 1 ? 0 : 1;
            if (switcherIndex == 1)
            {
                notifyIcon.Text = "Видеообои ВКЛ";
                activeSwitchOnSign(true);
                playlistSelectButton.Enabled = false;

                Program.bcgwork.Start();
            }
            else
            {
                notifyIcon.Text = "Видеообои ВЫКЛ";
                activeSwitchOnSign(false);
                playlistSelectButton.Enabled = true;

                Program.bcgwork.Stop();
            }
        }
        // Информация о программе
        private void aboutImage_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(aboutImage, "Aladser's Видеобои 1.33\n2022");
        }
        // Переключение автозагрузки
        private void autoLoader_CheckedChanged(object sender, EventArgs e)
        {
            Program.editAutoLoader(autoloaderCheckBox.Checked);
        }
        // Сворачивание в трей
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            Hide();
            notifyIcon.Visible = true;
        }
        // смена плейлиста
        private void playlistSelectButton_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            ofd.InitialDirectory = Path.GetDirectoryName(ofd.FileName);
            Program.config.PlaylistPath = ofd.FileName;
            playlistNameLabel.Text = ofd.FileName;
            workSwitcher.Enabled = true;
        }
        // переключение времени заставки
        private void TimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.bcgwork.SetTimePeriod(timeComboBox.SelectedIndex);
            Program.config.InactionNumber = timeComboBox.SelectedIndex;
        }
        // переключение автопоказа обоев
        private void autoShowCheckBoxCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Program.config.AutoShow = autoShowCheckBox.Checked;
        }
        // переключение Поверх всех окон
        private void overWindowCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Program.config.OverWindows = overWindowCheckBox.Checked;
        }
        // Разворачивание окна
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        // Скрытие или закрытие программы
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Program.bcgwork.IsActive())
            {
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
                this.Hide();
            }
        }
        // активировать обои
        private void activeSwitchOnSign(bool index)
        {
            switcherIndex = index ? 1 : 0;
            workSwitcher.Image = switcher[switcherIndex];
        }
    }
}
