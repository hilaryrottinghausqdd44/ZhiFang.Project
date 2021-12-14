using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data.SqlClient;
using System.Data;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66
{
    /// <summary>
	/// 数据访问类SampleType。
	/// </summary>
    public class WardType : IDWardType
    {
        public WardType()
        { }
        #region  成员方法
        public int Add(Model.WardType t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.WardType t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select WardNo,CName ");
            strSql.Append(" from wardType where 1=1");
            if (strWhere != null && strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public int Update(Model.WardType t)
        {
            throw new NotImplementedException();
        }
       




        #endregion  成员方法
    }
}
