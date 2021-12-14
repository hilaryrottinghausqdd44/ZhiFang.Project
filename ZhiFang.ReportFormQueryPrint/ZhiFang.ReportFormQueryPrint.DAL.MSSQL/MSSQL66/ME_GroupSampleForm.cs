using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;
using System.Data.SqlClient;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66
{
	/// <summary>
	/// 访问数据层框架微生物
	/// </summary>
    public class ME_GroupSampleForm
    {
        DbHelperSQLObj DbHelperSQL = new DbHelperSQLObj(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));
        public int UpDateMEPrintCount(string GroupSampleFormID,string GTestDate,string OperateHost)
        {
            try
            {
                if (GroupSampleFormID != null && GroupSampleFormID != "" && GTestDate != null && GTestDate != "")
                {
                    SqlParameter sp = new SqlParameter("@GroupSampleFormID", SqlDbType.BigInt);
                    SqlParameter sp1 = new SqlParameter("@GTestDate", SqlDbType.DateTime);
                    SqlParameter sp2 = new SqlParameter("@OperateHost", SqlDbType.VarChar, 50);
                    sp.Value = GroupSampleFormID;
                    sp1.Value = GTestDate;
                    if (OperateHost != "" && OperateHost != null)
                    {
                        sp2.Value = OperateHost;
                    }
                    else {
                        sp2.Value = null;
                    }

                    int  aa = DbHelperSQL.RunProcedure("UpDateMEPrintCount", new SqlParameter[] { sp, sp1, sp2 },  out int rowsAffected);
                    return rowsAffected;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}

