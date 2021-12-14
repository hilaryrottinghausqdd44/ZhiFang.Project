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
    public class RBACModuleDao_SQL : IDBaseDao<RBACModule, long>, IDRBACModuleDao_SQL
    {
        //查询字段
        private string FieldStr = "LabID,ModuleID,AppComID,ParentID,LevelNum,TreeCatalog,IsLeaf,ModuleType,PicFile,URL,Para,Owner,UseCode,StandCode,DeveCode,CName,EName,SName,Shortcode,PinYinZiTou,Comment,IsUse,DispOrder,DataAddTime,DataUpdateTime,DataTimeStamp";
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM RBAC_Module ");
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
            strSql.Append("select count(1) FROM RBAC_Module ");
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
            strSql.Append(" FROM RBAC_Module ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.QuerySql(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RBACModule GetModel(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(this.FieldStr);
            strSql.Append(" from RBAC_Module ");
            strSql.Append(" where ModuleID=" + id + " ");
            RBACModule model = new RBACModule();
            DataSet ds = DbHelperSQL.QuerySql(strSql.ToString());
            if (ds.Tables.Count > 0  && ds.Tables[0].Rows.Count > 0)
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
        public RBACModule DataRowToModel(DataRow row)
        {
            RBACModule model = new RBACModule();
            if (row != null)
            {
                if (row["LabID"] != null && row["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(row["LabID"].ToString());
                }
                if (row["ModuleID"] != null && row["ModuleID"].ToString() != "")
                {
                    model.Id = long.Parse(row["ModuleID"].ToString());
                }
                //if (row["AppComID"] != null && row["AppComID"].ToString() != "")
                //{
                //    model.AppComID = long.Parse(row["AppComID"].ToString());
                //}
                if (row["ParentID"] != null && row["ParentID"].ToString() != "")
                {
                    model.ParentID = long.Parse(row["ParentID"].ToString());
                }
                if (row["LevelNum"] != null && row["LevelNum"].ToString() != "")
                {
                    model.LevelNum = int.Parse(row["LevelNum"].ToString());
                }
                if (row["TreeCatalog"] != null && row["TreeCatalog"].ToString() != "")
                {
                    model.TreeCatalog = int.Parse(row["TreeCatalog"].ToString());
                }
                if (row["IsLeaf"] != null && row["IsLeaf"].ToString() != "")
                {
                    if ((row["IsLeaf"].ToString() == "1") || (row["IsLeaf"].ToString().ToLower() == "true"))
                    {
                        model.IsLeaf = true;
                    }
                    else
                    {
                        model.IsLeaf = false;
                    }
                }
                if (row["ModuleType"] != null && row["ModuleType"].ToString() != "")
                {
                    model.ModuleType = int.Parse(row["ModuleType"].ToString());
                }
                if (row["PicFile"] != null)
                {
                    model.PicFile = row["PicFile"].ToString();
                }
                if (row["URL"] != null)
                {
                    model.Url = row["URL"].ToString();
                }
                if (row["Para"] != null)
                {
                    model.Para = row["Para"].ToString();
                }
                if (row["Owner"] != null)
                {
                    model.Owner = row["Owner"].ToString();
                }
                if (row["UseCode"] != null)
                {
                    model.UseCode = row["UseCode"].ToString();
                }
                if (row["StandCode"] != null)
                {
                    model.StandCode = row["StandCode"].ToString();
                }
                if (row["DeveCode"] != null)
                {
                    model.DeveCode = row["DeveCode"].ToString();
                }
                if (row["CName"] != null)
                {
                    model.CName = row["CName"].ToString();
                }
                if (row["EName"] != null)
                {
                    model.EName = row["EName"].ToString();
                }
                if (row["SName"] != null)
                {
                    model.SName = row["SName"].ToString();
                }
                if (row["Shortcode"] != null)
                {
                    model.Shortcode = row["Shortcode"].ToString();
                }
                if (row["PinYinZiTou"] != null)
                {
                    model.PinYinZiTou = row["PinYinZiTou"].ToString();
                }
                if (row["Comment"] != null)
                {
                    model.Comment = row["Comment"].ToString();
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
            }
            return model;
        }
        public IList<RBACModule> GetListByHQL(string strSqlWhere)
        {
            IList<RBACModule> tempList = new List<RBACModule>();
            DataSet ds = GetList(strSqlWhere);
            if (ds.Tables.Count > 0  && ds.Tables[0].Rows.Count > 0)
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
        public RBACModule Get(long id)
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
                strSql.Append("order by T.DeptID desc");
            }
            strSql.Append(")AS Row, T.*  from RBAC_Module T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.QuerySql(strSql.ToString());
        }

        public EntityList<RBACModule> GetListByHQL(string strSqlWhere, int start, int count)
        {
            EntityList<RBACModule> entityList = new EntityList<RBACModule>();
            entityList.count = GetRecordCount(strSqlWhere);
            if (entityList.count <= 0) return entityList;

            DataSet ds = GetListByPage(strSqlWhere, "", start, count);
            if (ds.Tables.Count > 0  && ds.Tables[0].Rows.Count > 0)
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

        public EntityList<RBACModule> GetListByHQL(string strSqlWhere, string Order, int start, int count)
        {
            EntityList<RBACModule> entityList = new EntityList<RBACModule>();
            entityList.count = GetRecordCount(strSqlWhere);
            if (entityList.count <= 0) return entityList;

            DataSet ds = GetListByPage(strSqlWhere, Order, start, count);
            if (ds.Tables.Count > 0  && ds.Tables[0].Rows.Count > 0)
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

        public IList<RBACModule> LoadAll()
        {
            IList<RBACModule> tempList = new List<RBACModule>();

            DataSet ds = GetList("");
            if (ds.Tables.Count > 0  && ds.Tables[0].Rows.Count > 0)
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
        public IList<RBACModule> ObtainLoadAll()
        {
            IList<RBACModule> tempList = new List<RBACModule>();

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
        public IList<RBACModule> GetObjects(RBACModule entity)
        {
            throw new NotImplementedException();
        }
        public bool BatchSaveVO(RBACModule voList)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(RBACModule entity)
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

        public void Evict(RBACModule entity)
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
        public bool Save(RBACModule entity)
        {
            throw new NotImplementedException();
        }

        public object SaveByEntity(RBACModule entity)
        {
            throw new NotImplementedException();
        }

        public bool SaveOrUpdate(RBACModule entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(string[] strParas)
        {
            throw new NotImplementedException();
        }

        public bool Update(RBACModule entity)
        {
            throw new NotImplementedException();
        }

        public int UpdateByHql(string hql)
        {
            throw new NotImplementedException();
        }
    }
}
