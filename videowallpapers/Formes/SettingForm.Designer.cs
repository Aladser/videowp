namespace videowp.Formes
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.overWindowsCheckbox = new System.Windows.Forms.CheckBox();
            this.resetUpdateSrvBtn = new System.Windows.Forms.Button();
            this.setUpdateSrvBtn = new System.Windows.Forms.Button();
            this.updateSrvField = new System.Windows.Forms.TextBox();
            this.autoShowCheckbox = new System.Windows.Forms.CheckBox();
            this.autoLoaderCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.overWindowsCheckbox);
            this.groupBox1.Controls.Add(this.resetUpdateSrvBtn);
            this.groupBox1.Controls.Add(this.setUpdateSrvBtn);
            this.groupBox1.Controls.Add(this.updateSrvField);
            this.groupBox1.Controls.Add(this.autoShowCheckbox);
            this.groupBox1.Controls.Add(this.autoLoaderCheckbox);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(396, 257);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(114, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 21);
            this.label1.TabIndex = 9;
            this.label1.Text = "Обновления плейлиста";
            // 
            // overWindowsCheckbox
            // 
            this.overWindowsCheckbox.Location = new System.Drawing.Point(16, 99);
            this.overWindowsCheckbox.Name = "overWindowsCheckbox";
            this.overWindowsCheckbox.Size = new System.Drawing.Size(176, 26);
            this.overWindowsCheckbox.TabIndex = 8;
            this.overWindowsCheckbox.Text = "Поверх всех окон";
            this.overWindowsCheckbox.UseVisualStyleBackColor = true;
            this.overWindowsCheckbox.CheckedChanged += new System.EventHandler(this.OverWindowsCheckbox_CheckedChanged);
            // 
            // resetUpdateSrvBtn
            // 
            this.resetUpdateSrvBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resetUpdateSrvBtn.Location = new System.Drawing.Point(205, 197);
            this.resetUpdateSrvBtn.Name = "resetUpdateSrvBtn";
            this.resetUpdateSrvBtn.Size = new System.Drawing.Size(85, 29);
            this.resetUpdateSrvBtn.TabIndex = 7;
            this.resetUpdateSrvBtn.Text = "Сбросить";
            this.resetUpdateSrvBtn.UseVisualStyleBackColor = true;
            this.resetUpdateSrvBtn.Click += new System.EventHandler(this.ResetUpdateSrvBtn_Click);
            // 
            // setUpdateSrvBtn
            // 
            this.setUpdateSrvBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.setUpdateSrvBtn.Location = new System.Drawing.Point(107, 197);
            this.setUpdateSrvBtn.Name = "setUpdateSrvBtn";
            this.setUpdateSrvBtn.Size = new System.Drawing.Size(85, 29);
            this.setUpdateSrvBtn.TabIndex = 5;
            this.setUpdateSrvBtn.Text = "Установить";
            this.setUpdateSrvBtn.UseVisualStyleBackColor = true;
            this.setUpdateSrvBtn.Click += new System.EventHandler(this.SetUpdateSrvBtn_Click);
            // 
            // updateSrvField
            // 
            this.updateSrvField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.updateSrvField.Location = new System.Drawing.Point(16, 152);
            this.updateSrvField.Name = "updateSrvField";
            this.updateSrvField.Size = new System.Drawing.Size(366, 29);
            this.updateSrvField.TabIndex = 3;
            this.updateSrvField.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.updateSrvField.DoubleClick += new System.EventHandler(this.updateSrvField_DoubleClick);
            // 
            // autoShowCheckbox
            // 
            this.autoShowCheckbox.AutoSize = true;
            this.autoShowCheckbox.Location = new System.Drawing.Point(16, 37);
            this.autoShowCheckbox.Name = "autoShowCheckbox";
            this.autoShowCheckbox.Size = new System.Drawing.Size(176, 25);
            this.autoShowCheckbox.TabIndex = 1;
            this.autoShowCheckbox.Text = "Автозапуск заставки";
            this.autoShowCheckbox.UseVisualStyleBackColor = true;
            this.autoShowCheckbox.CheckedChanged += new System.EventHandler(this.AutoShowCheckbox_CheckedChanged);
            // 
            // autoLoaderCheckbox
            // 
            this.autoLoaderCheckbox.AutoSize = true;
            this.autoLoaderCheckbox.Location = new System.Drawing.Point(16, 68);
            this.autoLoaderCheckbox.Name = "autoLoaderCheckbox";
            this.autoLoaderCheckbox.Size = new System.Drawing.Size(124, 25);
            this.autoLoaderCheckbox.TabIndex = 0;
            this.autoLoaderCheckbox.Text = "Автозагрузка";
            this.autoLoaderCheckbox.UseVisualStyleBackColor = true;
            this.autoLoaderCheckbox.CheckedChanged += new System.EventHandler(this.AutoLoaderCheckbox_CheckedChanged);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(420, 281);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingForm";
            this.Text = "Видеобои";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox autoShowCheckbox;
        private System.Windows.Forms.CheckBox autoLoaderCheckbox;
        private System.Windows.Forms.TextBox updateSrvField;
        private System.Windows.Forms.Button setUpdateSrvBtn;
        private System.Windows.Forms.Button resetUpdateSrvBtn;
        private System.Windows.Forms.CheckBox overWindowsCheckbox;
        private System.Windows.Forms.Label label1;
    }
}