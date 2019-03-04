namespace CWDM1To4.Option
{
    partial class FormSweepWave
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.ipAddressControl = new IPAddressControlLib.IPAddressControl();
            this.textBoxStopWave = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtBoxStartWave = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "服务器IP：";
            // 
            // ipAddressControl
            // 
            this.ipAddressControl.AllowInternalTab = false;
            this.ipAddressControl.AutoHeight = true;
            this.ipAddressControl.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl.Location = new System.Drawing.Point(99, 93);
            this.ipAddressControl.MinimumSize = new System.Drawing.Size(96, 21);
            this.ipAddressControl.Name = "ipAddressControl";
            this.ipAddressControl.ReadOnly = false;
            this.ipAddressControl.Size = new System.Drawing.Size(100, 21);
            this.ipAddressControl.TabIndex = 18;
            this.ipAddressControl.Text = "...";
            // 
            // textBoxStopWave
            // 
            this.textBoxStopWave.Location = new System.Drawing.Point(99, 60);
            this.textBoxStopWave.Name = "textBoxStopWave";
            this.textBoxStopWave.Size = new System.Drawing.Size(100, 21);
            this.textBoxStopWave.TabIndex = 17;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(28, 64);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 16;
            this.label14.Text = "终止波长：";
            // 
            // txtBoxStartWave
            // 
            this.txtBoxStartWave.Location = new System.Drawing.Point(99, 27);
            this.txtBoxStartWave.Name = "txtBoxStartWave";
            this.txtBoxStartWave.Size = new System.Drawing.Size(100, 21);
            this.txtBoxStartWave.TabIndex = 13;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(227, 31);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(125, 12);
            this.label13.TabIndex = 15;
            this.label13.Text = "(半波1460，全波1260)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(28, 32);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 14;
            this.label12.Text = "起始波长：";
            // 
            // FormSweepWave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ipAddressControl);
            this.Controls.Add(this.textBoxStopWave);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtBoxStartWave);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Name = "FormSweepWave";
            this.Size = new System.Drawing.Size(373, 150);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private IPAddressControlLib.IPAddressControl ipAddressControl;
        private System.Windows.Forms.TextBox textBoxStopWave;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtBoxStartWave;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
    }
}
