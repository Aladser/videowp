using System;
using System.IO;
using System.Windows.Forms;
using videowp.Classes;

namespace videowp.Formes
{
    public partial class SettingForm : Form
    {
        bool firstLoadBoot = true; // флаг автозагрузки
        bool firstShowBoot = true; // флаг первого показа окна
        readonly UpdateSearch updateSrv;
        string lastSrv = "";
        public SettingForm(MainForm mf, UpdateSearch updateSrv)
        {
            InitializeComponent();
            CenterToScreen();

            int left = mf.Left + (this.Width - mf.Width) / 2;
            int top = mf.Top + (this.Height - mf.Height) / 2;
            Left = left;
            Top = top;

            autoLoaderCheckbox.Checked = File.Exists(Program.SHORTCUT);
            autoShowCheckbox.Checked = Program.config.AutoShow == 1;
            overWindowsCheckbox.Checked = Program.config.OverWindows == 1;
            this.updateSrv = updateSrv;
            updateSrvField.Text = !updateSrv.SharePath.Equals("") ? updateSrv.IsShare() ? updateSrv.SharePath : $"{updateSrv.SharePath}: нет связи" : "";
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
                Program.SetShare(srvName);
                new Copying(updateSrv.BW_GetFilesFromShare).Start();
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
