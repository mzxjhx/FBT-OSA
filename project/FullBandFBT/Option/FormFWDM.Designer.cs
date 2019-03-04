namespace CWDM1To4.Option
{
    partial class FormFWDM
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
            this.label2 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.textBoxFWDMPCP1 = new System.Windows.Forms.TextBox();
            this.textBoxFWDMRCP1 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.textBoxPassISOPoint = new System.Windows.Forms.TextBox();
            this.textBoxRefISOPoint = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "反射波段:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(20, 26);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(59, 12);
            this.label21.TabIndex = 17;
            this.label21.Text = "透射波段:";
            // 
            // textBoxFWDMPCP1
            // 
            this.textBoxFWDMPCP1.Font = new System.Drawing.Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFWDMPCP1.Location = new System.Drawing.Point(81, 20);
            this.textBoxFWDMPCP1.Name = "textBoxFWDMPCP1";
            this.textBoxFWDMPCP1.Size = new System.Drawing.Size(218, 24);
            this.textBoxFWDMPCP1.TabIndex = 13;
            this.textBoxFWDMPCP1.Text = "1260~1490";
            this.textBoxFWDMPCP1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxFWDMRCP1
            // 
            this.textBoxFWDMRCP1.Font = new System.Drawing.Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFWDMRCP1.Location = new System.Drawing.Point(72, 27);
            this.textBoxFWDMRCP1.Name = "textBoxFWDMRCP1";
            this.textBoxFWDMRCP1.Size = new System.Drawing.Size(218, 24);
            this.textBoxFWDMRCP1.TabIndex = 12;
            this.textBoxFWDMRCP1.Text = "1490~1550";
            this.textBoxFWDMRCP1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(20, 54);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(132, 16);
            this.checkBox1.TabIndex = 19;
            this.checkBox1.Text = "按点计算透射隔离度";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(11, 60);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(132, 16);
            this.checkBox2.TabIndex = 20;
            this.checkBox2.Text = "按点计算反射隔离度";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // textBoxPassISOPoint
            // 
            this.textBoxPassISOPoint.Location = new System.Drawing.Point(81, 77);
            this.textBoxPassISOPoint.Name = "textBoxPassISOPoint";
            this.textBoxPassISOPoint.Size = new System.Drawing.Size(218, 21);
            this.textBoxPassISOPoint.TabIndex = 21;
            // 
            // textBoxRefISOPoint
            // 
            this.textBoxRefISOPoint.Location = new System.Drawing.Point(72, 82);
            this.textBoxRefISOPoint.Name = "textBoxRefISOPoint";
            this.textBoxRefISOPoint.Size = new System.Drawing.Size(218, 21);
            this.textBoxRefISOPoint.TabIndex = 22;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxFWDMPCP1);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.textBoxPassISOPoint);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(16, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(317, 115);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "透射";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxFWDMRCP1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBoxRefISOPoint);
            this.groupBox2.Controls.Add(this.checkBox2);
            this.groupBox2.Location = new System.Drawing.Point(16, 141);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(317, 145);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "反射";
            // 
            // FormFWDM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormFWDM";
            this.Size = new System.Drawing.Size(360, 305);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBoxFWDMPCP1;
        private System.Windows.Forms.TextBox textBoxFWDMRCP1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TextBox textBoxPassISOPoint;
        private System.Windows.Forms.TextBox textBoxRefISOPoint;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}