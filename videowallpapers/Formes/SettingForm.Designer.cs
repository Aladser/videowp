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
            this.autoShowCheckbox = new System.Windows.Forms.CheckBox();
            this.autoLoaderCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.autoShowCheckbox);
            this.groupBox1.Controls.Add(this.autoLoaderCheckbox);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 283);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки";
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
            this.ClientSize = new System.Drawing.Size(418, 307);
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
    }
}