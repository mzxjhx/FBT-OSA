namespace CWDM1To4
{
    partial class FormLogin
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
            this.textBoxPassWord = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comBoxUser = new System.Windows.Forms.ComboBox();
            this.signleButton2 = new FormUI.SignleButton();
            this.signleButton1 = new FormUI.SignleButton();
            this.SuspendLayout();
            // 
            // textBoxPassWord
            // 
            this.textBoxPassWord.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxPassWord.Location = new System.Drawing.Point(246, 149);
            this.textBoxPassWord.Name = "textBoxPassWord";
            this.textBoxPassWord.PasswordChar = '*';
            this.textBoxPassWord.Size = new System.Drawing.Size(92, 23);
            this.textBoxPassWord.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(187, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(199, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "密码：";
            // 
            // comBoxUser
            // 
            this.comBoxUser.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comBoxUser.FormattingEnabled = true;
            this.comBoxUser.Location = new System.Drawing.Point(246, 117);
            this.comBoxUser.Name = "comBoxUser";
            this.comBoxUser.Size = new System.Drawing.Size(92, 22);
            this.comBoxUser.TabIndex = 0;
            // 
            // signleButton2
            // 
            this.signleButton2.BaseColor = System.Drawing.Color.LawnGreen;
            this.signleButton2.ClickColor = System.Drawing.Color.DarkBlue;
            this.signleButton2.Colorstyle = FormUI.SignleButton.Style.Gradient;
            this.signleButton2.Command = null;
            this.signleButton2.EnterColor = System.Drawing.Color.RoyalBlue;
            this.signleButton2.Location = new System.Drawing.Point(265, 196);
            this.signleButton2.Name = "signleButton2";
            this.signleButton2.Size = new System.Drawing.Size(73, 28);
            this.signleButton2.TabIndex = 6;
            this.signleButton2.Text = "退 出";
            this.signleButton2.UseVisualStyleBackColor = true;
            // 
            // signleButton1
            // 
            this.signleButton1.BaseColor = System.Drawing.Color.LawnGreen;
            this.signleButton1.ClickColor = System.Drawing.Color.DarkBlue;
            this.signleButton1.Colorstyle = FormUI.SignleButton.Style.Gradient;
            this.signleButton1.Command = null;
            this.signleButton1.EnterColor = System.Drawing.Color.RoyalBlue;
            this.signleButton1.Location = new System.Drawing.Point(179, 196);
            this.signleButton1.Name = "signleButton1";
            this.signleButton1.Size = new System.Drawing.Size(73, 28);
            this.signleButton1.TabIndex = 5;
            this.signleButton1.Text = "登 录";
            this.signleButton1.UseVisualStyleBackColor = true;
            // 
            // FormLogin
            // 
            this.AcceptButton = this.signleButton1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(362, 236);
            this.ControlBox = false;
            this.Controls.Add(this.signleButton2);
            this.Controls.Add(this.signleButton1);
            this.Controls.Add(this.comBoxUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPassWord);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormLogin_MouseUp);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormLogin_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormLogin_MouseDown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLogin_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormLogin_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPassWord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comBoxUser;
        private FormUI.SignleButton signleButton1;
        private FormUI.SignleButton signleButton2;
    }
}