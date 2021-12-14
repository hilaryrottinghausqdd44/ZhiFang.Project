namespace ZhiFang.DAL.MsSql.Weblis
{
    using ZhiFang.Common.Public;
    using ZhiFang.DBUtility;
    using ZhiFang.IDAL;
    using ZhiFang.Model;
    using System;
    using System.Data;
    using System.Text;

    public class ReportFormMerge : BaseDALLisDB, IDReportFormMerge
    {
        public ReportFormMerge(string dbsourceconn)
       {
           DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
       }
        public ReportFormMerge()
       {
           DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
       }
        public DataTable GetModelDataFrFormAll(string[] FormNo)
        {
            try
            {
                string formnoor = "";
                for (int i = 0; i < FormNo.Length; i++)
                {
                    if (FormNo[i] != null && FormNo[i].Trim() != "")
                    {
                        formnoor += " reportformid= '" + FormNo[i].Trim() + "' or ";
                    }
                }
                ;
                string sql = " select * from reportformalldatasource where " + formnoor.Substring(0, formnoor.LastIndexOf("or") - 1) + "";
                DataTable table = new DataTable();
                table = DbHelperSQL.ExecuteDataSet(sql.ToString()).Tables[0];
                table.TableName = "frformall";
                Common.Log.Log.Info(sql.ToString());
                return table;
            }
            catch(Exception ex)
            {
                Common.Log.Log.Error(ex.ToString());
                return new DataTable();
            }
        }
        public DataTable GetModelDataFrFormAll(string[] FormNo,string sortsql)
        {
            try
            {
                string formnoor = "";
                for (int i = 0; i < FormNo.Length; i++)
                {
                    if (FormNo[i] != null && FormNo[i].Trim() != "")
                    {
                        formnoor += " reportformid= '" + FormNo[i].Trim() + "' or ";
                    }
                }
                ;
                string sql = " select * from reportformalldatasource where " + formnoor.Substring(0, formnoor.LastIndexOf("or") - 1) + "";
                sql += " " + sortsql;
                DataTable table = new DataTable();
                table = DbHelperSQL.ExecuteDataSet(sql.ToString()).Tables[0];
                table.TableName = "frformall";
                Common.Log.Log.Info(sql.ToString());
                return table;
            }
            catch (Exception ex)
            {
                Common.Log.Log.Error(ex.ToString());
                return new DataTable();
            }
        }

       
    }
}

