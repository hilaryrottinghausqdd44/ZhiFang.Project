
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ZhiFang.DAO.SQL.WebAssist.GKBarcode;
using ZhiFang.Entity.WebAssist.GKBarcode;
using ZhiFang.IDAO.NHB.WebAssist;

namespace ZhiFang.DAO.SQL.WebAssist
{
    /// <summary>
    /// 数据访问类:Department
    /// </summary>
    public partial class Department_SQL: IDepartment_SQL
    {
        public Department_SQL()
        { }
        #region  Method
        //查询字段
        private string FieldStr = "DepID,DepName,QueryCode,Users,OperateTypeNo,Dglab_Index,[Update],ExeFileUpdate";

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Department GetModel()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(this.FieldStr);
            strSql.Append(" from Department ");
            strSql.Append(" where ");
            Department model = new Department();
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
        public Department DataRowToModel(DataRow row)
        {
            Department model = new Department();
            if (row != null)
            {
                if (row["DepID"] != null && row["DepID"].ToString() != "")
                {
                    model.DepID = int.Parse(row["DepID"].ToString());
                }
                if (row["DepName"] != null)
                {
                    model.DepName = row["DepName"].ToString();
                }
                if (row["QueryCode"] != null)
                {
                    model.QueryCode = row["QueryCode"].ToString();
                }
                if (row["Users"] != null)
                {
                    model.Users = row["Users"].ToString();
                }
                if (row["OperateTypeNo"] != null && row["OperateTypeNo"].ToString() != "")
                {
                    model.OperateTypeNo = int.Parse(row["OperateTypeNo"].ToString());
                }
                if (row["Dglab_Index"] != null && row["Dglab_Index"].ToString() != "")
                {
                    model.Dglab_Index = int.Parse(row["Dglab_Index"].ToString());
                }
                if (row["Update"] != null)
                {
                    model.Update = row["Update"].ToString();
                }
                if (row["ExeFileUpdate"] != null && row["ExeFileUpdate"].ToString() != "")
                {
                    model.ExeFileUpdate = (byte[])row["ExeFileUpdate"];
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
            strSql.Append(" FROM Department ");
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
            strSql.Append(" FROM Department ");
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
            strSql.Append("select count(1) FROM Department ");
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
            strSql.Append(")AS Row, T.*  from Department T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.QuerySql(DbHelperSQL.ConnectionString, strSql.ToString());
        }
        public IList<Department> GetListByHQL(string strSqlWhere)
        {
            IList<Department> tempList = new List<Department>();
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
        /*
		*/

        #endregion  Method
    }
}

