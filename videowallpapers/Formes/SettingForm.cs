using System;
using System.IO;
using System.Windows.Forms;

namespace videowp.Formes
{
    public partial class SettingForm : Form
    {
        bool firstLoadBoot = true; // флаг автозагрузки
        bool firstShowBoot = true; // флаг первого показа окна
        string lastSrv = "";
        public SettingForm()
        {
            InitializeComponent();
            CenterToScreen();
            Show();

            autoLoaderCheckbox.Checked = File.Exists(Program.shortcut);
            autoShowCheckbox.Checked = Program.config.AutoShow == 1;
            overWindowsCheckbox.Checked = Program.config.OverWindows == 1;
            // сетевая папка с видео
            if (!Program.config.UpdateServer.Equals(""))
            {
                updateSrvField.Text = Program.plCtrl.IsShare() ? Program.config.UpdateServer : $"{ Program.config.UpdateServer} (нет связи)";
            }
        }

        // флаг автозагрузки
        private void AutoLoaderCheckbox_CheckedChanged(object sender, System.EventArgs e)
        {
            // первый фальшивый запуск
            if (firstLoadBoot && File.Exists(Program.shortcut))
            {
                firstLoadBoot = false;
                return;
            }
            // переключение
            else
            {
                bool index = !File.Exists(Program.shortcut);
                Program.EditAutoLoader(index);
                firstLoadBoot = false;
            }
        }

        // флаг автопоказа
        private void AutoShowCheckbox_CheckedChanged(object sender, System.EventArgs e)
        {
            // первый фальшивый запуск
            if (firstShowBoot && Program.config.AutoShow==1)
            {
                firstShowBoot = false;
                return;
            }
            else
            {
                Program.config.AutoShow = Program.config.AutoShow == 1 ? 0 : 1;
                firstShowBoot = false;
            }
        }

        // Флаг Поверх всех окон
        private void OverWindowsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            // первый фальшивый запуск
            if (firstShowBoot && Program.config.OverWindows == 1)
            {
                firstShowBoot = false;
                return;
            }
            else
            {
                firstShowBoot = false;
                Program.config.OverWindows = Convert.ToInt32(overWindowsCheckbox.Checked);
            }
        }

        // ввод названия сервера
        // \\192.168.1.100\Data\video
        private void SetUpdateSrvBtn_Click(object sender, EventArgs e)
        {
            string srvName = updateSrvField.Text;
            if (Directory.Exists(srvName))
            {
                Program.config.UpdateServer = srvName;
                Program.plCtrl.SetShare(srvName);
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
            Program.config.UpdateServer = "";
        }
        
        private void updateSrvField_DoubleClick(object sender, EventArgs e){updateSrvField.Text = ""; } // очистка поля пути к серверу
    }
}
