using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.Formula.Functions;
using ZhiFang.DAO.SQL.ReagentSys.Client.Client;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.Base;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.DAO.SQL.ReagentSys.Client
{
    public class CenOrgDao_SQL : IDBaseDao<CenOrg, long>, IDCenOrgDao_SQL
    {
        //查询字段
        private string FieldStr = "OrgID,OrgNo,OrgTypeID,POrgID,POrgNo,CName,EName,ServerIP,ServerPort,ShortCode,Address,Contact,Tel,Fox,Email,WebAddress,Memo,DispOrder,Visible,ZX1,ZX2,ZX3,DataUpdateTime,Tel1,HotTel,HotTel1,DataAddTime,DataTimeStamp,LabID";
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM CenOrg ");
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
            strSql.Append("select count(1) FROM CenOrg ");
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
            strSql.Append(" FROM CenOrg ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.QuerySql(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CenOrg GetModel(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(this.FieldStr);
            strSql.Append(" from CenOrg ");
            strSql.Append(" where OrgID=" + id + " ");
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
        public CenOrg DataRowToModel(DataRow row)
        {
            CenOrg model = new CenOrg();
            if (row != null)
            {
                if (row["OrgID"] != null && row["OrgID"].ToString() != "")
                {
                    model.Id = long.Parse(row["OrgID"].ToString());
                }
                if (row["OrgNo"] != null && row["OrgNo"].ToString() != "")
                {
                    model.OrgNo = int.Parse(row["OrgNo"].ToString());
                }
                if (row["OrgTypeID"] != null && row["OrgTypeID"].ToString() != "")
                {
                    model.OrgTypeID = long.Parse(row["OrgTypeID"].ToString());
                }
                if (row["POrgID"] != null && row["POrgID"].ToString() != "")
                {
                    model.POrgID = long.Parse(row["POrgID"].ToString());
                }
                if (row["POrgNo"] != null && row["POrgNo"].ToString() != "")
                {
                    model.POrgNo = int.Parse(row["POrgNo"].ToString());
                }
                if (row["CName"] != null)
                {
                    model.CName = row["CName"].ToString();
                }
                if (row["EName"] != null)
                {
                    model.EName = row["EName"].ToString();
                }
                if (row["ServerIP"] != null)
                {
                    model.ServerIP = row["ServerIP"].ToString();
                }
                if (row["ServerPort"] != null)
                {
                    model.ServerPort = row["ServerPort"].ToString();
                }
                if (row["ShortCode"] != null)
                {
                    model.ShortCode = row["ShortCode"].ToString();
                }
                if (row["Address"] != null)
                {
                    model.Address = row["Address"].ToString();
                }
                if (row["Contact"] != null)
                {
                    model.Contact = row["Contact"].ToString();
                }
                if (row["Tel"] != null)
                {
                    model.Tel = row["Tel"].ToString();
                }
                if (row["Fox"] != null)
                {
                    model.Fox = row["Fox"].ToString();
                }
                if (row["Email"] != null)
                {
                    model.Email = row["Email"].ToString();
                }
                if (row["WebAddress"] != null)
                {
                    model.WebAddress = row["WebAddress"].ToString();
                }
                if (row["Memo"] != null)
                {
                    model.Memo = row["Memo"].ToString();
                }
                if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(row["DispOrder"].ToString());
                }
                if (row["Visible"] != null && row["Visible"].ToString() != "")
                {
                    model.Visible = int.Parse(row["Visible"].ToString());
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
                if (row["Tel1"] != null)
                {
                    model.Tel1 = row["Tel1"].ToString();
                }
                if (row["HotTel"] != null)
                {
                    model.HotTel = row["HotTel"].ToString();
                }
                if (row["HotTel1"] != null)
                {
                    model.HotTel1 = row["HotTel1"].ToString();
                }
                if (row["DataAddTime"] != null && row["DataAddTime"].ToString() != "")
                {
                    model.DataAddTime = DateTime.Parse(row["DataAddTime"].ToString());
                }
                if (row["DataTimeStamp"] != null && row["DataTimeStamp"].ToString() != "")
                {
                    model.DataTimeStamp = row["DataTimeStamp"] as byte[];
                }
                if (row["LabID"] != null && row["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(row["LabID"].ToString());
                }
            }
            return model;
        }
        public IList<CenOrg> GetListByHQL(string strSqlWhere)
        {
            IList<CenOrg> tempList = new List<CenOrg>();
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
        public CenOrg Get(long id)
        {
            return GetModel(id);
        }
        public CenOrg ObtainById(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(this.FieldStr);
            strSql.Append(" from CenOrg ");
            strSql.Append(" where OrgID=" + id + " ");
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
                strSql.Append("order by T.OrgID desc");
            }
            strSql.Append(")AS Row, T.*  from CenOrg T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.QuerySql(strSql.ToString());
        }

        public EntityList<CenOrg> GetListByHQL(string strSqlWhere, int start, int count)
        {
            EntityList<CenOrg> entityList = new EntityList<CenOrg>();
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

        public EntityList<CenOrg> GetListByHQL(string strSqlWhere, string Order, int start, int count)
        {
            EntityList<CenOrg> entityList = new EntityList<CenOrg>();
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

        public IList<CenOrg> LoadAll()
        {
            IList<CenOrg> tempList = new List<CenOrg>();

            DataSet ds = DbHelperSQL.QuerySql("");
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

        public IList<CenOrg> GetObjects(CenOrg entity)
        {
            throw new NotImplementedException();
        }
        public bool BatchSaveVO(CenOrg voList)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(CenOrg entity)
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

        public void Evict(CenOrg entity)
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
        public bool Save(CenOrg entity)
        {
            throw new NotImplementedException();
        }

        public object SaveByEntity(CenOrg entity)
        {
            throw new NotImplementedException();
        }

        public bool SaveOrUpdate(CenOrg entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(string[] strParas)
        {
            throw new NotImplementedException();
        }

        public bool Update(CenOrg entity)
        {
            throw new NotImplementedException();
        }

        public int UpdateByHql(string hql)
        {
            throw new NotImplementedException();
        }
    }
}
