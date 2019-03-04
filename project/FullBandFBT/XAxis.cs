using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace CWDM1To4
{
    /*
     * 定义坐标类,包括单中心波长器件和双心波长器件
     * 
     * 2014-1-26 添加FWDM器件，该器件有一个透射中心波长和两个反射中心波长
     * 计算时，之前定义双透器件的ITU及PB作为反射。单独定义FWDMCW和FWDMPB
     * 
     */
    public class XAxis
    {
        #region Field
        /// <summary>
        /// 是否零通道波长
        /// 中心波长分为0通道和1通道
        /// 0通道如1270，1通道1271
        /// </summary>
        private bool IsZero = true;

        /// <summary>
        /// 计算起始波长
        /// </summary>
        private int startWave;
        public int StartWave
        {
            get { return startWave; }
            set
            {
                if (value < 1260)
                {
                    MessageBox.Show("计算起始波长值超出范围！", "设置错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    startWave = value;
                }
            }
        }

        /// <summary>
        /// 计算终止波长
        /// </summary>
        private int stopWave;
        public int StopWave
        {
            get { return stopWave; }
            set
            {
                if (value < 1260 || value > 1650)
                {
                    MessageBox.Show("计算终止波长值超出范围！", "设置错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    stopWave = value;
                }
            }
        }

        //

        private int centerWave1;
        /// <summary>
        /// 中心波长1
        /// </summary>
        public int CenterWave1
        {
            get { return centerWave1; }
            set
            {
                if (value < 1260 || value > 1650)
                {
                    MessageBox.Show("中心波长值超出范围！", "设置错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    centerWave1 = value;
                    if (centerWave1 % 10 == 1)
                    {
                        IsZero = false;
                    }
                    //leftPB1 = centerWave1 - pb1;
                    //rightPB1 = centerWave1 + pb1;
                    //leftITU1 = centerWave1 - (20 - pb1);
                    //rightITU1 = centerWave1 + (20 - pb1);
                }
            }
        }

        //

        private int centerWave2;
        /// <summary>
        /// 中心波长2
        /// </summary>
        public int CenterWave2
        {
            get { return centerWave2; }
            set
            {
                if (value < 1260 || value > 1650)
                {
                    MessageBox.Show("中心波长值超出范围！", "设置错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    centerWave2 = value;
                    if (centerWave1 % 10 == 1)
                    {
                        IsZero = false;
                    }
                    //leftPB2 = centerWave2 - pb2;
                    //rightPB2 = centerWave2 + pb2;
                    //leftITU2 = centerWave2 - (20 - pb2);
                    //rightITU2 = centerWave2 + (20 - pb2);
                }
            }
        }

        private double pb1;
        /// <summary>
        /// 带宽1
        /// </summary>
        public double PB1
        {
            get { return pb1; }
            set
            {
                if (value > 390)
                {
                    MessageBox.Show("带宽值超出范围！", "设置错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    pb1 = value;
                    //leftPB1 = centerWave1 - pb1;
                    //rightPB1 = centerWave1 + pb1;
                    //leftITU1 = centerWave1 - (20 - pb1);
                    //rightITU1 = centerWave1 + (20 - pb1);
                }
            }
        }

        //
        private double pb2;
        /// <summary>
        /// 带宽2
        /// </summary>
        public double PB2
        {
            get { return pb2; }
            set
            {
                if (value > 390)
                {
                    MessageBox.Show("带宽值超出范围！", "设置错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    pb2 = value;
                    //leftPB2 = centerWave2 - pb2;
                    //rightPB2 = centerWave2 + pb2;
                    //leftITU2 = centerWave2 - (20 - pb2);
                    //rightITU2 = centerWave2 + (20 - pb2);
                }
            }
        }

        /*****************************************************************
         * OSA扫描从1260～1650nm范围，共扫描10000个点，计算时要首先确定pb及ITU带宽相应的坐标点位置
         * 以下是计算所需扫描坐标点定义
         * 这里定义双中心波长器件，可以与中心波长器件共用
         ******************************************************************/
        /// <summary>
        /// 左ITU1波长点
        /// </summary>
        public double leftITU1 { private set;get; }

        /// <summary>
        /// 中心波长1左带宽点
        /// </summary>
        public double leftPB1{ private set;get; }

        /// <summary>
        /// 中心波长1右带宽波长点
        /// </summary>
        public double rightPB1{ private set;get; }

        /// <summary>
        /// 右ITU1波长点
        /// </summary>
        public double rightITU1{ private set;get; }

        /// <summary>
        /// 左ITU2波长点
        /// </summary>
        public double leftITU2{ private set;get; }

        /// <summary>
        /// 波长2左带宽点
        /// </summary>
        public double leftPB2{ private set;get; }

        /// <summary>
        /// 波长2右带宽点
        /// </summary>
        public double rightPB2{ private set;get; }

        /// <summary>
        /// ITU2右带宽
        /// </summary>
        public double rightITU2{ private set;get; }

        /// <summary>
        /// FWDM透射中心波长
        /// </summary>
        public double FWDMCW;

        /// <summary>
        /// FWDM透射带宽
        /// </summary>
        public double FWDMPB;
        /// <summary>
        /// FWDM透射坐标点数组索引
        /// </summary>
        public int XleftITU3, XrightITU3, XleftPB3, XrightPB3;

        //X轴坐标点，参考值数组里的第N个点，计算规格参数。用于双透CWDM透反射及FWDM反射
        public int XleftITU1, XrightITU1, XleftPB1, XrightPB1, XrightITU2, XleftITU2, XleftPB2, XrightPB2, Xstart, Xstop;
        //参考值坐标点
        public int x1260, x1350, x1450, x1550, x1650;

        #endregion

        #region Method

        /// <summary>
        /// 单透器件带宽、隔离度所在x轴数组坐标位置
        /// OSA扫描波长为1260～1650
        /// </summary>
        public void GetSignelAxia()
        {
            leftPB1 = centerWave1 - pb1;
            rightPB1 = centerWave1 + pb1;
            leftITU1 = centerWave1 - (20 - pb1);
            rightITU1 = centerWave1 + (20 - pb1);
            
            XleftITU1 = (int)((leftITU1 - 1260) * 10000 / (1650 - 1260) + 1);
            XleftPB1 = (int)((leftPB1 - 1260) * 10000 / (1650 - 1260) + 1);
            XrightPB1 = (int)((rightPB1 - 1260) * 10000 / (1650 - 1260) + 1);
            XrightITU1 = (int)((rightITU1 - 1260) * 10000 / (1650 - 1260) + 1);
            Xstart = (int)((startWave - 1260) * 10000 / (1650 - 1260) + 1);
            Xstop = (int)((stopWave - 1260) * 10000 / (1650 - 1260) + 1);
            x1260 = 0;
            x1350 = 2309;
            x1450 = 4873;
            x1550 = 7437;
            x1650 = 9999;
        }

        /// <summary>
        /// 计算FWDM透射和反射的坐标点数组索引
        /// </summary>
        public void GetFWDMAxia()
        {

            //透射波长点
            XleftITU3 = (int)((centerWave1 + pb1 - 1260) * 10000 / (1650 - 1260) + 1);//左反射带的右边点
            XleftPB3 = (int)((FWDMCW - FWDMPB - 1260) * 10000 / (1650 - 1260) + 1);
            XrightPB3 = (int)((FWDMCW + FWDMPB - 1260) * 10000 / (1650 - 1260) + 1);
            XrightITU3 = (int)((centerWave2 + pb2 - 1260) * 10000 / (1650 - 1260) + 1);//右反射带的左边点

            //反射波长点
            leftPB1 = centerWave1 - pb1;
            rightPB1 = centerWave1 + pb1;
            leftITU1 = centerWave1 - (20 - pb1);
            rightITU1 = centerWave1 + (20 - pb1);

            leftPB2 = centerWave2 - pb2;
            rightPB2 = centerWave2 + pb2;

            XleftITU1 = XleftPB3;
            XrightITU1 = XrightPB3;

            XleftPB1 = (int)((leftPB1 - 1260) * 10000 / (1650 - 1260) + 1);
            XrightPB1 = (int)((rightPB1 - 1260) * 10000 / (1650 - 1260) + 1);
            
            XleftPB2 = (int)((leftPB2 - 1260) * 10000 / (1650 - 1260) + 1);
            XrightPB2 = (int)((rightPB2 - 1260) * 10000 / (1650 - 1260) + 1);

        }

        /// <summary>
        /// 双透器件，计算每通道的计算坐标点
        /// FWDM器件，计算反射坐标点数组索引
        /// FWDM反射只取两个窗口值，不从起始波长算到终止波长
        /// </summary>
        public void GetDualWdmAxia()
        {
            leftPB1 = centerWave1 - pb1;
            rightPB1 = centerWave1 + pb1;
            leftITU1 = getLeftISO(centerWave1 - pb1);
            rightITU1 = getRightISO(centerWave1 + pb1);
            //leftITU1 = centerWave1 - 20 + pb1;
            //rightITU1 = centerWave1 + 20 - pb1;

            leftPB2 = centerWave2 - pb2;
            rightPB2 = centerWave2 + pb2;
            leftITU2 = getLeftISO(centerWave2 - pb2);
            rightITU2 = getRightISO(centerWave2 + pb2);
            //leftITU2 = centerWave2 - (20 - pb2);
            //rightITU2 = centerWave2 + (20 - pb2);

            XleftITU1 = (int)((leftITU1 - 1260) * 10000 / (1650 - 1260) + 1);
            XleftPB1 = (int)((leftPB1 - 1260) * 10000 / (1650 - 1260) + 1);
            XrightPB1 = (int)((rightPB1 - 1260) * 10000 / (1650 - 1260) + 1);
            XrightITU1 = (int)((rightITU1 - 1260) * 10000 / (1650 - 1260) + 1);
            XleftITU2 = (int)((leftITU2 - 1260) * 10000 / (1650 - 1260) + 1);
            XleftPB2 = (int)((leftPB2 - 1260) * 10000 / (1650 - 1260) + 1);
            XrightPB2 = (int)((rightPB2 - 1260) * 10000 / (1650 - 1260) + 1);
            XrightITU2 = (int)((rightITU2 - 1260) * 10000 / (1650 - 1260) + 1);
            Xstart = (int)((startWave - 1260) * 10000 / (1650 - 1260) + 1);
            Xstop = (int)((stopWave - 1260) * 10000 / (1650 - 1260) + 1);
            x1260 = 0;
            x1350 = 2309;
            x1450 = 4873;
            x1550 = 7437;
            x1650 = 9999;
        }

        /// <summary>
        /// 确定左隔离度
        /// </summary>
        /// <param name="wave"></param>
        /// <returns></returns>
        private double getLeftISO(double wave)
        {
            double iso = 0;
            if (wave < 1270)
            {
                iso = 0;
            }
            else if (wave >= 1270 && wave < 1290)
            {
                 iso = 1277.5;
            }
            else if (wave >= 1290 && wave < 1310)
	        {
                 iso = 1297.5;          
	        }
            else if (wave >= 1310 && wave < 1330)
            {
                 iso = 1317.5;
            }
            else if (wave >= 1330 && wave < 1350)
            {
                iso = 1337.5;
            }
            else if (wave >= 1350 && wave < 1370)
            {
                 iso = 1357.5;
            }
            else if (wave >= 1390 && wave < 1410)
            {
                 iso = 1397.5;
            }
            else if (wave >= 1410 && wave < 1430)
            {
                 iso = 1417.5;
            }
            else if (wave >= 1430 && wave < 1450)
            {
                 iso = 1437.5;
            }
            else if (wave >= 1450 && wave < 1470)
            {
                 iso = 1457.5;
            }
            else if (wave >= 1470 && wave < 1490)
            {
                 iso = 1477.5;
            }
            else if (wave >= 1490 && wave < 1510)
            {
                 iso = 1497.5;
            }
            else if (wave >= 1510 && wave < 1530)
            {
                 iso = 1517.5;
            }
            else if (wave >= 1530 && wave < 1550)
            {
                 iso = 1537.5;
            }
            else if (wave >= 1550 && wave < 1570)
            {
                 iso = 1557.5;
            }
            else if (wave >= 1570 && wave < 1590)
            {
                 iso = 1577.5;
            }
            else if (wave >= 1590 && wave < 1610)
            {
                 iso = 1597.5;
            }
            else if (wave >= 1610 && wave < 1630)
            {
                 iso = 1617.5;
            }
            if (IsZero == false)    //1通道波长
            {
                iso++;
            }
            return iso;
        }

        /// <summary>
        /// 确定左隔离度
        /// </summary>
        /// <param name="wave"></param>
        /// <returns></returns>
        private double getRightISO(double wave)
        {
            double iso = 0;
            if (wave >= 1270 && wave < 1290)
            {
                 iso = 1282.5;
            }
            else if (wave >= 1290 && wave < 1310)
            {
                 iso = 1302.5;
            }
            else if (wave >= 1310 && wave < 1330)
            {
                 iso = 1322.5;
            }
             else if (wave >= 1330 && wave < 1350)
	        {
                  iso = 1342.5;
	        }
            else if (wave >= 1350 && wave < 1370)
            {
                 iso = 1362.5;
            }
            else if (wave >= 1370 && wave < 1390)
            {
                 iso = 1382.5;
            }
            else if (wave >= 1390 && wave < 1410)
            {
                 iso = 1402.5;
            }
            else if (wave >= 1410 && wave < 1430)
            {
                 iso = 1422.5;
            }
            else if (wave >= 1430 && wave < 1450)
            {
                 iso = 1442.5;
            }
            else if (wave >= 1450 && wave < 1470)
            {
                 iso = 1462.5;
            }
            else if (wave >= 1470 && wave < 1490)
            {
                 iso = 1482.5;
            }
            else if (wave >= 1490 && wave < 1510)
            {
                 iso = 1502.5;
            }
            else if (wave >= 1510 && wave < 1530)
            {
                 iso = 1522.5;
            }
            else if (wave >= 1530 && wave < 1550)
            {
                 iso = 1542.5;
            }
            else if (wave >= 1550 && wave < 1570)
            {
                 iso = 1562.5;
            }
            else if (wave >= 1570 && wave < 1590)
            {
                 iso = 1582.5;
            }
            else if (wave >= 1590 && wave < 1610)
            {
                 iso = 1602.5;
            }
            else if (wave >= 1610 && wave < 1630)
            {
                iso = 1622.5;
            }
            if (IsZero == false)    //1通道波长
            {
                iso++;
            }
            return iso;
        }

        #endregion
    }
}
