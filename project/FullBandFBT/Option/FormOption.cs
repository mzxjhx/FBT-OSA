using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OptionForm;
using CalcSpec;
namespace CWDM1To4.Option
{
    public partial class FormOption : Form
    {
        public FormOption()
        {
            InitializeComponent();
        }

        protected List<IDialogPanel> OptionPanels = new List<IDialogPanel>();

        IDialogPanelDescriptor descriptor;

        private void AcceptEvent(object sender, EventArgs e)
        {
            foreach (IDialogPanel pane in OptionPanels)
            {
                if (!pane.ReceiveDialogMessage(DialogMessage.OK))
                {
                    return;
                }
                //pane.StorePanelContents();
            }
            DialogResult = DialogResult.OK;

            this.Close();
        }

        private void FormOption_Load(object sender, EventArgs e)
        {
            //treeView.MouseDown += new MouseEventHandler(TreeMouseDown);

            button1.Text = "确定";
            treeView.AfterSelect    += new TreeViewEventHandler(SelectNode);
            button1.Click           += new EventHandler(AcceptEvent);
            BuildChildItems();
        }

        void BuildChildItems()
        {
            TreeNode newNode;
            descriptor = new DefaultDialogPanelDescriptor();
            descriptor.DialogPanel = new FormParameter();
            descriptor.Label = "规格指标";
            newNode = new TreeNode(descriptor.Label);
            newNode.Tag = descriptor;
            treeView.Nodes.Add(newNode);

            //descriptor = new DefaultDialogPanelDescriptor();
            //descriptor.DialogPanel = new FormFBT();
            //descriptor.Label = "波段配置";
            //newNode = new TreeNode(descriptor.Label);
            //newNode.Tag = descriptor;
            //treeView.Nodes.Add(newNode);

            //descriptor = new DefaultDialogPanelDescriptor();
            //descriptor.DialogPanel = new FormSweepWave();
            //descriptor.Label = "扫描波长";
            //newNode = new TreeNode(descriptor.Label);
            //newNode.Tag = descriptor;
            //treeView.Nodes.Add(newNode);

            //descriptor = new DefaultDialogPanelDescriptor();
            //descriptor.DialogPanel = new FormSignle();
            //descriptor.Label = "单透器件";
            //newNode = new TreeNode(descriptor.Label);
            //newNode.Tag = descriptor;
            //treeView.Nodes.Add(newNode);

            //descriptor = new DefaultDialogPanelDescriptor();
            //descriptor.DialogPanel = new FormFWDM();
            //descriptor.Label = "FWDM";
            //newNode = new TreeNode(descriptor.Label);
            //newNode.Tag = descriptor;
            //treeView.Nodes.Add(newNode);

            //descriptor = new DefaultDialogPanelDescriptor();
            //descriptor.DialogPanel = new FormDWDM();
            //descriptor.Label = "DWDM";
            //newNode = new TreeNode(descriptor.Label);
            //newNode.Tag = descriptor;
            //treeView.Nodes.Add(newNode);

            //descriptor = new DefaultDialogPanelDescriptor();
            //descriptor.DialogPanel = new FormDual();
            //descriptor.Label = "双透器件";
            //newNode = new TreeNode(descriptor.Label);
            //newNode.Tag = descriptor;
            //treeView.Nodes.Add(newNode);

            //descriptor = new DefaultDialogPanelDescriptor();
            //descriptor.DialogPanel = new FormMake();
            //descriptor.Label = "DWDM芯检";
            //newNode = new TreeNode(descriptor.Label);
            //newNode.Tag = descriptor;
            //treeView.Nodes.Add(newNode);

            //descriptor = new DefaultDialogPanelDescriptor();
            //descriptor.DialogPanel = new FormFD();
            //descriptor.Label = "FD器件";
            //newNode = new TreeNode(descriptor.Label);
            //newNode.Tag = descriptor;
            //treeView.Nodes.Add(newNode);

            //descriptor = new DefaultDialogPanelDescriptor();
            //descriptor.DialogPanel = new FormCOM();
            //descriptor.Label = "光开关端口配置";
            //newNode = new TreeNode(descriptor.Label);
            //newNode.Tag = descriptor;
            //treeView.Nodes.Add(newNode);

        }

        void SelectNode(object sender, TreeViewEventArgs e)
        {
            SetOptionPanelTo(treeView.SelectedNode);
        }

        void SetOptionPanelTo(TreeNode node)
        {
            IDialogPanelDescriptor descriptor = node.Tag as IDialogPanelDescriptor;
            if (descriptor != null && descriptor.DialogPanel != null && descriptor.DialogPanel.Control != null)
            {
                if (!OptionPanels.Contains(descriptor.DialogPanel))
                {
                    descriptor.DialogPanel.Control.Dock = DockStyle.Fill;
                    OptionPanels.Add(descriptor.DialogPanel);
                }
                descriptor.DialogPanel.ReceiveDialogMessage(DialogMessage.Activated);
                this.panel.Controls.Clear();
                this.panel.Controls.Add(descriptor.DialogPanel.Control);

            }
        }
    }
}
