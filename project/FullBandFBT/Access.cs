using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.IO;
using ADOX;
using ADODB;
using System.Data;

namespace CWDM1To4
{
    class Access
    {
        public static string Oledbcon="Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
        public string cmd { get; set; }
        public string path { get; set; }
        public string Source { get; set; }

        public static OleDbConnection conn;

        //public Access(string source)
        //{
        //    cmd = Oledbcon + source;
        //}

        /// <summary>
        /// 连接数据库
        /// </summary>
        public void Getcon()
        {
            conn = new OleDbConnection(Oledbcon+Source);
            conn.Open();
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void ConnClose()
        {
            conn.Dispose();
            conn.Close();
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        public void Oledbcmd()
        {

            OleDbCommand olecmd = new OleDbCommand(cmd, conn);
            olecmd.ExecuteNonQuery();
        }


        /// <summary>
        /// 创建access数据库
        /// </summary>
        /// <param name="db">数据库名称</param>
        /// <param name="tb1">表名称</param>
        /// <param name="tb2">表名称</param>
        public void CreatDB(string tb1,string tb2)
        {

            if (!Directory.Exists(@"C:\光纤产品数据库\"))
            {
                Directory.CreateDirectory(@"C:\光纤产品数据库\");
            }

            ///按日期保存数据
            Source = @"C:\光纤产品数据库\";

            Source += DateTime.Now.Date.ToString("D");

            Source += ".mdb";

            if (!File.Exists(Source))
            {
                //File.CreateDirectory(Savename);
                ADOX.Catalog cat = new ADOX.Catalog();

                string stcon = "Provider =Microsoft.Jet.OLEDB.4.0;Data Source = ";

                stcon += Source;
                cat.Create(stcon);

                ADODB.Connection cn = new ADODB.Connection();

                cn.Open(stcon, null, null, -1);
                cat.ActiveConnection = cn;

                ADOX.Table table = new ADOX.Table();
                table.ParentCatalog = cat;
                table.Name = tb1;      //创建一个表                

                ADOX.Column column = new ADOX.Column();
                column.ParentCatalog = cat;

                //table.Columns.Append("Time", ADOX.DataTypeEnum.adDBDate,50);
                table.Columns.Append("Serial", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("ProductType", ADOX.DataTypeEnum.adVarWChar, 50);
                table.Columns.Append("ProcuctNumber", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("WenduType", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("TestStep", ADOX.DataTypeEnum.adVarWChar, 20);
                table.Columns.Append("WorkNumber", ADOX.DataTypeEnum.adVarWChar, 20);

                table.Columns.Append("InsertLoss", ADOX.DataTypeEnum.adDouble, 0);
                table.Columns.Append("Ripple", ADOX.DataTypeEnum.adDouble, 0);
                table.Columns.Append("IsoAdj", ADOX.DataTypeEnum.adDouble, 0);
                table.Columns.Append("Nadj", ADOX.DataTypeEnum.adDouble, 0);
                table.Columns.Append("D_IL", ADOX.DataTypeEnum.adDouble, 0);
                table.Columns.Append("D_Ripple", ADOX.DataTypeEnum.adDouble, 0);
                cat.Tables.Append(table);
                table = null;
                
                ADOX.Table table1 = new ADOX.Table();
                table1.ParentCatalog = cat;
                table1.Name = tb2;

                table1.Columns.Append("Serial", ADOX.DataTypeEnum.adVarWChar, 20);
                table1.Columns.Append("ProductType", ADOX.DataTypeEnum.adVarWChar, 50);
                table1.Columns.Append("ProcuctNumber", ADOX.DataTypeEnum.adVarWChar, 20);
                table1.Columns.Append("WenduType", ADOX.DataTypeEnum.adVarWChar, 20);
                table1.Columns.Append("TestStep", ADOX.DataTypeEnum.adVarWChar, 20);
                table1.Columns.Append("WorkNumber", ADOX.DataTypeEnum.adVarWChar, 20);

                table1.Columns.Append("InsertLoss", ADOX.DataTypeEnum.adDouble, 0);
                table1.Columns.Append("Ripple", ADOX.DataTypeEnum.adDouble, 0);
                table1.Columns.Append("IsoAdj", ADOX.DataTypeEnum.adDouble, 0);

                cat.Tables.Append(table1);
                table1 = null;
                cat = null;
                cn.Close();
            } 

        }
        
        /// <summary>
        /// 创建数据读取器
        /// </summary>
        /// <param name="olestr">oleDB语句</param>
        /// <returns></returns>reader实例
        public OleDbDataReader GetReader(string olestr)
        {
            Getcon();
            OleDbCommand cmdd = new OleDbCommand(olestr,conn);

            OleDbDataReader reader = cmdd.ExecuteReader();
            return reader;
        }

        /// <summary>
        /// 创建数据集
        /// </summary>
        /// <param name="sql">查询命令</param>
        /// <param name="table">表名</param>
        /// <returns>返回数据集对象</returns>
        public DataSet GetdataSet(string sql)//,string table)
        {
            Getcon();
            OleDbDataAdapter SQLda = new OleDbDataAdapter(sql,conn);
            DataSet ds = new DataSet();
            SQLda.Fill(ds);//,table);
            ConnClose();
            return ds;
        }

    }
}
