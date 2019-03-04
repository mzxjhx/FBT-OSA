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
using System.Text.RegularExpressions;
using FormUI;
namespace CWDM1To4.Option
{
    public partial class FormFWDM :BaseOptionForm
    {
        public FormFWDM()
        {
            InitializeComponent();
        }
        //CalcFWDM fwdm = new CalcFWDM();
        //public FormFWDM(CalcFWDM invoker)
        //    : this()
        //{ 
            
        //}

        public override void LoadPanelContents()
        {
            textBoxFWDMPCP1.Text = inifile.ReadString("FWDM", "FWDMPCP1", "1");
            //textBoxFWDMPCP2.Text = inifile.ReadString("FWDM", "FWDMPCP2", "1");
            textBoxFWDMRCP1.Text = inifile.ReadString("FWDM", "FWDMRCP1", "1");
            //textBoxFWDMRCP2.Text = inifile.ReadString("FWDM", "FWDMRCP2", "1");
            //textBoxFWDMPISO.Text = inifile.ReadString("FWDM", "FWDMPISO", "1");

        }

        public override bool StorePanelContents()
        {
            //匹配1***(.*)~1***(.*)或1***.*(.*)~1***(.*)&1***.*(.*)~1***(.*)   (\.\d)?小数出现一次或零次
            string patten = @"^1\d{3}(\.\d)?~1\d{3}(\.\d)?$|^1\d{3}(\.\d)?~1\d{3}(\.\d)?&1\d{3}(\.\d)?~1\d{3}(\.\d)?$";

            if (!(Regex.IsMatch(textBoxFWDMPCP1.Text, patten) && Regex.IsMatch(textBoxFWDMRCP1.Text, patten)))
            {
                MessageBox.Show(" 不是有效数字！ ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return false;
            }

            if (checkBox1.Checked)
            {

                if (Regex.IsMatch(textBoxPassISOPoint.Text, @"^\d{4}(\.\d)?/\d{4}(\.\d)?$"))
                {
                    CalcFWDM.IsPassBandISO = false;
                    CalcFWDM.PassISOPoints = new string[textBoxPassISOPoint.Text.Trim().Split('/').Length];
                    for (int i = 0; i < textBoxPassISOPoint.Text.Trim().Split('/').Length; i++)
                    {
                        CalcFWDM.PassISOPoints[i] = textBoxPassISOPoint.Text.Trim().Split('/')[i];
                    }
                }
                else
                {
                    MessageBox.Show(" 输入格式错误,按1***/1***格式输入 ", "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
                CalcFWDM.IsPassBandISO = true;

            if (checkBox2.Checked)
            {
                if (Regex.IsMatch(textBoxRefISOPoint.Text, @"^\d{4}(\.\d)?/\d{4}(\.\d)?$"))//格式：1256(.*)/1365(.*)
                {
                    CalcFWDM.IsRefBandISO = false; //ISO算单点
                    CalcFWDM.RefISOPoints = new string[textBoxRefISOPoint.Text.Trim().Split('/').Length];
                    for (int i = 0; i < CalcFWDM.RefISOPoints.Length; i++)
                    {
                        CalcFWDM.RefISOPoints[i] = textBoxRefISOPoint.Text.Trim().Split('/')[i];
                    }
                }
                else
                {
                    MessageBox.Show(" 输入格式错误,按1***/1***格式输入 ", "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
                CalcFWDM.IsRefBandISO = true;

            inifile.WriteString("FWDM", "FWDMPCP1", textBoxFWDMPCP1.Text);
            //inifile.WriteString("FWDM", "FWDMPCP2", textBoxFWDMPCP2.Text);
            inifile.WriteString("FWDM", "FWDMRCP1", textBoxFWDMRCP1.Text);
            //inifile.WriteString("FWDM", "FWDMRCP2", textBoxFWDMRCP2.Text);
            //inifile.WriteString("FWDM", "FWDMPISO", textBoxFWDMPISO.Text);
            Server.OnFWDMChanged(new FwdmEventArgs() 
                { PassBand = textBoxFWDMPCP1.Text, ReflectBand = textBoxFWDMRCP1.Text }
            );

            if (Sweep.GetInstance().TestModel == SweepModel.FWDM)
            {
                StatusBarServices.CenterWave = "";
                StatusBarServices.PB = "";
            }
            return true;
        }

    }
}
