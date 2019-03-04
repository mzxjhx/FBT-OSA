using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
namespace CWDM1To4
{
    public class SaveCWDM //: ISave
    {
        DataOperation DataOperation = new DataOperation(); 
        /// <summary>
        /// 本机保存数据
        /// </summary>
        /// <param name="info"></param>
        /// <param name="path"></param>
        /// <param name="paramentN"></param>
        /// <param name="paramentL"></param>
        /// <param name="paramentH"></param>
        public void SaveCSV(Info info, string path, Parament paramentN, Parament paramentL, Parament paramentH)
        {
            //每天保存一个文件，以SN号命名，格式CSV
            using (FileStream fs = new FileStream(path + info.SN + ".CSV", FileMode.OpenOrCreate))
            {
                StreamWriter sw = new StreamWriter(fs);//,Encoding.Unicode);

                sw.Write("Number,");
                sw.Write("Specification,");
                sw.Write("SN,");
                sw.Write("TableNo,");
                sw.Write("WorkID,");
                sw.Write("Pass,");

                sw.Write("IL,");
                sw.Write("Ripple,");
                sw.Write("ISO_ADJ,");

                sw.Write("IL,");
                sw.Write("Ripple,");
                sw.Write("ISO_ADJ,");

                sw.Write("IL,");
                sw.Write("Ripple,");
                sw.Write("ISO_ADJ,");
                sw.Write("TDL,");

                sw.Write("IL_R,");
                sw.Write("Ripple_R,");
                sw.Write("ISO_R,");

                sw.Write("IL_R,");
                sw.Write("Ripple_R,");
                sw.Write("ISO_R,");

                sw.Write("IL_R,");
                sw.Write("Ripple_R,");
                sw.Write("ISO_R,");
                sw.WriteLine("TDL_R");


                sw.Write(info.ProductNo + ",");
                sw.Write(info.spec + ",");
                sw.Write(info.SN + ",");
                
                sw.Write(info.Table + ",");
                sw.Write(info.WorkID + ",");
                //sw.Write("PN,");
                if (info.IsPass == true)
                {
                    sw.Write("Yes,");
                }
                else
                {
                    sw.Write("No,");
                }
                sw.Write(paramentN.Insertloss + ",");
                sw.Write(paramentN.Ripple + ",");
                sw.Write(paramentN.ISO_ADJ + ",");

                sw.Write(paramentL.Insertloss + ",");
                sw.Write(paramentL.Ripple + ",");
                sw.Write(paramentL.ISO_ADJ + ",");

                sw.Write(paramentH.Insertloss + ",");
                sw.Write(paramentH.Ripple + ",");
                sw.Write(paramentH.ISO_ADJ + ",");
                sw.Write(paramentH.TDL + ",");

                sw.Write(paramentN.D_IL + ",");
                sw.Write(paramentN.Ripple_R + ",");
                sw.Write(paramentN.ISO_R + ",");

                sw.Write(paramentL.D_IL + ",");
                sw.Write(paramentL.Ripple_R + ",");
                sw.Write(paramentL.ISO_R + ",");

                sw.Write(paramentH.D_IL + ",");
                sw.Write(paramentH.Ripple_R + ",");
                sw.Write(paramentH.ISO_R + ",");
                sw.Write(paramentH.TDL_R);

                sw.Close();
            }

            //每天保存一个文件，以SN号命名，格式CSV
            using (FileStream fs = new FileStream(path + "\\Ref" + info.SN + ".CSV", FileMode.OpenOrCreate))
            {
                StreamWriter sw = new StreamWriter(fs);//,Encoding.Unicode);
                sw.WriteLine("xAxis,PN,PL,PH,RN,RL,RH");
                for (int i = 0; i < 10000; i++)
                {
                    //sw.WriteLine(TraceX[i] + "," + TraceYPN[i] + "," + TraceYPL[i] + "," + TraceYRH[i] + "," + TraceYRN[i] + "," + TraceYRL[i] + "," + TraceYRH[i]);
                    //将保存改成参考值与器件曲线的差值，方便后期直接分析计算
                    sw.WriteLine(info.TraceX[i] + "," + (info.PassRef[i] - info.TraceYPN[i]) + "," + (info.PassRef[i] - info.TraceYPL[i]) + "," + (info.PassRef[i] - info.TraceYPH[i]) + "," + (info.ReflectRef[i] - info.TraceYRN[i]) + "," + (info.ReflectRef[i] - info.TraceYRL[i]) + "," + (info.ReflectRef[i] - info.TraceYRH[i]));
                }
                sw.Flush();
                sw.Close();
            }
        }
        /// <summary>
        /// 保存数据库
        /// </summary>
        public void SaveSql(Info info, Parament paramentN, Parament paramentL, Parament paramentH)
        {
            string pass = info.IsPass == true ? "Pass" : "Failed";
            //            string insert = @"insert into tb_Parament (制造单号,规格,SN,测试台,工号,合格,常温透射IL,常温透射Ripple,常温透射ISO,
            //                        低温透射IL,低温透射Ripple,低温透射ISO,高温透射IL,高温透射Ripple,高温透射ISO,透射TDL,
            //                        常温反射IL,常温反射Ripple,常温反射ISO,低温反射IL,低温反射Ripple,低温反射ISO,
            //                        高温反射IL,高温反射Ripple,高温反射ISO,反射TDL)
            string insert = @"insert into tb_Parament values('" + DateTime.Now.ToString() + "','"
                                + info.ProductNo + "','"
                                + info.spec + "','"
                                + info.SN + "',"
                                + info.Table + ",'"
                                + info.WorkID + "','"
                                + pass + "',"
                                + paramentN.Insertloss + ","
                                + paramentN.Ripple + ","
                                + paramentN.ISO_ADJ + ","

                                + paramentL.Insertloss + ","
                                + paramentL.Ripple + ","
                                + paramentL.ISO_ADJ + ","

                                + paramentH.Insertloss + ","
                                + paramentH.Ripple + ","
                                + paramentH.ISO_ADJ + ","

                                + paramentH.TDL + ","

                                + paramentN.D_IL + ","
                                + paramentN.Ripple_R + ","
                                + paramentN.ISO_R + ","

                                + paramentL.D_IL + ","
                                + paramentL.Ripple_R + ","
                                + paramentL.ISO_R + ","

                                + paramentH.D_IL + ","
                                + paramentH.Ripple_R + ","
                                + paramentH.ISO_R + ","

                                + paramentH.TDL_R + ")";
            DataOperation.getsqlcommand(insert);
            DataOperation.con_close();
        }

