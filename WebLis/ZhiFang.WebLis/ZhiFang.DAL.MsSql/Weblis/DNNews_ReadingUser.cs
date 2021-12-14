using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.IDAL;

namespace ZhiFang.DAL.MsSql.Weblis
{
    //文档阅读对象表
    public class DNNews_ReadingUser : BaseDALLisDB, IDNNews_ReadingUser
    {
        public DNNews_ReadingUser(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public DNNews_ReadingUser()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.N_News_ReadingUser model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into N_News_ReadingUser(");
            strSql.Append("LabID,FileReadingUserID,FileID,Type,DeptID,RoleID,UserID,Memo,DispOrder,IsUse,CreatorID,CreatorName,DataAddTime,DataUpdateTime");
            strSql.Append(") values (");
            strSql.Append("@LabID,@FileReadingUserID,@FileID,@Type,@DeptID,@RoleID,@UserID,@Memo,@DispOrder,@IsUse,@CreatorID,@CreatorName,@DataAddTime,@DataUpdateTime");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@LabID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@FileReadingUserID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@FileID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@Type", SqlDbType.BigInt,8) ,
                        new SqlParameter("@DeptID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@RoleID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@UserID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,
                        new SqlParameter("@IsUse", SqlDbType.Bit,1) ,
                        new SqlParameter("@CreatorID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@CreatorName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@DataAddTime", SqlDbType.DateTime) ,
                        new SqlParameter("@DataUpdateTime", SqlDbType.DateTime)

            };

            parameters[0].Value = model.LabID;
            parameters[1].Value = model.FileReadingUserID;
            parameters[2].Value = model.FileID;
            parameters[3].Value = model.Type;
            parameters[4].Value = model.DeptID;
            parameters[5].Value = model.RoleID;
            parameters[6].Value = model.UserID;
            parameters[7].Value = model.Memo;
            parameters[8].Value = model.DispOrder;
            parameters[9].Value = model.IsUse;
            parameters[10].Value = model.CreatorID;
            parameters[11].Value = model.CreatorName;
            parameters[12].Value = model.DataAddTime;
            parameters[13].Value = model.DataUpdateTime;
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters)>0;

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.N_News_ReadingUser model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update N_News_ReadingUser set ");

            strSql.Append(" LabID = @LabID , ");
            strSql.Append(" FileReadingUserID = @FileReadingUserID , ");
            strSql.Append(" FileID = @FileID , ");
            strSql.Append(" Type = @Type , ");
            strSql.Append(" DeptID = @DeptID , ");
            strSql.Append(" RoleID = @RoleID , ");
            strSql.Append(" UserID = @UserID , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" DispOrder = @DispOrder , ");
            strSql.Append(" IsUse = @IsUse , ");
            strSql.Append(" CreatorID = @CreatorID , ");
            strSql.Append(" CreatorName = @CreatorName , ");
            strSql.Append(" DataAddTime = @DataAddTime , ");
            strSql.Append(" DataUpdateTime = @DataUpdateTime  ");
            strSql.Append(" where FileReadingUserID=@FileReadingUserID  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@LabID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@FileReadingUserID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@FileID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@Type", SqlDbType.BigInt,8) ,
                        new SqlParameter("@DeptID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@RoleID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@UserID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,
                        new SqlParameter("@IsUse", SqlDbType.Bit,1) ,
                        new SqlParameter("@CreatorID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@CreatorName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@DataAddTime", SqlDbType.DateTime) ,
                        new SqlParameter("@DataUpdateTime", SqlDbType.DateTime)

            };

            parameters[0].Value = model.LabID;
            parameters[1].Value = model.FileReadingUserID;
            parameters[2].Value = model.FileID;
            parameters[3].Value = model.Type;
            parameters[4].Value = model.DeptID;
            parameters[5].Value = model.RoleID;
            parameters[6].Value = model.UserID;
            parameters[7].Value = model.Memo;
            parameters[8].Value = model.DispOrder;
            parameters[9].Value = model.IsUse;
            parameters[10].Value = model.CreatorID;
            parameters[11].Value = model.CreatorName;
            parameters[12].Value = model.DataAddTime;
            parameters[13].Value = model.DataUpdateTime;
            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
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
        public bool Delete(long FileReadingUserID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from N_News_ReadingUser ");
            strSql.Append(" where FileReadingUserID=@FileReadingUserID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@FileReadingUserID", SqlDbType.BigInt,8)          };
            parameters[0].Value = FileReadingUserID;


            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
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
        public Model.N_News_ReadingUser GetModel(long FileReadingUserID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LabID, FileReadingUserID, FileID, Type, DeptID, RoleID, UserID, Memo, DispOrder, IsUse, CreatorID, CreatorName, DataAddTime, DataUpdateTime  ");
            strSql.Append("  from N_News_ReadingUser ");
            strSql.Append(" where FileReadingUserID=@FileReadingUserID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@FileReadingUserID", SqlDbType.BigInt,8)          };
            parameters[0].Value = FileReadingUserID;


            Model.N_News_ReadingUser model = new Model.N_News_ReadingUser();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(ds.Tables[0].Rows[0]["LabID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FileReadingUserID"].ToString() != "")
                {
                    model.FileReadingUserID = long.Parse(ds.Tables[0].Rows[0]["FileReadingUserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FileID"].ToString() != "")
                {
                    model.FileID = long.Parse(ds.Tables[0].Rows[0]["FileID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = long.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DeptID"].ToString() != "")
                {
                    model.DeptID = long.Parse(ds.Tables[0].Rows[0]["DeptID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RoleID"].ToString() != "")
                {
                    model.RoleID = long.Parse(ds.Tables[0].Rows[0]["RoleID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = long.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                if (ds.Tables[0].Rows[0]["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsUse"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsUse"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsUse"].ToString().ToLower() == "true"))
                    {
                        model.IsUse = true;
                    }
                    else
                    {
                        model.IsUse = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["CreatorID"].ToString() != "")
                {
                    model.CreatorID = long.Parse(ds.Tables[0].Rows[0]["CreatorID"].ToString());
                }
                model.CreatorName = ds.Tables[0].Rows[0]["CreatorName"].ToString();
                if (ds.Tables[0].Rows[0]["DataAddTime"].ToString() != "")
                {
                    model.DataAddTime = DateTime.Parse(ds.Tables[0].Rows[0]["DataAddTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DataUpdateTime"].ToString() != "")
                {
                    model.DataUpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["DataUpdateTime"].ToString());
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
            strSql.Append(" FROM N_News_ReadingUser ");
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
            strSql.Append(" FROM N_News_ReadingUser ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }


    }
}

