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
    public class BModuleGridControlList : IDBModuleGridControlList
    {
        public int Add(Model.BModuleGridControlList t)
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

            if (t.GridID >= 0)
            {
                strSql1.Append("GridID,");
                strSql2.Append(t.GridID + ",");
            }

            if (t.GridCode != null)
            {
                strSql1.Append("GridCode ,");
                strSql2.Append("'" + t.GridCode + "',");
            }
            if (t.MapField != null)
            {
                strSql1.Append("MapField ,");
                strSql2.Append("'" + t.MapField + "',");
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
            if (t.ColName != null)
            {
                strSql1.Append("ColName ,");
                strSql2.Append("'" + t.ColName + "',");
            }
            if (t.OrderType != null)
            {
                strSql1.Append("OrderType ,");
                strSql2.Append("'" + t.OrderType + "',");
            }
            strSql1.Append("IsOrder ,");
            if (t.IsOrder)
            {
                strSql2.Append("1,");
            }
            else
            {
                strSql2.Append("0,");
            }

            if (t.URL != null)
            {
                strSql1.Append("URL ,");
                strSql2.Append("'" + t.URL + "',");
            }

            if (t.ColData != null)
            {
                strSql1.Append("ColData ,");
                strSql2.Append("'" + t.ColData + "',");
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
            strSql1.Append("IsHide ,");
            if (t.IsHide)
            {
                strSql2.Append("1,");
            }
            else
            {
                strSql2.Append("0,");
            }
            if (t.Width != null)
            {
                strSql1.Append("Width ,");
                strSql2.Append("'" + t.Width + "',");
            }
            if (t.MinWidth != null)
            {
                strSql1.Append("MinWidth ,");
                strSql2.Append("'" + t.MinWidth + "',");
            }
            if (t.Edit != null)
            {
                strSql1.Append("Edit ,");
                strSql2.Append("'" + t.Edit + "',");
            }
            if (t.Toolbar != null)
            {
                strSql1.Append("Toolbar ,");
                strSql2.Append("'" + t.Toolbar + "',");
            }
            if (t.Align != null)
            {
                strSql1.Append("Align ,");
                strSql2.Append("'" + t.Align + "',");
            }
            if (t.Fixed != null)
            {
                strSql1.Append("Fixed ,");
                strSql2.Append("'" + t.Fixed + "',");
            }
            if (t.SourceCode != null)
            {
                strSql1.Append("SourceCode ,");
                strSql2.Append("'" + t.SourceCode + "',");
            }
            strSql1.Append("GridControlID,");
            strSql2.Append(GUIDHelp.GetGUIDInt() + ",");
            strSql1.Append("DataAddTime");
            strSql2.Append("'" + DateTime.Now + "'");
            strSql.Append("insert into B_Module_GridControlList(");
            strSql.Append(strSql1.ToString());
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString());
            strSql.Append(")");
            ZhiFang.Common.Log.Log.Debug("B_Module_GridControlList.Add sql = " + strSql.ToString());
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        public int deleteById(long id)
        {
            int i = 0;
            string sql = "delete from B_Module_GridControlList where GridControlID=" + id;
            ZhiFang.Common.Log.Log.Debug("B_Module_GridControlList.deleteById sql = " + sql);
            i = DbHelperSQL.ExecuteSql(sql);
            return i;
        }

        public DataSet GetList(string strWhere)
        {
            DataSet ds = new DataSet();
            if (strWhere == null || strWhere.Length < 1)
            {
                ZhiFang.Common.Log.Log.Debug("BModuleGridControlList_GetList:条件不符合规范 strWhere=" + strWhere);
                return ds;
            }

            string sql = "select * from B_Module_GridControlList where " + strWhere;
            ZhiFang.Common.Log.Log.Debug("BModuleGridControlList_GetList:sql=" + sql);
            return DbHelperSQL.Query(sql);
        }

        public DataSet GetList(Model.BModuleGridControlList t)
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
                ZhiFang.Common.Log.Log.Debug("BModuleGridControlList_GetListSort:条件不符合规范 strWhere=" + strWhere);
                return ds;
            }

            string sql = "select * from B_Module_GridControlList where " + strWhere;
            if (!string.IsNullOrWhiteSpace(sortFields))
            {
                sql += " order by " + sortFields;
            }
            ZhiFang.Common.Log.Log.Debug("BModuleGridControlList_GetListSort:sql=" + sql);
            return DbHelperSQL.Query(sql);
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public int Update(Model.BModuleGridControlList t)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE B_Module_GridControlList SET ");

            builder.Append("DataUpdateTime=" + "'" + DateTime.Now + "'");

            if (!string.IsNullOrWhiteSpace(t.LabNO))
            {
                builder.Append(",LabNO=" + "'" + t.LabNO + "'");
            }
            if (t.LabID >= 0)
            {
                builder.Append(",LabID=" + t.LabID);
            }
            if (t.GridID >= 0)
            {
                builder.Append(",GridID=" + t.GridID);
            }
            if (t.GridCode != null)
            {
                builder.Append(",GridCode=" + "'" + t.GridCode + "'");
            }
            if (t.MapField != null)
            {
                builder.Append(",MapField=" + "'" + t.MapField + "'");
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
            if (t.ColName != null)
            {
                builder.Append(",ColName=" + "'" + t.ColName + "'");
            }
            if (t.OrderType != null)
            {
                builder.Append(",OrderType=" + "'" + t.OrderType + "'");
            }
            if (t.IsOrder)
            {
                builder.Append(",IsOrder=1");
            }
            else
            {
                builder.Append(",IsOrder=0");
            }
            if (t.ColData != null)
            {
                builder.Append(",ColData=" + "'" + t.ColData + "'");
            }
            if (t.URL != null)
            {
                builder.Append(",URL=" + "'" + t.URL + "'");
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
            if (t.IsHide)
            {
                builder.Append(",IsHide=1");
            }
            else
            {
                builder.Append(",IsHide=0");
            }
            if (t.Width != null)
            {
                builder.Append(",Width=" + "'" + t.Width + "'");
            }
            if (t.MinWidth != null)
            {
                builder.Append(",MinWidth=" + "'" + t.MinWidth + "'");
            }
            if (t.Edit != null)
            {
                builder.Append(",Edit=" + "'" + t.Edit + "'");
            }
            if (t.Toolbar != null)
            {
                builder.Append(",Toolbar=" + "'" + t.Toolbar + "'");
            }
            if (t.Align != null)
            {
                builder.Append(",Align=" + "'" + t.Align + "'");
            }
            if (t.Fixed != null)
            {
                builder.Append(",Fixed=" + "'" + t.Fixed + "'");
            }
            if (t.SourceCode != null)
            {
                builder.Append(",SourceCode=" + "'" + t.SourceCode + "'");
            }



            builder.Append(" WHERE GridControlID=" + t.GridControlID);
            ZhiFang.Common.Log.Log.Debug(builder.ToString());
            return DbHelperSQL.ExecuteSql(builder.ToString());
        }
    }
}
