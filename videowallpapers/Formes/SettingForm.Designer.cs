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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.updateTimeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.overWindowsCheckbox = new System.Windows.Forms.CheckBox();
            this.resetUpdateSrvBtn = new System.Windows.Forms.Button();
            this.setUpdateSrvBtn = new System.Windows.Forms.Button();
            this.updateSrvField = new System.Windows.Forms.TextBox();
            this.autoShowCheckbox = new System.Windows.Forms.CheckBox();
            this.autoLoaderCheckbox = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.updateTimeComboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.overWindowsCheckbox);
            this.groupBox1.Controls.Add(this.resetUpdateSrvBtn);
            this.groupBox1.Controls.Add(this.setUpdateSrvBtn);
            this.groupBox1.Controls.Add(this.updateSrvField);
            this.groupBox1.Controls.Add(this.autoShowCheckbox);
            this.groupBox1.Controls.Add(this.autoLoaderCheckbox);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(655, 346);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(399, 284);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 32);
            this.label3.TabIndex = 12;
            this.label3.Text = "МИН";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(85, 282);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(215, 32);
            this.label2.TabIndex = 11;
            this.label2.Text = "Период обновлений";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // updateTimeComboBox
            // 
            this.updateTimeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.updateTimeComboBox.FormattingEnabled = true;
            this.updateTimeComboBox.Items.AddRange(new object[] {
            "1",
            "30",
            "60",
            "120",
            "240",
            "480"});
            this.updateTimeComboBox.Location = new System.Drawing.Point(308, 281);
            this.updateTimeComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.updateTimeComboBox.Name = "updateTimeComboBox";
            this.updateTimeComboBox.Size = new System.Drawing.Size(83, 36);
            this.updateTimeComboBox.TabIndex = 10;
            this.updateTimeComboBox.TabStop = false;
            this.updateTimeComboBox.SelectedIndexChanged += new System.EventHandler(this.UpdateTimeComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 102);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 28);
            this.label1.TabIndex = 9;
            this.label1.Text = "Обновления плейлиста";
            // 
            // overWindowsCheckbox
            // 
            this.overWindowsCheckbox.Location = new System.Drawing.Point(437, 46);
            this.overWindowsCheckbox.Margin = new System.Windows.Forms.Padding(4);
            this.overWindowsCheckbox.Name = "overWindowsCheckbox";
            this.overWindowsCheckbox.Size = new System.Drawing.Size(208, 32);
            this.overWindowsCheckbox.TabIndex = 8;
            this.overWindowsCheckbox.Text = "Поверх всех окон";
            this.overWindowsCheckbox.UseVisualStyleBackColor = true;
            this.overWindowsCheckbox.CheckedChanged += new System.EventHandler(this.OverWindowsCheckbox_CheckedChanged);
            // 
            // resetUpdateSrvBtn
            // 
            this.resetUpdateSrvBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resetUpdateSrvBtn.Location = new System.Drawing.Point(321, 226);
            this.resetUpdateSrvBtn.Margin = new System.Windows.Forms.Padding(4);
            this.resetUpdateSrvBtn.Name = "resetUpdateSrvBtn";
            this.resetUpdateSrvBtn.Size = new System.Drawing.Size(113, 36);
            this.resetUpdateSrvBtn.TabIndex = 7;
            this.resetUpdateSrvBtn.Text = "Сбросить";
            this.resetUpdateSrvBtn.UseVisualStyleBackColor = true;
            this.resetUpdateSrvBtn.Click += new System.EventHandler(this.ResetUpdateSrvBtn_Click);
            // 
            // setUpdateSrvBtn
            // 
            this.setUpdateSrvBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.setUpdateSrvBtn.Location = new System.Drawing.Point(187, 226);
            this.setUpdateSrvBtn.Margin = new System.Windows.Forms.Padding(4);
            this.setUpdateSrvBtn.Name = "setUpdateSrvBtn";
            this.setUpdateSrvBtn.Size = new System.Drawing.Size(113, 36);
            this.setUpdateSrvBtn.TabIndex = 5;
            this.setUpdateSrvBtn.Text = "Установить";
            this.setUpdateSrvBtn.UseVisualStyleBackColor = true;
            this.setUpdateSrvBtn.Click += new System.EventHandler(this.SetUpdateSrvBtn_Click);
            // 
            // updateSrvField
            // 
            this.updateSrvField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.updateSrvField.Location = new System.Drawing.Point(71, 132);
            this.updateSrvField.Margin = new System.Windows.Forms.Padding(4);
            this.updateSrvField.Name = "updateSrvField";
            this.updateSrvField.Size = new System.Drawing.Size(490, 34);
            this.updateSrvField.TabIndex = 3;
            this.updateSrvField.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.updateSrvField.DoubleClick += new System.EventHandler(this.UpdateSrvField_DoubleClick);
            // 
            // autoShowCheckbox
            // 
            this.autoShowCheckbox.AutoSize = true;
            this.autoShowCheckbox.Location = new System.Drawing.Point(21, 46);
            this.autoShowCheckbox.Margin = new System.Windows.Forms.Padding(4);
            this.autoShowCheckbox.Name = "autoShowCheckbox";
            this.autoShowCheckbox.Size = new System.Drawing.Size(222, 32);
            this.autoShowCheckbox.TabIndex = 1;
            this.autoShowCheckbox.Text = "Автозапуск заставки";
            this.autoShowCheckbox.UseVisualStyleBackColor = true;
            this.autoShowCheckbox.CheckedChanged += new System.EventHandler(this.AutoShowCheckbox_CheckedChanged);
            // 
            // autoLoaderCheckbox
            // 
            this.autoLoaderCheckbox.AutoSize = true;
            this.autoLoaderCheckbox.Location = new System.Drawing.Point(264, 46);
            this.autoLoaderCheckbox.Margin = new System.Windows.Forms.Padding(4);
            this.autoLoaderCheckbox.Name = "autoLoaderCheckbox";
            this.autoLoaderCheckbox.Size = new System.Drawing.Size(156, 32);
            this.autoLoaderCheckbox.TabIndex = 0;
            this.autoLoaderCheckbox.Text = "Автозагрузка";
            this.autoLoaderCheckbox.UseVisualStyleBackColor = true;
            this.autoLoaderCheckbox.CheckedChanged += new System.EventHandler(this.AutoLoaderCheckbox_CheckedChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.progressBar1.Location = new System.Drawing.Point(70, 183);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(491, 23);
            this.progressBar1.TabIndex = 13;
            this.progressBar1.Visible = false;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(689, 377);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox updateTimeComboBox;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}