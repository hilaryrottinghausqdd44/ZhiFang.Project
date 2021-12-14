using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using System.Data.SqlClient;
using System.Data;
namespace ZhiFang.DAL.MsSql.Weblis
{
    public class B_PhysicalExamType : BaseDALLisDB, IDBPhysicalExamType
    {
        public B_PhysicalExamType(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_PhysicalExamType()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        public System.Data.DataSet GetList(Model.BPhysicalExamType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,CName,ShortCode,Visible,DispOrder,DataAddTime,DTimeStampe");
            strSql.Append(" FROM B_PhysicalExamType where 1=1");
            if (model != null)
            {
                if (model.Id.HasValue)
                    strSql.Append(" and Id=" + model.Id + "");
                if (model.CName != null)
                    strSql.Append(" and CName=" + model.CName + "");
                if (model.ShortCode != null)
                    strSql.Append(" and ShortCode=" + model.ShortCode + "");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public int Add(Model.BPhysicalExamType model)
        {
            if (!model.Id.HasValue)
                model.Id = BLL.Common.GUIDHelp.GetGUIDLong();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into B_PhysicalExamType(");
            strSql.Append("Id,CName,ShortCode,Visible,DispOrder,DataAddTime)");
            strSql.Append(" values (");
            strSql.Append("@Id,@CName,@ShortCode,@Visible,@DispOrder,@DataAddTime)");
            //strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                new SqlParameter("@Id", SqlDbType.BigInt,8),
                    new SqlParameter("@CName", SqlDbType.VarChar,200),
                    new SqlParameter("@ShortCode", SqlDbType.VarChar,50),
                new SqlParameter("@Visible", SqlDbType.Int,4),
                    new SqlParameter("@DispOrder", SqlDbType.Int,4),
            new SqlParameter("@DataAddTime", SqlDbType.DateTime,20)};

            parameters[0].Value = model.Id;
            parameters[1].Value = model.CName;
            parameters[2].Value = model.ShortCode;
            parameters[3].Value = model.Visible;
            parameters[4].Value = model.DispOrder;
            parameters[5].Value = DateTime.Now;

            object obj = DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);//.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int Update(Model.BPhysicalExamType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_PhysicalExamType set ");
            if (model.CName != null)
            {
                strSql.Append("CName='" + model.CName + "',");
            }
            if (model.ShortCode != null)
            {
                strSql.Append("ShortCode='" + model.ShortCode + "',");
            }
            if (model.Visible != null)
            {
                strSql.Append("Visible='" + model.Visible + "',");
            }
            if (model.DispOrder != null)
            {
                strSql.Append("DispOrder='" + model.DispOrder + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + " ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_PhysicalExamType");
            strSql.Append(" where Id=" + Id + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(long Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_PhysicalExamType ");
            strSql.Append(" where Id=" + Id + " ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        public System.Data.DataSet GetAllList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,CName,ShortCode,Visible,DispOrder,DataAddTime,DTimeStampe");
            strSql.Append(" FROM B_PhysicalExamType where 1=1");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            //throw new NotImplementedException();
        }

        public Model.BPhysicalExamType GetModel(long Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" Id,CName,ShortCode,Visible,DispOrder,DataAddTime,DTimeStampe");
            strSql.Append(" from B_PhysicalExamType ");
            strSql.Append(" where Id=" + Id + " ");
            Model.BPhysicalExamType model = new Model.BPhysicalExamType();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = long.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CName"].ToString() != "")
                {
                    model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShortCode"].ToString() != "")
                {
                    model.ShortCode = ds.Tables[0].Rows[0]["ShortCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Visible"].ToString() != "")
                {
                    model.Visible = int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
                }

                if (ds.Tables[0].Rows[0]["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DataAddTime"].ToString() != "")
                {
                    model.DataAddTime = DateTime.Parse(ds.Tables[0].Rows[0]["DataAddTime"].ToString());
                }
                if (ds.Tables[0].Columns.Contains("DTimeStampe") && ds.Tables[0].Rows[0]["DTimeStampe"].ToString() != "")
                {
                    System.Byte[] tmpdts = ds.Tables[0].Rows[0]["DTimeStampe"] as System.Byte[];
                    model.DTimeStampe = tmpdts;
                }
                return model;

            }
            else
                return null;
        }
        public int AddUpdateByDataSet(System.Data.DataSet ds)
        {
            return DbHelperSQL.GetMaxID("Id", "B_PhysicalExamType");
        }
        public System.Data.DataSet GetList(int Top, Model.BPhysicalExamType model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM B_PhysicalExamType ");
            
            if (model.CName != null)
            {

                strSql.Append(" and CName='" + model.CName + "' ");
            }

            if (model.ShortCode != null)
            {

                strSql.Append(" and ShortCode='" + model.ShortCode + "' ");
            }

            if (model.Visible != null)
            {
                strSql.Append(" and Visible=" + model.Visible + " ");
            }

            if (model.DispOrder != null)
            {
                strSql.Append(" and DispOrder=" + model.DispOrder + " ");
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_PhysicalExamType ");
            string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
        }

        public int GetTotalCount(Model.BPhysicalExamType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_PhysicalExamType where 1=1 ");

            string strLike = "";
            if (model != null && model.SearchLikeKey != null)
            {
                strLike = " and (CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
            }
            strSql.Append(strLike);
            string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
        }

        public System.Data.DataSet GetListByPage(Model.BPhysicalExamType model, int nowPageNum, int nowPageSize)
        {
            string strLike = "";

            StringBuilder strSql = new StringBuilder();
            if (model.SearchLikeKey != null)
            {
                strLike = " and ( CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
            }
            strSql.Append("select top " + nowPageSize + "  * from B_PhysicalExamType where Id not in  ");
            strSql.Append("(select top " + (nowPageSize * nowPageNum) + " Id from B_PhysicalExamType where 1=1 " + strLike + " order by " + model.OrderField + " desc ) " + strLike + " order by " + model.OrderField + " desc ");
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
            strSql.Append(" FROM B_PhysicalExamType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
                strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
    }
}
