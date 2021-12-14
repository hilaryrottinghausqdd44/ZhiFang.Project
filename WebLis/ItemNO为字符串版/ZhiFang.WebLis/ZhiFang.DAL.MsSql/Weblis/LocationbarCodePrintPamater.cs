using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Weblis
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
            strSql.Append(" where Id= " + Id.ToString());
            //SqlParameter[] parameters = {
            //        new SqlParameter("@GUID", SqlDbType.UniqueIdentifier,16)			};
            //parameters[0].Value = GUID;

            return DbHelperSQL.Exists(strSql.ToString());
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.LocationbarCodePrintPamater model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into LocationbarCodePrintPamater(");
            strSql.Append("Id,AccountId,ParaMeter,CreateDateTime,UpdateDateTime)");//TimeStamp
            strSql.Append(" values (");
            strSql.Append("@Id,@AccountId,@ParaMeter,@CreateDateTime,@UpdateDateTime)");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt,16),
					new SqlParameter("@AccountId", SqlDbType.VarChar,30),
					new SqlParameter("@ParaMeter", SqlDbType.NText),
					new SqlParameter("@CreateDateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateDateTime", SqlDbType.DateTime)
                    //new SqlParameter("@TimeStamp", SqlDbType.Timestamp,8)}
                                        };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.AccountId;
            parameters[2].Value = model.ParaMeter;
            parameters[3].Value = model.CreateDateTime;
            parameters[4].Value = model.UpdateDateTime;
           // parameters[5].Value = model.TimeStamp;

            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
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
            strSql.Append("AccountId=@AccountId,");
            strSql.Append("ParaMeter=@ParaMeter,");
            strSql.Append("CreateDateTime=@CreateDateTime,");
            strSql.Append("UpdateDateTime=@UpdateDateTime");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@AccountId", SqlDbType.VarChar,30),
					new SqlParameter("@ParaMeter", SqlDbType.NText),
					new SqlParameter("@CreateDateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateDateTime", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.BigInt,16)};
            parameters[0].Value = model.AccountId;
            parameters[1].Value = model.ParaMeter;
            parameters[2].Value = model.CreateDateTime;
            parameters[3].Value = model.UpdateDateTime;
            parameters[4].Value = model.Id;

            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
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
        /// 删除一条数据
        /// </summary>
        public bool Delete(long Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from LocationbarCodePrintPamater ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt,16)			};
            parameters[0].Value = Id;

            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Delete(string AccountId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from LocationbarCodePrintPamater ");
            strSql.Append(" where AccountId=@AccountId ");
            SqlParameter[] parameters = {
					new SqlParameter("@AccountId", SqlDbType.VarChar,20)			};
            parameters[0].Value = AccountId;
            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from LocationbarCodePrintPamater ");
            strSql.Append(" where Id in (" + IDlist + ")  ");
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
            strSql.Append("select  top 1 Id,AccountId,ParaMeter,CreateDateTime,UpdateDateTime,TimeStamp from LocationbarCodePrintPamater ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt,16)			};
            parameters[0].Value = Id;

            Model.LocationbarCodePrintPamater model = new Model.LocationbarCodePrintPamater();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);
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
                //if (row["TimeStamp"] != null && row["TimeStamp"].ToString() != "")
                //{
                //    model.TimeStamp = DateTime.Parse(row["TimeStamp"].ToString());
                //}
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

        public Model.LocationbarCodePrintPamater GetModel(string AccountId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,AccountId,ParaMeter,CreateDateTime,UpdateDateTime,TimeStamp from LocationbarCodePrintPamater ");
            strSql.Append(" where AccountId=@AccountId ");
            SqlParameter[] parameters = {
					new SqlParameter("@AccountId", SqlDbType.VarChar,20)			};
            parameters[0].Value = AccountId;

            Model.LocationbarCodePrintPamater model = new Model.LocationbarCodePrintPamater();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

    }
}
