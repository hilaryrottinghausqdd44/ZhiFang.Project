using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using ZhiFang.ReportFormQueryPrint.Model;

//Please add references
namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66
{
	/// <summary>
	/// 数据访问类:NRequestForm
	/// </summary>
	public partial class NRequestItem : IDNRequestItem
    {
        DbHelperSQLObj DbHelperSQL = new DbHelperSQLObj(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));
        public NRequestItem()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string SerialNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from NRequestItem");
			strSql.Append(" where SerialNo=@SerialNo ");
			SqlParameter[] parameters = {
					new SqlParameter("@SerialNo", SqlDbType.VarChar,40)};
			parameters[0].Value = SerialNo;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        public int Add(Model.NRequestItem t)
        {
            throw new NotImplementedException();
        }

        public int Update(Model.NRequestItem t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.NRequestItem t)
        {
            throw new NotImplementedException();
        }

        public int Delete(string SerialNo)
        {
            throw new NotImplementedException();
        }

        public Model.NRequestForm GetModel(string SerialNo)
        {
            throw new NotImplementedException();
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            
            strSql.Append("select *  FROM NRequestItem ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }

            ZhiFang.Common.Log.Log.Debug("6.6库.NRequestItem.GetList.strSql:" + strSql.ToString());
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public int GetCountFormFull(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as CountNo ");
            strSql.Append(" FROM NRequestItem ");
            if (where.Trim() != "")
            {
                strSql.Append(" where " + where);
            }
            ZhiFang.Common.Log.Log.Debug("6.6库.NRequestItem.GetList_FormFull.strSql:" + strSql.ToString());
            var counto = DbHelperSQL.GetSingle(strSql.ToString());
            int count = 0;
            if (counto != null)
            {
                count = int.Parse(counto.ToString());
            }
            return count;
        }

        public DataSet GetList_FormFull(string fields, string strWhere) {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(fields))
            {
                strSql.Append("select " + fields + " ");
            }
            else
            {
                strSql.Append("select * ");
            }
            strSql.Append(" FROM NRequestItem ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" group by serialno ");
            ZhiFang.Common.Log.Log.Debug("6.6库.NRequestItem.GetList_FormFull.strSql:" + strSql.ToString());
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion
    }
}

