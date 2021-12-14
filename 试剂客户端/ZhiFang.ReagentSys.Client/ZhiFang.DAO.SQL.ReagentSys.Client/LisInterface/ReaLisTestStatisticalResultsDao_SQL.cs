using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZhiFang.DAO.SQL.ReagentSys.Client.Client;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;


namespace ZhiFang.DAO.SQL.ReagentSys.Client
{
    public class ReaLisTestStatisticalResultsDao_SQL : IDBaseDao<ReaLisTestStatisticalResults, long>, IDReaLisTestStatisticalResultsDao_SQL
    {
        DBUtility.SqlServerHelperP DbHelperSQL = new LisInterface.DbHelperSQLP(LisInterface.DbHelperSQLP.GetConnectionString());

        #region  公共生成
        //查询字段
        private string FieldStr = "LabID,Id,TestDate,TestEquipID,TestEquipCode,TestEquipName,TestEquipTypeCode,TestEquipTypeName,TestType,TestItemID,TestItemCode,TestItemCName,TestItemSName,TestItemEName,TestCount,Price,SumTotal,DataAddTime,DataTimeStamp";
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Rea_LisTestStatisticalResults ");
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
		/// 获取记录总数
		/// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Rea_LisTestStatisticalResults ");
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
        public int GetListCountByHQL(string strSqlWhere)
        {
            return GetRecordCount(strSqlWhere);
        }

        public object GetTotalByHQL(string strSqlWhere, string field)
        {
            return GetRecordCount(strSqlWhere);
        }

        /// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  ");
            strSql.Append(this.FieldStr);
            strSql.Append(" FROM Rea_LisTestStatisticalResults ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.QuerySql(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ReaLisTestStatisticalResults GetModel(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(this.FieldStr);
            strSql.Append(" from Rea_LisTestStatisticalResults ");
            strSql.Append(" where Id=" + id + " ");
            ReaLisTestStatisticalResults model = new ReaLisTestStatisticalResults();
            DataSet ds = DbHelperSQL.QuerySql(strSql.ToString());
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
        public ReaLisTestStatisticalResults DataRowToModel(DataRow row)
        {
            ReaLisTestStatisticalResults model = new ReaLisTestStatisticalResults();
            if (row != null)
            {
                if (row["LabID"] != null && row["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(row["LabID"].ToString());
                }
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = long.Parse(row["Id"].ToString());
                }
                if (row["TestDate"] != null && row["TestDate"].ToString() != "")
                {
                    model.TestDate = DateTime.Parse(row["TestDate"].ToString());
                }
                if (row["TestEquipID"] != null && row["TestEquipID"].ToString() != "")
                {
                    model.TestEquipID = long.Parse(row["TestEquipID"].ToString());
                }
                if (row["TestEquipCode"] != null)
                {
                    model.TestEquipCode = row["TestEquipCode"].ToString();
                }
                if (row["TestEquipName"] != null)
                {
                    model.TestEquipName = row["TestEquipName"].ToString();
                }
                if (row["TestEquipTypeCode"] != null)
                {
                    model.TestEquipTypeCode = row["TestEquipTypeCode"].ToString();
                }
                if (row["TestEquipTypeName"] != null)
                {
                    model.TestEquipTypeName = row["TestEquipTypeName"].ToString();
                }
                if (row["TestType"] != null)
                {
                    model.TestType = row["TestType"].ToString();
                }
                if (row["TestItemID"] != null && row["TestItemID"].ToString() != "")
                {
                    model.TestItemID = long.Parse(row["TestItemID"].ToString());
                }
                if (row["TestItemCode"] != null)
                {
                    model.TestItemCode = row["TestItemCode"].ToString();
                }
                if (row["TestItemCName"] != null)
                {
                    model.TestItemCName = row["TestItemCName"].ToString();
                }
                if (row["TestItemSName"] != null)
                {
                    model.TestItemSName = row["TestItemSName"].ToString();
                }
                if (row["TestItemEName"] != null)
                {
                    model.TestItemEName = row["TestItemEName"].ToString();
                }
                if (row["TestCount"] != null && row["TestCount"].ToString() != "")
                {
                    model.TestCount = int.Parse(row["TestCount"].ToString());
                }
                if (row["Price"] != null && row["Price"].ToString() != "")
                {
                    model.Price = double.Parse(row["Price"].ToString());
                }
                if (row["SumTotal"] != null && row["SumTotal"].ToString() != "")
                {
                    model.SumTotal = double.Parse(row["SumTotal"].ToString());
                }
                if (row["DataAddTime"] != null && row["DataAddTime"].ToString() != "")
                {
                    model.DataAddTime = DateTime.Parse(row["DataAddTime"].ToString());
                }
                if (row["DataTimeStamp"] != null && row["DataTimeStamp"].ToString() != "")
                {
                    model.DataTimeStamp = row["DataTimeStamp"] as System.Byte[];
                }
            }
            return model;
        }
        public IList<ReaLisTestStatisticalResults> GetListByHQL(string strSqlWhere)
        {
            IList<ReaLisTestStatisticalResults> tempList = new List<ReaLisTestStatisticalResults>();
            DataSet ds = GetList(strSqlWhere);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    tempList.Add(DataRowToModel(row));
                }
            }
            else
            {
                return tempList;
            }
            return tempList;
        }
        public ReaLisTestStatisticalResults Get(long id)
        {
            return GetModel(id);
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
                strSql.Append("order by T.TestItemID desc");
            }
            strSql.Append(")AS Row, T.*  from Rea_LisTestStatisticalResults T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.QuerySql(strSql.ToString());
        }

