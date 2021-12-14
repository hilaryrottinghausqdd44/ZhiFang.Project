using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IDAO.Base;
using ZhiFang.IDAO.NHB.BloodTransfusion;
using ZhiFang.DAO.SQL.BloodTransfusion;

namespace ZhiFang.DAO.SQL.BloodTransfusion
{
    public class VBloodLisResultDao_SQL : IDBaseDao<VBloodLisResult, long>, IDVBloodLisResultDao_SQL
    {
        DBUtility.SqlServerHelperP DbHelperSQL = new LisInterface.DbHelperSQLP(LisInterface.DbHelperSQLP.GetConnectionString());

        #region  公共生成
        //查询字段
        private string FieldStr = "*";
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM v_blood_lisresult vbloodlisresult ");
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
            strSql.Append("select count(1) FROM v_blood_lisresult vbloodlisresult ");
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
            strSql.Append(" FROM v_blood_lisresult vbloodlisresult ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.QuerySql(strSql.ToString());
        }
        /// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet SelectList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  ");
            strSql.Append(this.FieldStr);
            strSql.Append(" FROM v_blood_lisresult vbloodlisresult ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.SelectSql(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VBloodLisResult GetModel(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(this.FieldStr);
            strSql.Append(" from v_blood_lisresult vbloodlisresult ");
            strSql.Append(" where Id=" + id + " ");
            VBloodLisResult model = new VBloodLisResult();
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
        public VBloodLisResult DataRowToModel(DataRow row)
        {
            VBloodLisResult model = new VBloodLisResult();
            if (row != null)
            {
                //if (row["LabID"] != null && row["LabID"].ToString() != "")
                //{
                //    model.LabID = long.Parse(row["LabID"].ToString());
                //}
                //if (row["Id"] != null && row["Id"].ToString() != "")
                //{
                //    model.Id = long.Parse(row["Id"].ToString());
                //}
                if (row["BarCode"] != null)
                {
                    model.BarCode = row["BarCode"].ToString();
                }
                if (row["ItemNo"] != null)
                {
                    model.ItemNo = row["ItemNo"].ToString();
                }
                if (row["ItemName"] != null)
                {
                    model.ItemName = row["ItemName"].ToString();
                }
                if (row["EName"] != null)
                {
                    model.EName = row["EName"].ToString();
                }
                if (row["ParItemNo"] != null)
                {
                    model.ParItemNo = row["ParItemNo"].ToString();
                }
                if (row["ReceiveDate"] != null && row["ReceiveDate"].ToString() != "")
                {
                    model.ReceiveDate = DateTime.Parse(row["ReceiveDate"].ToString());
                }
                if (row["ReportDesc"] != null)
                {
                    model.ReportDesc = row["ReportDesc"].ToString();
                }
                if (row["b3code"] != null)
                {
                    model.B3code = row["b3code"].ToString();
                }
                if (row["CheckDateTime"] != null)
                {
                    model.CheckDateTime = row["CheckDateTime"].ToString();
                }
                if (row["Checker"] != null)
                {
                    model.Checker = row["Checker"].ToString();
                }
                if (row["PatName"] != null)
                {
                    model.PatName = row["PatName"].ToString();
                }
                if (row["SampleNo"] != null)
                {
                    model.SampleNo = row["SampleNo"].ToString();
                }
                if (row["SectionNo"] != null)
                {
                    model.SectionNo = row["SectionNo"].ToString();
                }
                if (row["TestTypeNo"] != null)
                {
                    model.TestTypeNo = row["TestTypeNo"].ToString();
                }
                if (row["PatNo"] != null)
                {
                    model.PatNo = row["PatNo"].ToString();
                }
                if (row["itemtesttime"] != null)
                {
                    model.Itemtesttime = row["itemtesttime"].ToString();
                }
                if (row["itemtesttime"] != null)
                {
                    model.Itemtesttime = row["itemtesttime"].ToString();
                }
                if (row["ItemResult"] != null)
                {
                    model.ItemResult = row["ItemResult"].ToString();
                }
                
                if (row["ItemUnit"] != null)
                {
                    model.ItemUnit = row["ItemUnit"].ToString();
                }
                //if (row["DataTimeStamp"] != null && row["DataTimeStamp"].ToString() != "")
                //{
                //    model.DataTimeStamp = row["DataTimeStamp"] as System.Byte[];
                //}
            }
            return model;
        }
        public IList<VBloodLisResult> GetListByHQL(string strSqlWhere)
        {
            IList<VBloodLisResult> tempList = new List<VBloodLisResult>();
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
        public IList<VBloodLisResult> SelectListByHQL(string strSqlWhere)
        {
            IList<VBloodLisResult> tempList = new List<VBloodLisResult>();
            DataSet ds = SelectList(strSqlWhere);
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
        public VBloodLisResult Get(long id)
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
                strSql.Append("order by T.ItemNo desc");
            }
            strSql.Append(")AS Row, T.*  from v_blood_lisresult T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.QuerySql(strSql.ToString());
        }

        public EntityList<VBloodLisResult> GetListByHQL(string strSqlWhere, int start, int count)
        {
            EntityList<VBloodLisResult> entityList = new EntityList<VBloodLisResult>();
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

        public EntityList<VBloodLisResult> GetListByHQL(string strSqlWhere, string Order, int start, int count)
        {
            EntityList<VBloodLisResult> entityList = new EntityList<VBloodLisResult>();
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

        public IList<VBloodLisResult> LoadAll()
        {
            IList<VBloodLisResult> tempList = new List<VBloodLisResult>();

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

        public IList<VBloodLisResult> GetObjects(VBloodLisResult entity)
        {
            throw new NotImplementedException();
        }
        public bool BatchSaveVO(VBloodLisResult voList)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(VBloodLisResult entity)
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

        public void Evict(VBloodLisResult entity)
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
        public bool Save(VBloodLisResult entity)
        {
            throw new NotImplementedException();
        }

        public object SaveByEntity(VBloodLisResult entity)
        {
            throw new NotImplementedException();
        }

        public bool SaveOrUpdate(VBloodLisResult entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(string[] strParas)
        {
            throw new NotImplementedException();
        }

        public bool Update(VBloodLisResult entity)
        {
            throw new NotImplementedException();
        }

        public int UpdateByHql(string hql)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 获取LIS统计结果

        #endregion
    }
}
