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
    public class ReaTestEquipLabDao_SQL : IDBaseDao<ReaTestEquipLab, long>, IDReaTestEquipLabDao_SQL
    {
        DBUtility.SqlServerHelperP DbHelperSQL = new LisInterface.DbHelperSQLP(LisInterface.DbHelperSQLP.GetConnectionString());
        //查询字段
        private string FieldStr = "TestEquipID,TestProdEquipID,LabID,ProdOrgID,CompOrgID,TestEquipTypeID,CName,EName,ShortCode,Memo,Visible,DispOrder,LisCode,DataUpdateTime,DataAddTime,DataTimeStamp";
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Rea_TestEquipLab ");
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
            strSql.Append("select count(1) FROM Rea_TestEquipLab ");
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
            strSql.Append(" FROM Rea_TestEquipLab ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.QuerySql(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ReaTestEquipLab GetModel(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(this.FieldStr);
            strSql.Append(" from Rea_TestEquipLab ");
            strSql.Append(" where TestEquipID=" + id + " ");
            ReaTestEquipLab model = new ReaTestEquipLab();
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
        public ReaTestEquipLab DataRowToModel(DataRow row)
        {
            ReaTestEquipLab model = new ReaTestEquipLab();
            if (row != null)
            {
                if (row["TestEquipID"] != null && row["TestEquipID"].ToString() != "")
                {
                    model.Id = long.Parse(row["TestEquipID"].ToString());
                }
                if (row["TestProdEquipID"] != null && row["TestProdEquipID"].ToString() != "")
                {
                    model.TestProdEquipID = long.Parse(row["TestProdEquipID"].ToString());
                }
                if (row["LabID"] != null && row["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(row["LabID"].ToString());
                }
                if (row["ProdOrgID"] != null && row["ProdOrgID"].ToString() != "")
                {
                    model.ProdOrgID = long.Parse(row["ProdOrgID"].ToString());
                }
                if (row["CompOrgID"] != null && row["CompOrgID"].ToString() != "")
                {
                    model.CompOrgID = long.Parse(row["CompOrgID"].ToString());
                }
                if (row["TestEquipTypeID"] != null && row["TestEquipTypeID"].ToString() != "")
                {
                    model.TestEquipTypeID = long.Parse(row["TestEquipTypeID"].ToString());
                }
                if (row["CName"] != null)
                {
                    model.CName = row["CName"].ToString();
                }
                if (row["EName"] != null)
                {
                    model.EName = row["EName"].ToString();
                }
                if (row["ShortCode"] != null)
                {
                    model.ShortCode = row["ShortCode"].ToString();
                }
                if (row["Memo"] != null)
                {
                    model.Memo = row["Memo"].ToString();
                }
                if (row["Visible"] != null && row["Visible"].ToString() != "")
                {
                    model.Visible = int.Parse(row["Visible"].ToString());
                }
                if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(row["DispOrder"].ToString());
                }
                if (row["LisCode"] != null)
                {
                    model.LisCode = row["LisCode"].ToString();
                }
                if (row["DataUpdateTime"] != null && row["DataUpdateTime"].ToString() != "")
                {
                    model.DataUpdateTime = DateTime.Parse(row["DataUpdateTime"].ToString());
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
        public IList<ReaTestEquipLab> GetListByHQL(string strSqlWhere)
        {
            IList<ReaTestEquipLab> tempList = new List<ReaTestEquipLab>();
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
        public ReaTestEquipLab Get(long id)
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
            strSql.Append(")AS Row, T.*  from Rea_TestEquipLab T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.QuerySql(strSql.ToString());
        }

        public EntityList<ReaTestEquipLab> GetListByHQL(string strSqlWhere, int start, int count)
        {
            EntityList<ReaTestEquipLab> entityList = new EntityList<ReaTestEquipLab>();
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

        public EntityList<ReaTestEquipLab> GetListByHQL(string strSqlWhere, string Order, int start, int count)
        {
            EntityList<ReaTestEquipLab> entityList = new EntityList<ReaTestEquipLab>();
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

        public IList<ReaTestEquipLab> LoadAll()
        {
            IList<ReaTestEquipLab> tempList = new List<ReaTestEquipLab>();

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

        public IList<ReaTestEquipLab> GetObjects(ReaTestEquipLab entity)
        {
            throw new NotImplementedException();
        }
        public bool BatchSaveVO(ReaTestEquipLab voList)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(ReaTestEquipLab entity)
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

        public void Evict(ReaTestEquipLab entity)
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
        public bool Save(ReaTestEquipLab entity)
        {
            throw new NotImplementedException();
        }

        public object SaveByEntity(ReaTestEquipLab entity)
        {
            throw new NotImplementedException();
        }

        public bool SaveOrUpdate(ReaTestEquipLab entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(string[] strParas)
        {
            throw new NotImplementedException();
        }

        public bool Update(ReaTestEquipLab entity)
        {
            throw new NotImplementedException();
        }

        public int UpdateByHql(string hql)
        {
            throw new NotImplementedException();
        }
    }
}
