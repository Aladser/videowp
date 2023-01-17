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
            this.resetBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.setUpdateSrvBtn = new System.Windows.Forms.Button();
            this.updateSrvField = new System.Windows.Forms.TextBox();
            this.autoShowCheckbox = new System.Windows.Forms.CheckBox();
            this.autoLoaderCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.resetBtn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.setUpdateSrvBtn);
            this.groupBox1.Controls.Add(this.updateSrvField);
            this.groupBox1.Controls.Add(this.autoShowCheckbox);
            this.groupBox1.Controls.Add(this.autoLoaderCheckbox);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(396, 268);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки";
            // 
            // resetBtn
            // 
            this.resetBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resetBtn.Location = new System.Drawing.Point(178, 202);
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.Size = new System.Drawing.Size(85, 29);
            this.resetBtn.TabIndex = 7;
            this.resetBtn.Text = "Сбросить";
            this.resetBtn.UseVisualStyleBackColor = true;
            this.resetBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 21);
            this.label1.TabIndex = 6;
            this.label1.Text = "Источник видео:";
            // 
            // setUpdateSrvBtn
            // 
            this.setUpdateSrvBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.setUpdateSrvBtn.Location = new System.Drawing.Point(37, 202);
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
            this.updateSrvField.Location = new System.Drawing.Point(16, 156);
            this.updateSrvField.Name = "updateSrvField";
            this.updateSrvField.Size = new System.Drawing.Size(264, 29);
            this.updateSrvField.TabIndex = 3;
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
            this.ClientSize = new System.Drawing.Size(420, 301);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button resetBtn;
    }
}