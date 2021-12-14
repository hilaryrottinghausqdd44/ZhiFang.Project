using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Common;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using ZhiFang.ReportFormQueryPrint.IDAL;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter
{
    public class BModuleModuleFormGridLink : IDBModuleModuleFormGridLink
    {
        public int Add(Model.BModuleModuleFormGridLink t)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (t.LabNo != null)
            {
                strSql1.Append("LabNo ,");
                strSql2.Append("'" + t.LabNo + "',");
            }
            if (t.LabID >= 0)
            {
                strSql1.Append("LabID,");
                strSql2.Append(t.LabID + ",");
            }
            if (t.FormID >= 0)
            {
                strSql1.Append("FormID,");
                strSql2.Append(t.FormID + ",");
            }
            if (t.GridID >= 0)
            {
                strSql1.Append("GridID,");
                strSql2.Append(t.GridID + ",");
            }
            if (t.ModuleID >= 0)
            {
                strSql1.Append("ModuleID,");
                strSql2.Append(t.ModuleID + ",");
            }
            if (t.ChartID >= 0)
            {
                strSql1.Append("ChartID,");
                strSql2.Append(t.ChartID + ",");
            }
            if (t.FormCode != null)
            {
                strSql1.Append("FormCode ,");
                strSql2.Append("'" + t.FormCode + "',");
            }
            if (t.GridCode != null)
            {
                strSql1.Append("GridCode ,");
                strSql2.Append("'" + t.GridCode + "',");
            }
            if (t.ChartCode != null)
            {
                strSql1.Append("ChartCode ,");
                strSql2.Append("'" + t.ChartCode + "',");
            }
            if (t.CName != null)
            {
                strSql1.Append("CName ,");
                strSql2.Append("'" + t.CName + "',");
            }
            if (t.TypeID >= 0)
            {
                strSql1.Append("TypeID,");
                strSql2.Append(t.TypeID + ",");
            }
            if (t.TypeName != null)
            {
                strSql1.Append("TypeName ,");
                strSql2.Append("'" + t.TypeName + "',");
            }
            if (t.DispOrder >= 0)
            {
                strSql1.Append("DispOrder,");
                strSql2.Append(t.DispOrder + ",");
            }

            if (t.ShortName != null)
            {
                strSql1.Append("ShortName ,");
                strSql2.Append("'" + t.ShortName + "',");
            }
            if (t.ShortCode != null)
            {
                strSql1.Append("ShortCode ,");
                strSql2.Append("'" + t.ShortCode + "',");
            }
            if (t.StandCode != null)
            {
                strSql1.Append("StandCode ,");
                strSql2.Append("'" + t.StandCode + "',");
            }

            strSql1.Append("IsUse ,");
            if (t.IsUse)
            {
                strSql2.Append("1,");
            }
            else
            {
                strSql2.Append("0,");
            }


            strSql1.Append("ModuleFormGridLinkID,");
            strSql2.Append(GUIDHelp.GetGUIDInt() + ",");
            strSql1.Append("DataAddTime");
            strSql2.Append("'" + DateTime.Now + "'");
            strSql.Append("insert into B_Module_ModuleFormGridLink(");
            strSql.Append(strSql1.ToString());
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString());
            strSql.Append(")");
            ZhiFang.Common.Log.Log.Debug("B_Module_ModuleFormGridLink.Add sql = " + strSql.ToString());
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        public int deleteById(long id)
        {
            int i = 0;
            string sql = "delete from B_Module_ModuleFormGridLink where ModuleFormGridLinkID=" + id;
            ZhiFang.Common.Log.Log.Debug("B_Module_ModuleFormGridLink.deleteById sql = " + sql);
            i = DbHelperSQL.ExecuteSql(sql);
            return i;
        }

        public DataSet GetList(string strWhere)
        {
            DataSet ds = new DataSet();
            if (strWhere == null || strWhere.Length < 1)
            {
                ZhiFang.Common.Log.Log.Debug("BModuleModuleFormGridLink_GetList:条件不符合规范 strWhere=" + strWhere);
                return ds;
            }

            string sql = "select * from B_Module_ModuleFormGridLink where " + strWhere;
            ZhiFang.Common.Log.Log.Debug("BModuleModuleFormGridLink_GetList:sql=" + sql);
            return DbHelperSQL.Query(sql);
        }

        public DataSet GetList(Model.BModuleModuleFormGridLink t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public int Update(Model.BModuleModuleFormGridLink t)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE B_Module_ModuleFormGridLink SET ");

            builder.Append("DataUpdateTime=" + "'" + DateTime.Now + "'");

            if (!string.IsNullOrWhiteSpace(t.LabNo))
            {
                builder.Append(",LabNo=" + "'" + t.LabNo + "'");
            }
            if (t.LabID >= 0)
            {
                builder.Append(",LabID=" + t.LabID);

            }
            if (t.FormID >= 0)
            {
                builder.Append(",FormID=" + t.FormID);

            }
            if (t.GridID >= 0)
            {
                builder.Append(",GridID=" + t.GridID);

            }
            if (t.ModuleID >= 0)
            {
                builder.Append(",ModuleID=" + t.ModuleID);

            }
            if (t.ChartID >= 0)
            {
                builder.Append(",ChartID=" + t.ChartID);

            }
            if (t.FormCode != null)
            {
                builder.Append(",FormCode=" + "'" + t.FormCode + "'");
            }
            if (t.GridCode != null)
            {
                builder.Append(",GridCode=" + "'" + t.GridCode + "'");
            }
            if (t.ChartCode != null)
            {
                builder.Append(",ChartCode=" + "'" + t.ChartCode + "'");
            }
            if (t.CName != null)
            {
                builder.Append(",CName=" + "'" + t.CName + "'");
            }
            if (t.TypeID >= 0)
            {
                builder.Append(",TypeID=" + t.TypeID);
            }
            if (t.TypeName != null)
            {
                builder.Append(",TypeName=" + "'" + t.TypeName + "'");
            }
            if (t.DispOrder >= 0)
            {
                builder.Append(",DispOrder=" + t.DispOrder);
            }
            if (t.IsUse)
            {
                builder.Append(",IsUse=1");
            }
            else
            {
                builder.Append(",IsUse=0");
            }
            if (t.ShortName != null)
            {
                builder.Append(",ShortName=" + "'" + t.ShortName + "'");
            }
            if (t.ShortCode != null)
            {
                builder.Append(",ShortCode=" + "'" + t.ShortCode + "'");
            }
            if (t.StandCode != null)
            {
                builder.Append(",StandCode=" + "'" + t.StandCode + "'");
            }


            builder.Append(" WHERE ModuleFormGridLinkID=" + t.ModuleFormGridLinkID);
            ZhiFang.Common.Log.Log.Debug(builder.ToString());
            return DbHelperSQL.ExecuteSql(builder.ToString());
        }
    }
}
