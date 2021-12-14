using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.IDAL;

namespace ZhiFang.DAL.MsSql.Weblis
{
    //N_NewsAreaClientLink
    public class DNNewsAreaClientLink : BaseDALLisDB, IDNNewsAreaClientLink
    {
        public DNNewsAreaClientLink(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public DNNewsAreaClientLink()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.N_NewsAreaClientLink model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into N_NewsAreaClientLink(");
            strSql.Append("LabId,NewsAreaClientLinkId,NewsAreaId,NewsAreaName,ClientNo,ClientName,Memo,DispOrder,IsUse,DataAddTime,DataUpdateTime");
            strSql.Append(") values (");
            strSql.Append("@LabId,@NewsAreaClientLinkId,@NewsAreaId,@NewsAreaName,@ClientNo,@ClientName,@Memo,@DispOrder,@IsUse,@DataAddTime,@DataUpdateTime");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@LabId", SqlDbType.BigInt,8) ,
                        new SqlParameter("@NewsAreaClientLinkId", SqlDbType.BigInt,8) ,
                        new SqlParameter("@NewsAreaId", SqlDbType.BigInt,8) ,
                        new SqlParameter("@NewsAreaName", SqlDbType.VarChar,500) ,
                        new SqlParameter("@ClientNo", SqlDbType.BigInt,8) ,
                        new SqlParameter("@ClientName", SqlDbType.VarChar,500) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,
                        new SqlParameter("@IsUse", SqlDbType.Bit,1) ,
                        new SqlParameter("@DataAddTime", SqlDbType.DateTime) ,
                        new SqlParameter("@DataUpdateTime", SqlDbType.DateTime)

            };

            parameters[0].Value = model.LabId;
            parameters[1].Value = model.NewsAreaClientLinkId;
            parameters[2].Value = model.NewsAreaId;
            parameters[3].Value = model.NewsAreaName;
            parameters[4].Value = model.ClientNo;
            parameters[5].Value = model.ClientName;
            parameters[6].Value = model.Memo;
            parameters[7].Value = model.DispOrder;
            parameters[8].Value = model.IsUse;
            parameters[9].Value = model.DataAddTime;
            parameters[10].Value = model.DataUpdateTime;
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0;

        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long NewsAreaClientLinkId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from N_NewsAreaClientLink ");
            strSql.Append(" where NewsAreaClientLinkId=@NewsAreaClientLinkId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NewsAreaClientLinkId", SqlDbType.BigInt,8)
            };
            parameters[0].Value = NewsAreaClientLinkId;


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
        public Model.N_NewsAreaClientLink GetModel(long NewsAreaClientLinkId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LabId, NewsAreaClientLinkId, NewsAreaId, NewsAreaName, ClientNo, ClientName, Memo, DispOrder, IsUse, DataAddTime, DataUpdateTime  ");
            strSql.Append("  from N_NewsAreaClientLink ");
            strSql.Append(" where NewsAreaClientLinkId=@NewsAreaClientLinkId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NewsAreaClientLinkId", SqlDbType.BigInt,8)
            };
            parameters[0].Value = NewsAreaClientLinkId;


            Model.N_NewsAreaClientLink model = new Model.N_NewsAreaClientLink();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LabId"].ToString() != "")
                {
                    model.LabId = long.Parse(ds.Tables[0].Rows[0]["LabId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NewsAreaClientLinkId"].ToString() != "")
                {
                    model.NewsAreaClientLinkId = long.Parse(ds.Tables[0].Rows[0]["NewsAreaClientLinkId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NewsAreaId"].ToString() != "")
                {
                    model.NewsAreaId = long.Parse(ds.Tables[0].Rows[0]["NewsAreaId"].ToString());
                }
                model.NewsAreaName = ds.Tables[0].Rows[0]["NewsAreaName"].ToString();
                if (ds.Tables[0].Rows[0]["ClientNo"].ToString() != "")
                {
                    model.ClientNo = long.Parse(ds.Tables[0].Rows[0]["ClientNo"].ToString());
                }
                model.ClientName = ds.Tables[0].Rows[0]["ClientName"].ToString();
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
            strSql.Append(" FROM N_NewsAreaClientLink ");
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
            strSql.Append(" FROM N_NewsAreaClientLink ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetList(string where, int page, int limit, string Sort)
        {
            if (where == null || where.Trim() == "")
            {
                where = "1=1";
            }
            Sort = (Sort == null || Sort.Trim() == "") ? " ClientNo asc" : Sort;
            page = page < 1 ? 1 : page;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + limit + "  * from N_NewsAreaClientLink where NewsAreaClientLinkId not in  ");
            strSql.Append("(select top " + (limit * (page - 1)) + " NewsAreaClientLinkId from N_NewsAreaClientLink where 1=1 and " + where + " order by " + Sort + "  ) and " + where + " order by " + Sort + " ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public int GetCount(string where)
        {
            if (where == null || where.Trim() == "")
            {
                where = "1=1";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from N_NewsAreaClientLink where 1=1 and " + where + "  ");
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

        public DataSet GetNewsAreaIdUnSelectList(string NewsAreaId, string where, int page, int limit, string Sort)
        {
            string sqlwhere= "1=1";
            if (NewsAreaId != null && NewsAreaId.Trim() != "")
            {
                sqlwhere = " ClientNo not in (select ClientNo from N_NewsAreaClientLink where NewsAreaId="+ NewsAreaId + " ) ";
            }
            if (where != null && where.Trim() != "")
            {
                sqlwhere += "and " + where;
            }
            Sort = (Sort == null || Sort.Trim() == "") ? " ClientNo asc" : Sort;
            page = page < 1 ? 1 : page;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select top " + limit + "  * from CLIENTELE where ClIENTNO not in    ");
            strSql.Append("(select top " + (limit * (page - 1)) + " ClIENTNO from N_NewsAreaClientLink where 1=1 and "+ sqlwhere + " order by " + Sort + "  ) and " + sqlwhere + " order by " + Sort + " ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public int GetNewsAreaIdUnSelectListCount(string NewsAreaId,string where)
        {
            string sqlwhere = "1=1";
            if (NewsAreaId != null && NewsAreaId.Trim() != "")
            {
                sqlwhere = " ClientNo not in (select ClientNo from N_NewsAreaClientLink where NewsAreaId=" + NewsAreaId + " ) ";
            }
            if (where != null && where.Trim() != "")
            {
                sqlwhere += "and "+ where;
            }

            StringBuilder strSql = new StringBuilder();
          
            strSql.Append("select count(*) from CLIENTELE where 1=1 and " + sqlwhere + "  ");
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

