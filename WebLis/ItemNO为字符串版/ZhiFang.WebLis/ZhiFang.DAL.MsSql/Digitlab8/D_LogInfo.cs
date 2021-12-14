using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8
{
    //D_LogInfo
    public partial class D_LogInfo : BaseDALLisDB,IDLogInfo
    {
        public D_LogInfo(string dbsourceconn)
       {
           DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
       }
        public D_LogInfo()
        {
           
        }
        /// <summary>
        /// 供调用的增加日志方法，记录字典表的增删改操作
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="strLabCode"></param>
        /// <param name="strUserID"></param>
        /// <param name="strUserName"></param>
        /// <param name="dataAddTime"></param>
        /// <param name="intUserFLag"></param>
        /// <returns>true/false</returns>
        public int OperateLog(string strTableName, string strUserID, string strUserName, DateTime dataAddTime, int intUserFLag)
        {
            ZhiFang.Model.LogInfo logModel = new Model.LogInfo();
            logModel.TableName = strTableName.Trim();
            logModel.UserID = strUserID.Trim();
            logModel.UserName = strUserName.Trim();
            logModel.AddTime = dataAddTime;
            logModel.UseFlag = intUserFLag;
            return Add(logModel);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.LogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into D_LogInfo(");
            strSql.Append("TableName,UserID,UserName,AddTime,UseFlag");
            strSql.Append(") values (");
            strSql.Append("@TableName,@UserID,@UserName,@AddTime,@UseFlag");
            strSql.Append(") ");
            //strSql.Append(";select @@IDENTITY");		
            SqlParameter[] parameters = {
			            new SqlParameter("@TableName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UserID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UserName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AddTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.TableName;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.AddTime;
            parameters[4].Value = model.UseFlag;
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.LogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update D_LogInfo set ");

            strSql.Append(" TableName = @TableName , ");
            strSql.Append(" UserID = @UserID , ");
            strSql.Append(" UserName = @UserName , ");
            strSql.Append(" AddTime = @AddTime , ");
            strSql.Append(" UseFlag = @UseFlag  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Id", SqlDbType.Int,4) ,            
                        new SqlParameter("@TableName", SqlDbType.VarChar,50) ,                  
                        new SqlParameter("@UserID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UserName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AddTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };

            if (model.Id != null)
            {
                parameters[0].Value = model.Id;
            }

            if (model.TableName != null)
            {
                parameters[1].Value = model.TableName;
            }

            if (model.UserID != null)
            {
                parameters[2].Value = model.UserID;
            }

            if (model.UserName != null)
            {
                parameters[3].Value = model.UserName;
            }

            if (model.AddTime != null)
            {
                parameters[4].Value = model.AddTime;
            }

            if (model.UseFlag != null)
            {
                parameters[5].Value = model.UseFlag;
            }

            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from D_LogInfo ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
};
            parameters[0].Value = Id;


            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from D_LogInfo ");
            strSql.Append(" where ID in (" + Idlist + ")  ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.LogInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, TableName, DTimeStampe, UserID, UserName, AddTime, UseFlag  ");
            strSql.Append("  from D_LogInfo ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
};
            parameters[0].Value = Id;


            ZhiFang.Model.LogInfo model = new ZhiFang.Model.LogInfo();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.TableName = ds.Tables[0].Rows[0]["TableName"].ToString();
              
                if (ds.Tables[0].Rows[0]["DTimeStampe"].ToString() != "")
                {
                    model.DTimeStampe = DateTime.Parse(ds.Tables[0].Rows[0]["DTimeStampe"].ToString());
                }
                model.UserID = ds.Tables[0].Rows[0]["UserID"].ToString();
                model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                if (ds.Tables[0].Rows[0]["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UseFlag"].ToString() != "")
                {
                    model.UseFlag = int.Parse(ds.Tables[0].Rows[0]["UseFlag"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM D_LogInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
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
            strSql.Append(" * ");
            strSql.Append(" FROM D_LogInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.LogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM D_LogInfo where 1=1 ");

            if (model.TableName != null)
            {
                strSql.Append(" and TableName='" + model.TableName + "' ");
            }


            if (model.DTimeStampe != null)
            {
                strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
            }

            if (model.UserID != null)
            {
                strSql.Append(" and UserID='" + model.UserID + "' ");
            }

            if (model.UserName != null)
            {
                strSql.Append(" and UserName='" + model.UserName + "' ");
            }

            if (model.AddTime != null)
            {
                strSql.Append(" and AddTime='" + model.AddTime + "' ");
            }

            if (model.UseFlag != null)
            {
                strSql.Append(" and UseFlag=" + model.UseFlag + " ");
            }
            strSql.Append(" order by DTimeStampe desc ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        /// <summary>
        /// 根据客户端提交的时间获取DataSet
        /// </summary>
        public DataSet GetListByTimeStampe(ZhiFang.Model.LogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select '' as Status,'' as MsgRemark,TableName,max(convert(int,DTimeStampe)) as DTimeStampe ");
            strSql.Append(" FROM D_LogInfo where 1=1 ");
            if (model.IntTimeStampe != null && model.IntTimeStampe != 999999)
            {
                strSql.Append(" and convert(int,DTimeStampe)> '" + model.IntTimeStampe + "' ");
            }
            strSql.Append(" group by TableName order by DTimeStampe ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM D_LogInfo");
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

        public int GetTotalCount(Model.LogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM D_LogInfo where 1=1 ");

            if (model.TableName != null)
            {
                strSql.Append(" and TableName='" + model.TableName + "' ");
            }

            if (model.DTimeStampe != null)
            {
                strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
            }

            if (model.UserID != null)
            {
                strSql.Append(" and UserID='" + model.UserID + "' ");
            }

            if (model.UserName != null)
            {
                strSql.Append(" and UserName='" + model.UserName + "' ");
            }

            if (model.AddTime != null)
            {
                strSql.Append(" and AddTime='" + model.AddTime + "' ");
            }

            if (model.UseFlag != null)
            {
                strSql.Append(" and UseFlag=" + model.UseFlag + " ");
            }
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }


        #region IDataPage<LogInfo> 成员

        public DataSet GetListByPage(Model.LogInfo t, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }

        #endregion

        
        #region IDataBase<LogInfo> 成员
        public int AddUpdateByDataSet(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }
            else
                return 1;
        }
        public int AddByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into D_LogInfo (");
                strSql.Append("TableName,UserID,UserName,AddTime,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["TableName"].ToString().Trim() + "','" + dr["UserID"].ToString().Trim() + "','" + dr["UserName"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["UseFlag"].ToString().Trim() + "'");
                strSql.Append(") ");
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update D_LogInfo set ");

                strSql.Append(" TableName = '" + dr["TableName"].ToString().Trim() + "' , ");
                strSql.Append(" UserID = '" + dr["UserID"].ToString().Trim() + "' , ");
                strSql.Append(" UserName = '" + dr["UserName"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where ='" + dr[""].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        #endregion


        #region IDataBase<LogInfo> 成员

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "D_LogInfo");
        }

        public DataSet GetList(int Top, Model.LogInfo t, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public DataSet GetAllList()
        {
            return GetList("");
        }

        #endregion
    }
}