        /// <summary>
        /// 更新数据库
        /// </summary>
        public void UpdataSql(Info info, Parament paramentN, Parament paramentL, Parament paramentH)
        {
            string pass = info.IsPass == true ? "Pass" : "Failed";
            string update = @"update tb_Parament set 日期= '" + DateTime.Now.ToString() + "',"
                            + "SN = '" + info.SN + "',"
                            + "测试台 = '" + info.Table + "',"
                            + "合格 = '" + pass + "',"
                            + "常温透射IL = '" + paramentN.Insertloss + "',"
                            + "常温透射Ripple = '" + paramentN.Ripple + "',"
                            + "常温透射ISO = '" + paramentN.ISO_ADJ + "',"

                            + "低温透射IL = '" + paramentL.Insertloss + "',"
                            + "低温透射Ripple = '" + paramentL.Ripple + "',"
                            + "低温透射ISO = '" + paramentL.ISO_ADJ + "',"

                            + "高温透射IL = '" + paramentH.Insertloss + "',"
                            + "高温透射Ripple = '" + paramentH.Ripple + "',"
                            + "高温透射ISO = '" + paramentH.ISO_ADJ + "',"

                            + "透射TDL = '" + paramentH.TDL + "',"

                            + "常温反射IL = '" + paramentN.D_IL + "',"
                            + "常温反射Ripple = '" + paramentN.Ripple_R + "',"
                            + "常温反射ISO = '" + paramentN.ISO_R + "',"

                            + "低温反射IL = '" + paramentL.D_IL + "',"
                            + "低温反射Ripple = '" + paramentL.Ripple_R + "',"
                            + "低温反射ISO = '" + paramentL.ISO_R + "',"

                            + "高温反射IL = '" + paramentH.D_IL + "',"
                            + "高温反射Ripple = '" + paramentH.Ripple_R + "',"
                            + "高温反射ISO = '" + paramentH.ISO_R + "',"

                            + "反射TDL = '" + paramentH.TDL_R + "' where SN ='" + info.SN + "'";


            DataOperation.getsqlcommand(update);
            DataOperation.con_close();
        }
    }

    public class SaveModuel //: ISave
    {
        #region ISave 成员

        public void SaveCSV(Info info, string path, Parament paramentN, Parament paramentL, Parament paramentH)
        {
            
        }

        public void SaveSql(Info info,Parament paramentN,Parament paramentL,Parament paramentH)
        {
            
        }

        public void UpdataSql(Info info,Parament paramentN,Parament paramentL,Parament paramentH)
        { 
            
        }

        #endregion
    }

}
