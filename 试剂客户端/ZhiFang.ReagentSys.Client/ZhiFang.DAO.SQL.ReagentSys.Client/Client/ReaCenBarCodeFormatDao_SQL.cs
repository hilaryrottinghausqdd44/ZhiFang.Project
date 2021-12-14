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
    public class ReaCenBarCodeFormatDao_SQL : IDBaseDao<ReaCenBarCodeFormat, long>, IDReaCenBarCodeFormatDao_SQL
    {
        //查询字段
        private string FieldStr = "LabID,CBCFID,PlatformOrgNo,CName,BarCodeFormatExample,RegularExpression,SplitCount,SName,ShortCode,pinyinzitou,DispOrder,IsUse,Memo,DataAddTime,DataTimeStamp,Type";
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Rea_CenBarCodeFormat ");
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
            strSql.Append("select count(1) FROM Rea_CenBarCodeFormat ");
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
            strSql.Append(" FROM Rea_CenBarCodeFormat ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.QuerySql(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ReaCenBarCodeFormat GetModel(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(this.FieldStr);
            strSql.Append(" from Rea_CenBarCodeFormat ");
            strSql.Append(" where CBCFID=" + id + " ");
            ReaCenBarCodeFormat model = new ReaCenBarCodeFormat();
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
        public ReaCenBarCodeFormat DataRowToModel(DataRow row)
        {
            ReaCenBarCodeFormat model = new ReaCenBarCodeFormat();
            if (row != null)
            {
                if (row["LabID"] != null && row["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(row["LabID"].ToString());
                }
                if (row["CBCFID"] != null && row["CBCFID"].ToString() != "")
                {
                    model.Id = long.Parse(row["CBCFID"].ToString());
                }
                if (row["PlatformOrgNo"] != null && row["PlatformOrgNo"].ToString() != "")
                {
                    model.PlatformOrgNo = int.Parse(row["PlatformOrgNo"].ToString());
                }
                if (row["CName"] != null)
                {
                    model.CName = row["CName"].ToString();
                }
                if (row["BarCodeFormatExample"] != null)
                {
                    model.BarCodeFormatExample = row["BarCodeFormatExample"].ToString();
                }
                if (row["RegularExpression"] != null)
                {
                    model.RegularExpression = row["RegularExpression"].ToString();
                }
                if (row["SplitCount"] != null && row["SplitCount"].ToString() != "")
                {
                    model.SplitCount = int.Parse(row["SplitCount"].ToString());
                }
                if (row["SName"] != null)
                {
                    model.SName = row["SName"].ToString();
                }
                if (row["ShortCode"] != null)
                {
                    model.ShortCode = row["ShortCode"].ToString();
                }
                if (row["pinyinzitou"] != null)
                {
                    model.Pinyinzitou = row["pinyinzitou"].ToString();
                }
                if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(row["DispOrder"].ToString());
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
                    model.DataTimeStamp = row["DataTimeStamp"] as byte[];
                }
                if (row["Type"] != null && row["Type"].ToString() != "")
                {
                    model.Type = long.Parse(row["Type"].ToString());
                }
            }
            return model;
        }
        public IList<ReaCenBarCodeFormat> GetListByHQL(string strSqlWhere)
        {
            IList<ReaCenBarCodeFormat> tempList = new List<ReaCenBarCodeFormat>();
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
        public ReaCenBarCodeFormat Get(long id)
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
                strSql.Append("order by T.CBCFID desc");
            }
            strSql.Append(")AS Row, T.*  from Rea_CenBarCodeFormat T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.QuerySql(strSql.ToString());
        }

        public EntityList<ReaCenBarCodeFormat> GetListByHQL(string strSqlWhere, int start, int count)
        {
            EntityList<ReaCenBarCodeFormat> entityList = new EntityList<ReaCenBarCodeFormat>();
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

        public EntityList<ReaCenBarCodeFormat> GetListByHQL(string strSqlWhere, string Order, int start, int count)
        {
            EntityList<ReaCenBarCodeFormat> entityList = new EntityList<ReaCenBarCodeFormat>();
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

        public IList<ReaCenBarCodeFormat> LoadAll()
        {
            IList<ReaCenBarCodeFormat> tempList = new List<ReaCenBarCodeFormat>();

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

        public IList<ReaCenBarCodeFormat> GetObjects(ReaCenBarCodeFormat entity)
        {
            throw new NotImplementedException();
        }
        public bool BatchSaveVO(ReaCenBarCodeFormat voList)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(ReaCenBarCodeFormat entity)
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

        public void Evict(ReaCenBarCodeFormat entity)
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
        public bool Save(ReaCenBarCodeFormat entity)
        {
            throw new NotImplementedException();
        }

        public object SaveByEntity(ReaCenBarCodeFormat entity)
        {
            throw new NotImplementedException();
        }

        public bool SaveOrUpdate(ReaCenBarCodeFormat entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(string[] strParas)
        {
            throw new NotImplementedException();
        }

        public bool Update(ReaCenBarCodeFormat entity)
        {
            throw new NotImplementedException();
        }

        public int UpdateByHql(string hql)
        {
            throw new NotImplementedException();
        }
    }
}
