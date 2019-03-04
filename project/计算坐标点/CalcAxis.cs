using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDMAxia
{
    /*
     *2014-11-27
     *WDM器件类型：WDM单透、WDM双透、FWDM
     *定义两个中心波长，两个带宽，用于设定中心波长
     *程序计算有变动，只需要升级改动该类
     *
     * FWDM器件，反射只有两个有效带宽，透射ISO计算反射带宽的左右两个点。
     */
    public class CalcAxis
    {
        #region Field
        /// <summary>
        /// 是否零通道波长
        /// 中心波长分为0通道和1通道
        /// 0通道如1270，1通道1271
        /// </summary>
        private bool _Ch1IsZero = true;
        private bool _Ch2IsZero = true;
        /// <summary>
        /// DWDM通道带宽
        /// </summary>
        private double DWDMPB = 0.8;

        private bool _Is100G = true;
        /// <summary>
        /// DWDM带宽
        /// </summary>
        public bool Is100G
        {
            get { return _Is100G; }
            set 
            {
                _Is100G = value;
                if (_Is100G)
                {
                    DWDMPB = 0.8;
                }
                else
                    DWDMPB = 1.6;
            }
        }


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
                    return;
                }
                else
                {
                    stopWave = value;
                }
            }
        }

        //

        private double centerWave1;
        /// <summary>
        /// 中心波长1
        /// </summary>
        public double CenterWave1
        {
            get { return centerWave1; }
            set
            {
                if (value < 1260 || value > 1650)
                {
                    return;
                }
                else
                {
                    centerWave1 = value;
                    if (centerWave1 % 10 == 1)
                    {
                        _Ch1IsZero = false;
                    }
                    else
                    {
                        _Ch1IsZero = true;
                    }
                }
            }
        }

        private double centerWave2;
        /// <summary>
        /// 中心波长2
        /// </summary>
        public double CenterWave2
        {
            get { return centerWave2; }
            set
            {
                if (value < 1260 || value > 1650)
                {
                    centerWave2 = 0;
                    //return;
                }
                else
                {
                    centerWave2 = value;
                    if (centerWave2 % 10 == 1)
                    {
                        _Ch2IsZero = false;
                    }
                    else
                    {
                        _Ch2IsZero = true;
                    }
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
                    return;
                }
                else
                {
                    pb1 = value;
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
                    return;
                }
                else
                {
                    pb2 = value;
                }
            }
        }

        //public int CenterWave3 { get; set; }
        //public int CenterWave4 { get; set; }
        public double CenterWave3 { get; set; }
        public double CenterWave4 { get; set; }
        public double PB3 { get; set; }
        public double PB4 { get; set; }

        /*****************************************************************
         * OSA扫描从1260～1650nm范围，共扫描10000个点，计算时要首先确定pb及ITU带宽相应的坐标点位置
         * 以下是计算所需扫描坐标点定义
         * 这里定义双中心波长器件，可以与中心波长器件共用
         ******************************************************************/
        /// <summary>
        /// 左ITU1波长点
        /// </summary>
        public double leftITU1;

        /// <summary>
        /// 中心波长1左带宽点
        /// </summary>
        public double leftPB1 { private set; get; }

        /// <summary>
        /// 中心波长1右带宽波长点
        /// </summary>
        public double rightPB1 { private set; get; }

        /// <summary>
        /// 右ITU1波长点
        /// </summary>
        public double rightITU1;

        /// <summary>
        /// 左ITU2波长点
        /// </summary>
        public double leftITU2;

        /// <summary>
        /// 波长2左带宽点
        /// </summary>
        public double leftPB2 { private set; get; }

        /// <summary>
        /// 波长2右带宽点
        /// </summary>
        public double rightPB2 { private set; get; }

        /// <summary>
        /// ITU2右带宽
        /// </summary>
        public double rightITU2;

        public double leftPB3 { private set; get; }

        public double rightPB3 { private set; get; }

        public double leftITU3;

        public double rightITU3;

        public double leftPB4 { private set; get; }

        public double rightPB4 { private set; get; }

        public double leftITU4;

        public double rightITU4;

        /*
         * 定义四组用于计算的坐标点，从OSA扫描所得10000个点中取得计算需要波长点
         */
        /// <summary>
        /// 
        /// </summary>
        public int XleftITU1, XrightITU1, XleftPB1, XrightPB1;
        /// <summary>
        /// X轴坐标点，参考值数组里的第N个点，计算规格参数
        /// </summary>
        public int XrightITU2, XleftITU2, XleftPB2, XrightPB2;
        /// <summary>
        /// FWDM透射坐标点数组索引
        /// </summary>
        public int XleftITU3, XrightITU3, XleftPB3, XrightPB3;
        /// <summary>
        /// FWDM透射坐标点数组索引
        /// </summary>
        public int XleftITU4, XrightITU4, XleftPB4, XrightPB4;

        /// <summary>
        /// 计算起始坐标点
        /// </summary>
        public int Xstart;
        /// <summary>
        /// 计算终止坐标点
        /// </summary>
        public int Xstop;
        //参考值坐标点
        public int x1260, x1350, x1450, x1550, x1650;

        #endregion

        #region Method

        /// <summary>
        /// DWDM计算所用各波长点
        /// 扫描21500个点，由AQ6370自动计算
        /// 扫描范围1525～1610，分辨率0.02nm
        /// </summary>
        public void GetDWDMAxia()
        {
            leftPB1 = centerWave1 - pb1;
            rightPB1 = centerWave1 + pb1;
            leftITU1 = centerWave1 - (DWDMPB - pb1);
            rightITU1 = centerWave1 + (DWDMPB - pb1);

            XleftITU1 = (int)((leftITU1 - 1525) * 21500 / 85 + 1);
            XleftPB1 = (int)((leftPB1 - 1525) * 21500 / 85 + 1);
            XrightPB1 = (int)((rightPB1 - 1525) * 21500 / 85 + 1);
            XrightITU1 = (int)((rightITU1 - 1525) * 21500 / 85 + 1);
            Xstart = (int)((startWave - 1525) * 21500 / 85 + 1);
            Xstop = (int)((stopWave - 1525) * 21500 / 85 + 1);
            x1260 = 0;
            x1350 = 2309;
            x1450 = 4873;
            x1550 = 7437;
            x1650 = 9999;

        }

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
            leftPB1 = centerWave1 - pb1;
            rightPB1 = centerWave1 + pb1;

            XleftITU1 = (int)((leftITU1 - 1260) * 10000 / (1650 - 1260) + 1);
            XleftPB1 = (int)((leftPB1 - 1260) * 10000 / (1650 - 1260) + 1);
            XrightPB1 = (int)((rightPB1 - 1260) * 10000 / (1650 - 1260) + 1);
            XrightITU1 = (int)((rightITU1 - 1260) * 10000 / (1650 - 1260) + 1);

            if (centerWave2 != 0)
            {
                leftPB2 = centerWave2 - pb2;
                rightPB2 = centerWave2 + pb2;
                XleftITU2 = (int)((leftITU2 - 1260) * 10000 / (1650 - 1260) + 1);
                XleftPB2 = (int)((leftPB2 - 1260) * 10000 / (1650 - 1260) + 1);
                XrightPB2 = (int)((rightPB2 - 1260) * 10000 / (1650 - 1260) + 1);
                XrightITU2 = (int)((rightITU2 - 1260) * 10000 / (1650 - 1260) + 1);
            }

            //反射波长点
            leftPB3 = CenterWave3 - PB3;
            rightPB3 = CenterWave3 + PB3;

            XleftITU3 = (int)((leftITU3 - 1260) * 10000 / (1650 - 1260) + 1);
            XleftPB3 = (int)((leftPB3 - 1260) * 10000 / (1650 - 1260) + 1);
            XrightPB3 = (int)((rightPB3 - 1260) * 10000 / (1650 - 1260) + 1);
            XrightITU3 = (int)((rightITU3 - 1260) * 10000 / (1650 - 1260) + 1);

            if (CenterWave4 != 0)
            {
                leftPB4 = CenterWave4 - PB4;
                rightPB4 = CenterWave4 + PB4;

                XleftITU4 = (int)((leftITU4 - 1260) * 10000 / (1650 - 1260) + 1);
                XleftPB4 = (int)((leftPB4 - 1260) * 10000 / (1650 - 1260) + 1);
                XrightPB4 = (int)((rightPB4 - 1260) * 10000 / (1650 - 1260) + 1);
                XrightITU4 = (int)((rightITU4 - 1260) * 10000 / (1650 - 1260) + 1);
            }

            Xstart = (int)((startWave - 1260) * 10000 / (1650 - 1260) + 1);
            Xstop = (int)((stopWave - 1260) * 10000 / (1650 - 1260) + 1);
            x1260 = 0;
            x1350 = 2309;
            x1450 = 4873;
            x1550 = 7437;

            //反射波长点

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
            if (pb1 > 7.5)
            {
                if (pb1 % 7.5 == 0)
                {
                    leftITU1 = getLeftISO(centerWave1 - pb1, 7.5, _Ch1IsZero);
                    rightITU1 = getRightISO(centerWave1 + pb1, 7.5, _Ch1IsZero);
                }
                else if (pb1 % 7 == 0)
                {
                    leftITU1 = getLeftISO(centerWave1 - pb1, 7, _Ch1IsZero);
                    rightITU1 = getRightISO(centerWave1 + pb1, 7, _Ch1IsZero);
                }
                else if (pb1 % 6.5 == 0)
                {
                    leftITU1 = getLeftISO(centerWave1 - pb1, 6.5, _Ch1IsZero);
                    rightITU1 = getRightISO(centerWave1 + pb1, 6.5, _Ch1IsZero);
                }
                else if (pb1 % 6 == 0)
                {
                    leftITU1 = getLeftISO(centerWave1 - pb1, 6, _Ch1IsZero);
                    rightITU1 = getRightISO(centerWave1 + pb1, 6, _Ch1IsZero);
                }
                else
                {
                    leftITU1 = getLeftISO(centerWave1 - pb1, pb1, _Ch1IsZero);
                    rightITU1 = getRightISO(centerWave1 + pb1, pb1, _Ch1IsZero);
                }
            }
            else
            {
                leftITU1 = getLeftISO(centerWave1 - pb1, pb1, _Ch1IsZero);
                rightITU1 = getRightISO(centerWave1 + pb1, pb1, _Ch1IsZero);
            }


            //leftITU1 = centerWave1 - 20 + pb1;
            //rightITU1 = centerWave1 + 20 - pb1;

            leftPB2 = centerWave2 - pb2;
            rightPB2 = centerWave2 + pb2;
            leftITU2 = getLeftISO(centerWave2 - pb2, pb2, _Ch2IsZero);
            rightITU2 = getRightISO(centerWave2 + pb2, pb2, _Ch2IsZero);
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
        /// <param name="wave">波长</param>
        /// <param name="pb">带宽</param>
        /// <param name="zero">是否0通道器件</param>
        /// <returns></returns>
        private double getLeftISO(double wave, double pb, bool zero)
        {
            double iso = 0;
            if (wave < 1270)
            {
                iso = 0;
            }
            else if (wave >= 1270 && wave < 1290)
            {
                iso = 1270 + pb;
            }
            else if (wave >= 1290 && wave < 1310)
            {
                iso = 1290 + pb;
            }
            else if (wave >= 1310 && wave < 1330)
            {
                iso = 1310 + pb;
            }
            else if (wave >= 1330 && wave < 1350)
            {
                iso = 1330 + pb;
            }
            else if (wave >= 1350 && wave < 1370)
            {
                iso = 1350 + pb;
            }
            else if (wave >= 1370 && wave < 1390)
            {
                iso = 1370 + pb;
            }
            else if (wave >= 1390 && wave < 1410)
            {
                iso = 1390 + pb;
            }
            else if (wave >= 1410 && wave < 1430)
            {
                iso = 1410 + pb;
            }
            else if (wave >= 1430 && wave < 1450)
            {
                iso = 1430 + pb;
            }
            else if (wave >= 1450 && wave < 1470)
            {
                iso = 1450 + pb;
            }
            else if (wave >= 1470 && wave < 1490)
            {
                iso = 1470 + pb;
            }
            else if (wave >= 1490 && wave < 1510)
            {
                iso = 1490 + pb;
            }
            else if (wave >= 1510 && wave < 1530)
            {
                iso = 1510 + pb;
            }
            else if (wave >= 1530 && wave < 1550)
            {
                iso = 1530 + pb;
            }
            else if (wave >= 1550 && wave < 1570)
            {
                iso = 1550 + pb;
            }
            else if (wave >= 1570 && wave < 1590)
            {
                iso = 1570 + pb;
            }
            else if (wave >= 1590 && wave < 1610)
            {
                iso = 1590 + pb;
            }
            else if (wave >= 1610 && wave < 1630)
            {
                iso = 1610 + pb;
            }
            if (zero == false)    //1通道波长
            {
                iso++;
            }
            return iso;
        }

        /// <summary>
        /// 确定右隔离度
        /// </summary>
        /// <param name="wave"></param>
        /// <returns></returns>
        private double getRightISO(double wave, double pb, bool zero)
        {
            double iso = 0;
            if (wave >= 1270 && wave < 1290)
            {
                //iso = 1282.5;
                iso = 1290 - pb;
            }
            else if (wave >= 1290 && wave < 1310)
            {
                //iso = 1302.5;
                iso = 1310 - pb;
            }
            else if (wave >= 1310 && wave < 1330)
            {
                //iso = 1322.5;
                iso = 1330 - pb;
            }
            else if (wave >= 1330 && wave < 1350)
            {
                //iso = 1342.5;
                iso = 1350 - pb;
            }
            else if (wave >= 1350 && wave < 1370)
            {
                //iso = 1362.5;
                iso = 1370 - pb;
            }
            else if (wave >= 1370 && wave < 1390)
            {
                //iso = 1382.5;
                iso = 1390 - pb;
            }
            else if (wave >= 1390 && wave < 1410)
            {
                //iso = 1402.5;
                iso = 1410 - pb;
            }
            else if (wave >= 1410 && wave < 1430)
            {
                //iso = 1422.5;
                iso = 1430 - pb;
            }
            else if (wave >= 1430 && wave < 1450)
            {
                //iso = 1442.5;
                iso = 1450 - pb;
            }
            else if (wave >= 1450 && wave < 1470)
            {
                //iso = 1462.5;
                iso = 1470 - pb;
            }
            else if (wave >= 1470 && wave < 1490)
            {
                //iso = 1482.5;
                iso = 1490 - pb;
            }
            else if (wave >= 1490 && wave < 1510)
            {
                //iso = 1502.5;
                iso = 1510 - pb;
            }
            else if (wave >= 1510 && wave < 1530)
            {
                //iso = 1522.5;
                iso = 1530 - pb;
            }
            else if (wave >= 1530 && wave < 1550)
            {
                //iso = 1542.5;
                iso = 1550 - pb;
            }
            else if (wave >= 1550 && wave < 1570)
            {
                //iso = 1562.5;
                iso = 1570 - pb;
            }
            else if (wave >= 1570 && wave < 1590)
            {
                //iso = 1582.5;
                iso = 1590 - pb;
            }
            else if (wave >= 1590 && wave < 1610)
            {
                //iso = 1602.5;
                iso = 1610 - pb;
            }
            else if (wave >= 1610 && wave < 1630)
            {
                //iso = 1622.5;
                iso = 1630 - pb;
            }
            if (zero == false)    //1通道波长
            {
                iso++;
            }
            return iso;
        }

        #endregion
    }
}
