using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.IDAL;

namespace ZhiFang.DAL.MsSql.Weblis
{
    //文档阅读记录表
    public class DNNews_ReadingLog : BaseDALLisDB, IDNNews_ReadingLog
    {
        public DNNews_ReadingLog(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public DNNews_ReadingLog()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.N_News_ReadingLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into N_News_ReadingLog(");
            strSql.Append("LabID,FileReadingLogID,FileID,ReaderID,ReaderName,ReadTimes,Memo,DispOrder,IsUse,CreatorID,CreatorName,DataAddTime");
            strSql.Append(") values (");
            strSql.Append("@LabID,@FileReadingLogID,@FileID,@ReaderID,@ReaderName,@ReadTimes,@Memo,@DispOrder,@IsUse,@CreatorID,@CreatorName,@DataAddTime");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@LabID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@FileReadingLogID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@FileID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@ReaderID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@ReaderName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ReadTimes", SqlDbType.Int,4) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,
                        new SqlParameter("@IsUse", SqlDbType.Bit,1) ,
                        new SqlParameter("@CreatorID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@CreatorName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@DataAddTime", SqlDbType.DateTime)

            };

            parameters[0].Value = model.LabID;
            parameters[1].Value = model.FileReadingLogID;
            parameters[2].Value = model.FileID;
            parameters[3].Value = model.ReaderID;
            parameters[4].Value = model.ReaderName;
            parameters[5].Value = model.ReadTimes;
            parameters[6].Value = model.Memo;
            parameters[7].Value = model.DispOrder;
            parameters[8].Value = model.IsUse;
            parameters[9].Value = model.CreatorID;
            parameters[10].Value = model.CreatorName;
            parameters[11].Value = model.DataAddTime;
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters)>0;

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.N_News_ReadingLog model)
        {
            
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update N_News_ReadingLog set ");
            if (model.LabID != null)
            {
                strSql.Append("LabID=" + model.LabID + ",");
            }
            if (model.FileID != null)
            {
                strSql.Append("FileID=" + model.FileID + ",");
            }
            if (model.ReaderID != null)
            {
                strSql.Append("ReaderID=" + model.ReaderID + ",");
            }
            else
            {
                strSql.Append("ReaderID= null ,");
            }
            if (model.ReaderName != null)
            {
                strSql.Append("ReaderName='" + model.ReaderName + "',");
            }
            else
            {
                strSql.Append("ReaderName= null ,");
            }
            if (model.ReadTimes != null)
            {
                strSql.Append("ReadTimes=" + model.ReadTimes + ",");
            }
            else
            {
                strSql.Append("ReadTimes= null ,");
            }
            if (model.Memo != null)
            {
                strSql.Append("Memo='" + model.Memo + "',");
            }
            else
            {
                strSql.Append("Memo= null ,");
            }
            if (model.DispOrder != null)
            {
                strSql.Append("DispOrder=" + model.DispOrder + ",");
            }
            else
            {
                strSql.Append("DispOrder= null ,");
            }
            if (model.IsUse != null)
            {
                strSql.Append("IsUse=" + (model.IsUse ? 1 : 0) + ",");
            }
            else
            {
                strSql.Append("IsUse= null ,");
            }
            if (model.CreatorID != null)
            {
                strSql.Append("CreatorID=" + model.CreatorID + ",");
            }
            else
            {
                strSql.Append("CreatorID= null ,");
            }
            if (model.CreatorName != null)
            {
                strSql.Append("CreatorName='" + model.CreatorName + "',");
            }
            else
            {
                strSql.Append("CreatorName= null ,");
            }
            if (model.DataAddTime != null)
            {
                strSql.Append("DataAddTime='" + model.DataAddTime + "',");
            }
            else
            {
                strSql.Append("DataAddTime= null ,");
            }
            if (model.DataUpdateTime != null)
            {
                strSql.Append("DataUpdateTime='" + model.DataUpdateTime + "',");
            }
            else
            {
                strSql.Append("DataUpdateTime= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where FileReadingLogID=" + model.FileReadingLogID + " ");
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            if (rowsAffected > 0)
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
        public bool Delete(long FileReadingLogID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from N_News_ReadingLog ");
            strSql.Append(" where FileReadingLogID=@FileReadingLogID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@FileReadingLogID", SqlDbType.BigInt,8)           };
            parameters[0].Value = FileReadingLogID;


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
        public Model.N_News_ReadingLog GetModel(long FileReadingLogID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LabID, FileReadingLogID, FileID, ReaderID, ReaderName, ReadTimes, Memo, DispOrder, IsUse, CreatorID, CreatorName, DataAddTime, DataUpdateTime");
            strSql.Append("  from N_News_ReadingLog ");
            strSql.Append(" where FileReadingLogID=@FileReadingLogID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@FileReadingLogID", SqlDbType.BigInt,8)           };
            parameters[0].Value = FileReadingLogID;


            Model.N_News_ReadingLog model = new Model.N_News_ReadingLog();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(ds.Tables[0].Rows[0]["LabID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FileReadingLogID"].ToString() != "")
                {
                    model.FileReadingLogID = long.Parse(ds.Tables[0].Rows[0]["FileReadingLogID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FileID"].ToString() != "")
                {
                    model.FileID = long.Parse(ds.Tables[0].Rows[0]["FileID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReaderID"].ToString() != "")
                {
                    model.ReaderID = long.Parse(ds.Tables[0].Rows[0]["ReaderID"].ToString());
                }
                model.ReaderName = ds.Tables[0].Rows[0]["ReaderName"].ToString();
                if (ds.Tables[0].Rows[0]["ReadTimes"].ToString() != "")
                {
                    model.ReadTimes = int.Parse(ds.Tables[0].Rows[0]["ReadTimes"].ToString());
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
            strSql.Append(" FROM N_News_ReadingLog ");
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
            strSql.Append(" FROM N_News_ReadingLog ");
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as count FROM N_News_ReadingLog ");
            if (where.Trim() != "")
            {
                strSql.Append(" where " + where);
            }
            string result= DbHelperSQL.ExecuteScalar(strSql.ToString());
            int a;
            if (int.TryParse(result, out a))
            {
                return a;
            }
            return 0;
        }

        public void UpdateTimes(string where)
        {
            StringBuilder strSql = new StringBuilder();
           
            if (where.Trim() != "")
            {
                strSql.Append("update N_News_ReadingLog set ReadTimes=ReadTimes+1 where " + where);
            }
            else
            {
                return;
            }
            ZhiFang.Common.Log.Log.Debug(" UpdateTimes.strSql=" + strSql.ToString());
            int result = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }
    }
}

