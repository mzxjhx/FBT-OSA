namespace CWDM1To4.Option
{
    partial class FormSignle
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
            this.txtBoxPB1 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.textBoxCenterWave1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtBoxPB1
            // 
            this.txtBoxPB1.Location = new System.Drawing.Point(271, 27);
            this.txtBoxPB1.Name = "txtBoxPB1";
            this.txtBoxPB1.Size = new System.Drawing.Size(87, 21);
            this.txtBoxPB1.TabIndex = 13;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(220, 31);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(47, 12);
            this.label18.TabIndex = 15;
            this.label18.Text = "带宽1：";
            // 
            // textBoxCenterWave1
            // 
            this.textBoxCenterWave1.Location = new System.Drawing.Point(104, 27);
            this.textBoxCenterWave1.Name = "textBoxCenterWave1";
            this.textBoxCenterWave1.Size = new System.Drawing.Size(88, 21);
            this.textBoxCenterWave1.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "中心波长1：";
            // 
            // FormSignle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtBoxPB1);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.textBoxCenterWave1);
            this.Controls.Add(this.label1);
            this.Name = "FormSignle";
            this.Size = new System.Drawing.Size(382, 146);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxPB1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBoxCenterWave1;
        private System.Windows.Forms.Label label1;
    }
}