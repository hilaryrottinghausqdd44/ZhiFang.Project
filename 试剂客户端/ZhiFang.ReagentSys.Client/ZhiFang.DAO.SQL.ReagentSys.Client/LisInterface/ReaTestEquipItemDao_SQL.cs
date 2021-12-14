using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ZhiFang.DAO.SQL.ReagentSys.Client.LisInterface;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.SQL.ReagentSys.Client
{
    public class ReaTestEquipItemDao_SQL : IDBaseDao<ReaTestEquipItem, long>, IDReaTestEquipItemDao_SQL
    {
        DBUtility.SqlServerHelperP DbHelperSQL = new LisInterface.DbHelperSQLP(LisInterface.DbHelperSQLP.GetConnectionString());
        //查询字段
        private string FieldStr = "TestEquipItemID,TestEquipID,TestItemID,DataAddTime,DataTimeStamp";
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Rea_TestEquipItem ");
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
            strSql.Append("select count(1) FROM Rea_TestEquipItem ");
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
            strSql.Append(" FROM Rea_TestEquipItem ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.QuerySql(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ReaTestEquipItem GetModel(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(this.FieldStr);
            strSql.Append(" from Rea_TestEquipItem ");
            strSql.Append(" where TestEquipID=" + id + " ");
            ReaTestEquipItem model = new ReaTestEquipItem();
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
        public ReaTestEquipItem DataRowToModel(DataRow row)
        {
            ReaTestEquipItem model = new ReaTestEquipItem();
            if (row != null)
            {
                //if (row["TestEquipItemID"] != null && row["TestEquipItemID"].ToString() != "")
                //{
                //    model.Id = long.Parse(row["TestEquipItemID"].ToString());
                //}
                if (row["TestEquipID"] != null && row["TestEquipID"].ToString() != "")
                {
                    model.TestEquipID = long.Parse(row["TestEquipID"].ToString());
                }
                if (row["TestItemID"] != null && row["TestItemID"].ToString() != "")
                {
                    model.TestItemID = long.Parse(row["TestItemID"].ToString());
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
        public IList<ReaTestEquipItem> GetListByHQL(string strSqlWhere)
        {
            IList<ReaTestEquipItem> tempList = new List<ReaTestEquipItem>();
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
        public ReaTestEquipItem Get(long id)
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
                strSql.Append("order by T.TestEquipID desc");
            }
            strSql.Append(")AS Row, T.*  from Rea_TestEquipItem T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.QuerySql(strSql.ToString());
        }

        public EntityList<ReaTestEquipItem> GetListByHQL(string strSqlWhere, int start, int count)
        {
            EntityList<ReaTestEquipItem> entityList = new EntityList<ReaTestEquipItem>();
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

        public EntityList<ReaTestEquipItem> GetListByHQL(string strSqlWhere, string Order, int start, int count)
        {
            EntityList<ReaTestEquipItem> entityList = new EntityList<ReaTestEquipItem>();
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

        public IList<ReaTestEquipItem> LoadAll()
        {
            IList<ReaTestEquipItem> tempList = new List<ReaTestEquipItem>();

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

        public IList<ReaTestEquipItem> GetObjects(ReaTestEquipItem entity)
        {
            throw new NotImplementedException();
        }
        public bool BatchSaveVO(ReaTestEquipItem voList)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(ReaTestEquipItem entity)
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

        public void Evict(ReaTestEquipItem entity)
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
        public bool Save(ReaTestEquipItem entity)
        {
            throw new NotImplementedException();
        }

        public object SaveByEntity(ReaTestEquipItem entity)
        {
            throw new NotImplementedException();
        }

        public bool SaveOrUpdate(ReaTestEquipItem entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(string[] strParas)
        {
            throw new NotImplementedException();
        }

        public bool Update(ReaTestEquipItem entity)
        {
            throw new NotImplementedException();
        }

        public int UpdateByHql(string hql)
        {
            throw new NotImplementedException();
        }
    }
}
