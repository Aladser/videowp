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
        readonly MainForm parentForm;
        readonly PlaylistControl playlist;
        readonly ConfigControl config;
        readonly PlaylistUpdatesBW updateSrv;
        string lastSrv = "";

        public SettingForm(MainForm parentForm, PlaylistControl pl, ConfigControl config, PlaylistUpdatesBW updateSrv)
        {
            InitializeComponent();
            CenterToScreen();
            this.parentForm = parentForm;
            this.playlist = pl;
            this.config = config;

            autoLoaderCheckbox.Checked = File.Exists(Program.SHORTCUT);
            autoShowCheckbox.Checked = config.AutoShow == 1;
            overWindowsCheckbox.Checked = config.OverWindows == 1;
            this.updateSrv = updateSrv;
            updateSrvField.Text = !updateSrv.SharePath.Equals("") ? updateSrv.IsShareConnection() ? updateSrv.SharePath : $"{updateSrv.SharePath}: нет связи" : "";
            updateTimeComboBox.SelectedIndex = config.UpdateTime;
        }

        // активация сервера обновлений плейлиста
        // \\x.x.x.x\video
        private void SetUpdateSrvBtn_Click(object sender, EventArgs e)
        {
            string srvName = updateSrvField.Text;
            if (Directory.Exists(srvName))
            {
                config.UpdateServer = srvName;
                updateSrv.SetShare(srvName);
                parentForm.SetPlayerActivation(0);
                if (playlist.IsEmpty())
                    new FuncBackwork(updateSrv.BW_GetFilesFromShare, this).Start();
                else
                {
                    if (!updateSrv.IsActive()) updateSrv.Start();
                    else new FuncBackwork(updateSrv.BW_GetFilesFromShare).Start();
                }
            }
            else if (!srvName.Equals(""))
            {
                lastSrv = updateSrvField.Text.Contains("нет связи") ? lastSrv : updateSrvField.Text;
                updateSrvField.Text = $"{lastSrv}: нет связи";
            }
        }

        // флаг автозагрузки
        private void AutoLoaderCheckbox_CheckedChanged(object sender, EventArgs e)
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
                Program.IsAutoLoader = index;
                firstLoadBoot = false;
            }
        }

        // флаг автопоказа
        private void AutoShowCheckbox_CheckedChanged(object sender, EventArgs e)
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

        // сброс сервера обновлений
        private void ResetUpdateSrvBtn_Click(object sender, EventArgs e)
        {
            updateSrvField.Text = "";
            config.UpdateServer = "";
            if (updateSrv.IsActive())  updateSrv.Stop();
        }

        // очистка поля пути к серверу
        private void UpdateSrvField_DoubleClick(object sender, EventArgs e)
        {
            updateSrvField.Text = ""; 
        } 

        // установка времени
        private void UpdateTimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            config.UpdateTime = updateTimeComboBox.SelectedIndex;
        }
    }
}
