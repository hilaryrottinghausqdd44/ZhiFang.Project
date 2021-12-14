using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.DAO.SQL.ReagentSys.Client.Client;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;


namespace ZhiFang.DAO.SQL.ReagentSys.Client
{
    public class ReaTestItemDao_SQL : IDBaseDao<ReaTestItem, long>, IDReaTestItemDao_SQL
    {
        DBUtility.SqlServerHelperP DbHelperSQL = new LisInterface.DbHelperSQLP(LisInterface.DbHelperSQLP.GetConnectionString());
        //查询字段
        private string FieldStr = "LabID,TestItemID,CName,EName,Price,ShortCode,DispOrder,Visible,LisCode,ZX1,ZX2,ZX3,DataUpdateTime,Memo,DataAddTime,DataTimeStamp,SName";
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Rea_TestItem ");
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
            strSql.Append("select count(1) FROM Rea_TestItem ");
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
            strSql.Append(" FROM Rea_TestItem ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.QuerySql(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ReaTestItem GetModel(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(this.FieldStr);
            strSql.Append(" from Rea_TestItem ");
            strSql.Append(" where TestItemID=" + id + " ");
            ReaTestItem model = new ReaTestItem();
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
        public ReaTestItem DataRowToModel(DataRow row)
        {
            ReaTestItem model = new ReaTestItem();
            if (row != null)
            {
                if (row["LabID"] != null && row["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(row["LabID"].ToString());
                }
                if (row["TestItemID"] != null && row["TestItemID"].ToString() != "")
                {
                    model.Id = long.Parse(row["TestItemID"].ToString());
                }
                if (row["CName"] != null)
                {
                    model.CName = row["CName"].ToString();
                }
                if (row["EName"] != null)
                {
                    model.EName = row["EName"].ToString();
                }
                if (row["Price"] != null && row["Price"].ToString() != "")
                {
                    model.Price = double.Parse(row["Price"].ToString());
                }
                if (row["ShortCode"] != null)
                {
                    model.ShortCode = row["ShortCode"].ToString();
                }
                if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(row["DispOrder"].ToString());
                }
                if (row["Visible"] != null && row["Visible"].ToString() != "")
                {
                    model.Visible = int.Parse(row["Visible"].ToString());
                }
                if (row["LisCode"] != null)
                {
                    model.LisCode = row["LisCode"].ToString();
                }
                if (row["ZX1"] != null)
                {
                    model.ZX1 = row["ZX1"].ToString();
                }
                if (row["ZX2"] != null)
                {
                    model.ZX2 = row["ZX2"].ToString();
                }
                if (row["ZX3"] != null)
                {
                    model.ZX3 = row["ZX3"].ToString();
                }
                if (row["DataUpdateTime"] != null && row["DataUpdateTime"].ToString() != "")
                {
                    model.DataUpdateTime = DateTime.Parse(row["DataUpdateTime"].ToString());
                }
                if (row["Memo"] != null)
                {
                    model.Memo = row["Memo"].ToString();
                }
                if (row["DataAddTime"] != null && row["DataAddTime"].ToString() != "")
                {
                    model.DataAddTime = DateTime.Parse(row["DataAddTime"].ToString());
                }
                if (row["DataTimeStamp"] != null && row["DataTimeStamp"].ToString() != "")
                {
                    model.DataTimeStamp = row["DataTimeStamp"] as System.Byte[];
                }
                if (row["SName"] != null)
                {
                    model.SName = row["SName"].ToString();
                }
            }
            return model;
        }
        public IList<ReaTestItem> GetListByHQL(string strSqlWhere)
        {
            IList<ReaTestItem> tempList = new List<ReaTestItem>();
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
        public ReaTestItem Get(long id)
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
            strSql.Append(")AS Row, T.*  from Rea_TestItem T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.QuerySql(strSql.ToString());
        }

        public EntityList<ReaTestItem> GetListByHQL(string strSqlWhere, int start, int count)
        {
            EntityList<ReaTestItem> entityList = new EntityList<ReaTestItem>();
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

        public EntityList<ReaTestItem> GetListByHQL(string strSqlWhere, string Order, int start, int count)
        {
            EntityList<ReaTestItem> entityList = new EntityList<ReaTestItem>();
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

        public IList<ReaTestItem> LoadAll()
        {
            IList<ReaTestItem> tempList = new List<ReaTestItem>();

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

        public IList<ReaTestItem> GetObjects(ReaTestItem entity)
        {
            throw new NotImplementedException();
        }
        public bool BatchSaveVO(ReaTestItem voList)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(ReaTestItem entity)
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

        public void Evict(ReaTestItem entity)
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
        public bool Save(ReaTestItem entity)
        {
            throw new NotImplementedException();
        }

        public object SaveByEntity(ReaTestItem entity)
        {
            throw new NotImplementedException();
        }

        public bool SaveOrUpdate(ReaTestItem entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(string[] strParas)
        {
            throw new NotImplementedException();
        }

        public bool Update(ReaTestItem entity)
        {
            throw new NotImplementedException();
        }

        public int UpdateByHql(string hql)
        {
            throw new NotImplementedException();
        }
    }
}
