namespace oopv16
{
    partial class Launch
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
            this.Lamp = new System.Windows.Forms.Button();
            this.Heater = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Lamp
            // 
            this.Lamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Lamp.Location = new System.Drawing.Point(12, 145);
            this.Lamp.Name = "Lamp";
            this.Lamp.Size = new System.Drawing.Size(188, 76);
            this.Lamp.TabIndex = 0;
            this.Lamp.Text = "Лампа Накаливания";
            this.Lamp.UseVisualStyleBackColor = true;
            this.Lamp.Click += new System.EventHandler(this.Lamp_Click);
            // 
            // Heater
            // 
            this.Heater.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Heater.Location = new System.Drawing.Point(211, 145);
            this.Heater.Name = "Heater";
            this.Heater.Size = new System.Drawing.Size(188, 76);
            this.Heater.TabIndex = 1;
            this.Heater.Text = "Обогреватель";
            this.Heater.UseVisualStyleBackColor = true;
            this.Heater.Click += new System.EventHandler(this.Heater_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(80, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Что будем симулировать?";
            // 
            // Launch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 352);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Heater);
            this.Controls.Add(this.Lamp);
            this.Name = "Launch";
            this.Text = "Симулятор Электроприборов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Lamp;
        private System.Windows.Forms.Button Heater;
        private System.Windows.Forms.Label label1;
    }
}