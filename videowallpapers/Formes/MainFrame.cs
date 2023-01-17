using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using videowp.Classes;
using videowp.Formes;

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

            // цвет элементов
            timeComboBox.DrawMode = DrawMode.OwnerDrawVariable;
            startPeriodLabel.ForeColor = Color.Green;
            endPeriodLabel.ForeColor = Color.Green;
            playlistSelectButton.BackColor = Color.White;
            this.BackColor = Color.White;

            timeComboBox.SelectedIndex = Program.config.InactionIndex;          // считывание времени заставки                                                                             
            overWindowPictureBox.Image = checkBoxPictures[Program.config.OverWindows]; // флаг поверх всех окон

            // считывание playerpath 
            if (File.Exists(Program.config.PlaylistPath))
            {
                playlistNameLabel.Text = Program.config.PlaylistPath;

                // проверка активности сервера обновлений
                if (Program.config.Updates != "") Program.updateCtrl.CheckUpdates();

                // проверка автозапуска
                if (Program.config.AutoShow == 1)
                {
                    displayWallpapers(true);
                    Program.bcgwork.Start();
                    playlistSelectButton.Enabled = false;
                }
                else
                {
                    displayWallpapers(false);
                    this.Show();
                }
            }
            else
            {
                playlistNameLabel.Text = "Не найден плейлист";
                displayWallpapers(false);
                this.Show();
                showWallpaperSwitcher.Enabled = false;
                showWallpaperSwitcher.Image = switcher[2];
                playlistSelectButton.Enabled = true;
            }

            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ofd.Filter = Program.filefilter;
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Видеобои 1.34\nAladser ©\n2022");
        }

        // переключить показ обоев
        void ShowWallpaperSwitcher_Click(object sender, EventArgs e)
        {
            switcherIndex = switcherIndex == 1 ? 0 : 1;
            if (switcherIndex == 1)
            {
                displayWallpapers(true);
                playlistSelectButton.Enabled = false;

                Program.bcgwork.Start();
            }
            else
            {
                displayWallpapers(false);
                playlistSelectButton.Enabled = true;

                Program.bcgwork.Stop();
            }
        }
        // активировать обои
        void displayWallpapers(bool index)
        {
            showWallpaperSwitcher.Image = switcher[index?1:0];
        }

        // переключение времени заставки
        void TimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.config.InactionIndex = timeComboBox.SelectedIndex;
        }

        // переключение поверх окон
        private void OverWindowPictureBox_Click(object sender, EventArgs e)
        {
            Program.config.OverWindows = Program.config.OverWindows==1 ? 0 : 1;
            overWindowPictureBox.Image = checkBoxPictures[Program.config.OverWindows];
        }

        // Сворачивание в трей
        void MainForm_SizeChanged(object sender, EventArgs e){this.Hide();}
        // Разворачивание окна
        void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e){this.Show();}

        // смена плейлиста
        void PlaylistSelectButton_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() != DialogResult.OK) return;

            ofd.InitialDirectory = Path.GetDirectoryName(ofd.FileName);
            Program.config.PlaylistPath = ofd.FileName;
            playlistNameLabel.Text = ofd.FileName;

            showWallpaperSwitcher.Enabled = true;
            showWallpaperSwitcher.Image = switcher[0];
        }

        // отрисовка combobox
        private void timeComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            var cmb = (ComboBox)sender;

            e.DrawBackground();
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.Green), e.Bounds);
                e.Graphics.DrawString(cmb.Items[e.Index].ToString(), cmb.Font, new SolidBrush(Color.White), e.Bounds, StringFormat.GenericDefault);
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
                e.Graphics.DrawString(cmb.Items[e.Index].ToString(), cmb.Font, new SolidBrush(Color.Green), e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        // Скрытие или закрытие программы
        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        // открыть окно настроек
        private void SetupMenuItem_Click(object sender, EventArgs e)
        {
            new SettingForm();
        }
        // закрыть программу
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Program.bcgwork.Stop();
            Process.GetCurrentProcess().Kill();
        }
    }
}
