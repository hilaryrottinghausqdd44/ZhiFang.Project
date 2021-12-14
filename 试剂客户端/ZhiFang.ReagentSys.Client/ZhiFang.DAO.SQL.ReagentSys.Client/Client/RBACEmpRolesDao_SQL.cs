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
using ZhiFang.IDAO.Base;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.DAO.SQL.ReagentSys.Client
{
    public class RBACEmpRolesDao_SQL : IDBaseDao<RBACEmpRoles, long>, IDRBACEmpRolesDao_SQL
    {
        //查询字段
        private string FieldStr = "LabID,EmpRoleID,EmpID,RoleID,IsUse,DispOrder,DataAddTime,DataUpdateTime,DataTimeStamp,Validity";
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM RBAC_EmpRoles ");
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
            strSql.Append("select count(1) FROM RBAC_EmpRoles ");
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
            strSql.Append(" FROM RBAC_EmpRoles ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.QuerySql(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RBACEmpRoles GetModel(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(this.FieldStr);
            strSql.Append(" from RBAC_EmpRoles ");
            strSql.Append(" where EmpRoleID=" + id + " ");
            RBACEmpRoles model = new RBACEmpRoles();
            DataSet ds = DbHelperSQL.QuerySql(strSql.ToString());
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
        public RBACEmpRoles DataRowToModel(DataRow row)
        {
            RBACEmpRoles model = new RBACEmpRoles();
            if (row != null)
            {
                if (row["LabID"] != null && row["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(row["LabID"].ToString());
                }
                if (row["EmpRoleID"] != null && row["EmpRoleID"].ToString() != "")
                {
                    model.Id = long.Parse(row["EmpRoleID"].ToString());
                }
                if (row["EmpID"] != null && row["EmpID"].ToString() != "")
                {
                    model.HREmployee = DataAccess_SQL.CreateHREmployeeDao_SQL().Get(long.Parse(row["EmpID"].ToString())); ;
                }
                if (row["RoleID"] != null && row["RoleID"].ToString() != "")
                {
                    model.RBACRole = DataAccess_SQL.CreateRBACRoleDao_SQL().Get(long.Parse(row["RoleID"].ToString())); ;
                }
                if (row["IsUse"] != null && row["IsUse"].ToString() != "")
                {
                    if ((row["IsUse"].ToString() == "1") || (row["IsUse"].ToString().ToLower() == "true"))
                    {
                        model.IsUse = true;
                    }
                    else
                    {
                        model.IsUse = false;
                    }
                }
                if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(row["DispOrder"].ToString());
                }
                if (row["DataAddTime"] != null && row["DataAddTime"].ToString() != "")
                {
                    model.DataAddTime = DateTime.Parse(row["DataAddTime"].ToString());
                }
                if (row["DataUpdateTime"] != null && row["DataUpdateTime"].ToString() != "")
                {
                    model.DataUpdateTime = DateTime.Parse(row["DataUpdateTime"].ToString());
                }
                if (row["DataTimeStamp"] != null && row["DataTimeStamp"].ToString() != "")
                {
                    model.DataTimeStamp = row["DataTimeStamp"] as byte[];
                }
                if (row["Validity"] != null)
                {
                    model.Validity = row["Validity"].ToString();
                }
            }
            return model;
        }
        public IList<RBACEmpRoles> GetListByHQL(string strSqlWhere)
        {
            IList<RBACEmpRoles> tempList = new List<RBACEmpRoles>();
            DataSet ds = GetList(strSqlWhere);
            if (ds.Tables[0].Rows.Count > 0)
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
        public RBACEmpRoles Get(long id)
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
                strSql.Append("order by T.EmpRoleID desc");
            }
            strSql.Append(")AS Row, T.*  from RBAC_EmpRoles T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.QuerySql(strSql.ToString());
        }

        public EntityList<RBACEmpRoles> GetListByHQL(string strSqlWhere, int start, int count)
        {
            EntityList<RBACEmpRoles> entityList = new EntityList<RBACEmpRoles>();
            entityList.count = GetRecordCount(strSqlWhere);
            if (entityList.count <= 0) return entityList;

            DataSet ds = GetListByPage(strSqlWhere, "", start, count);
            if (ds.Tables[0].Rows.Count > 0)
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

        public EntityList<RBACEmpRoles> GetListByHQL(string strSqlWhere, string Order, int start, int count)
        {
            EntityList<RBACEmpRoles> entityList = new EntityList<RBACEmpRoles>();
            entityList.count = GetRecordCount(strSqlWhere);
            if (entityList.count <= 0) return entityList;

            DataSet ds = GetListByPage(strSqlWhere, Order, start, count);
            if (ds.Tables[0].Rows.Count > 0)
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

        public IList<RBACEmpRoles> LoadAll()
        {
            IList<RBACEmpRoles> tempList = new List<RBACEmpRoles>();

            DataSet ds = GetList("");
            if (ds.Tables[0].Rows.Count > 0)
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

        public IList<RBACEmpRoles> GetObjects(RBACEmpRoles entity)
        {
            throw new NotImplementedException();
        }
        public bool BatchSaveVO(RBACEmpRoles voList)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(RBACEmpRoles entity)
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

        public void Evict(RBACEmpRoles entity)
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
        public bool Save(RBACEmpRoles entity)
        {
            throw new NotImplementedException();
        }

        public object SaveByEntity(RBACEmpRoles entity)
        {
            throw new NotImplementedException();
        }

        public bool SaveOrUpdate(RBACEmpRoles entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(string[] strParas)
        {
            throw new NotImplementedException();
        }

        public bool Update(RBACEmpRoles entity)
        {
            throw new NotImplementedException();
        }

        public int UpdateByHql(string hql)
        {
            throw new NotImplementedException();
        }
    }
}
