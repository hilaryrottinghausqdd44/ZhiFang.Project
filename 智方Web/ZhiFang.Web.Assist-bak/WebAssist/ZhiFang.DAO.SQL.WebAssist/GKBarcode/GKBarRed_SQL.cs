using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZhiFang.DAO.SQL.WebAssist.GKBarcode;
using ZhiFang.Entity.WebAssist.GKBarcode;
using ZhiFang.IDAO.NHB.WebAssist;

namespace ZhiFang.DAO.SQL.WebAssist
{
    /// <summary>
    /// 数据访问类:GKBarRed
    /// </summary>
    public partial class GKBarRed_SQL : IGKBarRed_SQL
    {
        public GKBarRed_SQL()
        { }
        #region  Method
        //查询字段
        private string FieldStr = " Sel,RecDate,RecTime,DepID,TestTpyeID,CollecterID,Information1,Information2,Information3,Information4,BarCode,PrintInfor,RecievedInfor,Apply,ApplyDate,ApplyTime,CollectDate,CollectTime,ChroniclerID,TestResult,Judge,Judge_Operator,Judge_Date,Technician,TestDate,Fee,Archived,MonitorType,SerialNo,IsSync ";

        public bool UpdateIsSync(GKBarRed model, bool isSync)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GKBarRed set ");
            strSql.Append("IsSync=@IsSync");
            strSql.Append(" where BarCode=@BarCode and SerialNo=@SerialNo  ");

            SqlParameter[] parameters = {
                    new SqlParameter("@IsSync", SqlDbType.Bit,1),
                    new SqlParameter("@BarCode", SqlDbType.VarChar,50),
                    new SqlParameter("@SerialNo", SqlDbType.Int,4)
            };
            parameters[0].Value = isSync;// model.IsSync;
            parameters[1].Value = model.BarCode;
            parameters[2].Value = model.SerialNo;

            int rows = 0;
            rows = DbHelperSQL.ExecuteSql(DbHelperSQL.ConnectionString, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public IList<GKBarRed> GetListByHQL(string strSqlWhere)
        {
            IList<GKBarRed> tempList = new List<GKBarRed>();
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
        public GKBarRed GetModel(int SerialNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(this.FieldStr);
            strSql.Append(" from GKBarRed ");
            strSql.Append(" where SerialNo=" + SerialNo + "");
            GKBarRed model = new GKBarRed();
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
        public GKBarRed DataRowToModel(DataRow row)
        {
            GKBarRed model = new GKBarRed();
            if (row != null)
            {
                if (row["Sel"] != null)
                {
                    model.Sel = row["Sel"].ToString();
                }
                if (row["IsSync"] != null && row["IsSync"].ToString() != "")
                {
                    model.IsSync = bool.Parse(row["IsSync"].ToString());
                }
                if (row["RecDate"] != null && row["RecDate"].ToString() != "")
                {
                    model.RecDate = DateTime.Parse(row["RecDate"].ToString());
                }
                if (row["RecTime"] != null && row["RecTime"].ToString() != "")
                {
                    model.RecTime = DateTime.Parse(row["RecTime"].ToString());
                }
                if (row["DepID"] != null && row["DepID"].ToString() != "")
                {
                    model.DepID = int.Parse(row["DepID"].ToString());
                }
                if (row["TestTpyeID"] != null && row["TestTpyeID"].ToString() != "")
                {
                    model.TestTpyeID = int.Parse(row["TestTpyeID"].ToString());
                }
                if (row["CollecterID"] != null && row["CollecterID"].ToString() != "")
                {
                    model.CollecterID = int.Parse(row["CollecterID"].ToString());
                }
                if (row["Information1"] != null)
                {
                    model.Information1 = row["Information1"].ToString();
                }
                if (row["Information2"] != null)
                {
                    model.Information2 = row["Information2"].ToString();
                }
                if (row["Information3"] != null)
                {
                    model.Information3 = row["Information3"].ToString();
                }
                if (row["Information4"] != null)
                {
                    model.Information4 = row["Information4"].ToString();
                }
                if (row["BarCode"] != null)
                {
                    model.BarCode = row["BarCode"].ToString();
                }
                if (row["PrintInfor"] != null)
                {
                    model.PrintInfor = row["PrintInfor"].ToString();
                }
                if (row["RecievedInfor"] != null)
                {
                    model.RecievedInfor = row["RecievedInfor"].ToString();
                }
                if (row["Apply"] != null)
                {
                    model.Apply = row["Apply"].ToString();
                }
                if (row["ApplyDate"] != null)
                {
                    model.ApplyDate = row["ApplyDate"].ToString();
                }
                if (row["ApplyTime"] != null)
                {
                    model.ApplyTime = row["ApplyTime"].ToString();
                }
                if (row["CollectDate"] != null && row["CollectDate"].ToString() != "")
                {
                    model.CollectDate = DateTime.Parse(row["CollectDate"].ToString());
                }
                if (row["CollectTime"] != null && row["CollectTime"].ToString() != "")
                {
                    model.CollectTime = DateTime.Parse(row["CollectTime"].ToString());
                }
                if (row["ChroniclerID"] != null && row["ChroniclerID"].ToString() != "")
                {
                    model.ChroniclerID = int.Parse(row["ChroniclerID"].ToString());
                }
                if (row["TestResult"] != null)
                {
                    model.TestResult = row["TestResult"].ToString();
                }
                if (row["Judge"] != null)
                {
                    model.Judge = row["Judge"].ToString();
                }
                if (row["Judge_Operator"] != null)
                {
                    model.Judge_Operator = row["Judge_Operator"].ToString();
                }
                if (row["Judge_Date"] != null && row["Judge_Date"].ToString() != "")
                {
                    model.Judge_Date = DateTime.Parse(row["Judge_Date"].ToString());
                }
                if (row["Technician"] != null)
                {
                    model.Technician = row["Technician"].ToString();
                }
                if (row["TestDate"] != null && row["TestDate"].ToString() != "")
                {
                    model.TestDate = DateTime.Parse(row["TestDate"].ToString());
                }
                if (row["Fee"] != null)
                {
                    model.Fee = row["Fee"].ToString();
                }
                if (row["Archived"] != null)
                {
                    model.Archived = row["Archived"].ToString();
                }
                if (row["MonitorType"] != null)
                {
                    model.MonitorType = row["MonitorType"].ToString();
                }
                if (row["SerialNo"] != null && row["SerialNo"].ToString() != "")
                {
                    model.SerialNo = int.Parse(row["SerialNo"].ToString());
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
            strSql.Append(" FROM GKBarRed ");
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
            strSql.Append(" FROM GKBarRed ");
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
            strSql.Append("select count(1) FROM GKBarRed ");
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
            strSql.Append(")AS Row, T.*  from GKBarRed T ");
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

