using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.IDAL;

namespace ZhiFang.DAL.MsSql.Weblis
{
    //N_NewsAreaLink
    public partial class DNNewsAreaLink : BaseDALLisDB, IDNNewsAreaLink
    {
        public DNNewsAreaLink(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public DNNewsAreaLink()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.N_NewsAreaLink model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into N_NewsAreaLink(");
            strSql.Append("LabID,NewsAreaLinkId,NewsAreaId,NewsAreaName,NewsId,NewsIdName,Memo,DispOrder,IsUse,DataAddTime,DataUpdateTime");
            strSql.Append(") values (");
            strSql.Append("@LabID,@NewsAreaLinkId,@NewsAreaId,@NewsAreaName,@NewsId,@NewsIdName,@Memo,@DispOrder,@IsUse,@DataAddTime,@DataUpdateTime");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@LabID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@NewsAreaLinkId", SqlDbType.BigInt,8) ,
                        new SqlParameter("@NewsAreaId", SqlDbType.BigInt,8) ,
                        new SqlParameter("@NewsAreaName", SqlDbType.VarChar,500) ,
                        new SqlParameter("@NewsId", SqlDbType.BigInt,8) ,
                        new SqlParameter("@NewsIdName", SqlDbType.VarChar,500) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,
                        new SqlParameter("@IsUse", SqlDbType.Bit,1) ,
                        new SqlParameter("@DataAddTime", SqlDbType.DateTime),
                        new SqlParameter("@DataUpdateTime", SqlDbType.DateTime)

            };

            parameters[0].Value = model.LabID;
            parameters[1].Value = model.NewsAreaLinkId;
            parameters[2].Value = model.NewsAreaId;
            parameters[3].Value = model.NewsAreaName;
            parameters[4].Value = model.NewsId;
            parameters[5].Value = model.NewsIdName;
            parameters[6].Value = model.Memo;
            parameters[7].Value = model.DispOrder;
            parameters[8].Value = model.IsUse;
            parameters[9].Value = model.DataAddTime;
            parameters[10].Value = model.DataUpdateTime;
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters)>0;

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long NewsAreaLinkId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from N_NewsAreaLink ");
            strSql.Append(" where NewsAreaLinkId=@NewsAreaLinkId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NewsAreaLinkId", SqlDbType.BigInt,8)   };
            parameters[0].Value = NewsAreaLinkId;

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
        public Model.N_NewsAreaLink GetModel(long NewsAreaLinkId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LabID, NewsAreaLinkId, NewsAreaId, NewsAreaName, NewsId, NewsIdName, Memo, DispOrder, IsUse, DataAddTime, DataUpdateTime  ");
            strSql.Append("  from N_NewsAreaLink ");
            strSql.Append(" where NewsAreaLinkId=@NewsAreaLinkId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NewsAreaLinkId", SqlDbType.BigInt,8)  };
            parameters[0].Value = NewsAreaLinkId;


            Model.N_NewsAreaLink model = new Model.N_NewsAreaLink();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(ds.Tables[0].Rows[0]["LabID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NewsAreaLinkId"].ToString() != "")
                {
                    model.NewsAreaLinkId = long.Parse(ds.Tables[0].Rows[0]["NewsAreaLinkId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NewsAreaId"].ToString() != "")
                {
                    model.NewsAreaId = long.Parse(ds.Tables[0].Rows[0]["NewsAreaId"].ToString());
                }
                model.NewsAreaName = ds.Tables[0].Rows[0]["NewsAreaName"].ToString();
                if (ds.Tables[0].Rows[0]["NewsId"].ToString() != "")
                {
                    model.NewsId = long.Parse(ds.Tables[0].Rows[0]["NewsId"].ToString());
                }
                model.NewsIdName = ds.Tables[0].Rows[0]["NewsIdName"].ToString();
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
            strSql.Append(" FROM N_NewsAreaLink ");
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
            strSql.Append(" FROM N_NewsAreaLink ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetList(string where, int page, int limit, string Sort )
        {
            if (where == null || where.Trim() == "")
            {
                where = "1=1";
            }
            Sort = (Sort == null || Sort.Trim() == "") ? " DispOrder asc" : Sort;
            page = page < 1 ? 1 : page;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + limit + "  * from N_NewsAreaLink where NewsAreaLinkId not in  ");
            strSql.Append("(select top " + (limit * (page - 1)) + " NewsAreaLinkId from N_NewsAreaLink where 1=1 and " + where + " order by " + Sort + "  ) and " + where + " order by " + Sort + " ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public int GetCount(string where)
        {
            if (where == null || where.Trim() == "")
            {
                where = "1=1";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from N_NewsAreaLink where 1=1 and " + where + "  ");
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

        public DataSet GetNewsAreaIdUnSelectList(string NewsId, int page, int limit, string Sort)
        {
            string where = "1=1";
            if (NewsId != null && NewsId.Trim() != "")
            {
                where = " NewsAreaId not in (select NewsAreaId from N_NewsAreaLink where NewsId=" + NewsId + " ) ";
            }
            Sort = (Sort == null || Sort.Trim() == "") ? " DispOrder asc" : Sort;
            page = page < 1 ? 1 : page;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select top " + limit + "  * from N_News_Area where NewsAreaId not in    ");
            strSql.Append("(select top " + (limit * (page - 1)) + " NewsAreaId from N_News_Area where 1=1 and " + where + " order by " + Sort + "  ) and " + where + " order by " + Sort + " ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public int GetNewsAreaIdUnSelectListCount(string NewsId)
        {
            string where = "1=1";
            if (NewsId != null && NewsId.Trim() != "")
            {
                where = " NewsAreaId not in (select NewsAreaId from N_NewsAreaLink where NewsId=" + NewsId + " ) ";
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

