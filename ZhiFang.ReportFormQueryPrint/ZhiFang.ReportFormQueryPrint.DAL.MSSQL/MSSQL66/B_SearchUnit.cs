using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66
{
    /// <summary>
    /// 数据访问类Doctor。
    /// </summary>
    public class BSearchUnit : IDBSearchUnit
    {
        public int Add(Model.BSearchUnit t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.BSearchUnit t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(string strWhere)
        {
            DataSet ds = new DataSet();
            if (strWhere == null || strWhere.Length < 1)
            {
                ZhiFang.Common.Log.Log.Debug("ReportPrint_GetList:条件不符合规范 strWhere=" + strWhere);
                return ds;
            }

            string sql = "select * from B_SearchUnit where " + strWhere;
            ZhiFang.Common.Log.Log.Debug("ReportPrint_GetList:sql=" + sql);
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

        public int Update(Model.BSearchUnit t)
        {
            throw new NotImplementedException();
        }
    }
}

