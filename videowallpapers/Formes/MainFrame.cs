using System;
using System.IO;
using System.Windows.Forms;

namespace videowp
{
    public partial class MainForm : Form
    {
        readonly OpenFileDialog ofd = new OpenFileDialog();
        /// <summary>
        /// переключатель
        /// </summary>
        readonly System.Drawing.Bitmap[] switcher = {Properties.Resources.offbtn, Properties.Resources.onbtn};
        /// <summary>
        /// индекс переключателя  
        /// </summary>
        int switcherIndex;                                                           

        public MainForm()
        {
            InitializeComponent();
            CenterToScreen();
            notifyIcon.Text = "Aladser Видеообои";

            if (File.Exists(Program.shortcut)) autoloaderCheckBox.Checked = true;// проверка автозапуска
            timeComboBox.SelectedIndex = Program.config.InactionIndex;          // считывание времени заставки            
            autoShowCheckBox.Checked = Program.config.AutoShow;                  // считывание autoshow
            overWindowCheckBox.Checked = Program.config.OverWindows;             // флаг Поверх всех окон

            // считывание playerpath
            if (File.Exists(Program.config.PlaylistPath))
            {
                playlistNameLabel.Text = Program.config.PlaylistPath;

                if (Program.config.AutoShow)
                {
                    showOnBtn(true);
                    Program.bcgwork.Start();
                }
                else
                {
                    showOnBtn(false);
                    this.Show();
                }
            }
            else
            {
                playlistNameLabel.Text = "Не найден плейлист";
                showOnBtn(false);
                showWallpaperSwitcher.Enabled = false;
                playlistSelectButton.Enabled = true;
                this.Show();
            }

            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ofd.Filter = Program.filefilter;
        }

        // переключить показ обоев
        void showWallpaperSwitcher_Click(object sender, EventArgs e)
        {
            switcherIndex = switcherIndex == 1 ? 0 : 1;
            if (switcherIndex == 1)
            {
                showOnBtn(true);
                playlistSelectButton.Enabled = false;

                Program.bcgwork.Start();
            }
            else
            {
                showOnBtn(false);
                playlistSelectButton.Enabled = true;

                Program.bcgwork.Stop();
            }
        }
        // активировать обои
        void showOnBtn(bool index)
        {
            switcherIndex = index ? 1 : 0;
            showWallpaperSwitcher.Image = switcher[switcherIndex];
        }

        // Информация о программе
        void aboutImage_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(aboutImage, "Видеобои 1.33\nAladser ©\n2022");
        }
        // Переключение автозагрузки
        void autoLoader_CheckedChanged(object sender, EventArgs e)
        {
            Program.editAutoLoader(autoloaderCheckBox.Checked);
        }
        // Сворачивание в трей
        void MainForm_SizeChanged(object sender, EventArgs e)
        {
            this.Hide();
            notifyIcon.Visible = true;
        }
        // смена плейлиста
        void playlistSelectButton_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() != DialogResult.OK) return;

            ofd.InitialDirectory = Path.GetDirectoryName(ofd.FileName);
            Program.config.PlaylistPath = ofd.FileName;
            playlistNameLabel.Text = ofd.FileName;

            showWallpaperSwitcher.Enabled = true;
        }
        // переключение времени заставки
        void TimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.config.InactionIndex = timeComboBox.SelectedIndex;
        }
        // переключение автопоказа обоев
        void autoShowCheckBoxCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Program.config.AutoShow = autoShowCheckBox.Checked;
        }
        // переключение Поверх всех окон
        void overWindowCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Program.config.OverWindows = overWindowCheckBox.Checked;
        }
        // Разворачивание окна
        void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        // Скрытие или закрытие программы
        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Program.bcgwork.IsActive())
                Application.Exit();
            else
                e.Cancel = true;
                this.Hide();
        }

    }
}
