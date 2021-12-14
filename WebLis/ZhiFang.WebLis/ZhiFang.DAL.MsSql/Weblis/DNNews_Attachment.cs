using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.IDAL;

namespace ZhiFang.DAL.MsSql.Weblis
{
    //文档附件表
    public class DNNews_Attachment : BaseDALLisDB, IDNNews_Attachment
    {
        public DNNews_Attachment(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public DNNews_Attachment()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.N_News_Attachment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into N_News_Attachment(");
            strSql.Append("LabID,FileAttachmentID,FileID,FileName,FileExt,FileSize,FilePath,Memo,DispOrder,IsUse,CreatorID,CreatorName,DataAddTime,DataUpdateTime,NewFileName,FileType");
            strSql.Append(") values (");
            strSql.Append("@LabID,@FileAttachmentID,@FileID,@FileName,@FileExt,@FileSize,@FilePath,@Memo,@DispOrder,@IsUse,@CreatorID,@CreatorName,@DataAddTime,@DataUpdateTime,@NewFileName,@FileType");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@LabID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@FileAttachmentID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@FileID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@FileName", SqlDbType.VarChar,500) ,
                        new SqlParameter("@FileExt", SqlDbType.VarChar,100) ,
                        new SqlParameter("@FileSize", SqlDbType.BigInt,8) ,
                        new SqlParameter("@FilePath", SqlDbType.VarChar,500) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,
                        new SqlParameter("@IsUse", SqlDbType.Bit,1) ,
                        new SqlParameter("@CreatorID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@CreatorName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@DataAddTime", SqlDbType.DateTime) ,
                        new SqlParameter("@DataUpdateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@NewFileName", SqlDbType.VarChar,500) ,
                        new SqlParameter("@FileType", SqlDbType.VarChar,100)

            };

            parameters[0].Value = model.LabID;
            parameters[1].Value = model.FileAttachmentID;
            parameters[2].Value = model.FileID;
            parameters[3].Value = model.FileName;
            parameters[4].Value = model.FileExt;
            parameters[5].Value = model.FileSize;
            parameters[6].Value = model.FilePath;
            parameters[7].Value = model.Memo;
            parameters[8].Value = model.DispOrder;
            parameters[9].Value = model.IsUse;
            parameters[10].Value = model.CreatorID;
            parameters[11].Value = model.CreatorName;
            parameters[12].Value = model.DataAddTime;
            parameters[13].Value = model.DataUpdateTime;
            parameters[14].Value = model.NewFileName;
            parameters[15].Value = model.FileType;
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters)>0;

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.N_News_Attachment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update N_News_Attachment set ");

            strSql.Append(" LabID = @LabID , ");
            strSql.Append(" FileAttachmentID = @FileAttachmentID , ");
            strSql.Append(" FileID = @FileID , ");
            strSql.Append(" FileName = @FileName , ");
            strSql.Append(" FileExt = @FileExt , ");
            strSql.Append(" FileSize = @FileSize , ");
            strSql.Append(" FilePath = @FilePath , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" DispOrder = @DispOrder , ");
            strSql.Append(" IsUse = @IsUse , ");
            strSql.Append(" CreatorID = @CreatorID , ");
            strSql.Append(" CreatorName = @CreatorName , ");
            strSql.Append(" DataAddTime = @DataAddTime , ");
            strSql.Append(" DataUpdateTime = @DataUpdateTime , ");
            strSql.Append(" NewFileName = @NewFileName , ");
            strSql.Append(" FileType = @FileType  ");
            strSql.Append(" where FileAttachmentID=@FileAttachmentID  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@LabID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@FileAttachmentID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@FileID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@FileName", SqlDbType.VarChar,500) ,
                        new SqlParameter("@FileExt", SqlDbType.VarChar,100) ,
                        new SqlParameter("@FileSize", SqlDbType.BigInt,8) ,
                        new SqlParameter("@FilePath", SqlDbType.VarChar,500) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,
                        new SqlParameter("@IsUse", SqlDbType.Bit,1) ,
                        new SqlParameter("@CreatorID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@CreatorName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@DataAddTime", SqlDbType.DateTime) ,
                        new SqlParameter("@DataUpdateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@NewFileName", SqlDbType.VarChar,500) ,
                        new SqlParameter("@FileType", SqlDbType.VarChar,100)

            };

            parameters[0].Value = model.LabID;
            parameters[1].Value = model.FileAttachmentID;
            parameters[2].Value = model.FileID;
            parameters[3].Value = model.FileName;
            parameters[4].Value = model.FileExt;
            parameters[5].Value = model.FileSize;
            parameters[6].Value = model.FilePath;
            parameters[7].Value = model.Memo;
            parameters[8].Value = model.DispOrder;
            parameters[9].Value = model.IsUse;
            parameters[10].Value = model.CreatorID;
            parameters[11].Value = model.CreatorName;
            parameters[12].Value = model.DataAddTime;
            parameters[13].Value = model.DataUpdateTime;
            parameters[14].Value = model.NewFileName;
            parameters[15].Value = model.FileType;
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
        public bool Delete(long FileAttachmentID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from N_News_Attachment ");
            strSql.Append(" where FileAttachmentID=@FileAttachmentID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@FileAttachmentID", SqlDbType.BigInt,8)           };
            parameters[0].Value = FileAttachmentID;


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
        public Model.N_News_Attachment GetModel(long FileAttachmentID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LabID, FileAttachmentID, FileID, FileName, FileExt, FileSize, FilePath, Memo, DispOrder, IsUse, CreatorID, CreatorName, DataAddTime, DataUpdateTime,  NewFileName, FileType  ");
            strSql.Append("  from N_News_Attachment ");
            strSql.Append(" where FileAttachmentID=@FileAttachmentID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@FileAttachmentID", SqlDbType.BigInt,8)           };
            parameters[0].Value = FileAttachmentID;


            Model.N_News_Attachment model = new Model.N_News_Attachment();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(ds.Tables[0].Rows[0]["LabID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FileAttachmentID"].ToString() != "")
                {
                    model.FileAttachmentID = long.Parse(ds.Tables[0].Rows[0]["FileAttachmentID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FileID"].ToString() != "")
                {
                    model.FileID = long.Parse(ds.Tables[0].Rows[0]["FileID"].ToString());
                }
                model.FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                model.FileExt = ds.Tables[0].Rows[0]["FileExt"].ToString();
                if (ds.Tables[0].Rows[0]["FileSize"].ToString() != "")
                {
                    model.FileSize = long.Parse(ds.Tables[0].Rows[0]["FileSize"].ToString());
                }
                model.FilePath = ds.Tables[0].Rows[0]["FilePath"].ToString();
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
                //if (ds.Tables[0].Rows[0]["DataTimeStamp"].ToString() != "")
                //{
                //    model.DataTimeStamp = DateTime.Parse(ds.Tables[0].Rows[0]["DataTimeStamp"].ToString());
                //}
                model.NewFileName = ds.Tables[0].Rows[0]["NewFileName"].ToString();
                model.FileType = ds.Tables[0].Rows[0]["FileType"].ToString();

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
            strSql.Append(" FROM N_News_Attachment ");
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
            strSql.Append(" FROM N_News_Attachment ");
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

