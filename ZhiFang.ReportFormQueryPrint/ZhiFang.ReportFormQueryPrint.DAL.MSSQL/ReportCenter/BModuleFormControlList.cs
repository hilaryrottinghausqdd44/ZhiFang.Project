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
    public class BModuleFormControlList : IDBModuleFormControlList
    {
        public int Add(Model.BModuleFormControlList t)
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

            if (t.FormID >= 0)
            {
                strSql1.Append("FormID,");
                strSql2.Append(t.FormID + ",");
            }

            if (t.FormCode != null)
            {
                strSql1.Append("FormCode ,");
                strSql2.Append("'" + t.FormCode + "',");
            }
            if (t.MapField != null)
            {
                strSql1.Append("MapField ,");
                strSql2.Append("'" + t.MapField + "',");
            }
            if (t.DefaultValue != null)
            {
                strSql1.Append("DefaultValue ,");
                strSql2.Append("'" + t.DefaultValue + "',");
            }
            if (t.TextField != null)
            {
                strSql1.Append("TextField ,");
                strSql2.Append("'" + t.TextField + "',");
            }
            if (t.ValueField != null)
            {
                strSql1.Append("ValueField ,");
                strSql2.Append("'" + t.ValueField + "',");
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
            if (t.ClassName != null)
            {
                strSql1.Append("ClassName ,");
                strSql2.Append("'" + t.ClassName + "',");
            }
            if (t.StyleContent != null)
            {
                strSql1.Append("StyleContent ,");
                strSql2.Append("'" + t.StyleContent + "',");
            }
            if (t.Label != null)
            {
                strSql1.Append("Label ,");
                strSql2.Append("'" + t.Label + "',");
            }
            if (t.CallBackFunc != null)
            {
                strSql1.Append("CallBackFunc ,");
                strSql2.Append("'" + t.CallBackFunc + "',");
            }
            if (t.Cols >= 0)
            {
                strSql1.Append("Cols,");
                strSql2.Append(t.Cols + ",");
            }
            if (t.URL != null)
            {
                strSql1.Append("URL ,");
                strSql2.Append("'" + t.URL + "',");
            }

            if (t.DataJSON != null)
            {
                strSql1.Append("DataJSON ,");
                strSql2.Append("'" + t.DataJSON + "',");
            }
            strSql1.Append("IsHasNull,");
            if (t.IsHasNull)
            {
                strSql2.Append("1,");
            }
            else
            {
                strSql2.Append("0,");
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

            strSql1.Append("FormControlID,");
            strSql2.Append(GUIDHelp.GetGUIDInt() + ",");
            strSql1.Append("DataAddTime");
            strSql2.Append("'" + DateTime.Now + "'");
            strSql.Append("insert into B_Module_FormControlList(");
            strSql.Append(strSql1.ToString());
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString());
            strSql.Append(")");
            ZhiFang.Common.Log.Log.Debug("B_Module_FormControlList.Add sql = " + strSql.ToString());
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        public int deleteById(long id)
        {
            int i = 0;
            string sql = "delete from B_Module_FormControlList where FormControlID=" + id;
            ZhiFang.Common.Log.Log.Debug("B_Module_FormControlList.deleteById sql = " + sql);
            i = DbHelperSQL.ExecuteSql(sql);
            return i;
        }

        public DataSet GetList(string strWhere)
        {
            DataSet ds = new DataSet();
            if (strWhere == null || strWhere.Length < 1)
            {
                ZhiFang.Common.Log.Log.Debug("BModuleFormControlList_GetList:条件不符合规范 strWhere=" + strWhere);
                return ds;
            }

            string sql = "select * from B_Module_FormControlList where " + strWhere;
            ZhiFang.Common.Log.Log.Debug("BModuleFormControlList_GetList:sql=" + sql);
            return DbHelperSQL.Query(sql);
        }

        public DataSet GetList(Model.BModuleFormControlList t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public DataSet GetListSort(string strWhere, string sortFields)
        {
            DataSet ds = new DataSet();
            if (strWhere == null || strWhere.Length < 1)
            {
                ZhiFang.Common.Log.Log.Debug("BModuleFormControlList_GetListSort:条件不符合规范 strWhere=" + strWhere);
                return ds;
            }

            string sql = "select * from B_Module_FormControlList where " + strWhere;
            if (!string.IsNullOrWhiteSpace(sortFields))
            {
                sql += " order by " + sortFields;
            }
            ZhiFang.Common.Log.Log.Debug("BModuleFormControlList_GetListSort:sql=" + sql);
            return DbHelperSQL.Query(sql);
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public int Update(Model.BModuleFormControlList t)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE B_Module_FormControlList SET ");

            builder.Append("DataUpdateTime=" + "'" + DateTime.Now + "'");

            if (!string.IsNullOrWhiteSpace(t.LabNO))
            {
                builder.Append(",LabNO=" + "'" + t.LabNO + "'");
            }
            if (t.LabID >= 0)
            {
                builder.Append(",LabID=" + t.LabID);
            }
            if (t.FormID >= 0)
            {
                builder.Append(",FormID=" + t.FormID);
            }
            if (t.FormCode != null)
            {
                builder.Append(",FormCode=" + "'" + t.FormCode + "'");
            }
            if (t.MapField != null)
            {
                builder.Append(",MapField=" + "'" + t.MapField + "'");
            }
            if (t.DefaultValue != null)
            {
                builder.Append(",DefaultValue=" + "'" + t.DefaultValue + "'");
            }
            if (t.TextField != null)
            {
                builder.Append(",TextField=" + "'" + t.TextField + "'");
            }
            if (t.ValueField != null)
            {
                builder.Append(",ValueField=" + "'" + t.ValueField + "'");
            }
            if (t.TypeID >= 0)
            {
                builder.Append(",TypeID=" + t.TypeID);
            }
            if (t.TypeName != null)
            {
                builder.Append(",TypeName=" + "'" + t.TypeName + "'");
            }
            if (t.ClassName != null)
            {
                builder.Append(",ClassName=" + "'" + t.ClassName + "'");
            }
            if (t.StyleContent != null)
            {
                builder.Append(",StyleContent=" + "'" + t.StyleContent + "'");
            }
            if (t.Label != null)
            {
                builder.Append(",Label=" + "'" + t.Label + "'");
            }
            if (t.CallBackFunc != null)
            {
                builder.Append(",CallBackFunc=" + "'" + t.CallBackFunc + "'");
            }
            if (t.Cols >= 0)
            {
                builder.Append(",Cols=" + t.Cols);
            }
            if (t.URL != null)
            {
                builder.Append(",URL=" + "'" + t.URL + "'");
            }


            if (t.DataJSON != null)
            {
                builder.Append(",DataJSON=" + "'" + t.DataJSON + "'");
            }
            if (t.IsHasNull)
            {
                builder.Append(",IsHasNull=1");
            }
            else
            {
                builder.Append(",IsHasNull=0");
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

            builder.Append(" WHERE FormControlID=" + t.FormControlID);
            ZhiFang.Common.Log.Log.Debug(builder.ToString());
            return DbHelperSQL.ExecuteSql(builder.ToString());
        }
    }
}
