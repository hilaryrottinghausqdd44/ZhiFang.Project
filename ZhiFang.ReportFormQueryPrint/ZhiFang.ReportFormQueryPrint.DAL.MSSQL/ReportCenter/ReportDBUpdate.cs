using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.Configuration;
using System.Collections.Specialized;
using ZhiFang.ReportFormQueryPrint.DBUtility;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class ReportDBUpdate : IReportDBUpdate
    {
        public int UpdatedReportDB(string reserved)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_Parameter where id = 28");
            bool isdbVersion = DbHelperSQL.Exists(strSql.ToString());
            if (!isdbVersion)
            {
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("INSERT INTO [dbo].[B_Parameter]  VALUES (N'28', N'页面公共配置', N'dbVersion', N'config', N'dbVersion', N'1.0.0.0', NULL, NULL, NULL, NULL, NULL, NULL)");
                int  inum= DbHelperSQL.ExecuteSql(strSql2.ToString());
                if (inum>0) {

                }

            }
            else {

            }


            return 1;
        }

        /// <summary>
        /// 检查数据库对象是否存在（表-U、视图-V、存储过程-P、触发器-TR、标量函数-FN等）
        /// </summary>
        /// <param name="objectname"></param>
        /// <param name="objectType"></param>
        /// <returns></returns>
        private static bool CheckDataObjectIsExists(string objectname, string objectType)
        {
            string objectID = "";
            DataSet ds = DbHelperSQL.Query("select object_id(\'" + objectname + "\', \'" + objectType + "\') as ObjectID");
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                objectID = ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return false;
            }
            if (objectID != null && objectID != "")
            {
                ZhiFang.Common.Log.Log.Debug("objectID:" + objectID);
            }
            else
            {
                return false;
            }
            return (!string.IsNullOrEmpty(objectID));
        }

       
    }
}
