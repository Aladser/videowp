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
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.playlistLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.overWindowLabel = new System.Windows.Forms.Label();
            this.endPeriodLabel = new System.Windows.Forms.Label();
            this.timeComboBox = new System.Windows.Forms.ComboBox();
            this.startPeriodLabel = new System.Windows.Forms.Label();
            this.overWindowPictureBox = new System.Windows.Forms.PictureBox();
            this.showWallpaperSwitcher = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.setupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.overWindowPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.showWallpaperSwitcher)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // playlistNameLabel
            // 
            this.playlistNameLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.playlistNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.playlistNameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playlistNameLabel.Location = new System.Drawing.Point(12, 60);
            this.playlistNameLabel.Name = "playlistNameLabel";
            this.playlistNameLabel.Size = new System.Drawing.Size(480, 33);
            this.playlistNameLabel.TabIndex = 3;
            this.playlistNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // playlistSelectButton
            // 
            this.playlistSelectButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playlistSelectButton.Location = new System.Drawing.Point(498, 60);
            this.playlistSelectButton.Name = "playlistSelectButton";
            this.playlistSelectButton.Size = new System.Drawing.Size(112, 33);
            this.playlistSelectButton.TabIndex = 4;
            this.playlistSelectButton.Text = "Выбор";
            this.playlistSelectButton.UseVisualStyleBackColor = true;
            this.playlistSelectButton.Click += new System.EventHandler(this.PlaylistSelectButton_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Видео обои";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // playlistLabel
            // 
            this.playlistLabel.AutoSize = true;
            this.playlistLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playlistLabel.Location = new System.Drawing.Point(12, 35);
            this.playlistLabel.Name = "playlistLabel";
            this.playlistLabel.Size = new System.Drawing.Size(66, 17);
            this.playlistLabel.TabIndex = 10;
            this.playlistLabel.Text = "Плейлист:";
            // 
            // overWindowLabel
            // 
            this.overWindowLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.overWindowLabel.Location = new System.Drawing.Point(47, 179);
            this.overWindowLabel.Name = "overWindowLabel";
            this.overWindowLabel.Size = new System.Drawing.Size(222, 24);
            this.overWindowLabel.TabIndex = 28;
            this.overWindowLabel.Text = "Поверх всех окон";
            // 
            // endPeriodLabel
            // 
            this.endPeriodLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.endPeriodLabel.Location = new System.Drawing.Point(420, 117);
            this.endPeriodLabel.Name = "endPeriodLabel";
            this.endPeriodLabel.Size = new System.Drawing.Size(56, 25);
            this.endPeriodLabel.TabIndex = 7;
            this.endPeriodLabel.Text = "мин";
            this.endPeriodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timeComboBox
            // 
            this.timeComboBox.BackColor = System.Drawing.SystemColors.Control;
            this.timeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeComboBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.timeComboBox.FormattingEnabled = true;
            this.timeComboBox.Items.AddRange(new object[] {
            "0.05",
            "1",
            "3",
            "5",
            "10",
            "15 "});
            this.timeComboBox.Location = new System.Drawing.Point(346, 117);
            this.timeComboBox.Name = "timeComboBox";
            this.timeComboBox.Size = new System.Drawing.Size(68, 25);
            this.timeComboBox.TabIndex = 5;
            this.timeComboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.timeComboBox_DrawItem);
            this.timeComboBox.SelectedIndexChanged += new System.EventHandler(this.TimeComboBox_SelectedIndexChanged);
            // 
            // startPeriodLabel
            // 
            this.startPeriodLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startPeriodLabel.Location = new System.Drawing.Point(212, 117);
            this.startPeriodLabel.Name = "startPeriodLabel";
            this.startPeriodLabel.Size = new System.Drawing.Size(130, 25);
            this.startPeriodLabel.TabIndex = 6;
            this.startPeriodLabel.Text = "Время бездействия:";
            this.startPeriodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // overWindowPictureBox
            // 
            this.overWindowPictureBox.Location = new System.Drawing.Point(15, 179);
            this.overWindowPictureBox.Name = "overWindowPictureBox";
            this.overWindowPictureBox.Size = new System.Drawing.Size(26, 24);
            this.overWindowPictureBox.TabIndex = 25;
            this.overWindowPictureBox.TabStop = false;
            this.overWindowPictureBox.Click += new System.EventHandler(this.OverWindowPictureBox_Click);
            // 
            // showWallpaperSwitcher
            // 
            this.showWallpaperSwitcher.Image = global::videowp.Properties.Resources.offbtn;
            this.showWallpaperSwitcher.Location = new System.Drawing.Point(528, 168);
            this.showWallpaperSwitcher.Name = "showWallpaperSwitcher";
            this.showWallpaperSwitcher.Size = new System.Drawing.Size(82, 35);
            this.showWallpaperSwitcher.TabIndex = 22;
            this.showWallpaperSwitcher.TabStop = false;
            this.showWallpaperSwitcher.Click += new System.EventHandler(this.ShowWallpaperSwitcher_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setupMenuItem,
            this.exitMenuItem,
            this.aboutMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(622, 24);
            this.menuStrip1.TabIndex = 29;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // setupMenuItem
            // 
            this.setupMenuItem.Name = "setupMenuItem";
            this.setupMenuItem.Size = new System.Drawing.Size(79, 20);
            this.setupMenuItem.Text = "Настройки";
            this.setupMenuItem.Click += new System.EventHandler(this.SetupMenuItem_Click);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(54, 20);
            this.exitMenuItem.Text = "Выход";
            this.exitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(94, 20);
            this.aboutMenuItem.Text = "О программе";
            this.aboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(622, 215);
            this.Controls.Add(this.endPeriodLabel);
            this.Controls.Add(this.timeComboBox);
            this.Controls.Add(this.startPeriodLabel);
            this.Controls.Add(this.overWindowLabel);
            this.Controls.Add(this.overWindowPictureBox);
            this.Controls.Add(this.showWallpaperSwitcher);
            this.Controls.Add(this.playlistLabel);
            this.Controls.Add(this.playlistSelectButton);
            this.Controls.Add(this.playlistNameLabel);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Anonymous Pro", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Видеобои 1.34 Тест 1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.overWindowPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.showWallpaperSwitcher)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label playlistNameLabel;
        private System.Windows.Forms.Button playlistSelectButton;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label playlistLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.PictureBox showWallpaperSwitcher;
        private System.Windows.Forms.PictureBox overWindowPictureBox;
        private System.Windows.Forms.Label overWindowLabel;
        private System.Windows.Forms.Label endPeriodLabel;
        private System.Windows.Forms.ComboBox timeComboBox;
        private System.Windows.Forms.Label startPeriodLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem setupMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
    }
}

