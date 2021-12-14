using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using ZhiFang.ReportFormQueryPrint.Common;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter
{
    /// <summary>
    /// 数据访问类:SC_Operation
    /// </summary>
    public  class SC_Operation : ISC_Operation
    {
        public SC_Operation()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long SCOperationID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SC_Operation");
            strSql.Append(" where SCOperationID=SQL2012SCOperationID ");
            SqlParameter[] parameters = {
                    new SqlParameter("SQL2012SCOperationID", SqlDbType.BigInt,8)            };
            parameters[0].Value = SCOperationID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.SC_Operation t)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (t.BobjectID != null )
            {
                strSql1.Append("BobjectID ,");
                strSql2.Append("'" + t.BobjectID + "',");
            }
            if (t.Type != null)
            {
                strSql1.Append("Type ,");
                strSql2.Append("'" + t.Type + "',");
            }
            if (t.Memo != null)
            {
                strSql1.Append("Memo ,");
                strSql2.Append("'" + t.Memo + "',");
            }
            if (t.DispOrder != null)
            {
                strSql1.Append("DispOrder ,");
                strSql2.Append("'" + t.DispOrder + "',");
            }
            if (t.IsUse != null)
            {
                strSql1.Append("IsUse,");
                strSql2.Append("'" + t.IsUse + "',");
            }
            if (t.CreatorID != null )
            {
                strSql1.Append("CreatorID,");
                strSql2.Append("'" + t.CreatorID + "',");
            }
            if (t.CreatorName != null)
            {
                strSql1.Append("CreatorName,");
                strSql2.Append(t.CreatorName + ",");
            }
            if (t.TypeName != null)
            {
                strSql1.Append("TypeName,");
                strSql2.Append("'" + t.TypeName + "',");
            }
            if (t.BusinessModuleCode != null)
            {
                strSql1.Append("BusinessModuleCode,");
                strSql2.Append("'" + t.BusinessModuleCode + "',");
            }
            strSql1.Append("SCOperationID,");
            strSql2.Append(GUIDHelp.GetGUIDInt() + ",");
            strSql1.Append("DataAddTime");
            strSql2.Append("'" + DateTime.Now + "'");
            strSql.Append("insert into SC_Operation(");
            strSql.Append(strSql1.ToString());
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString());
            strSql.Append(")");
            ZhiFang.Common.Log.Log.Debug("SC_Operation.Add sql = " + strSql.ToString());
            int  rows= DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ZhiFang.ReportFormQueryPrint.Model.SC_Operation model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_Operation set ");
            strSql.Append("LabID=SQL2012LabID,");
            strSql.Append("BobjectID=SQL2012BobjectID,");
            strSql.Append("Type=SQL2012Type,");
            strSql.Append("Memo=SQL2012Memo,");
            strSql.Append("DispOrder=SQL2012DispOrder,");
            strSql.Append("IsUse=SQL2012IsUse,");
            strSql.Append("CreatorID=SQL2012CreatorID,");
            strSql.Append("CreatorName=SQL2012CreatorName,");
            strSql.Append("DataAddTime=SQL2012DataAddTime,");
            strSql.Append("DataUpdateTime=SQL2012DataUpdateTime,");
            strSql.Append("TypeName=SQL2012TypeName,");
            strSql.Append("BusinessModuleCode=SQL2012BusinessModuleCode");
            strSql.Append(" where SCOperationID=SQL2012SCOperationID ");
            SqlParameter[] parameters = {
                    new SqlParameter("SQL2012LabID", SqlDbType.BigInt,8),
                    new SqlParameter("SQL2012BobjectID", SqlDbType.BigInt,8),
                    new SqlParameter("SQL2012Type", SqlDbType.BigInt,8),
                    new SqlParameter("SQL2012Memo", SqlDbType.VarChar,500),
                    new SqlParameter("SQL2012DispOrder", SqlDbType.Int,4),
                    new SqlParameter("SQL2012IsUse", SqlDbType.Bit,1),
                    new SqlParameter("SQL2012CreatorID", SqlDbType.BigInt,8),
                    new SqlParameter("SQL2012CreatorName", SqlDbType.VarChar,50),
                    new SqlParameter("SQL2012DataAddTime", SqlDbType.DateTime),
                    new SqlParameter("SQL2012DataUpdateTime", SqlDbType.DateTime),
                    new SqlParameter("SQL2012TypeName", SqlDbType.VarChar,50),
                    new SqlParameter("SQL2012BusinessModuleCode", SqlDbType.VarChar,50),
                    new SqlParameter("SQL2012SCOperationID", SqlDbType.BigInt,8)};
            parameters[1].Value = model.BobjectID;
            parameters[2].Value = model.Type;
            parameters[3].Value = model.Memo;
            parameters[4].Value = model.DispOrder;
            parameters[5].Value = model.IsUse;
            parameters[6].Value = model.CreatorID;
            parameters[7].Value = model.CreatorName;
            parameters[9].Value = model.DataUpdateTime;
            parameters[10].Value = model.TypeName;
            parameters[11].Value = model.BusinessModuleCode;
            parameters[12].Value = model.SCOperationID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long SCOperationID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SC_Operation ");
            strSql.Append(" where SCOperationID=SQL2012SCOperationID ");
            SqlParameter[] parameters = {
                    new SqlParameter("SQL2012SCOperationID", SqlDbType.BigInt,8)            };
            parameters[0].Value = SCOperationID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string SCOperationIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SC_Operation ");
            strSql.Append(" where SCOperationID in (" + SCOperationIDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.ReportFormQueryPrint.Model.SC_Operation GetModel(long SCOperationID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 LabID,SCOperationID,BobjectID,Type,Memo,DispOrder,IsUse,CreatorID,CreatorName,DataAddTime,DataUpdateTime,DataTimeStamp,TypeName,BusinessModuleCode from SC_Operation ");
            strSql.Append(" where SCOperationID=SQL2012SCOperationID ");
            SqlParameter[] parameters = {
                    new SqlParameter("SQL2012SCOperationID", SqlDbType.BigInt,8)            };
            parameters[0].Value = SCOperationID;

            ZhiFang.ReportFormQueryPrint.Model.SC_Operation model = new ZhiFang.ReportFormQueryPrint.Model.SC_Operation();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
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
        public ZhiFang.ReportFormQueryPrint.Model.SC_Operation DataRowToModel(DataRow row)
        {
            ZhiFang.ReportFormQueryPrint.Model.SC_Operation model = new ZhiFang.ReportFormQueryPrint.Model.SC_Operation();
            if (row != null)
            {
                
                if (row["SCOperationID"] != null && row["SCOperationID"].ToString() != "")
                {
                    model.SCOperationID = long.Parse(row["SCOperationID"].ToString());
                }
                if (row["BobjectID"] != null && row["BobjectID"].ToString() != "")
                {
                    model.BobjectID = long.Parse(row["BobjectID"].ToString());
                }
                if (row["Type"] != null && row["Type"].ToString() != "")
                {
                    model.Type = long.Parse(row["Type"].ToString());
                }
                if (row["Memo"] != null)
                {
                    model.Memo = row["Memo"].ToString();
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
                if (row["CreatorID"] != null && row["CreatorID"].ToString() != "")
                {
                    model.CreatorID = long.Parse(row["CreatorID"].ToString());
                }
                if (row["CreatorName"] != null)
                {
                    model.CreatorName = row["CreatorName"].ToString();
                }
               
                if (row["DataUpdateTime"] != null && row["DataUpdateTime"].ToString() != "")
                {
                    model.DataUpdateTime = DateTime.Parse(row["DataUpdateTime"].ToString());
                }
                
                if (row["TypeName"] != null)
                {
                    model.TypeName = row["TypeName"].ToString();
                }
                if (row["BusinessModuleCode"] != null)
                {
                    model.BusinessModuleCode = row["BusinessModuleCode"].ToString();
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
            strSql.Append("select LabID,SCOperationID,BobjectID,Type,Memo,DispOrder,IsUse,CreatorID,CreatorName,DataAddTime,DataUpdateTime,DataTimeStamp,TypeName,BusinessModuleCode ");
            strSql.Append(" FROM SC_Operation ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
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
            strSql.Append(" LabID,SCOperationID,BobjectID,Type,Memo,DispOrder,IsUse,CreatorID,CreatorName,DataAddTime,DataUpdateTime,DataTimeStamp,TypeName,BusinessModuleCode ");
            strSql.Append(" FROM SC_Operation ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM SC_Operation ");
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
                strSql.Append("order by T.SCOperationID desc");
            }
            strSql.Append(")AS Row, T.*  from SC_Operation T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("SQL2012tblName", SqlDbType.VarChar, 255),
					new SqlParameter("SQL2012fldName", SqlDbType.VarChar, 255),
					new SqlParameter("SQL2012PageSize", SqlDbType.Int),
					new SqlParameter("SQL2012PageIndex", SqlDbType.Int),
					new SqlParameter("SQL2012IsReCount", SqlDbType.Bit),
					new SqlParameter("SQL2012OrderType", SqlDbType.Bit),
					new SqlParameter("SQL2012strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "SC_Operation";
			parameters[1].Value = "SCOperationID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

