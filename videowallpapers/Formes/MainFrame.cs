using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using videowp.Formes;

namespace videowp
{
    public partial class MainForm : Form
    {
        //readonly OpenFileDialog ofd = new OpenFileDialog();
        readonly FolderBrowserDialog fbd = new FolderBrowserDialog();
        // переключатель
        readonly Bitmap[] switcher = {Properties.Resources.offbtn, Properties.Resources.onbtn, Properties.Resources.disabledbtn};
        // индекс переключателя  
        int switcherIndex;

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

            timeComboBox.SelectedIndex = Program.config.InactionIndex; // считывание времени заставки

            // плейлист 
            if (!Program.plCtrl.playlistFolderPath.Equals(""))
            {
                if (!Program.plCtrl.IsEmpty())
                {
                    playlistFolderNameLabel.Text = Program.plCtrl.playlistFolderPath;
                    fbd.SelectedPath = Program.plCtrl.playlistFolderPath;
                }
                else
                {
                    playlistFolderNameLabel.Text = $"{Program.plCtrl.playlistFolderPath} (Пусто)";
                    fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    showWallpaperSwitcher.Image = switcher[2];
                }
            }
            else
            {
                playlistFolderNameLabel.Text = "";
                fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
            Program.plCtrl.CheckFilesInFolder();

            // проверка автозапуска
            // есть автозапуск
            if (Program.config.AutoShow==1 && (!Program.plCtrl.playlistFolderPath.Equals("") || !Program.plCtrl.IsEmpty()))
            {
                ActiveVideoSwitcher(true);
                Program.bcgwork.Start();
                playlistSelectButton.Enabled = false;
            }
            // пустая папка с видео, или нет плейлиста
            else if (Program.plCtrl.playlistFolderPath.Equals("") || Program.plCtrl.IsEmpty())
            {
                showWallpaperSwitcher.Image = switcher[2];
                showWallpaperSwitcher.Enabled = false;
                this.Show();
            }
            else
            {
                ActiveVideoSwitcher(false);
                this.Show();
            }          
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Видеобои 1.4\nAladser ©\n2022");
        }

        // переключить показ обоев
        void ShowWallpaperSwitcher_Click(object sender, EventArgs e)
        {
            switcherIndex = switcherIndex == 1 ? 0 : 1;
            if (switcherIndex == 1)
            {
                ActiveVideoSwitcher(true);
                playlistSelectButton.Enabled = false;

                Program.bcgwork.Start();
            }
            else
            {
                ActiveVideoSwitcher(false);
                playlistSelectButton.Enabled = true;

                Program.bcgwork.Stop();
            }
        }
        // изменить изображение переключателя
        void ActiveVideoSwitcher(bool index){showWallpaperSwitcher.Image = switcher[index?1:0];}

        // переключение времени заставки
        void TimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.config.InactionIndex = timeComboBox.SelectedIndex;
        }

        // Сворачивание в трей
        void MainForm_SizeChanged(object sender, EventArgs e){this.Hide();}
        // Разворачивание окна
        void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e){this.Show();}

        // смена плейлиста
        void PlaylistSelectButton_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.OK) return;

            Program.config.PlaylistFolderPath = fbd.SelectedPath;
            Program.plCtrl.playlistFolderPath = fbd.SelectedPath;
            playlistFolderNameLabel.Text = !Program.plCtrl.IsEmpty() ? fbd.SelectedPath : $"{fbd.SelectedPath} (Пусто)";
            Program.plCtrl.CheckFilesInFolder();

            showWallpaperSwitcher.Enabled = !Program.plCtrl.IsEmpty();
            showWallpaperSwitcher.Image = Program.plCtrl.IsEmpty() ? switcher[2] : switcher[0];
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

        private void setupBtn_Click(object sender, EventArgs e)
        {
            new SettingForm();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Program.bcgwork.Stop();
            Process.GetCurrentProcess().Kill();
        }
    }
}
