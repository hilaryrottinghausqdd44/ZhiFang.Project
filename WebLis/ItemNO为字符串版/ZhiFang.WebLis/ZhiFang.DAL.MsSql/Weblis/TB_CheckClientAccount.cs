using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace ZhiFang.DAL.MsSql.Weblis
{
    /// <summary>
    /// 数据访问类:TB_CheckClientAccount
    /// </summary>
    public partial class TB_CheckClientAccount : BaseDALLisDB, IDTB_CheckClientAccount
    {
        public TB_CheckClientAccount(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public TB_CheckClientAccount()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TB_CheckClientAccount");
            strSql.Append(" where id=" + id + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.TB_CheckClientAccount model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.id != null)
            {
                strSql1.Append("id,");
                strSql2.Append("" + model.id + ",");
            }
            if (model.monthname != null)
            {
                strSql1.Append("monthname,");
                strSql2.Append("'" + model.monthname + "',");
            }
            if (model.clientname != null)
            {
                strSql1.Append("clientname,");
                strSql2.Append("'" + model.clientname + "',");
            }
            if (model.status != null)
            {
                strSql1.Append("status,");
                strSql2.Append("'" + model.status + "',");
            }
            if (model.remark != null)
            {
                strSql1.Append("remark,");
                strSql2.Append("'" + model.remark + "',");
            }
            if (model.checkdate != null)
            {
                strSql1.Append("checkdate,");
                strSql2.Append("'" + model.checkdate + "',");
            }
            if (model.filepath != null)
            {
                strSql1.Append("filepath,");
                strSql2.Append("'" + model.filepath + "',");
            }
            if (model.createdate != null)
            {
                strSql1.Append("createdate,");
                strSql2.Append("'" + model.createdate + "',");
            }
            if (model.reply != null)
            {
                strSql1.Append("reply,");
                strSql2.Append("'" + model.reply + "',");
            }
            if (model.clientno != null)
            {
                strSql1.Append("clientno,");
                strSql2.Append("" + model.clientno + ",");
            }
            if (model.auditstatus != null)
            {
                strSql1.Append("auditstatus,");
                strSql2.Append("'" + model.auditstatus + "',");
            }
            if (model.downloadfile != null)
            {
                strSql1.Append("downloadfile,");
                strSql2.Append("'" + model.downloadfile + "',");
            }
            if (model.count != null)
            {
                strSql1.Append("count,");
                strSql2.Append("'" + model.count + "',");
            }
            if (model.sumprice != null)
            {
                strSql1.Append("sumprice,");
                strSql2.Append("'" + model.sumprice + "',");
            }
            if (model.filepathitem != null)
            {
                strSql1.Append("filepathitem,");
                strSql2.Append("'" + model.filepathitem + "',");
            }
            if (model.downloadfileitem != null)
            {
                strSql1.Append("downloadfileitem,");
                strSql2.Append("'" + model.downloadfileitem + "',");
            }
            strSql.Append("insert into TB_CheckClientAccount(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ZhiFang.Model.TB_CheckClientAccount model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_CheckClientAccount set ");
            if (model.id != null)
            {
                strSql.Append("id=" + model.id + ",");
            }
            if (model.monthname != null)
            {
                strSql.Append("monthname='" + model.monthname + "',");
            }
            if (model.clientname != null)
            {
                strSql.Append("clientname='" + model.clientname + "',");
            }
            if (model.status != null)
            {
                strSql.Append("status='" + model.status + "',");
            }
            if (model.remark != null)
            {
                strSql.Append("remark='" + model.remark + "',");
            }
            if (model.checkdate != null)
            {
                strSql.Append("checkdate='" + model.checkdate + "',");
            }
            if (model.filepath != null)
            {
                strSql.Append("filepath='" + model.filepath + "',");
            }
            if (model.createdate != null)
            {
                strSql.Append("createdate='" + model.createdate + "',");
            }
            if (model.reply != null)
            {
                strSql.Append("reply='" + model.reply + "',");
            }
            if (model.clientno != null)
            {
                strSql.Append("clientno=" + model.clientno + ",");
            }
            if (model.auditstatus != null)
            {
                strSql.Append("auditstatus='" + model.auditstatus + "',");
            }
            if (model.downloadfile != null)
            {
                strSql.Append("downloadfile='" + model.downloadfile + "',");
            }
            if (model.sumprice != null)
            {
                strSql.Append("sumprice='" + model.sumprice + "',");
            }
            if (model.filepathitem != null)
            {
                strSql.Append("filepathitem='" + model.filepathitem + "',");
            }
            if (model.downloadfileitem != null)
            {
                strSql.Append("downloadfileitem='" + model.downloadfileitem + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where id=" + model.id + "");
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
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_CheckClientAccount ");
            strSql.Append(" where id=" + id + "");
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }		/// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_CheckClientAccount ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
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
        public ZhiFang.Model.TB_CheckClientAccount GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" id,id,monthname,clientname,status,remark,checkdate,filepath,createdate,reply,clientno,auditstatus,downloadfile,count,sumprice,filepathitem,downloadfileitem ");
            strSql.Append(" from TB_CheckClientAccount ");
            strSql.Append(" where id=" + id + "");
            ZhiFang.Model.TB_CheckClientAccount model = new ZhiFang.Model.TB_CheckClientAccount();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.TB_CheckClientAccount DataRowToModel(DataRow row)
        {
            ZhiFang.Model.TB_CheckClientAccount model = new ZhiFang.Model.TB_CheckClientAccount();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["monthname"] != null)
                {
                    model.monthname = row["monthname"].ToString();
                }
                if (row["clientname"] != null)
                {
                    model.clientname = row["clientname"].ToString();
                }
                if (row["status"] != null)
                {
                    model.status = row["status"].ToString();
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
                }
                if (row["checkdate"] != null && row["checkdate"].ToString() != "")
                {
                    model.checkdate = DateTime.Parse(row["checkdate"].ToString());
                }
                if (row["filepath"] != null)
                {
                    model.filepath = row["filepath"].ToString();
                }
                if (row["createdate"] != null && row["createdate"].ToString() != "")
                {
                    model.createdate = DateTime.Parse(row["createdate"].ToString());
                }
                if (row["reply"] != null)
                {
                    model.reply = row["reply"].ToString();
                }
                if (row["clientno"] != null && row["clientno"].ToString() != "")
                {
                    model.clientno = row["clientno"].ToString();
                }
                if (row["auditstatus"] != null)
                {
                    model.auditstatus = row["auditstatus"].ToString();
                }
                if (row["downloadfile"] != null)
                {
                    model.downloadfile = row["downloadfile"].ToString();
                }
                if (row["count"] != null)
                {
                    model.count = row["count"].ToString();
                }
                if (row["sumprice"] != null)
                {
                    model.sumprice = row["sumprice"].ToString();
                }
                if (row["filepathitem"] != null)
                {
                    model.filepathitem = row["filepathitem"].ToString();
                }
                if (row["downloadfileitem"] != null)
                {
                    model.downloadfileitem = row["downloadfileitem"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,id,monthname,clientname,status,remark,checkdate,filepath,createdate,reply,clientno,auditstatus,downloadfile,count,sumprice,filepathitem,downloadfileitem ");
            strSql.Append(" FROM TB_CheckClientAccount ");
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
            strSql.Append(" id,id,monthname,clientname,status,remark,checkdate,filepath,createdate,reply,clientno,auditstatus,downloadfile,count,sumprice,filepathitem,downloadfileitem ");
            strSql.Append(" FROM TB_CheckClientAccount ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere, ZhiFang.Model.TB_CheckClientAccount model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM TB_CheckClientAccount ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" and " + strWhere);
            }
            if (model.monthname != null)
            {
                strSql.Append(" and monthname like ");
                strSql.Append("'%" + model.monthname + "%'");
            }
            if (model.clientname != null)
            {
                strSql.Append(" and clientname like ");
                strSql.Append("'%" + model.clientname + "%'");
            }
            if (model.status != null)
            {
                strSql.Append(" and status =");
                strSql.Append("'" + model.status + "'");
            }
            Common.Log.Log.Info("TB_CheckClientAccount GetRecordCount:" + strSql);
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, ZhiFang.Model.TB_CheckClientAccount model, int nowpagenum, int nowpagesize)
        {

            string strtop = "";
            if (nowpagesize != 0)
            {
                strtop = " top " + nowpagesize;
            }

            string strpage = "";
            if (nowpagenum <=1 )
            {
                nowpagenum = 0;

            }
            strpage = "top " + (nowpagesize * (nowpagenum-1));

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            if (nowpagenum > 0 && nowpagesize > 0)
            {

                if (!string.IsNullOrEmpty(strWhere.Trim()))
                {
                    strSql1.Append(" and " + strWhere);
                }
                if (model.monthname != null)
                {
                    strSql1.Append(" and monthname like ");
                    strSql1.Append("'%" + model.monthname + "%'");
                }
                if (model.clientname != null)
                {
                    strSql1.Append(" and clientname like ");
                    strSql1.Append("'%" + model.clientname + "%'");
                }
                if (model.status != null)
                {
                    strSql1.Append(" and status =");
                    strSql1.Append("'" + model.status + "'");
                }
                strSql.Append(" select  " + strtop + "  id ,monthname, clientname, status, remark, filepath,filepathitem,checkdate ,createdate, clientno, auditstatus ,downloadfile, sumprice from TB_CheckClientAccount where 1=1 ");
                if (nowpagesize > 1)
                {
                    strSql.Append(" and id not in ( ");
                    strSql.Append(" select  " + strpage + " id from TB_CheckClientAccount where 1=1 " + strSql1 + " order by id desc ) ");
                }
                strSql.Append("  " + strSql1 + " order by id desc ");

               
            }
            else
            {
                strSql.Append("SELECT top " + nowpagesize + " * FROM  TB_CheckClientAccount WHERE 1=1 ");
                if (!string.IsNullOrEmpty(strWhere.Trim()))
                {
                    strSql.Append(" and " + strWhere);
                }
                if (model.monthname != null)
                {
                    strSql.Append(" and monthname like ");
                    strSql.Append("'%" + model.monthname + "%'");
                }
                if (model.clientname != null)
                {
                    strSql.Append(" and clientname like ");
                    strSql.Append("'%" + model.clientname + "%'");
                }
                if (model.status != null)
                {
                    strSql.Append(" and status =");
                    strSql.Append("'" + model.status + "'");
                }
            }
            Common.Log.Log.Info("TB_CheckClientAccount GetListByPage:"+ strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

      
        

        /*
        */

        #endregion  Method
   
    }
}

