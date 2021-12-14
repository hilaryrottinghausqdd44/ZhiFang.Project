using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;
using ZhiFang.ReportFormQueryPrint.Model;
using System.Data.SqlClient;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter
{
    /// <summary>
    /// 数据访问类Doctor。
    /// </summary>
    public class BParameter : IDBParameter
    {
        public int Add(Model.BParameter t)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            strSql1.Append("LabID,");
            strSql2.Append("0,");
            if (t.Name != null)
            {
                strSql1.Append("Name,");
                strSql2.Append("'" + t.Name + "',");
            }
            if (t.ParaType != null)
            {
                strSql1.Append("ParaType,");
                strSql2.Append("'" + t.ParaType + "',");
            }
            if (t.SName != null)
            {
                strSql1.Append("SName,");
                strSql2.Append("'" + t.SName + "',");
            }
            if (t.ParaNo != null)
            {
                strSql1.Append("ParaNo,");
                strSql2.Append("'" + t.ParaNo + "',");
            }
            if (t.ParaValue != null)
            {
                strSql1.Append("ParaValue,");
                strSql2.Append("'" + t.ParaValue + "',");
            }
            if (t.Site != null)
            {
                strSql1.Append("Site,");
                strSql2.Append("'" + t.Site + "',");
            }
            if (t.ParaDesc != null)
            {
                strSql1.Append("ParaDesc,");
                strSql2.Append("'" + t.ParaDesc + "',");
            }
            if (t.IsUse != null)
            {
                strSql1.Append("IsUse,");
                strSql2.Append("'" + t.IsUse + "',");
            }
            if (t.ShortCode != null)
            {
                strSql1.Append("ShortCode,");
                strSql2.Append("'" + t.ShortCode + "',");
            }
            if (t.PinYinZiTou != null)
            {
                strSql1.Append("PinYinZiTou,");
                strSql2.Append("'" + t.PinYinZiTou + "',");
            }
            strSql1.Append("ParameterID,");
            strSql2.Append(Common.GUIDHelp.GetGUIDInt()+",");
            strSql1.Append("DataUpdateTime");
            strSql2.Append("'" + DateTime.Now + "'");
            strSql.Append("insert into B_Parameter(");
            strSql.Append(strSql1.ToString());
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString());
            strSql.Append(")");
            ZhiFang.Common.Log.Log.Debug("B_Parameter.Add sql = " + strSql.ToString());
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        public int GetCount(string strwhere)
        {
            return 0;
        }
        public bool Exists(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_Parameter");
            strSql.Append(strWhere);
            return DbHelperSQL.Exists(strSql.ToString());
        }
        public DataSet GetList(Model.BParameter t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(string strWhere)
        {
            string sql = "select * from B_Parameter where 1=1 and " + strWhere;
            ZhiFang.Common.Log.Log.Debug("B_Parameter.GetList: sql = " + sql);
            return DbHelperSQL.Query(sql);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public int Update(Model.BParameter t)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder sq = new StringBuilder();
            sq.Append("update B_Parameter set ");
            sq.Append("Name='"+t.Name + "',");
            sq.Append("SName='" + t.SName + "',");
            sq.Append("ParaType='" + t.ParaType + "',");
            sq.Append("ParaNo='" + t.ParaNo + "',");
            sq.Append("ParaValue='" + t.ParaValue + "',");
            sq.Append("DataUpdateTime='" + DateTime.Now + "',");
            sq.Append("Site='" + t.Site + "'");
            sq.Append(" where  ParameterID=" + t.ParameterID + "");
            #region 有问题
            //strSql.Append("update B_Parameter set ");
            //strSql.Append("Name=@Name,");
            //strSql.Append("SName=@SName,");
            //strSql.Append("ParaType=@ParaType,");
            //strSql.Append("ParaNo=@ParaNo,");
            //strSql.Append("ParaValue=@ParaValue,");
            //strSql.Append("Site=@Site,");
            //strSql.Append("DataUpdateTime=@DataUpdateTime");
            //strSql.Append(" where  Id=@Id");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@Name", SqlDbType.VarChar,50),
            //        new SqlParameter("@SName", SqlDbType.VarChar,50),
            //        new SqlParameter("@ParaType", SqlDbType.VarChar,50),
            //        new SqlParameter("@ParaNo", SqlDbType.VarChar,50),
            //        new SqlParameter("@ParaValue", SqlDbType.VarChar,50),
            //        new SqlParameter("@Site", SqlDbType.VarChar,50),
            //        new SqlParameter("@DataUpdateTime", SqlDbType.DateTime),
            //        new SqlParameter("@Id",SqlDbType.BigInt)
            //};
            //parameters[0].Value = t.Name;
            //parameters[1].Value = t.SName;
            //parameters[2].Value = t.ParaType;
            //parameters[3].Value = t.ParaNo;
            //parameters[3].Value = t.ParaValue;
            //parameters[3].Value = t.Site;
            //parameters[4].Value = DateTime.Now;
            //parameters[5].Value = t.Id;
            //ZhiFang.Common.Log.Log.Debug("B_Parameter.update sql = " + strSql.ToString());
            //ZhiFang.Common.Log.Log.Debug("Id=" + t.Id + " Name=" +t.Name+" SName="+t.SName+" ParaNo="+t.ParaNo+" ParaType="+t.ParaType+" ParaValue="+t.ParaValue+" Site="+t.Site);
            //return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            #endregion
            ZhiFang.Common.Log.Log.Debug("BParameter.update sql;"+sq.ToString());
            return DbHelperSQL.ExecuteSql(sq.ToString());
            
        }

        public int deleteBySName(string appType)
        {
            string sql = "delete from B_Parameter where sname='" + appType + "'";
            ZhiFang.Common.Log.Log.Debug("B_Parameter.deleteBySName sql = " + sql);
            return DbHelperSQL.ExecuteSql(sql);
        }

        public DataSet GetSeniorPublicSetting(string SName, string ParaNo)
        {
            string sql = "select * from B_Parameter where SName = " + SName + " and ParaNo =" + ParaNo;
            ZhiFang.Common.Log.Log.Debug("B_Parameter.deleteBySName sql = " + sql);
            return DbHelperSQL.Query(sql);
        }
    }
}

