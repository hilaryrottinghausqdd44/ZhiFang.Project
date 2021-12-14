using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.IDAL;

namespace ZhiFang.DAL.MsSql.Weblis
{
    //文档交流表（不附带附件）
    public class DNNews_Interaction : BaseDALLisDB, IDNNews_Interaction
    {
        public DNNews_Interaction(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public DNNews_Interaction()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.N_News_Interaction model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into N_News_Interaction(");
            strSql.Append("LabID,InteractionID,FileID,Contents,SenderID,SenderName,ReceiverID,ReceiverName,HasAttachment,Memo,IsUse,DataAddTime");
            strSql.Append(") values (");
            strSql.Append("@LabID,@InteractionID,@FileID,@Contents,@SenderID,@SenderName,@ReceiverID,@ReceiverName,@HasAttachment,@Memo,@IsUse,@DataAddTime");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@LabID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@InteractionID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@FileID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@Contents", SqlDbType.VarChar,-1) ,
                        new SqlParameter("@SenderID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@SenderName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ReceiverID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@ReceiverName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@HasAttachment", SqlDbType.Bit,1) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@IsUse", SqlDbType.Bit,1) ,
                        new SqlParameter("@DataAddTime", SqlDbType.DateTime) 

            };

            parameters[0].Value = model.LabID;
            parameters[1].Value = model.InteractionID;
            parameters[2].Value = model.FileID;
            parameters[3].Value = model.Contents;
            parameters[4].Value = model.SenderID;
            parameters[5].Value = model.SenderName;
            parameters[6].Value = model.ReceiverID;
            parameters[7].Value = model.ReceiverName;
            parameters[8].Value = model.HasAttachment;
            parameters[9].Value = model.Memo;
            parameters[10].Value = model.IsUse;
            parameters[11].Value = model.DataAddTime;
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters)>0;

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.N_News_Interaction model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update N_News_Interaction set ");

            strSql.Append(" LabID = @LabID , ");
            strSql.Append(" InteractionID = @InteractionID , ");
            strSql.Append(" FileID = @FileID , ");
            strSql.Append(" Contents = @Contents , ");
            strSql.Append(" SenderID = @SenderID , ");
            strSql.Append(" SenderName = @SenderName , ");
            strSql.Append(" ReceiverID = @ReceiverID , ");
            strSql.Append(" ReceiverName = @ReceiverName , ");
            strSql.Append(" HasAttachment = @HasAttachment , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" IsUse = @IsUse , ");
            strSql.Append(" DataAddTime = @DataAddTime  ");
            strSql.Append(" where InteractionID=@InteractionID  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@LabID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@InteractionID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@FileID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@Contents", SqlDbType.VarChar,-1) ,
                        new SqlParameter("@SenderID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@SenderName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ReceiverID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@ReceiverName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@HasAttachment", SqlDbType.Bit,1) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@IsUse", SqlDbType.Bit,1) ,
                        new SqlParameter("@DataAddTime", SqlDbType.DateTime)

            };

            parameters[0].Value = model.LabID;
            parameters[1].Value = model.InteractionID;
            parameters[2].Value = model.FileID;
            parameters[3].Value = model.Contents;
            parameters[4].Value = model.SenderID;
            parameters[5].Value = model.SenderName;
            parameters[6].Value = model.ReceiverID;
            parameters[7].Value = model.ReceiverName;
            parameters[8].Value = model.HasAttachment;
            parameters[9].Value = model.Memo;
            parameters[10].Value = model.IsUse;
            parameters[11].Value = model.DataAddTime;
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
        public bool Delete(long InteractionID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from N_News_Interaction ");
            strSql.Append(" where InteractionID=@InteractionID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@InteractionID", SqlDbType.BigInt,8)          };
            parameters[0].Value = InteractionID;


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
        public Model.N_News_Interaction GetModel(long InteractionID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LabID, InteractionID, FileID, Contents, SenderID, SenderName, ReceiverID, ReceiverName, HasAttachment, Memo, IsUse, DataAddTime  ");
            strSql.Append("  from N_News_Interaction ");
            strSql.Append(" where InteractionID=@InteractionID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@InteractionID", SqlDbType.BigInt,8)          };
            parameters[0].Value = InteractionID;


            Model.N_News_Interaction model = new Model.N_News_Interaction();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(ds.Tables[0].Rows[0]["LabID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["InteractionID"].ToString() != "")
                {
                    model.InteractionID = long.Parse(ds.Tables[0].Rows[0]["InteractionID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FileID"].ToString() != "")
                {
                    model.FileID = long.Parse(ds.Tables[0].Rows[0]["FileID"].ToString());
                }
                model.Contents = ds.Tables[0].Rows[0]["Contents"].ToString();
                if (ds.Tables[0].Rows[0]["SenderID"].ToString() != "")
                {
                    model.SenderID = long.Parse(ds.Tables[0].Rows[0]["SenderID"].ToString());
                }
                model.SenderName = ds.Tables[0].Rows[0]["SenderName"].ToString();
                if (ds.Tables[0].Rows[0]["ReceiverID"].ToString() != "")
                {
                    model.ReceiverID = long.Parse(ds.Tables[0].Rows[0]["ReceiverID"].ToString());
                }
                model.ReceiverName = ds.Tables[0].Rows[0]["ReceiverName"].ToString();
                if (ds.Tables[0].Rows[0]["HasAttachment"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["HasAttachment"].ToString() == "1") || (ds.Tables[0].Rows[0]["HasAttachment"].ToString().ToLower() == "true"))
                    {
                        model.HasAttachment = true;
                    }
                    else
                    {
                        model.HasAttachment = false;
                    }
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
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
                if (ds.Tables[0].Rows[0]["DataAddTime"].ToString() != "")
                {
                    model.DataAddTime = DateTime.Parse(ds.Tables[0].Rows[0]["DataAddTime"].ToString());
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
            strSql.Append(" FROM N_News_Interaction ");
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
            strSql.Append(" FROM N_News_Interaction ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetList(string where, int page, int limit, string Sort = "DispOrder asc ")
        {
            throw new NotImplementedException();
        }

        public int GetCount(string where)
        {
            throw new NotImplementedException();
        }
    }
}

