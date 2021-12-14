
using System;
using System.Data;
using System.Text;
using ZhiFang.IDAO.NHB.WebAssist;
using System.Collections.Generic;
using ZhiFang.DAO.SQL.WebAssist.HisInterface;
using ZhiFang.Entity.WebAssist.HisInterface;

namespace ZhiFang.DAO.SQL.WebAssist
{
    /// <summary>
    /// 百色市人民医院科室人员关系数据访问类:BSDeptUserVODao_SQL
    /// </summary>
    public partial class BSDeptUserVODao_SQL : IDBSDeptUserVODao_SQL
    {
        public BSDeptUserVODao_SQL()
        { }

        #region  Method
        //查询字段
        private string FieldStr = " PERSONAL_CODE,PERSONAL_NAME,PERSONAL_DEPT,PERSONAL_DEPT_NAME,PERSONAL_CLASS";
        public IList<BSDeptUserVO> GetListByHQL(string strSqlWhere)
        {
            IList<BSDeptUserVO> tempList = new List<BSDeptUserVO>();
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
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BSDeptUserVO GetModel()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(this.FieldStr);
            strSql.Append(" from [LAB_PERSONAL_INFORMATION] ");
            strSql.Append(" where ");
            BSDeptUserVO model = new BSDeptUserVO();
            DataSet ds = DbHelperSQL.QuerySql(DbHelperSQL.ConnectionString, strSql.ToString());
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
        public BSDeptUserVO DataRowToModel(DataRow row)
        {
            BSDeptUserVO model = new BSDeptUserVO();
            if (row != null)
            {
                if (row["PERSONAL_CODE"] != null && row["PERSONAL_CODE"].ToString() != "")
                {
                    model.PERSONAL_CODE = row["PERSONAL_CODE"].ToString();
                }
                if (row["PERSONAL_NAME"] != null)
                {
                    model.PERSONAL_NAME = row["PERSONAL_NAME"].ToString();
                }
                if (row["PERSONAL_CLASS"] != null)
                {
                    model.PERSONAL_CLASS = row["PERSONAL_CLASS"].ToString();
                }
                if (row["PERSONAL_DEPT"] != null)
                {
                    model.PERSONAL_DEPT = row["PERSONAL_DEPT"].ToString();
                }
                if (row["PERSONAL_DEPT_NAME"] != null)
                {
                    model.PERSONAL_DEPT_NAME = row["PERSONAL_DEPT_NAME"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(this.FieldStr);
            strSql.Append(" FROM [LAB_PERSONAL_INFORMATION]  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.QuerySql(DbHelperSQL.ConnectionString, strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(this.FieldStr);
            strSql.Append(" FROM [LAB_PERSONAL_INFORMATION] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.QuerySql(DbHelperSQL.ConnectionString, strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM [LAB_PERSONAL_INFORMATION] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(DbHelperSQL.ConnectionString, strSql.ToString());
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
                strSql.Append("order by T.SerialNo desc");
            }
            strSql.Append(")AS Row, T.*  from [LAB_PERSONAL_INFORMATION] T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.QuerySql(DbHelperSQL.ConnectionString, strSql.ToString());
        }

        #endregion  Method
    }
}

