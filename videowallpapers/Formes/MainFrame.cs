using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using videowp.Classes;
using videowp.Formes;
using ComboBox = System.Windows.Forms.ComboBox;

namespace videowp
{
    internal partial class MainForm : Form
    {
        readonly int OFF = 0;
        readonly int ON = 1;
        readonly int DISABLED = 2;

        readonly ConfigControl config;
        readonly PlayerBW playerBW;
        readonly PlaylistControl playlist;
        readonly PlaylistUpdatesBW updateSearch;

        readonly FolderBrowserDialog fbd = new FolderBrowserDialog();
        readonly Bitmap[] switcher = {Properties.Resources.offbtn, Properties.Resources.onbtn, Properties.Resources.disabledbtn}; // переключатель        
        int switcherIndex; // индекс переключателя 

        public MainForm(ConfigControl config, PlayerBW playerBW, PlaylistControl pl, PlaylistUpdatesBW updtSrch)
        {
            InitializeComponent();
            CenterToScreen();
            this.config = config;
            this.playerBW = playerBW;
            playlist = pl;
            updateSearch = updtSrch;

            notifyIcon.Text = "Aladser VW";
            timeComboBox.SelectedIndex = config.InactionIndex; // считывание времени заставки

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
            if (config.AutoShow==1 && !playlist.playlistFolderPath.Equals("") && !playlist.IsEmpty())
            {
                SetSwitcherImage(ON);               
                playlistSelectButton.Enabled = false;
                playerBW.Start();
            }
            // пустая папка с видео
            else if (playlist.playlistFolderPath.Equals("") || playlist.IsEmpty())
            {
                SetSwitcherImage(DISABLED);
                showWallpaperSwitcher.Image = switcher[DISABLED];
                showWallpaperSwitcher.Enabled = false;
                this.Show();
            }
            else
            {
                SetSwitcherImage(OFF);
                this.Show();
            }
            // создание файла с версией программы
            string versionPath = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\{this.Text}.info";           
            string[] arr  = Directory.GetFiles(Path.GetDirectoryName(Application.ExecutablePath), "Видеобои*.info");
            if (arr.Length != 0 && !arr[0].Equals(versionPath)) File.Delete(arr[0]);
            if (!File.Exists(versionPath)) File.Create(versionPath);
        }

        // переключить показ обоев
        void ShowWallpaperSwitcher_Click(object sender, EventArgs e)
        {
            switcherIndex = switcherIndex == ON ? OFF : ON;
            SetPlayerActivation(switcherIndex);
            setPlayerExecution(switcherIndex);
        }
        
        // смена статуса переключателя плеера
        public void SetPlayerActivation(int index)
        {
            playlistFolderNameLabel.Text = playlist.playlistFolderPath;
            switcherIndex = index;
            showWallpaperSwitcher.Enabled = true;
            showWallpaperSwitcher.Image = switcher[index];
        }

        // включение - выключение плеера
        void setPlayerExecution(int index)
        {
            if (index == ON)
            {
                playerBW.Start();
            }
            else
            {
                playerBW.Stop();
            }
        }

        // изменить изображение переключателя
        void SetSwitcherImage(int index){
            switcherIndex = index;
            showWallpaperSwitcher.Image = switcher[index];
        }

        // переключение времени заставки
        void TimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            config.InactionIndex = timeComboBox.SelectedIndex;
        }

        // Сворачивание в трей
        void MainForm_SizeChanged(object sender, EventArgs e){this.Hide();}

        // Разворачивание окна
        void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e){this.Show();}

        // смена плейлиста
        void PlaylistSelectButton_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.OK) return;

            config.PlaylistFolderPath = fbd.SelectedPath;
            playlist.playlistFolderPath = fbd.SelectedPath;
            playlistFolderNameLabel.Text = !playlist.IsEmpty() ? fbd.SelectedPath : $"{fbd.SelectedPath} (Пусто)";
            playlist.CheckFilesInPlaylist();
            if (updateSearch.IsActive()) new FuncBackwork(updateSearch.BW_GetFilesFromShare).Start();
            showWallpaperSwitcher.Enabled = !playlist.IsEmpty();
            showWallpaperSwitcher.Image = playlist.IsEmpty() ? switcher[DISABLED] : switcher[OFF];
        }

        /// <summary>
        /// Асинхронная смена статуса плейлиста
        /// </summary>
        public void CheckEmptyPlaylist()
        {
            if (!playlist.IsEmpty())
            {
                playlistFolderNameLabel.Invoke(new MethodInvoker(delegate { playlistFolderNameLabel.Text = playlist.playlistFolderPath; }));
                showWallpaperSwitcher.Image = switcher[OFF];
                playlistFolderNameLabel.Invoke(new MethodInvoker(delegate { showWallpaperSwitcher.Enabled = true; }));
            }
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

        // показ окна настроек
        void SetupBtn_Click(object sender, EventArgs e){
            new SettingForm(this, playlist, config, updateSearch).ShowDialog();
        }

        // свернуть окно
        void MinBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        // закрыть окно
        void ExitBtn_Click(object sender, EventArgs e)
        {
            updateSearch.Stop();
            playerBW.Stop();
            Process.GetCurrentProcess().Kill();
        }
    }
}
