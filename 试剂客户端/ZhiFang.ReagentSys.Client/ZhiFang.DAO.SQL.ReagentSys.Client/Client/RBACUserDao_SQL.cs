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
    public class RBACUserDao_SQL : IDBaseDao<RBACUser, long>, IDRBACUserDao_SQL
    {
        //查询字段
        private string FieldStr = "LabID,UserID,EmpID,UseCode,Account,PWD,EnMPwd,PwdExprd,AccExprd,AccLock,AuUnlock,AccLockDt,LoginTime,AccBeginTime,AccEndTime,StandCode,CName,EName,SName,Shortcode,PinYinZiTou,Comment,IsUse,DispOrder,DataAddTime,DataUpdateTime,DataTimeStamp";
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM RBAC_User ");
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
            strSql.Append("select count(1) FROM RBAC_User ");
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
            strSql.Append(" FROM RBAC_User ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.QuerySql(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RBACUser GetModel(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(this.FieldStr);
            strSql.Append(" from RBAC_User ");
            strSql.Append(" where UserID=" + id + " ");
            RBACUser model = new RBACUser();
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
        public RBACUser DataRowToModel(DataRow row)
        {
            RBACUser model = new RBACUser();
            if (row != null)
            {
                if (row["LabID"] != null && row["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(row["LabID"].ToString());
                }
                if (row["UserID"] != null && row["UserID"].ToString() != "")
                {
                    model.Id = long.Parse(row["UserID"].ToString());
                }
                if (row["EmpID"] != null && row["EmpID"].ToString() != "")
                {
                    model.HREmployee = DataAccess_SQL.CreateHREmployeeDao_SQL().Get(long.Parse(row["EmpID"].ToString()));
                }
                if (row["UseCode"] != null)
                {
                    model.UseCode = row["UseCode"].ToString();
                }
                if (row["Account"] != null)
                {
                    model.Account = row["Account"].ToString();
                }
                if (row["PWD"] != null)
                {
                    model.PWD = row["PWD"].ToString();
                }
                if (row["EnMPwd"] != null && row["EnMPwd"].ToString() != "")
                {
                    if ((row["EnMPwd"].ToString() == "1") || (row["EnMPwd"].ToString().ToLower() == "true"))
                    {
                        model.EnMPwd = true;
                    }
                    else
                    {
                        model.EnMPwd = false;
                    }
                }
                if (row["PwdExprd"] != null && row["PwdExprd"].ToString() != "")
                {
                    if ((row["PwdExprd"].ToString() == "1") || (row["PwdExprd"].ToString().ToLower() == "true"))
                    {
                        model.PwdExprd = true;
                    }
                    else
                    {
                        model.PwdExprd = false;
                    }
                }
                if (row["AccExprd"] != null && row["AccExprd"].ToString() != "")
                {
                    if ((row["AccExprd"].ToString() == "1") || (row["AccExprd"].ToString().ToLower() == "true"))
                    {
                        model.AccExprd = true;
                    }
                    else
                    {
                        model.AccExprd = false;
                    }
                }
                if (row["AccLock"] != null && row["AccLock"].ToString() != "")
                {
                    if ((row["AccLock"].ToString() == "1") || (row["AccLock"].ToString().ToLower() == "true"))
                    {
                        model.AccLock = true;
                    }
                    else
                    {
                        model.AccLock = false;
                    }
                }
                if (row["AuUnlock"] != null && row["AuUnlock"].ToString() != "")
                {
                    model.AuUnlock = int.Parse(row["AuUnlock"].ToString());
                }
                if (row["AccLockDt"] != null && row["AccLockDt"].ToString() != "")
                {
                    model.AccLockDt = DateTime.Parse(row["AccLockDt"].ToString());
                }
                if (row["LoginTime"] != null && row["LoginTime"].ToString() != "")
                {
                    model.LoginTime = DateTime.Parse(row["LoginTime"].ToString());
                }
                if (row["AccBeginTime"] != null && row["AccBeginTime"].ToString() != "")
                {
                    model.AccBeginTime = DateTime.Parse(row["AccBeginTime"].ToString());
                }
                if (row["AccEndTime"] != null && row["AccEndTime"].ToString() != "")
                {
                    model.AccEndTime = DateTime.Parse(row["AccEndTime"].ToString());
                }
                if (row["StandCode"] != null)
                {
                    model.StandCode = row["StandCode"].ToString();
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
        public IList<RBACUser> GetListByHQL(string strSqlWhere)
        {
            IList<RBACUser> tempList = new List<RBACUser>();
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
        public RBACUser Get(long id)
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
                strSql.Append("order by T.UserID desc");
            }
            strSql.Append(")AS Row, T.*  from RBAC_User T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.QuerySql(strSql.ToString());
        }

        public EntityList<RBACUser> GetListByHQL(string strSqlWhere, int start, int count)
        {
            EntityList<RBACUser> entityList = new EntityList<RBACUser>();
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

        public EntityList<RBACUser> GetListByHQL(string strSqlWhere, string Order, int start, int count)
        {
            EntityList<RBACUser> entityList = new EntityList<RBACUser>();
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

        public IList<RBACUser> LoadAll()
        {
            IList<RBACUser> tempList = new List<RBACUser>();

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

        public IList<RBACUser> GetObjects(RBACUser entity)
        {
            throw new NotImplementedException();
        }
        public bool BatchSaveVO(RBACUser voList)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(RBACUser entity)
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

        public void Evict(RBACUser entity)
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
        public bool Save(RBACUser entity)
        {
            throw new NotImplementedException();
        }

        public object SaveByEntity(RBACUser entity)
        {
            throw new NotImplementedException();
        }

        public bool SaveOrUpdate(RBACUser entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(string[] strParas)
        {
            throw new NotImplementedException();
        }

        public bool Update(RBACUser entity)
        {
            throw new NotImplementedException();
        }

        public int UpdateByHql(string hql)
        {
            throw new NotImplementedException();
        }
    }
}
