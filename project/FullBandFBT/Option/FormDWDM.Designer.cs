namespace CWDM1To4.Option
{
    partial class FormDWDM
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
            this.label28 = new System.Windows.Forms.Label();
            this.textBoxWaveRegion = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.radioButton200G = new System.Windows.Forms.RadioButton();
            this.radioButton100G = new System.Windows.Forms.RadioButton();
            this.txtBoxPB1 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.textBoxCenterWave1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxISOBand = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(177, 82);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(27, 19);
            this.label28.TabIndex = 40;
            this.label28.Text = "nm";
            // 
            // textBoxWaveRegion
            // 
            this.textBoxWaveRegion.Location = new System.Drawing.Point(91, 81);
            this.textBoxWaveRegion.Name = "textBoxWaveRegion";
            this.textBoxWaveRegion.Size = new System.Drawing.Size(80, 21);
            this.textBoxWaveRegion.TabIndex = 39;
            this.textBoxWaveRegion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(21, 85);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(65, 12);
            this.label27.TabIndex = 38;
            this.label27.Text = "中心波长±";
            // 
            // radioButton200G
            // 
            this.radioButton200G.AutoSize = true;
            this.radioButton200G.Location = new System.Drawing.Point(109, 53);
            this.radioButton200G.Name = "radioButton200G";
            this.radioButton200G.Size = new System.Drawing.Size(71, 16);
            this.radioButton200G.TabIndex = 36;
            this.radioButton200G.Text = "200G产品";
            this.radioButton200G.UseVisualStyleBackColor = true;
            // 
            // radioButton100G
            // 
            this.radioButton100G.AutoSize = true;
            this.radioButton100G.Checked = true;
            this.radioButton100G.Location = new System.Drawing.Point(23, 53);
            this.radioButton100G.Name = "radioButton100G";
            this.radioButton100G.Size = new System.Drawing.Size(71, 16);
            this.radioButton100G.TabIndex = 37;
            this.radioButton100G.TabStop = true;
            this.radioButton100G.Text = "100G产品";
            this.radioButton100G.UseVisualStyleBackColor = true;
            // 
            // txtBoxPB1
            // 
            this.txtBoxPB1.Location = new System.Drawing.Point(240, 21);
            this.txtBoxPB1.Name = "txtBoxPB1";
            this.txtBoxPB1.Size = new System.Drawing.Size(81, 21);
            this.txtBoxPB1.TabIndex = 33;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(195, 25);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 12);
            this.label18.TabIndex = 35;
            this.label18.Text = "带宽：";
            // 
            // textBoxCenterWave1
            // 
            this.textBoxCenterWave1.Location = new System.Drawing.Point(89, 21);
            this.textBoxCenterWave1.Name = "textBoxCenterWave1";
            this.textBoxCenterWave1.Size = new System.Drawing.Size(82, 21);
            this.textBoxCenterWave1.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 34;
            this.label1.Text = "中心波长：";
            // 
            // textBoxISOBand
            // 
            this.textBoxISOBand.Location = new System.Drawing.Point(23, 137);
            this.textBoxISOBand.Name = "textBoxISOBand";
            this.textBoxISOBand.Size = new System.Drawing.Size(298, 21);
            this.textBoxISOBand.TabIndex = 41;
            this.textBoxISOBand.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F);
            this.label2.Location = new System.Drawing.Point(23, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 12);
            this.label2.TabIndex = 42;
            this.label2.Text = "反射波段（** ～ ** && ** ～ **）";
            this.label2.Visible = false;
            // 
            // FormDWDM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxISOBand);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.textBoxWaveRegion);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.radioButton200G);
            this.Controls.Add(this.radioButton100G);
            this.Controls.Add(this.txtBoxPB1);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.textBoxCenterWave1);
            this.Controls.Add(this.label1);
            this.Name = "FormDWDM";
            this.Size = new System.Drawing.Size(345, 178);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox textBoxWaveRegion;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.RadioButton radioButton200G;
        private System.Windows.Forms.RadioButton radioButton100G;
        private System.Windows.Forms.TextBox txtBoxPB1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBoxCenterWave1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxISOBand;
        private System.Windows.Forms.Label label2;
    }
}