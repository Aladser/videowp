using System;
using System.IO;
using System.Windows.Forms;

namespace videowp
{
    public partial class MainForm : Form
    {       
        readonly OpenFileDialog ofd = new OpenFileDialog();      

        public MainForm()
        {
            InitializeComponent();
            CenterToScreen();
            if (File.Exists(Program.shortcut)) autoloaderCheckBox.Checked = true;         // проверка автозапуска
            timeComboBox.SelectedIndex = Program.cfgdata.period;                          // считывание времени заставки            
            autoShowCheckBox.Checked = Program.cfgdata.autoshow==0 ? false : true;        // считывание autoshow
            overWindowCheckBox.Checked = Program.cfgdata.overWindows == 0 ? false : true; // флаг Поверх всех окон
            // считывание playerpath
            if (File.Exists(Program.cfgdata.plpath))
            {
                playlistNameLabel.Text = Program.cfgdata.plpath;
            }
            else
            {
                playlistNameLabel.Text = "Не найден плейлист";
                ConfigStream.Write(Program.cfgpath, Program.cfgdata);
                switchPanel.Enabled = false;
                playlistSelectButton.Enabled = true;
                offRadioButton.Checked = true;
            }
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ofd.Filter = Program.filefilter;
            // показ обоев после запуска программы
            if (autoShowCheckBox.Checked && !Program.cfgdata.plpath.Equals(""))
            {
                onRadioButton.Checked = true;
                Program.bcgwork.start();
            }
            else
            {
                this.Show();
                offRadioButton.Checked = true;
            }
        }
        //Включить фоновую задачу
        private void OnRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (onRadioButton.Checked)
            {
                this.Text = "Видеобои 1.3: АКТИВНО";
                notifyIcon.Text = "Видеообои ВКЛ";
                playlistSelectButton.Enabled = false;            
                Program.bcgwork.start();
            }
            else
            {
                this.Text = "Видеобои 1.3";
                notifyIcon.Text = "Видеообои ВЫКЛ";
                playlistSelectButton.Enabled = true;                                
                Program.bcgwork.stop();
            }               
        }
        // Информация о программе
        private void aboutImage_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(aboutImage, "Aladser's Видеобои 1.3\n2022");
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
            ofd.InitialDirectory = Program.cfgdata.plpath;
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            ofd.InitialDirectory = Path.GetDirectoryName(ofd.FileName);
            Program.cfgdata.plpath = ofd.FileName;
            playlistNameLabel.Text = ofd.FileName;
            ConfigStream.Write(Program.cfgpath, Program.cfgdata);
            switchPanel.Enabled = true;
        }
        // переключение времени заставки
        private void TimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.bcgwork.setTimePeriod(timeComboBox.SelectedIndex);
            Program.cfgdata.period = timeComboBox.SelectedIndex;
            ConfigStream.Write(Program.cfgpath, Program.cfgdata);
        }
        // переключение автопоказа обоев
        private void autoShowCheckBoxCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Program.cfgdata.autoshow = autoShowCheckBox.Checked ? 1 : 0;
            ConfigStream.Write(Program.cfgpath, Program.cfgdata);
        }
        // переключение Поверх всех окон
        private void overWindowCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Program.cfgdata.overWindows = overWindowCheckBox.Checked ? 1 : 0;
            ConfigStream.Write(Program.cfgpath, Program.cfgdata);
        }
        // Переключение плеера
        private void mpvRB_CheckedChanged(object sender, EventArgs e)
        {
            ConfigStream.Write(Program.cfgpath, Program.cfgdata);
        }
        // Разворачивание окна
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }
        // Скрытие или закрытие программы
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Program.bcgwork.isActive())
            {
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
                this.Hide();
            }
        }
    }
}
