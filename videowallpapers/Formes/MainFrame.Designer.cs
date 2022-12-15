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
            this.autoShowLabel = new System.Windows.Forms.Label();
            this.autoLoaderLabel = new System.Windows.Forms.Label();
            this.overWindowLabel = new System.Windows.Forms.Label();
            this.endPeriodLabel = new System.Windows.Forms.Label();
            this.timeComboBox = new System.Windows.Forms.ComboBox();
            this.startPeriodLabel = new System.Windows.Forms.Label();
            this.overWindowPictureBox = new System.Windows.Forms.PictureBox();
            this.autoLoaderPictureBox = new System.Windows.Forms.PictureBox();
            this.autoShowPictureBox = new System.Windows.Forms.PictureBox();
            this.showWallpaperSwitcher = new System.Windows.Forms.PictureBox();
            this.aboutImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.overWindowPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.autoLoaderPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.autoShowPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.showWallpaperSwitcher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aboutImage)).BeginInit();
            this.SuspendLayout();
            // 
            // playlistNameLabel
            // 
            this.playlistNameLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.playlistNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.playlistNameLabel.Font = new System.Drawing.Font("Anonymous Pro", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playlistNameLabel.Location = new System.Drawing.Point(17, 36);
            this.playlistNameLabel.Name = "playlistNameLabel";
            this.playlistNameLabel.Size = new System.Drawing.Size(480, 23);
            this.playlistNameLabel.TabIndex = 3;
            this.playlistNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // playlistSelectButton
            // 
            this.playlistSelectButton.Font = new System.Drawing.Font("Anonymous Pro", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playlistSelectButton.Location = new System.Drawing.Point(516, 36);
            this.playlistSelectButton.Name = "playlistSelectButton";
            this.playlistSelectButton.Size = new System.Drawing.Size(91, 24);
            this.playlistSelectButton.TabIndex = 4;
            this.playlistSelectButton.Text = "Выбор";
            this.playlistSelectButton.UseVisualStyleBackColor = true;
            this.playlistSelectButton.Click += new System.EventHandler(this.playlistSelectButton_Click);
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
            this.playlistLabel.Font = new System.Drawing.Font("Anonymous Pro", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playlistLabel.Location = new System.Drawing.Point(14, 13);
            this.playlistLabel.Name = "playlistLabel";
            this.playlistLabel.Size = new System.Drawing.Size(70, 14);
            this.playlistLabel.TabIndex = 10;
            this.playlistLabel.Text = "Плейлист:";
            // 
            // autoShowLabel
            // 
            this.autoShowLabel.Font = new System.Drawing.Font("Anonymous Pro", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.autoShowLabel.Location = new System.Drawing.Point(48, 111);
            this.autoShowLabel.Name = "autoShowLabel";
            this.autoShowLabel.Size = new System.Drawing.Size(166, 21);
            this.autoShowLabel.TabIndex = 26;
            this.autoShowLabel.Text = "Автозапуск заставки";
            this.autoShowLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // autoLoaderLabel
            // 
            this.autoLoaderLabel.Font = new System.Drawing.Font("Anonymous Pro", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.autoLoaderLabel.Location = new System.Drawing.Point(48, 139);
            this.autoLoaderLabel.Name = "autoLoaderLabel";
            this.autoLoaderLabel.Size = new System.Drawing.Size(142, 21);
            this.autoLoaderLabel.TabIndex = 27;
            this.autoLoaderLabel.Text = "Автозагрузка";
            this.autoLoaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // overWindowLabel
            // 
            this.overWindowLabel.Font = new System.Drawing.Font("Anonymous Pro", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.overWindowLabel.Location = new System.Drawing.Point(48, 167);
            this.overWindowLabel.Name = "overWindowLabel";
            this.overWindowLabel.Size = new System.Drawing.Size(142, 21);
            this.overWindowLabel.TabIndex = 28;
            this.overWindowLabel.Text = "Поверх всех окон";
            this.overWindowLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // endPeriodLabel
            // 
            this.endPeriodLabel.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.endPeriodLabel.Location = new System.Drawing.Point(434, 73);
            this.endPeriodLabel.Name = "endPeriodLabel";
            this.endPeriodLabel.Size = new System.Drawing.Size(39, 22);
            this.endPeriodLabel.TabIndex = 7;
            this.endPeriodLabel.Text = "мин";
            this.endPeriodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timeComboBox
            // 
            this.timeComboBox.BackColor = System.Drawing.SystemColors.Control;
            this.timeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeComboBox.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.timeComboBox.FormattingEnabled = true;
            this.timeComboBox.Items.AddRange(new object[] {
            "0.05",
            "1",
            "3",
            "5",
            "10",
            "15 "});
            this.timeComboBox.Location = new System.Drawing.Point(358, 72);
            this.timeComboBox.Name = "timeComboBox";
            this.timeComboBox.Size = new System.Drawing.Size(68, 25);
            this.timeComboBox.TabIndex = 5;
            this.timeComboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.timeComboBox_DrawItem);
            this.timeComboBox.SelectedIndexChanged += new System.EventHandler(this.TimeComboBox_SelectedIndexChanged);
            // 
            // startPeriodLabel
            // 
            this.startPeriodLabel.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startPeriodLabel.Location = new System.Drawing.Point(197, 72);
            this.startPeriodLabel.Name = "startPeriodLabel";
            this.startPeriodLabel.Size = new System.Drawing.Size(153, 22);
            this.startPeriodLabel.TabIndex = 6;
            this.startPeriodLabel.Text = "Время бездействия:";
            this.startPeriodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // overWindowPictureBox
            // 
            this.overWindowPictureBox.Location = new System.Drawing.Point(17, 167);
            this.overWindowPictureBox.Name = "overWindowPictureBox";
            this.overWindowPictureBox.Size = new System.Drawing.Size(23, 21);
            this.overWindowPictureBox.TabIndex = 25;
            this.overWindowPictureBox.TabStop = false;
            this.overWindowPictureBox.Click += new System.EventHandler(this.overWindowPictureBox_Click);
            // 
            // autoLoaderPictureBox
            // 
            this.autoLoaderPictureBox.Location = new System.Drawing.Point(17, 139);
            this.autoLoaderPictureBox.Name = "autoLoaderPictureBox";
            this.autoLoaderPictureBox.Size = new System.Drawing.Size(23, 21);
            this.autoLoaderPictureBox.TabIndex = 24;
            this.autoLoaderPictureBox.TabStop = false;
            this.autoLoaderPictureBox.Click += new System.EventHandler(this.autoLoaderPictureBox_Click);
            // 
            // autoShowPictureBox
            // 
            this.autoShowPictureBox.Location = new System.Drawing.Point(17, 111);
            this.autoShowPictureBox.Name = "autoShowPictureBox";
            this.autoShowPictureBox.Size = new System.Drawing.Size(23, 21);
            this.autoShowPictureBox.TabIndex = 23;
            this.autoShowPictureBox.TabStop = false;
            this.autoShowPictureBox.Click += new System.EventHandler(this.autoShowPictureBox_Click);
            // 
            // showWallpaperSwitcher
            // 
            this.showWallpaperSwitcher.Image = global::videowp.Properties.Resources.offbtn;
            this.showWallpaperSwitcher.Location = new System.Drawing.Point(513, 139);
            this.showWallpaperSwitcher.Name = "showWallpaperSwitcher";
            this.showWallpaperSwitcher.Size = new System.Drawing.Size(95, 39);
            this.showWallpaperSwitcher.TabIndex = 22;
            this.showWallpaperSwitcher.TabStop = false;
            this.showWallpaperSwitcher.Click += new System.EventHandler(this.showWallpaperSwitcher_Click);
            // 
            // aboutImage
            // 
            this.aboutImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.aboutImage.Image = ((System.Drawing.Image)(resources.GetObject("aboutImage.Image")));
            this.aboutImage.InitialImage = ((System.Drawing.Image)(resources.GetObject("aboutImage.InitialImage")));
            this.aboutImage.Location = new System.Drawing.Point(593, 10);
            this.aboutImage.Name = "aboutImage";
            this.aboutImage.Size = new System.Drawing.Size(21, 18);
            this.aboutImage.TabIndex = 16;
            this.aboutImage.TabStop = false;
            this.aboutImage.MouseHover += new System.EventHandler(this.aboutImage_MouseHover);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(622, 202);
            this.Controls.Add(this.endPeriodLabel);
            this.Controls.Add(this.timeComboBox);
            this.Controls.Add(this.startPeriodLabel);
            this.Controls.Add(this.overWindowLabel);
            this.Controls.Add(this.autoLoaderLabel);
            this.Controls.Add(this.autoShowLabel);
            this.Controls.Add(this.overWindowPictureBox);
            this.Controls.Add(this.autoLoaderPictureBox);
            this.Controls.Add(this.autoShowPictureBox);
            this.Controls.Add(this.showWallpaperSwitcher);
            this.Controls.Add(this.aboutImage);
            this.Controls.Add(this.playlistLabel);
            this.Controls.Add(this.playlistSelectButton);
            this.Controls.Add(this.playlistNameLabel);
            this.Font = new System.Drawing.Font("Anonymous Pro", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Видеобои 1.33";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.overWindowPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.autoLoaderPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.autoShowPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.showWallpaperSwitcher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aboutImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label playlistNameLabel;
        private System.Windows.Forms.Button playlistSelectButton;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label playlistLabel;
        private System.Windows.Forms.PictureBox aboutImage;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.PictureBox showWallpaperSwitcher;
        private System.Windows.Forms.PictureBox autoShowPictureBox;
        private System.Windows.Forms.PictureBox autoLoaderPictureBox;
        private System.Windows.Forms.PictureBox overWindowPictureBox;
        private System.Windows.Forms.Label autoShowLabel;
        private System.Windows.Forms.Label autoLoaderLabel;
        private System.Windows.Forms.Label overWindowLabel;
        private System.Windows.Forms.Label endPeriodLabel;
        private System.Windows.Forms.ComboBox timeComboBox;
        private System.Windows.Forms.Label startPeriodLabel;
    }
}

