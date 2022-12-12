namespace videowp
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.playlistNameLabel = new System.Windows.Forms.Label();
            this.playlistSelectButton = new System.Windows.Forms.Button();
            this.timeComboBox = new System.Windows.Forms.ComboBox();
            this.periodLabel = new System.Windows.Forms.Label();
            this.minLabel = new System.Windows.Forms.Label();
            this.switchPanel = new System.Windows.Forms.Panel();
            this.offRadioButton = new System.Windows.Forms.RadioButton();
            this.onRadioButton = new System.Windows.Forms.RadioButton();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.playlistLabel = new System.Windows.Forms.Label();
            this.autoloaderCheckBox = new System.Windows.Forms.CheckBox();
            this.autoShowCheckBox = new System.Windows.Forms.CheckBox();
            this.aboutImage = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.overWindowCheckBox = new System.Windows.Forms.CheckBox();
            this.switchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aboutImage)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // playlistNameLabel
            // 
            this.playlistNameLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.playlistNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.playlistNameLabel.Location = new System.Drawing.Point(15, 34);
            this.playlistNameLabel.Name = "playlistNameLabel";
            this.playlistNameLabel.Size = new System.Drawing.Size(412, 21);
            this.playlistNameLabel.TabIndex = 3;
            this.playlistNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // playlistSelectButton
            // 
            this.playlistSelectButton.Enabled = false;
            this.playlistSelectButton.Location = new System.Drawing.Point(443, 34);
            this.playlistSelectButton.Name = "playlistSelectButton";
            this.playlistSelectButton.Size = new System.Drawing.Size(78, 22);
            this.playlistSelectButton.TabIndex = 4;
            this.playlistSelectButton.Text = "Выбор";
            this.playlistSelectButton.UseVisualStyleBackColor = true;
            this.playlistSelectButton.Click += new System.EventHandler(this.playlistSelectButton_Click);
            // 
            // timeComboBox
            // 
            this.timeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeComboBox.FormattingEnabled = true;
            this.timeComboBox.Items.AddRange(new object[] {
            "0.05",
            "1",
            "3",
            "5",
            "10",
            "15 "});
            this.timeComboBox.Location = new System.Drawing.Point(140, 21);
            this.timeComboBox.Name = "timeComboBox";
            this.timeComboBox.Size = new System.Drawing.Size(48, 21);
            this.timeComboBox.TabIndex = 5;
            this.timeComboBox.SelectedIndexChanged += new System.EventHandler(this.TimeComboBox_SelectedIndexChanged);
            // 
            // periodLabel
            // 
            this.periodLabel.Location = new System.Drawing.Point(21, 20);
            this.periodLabel.Name = "periodLabel";
            this.periodLabel.Size = new System.Drawing.Size(113, 21);
            this.periodLabel.TabIndex = 6;
            this.periodLabel.Text = "Время бездействия:";
            this.periodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // minLabel
            // 
            this.minLabel.Location = new System.Drawing.Point(194, 21);
            this.minLabel.Name = "minLabel";
            this.minLabel.Size = new System.Drawing.Size(33, 21);
            this.minLabel.TabIndex = 7;
            this.minLabel.Text = "мин";
            this.minLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // switchPanel
            // 
            this.switchPanel.Controls.Add(this.offRadioButton);
            this.switchPanel.Controls.Add(this.onRadioButton);
            this.switchPanel.Location = new System.Drawing.Point(438, 141);
            this.switchPanel.Name = "switchPanel";
            this.switchPanel.Size = new System.Drawing.Size(88, 61);
            this.switchPanel.TabIndex = 8;
            // 
            // offRadioButton
            // 
            this.offRadioButton.Location = new System.Drawing.Point(27, 27);
            this.offRadioButton.Name = "offRadioButton";
            this.offRadioButton.Size = new System.Drawing.Size(58, 21);
            this.offRadioButton.TabIndex = 1;
            this.offRadioButton.Text = "ВЫКЛ";
            this.offRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.offRadioButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.offRadioButton.UseVisualStyleBackColor = true;
            // 
            // onRadioButton
            // 
            this.onRadioButton.Checked = true;
            this.onRadioButton.Location = new System.Drawing.Point(27, 8);
            this.onRadioButton.Name = "onRadioButton";
            this.onRadioButton.Size = new System.Drawing.Size(58, 21);
            this.onRadioButton.TabIndex = 0;
            this.onRadioButton.TabStop = true;
            this.onRadioButton.Text = "ВКЛ";
            this.onRadioButton.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.onRadioButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.onRadioButton.UseVisualStyleBackColor = true;
            this.onRadioButton.CheckedChanged += new System.EventHandler(this.OnRadioButton_CheckedChanged);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Видео обои";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // playlistLabel
            // 
            this.playlistLabel.AutoSize = true;
            this.playlistLabel.Location = new System.Drawing.Point(12, 9);
            this.playlistLabel.Name = "playlistLabel";
            this.playlistLabel.Size = new System.Drawing.Size(59, 13);
            this.playlistLabel.TabIndex = 10;
            this.playlistLabel.Text = "Плейлист:";
            // 
            // autoloaderCheckBox
            // 
            this.autoloaderCheckBox.AutoSize = true;
            this.autoloaderCheckBox.Location = new System.Drawing.Point(15, 164);
            this.autoloaderCheckBox.Name = "autoloaderCheckBox";
            this.autoloaderCheckBox.Size = new System.Drawing.Size(96, 17);
            this.autoloaderCheckBox.TabIndex = 13;
            this.autoloaderCheckBox.Text = "Автозагрузка";
            this.autoloaderCheckBox.UseVisualStyleBackColor = true;
            this.autoloaderCheckBox.CheckedChanged += new System.EventHandler(this.autoLoader_CheckedChanged);
            // 
            // autoShowCheckBox
            // 
            this.autoShowCheckBox.AutoSize = true;
            this.autoShowCheckBox.Location = new System.Drawing.Point(15, 141);
            this.autoShowCheckBox.Name = "autoShowCheckBox";
            this.autoShowCheckBox.Size = new System.Drawing.Size(135, 17);
            this.autoShowCheckBox.TabIndex = 15;
            this.autoShowCheckBox.Text = "Автозапуск заставки";
            this.autoShowCheckBox.UseVisualStyleBackColor = true;
            this.autoShowCheckBox.CheckedChanged += new System.EventHandler(this.autoShowCheckBoxCheckBox_CheckedChanged);
            // 
            // aboutImage
            // 
            this.aboutImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.aboutImage.Image = global::videowp.Properties.Resources.info;
            this.aboutImage.InitialImage = global::videowp.Properties.Resources.info;
            this.aboutImage.Location = new System.Drawing.Point(508, 9);
            this.aboutImage.Name = "aboutImage";
            this.aboutImage.Size = new System.Drawing.Size(18, 17);
            this.aboutImage.TabIndex = 16;
            this.aboutImage.TabStop = false;
            this.aboutImage.MouseHover += new System.EventHandler(this.aboutImage_MouseHover);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.periodLabel);
            this.panel1.Controls.Add(this.timeComboBox);
            this.panel1.Controls.Add(this.minLabel);
            this.panel1.Location = new System.Drawing.Point(15, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(506, 60);
            this.panel1.TabIndex = 20;
            // 
            // overWindowCheckBox
            // 
            this.overWindowCheckBox.AutoSize = true;
            this.overWindowCheckBox.Checked = true;
            this.overWindowCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.overWindowCheckBox.Location = new System.Drawing.Point(15, 187);
            this.overWindowCheckBox.Name = "overWindowCheckBox";
            this.overWindowCheckBox.Size = new System.Drawing.Size(116, 17);
            this.overWindowCheckBox.TabIndex = 21;
            this.overWindowCheckBox.Text = "Поверх всех окон";
            this.overWindowCheckBox.UseVisualStyleBackColor = true;
            this.overWindowCheckBox.CheckedChanged += new System.EventHandler(this.overWindowCheckBox_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 214);
            this.Controls.Add(this.overWindowCheckBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.aboutImage);
            this.Controls.Add(this.autoShowCheckBox);
            this.Controls.Add(this.autoloaderCheckBox);
            this.Controls.Add(this.playlistLabel);
            this.Controls.Add(this.switchPanel);
            this.Controls.Add(this.playlistSelectButton);
            this.Controls.Add(this.playlistNameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Видеобои 1.3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.switchPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.aboutImage)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label playlistNameLabel;
        private System.Windows.Forms.Button playlistSelectButton;
        private System.Windows.Forms.ComboBox timeComboBox;
        private System.Windows.Forms.Label periodLabel;
        private System.Windows.Forms.Label minLabel;
        private System.Windows.Forms.Panel switchPanel;
        private System.Windows.Forms.RadioButton offRadioButton;
        private System.Windows.Forms.RadioButton onRadioButton;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label playlistLabel;
        private System.Windows.Forms.CheckBox autoloaderCheckBox;
        private System.Windows.Forms.CheckBox autoShowCheckBox;
        private System.Windows.Forms.PictureBox aboutImage;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox overWindowCheckBox;
    }
}

