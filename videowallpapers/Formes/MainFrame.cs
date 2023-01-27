﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using videowp.Classes;
using videowp.Formes;

namespace videowp
{
    internal partial class MainForm : Form
    {
        readonly int OFF = 0;
        readonly int ON = 1;
        readonly int DISABLED = 2;
        readonly ConfigControl config;
        readonly PlayerBW bcgwork;
        readonly PlaylistControl playlist;
        readonly UpdateCheckBW updateSearch;
        readonly FolderBrowserDialog fbd = new FolderBrowserDialog();
        readonly Bitmap[] switcher = {Properties.Resources.offbtn, Properties.Resources.onbtn, Properties.Resources.disabledbtn}; // переключатель        
        int switcherIndex; // индекс переключателя 

        public MainForm(ConfigControl config, PlayerBW bcgwork, PlaylistControl pl, UpdateCheckBW updtSrch)
        {
            InitializeComponent();
            CenterToScreen();
            this.config = config;
            this.bcgwork = bcgwork;
            playlist = pl;
            updateSearch = updtSrch;

            notifyIcon.Text = "Aladser Видеообои";
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
                bcgwork.Start();
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
        }

        // переключить показ обоев
        void ShowWallpaperSwitcher_Click(object sender, EventArgs e)
        {
            switcherIndex = switcherIndex == ON ? OFF : ON;
            if (switcherIndex == ON)
            {
                SetSwitcherImage(ON);
                playlistSelectButton.Enabled = false;
                bcgwork.Start();
            }
            else
            {
                SetSwitcherImage(OFF);
                playlistSelectButton.Enabled = true;
                bcgwork.Stop();
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
            SettingForm sf = new SettingForm(this, config, updateSearch);
            sf.ShowDialog();
        }

        // закрыть окно
        void ExitBtn_Click(object sender, EventArgs e)
        {
            updateSearch.Stop();
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
