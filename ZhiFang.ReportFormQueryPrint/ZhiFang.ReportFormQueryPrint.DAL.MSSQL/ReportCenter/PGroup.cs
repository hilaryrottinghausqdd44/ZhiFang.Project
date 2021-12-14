using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Collections.Generic;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter
{
    /// <summary>
    /// 数据访问类:SectionPrint
    /// </summary>
    public class PGroup : IDPGroup
    {
        public int Add(Model.PGroup t)
        {
            throw new NotImplementedException();
        }

        public int Delete(int SectionNo, int Visible)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int SectionNo, int Visible)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.PGroup t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();

            if (strWhere != null && strWhere.Trim() != "")
            {
                strWhere = " where 1=1 and " + strWhere;
            }
            strSql.Append("SELECT * FROM PGroup " + strWhere);
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

        public Model.PGroup GetModel(int SectionNo, int Visible)
        {
            throw new NotImplementedException();
        }

        public Model.PGroup GetModel(string ClientNo, int SectionNo, int Visible)
        {
            throw new NotImplementedException();
        }

        public int Update(Model.PGroup t)
        {
            throw new NotImplementedException();
        }
    }
}

