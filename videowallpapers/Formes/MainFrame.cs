using System;
using System.Drawing;
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
        readonly Bitmap[] switcher = {Properties.Resources.offbtn, Properties.Resources.onbtn, Properties.Resources.disabledbtn};
        /// <summary>
        /// индекс переключателя  
        /// </summary>
        int switcherIndex;

        /// <summary>
        /// Флажки опций
        /// </summary>
        readonly Bitmap[] checkBoxPictures = { Properties.Resources.offSelectImg, Properties.Resources.onSelectImg };

        public MainForm()
        {
            InitializeComponent();
            CenterToScreen();
            notifyIcon.Text = "Aladser Видеообои";

            // цвет элементов выбора цвета
            timeComboBox.DrawMode = DrawMode.OwnerDrawVariable;
            startPeriodLabel.ForeColor = Color.Green;
            endPeriodLabel.ForeColor = Color.Green;

            playlistSelectButton.BackColor = Color.White;
            this.BackColor = Color.White;

            timeComboBox.SelectedIndex = Program.config.InactionIndex;          // считывание времени заставки
                                                                                
            autoShowPictureBox.Image = checkBoxPictures[Program.config.AutoShow];
            autoLoaderPictureBox.Image = File.Exists(Program.shortcut) ? checkBoxPictures[1] : checkBoxPictures[0];
            overWindowPictureBox.Image = checkBoxPictures[Program.config.OverWindows];

            // считывание playerpath
            if (File.Exists(Program.config.PlaylistPath))
            {
                playlistNameLabel.Text = Program.config.PlaylistPath;

                if (Program.config.AutoShow == 1)
                {
                    ShowOnBtn(true);
                    Program.bcgwork.Start();
                    playlistSelectButton.Enabled = false;
                }
                else
                {
                    ShowOnBtn(false);
                    this.Show();
                }
            }
            else
            {
                playlistNameLabel.Text = "Не найден плейлист";
                ShowOnBtn(false);
                showWallpaperSwitcher.Enabled = false;
                showWallpaperSwitcher.Image = switcher[2];
                playlistSelectButton.Enabled = true;
                this.Show();
            }

            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ofd.Filter = Program.filefilter;
        }

        private string[] animals = new string[]{"Elephant", "c r o c o d i l e", "lion"};
        private void ComboBox1_DrawItem(object sender,DrawItemEventArgs e)
        {

        }

        // переключить показ обоев
        void showWallpaperSwitcher_Click(object sender, EventArgs e)
        {
            switcherIndex = switcherIndex == 1 ? 0 : 1;
            if (switcherIndex == 1)
            {
                ShowOnBtn(true);
                playlistSelectButton.Enabled = false;

                Program.bcgwork.Start();
            }
            else
            {
                ShowOnBtn(false);
                playlistSelectButton.Enabled = true;

                Program.bcgwork.Stop();
            }
        }
        // активировать обои
        void ShowOnBtn(bool index)
        {
            switcherIndex = index ? 1 : 0;
            showWallpaperSwitcher.Image = switcher[switcherIndex];
        }

        private void autoShowPictureBox_Click(object sender, EventArgs e)
        {
            Program.config.AutoShow = Program.config.AutoShow == 1 ? 0 : 1;
            autoShowPictureBox.Image = checkBoxPictures[Program.config.AutoShow];
        }

        private void autoLoaderPictureBox_Click(object sender, EventArgs e)
        {
            int index = File.Exists(Program.shortcut) ? 0 : 1;
            Program.EditAutoLoader(index==1);
            autoLoaderPictureBox.Image = checkBoxPictures[index];
        }

        private void overWindowPictureBox_Click(object sender, EventArgs e)
        {
            Program.config.OverWindows = Program.config.OverWindows==1 ? 0 : 1;
            overWindowPictureBox.Image = checkBoxPictures[Program.config.OverWindows];
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
            showWallpaperSwitcher.Image = switcher[0];
        }
        // переключение времени заставки
        void TimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.config.InactionIndex = timeComboBox.SelectedIndex;
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
        // Информация о программе
        void aboutImage_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(aboutImage, "Видеобои 1.33\nAladser ©\n2022");
        }

        private void timeComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            var cmb = (ComboBox)sender;
            e.DrawBackground();

            // change background color
            e.Graphics.FillRectangle(Brushes.White, e.Bounds);

            e.Graphics.DrawString(cmb.Items[e.Index].ToString(), cmb.Font, new SolidBrush(Color.Green), e.Bounds);
            e.DrawFocusRectangle();
        }
    }
}
