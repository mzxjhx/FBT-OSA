using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Windows.Forms;

//Sql server数据库操作
namespace CWDM1To4
{
    class DataOperation
    {
        #region 定义全局变量
        public static SqlConnection My_con;

//        public static string mysqlcon = @"server=PC-20130603DEXQ\SQLEXPRESS;
//                                        integrated security =true;   
//                                        database=db_CWDM";
//        public static string mysqlcon = @"server=192.168.0.109;
//                                        integrated security =true;   
//                                        database=db_CWDM";
        public static string mysqlcon = @"server=192.168.0.5;uid=sa;pwd=1998;database=db_CWDM";

        public static string creat;
        #endregion

        // +DateTime.Now.Date.ToString("yyyy/MM/dd") +

        #region 创建数据库文件
        public void CreatDB()
        {
            if (!Directory.Exists(@"C:\CWDM器件测试数据库\"))
            {
                Directory.CreateDirectory(@"C:\CWDM器件测试数据库\");
            }
            //String datasource= @"Data Source=c:\CWDM器件测试数据库\db_CWDM";

            if (!File.Exists(@"c:\CWDM器件测试数据库\db_CWDM"))
            {
                // 打开数据库连接  if( conn.State != ConnectionState.Open) 
                getcon();
                string sql = "CREATE DATABASE db_CWDM ON PRIMARY" +
                    "(name=db_CWDM,"+
                    "filename =  'C:\\CWDM器件测试数据库\\db_CWDM.mdf',"+
                    "size=3, maxsize=5, filegrowth=10%)"+
                    "log on(name=mydbb_log,"+
                    "filename='C:\\CWDM器件测试数据库\\db_CWDM.ldf',"+
                    "size=3,maxsize=20,filegrowth=1)";

                SqlCommand cmd = new SqlCommand(sql, My_con);  
                try  
                {  
                    cmd.ExecuteNonQuery();  
                }  
                catch(SqlException ae)  
                {  
                    MessageBox.Show(ae.Message.ToString()); 
                } 

            }
        }
        #endregion

        #region 创建数据表文件
        /// <summary>
        /// 创建两个数据表
        /// </summary>
        /// <param name="table1">透射数据表</param>
        /// <param name="table2">反射数据表</param>
        public void CreatTable(string table1,string table2)
        {
            //string datime = DateTime.Now.Date.ToShortDateString();
            //datime = datime.Replace("-", "_");

            getcon();

            //透射表的插入语句
            creat = "CREATE TABLE "+
                     table1+
                     "(Serial nvarchar(50)," +
                     "ProductType  nchar(10)," +
                     "ProcuctNumber nvarchar(50)," +
                     "WenduType nchar(10)," +
                     "TestStep nchar(10)," +
                     "WorkNumber nchar(10)," +
                     "Time datetime," +
                     "InsertLoss float," +
                     "Ripple float," +
                     "IsoAdj float," +
                     "Nadj float," +
                     "D_IL float," +
                     "D_Ripple float)";

            SqlCommand cmd = new SqlCommand(creat, My_con);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ae)
            {
                //MessageBox.Show(ae.Message.ToString());
            } 

            //反射表的插入语句
            creat = "CREATE TABLE " +
                     table2 +
                     "(Serial nvarchar(50)," +
                     "ProductType  nchar(10)," +
                     "ProcuctNumber nvarchar(50)," +
                     "WenduType nchar(10)," +
                     "TestStep nchar(10)," +
                     "WorkNumber nchar(10)," +
                     "Time datetime," +
                     "InsertLoss float," +
                     "Ripple float," +
                     "IsoAdj float)";

            cmd.CommandText = creat;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ae)
            {
                //MessageBox.Show(ae.Message.ToString());
            } 
            cmd.Dispose();
            con_close();

        }
        #endregion

        #region  建立数据库连接
        /// <summary>
        /// 建立数据库连接.
        /// </summary>
        /// <returns>返回SqlConnection对象</returns>
        public static SqlConnection getcon()
        {
            My_con = new SqlConnection(mysqlcon);   //用SqlConnection对象与指定的数据库相连接
            My_con.Open();  //打开数据库连接
            return My_con;  //返回SqlConnection对象的信息
        }
        #endregion

        #region 执行SqlCommand命令
        /// <summary>
        /// 执行SqlCommand
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        public void getsqlcommand(string SQLstr)
        {
            getcon();   //打开与数据库的连接
            SqlCommand SQLcom = new SqlCommand(SQLstr, My_con); //创建一个SqlCommand对象，用于执行SQL语句
            SQLcom.ExecuteNonQuery();   //执行SQL语句
            SQLcom.Dispose();   //释放所有空间
            //con_close();    //调用con_close()方法，关闭与数据库的连接
        }
        #endregion

        #region  关闭数据库连接
        /// <summary>
        /// 关闭于数据库的连接.
        /// </summary>
        public void con_close()
        {
            if (My_con.State == ConnectionState.Open)   //判断是否打开与数据库的连接
            {
                My_con.Close();   //关闭数据库的连接
                My_con.Dispose();   //释放My_con变量的所有空间
            }
        }
        #endregion

        #region  数据读取器
        /// <summary>
        /// 数据读取器.
        /// </summary>
        /// <param name="SQLstr">SQL语句</param>
        /// <returns>返回bool型</returns>
        public SqlDataReader GetDataReader(string SQLstr)
        {
            getcon();   //打开与数据库的连接
            SqlCommand My_com = My_con.CreateCommand(); //创建一个SqlCommand对象，用于执行SQL语句
            My_com.CommandText = SQLstr;    //获取指定的SQL语句
            SqlDataReader My_read = My_com.ExecuteReader(); //执行SQL语名句，生成一个SqlDataReader对象
            //con_close();
            return My_read;
        }
        #endregion

        #region  创建DataSet对象
        /// <summary>
        /// 创建一个DataSet对象
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        /// <param name="M_str_table">表名</param>
        /// <returns>返回DataSet对象</returns>
        public DataSet GetDataSet(string SQLstr)//, string tableName)
        {
            getcon();   //打开与数据库的连接
            SqlDataAdapter SQLda = new SqlDataAdapter(SQLstr, My_con);  //创建一个SqlDataAdapter对象，并获取指定数据表的信息
            DataSet My_DataSet = new DataSet(); //创建DataSet对象
            SQLda.Fill(My_DataSet);//, tableName);  //通过SqlDataAdapter对象的Fill()方法，将数据表信息添加到DataSet对象中
            con_close();    //关闭数据库的连接
            return My_DataSet;  //返回DataSet对象的信息

            //WritePrivateProfileString(string section, string key, string val, string filePath);
        }
        #endregion

        //#region  测试数据库是否赋加
        ///// <summary>
        ///// 测试数据库是否赋加
        ///// </summary>
        //public void con_open()
        //{
        //    getcon();
        //    //con_close();
        //}
        //#endregion

    }
}
