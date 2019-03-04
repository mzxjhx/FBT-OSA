namespace CWDM1To4.Option
{
    partial class FormMake
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
            this.radioButton200G = new System.Windows.Forms.RadioButton();
            this.radioButton100G = new System.Windows.Forms.RadioButton();
            this.txtBoxPB1 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.textBoxCenterWave1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxJianju = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // radioButton200G
            // 
            this.radioButton200G.AutoSize = true;
            this.radioButton200G.Location = new System.Drawing.Point(101, 104);
            this.radioButton200G.Name = "radioButton200G";
            this.radioButton200G.Size = new System.Drawing.Size(71, 16);
            this.radioButton200G.TabIndex = 45;
            this.radioButton200G.Text = "200G产品";
            this.radioButton200G.UseVisualStyleBackColor = true;
            // 
            // radioButton100G
            // 
            this.radioButton100G.AutoSize = true;
            this.radioButton100G.Checked = true;
            this.radioButton100G.Location = new System.Drawing.Point(15, 104);
            this.radioButton100G.Name = "radioButton100G";
            this.radioButton100G.Size = new System.Drawing.Size(71, 16);
            this.radioButton100G.TabIndex = 46;
            this.radioButton100G.TabStop = true;
            this.radioButton100G.Text = "100G产品";
            this.radioButton100G.UseVisualStyleBackColor = true;
            // 
            // txtBoxPB1
            // 
            this.txtBoxPB1.Location = new System.Drawing.Point(234, 32);
            this.txtBoxPB1.Name = "txtBoxPB1";
            this.txtBoxPB1.Size = new System.Drawing.Size(81, 21);
            this.txtBoxPB1.TabIndex = 42;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(189, 36);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 12);
            this.label18.TabIndex = 44;
            this.label18.Text = "带宽：";
            // 
            // textBoxCenterWave1
            // 
            this.textBoxCenterWave1.Location = new System.Drawing.Point(83, 32);
            this.textBoxCenterWave1.Name = "textBoxCenterWave1";
            this.textBoxCenterWave1.Size = new System.Drawing.Size(82, 21);
            this.textBoxCenterWave1.TabIndex = 41;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 43;
            this.label1.Text = "中心波长：";
            // 
            // textBoxJianju
            // 
            this.textBoxJianju.Location = new System.Drawing.Point(83, 66);
            this.textBoxJianju.Name = "textBoxJianju";
            this.textBoxJianju.Size = new System.Drawing.Size(82, 21);
            this.textBoxJianju.TabIndex = 48;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 47;
            this.label2.Text = "间距：";
            // 
            // FormMake
            // 
            this.Controls.Add(this.textBoxJianju);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButton200G);
            this.Controls.Add(this.radioButton100G);
            this.Controls.Add(this.txtBoxPB1);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.textBoxCenterWave1);
            this.Controls.Add(this.label1);
            this.Name = "FormMake";
            this.Size = new System.Drawing.Size(349, 161);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButton200G;
        private System.Windows.Forms.RadioButton radioButton100G;
        private System.Windows.Forms.TextBox txtBoxPB1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBoxCenterWave1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxJianju;
        private System.Windows.Forms.Label label2;
    }
}
