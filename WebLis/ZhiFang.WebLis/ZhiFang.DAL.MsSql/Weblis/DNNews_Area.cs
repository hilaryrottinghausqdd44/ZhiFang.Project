using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.IDAL;

namespace ZhiFang.DAL.MsSql.Weblis
{
    //N_News_Area
    public class DNNews_Area : BaseDALLisDB,IDNNews_Area
    {
        public DNNews_Area(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public DNNews_Area()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.N_News_Area model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into N_News_Area(");
            strSql.Append("LabID,NewsAreaId,CName,SName,Shortcode,StandCode,DeveCode,PinYinZiTou,DispOrder,IsUse,Memo,DataAddTime");
            strSql.Append(") values (");
            strSql.Append("@LabID,@NewsAreaId,@CName,@SName,@Shortcode,@StandCode,@DeveCode,@PinYinZiTou,@DispOrder,@IsUse,@Memo,@DataAddTime");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@LabID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@NewsAreaId", SqlDbType.BigInt,8) ,
                        new SqlParameter("@CName", SqlDbType.VarChar,100) ,
                        new SqlParameter("@SName", SqlDbType.VarChar,40) ,
                        new SqlParameter("@Shortcode", SqlDbType.VarChar,20) ,
                        new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,
                        new SqlParameter("@DeveCode", SqlDbType.VarChar,50) ,
                        new SqlParameter("@PinYinZiTou", SqlDbType.VarChar,50) ,
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,
                        new SqlParameter("@IsUse", SqlDbType.Bit,1) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@DataAddTime", SqlDbType.DateTime)

            };

            parameters[0].Value = model.LabID;
            parameters[1].Value = model.NewsAreaId;
            parameters[2].Value = model.CName;
            parameters[3].Value = model.SName;
            parameters[4].Value = model.ShortCode;
            parameters[5].Value = model.StandCode;
            parameters[6].Value = model.DeveCode;
            parameters[7].Value = model.PinYinZiTou;
            parameters[8].Value = model.DispOrder;
            parameters[9].Value = model.IsUse;
            parameters[10].Value = model.Memo;
            parameters[11].Value = model.DataAddTime;
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters)>0;

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.N_News_Area model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update N_News_Area set ");

            strSql.Append(" LabID = @LabID , ");
            strSql.Append(" NewsAreaId = @NewsAreaId , ");
            strSql.Append(" CName = @CName , ");
            strSql.Append(" SName = @SName , ");
            strSql.Append(" Shortcode = @Shortcode , ");
            strSql.Append(" StandCode = @StandCode , ");
            strSql.Append(" DeveCode = @DeveCode , ");
            strSql.Append(" PinYinZiTou = @PinYinZiTou , ");
            strSql.Append(" DispOrder = @DispOrder , ");
            strSql.Append(" IsUse = @IsUse , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" DataAddTime = @DataAddTime  ");
            strSql.Append(" where NewsAreaId=@NewsAreaId  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@LabID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@NewsAreaId", SqlDbType.BigInt,8) ,
                        new SqlParameter("@CName", SqlDbType.VarChar,100) ,
                        new SqlParameter("@SName", SqlDbType.VarChar,40) ,
                        new SqlParameter("@Shortcode", SqlDbType.VarChar,20) ,
                        new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,
                        new SqlParameter("@DeveCode", SqlDbType.VarChar,50) ,
                        new SqlParameter("@PinYinZiTou", SqlDbType.VarChar,50) ,
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,
                        new SqlParameter("@IsUse", SqlDbType.Bit,1) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@DataAddTime", SqlDbType.DateTime) 
            };

            parameters[0].Value = model.LabID;
            parameters[1].Value = model.NewsAreaId;
            parameters[2].Value = model.CName;
            parameters[3].Value = model.SName;
            parameters[4].Value = model.ShortCode;
            parameters[5].Value = model.StandCode;
            parameters[6].Value = model.DeveCode;
            parameters[7].Value = model.PinYinZiTou;
            parameters[8].Value = model.DispOrder;
            parameters[9].Value = model.IsUse;
            parameters[10].Value = model.Memo;
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
        public bool Delete(long NewsAreaId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from N_News_Area ");
            strSql.Append(" where NewsAreaId=@NewsAreaId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NewsAreaId", SqlDbType.BigInt,8)         };
            parameters[0].Value = NewsAreaId;


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
        public Model.N_News_Area GetModel(long NewsAreaId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LabID, NewsAreaId, CName, SName, Shortcode, StandCode, DeveCode, PinYinZiTou, DispOrder, IsUse, Memo, DataAddTime  ");
            strSql.Append("  from N_News_Area ");
            strSql.Append(" where NewsAreaId=@NewsAreaId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NewsAreaId", SqlDbType.BigInt,8)         };
            parameters[0].Value = NewsAreaId;


            Model.N_News_Area model = new Model.N_News_Area();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(ds.Tables[0].Rows[0]["LabID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NewsAreaId"].ToString() != "")
                {
                    model.NewsAreaId = long.Parse(ds.Tables[0].Rows[0]["NewsAreaId"].ToString());
                }
                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                model.SName = ds.Tables[0].Rows[0]["SName"].ToString();
                model.ShortCode = ds.Tables[0].Rows[0]["Shortcode"].ToString();
                model.StandCode = ds.Tables[0].Rows[0]["StandCode"].ToString();
                model.DeveCode = ds.Tables[0].Rows[0]["DeveCode"].ToString();
                model.PinYinZiTou = ds.Tables[0].Rows[0]["PinYinZiTou"].ToString();
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
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
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
            strSql.Append(" FROM N_News_Area ");
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
            strSql.Append(" FROM N_News_Area ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetList(string where, int page, int limit, string Sort = "DispOrder asc ")
        {
            if (where == null || where.Trim() == "")
            {
                where = "1=1";
            }
            page = page < 1 ? 1 : page;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + limit + "  * from N_News_Area where NewsAreaId not in  ");
            strSql.Append("(select top " + (limit * (page - 1)) + " NewsAreaId from N_News_Area where 1=1 and " + where + " order by " + Sort + "  ) and " + where + " order by " + Sort + " ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public int GetCount(string where)
        {
            if (where == null || where.Trim() == "")
            {
                where = "1=1";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from N_News_Area where 1=1 and " + where + "  ");
            int count;
            if (int.TryParse(DbHelperSQL.ExecuteScalar(strSql.ToString()), out count))
            {
                return count;
            }
            else
            {
                return 0;
            }
        }
    }
}

