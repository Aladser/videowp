using System;
using System.IO;
using System.Windows.Forms;
using videowp.Classes;

namespace videowp.Formes
{
    public partial class SettingForm : Form
    {
        bool firstLoadBoot = true;
        bool firstShowBoot = true;
        public SettingForm()
        {
            InitializeComponent();
            CenterToScreen();
            Show();

            autoLoaderCheckbox.Checked = File.Exists(Program.shortcut);
            autoShowCheckbox.Checked = Program.config.AutoShow == 1;
            overWindowsCheckbox.Checked = Program.config.OverWindows == 1;

            updateSrvField.Text = Program.config.Updates;
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
                Program.config.OverWindows = Program.config.OverWindows == 1 ? 0 : 1;
                firstShowBoot = false;
            }
        }

        private void updateSrvField_DoubleClick(object sender, EventArgs e) { updateSrvField.Text = ""; }
        // ввод названия сервера
        // \\192.168.1.100\Data\video
        private void SetUpdateSrvBtn_Click(object sender, EventArgs e)
        {
            string serverAddress = updateSrvField.Text;
            if (Directory.Exists(serverAddress))
            {
                Program.config.Updates = serverAddress;
                Program.plCtrl = new PlaylistControl(Program.config.PlaylistFolderPath);
            }
            else
            {
                updateSrvField.Text = "сервер не существует";                
            }
        }
        // сброс сервера обновлений
        private void button1_Click(object sender, EventArgs e)
        {
            updateSrvField.Text = "";
            Program.config.Updates = "";
        }


    }
}
