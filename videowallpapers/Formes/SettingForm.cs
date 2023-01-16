using System.IO;
using System.Windows.Forms;

namespace videowp.Formes
{
    public partial class SettingForm : Form
    {
        bool firstBoot = true;
        public SettingForm()
        {
            InitializeComponent();
            CenterToScreen();
            Show();

            autoLoaderCheckbox.Checked = File.Exists(Program.shortcut);
            autoShowCheckbox.Checked = Program.config.AutoShow==1;
        }

        private void AutoLoaderCheckbox_CheckedChanged(object sender, System.EventArgs e)
        {
            // первый фальшивый запуск окна при активной автозагрузке
            if (firstBoot && File.Exists(Program.shortcut))
            {
                firstBoot = false;
                return;
            }
            // переключение
            else
            {
                bool index = !File.Exists(Program.shortcut);
                Program.EditAutoLoader(index);
                firstBoot = false;
            }
        }

        private void autoShowCheckbox_CheckedChanged(object sender, System.EventArgs e)
        {
            // первый фальшивый запуск окна при активной автозагрузке
            if (firstBoot && Program.config.AutoShow==1)
            {
                firstBoot = false;
                return;
            }
            // переключение
            else
                Program.config.AutoShow = Program.config.AutoShow==1 ? 0 : 1;
        }
    }
}
