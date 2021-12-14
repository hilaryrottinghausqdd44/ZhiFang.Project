using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.IDAL;

namespace ZhiFang.DAL.MsSql.Weblis
{
    //N_News_Type
    public  class DNNews_Type : BaseDALLisDB, IDNNews_Type
    {
        public DNNews_Type(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public DNNews_Type()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.N_News_Type model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into N_News_Type(");
            strSql.Append("TypeID,LabID,SName,Shortcode,PinYinZiTou,CName,DispOrder,IsUse,Memo,DataAddTime,StandCode,DeveCode");
            strSql.Append(") values (");
            strSql.Append("@TypeID,@LabID,@SName,@Shortcode,@PinYinZiTou,@CName,@DispOrder,@IsUse,@Memo,@DataAddTime,@StandCode,@DeveCode");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@TypeID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@LabID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@SName", SqlDbType.VarChar,40) ,
                        new SqlParameter("@Shortcode", SqlDbType.VarChar,20) ,
                        new SqlParameter("@PinYinZiTou", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CName", SqlDbType.VarChar,100) ,
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,
                        new SqlParameter("@IsUse", SqlDbType.Bit,1) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@DataAddTime", SqlDbType.DateTime) ,
                        new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,
                        new SqlParameter("@DeveCode", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.TypeID;
            parameters[1].Value = model.LabID;
            parameters[2].Value = model.SName;
            parameters[3].Value = model.ShortCode;
            parameters[4].Value = model.PinYinZiTou;
            parameters[5].Value = model.CName;
            parameters[6].Value = model.DispOrder;
            parameters[7].Value = model.IsUse;
            parameters[8].Value = model.Memo;
            parameters[9].Value = model.DataAddTime;
            parameters[10].Value = model.StandCode;
            parameters[11].Value = model.DeveCode;
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters)>0;

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.N_News_Type model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update N_News_Type set ");
            if (model.LabID != null)
            {
                strSql.Append("LabID=" + model.LabID + ",");
            }
            else
            {
                strSql.Append("LabID= null ,");
            }
            if (model.CName != null)
            {
                strSql.Append("CName='" + model.CName + "',");
            }
            if (model.SName != null)
            {
                strSql.Append("SName='" + model.SName + "',");
            }
            else
            {
                strSql.Append("SName= null ,");
            }
            if (model.ShortCode != null)
            {
                strSql.Append("Shortcode='" + model.ShortCode + "',");
            }
            else
            {
                strSql.Append("Shortcode= null ,");
            }
            if (model.PinYinZiTou != null)
            {
                strSql.Append("PinYinZiTou='" + model.PinYinZiTou + "',");
            }
            else
            {
                strSql.Append("PinYinZiTou= null ,");
            }
            if (model.DispOrder.HasValue)
            {
                strSql.Append("DispOrder=" + model.DispOrder + ",");
            }
            else
            {
                strSql.Append("DispOrder= null ,");
            }
            if (model.IsUse.HasValue)
            {
                strSql.Append("IsUse=" + (model.IsUse.Value ? 1 : 0) + ",");
            }
            else
            {
                strSql.Append("IsUse= null ,");
            }
            if (model.Memo != null)
            {
                strSql.Append("Memo='" + model.Memo + "',");
            }
            else
            {
                strSql.Append("Memo= null ,");
            }
            if (model.DataAddTime.HasValue)
            {
                strSql.Append("DataAddTime='" + model.DataAddTime + "',");
            }
            else
            {
                strSql.Append("DataAddTime= null ,");
            }
            if (model.StandCode != null)
            {
                strSql.Append("StandCode='" + model.StandCode + "',");
            }
            else
            {
                strSql.Append("StandCode= null ,");
            }
            if (model.DeveCode != null)
            {
                strSql.Append("DeveCode='" + model.DeveCode + "',");
            }
            else
            {
                strSql.Append("DeveCode= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where TypeID=" + model.TypeID + " ");
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
        public bool Delete(long TypeID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from N_News_Type ");
            strSql.Append(" where TypeID=@TypeID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@TypeID", SqlDbType.BigInt,8)         };
            parameters[0].Value = TypeID;


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
        public Model.N_News_Type GetModel(long TypeID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TypeID, LabID,  SName, Shortcode, PinYinZiTou, CName, DispOrder, IsUse, Memo, DataAddTime, DataTimeStamp, StandCode, DeveCode  ");
            strSql.Append("  from N_News_Type ");
            strSql.Append(" where TypeID=@TypeID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@TypeID", SqlDbType.BigInt,8)         };
            parameters[0].Value = TypeID;


            Model.N_News_Type model = new Model.N_News_Type();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["TypeID"].ToString() != "")
                {
                    model.TypeID = long.Parse(ds.Tables[0].Rows[0]["TypeID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(ds.Tables[0].Rows[0]["LabID"].ToString());
                }
                model.SName = ds.Tables[0].Rows[0]["SName"].ToString();
                model.ShortCode = ds.Tables[0].Rows[0]["Shortcode"].ToString();
                model.PinYinZiTou = ds.Tables[0].Rows[0]["PinYinZiTou"].ToString();
                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
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
                model.StandCode = ds.Tables[0].Rows[0]["StandCode"].ToString();
                model.DeveCode = ds.Tables[0].Rows[0]["DeveCode"].ToString();

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
            strSql.Append(" FROM N_News_Type ");
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
            strSql.Append(" FROM N_News_Type ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetList(string where, int page, int limit,string Sort= "DispOrder asc " )
        {
            if (where == null || where.Trim() == "")
            {
                where = "1=1";
            }
            page = page < 1 ? 1 : page;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + limit + "  * from N_News_Type where TypeID not in  ");
            strSql.Append("(select top " + (limit * (page-1)) + " TypeID from N_News_Type where 1=1 and " + where + " order by " + Sort + "  ) and " + where + " order by " + Sort + " ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public int GetCount(string where)
        {
            if (where == null || where.Trim() == "")
            {
                where = "1=1";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from N_News_Type where 1=1 and " + where + "  ");
            int count;
            if (int.TryParse(DbHelperSQL.ExecuteScalar(strSql.ToString()),out count))
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

