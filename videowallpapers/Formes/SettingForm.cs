using System;
using System.IO;
using System.Windows.Forms;

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
            autoShowCheckbox.Checked = Program.config.AutoShow==1;


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
    }
}