        public EntityList<ReaLisTestStatisticalResults> GetListByHQL(string strSqlWhere, int start, int count)
        {
            EntityList<ReaLisTestStatisticalResults> entityList = new EntityList<ReaLisTestStatisticalResults>();
            entityList.count = GetRecordCount(strSqlWhere);
            if (entityList.count <= 0) return entityList;

            DataSet ds = GetListByPage(strSqlWhere, "", start, count);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    entityList.list.Add(DataRowToModel(row));
                }
            }
            else
            {
                return entityList;
            }
            return entityList;
        }

        public EntityList<ReaLisTestStatisticalResults> GetListByHQL(string strSqlWhere, string Order, int start, int count)
        {
            EntityList<ReaLisTestStatisticalResults> entityList = new EntityList<ReaLisTestStatisticalResults>();
            entityList.count = GetRecordCount(strSqlWhere);
            if (entityList.count <= 0) return entityList;

            DataSet ds = GetListByPage(strSqlWhere, Order, start, count);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    entityList.list.Add(DataRowToModel(row));
                }
            }
            else
            {
                return entityList;
            }
            return entityList;
        }

        public IList<ReaLisTestStatisticalResults> LoadAll()
        {
            IList<ReaLisTestStatisticalResults> tempList = new List<ReaLisTestStatisticalResults>();

            DataSet ds = GetList("");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    tempList.Add(DataRowToModel(row));
                }
            }
            else
            {
                return tempList;
            }
            return tempList;
        }

        public IList<ReaLisTestStatisticalResults> GetObjects(ReaLisTestStatisticalResults entity)
        {
            throw new NotImplementedException();
        }
        public bool BatchSaveVO(ReaLisTestStatisticalResults voList)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(ReaLisTestStatisticalResults entity)
        {
            throw new NotImplementedException();
        }

        public int DeleteByHql(string hql)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByHQL(long id)
        {
            throw new NotImplementedException();
        }

        public void Evict(ReaLisTestStatisticalResults entity)
        {
            throw new NotImplementedException();
        }

        public IList<T> Find<T>(string hql)
        {
            throw new NotImplementedException();
        }
        public void Flush()
        {
            throw new NotImplementedException();
        }
        public bool Save(ReaLisTestStatisticalResults entity)
        {
            throw new NotImplementedException();
        }

        public object SaveByEntity(ReaLisTestStatisticalResults entity)
        {
            throw new NotImplementedException();
        }

        public bool SaveOrUpdate(ReaLisTestStatisticalResults entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(string[] strParas)
        {
            throw new NotImplementedException();
        }

        public bool Update(ReaLisTestStatisticalResults entity)
        {
            throw new NotImplementedException();
        }

        public int UpdateByHql(string hql)
        {
            throw new NotImplementedException();
        }
        #endregion
        
        #region 获取LIS统计结果
        /// <summary>
        /// 获取LIS检测结果
        /// </summary>
        /// <param name="testType">检测类型(Lis系统TestType的ID,多个时为:Common,Review)</param>
        /// <param name="beginDate">检测开始日期</param>
        /// <param name="endDate">检测结束日期</param>
        /// <param name="equipNo">检测仪器编码</param>
        /// <param name="where">获取(合并)检测结果后的过滤条件</param>
        /// <param name="order">获取(合并)检测结果后的排序</param>
        /// <returns></returns>
        public DataSet SelectLisTestStatisticalResultsList(string testType, string beginDate, string endDate, string equipNo, string where, string order)
        {
            string storedProcName = "P_LRMP_ReaLisTestStatisticalResults";
            IList<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter sqlPara1 = new SqlParameter("TestType", SqlDbType.Char);
            sqlPara1.Value = testType;
            parameters.Add(sqlPara1);

            SqlParameter sqlPara2 = new SqlParameter("BeginDate", SqlDbType.Char);
            sqlPara2.Value = beginDate;
            parameters.Add(sqlPara2);

            SqlParameter sqlPara3 = new SqlParameter("EndDate", SqlDbType.Char);
            sqlPara3.Value = endDate;
            parameters.Add(sqlPara3);

            SqlParameter sqlPara4 = new SqlParameter("EquipNo", SqlDbType.Char);
            sqlPara4.Value = equipNo;
            parameters.Add(sqlPara4);

            SqlParameter sqlPara5 = new SqlParameter("Where", SqlDbType.Char);
            sqlPara5.Value = where;
            parameters.Add(sqlPara5);

            SqlParameter sqlPara6 = new SqlParameter("Order", SqlDbType.Char);
            sqlPara6.Value = order;
            parameters.Add(sqlPara6);

            return DbHelperSQL.ExecuteProcedure(storedProcName, parameters.ToArray());
        }
        #endregion
    }
}
