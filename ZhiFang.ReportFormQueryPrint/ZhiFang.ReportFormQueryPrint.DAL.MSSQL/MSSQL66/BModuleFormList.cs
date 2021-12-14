using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Common;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using ZhiFang.ReportFormQueryPrint.IDAL;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66
{
    public class BModuleFormList : IDBModuleFormList
    {
        public int Add(Model.BModuleFormList t)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (t.LabNO != null)
            {
                strSql1.Append("LabNO ,");
                strSql2.Append("'" + t.LabNO + "',");
            }
            if (t.LabID >= 0)
            {
                strSql1.Append("LabID,");
                strSql2.Append(t.LabID + ",");
            }

            if (t.FormCode != null)
            {
                strSql1.Append("FormCode ,");
                strSql2.Append("'" + t.FormCode + "',");
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
            if (t.ClassID >= 0)
            {
                strSql1.Append("ClassID,");
                strSql2.Append(t.ClassID + ",");
            }
            if (t.ClassName != null)
            {
                strSql1.Append("ClassName ,");
                strSql2.Append("'" + t.ClassName + "',");
            }
            
            if (t.CName != null)
            {
                strSql1.Append("CName ,");
                strSql2.Append("'" + t.CName + "',");
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
            if (t.ZFStandCode != null)
            {
                strSql1.Append("ZFStandCode ,");
                strSql2.Append("'" + t.ZFStandCode + "',");
            }
            if (t.PinYinZiTou != null)
            {
                strSql1.Append("PinYinZiTou ,");
                strSql2.Append("'" + t.PinYinZiTou + "',");
            }
            if (t.DispOrder >= 0)
            {
                strSql1.Append("DispOrder ,");
                strSql2.Append(t.DispOrder + ",");
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
            if (t.SourceCodeUrl != null)
            {
                strSql1.Append("SourceCodeUrl ,");
                strSql2.Append("'" + t.SourceCodeUrl + "',");
            }
            if (t.SourceCode != null)
            {
                strSql1.Append("SourceCode ,");
                strSql2.Append("'" + t.SourceCode + "',");
            }
            if (t.Memo != null)
            {
                strSql1.Append("Memo ,");
                strSql2.Append("'" + t.Memo + "',");
            }

            strSql1.Append("FormID,");
            strSql2.Append(GUIDHelp.GetGUIDInt() + ",");
            strSql1.Append("DataAddTime");
            strSql2.Append("'" + DateTime.Now + "'");
            strSql.Append("insert into B_Module_FormList(");
            strSql.Append(strSql1.ToString());
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString());
            strSql.Append(")");
            ZhiFang.Common.Log.Log.Debug("B_Module_FormList.Add sql = " + strSql.ToString());
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        public int deleteById(long id)
        {
            int i = 0;
            string sql = "delete from B_Module_FormList where FormID=" + id;
            ZhiFang.Common.Log.Log.Debug("B_Module_FormList.deleteById sql = " + sql);
            i = DbHelperSQL.ExecuteSql(sql);
            return i;
        }

        public DataSet GetList(string strWhere)
        {
            DataSet ds = new DataSet();
            if (strWhere == null || strWhere.Length < 1)
            {
                ZhiFang.Common.Log.Log.Debug("BModuleFormList_GetList:条件不符合规范 strWhere=" + strWhere);
                return ds;
            }

            string sql = "select * from B_Module_FormList where " + strWhere;
            ZhiFang.Common.Log.Log.Debug("BModuleFormList_GetList:sql=" + sql);
            return DbHelperSQL.Query(sql);
        }

        public DataSet GetList(Model.BModuleFormList t)
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

        public int Update(Model.BModuleFormList t)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE B_Module_FormList SET ");

            builder.Append("DataUpdateTime=" + "'" + DateTime.Now + "'");

            if (!string.IsNullOrWhiteSpace(t.LabNO))
            {
                builder.Append(",LabNO=" + "'" + t.LabNO + "'");
            }
            if (t.LabID >= 0)
            {
                builder.Append(",LabID=" + t.LabID);
            }
            if (t.TypeID >= 0)
            {
                builder.Append(",TypeID=" + t.TypeID);
            }
            if (t.TypeName != null)
            {
                builder.Append(",TypeName=" + "'" + t.TypeName + "'");
            }
            if (t.ClassID >= 0)
            {
                builder.Append(",ClassID=" + t.ClassID);
            }
            if (t.ClassName != null)
            {
                builder.Append(",ClassName=" + "'" + t.ClassName + "'");
            }
            if (t.FormCode != null)
            {
                builder.Append(",FormCode=" + "'" + t.FormCode + "'");
            }
            
            if (t.CName != null)
            {
                builder.Append(",CName=" + "'" + t.CName + "'");
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
            if (t.ZFStandCode != null)
            {
                builder.Append(",ZFStandCode=" + "'" + t.ZFStandCode + "'");
            }
            if (t.PinYinZiTou != null)
            {
                builder.Append(",PinYinZiTou=" + "'" + t.PinYinZiTou + "'");
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
            if (t.SourceCodeUrl != null)
            {
                builder.Append(",SourceCodeUrl=" + "'" + t.SourceCodeUrl + "'");
            }
            if (t.SourceCode != null)
            {
                builder.Append(",SourceCode=" + "'" + t.SourceCode + "'");
            }
            if (t.Memo != null)
            {
                builder.Append(",Memo=" + "'" + t.Memo + "'");
            }

            builder.Append(" WHERE FormID=" + t.FormID);
            ZhiFang.Common.Log.Log.Debug(builder.ToString());
            return DbHelperSQL.ExecuteSql(builder.ToString());
        }
    }
}
