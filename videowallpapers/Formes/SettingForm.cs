using System;
using System.IO;
using System.Windows.Forms;
using videowp.Classes;

namespace videowp.Formes
{
    internal partial class SettingForm : Form
    {
        bool firstLoadBoot = true; // флаг автозагрузки
        bool firstShowBoot = true; // флаг первого показа окна
        readonly ConfigControl config;
        readonly UpdateSearchBW updateSrv;
        string lastSrv = "";
        public SettingForm(MainForm mf, ConfigControl config, UpdateSearchBW updateSrv)
        {
            InitializeComponent();
            CenterToScreen();
            this.config = config;

            int left = mf.Left + (this.Width - mf.Width) / 2;
            int top = mf.Top + (this.Height - mf.Height) / 2;
            Left = left;
            Top = top;

            autoLoaderCheckbox.Checked = File.Exists(Program.SHORTCUT);
            autoShowCheckbox.Checked = config.AutoShow == 1;
            overWindowsCheckbox.Checked = config.OverWindows == 1;
            this.updateSrv = updateSrv;
            updateSrvField.Text = !updateSrv.SharePath.Equals("") ? updateSrv.IsShare() ? updateSrv.SharePath : $"{updateSrv.SharePath}: нет связи" : "";
            updateTimeComboBox.SelectedIndex = config.UpdateTime;
        }

        // флаг автозагрузки
        private void AutoLoaderCheckbox_CheckedChanged(object sender, System.EventArgs e)
        {
            // первый фальшивый запуск
            if (firstLoadBoot && File.Exists(Program.SHORTCUT))
            {
                firstLoadBoot = false;
                return;
            }
            // переключение
            else
            {
                bool index = !File.Exists(Program.SHORTCUT);
                Program.EditAutoLoader(index);
                firstLoadBoot = false;
            }
        }

        // флаг автопоказа
        private void AutoShowCheckbox_CheckedChanged(object sender, System.EventArgs e)
        {
            // первый фальшивый запуск
            if (firstShowBoot && config.AutoShow==1)
            {
                firstShowBoot = false;
                return;
            }
            else
            {
                config.AutoShow = config.AutoShow == 1 ? 0 : 1;
                firstShowBoot = false;
            }
        }

        // Флаг Поверх всех окон
        private void OverWindowsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            // первый фальшивый запуск
            if (firstShowBoot && config.OverWindows == 1)
            {
                firstShowBoot = false;
                return;
            }
            else
            {
                firstShowBoot = false;
                config.OverWindows = Convert.ToInt32(overWindowsCheckbox.Checked);
            }
        }

        // ввод названия сервера
        // \\192.168.1.100\Data\video
        private void SetUpdateSrvBtn_Click(object sender, EventArgs e)
        {
            string srvName = updateSrvField.Text;
            if (Directory.Exists(srvName))
            {
                config.UpdateServer = srvName;
                Program.SetShare(srvName);
                new CopyingBW(updateSrv.BW_GetFilesFromShare).Start();
            }
            else if(!srvName.Equals(""))
            {
                lastSrv = updateSrvField.Text.Contains("нет связи") ? lastSrv : updateSrvField.Text;
                updateSrvField.Text = $"{lastSrv}: нет связи";
            }               
        }
        // сброс сервера обновлений
        private void ResetUpdateSrvBtn_Click(object sender, EventArgs e)
        {
            updateSrvField.Text = "";
            config.UpdateServer = "";
        }
        // очистка поля пути к серверу
        private void UpdateSrvField_DoubleClick(object sender, EventArgs e){updateSrvField.Text = ""; } 
        // установка времени
        private void UpdateTimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            config.UpdateTime = updateTimeComboBox.SelectedIndex;
        }
    }
}
