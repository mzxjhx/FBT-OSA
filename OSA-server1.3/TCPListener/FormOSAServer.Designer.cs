namespace OsaServer
{
    partial class FormOSAServer
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOSAServer));
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBoxStatus = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空等待列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialPortAse = new System.IO.Ports.SerialPort(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabControlEx = new CSharpWin.TabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.labelWait = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.listBoxUser = new System.Windows.Forms.ListBox();
            this.labelCurrent = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ribbonbtnStop = new RibbonStyle.RibbonMenuButton();
            this.ribbonBtnStartServer = new RibbonStyle.RibbonMenuButton();
            this.ribbonBtnUnlink = new RibbonStyle.RibbonMenuButton();
            this.ribbonbtnLink = new RibbonStyle.RibbonMenuButton();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.comboBoxOSA = new System.Windows.Forms.ComboBox();
            this.comboBoxSource = new System.Windows.Forms.ComboBox();
            this.btnConfig = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ipAddressOSA = new IPAddressControlLib.IPAddressControl();
            this.label9 = new System.Windows.Forms.Label();
            this.serialPortOsa = new System.IO.Ports.SerialPort(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxCRes = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxCSens = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxDses = new System.Windows.Forms.ComboBox();
            this.comboBoxDres = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxCPoint = new System.Windows.Forms.TextBox();
            this.textBoxDpoint = new System.Windows.Forms.TextBox();
            this.contextMenuStrip.SuspendLayout();
            this.tabControlEx.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-29, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "IP";
            // 
            // richTextBoxStatus
            // 
            this.richTextBoxStatus.ContextMenuStrip = this.contextMenuStrip;
            this.richTextBoxStatus.Location = new System.Drawing.Point(31, 128);
            this.richTextBoxStatus.Name = "richTextBoxStatus";
            this.richTextBoxStatus.Size = new System.Drawing.Size(373, 165);
            this.richTextBoxStatus.TabIndex = 20;
            this.richTextBoxStatus.Text = "";
            this.richTextBoxStatus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.richTextBoxStatus_MouseDown);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.清空ToolStripMenuItem,
            this.清空等待列表ToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(143, 48);
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.清空ToolStripMenuItem.Text = "清空";
            this.清空ToolStripMenuItem.Click += new System.EventHandler(this.清空ToolStripMenuItem_Click);
            // 
            // 清空等待列表ToolStripMenuItem
            // 
            this.清空等待列表ToolStripMenuItem.Name = "清空等待列表ToolStripMenuItem";
            this.清空等待列表ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.清空等待列表ToolStripMenuItem.Text = "清空等待列表";
            this.清空等待列表ToolStripMenuItem.Click += new System.EventHandler(this.清空等待列表ToolStripMenuItem_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Osa服务器正在运行";
            this.notifyIcon.Visible = true;
            // 
            // tabControlEx
            // 
            this.tabControlEx.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx.Controls.Add(this.tabPage1);
            this.tabControlEx.Controls.Add(this.tabPage2);
            this.tabControlEx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlEx.Location = new System.Drawing.Point(0, 0);
            this.tabControlEx.Name = "tabControlEx";
            this.tabControlEx.SelectedIndex = 0;
            this.tabControlEx.Size = new System.Drawing.Size(444, 397);
            this.tabControlEx.TabIndex = 43;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.tabPage1.Controls.Add(this.labelWait);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.listBoxUser);
            this.tabPage1.Controls.Add(this.labelCurrent);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.ribbonbtnStop);
            this.tabPage1.Controls.Add(this.ribbonBtnStartServer);
            this.tabPage1.Controls.Add(this.ribbonBtnUnlink);
            this.tabPage1.Controls.Add(this.ribbonbtnLink);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.richTextBoxStatus);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(436, 368);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "OSA Server";
            // 
            // labelWait
            // 
            this.labelWait.AutoSize = true;
            this.labelWait.ContextMenuStrip = this.contextMenuStrip;
            this.labelWait.Location = new System.Drawing.Point(291, 68);
            this.labelWait.Name = "labelWait";
            this.labelWait.Size = new System.Drawing.Size(17, 12);
            this.labelWait.TabIndex = 58;
            this.labelWait.Text = "无";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 57;
            this.label4.Text = "连接测试台";
            // 
            // listBoxUser
            // 
            this.listBoxUser.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxUser.FormattingEnabled = true;
            this.listBoxUser.ItemHeight = 12;
            this.listBoxUser.Location = new System.Drawing.Point(31, 37);
            this.listBoxUser.Name = "listBoxUser";
            this.listBoxUser.Size = new System.Drawing.Size(157, 76);
            this.listBoxUser.TabIndex = 56;
            // 
            // labelCurrent
            // 
            this.labelCurrent.AutoSize = true;
            this.labelCurrent.Location = new System.Drawing.Point(291, 43);
            this.labelCurrent.Name = "labelCurrent";
            this.labelCurrent.Size = new System.Drawing.Size(17, 12);
            this.labelCurrent.TabIndex = 55;
            this.labelCurrent.Text = "无";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(219, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 54;
            this.label3.Text = "当前使用：";
            // 
            // ribbonbtnStop
            // 
            this.ribbonbtnStop.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.ribbonbtnStop.BackColor = System.Drawing.Color.Transparent;
            this.ribbonbtnStop.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.ribbonbtnStop.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.ribbonbtnStop.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.ribbonbtnStop.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(177)))), ((int)(((byte)(118)))));
            this.ribbonbtnStop.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ribbonbtnStop.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.ribbonbtnStop.FadingSpeed = 35;
            this.ribbonbtnStop.FlatAppearance.BorderSize = 0;
            this.ribbonbtnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonbtnStop.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ribbonbtnStop.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Center;
            this.ribbonbtnStop.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Right;
            this.ribbonbtnStop.ImageOffset = 0;
            this.ribbonbtnStop.IsPressed = false;
            this.ribbonbtnStop.KeepPress = false;
            this.ribbonbtnStop.Location = new System.Drawing.Point(126, 312);
            this.ribbonbtnStop.MaxImageSize = new System.Drawing.Point(0, 0);
            this.ribbonbtnStop.MenuPos = new System.Drawing.Point(0, 0);
            this.ribbonbtnStop.Name = "ribbonbtnStop";
            this.ribbonbtnStop.Radius = 6;
            this.ribbonbtnStop.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.ribbonbtnStop.Size = new System.Drawing.Size(78, 35);
            this.ribbonbtnStop.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.ribbonbtnStop.SplitDistance = 0;
            this.ribbonbtnStop.TabIndex = 51;
            this.ribbonbtnStop.Tag = "";
            this.ribbonbtnStop.Text = " 关闭服务器";
            this.ribbonbtnStop.Title = "";
            this.ribbonbtnStop.UseVisualStyleBackColor = true;
            this.ribbonbtnStop.Click += new System.EventHandler(this.ribbonbtnStop_Click);
            // 
            // ribbonBtnStartServer
            // 
            this.ribbonBtnStartServer.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.ribbonBtnStartServer.BackColor = System.Drawing.Color.Transparent;
            this.ribbonBtnStartServer.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.ribbonBtnStartServer.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.ribbonBtnStartServer.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.ribbonBtnStartServer.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(177)))), ((int)(((byte)(118)))));
            this.ribbonBtnStartServer.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ribbonBtnStartServer.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.ribbonBtnStartServer.FadingSpeed = 35;
            this.ribbonBtnStartServer.FlatAppearance.BorderSize = 0;
            this.ribbonBtnStartServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonBtnStartServer.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ribbonBtnStartServer.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
            this.ribbonBtnStartServer.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
            this.ribbonBtnStartServer.ImageOffset = 0;
            this.ribbonBtnStartServer.IsPressed = false;
            this.ribbonBtnStartServer.KeepPress = false;
            this.ribbonBtnStartServer.Location = new System.Drawing.Point(31, 312);
            this.ribbonBtnStartServer.MaxImageSize = new System.Drawing.Point(0, 0);
            this.ribbonBtnStartServer.MenuPos = new System.Drawing.Point(0, 0);
            this.ribbonBtnStartServer.Name = "ribbonBtnStartServer";
            this.ribbonBtnStartServer.Radius = 6;
            this.ribbonBtnStartServer.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.ribbonBtnStartServer.Size = new System.Drawing.Size(82, 35);
            this.ribbonBtnStartServer.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.ribbonBtnStartServer.SplitDistance = 0;
            this.ribbonBtnStartServer.TabIndex = 50;
            this.ribbonBtnStartServer.Tag = "";
            this.ribbonBtnStartServer.Text = " 启动服务器";
            this.ribbonBtnStartServer.Title = "";
            this.ribbonBtnStartServer.UseVisualStyleBackColor = true;
            this.ribbonBtnStartServer.Click += new System.EventHandler(this.ribbonBtnStartServer_Click);
            // 
            // ribbonBtnUnlink
            // 
            this.ribbonBtnUnlink.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.ribbonBtnUnlink.BackColor = System.Drawing.Color.Transparent;
            this.ribbonBtnUnlink.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.ribbonBtnUnlink.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.ribbonBtnUnlink.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.ribbonBtnUnlink.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(177)))), ((int)(((byte)(118)))));
            this.ribbonBtnUnlink.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ribbonBtnUnlink.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.ribbonBtnUnlink.FadingSpeed = 35;
            this.ribbonBtnUnlink.FlatAppearance.BorderSize = 0;
            this.ribbonBtnUnlink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonBtnUnlink.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ribbonBtnUnlink.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Center;
            this.ribbonBtnUnlink.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Right;
            this.ribbonBtnUnlink.ImageOffset = 0;
            this.ribbonBtnUnlink.IsPressed = false;
            this.ribbonBtnUnlink.KeepPress = false;
            this.ribbonBtnUnlink.Location = new System.Drawing.Point(335, 312);
            this.ribbonBtnUnlink.MaxImageSize = new System.Drawing.Point(0, 0);
            this.ribbonBtnUnlink.MenuPos = new System.Drawing.Point(0, 0);
            this.ribbonBtnUnlink.Name = "ribbonBtnUnlink";
            this.ribbonBtnUnlink.Radius = 6;
            this.ribbonBtnUnlink.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.ribbonBtnUnlink.Size = new System.Drawing.Size(69, 35);
            this.ribbonBtnUnlink.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.ribbonBtnUnlink.SplitDistance = 0;
            this.ribbonBtnUnlink.TabIndex = 53;
            this.ribbonBtnUnlink.Tag = "";
            this.ribbonBtnUnlink.Text = "  断开OSA";
            this.ribbonBtnUnlink.Title = "";
            this.ribbonBtnUnlink.UseVisualStyleBackColor = true;
            this.ribbonBtnUnlink.Click += new System.EventHandler(this.ribbonBtnUnlink_Click);
            // 
            // ribbonbtnLink
            // 
            this.ribbonbtnLink.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.None;
            this.ribbonbtnLink.BackColor = System.Drawing.Color.Transparent;
            this.ribbonbtnLink.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.ribbonbtnLink.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.ribbonbtnLink.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.ribbonbtnLink.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(177)))), ((int)(((byte)(118)))));
            this.ribbonbtnLink.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ribbonbtnLink.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.ribbonbtnLink.FadingSpeed = 35;
            this.ribbonbtnLink.FlatAppearance.BorderSize = 0;
            this.ribbonbtnLink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonbtnLink.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ribbonbtnLink.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.Center;
            this.ribbonbtnLink.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Right;
            this.ribbonbtnLink.ImageOffset = 0;
            this.ribbonbtnLink.IsPressed = false;
            this.ribbonbtnLink.KeepPress = false;
            this.ribbonbtnLink.Location = new System.Drawing.Point(261, 312);
            this.ribbonbtnLink.MaxImageSize = new System.Drawing.Point(0, 0);
            this.ribbonbtnLink.MenuPos = new System.Drawing.Point(0, 0);
            this.ribbonbtnLink.Name = "ribbonbtnLink";
            this.ribbonbtnLink.Radius = 6;
            this.ribbonbtnLink.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.ribbonbtnLink.Size = new System.Drawing.Size(68, 35);
            this.ribbonbtnLink.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.No;
            this.ribbonbtnLink.SplitDistance = 0;
            this.ribbonbtnLink.TabIndex = 52;
            this.ribbonbtnLink.Tag = "";
            this.ribbonbtnLink.Text = "  连接OSA";
            this.ribbonbtnLink.Title = "";
            this.ribbonbtnLink.UseVisualStyleBackColor = true;
            this.ribbonbtnLink.Click += new System.EventHandler(this.ribbonbtnLink_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 49;
            this.label2.Text = "等待扫描：";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.btnConfig);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(436, 368);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "OSA Set";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxDpoint);
            this.groupBox3.Controls.Add(this.textBoxCPoint);
            this.groupBox3.Controls.Add(this.comboBoxDses);
            this.groupBox3.Controls.Add(this.comboBoxDres);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.comboBoxCSens);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.comboBoxCRes);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(19, 142);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(399, 178);
            this.groupBox3.TabIndex = 61;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "扫描配置";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 84);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 12);
            this.label13.TabIndex = 61;
            this.label13.Text = "OSA光开关";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 54);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 61;
            this.label14.Text = "光源光开关";
            // 
            // comboBoxOSA
            // 
            this.comboBoxOSA.FormattingEnabled = true;
            this.comboBoxOSA.Location = new System.Drawing.Point(81, 78);
            this.comboBoxOSA.Name = "comboBoxOSA";
            this.comboBoxOSA.Size = new System.Drawing.Size(80, 20);
            this.comboBoxOSA.TabIndex = 60;
            // 
            // comboBoxSource
            // 
            this.comboBoxSource.FormattingEnabled = true;
            this.comboBoxSource.Location = new System.Drawing.Point(81, 50);
            this.comboBoxSource.Name = "comboBoxSource";
            this.comboBoxSource.Size = new System.Drawing.Size(80, 20);
            this.comboBoxSource.TabIndex = 59;
            // 
            // btnConfig
            // 
            this.btnConfig.Location = new System.Drawing.Point(343, 333);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(75, 23);
            this.btnConfig.TabIndex = 1;
            this.btnConfig.Text = "确定";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.ipAddressOSA);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.comboBoxOSA);
            this.groupBox1.Controls.Add(this.comboBoxSource);
            this.groupBox1.Location = new System.Drawing.Point(19, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 114);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // ipAddressOSA
            // 
            this.ipAddressOSA.AllowInternalTab = false;
            this.ipAddressOSA.AutoHeight = true;
            this.ipAddressOSA.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressOSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressOSA.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressOSA.Location = new System.Drawing.Point(81, 21);
            this.ipAddressOSA.MinimumSize = new System.Drawing.Size(96, 21);
            this.ipAddressOSA.Name = "ipAddressOSA";
            this.ipAddressOSA.ReadOnly = false;
            this.ipAddressOSA.Size = new System.Drawing.Size(116, 21);
            this.ipAddressOSA.TabIndex = 6;
            this.ipAddressOSA.Text = "...";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(29, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "OSAIP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(110, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "C系列方案";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "点数";
            // 
            // comboBoxCRes
            // 
            this.comboBoxCRes.FormattingEnabled = true;
            this.comboBoxCRes.Items.AddRange(new object[] {
            "0.01nm",
            "0.05nm",
            "0.5nm",
            "1nm"});
            this.comboBoxCRes.Location = new System.Drawing.Point(81, 83);
            this.comboBoxCRes.Name = "comboBoxCRes";
            this.comboBoxCRes.Size = new System.Drawing.Size(88, 20);
            this.comboBoxCRes.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "分辨率";
            // 
            // comboBoxCSens
            // 
            this.comboBoxCSens.FormattingEnabled = true;
            this.comboBoxCSens.Items.AddRange(new object[] {
            "norm",
            "mid",
            "high1",
            "high2",
            "high3"});
            this.comboBoxCSens.Location = new System.Drawing.Point(81, 109);
            this.comboBoxCSens.Name = "comboBoxCSens";
            this.comboBoxCSens.Size = new System.Drawing.Size(88, 20);
            this.comboBoxCSens.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "灵敏度";
            // 
            // comboBoxDses
            // 
            this.comboBoxDses.FormattingEnabled = true;
            this.comboBoxDses.Items.AddRange(new object[] {
            "norm",
            "mid",
            "high1",
            "high2",
            "high3"});
            this.comboBoxDses.Location = new System.Drawing.Point(266, 109);
            this.comboBoxDses.Name = "comboBoxDses";
            this.comboBoxDses.Size = new System.Drawing.Size(83, 20);
            this.comboBoxDses.TabIndex = 12;
            // 
            // comboBoxDres
            // 
            this.comboBoxDres.FormattingEnabled = true;
            this.comboBoxDres.Items.AddRange(new object[] {
            "0.01nm",
            "0.05nm",
            "0.5nm",
            "1nm"});
            this.comboBoxDres.Location = new System.Drawing.Point(266, 83);
            this.comboBoxDres.Name = "comboBoxDres";
            this.comboBoxDres.Size = new System.Drawing.Size(83, 20);
            this.comboBoxDres.TabIndex = 10;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(290, 34);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 12);
            this.label15.TabIndex = 7;
            this.label15.Text = "D系列方案";
            // 
            // textBoxCPoint
            // 
            this.textBoxCPoint.Location = new System.Drawing.Point(81, 56);
            this.textBoxCPoint.Name = "textBoxCPoint";
            this.textBoxCPoint.Size = new System.Drawing.Size(88, 21);
            this.textBoxCPoint.TabIndex = 13;
            this.textBoxCPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxDpoint
            // 
            this.textBoxDpoint.Location = new System.Drawing.Point(266, 56);
            this.textBoxDpoint.Name = "textBoxDpoint";
            this.textBoxDpoint.Size = new System.Drawing.Size(83, 21);
            this.textBoxDpoint.TabIndex = 14;
            this.textBoxDpoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FormOSAServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 397);
            this.Controls.Add(this.tabControlEx);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormOSAServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OSA一拖四测试服务器";
            this.Load += new System.EventHandler(this.FormTCPServer_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.tabControlEx.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBoxStatus;
        private System.IO.Ports.SerialPort serialPortAse;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private CSharpWin.TabControlEx tabControlEx;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private RibbonStyle.RibbonMenuButton ribbonBtnStartServer;
        private RibbonStyle.RibbonMenuButton ribbonbtnStop;
        private RibbonStyle.RibbonMenuButton ribbonBtnUnlink;
        private RibbonStyle.RibbonMenuButton ribbonbtnLink;
        private System.IO.Ports.SerialPort serialPortOsa;
        private System.Windows.Forms.Label labelCurrent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem 清空等待列表ToolStripMenuItem;
        private System.Windows.Forms.ListBox listBoxUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelWait;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboBoxOSA;
        private System.Windows.Forms.ComboBox comboBoxSource;
        private IPAddressControlLib.IPAddressControl ipAddressOSA;
        private System.Windows.Forms.ComboBox comboBoxDses;
        private System.Windows.Forms.ComboBox comboBoxDres;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox comboBoxCSens;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxCRes;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDpoint;
        private System.Windows.Forms.TextBox textBoxCPoint;

    }
}

