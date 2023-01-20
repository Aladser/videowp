using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using videowp.Classes;
using videowp.Formes;

namespace videowp
{
    public partial class MainForm : Form
    {
        readonly int OFF = 0;
        readonly int ON = 1;
        readonly int DISABLED = 2;

        BackWork bcgwork;
        PlaylistControl playlist;
        UpdateSearch updateSearch;
        readonly FolderBrowserDialog fbd = new FolderBrowserDialog();
        readonly Bitmap[] switcher = {Properties.Resources.offbtn, Properties.Resources.onbtn, Properties.Resources.disabledbtn}; // переключатель        
        int switcherIndex; // индекс переключателя 

        public MainForm(BackWork bcgwork, PlaylistControl pl, UpdateSearch updtSrch)
        {
            InitializeComponent();
            CenterToScreen();
            this.bcgwork = bcgwork;
            playlist = pl;
            updateSearch = updtSrch;

            notifyIcon.Text = "Aladser Видеообои";
            timeComboBox.SelectedIndex = Program.config.InactionIndex; // считывание времени заставки

            // цвет элементов
            timeComboBox.DrawMode = DrawMode.OwnerDrawVariable;
            startPeriodLabel.ForeColor = Color.Green;
            endPeriodLabel.ForeColor = Color.Green;

            // плейлист 
            if (!playlist.IsEmpty())
            {
                playlistFolderNameLabel.Text = playlist.playlistFolderPath;
                fbd.SelectedPath = playlist.playlistFolderPath;
            }
            else
            {
                playlistFolderNameLabel.Text = $"{playlist.playlistFolderPath} (Пусто)";
                fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                showWallpaperSwitcher.Image = switcher[DISABLED];
            }

            // ***** проверка автозапуска *****
            // есть автозапуск
            if (Program.config.AutoShow==1 && !playlist.playlistFolderPath.Equals("") && !playlist.IsEmpty())
            {
                setSwitcherImage(ON);               
                playlistSelectButton.Enabled = false;
                bcgwork.Start();
            }
            // пустая папка с видео
            else if (playlist.playlistFolderPath.Equals("") || playlist.IsEmpty())
            {
                setSwitcherImage(DISABLED);
                showWallpaperSwitcher.Image = switcher[DISABLED];
                showWallpaperSwitcher.Enabled = false;
                this.Show();
            }
            else
            {
                setSwitcherImage(OFF);
                this.Show();
            }          
        }

        void AboutMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Видеобои 1.4\nAladser ©\n2022");
        }

        public void setNotifyIconText(string text)
        {
            notifyIcon.Text = text;
        }

        // переключить показ обоев
        void ShowWallpaperSwitcher_Click(object sender, EventArgs e)
        {
            switcherIndex = switcherIndex == ON ? OFF : ON;
            if (switcherIndex == ON)
            {
                setSwitcherImage(ON);
                playlistSelectButton.Enabled = false;
                bcgwork.Start();
            }
            else
            {
                setSwitcherImage(OFF);
                playlistSelectButton.Enabled = true;
                bcgwork.Stop();
            }
        }
        // изменить изображение переключателя
        void setSwitcherImage(int index){
            switcherIndex = index;
            showWallpaperSwitcher.Image = switcher[index];
        }

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
            playlist.playlistFolderPath = fbd.SelectedPath;
            playlistFolderNameLabel.Text = !playlist.IsEmpty() ? fbd.SelectedPath : $"{fbd.SelectedPath} (Пусто)";
            playlist.CheckFilesInPlaylist();

            showWallpaperSwitcher.Enabled = !playlist.IsEmpty();
            showWallpaperSwitcher.Image = playlist.IsEmpty() ? switcher[DISABLED] : switcher[OFF];
        }

        // отрисовка combobox
        private void TimeComboBox_DrawItem(object sender, DrawItemEventArgs e)
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
        void SetupBtn_Click(object sender, EventArgs e){
            SettingForm sf = new SettingForm(this, updateSearch);
            sf.ShowDialog();
        }
        // закрыть окно
        void ExitBtn_Click(object sender, EventArgs e)
        {
            bcgwork.Stop();
            Process.GetCurrentProcess().Kill();
        }
        // свернуть окно
        void MinBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
