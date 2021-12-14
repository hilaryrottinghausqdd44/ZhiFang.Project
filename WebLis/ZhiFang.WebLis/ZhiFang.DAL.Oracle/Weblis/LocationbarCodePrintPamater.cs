using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ZhiFang.IDAL;

namespace ZhiFang.DAL.Oracle.weblis
{
    public class LocationbarCodePrintPamater : BaseDALLisDB, IDLocationbarCodePrintPamater
    {
        public LocationbarCodePrintPamater(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public LocationbarCodePrintPamater()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from LocationbarCodePrintPamater");
            strSql.Append(" where Id=" + Id + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.LocationbarCodePrintPamater model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Id != null)
            {
                strSql1.Append("Id,");
                strSql2.Append("" + model.Id + ",");
            }
            if (model.AccountId != null)
            {
                strSql1.Append("AccountId,");
                strSql2.Append("'" + model.AccountId + "',");
            }
            if (model.ParaMeter != null)
            {
                strSql1.Append("ParaMeter,");
                strSql2.Append("'" + model.ParaMeter + "',");
            }
            if (model.CreateDateTime != null)
            {
                strSql1.Append("CreateDateTime,");
                strSql2.Append("'" + model.CreateDateTime + "',");
            }
            if (model.UpdateDateTime != null)
            {
                strSql1.Append("UpdateDateTime,");
                strSql2.Append("'" + model.UpdateDateTime + "',");
            }
            if (model.TimeStamp != null)
            {
                strSql1.Append("TimeStamp,");
                strSql2.Append("" + model.TimeStamp + ",");
            }
            strSql.Append("insert into LocationbarCodePrintPamater(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.LocationbarCodePrintPamater model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update LocationbarCodePrintPamater set ");
            if (model.AccountId != null)
            {
                strSql.Append("AccountId='" + model.AccountId + "',");
            }
            if (model.ParaMeter != null)
            {
                strSql.Append("ParaMeter='" + model.ParaMeter + "',");
            }
            else
            {
                strSql.Append("ParaMeter= null ,");
            }
            if (model.CreateDateTime != null)
            {
                strSql.Append("CreateDateTime='" + model.CreateDateTime + "',");
            }
            else
            {
                strSql.Append("CreateDateTime= null ,");
            }
            if (model.UpdateDateTime != null)
            {
                strSql.Append("UpdateDateTime='" + model.UpdateDateTime + "',");
            }
            else
            {
                strSql.Append("UpdateDateTime= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + " ");
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from LocationbarCodePrintPamater ");
            strSql.Append(" where Id=" + Id + " ");
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }		/// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from LocationbarCodePrintPamater ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.LocationbarCodePrintPamater GetModel(long Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select    ");
            strSql.Append(" Id,AccountId,ParaMeter,CreateDateTime,UpdateDateTime,TimeStamp ");
            strSql.Append(" from LocationbarCodePrintPamater ");
            strSql.Append(" where ROWNUM =1 and Id=" + Id + " ");
            Model.LocationbarCodePrintPamater model = new Model.LocationbarCodePrintPamater();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.LocationbarCodePrintPamater DataRowToModel(DataRow row)
        {
            Model.LocationbarCodePrintPamater model = new Model.LocationbarCodePrintPamater();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = long.Parse(row["Id"].ToString());
                }
                if (row["AccountId"] != null)
                {
                    model.AccountId = row["AccountId"].ToString();
                }
                if (row["ParaMeter"] != null)
                {
                    model.ParaMeter = row["ParaMeter"].ToString();
                }
                if (row["CreateDateTime"] != null && row["CreateDateTime"].ToString() != "")
                {
                    model.CreateDateTime = DateTime.Parse(row["CreateDateTime"].ToString());
                }
                if (row["UpdateDateTime"] != null && row["UpdateDateTime"].ToString() != "")
                {
                    model.UpdateDateTime = DateTime.Parse(row["UpdateDateTime"].ToString());
                }
                if (row["TimeStamp"] != null && row["TimeStamp"].ToString() != "")
                {
                    model.TimeStamp = DateTime.Parse(row["TimeStamp"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,AccountId,ParaMeter,CreateDateTime,UpdateDateTime,TimeStamp ");
            strSql.Append(" FROM LocationbarCodePrintPamater ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id,AccountId,ParaMeter,CreateDateTime,UpdateDateTime,TimeStamp ");
            strSql.Append(" FROM LocationbarCodePrintPamater ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM LocationbarCodePrintPamater ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from LocationbarCodePrintPamater T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }


        public bool Delete(string AccountId)
        {
            throw new NotImplementedException();
        }

        public Model.LocationbarCodePrintPamater GetModel(string AccountId)
        {
            throw new NotImplementedException();
        }
    }
}
