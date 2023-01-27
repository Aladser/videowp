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
            this.playlistFolderNameLabel = new System.Windows.Forms.Label();
            this.playlistSelectButton = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.playlistFolderLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.minBtn = new System.Windows.Forms.PictureBox();
            this.exitBtn = new System.Windows.Forms.PictureBox();
            this.infoBtn = new System.Windows.Forms.PictureBox();
            this.setupBtn = new System.Windows.Forms.PictureBox();
            this.endPeriodLabel = new System.Windows.Forms.Label();
            this.timeComboBox = new System.Windows.Forms.ComboBox();
            this.startPeriodLabel = new System.Windows.Forms.Label();
            this.showWallpaperSwitcher = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.minBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.setupBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.showWallpaperSwitcher)).BeginInit();
            this.SuspendLayout();
            // 
            // playlistFolderNameLabel
            // 
            this.playlistFolderNameLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.playlistFolderNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.playlistFolderNameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playlistFolderNameLabel.Location = new System.Drawing.Point(12, 60);
            this.playlistFolderNameLabel.Name = "playlistFolderNameLabel";
            this.playlistFolderNameLabel.Size = new System.Drawing.Size(480, 33);
            this.playlistFolderNameLabel.TabIndex = 3;
            this.playlistFolderNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // playlistFolderLabel
            // 
            this.playlistFolderLabel.AutoSize = true;
            this.playlistFolderLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playlistFolderLabel.Location = new System.Drawing.Point(12, 35);
            this.playlistFolderLabel.Name = "playlistFolderLabel";
            this.playlistFolderLabel.Size = new System.Drawing.Size(87, 17);
            this.playlistFolderLabel.TabIndex = 10;
            this.playlistFolderLabel.Text = "Папка видео:";
            // 
            // minBtn
            // 
            this.minBtn.ErrorImage = global::videowp.Properties.Resources.minbtn;
            this.minBtn.Image = global::videowp.Properties.Resources.minbtn;
            this.minBtn.InitialImage = global::videowp.Properties.Resources.minbtn;
            this.minBtn.Location = new System.Drawing.Point(512, 12);
            this.minBtn.Name = "minBtn";
            this.minBtn.Size = new System.Drawing.Size(20, 20);
            this.minBtn.TabIndex = 33;
            this.minBtn.TabStop = false;
            this.toolTip.SetToolTip(this.minBtn, "Свернуть");
            this.minBtn.Click += new System.EventHandler(this.MinBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Image = global::videowp.Properties.Resources.exiticon;
            this.exitBtn.Location = new System.Drawing.Point(590, 12);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(20, 20);
            this.exitBtn.TabIndex = 32;
            this.exitBtn.TabStop = false;
            this.toolTip.SetToolTip(this.exitBtn, "Выход");
            this.exitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // infoBtn
            // 
            this.infoBtn.Image = global::videowp.Properties.Resources.abouticon;
            this.infoBtn.Location = new System.Drawing.Point(538, 12);
            this.infoBtn.Name = "infoBtn";
            this.infoBtn.Size = new System.Drawing.Size(20, 20);
            this.infoBtn.TabIndex = 31;
            this.infoBtn.TabStop = false;
            this.toolTip.SetToolTip(this.infoBtn, "Видеобои 1.52\nAladser ©\r\n2022");
            // 
            // setupBtn
            // 
            this.setupBtn.Image = global::videowp.Properties.Resources.setupicon;
            this.setupBtn.Location = new System.Drawing.Point(564, 12);
            this.setupBtn.Name = "setupBtn";
            this.setupBtn.Size = new System.Drawing.Size(20, 20);
            this.setupBtn.TabIndex = 30;
            this.setupBtn.TabStop = false;
            this.toolTip.SetToolTip(this.setupBtn, "Настройки");
            this.setupBtn.Click += new System.EventHandler(this.SetupBtn_Click);
            // 
            // endPeriodLabel
            // 
            this.endPeriodLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.endPeriodLabel.Location = new System.Drawing.Point(222, 142);
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
            "0.01",
            "1",
            "3",
            "5",
            "10",
            "15 "});
            this.timeComboBox.Location = new System.Drawing.Point(148, 143);
            this.timeComboBox.Name = "timeComboBox";
            this.timeComboBox.Size = new System.Drawing.Size(68, 25);
            this.timeComboBox.TabIndex = 5;
            this.timeComboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.TimeComboBox_DrawItem);
            this.timeComboBox.SelectedIndexChanged += new System.EventHandler(this.TimeComboBox_SelectedIndexChanged);
            // 
            // startPeriodLabel
            // 
            this.startPeriodLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startPeriodLabel.Location = new System.Drawing.Point(12, 142);
            this.startPeriodLabel.Name = "startPeriodLabel";
            this.startPeriodLabel.Size = new System.Drawing.Size(130, 25);
            this.startPeriodLabel.TabIndex = 6;
            this.startPeriodLabel.Text = "Время бездействия:";
            this.startPeriodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // showWallpaperSwitcher
            // 
            this.showWallpaperSwitcher.Image = global::videowp.Properties.Resources.offbtn;
            this.showWallpaperSwitcher.Location = new System.Drawing.Point(528, 131);
            this.showWallpaperSwitcher.Name = "showWallpaperSwitcher";
            this.showWallpaperSwitcher.Size = new System.Drawing.Size(82, 37);
            this.showWallpaperSwitcher.TabIndex = 22;
            this.showWallpaperSwitcher.TabStop = false;
            this.showWallpaperSwitcher.Click += new System.EventHandler(this.ShowWallpaperSwitcher_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(622, 191);
            this.ControlBox = false;
            this.Controls.Add(this.minBtn);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.infoBtn);
            this.Controls.Add(this.setupBtn);
            this.Controls.Add(this.endPeriodLabel);
            this.Controls.Add(this.timeComboBox);
            this.Controls.Add(this.startPeriodLabel);
            this.Controls.Add(this.showWallpaperSwitcher);
            this.Controls.Add(this.playlistFolderLabel);
            this.Controls.Add(this.playlistSelectButton);
            this.Controls.Add(this.playlistFolderNameLabel);
            this.Font = new System.Drawing.Font("Anonymous Pro", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Видеобои 1.52";
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.minBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.setupBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.showWallpaperSwitcher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label playlistFolderNameLabel;
        private System.Windows.Forms.Button playlistSelectButton;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label playlistFolderLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.PictureBox showWallpaperSwitcher;
        private System.Windows.Forms.Label endPeriodLabel;
        private System.Windows.Forms.ComboBox timeComboBox;
        private System.Windows.Forms.Label startPeriodLabel;
        private System.Windows.Forms.PictureBox setupBtn;
        private System.Windows.Forms.PictureBox infoBtn;
        private System.Windows.Forms.PictureBox exitBtn;
        private System.Windows.Forms.PictureBox minBtn;
    }
}

