using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DBUtility;
using System.Data;

namespace ZhiFang.DAL.MsSql.Weblis
{
    /// <summary>
    /// 数据访问类:ReportItemFull
    /// </summary>
    public partial class ReportItemFull : BaseDALLisDB, IDReportItemFull
    {
        public ReportItemFull(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public ReportItemFull()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ReportItemID", "ReportItemFull");
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ReportFormID, string ReportItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ReportItemFull");
            strSql.Append(" where ReportFormID='" + ReportFormID + "' and ReportItemID=" + ReportItemID + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.ReportItemFull model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.ReportFormID != null)
            {
                strSql1.Append("ReportFormID,");
                strSql2.Append("'" + model.ReportFormID + "',");
            }
            if (model.ReportItemID != null)
            {
                strSql1.Append("ReportItemID,");
                strSql2.Append("" + model.ReportItemID + ",");
            }
            if (model.TESTITEMNAME != null)
            {
                strSql1.Append("TESTITEMNAME,");
                strSql2.Append("'" + model.TESTITEMNAME + "',");
            }
            if (model.TESTITEMSNAME != null)
            {
                strSql1.Append("TESTITEMSNAME,");
                strSql2.Append("'" + model.TESTITEMSNAME + "',");
            }
            if (model.RECEIVEDATE != null)
            {
                strSql1.Append("RECEIVEDATE,");
                strSql2.Append("'" + model.RECEIVEDATE + "',");
            }
            if (model.SECTIONNO != null)
            {
                strSql1.Append("SECTIONNO,");
                strSql2.Append("'" + model.SECTIONNO + "',");
            }
            if (model.TESTTYPENO != null)
            {
                strSql1.Append("TESTTYPENO,");
                strSql2.Append("'" + model.TESTTYPENO + "',");
            }
            if (model.SAMPLENO != null)
            {
                strSql1.Append("SAMPLENO,");
                strSql2.Append("'" + model.SAMPLENO + "',");
            }
            if (model.PARITEMNO != null)
            {
                strSql1.Append("PARITEMNO,");
                strSql2.Append("'" + model.PARITEMNO + "',");
            }
            if (model.ITEMNO != null)
            {
                strSql1.Append("ITEMNO,");
                strSql2.Append("'" + model.ITEMNO + "',");
            }
            if (model.ORIGINALVALUE != null)
            {
                strSql1.Append("ORIGINALVALUE,");
                strSql2.Append("'" + model.ORIGINALVALUE + "',");
            }
            if (model.REPORTVALUE != null)
            {
                strSql1.Append("REPORTVALUE,");
                strSql2.Append("'" + model.REPORTVALUE + "',");
            }
            if (model.ORIGINALDESC != null)
            {
                strSql1.Append("ORIGINALDESC,");
                strSql2.Append("'" + model.ORIGINALDESC + "',");
            }
            if (model.REPORTDESC != null)
            {
                strSql1.Append("REPORTDESC,");
                strSql2.Append("'" + model.REPORTDESC + "',");
            }
            if (model.STATUSNO != null)
            {
                strSql1.Append("STATUSNO,");
                strSql2.Append("'" + model.STATUSNO + "',");
            }
            if (model.EQUIPNO != null)
            {
                strSql1.Append("EQUIPNO,");
                strSql2.Append("'" + model.EQUIPNO + "',");
            }
            if (model.MODIFIED != null)
            {
                strSql1.Append("MODIFIED,");
                strSql2.Append("'" + model.MODIFIED + "',");
            }
            if (model.REFRANGE != null)
            {
                strSql1.Append("REFRANGE,");
                strSql2.Append("'" + model.REFRANGE + "',");
            }
            if (model.ITEMDATE != null)
            {
                strSql1.Append("ITEMDATE,");
                strSql2.Append("'" + model.ITEMDATE + "',");
            }
            if (model.ITEMTIME != null)
            {
                strSql1.Append("ITEMTIME,");
                strSql2.Append("'" + model.ITEMTIME + "',");
            }
            if (model.ISMATCH != null)
            {
                strSql1.Append("ISMATCH,");
                strSql2.Append("'" + model.ISMATCH + "',");
            }
            if (model.RESULTSTATUS != null)
            {
                strSql1.Append("RESULTSTATUS,");
                strSql2.Append("'" + model.RESULTSTATUS + "',");
            }
            if (model.TESTITEMDATETIME != null)
            {
                strSql1.Append("TESTITEMDATETIME,");
                strSql2.Append("'" + model.TESTITEMDATETIME + "',");
            }
            if (model.REPORTVALUEALL != null)
            {
                strSql1.Append("REPORTVALUEALL,");
                strSql2.Append("'" + model.REPORTVALUEALL + "',");
            }
            if (model.PARITEMNAME != null)
            {
                strSql1.Append("PARITEMNAME,");
                strSql2.Append("'" + model.PARITEMNAME + "',");
            }
            if (model.PARITEMSNAME != null)
            {
                strSql1.Append("PARITEMSNAME,");
                strSql2.Append("'" + model.PARITEMSNAME + "',");
            }
            if (model.DISPORDER != null)
            {
                strSql1.Append("DISPORDER,");
                strSql2.Append("'" + model.DISPORDER + "',");
            }
            if (model.ITEMORDER != null)
            {
                strSql1.Append("ITEMORDER,");
                strSql2.Append("'" + model.ITEMORDER + "',");
            }
            if (model.UNIT != null)
            {
                strSql1.Append("UNIT,");
                strSql2.Append("'" + model.UNIT + "',");
            }
            if (model.SERIALNO != null)
            {
                strSql1.Append("SERIALNO,");
                strSql2.Append("'" + model.SERIALNO + "',");
            }
            if (model.ZDY1 != null)
            {
                strSql1.Append("ZDY1,");
                strSql2.Append("'" + model.ZDY1 + "',");
            }
            if (model.ZDY2 != null)
            {
                strSql1.Append("ZDY2,");
                strSql2.Append("'" + model.ZDY2 + "',");
            }
            if (model.ZDY3 != null)
            {
                strSql1.Append("ZDY3,");
                strSql2.Append("'" + model.ZDY3 + "',");
            }
            if (model.ZDY4 != null)
            {
                strSql1.Append("ZDY4,");
                strSql2.Append("'" + model.ZDY4 + "',");
            }
            if (model.ZDY5 != null)
            {
                strSql1.Append("ZDY5,");
                strSql2.Append("'" + model.ZDY5 + "',");
            }
            if (model.HISORDERNO != null)
            {
                strSql1.Append("HISORDERNO,");
                strSql2.Append("'" + model.HISORDERNO + "',");
            }
            if (model.FORMNO != null)
            {
                strSql1.Append("FORMNO,");
                strSql2.Append("" + model.FORMNO + ",");
            }
            strSql.Append("insert into ReportItemFull(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.ReportItemFull model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ReportItemFull set ");
            if (model.TESTITEMNAME != null)
            {
                strSql.Append("TESTITEMNAME='" + model.TESTITEMNAME + "',");
            }
            if (model.TESTITEMSNAME != null)
            {
                strSql.Append("TESTITEMSNAME='" + model.TESTITEMSNAME + "',");
            }
            if (model.RECEIVEDATE != null)
            {
                strSql.Append("RECEIVEDATE='" + model.RECEIVEDATE + "',");
            }
            if (model.SECTIONNO != null)
            {
                strSql.Append("SECTIONNO='" + model.SECTIONNO + "',");
            }
            if (model.TESTTYPENO != null)
            {
                strSql.Append("TESTTYPENO='" + model.TESTTYPENO + "',");
            }
            if (model.SAMPLENO != null)
            {
                strSql.Append("SAMPLENO='" + model.SAMPLENO + "',");
            }
            if (model.PARITEMNO != null)
            {
                strSql.Append("PARITEMNO='" + model.PARITEMNO + "',");
            }
            if (model.ITEMNO != null)
            {
                strSql.Append("ITEMNO='" + model.ITEMNO + "',");
            }
            if (model.ORIGINALVALUE != null)
            {
                strSql.Append("ORIGINALVALUE='" + model.ORIGINALVALUE + "',");
            }
            if (model.REPORTVALUE != null)
            {
                strSql.Append("REPORTVALUE='" + model.REPORTVALUE + "',");
            }
            if (model.ORIGINALDESC != null)
            {
                strSql.Append("ORIGINALDESC='" + model.ORIGINALDESC + "',");
            }
            if (model.REPORTDESC != null)
            {
                strSql.Append("REPORTDESC='" + model.REPORTDESC + "',");
            }
            if (model.STATUSNO != null)
            {
                strSql.Append("STATUSNO='" + model.STATUSNO + "',");
            }
            if (model.EQUIPNO != null)
            {
                strSql.Append("EQUIPNO='" + model.EQUIPNO + "',");
            }
            if (model.MODIFIED != null)
            {
                strSql.Append("MODIFIED='" + model.MODIFIED + "',");
            }
            if (model.REFRANGE != null)
            {
                strSql.Append("REFRANGE='" + model.REFRANGE + "',");
            }
            if (model.ITEMDATE != null)
            {
                strSql.Append("ITEMDATE='" + model.ITEMDATE + "',");
            }
            if (model.ITEMTIME != null)
            {
                strSql.Append("ITEMTIME='" + model.ITEMTIME + "',");
            }
            if (model.ISMATCH != null)
            {
                strSql.Append("ISMATCH='" + model.ISMATCH + "',");
            }
            if (model.RESULTSTATUS != null)
            {
                strSql.Append("RESULTSTATUS='" + model.RESULTSTATUS + "',");
            }
            if (model.TESTITEMDATETIME != null)
            {
                strSql.Append("TESTITEMDATETIME='" + model.TESTITEMDATETIME + "',");
            }
            if (model.REPORTVALUEALL != null)
            {
                strSql.Append("REPORTVALUEALL='" + model.REPORTVALUEALL + "',");
            }
            if (model.PARITEMNAME != null)
            {
                strSql.Append("PARITEMNAME='" + model.PARITEMNAME + "',");
            }
            if (model.PARITEMSNAME != null)
            {
                strSql.Append("PARITEMSNAME='" + model.PARITEMSNAME + "',");
            }
            if (model.DISPORDER != null)
            {
                strSql.Append("DISPORDER='" + model.DISPORDER + "',");
            }
            if (model.ITEMORDER != null)
            {
                strSql.Append("ITEMORDER='" + model.ITEMORDER + "',");
            }
            if (model.UNIT != null)
            {
                strSql.Append("UNIT='" + model.UNIT + "',");
            }
            if (model.SERIALNO != null)
            {
                strSql.Append("SERIALNO='" + model.SERIALNO + "',");
            }
            if (model.ZDY1 != null)
            {
                strSql.Append("ZDY1='" + model.ZDY1 + "',");
            }
            if (model.ZDY2 != null)
            {
                strSql.Append("ZDY2='" + model.ZDY2 + "',");
            }
            if (model.ZDY3 != null)
            {
                strSql.Append("ZDY3='" + model.ZDY3 + "',");
            }
            if (model.ZDY4 != null)
            {
                strSql.Append("ZDY4='" + model.ZDY4 + "',");
            }
            if (model.ZDY5 != null)
            {
                strSql.Append("ZDY5='" + model.ZDY5 + "',");
            }
            if (model.HISORDERNO != null)
            {
                strSql.Append("HISORDERNO='" + model.HISORDERNO + "',");
            }
            if (model.FORMNO != null)
            {
                strSql.Append("FORMNO=" + model.FORMNO + ",");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where ReportFormID='" + model.ReportFormID + "' and ReportItemID=" + model.ReportItemID + " ");
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            return rowsAffected;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string ReportFormID, string ReportItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ReportItemFull ");
            strSql.Append(" where ReportFormID='" + ReportFormID + "' and ReportItemID=" + ReportItemID + " ");
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            return rowsAffected;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.ReportItemFull GetModel(string ReportFormID, string ReportItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" * ");
            strSql.Append(" from ReportItemFull ");
            strSql.Append(" where ReportFormID='" + ReportFormID + "' and ReportItemID=" + ReportItemID + " ");
            Model.ReportItemFull model = new Model.ReportItemFull();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ReportFormID"] != null && ds.Tables[0].Rows[0]["ReportFormID"].ToString() != "")
                {
                    model.ReportFormID = ds.Tables[0].Rows[0]["ReportFormID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ReportItemID"] != null && ds.Tables[0].Rows[0]["ReportItemID"].ToString() != "")
                {
                    model.ReportItemID = int.Parse(ds.Tables[0].Rows[0]["ReportItemID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TESTITEMNAME"] != null && ds.Tables[0].Rows[0]["TESTITEMNAME"].ToString() != "")
                {
                    model.TESTITEMNAME = ds.Tables[0].Rows[0]["TESTITEMNAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TESTITEMSNAME"] != null && ds.Tables[0].Rows[0]["TESTITEMSNAME"].ToString() != "")
                {
                    model.TESTITEMSNAME = ds.Tables[0].Rows[0]["TESTITEMSNAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RECEIVEDATE"] != null && ds.Tables[0].Rows[0]["RECEIVEDATE"].ToString() != "")
                {
                    model.RECEIVEDATE = DateTime.Parse(ds.Tables[0].Rows[0]["RECEIVEDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SECTIONNO"] != null && ds.Tables[0].Rows[0]["SECTIONNO"].ToString() != "")
                {
                    model.SECTIONNO = ds.Tables[0].Rows[0]["SECTIONNO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TESTTYPENO"] != null && ds.Tables[0].Rows[0]["TESTTYPENO"].ToString() != "")
                {
                    model.TESTTYPENO = ds.Tables[0].Rows[0]["TESTTYPENO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SAMPLENO"] != null && ds.Tables[0].Rows[0]["SAMPLENO"].ToString() != "")
                {
                    model.SAMPLENO = ds.Tables[0].Rows[0]["SAMPLENO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PARITEMNO"] != null && ds.Tables[0].Rows[0]["PARITEMNO"].ToString() != "")
                {
                    model.PARITEMNO = ds.Tables[0].Rows[0]["PARITEMNO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ITEMNO"] != null && ds.Tables[0].Rows[0]["ITEMNO"].ToString() != "")
                {
                    model.ITEMNO = ds.Tables[0].Rows[0]["ITEMNO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ORIGINALVALUE"] != null && ds.Tables[0].Rows[0]["ORIGINALVALUE"].ToString() != "")
                {
                    model.ORIGINALVALUE = ds.Tables[0].Rows[0]["ORIGINALVALUE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["REPORTVALUE"] != null && ds.Tables[0].Rows[0]["REPORTVALUE"].ToString() != "")
                {
                    model.REPORTVALUE = ds.Tables[0].Rows[0]["REPORTVALUE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ORIGINALDESC"] != null && ds.Tables[0].Rows[0]["ORIGINALDESC"].ToString() != "")
                {
                    model.ORIGINALDESC = ds.Tables[0].Rows[0]["ORIGINALDESC"].ToString();
                }
                if (ds.Tables[0].Rows[0]["REPORTDESC"] != null && ds.Tables[0].Rows[0]["REPORTDESC"].ToString() != "")
                {
                    model.REPORTDESC = ds.Tables[0].Rows[0]["REPORTDESC"].ToString();
                }
                if (ds.Tables[0].Rows[0]["STATUSNO"] != null && ds.Tables[0].Rows[0]["STATUSNO"].ToString() != "")
                {
                    model.STATUSNO = ds.Tables[0].Rows[0]["STATUSNO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EQUIPNO"] != null && ds.Tables[0].Rows[0]["EQUIPNO"].ToString() != "")
                {
                    model.EQUIPNO = ds.Tables[0].Rows[0]["EQUIPNO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MODIFIED"] != null && ds.Tables[0].Rows[0]["MODIFIED"].ToString() != "")
                {
                    model.MODIFIED = ds.Tables[0].Rows[0]["MODIFIED"].ToString();
                }
                if (ds.Tables[0].Rows[0]["REFRANGE"] != null && ds.Tables[0].Rows[0]["REFRANGE"].ToString() != "")
                {
                    model.REFRANGE = ds.Tables[0].Rows[0]["REFRANGE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ITEMDATE"] != null && ds.Tables[0].Rows[0]["ITEMDATE"].ToString() != "")
                {
                    model.ITEMDATE = DateTime.Parse(ds.Tables[0].Rows[0]["ITEMDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ITEMTIME"] != null && ds.Tables[0].Rows[0]["ITEMTIME"].ToString() != "")
                {
                    model.ITEMTIME = DateTime.Parse(ds.Tables[0].Rows[0]["ITEMTIME"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ISMATCH"] != null && ds.Tables[0].Rows[0]["ISMATCH"].ToString() != "")
                {
                    model.ISMATCH = ds.Tables[0].Rows[0]["ISMATCH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RESULTSTATUS"] != null && ds.Tables[0].Rows[0]["RESULTSTATUS"].ToString() != "")
                {
                    model.RESULTSTATUS = ds.Tables[0].Rows[0]["RESULTSTATUS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TESTITEMDATETIME"] != null && ds.Tables[0].Rows[0]["TESTITEMDATETIME"].ToString() != "")
                {
                    model.TESTITEMDATETIME = DateTime.Parse(ds.Tables[0].Rows[0]["TESTITEMDATETIME"].ToString());
                }
                if (ds.Tables[0].Rows[0]["REPORTVALUEALL"] != null && ds.Tables[0].Rows[0]["REPORTVALUEALL"].ToString() != "")
                {
                    model.REPORTVALUEALL = ds.Tables[0].Rows[0]["REPORTVALUEALL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PARITEMNAME"] != null && ds.Tables[0].Rows[0]["PARITEMNAME"].ToString() != "")
                {
                    model.PARITEMNAME = ds.Tables[0].Rows[0]["PARITEMNAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PARITEMSNAME"] != null && ds.Tables[0].Rows[0]["PARITEMSNAME"].ToString() != "")
                {
                    model.PARITEMSNAME = ds.Tables[0].Rows[0]["PARITEMSNAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DISPORDER"] != null && ds.Tables[0].Rows[0]["DISPORDER"].ToString() != "")
                {
                    model.DISPORDER = ds.Tables[0].Rows[0]["DISPORDER"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ITEMORDER"] != null && ds.Tables[0].Rows[0]["ITEMORDER"].ToString() != "")
                {
                    model.ITEMORDER = ds.Tables[0].Rows[0]["ITEMORDER"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UNIT"] != null && ds.Tables[0].Rows[0]["UNIT"].ToString() != "")
                {
                    model.UNIT = ds.Tables[0].Rows[0]["UNIT"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SERIALNO"] != null && ds.Tables[0].Rows[0]["SERIALNO"].ToString() != "")
                {
                    model.SERIALNO = ds.Tables[0].Rows[0]["SERIALNO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZDY1"] != null && ds.Tables[0].Rows[0]["ZDY1"].ToString() != "")
                {
                    model.ZDY1 = ds.Tables[0].Rows[0]["ZDY1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZDY2"] != null && ds.Tables[0].Rows[0]["ZDY2"].ToString() != "")
                {
                    model.ZDY2 = ds.Tables[0].Rows[0]["ZDY2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZDY3"] != null && ds.Tables[0].Rows[0]["ZDY3"].ToString() != "")
                {
                    model.ZDY3 = ds.Tables[0].Rows[0]["ZDY3"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZDY4"] != null && ds.Tables[0].Rows[0]["ZDY4"].ToString() != "")
                {
                    model.ZDY4 = ds.Tables[0].Rows[0]["ZDY4"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZDY5"] != null && ds.Tables[0].Rows[0]["ZDY5"].ToString() != "")
                {
                    model.ZDY5 = ds.Tables[0].Rows[0]["ZDY5"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HISORDERNO"] != null && ds.Tables[0].Rows[0]["HISORDERNO"].ToString() != "")
                {
                    model.HISORDERNO = ds.Tables[0].Rows[0]["HISORDERNO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FORMNO"] != null && ds.Tables[0].Rows[0]["FORMNO"].ToString() != "")
                {
                    model.FORMNO = int.Parse(ds.Tables[0].Rows[0]["FORMNO"].ToString());
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
            strSql.Append(" FROM ReportItemFull ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetColumns()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select top 1 * from ReportItemFull ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > -1)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM ReportItemFull ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (filedOrder == null)
            { }
            else
            {
                strSql.Append(" order by " + filedOrder);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /*
        */

        public DataSet GetList(ZhiFang.Model.ReportItemFull model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM ReportItemFull where 1=1");
                if (model.TESTITEMNAME != null)
                {
                    strSql.Append(" and TESTITEMNAME='" + model.TESTITEMNAME + "' ");
                }
                if (model.TESTITEMSNAME != null)
                {
                    strSql.Append(" and TESTITEMSNAME='" + model.TESTITEMSNAME + "' ");
                }
                if (model.RECEIVEDATE != null)
                {
                    strSql.Append(" and RECEIVEDATE='" + model.RECEIVEDATE + "' ");
                }
                if (model.SECTIONNO != null)
                {
                    strSql.Append(" and SECTIONNO='" + model.SECTIONNO + "' ");
                }
                if (model.TESTTYPENO != null)
                {
                    strSql.Append(" and TESTTYPENO='" + model.TESTTYPENO + "' ");
                }
                if (model.SAMPLENO != null)
                {
                    strSql.Append(" and SAMPLENO='" + model.SAMPLENO + "' ");
                }
                if (model.PARITEMNO != null)
                {
                    strSql.Append(" and PARITEMNO='" + model.PARITEMNO + "' ");
                }
                if (model.ITEMNO != null)
                {
                    strSql.Append(" and ITEMNO='" + model.ITEMNO + "' ");
                }
                if (model.ORIGINALVALUE != null)
                {
                    strSql.Append(" and ORIGINALVALUE='" + model.ORIGINALVALUE + "' ");
                }
                if (model.REPORTVALUE != null)
                {
                    strSql.Append(" and REPORTVALUE='" + model.REPORTVALUE + "' ");
                }
                if (model.ORIGINALDESC != null)
                {
                    strSql.Append(" and ORIGINALDESC='" + model.ORIGINALDESC + "' ");
                }
                if (model.REPORTDESC != null)
                {
                    strSql.Append(" and REPORTDESC='" + model.REPORTDESC + "' ");
                }
                if (model.STATUSNO != null)
                {
                    strSql.Append(" and STATUSNO='" + model.STATUSNO + "' ");
                }
                if (model.EQUIPNO != null)
                {
                    strSql.Append(" and EQUIPNO='" + model.EQUIPNO + "' ");
                }
                if (model.MODIFIED != null)
                {
                    strSql.Append(" and MODIFIED='" + model.MODIFIED + "' ");
                }
                if (model.REFRANGE != null)
                {
                    strSql.Append(" and REFRANGE='" + model.REFRANGE + "' ");
                }
                if (model.ITEMDATE != null)
                {
                    strSql.Append(" and ITEMDATE='" + model.ITEMDATE + "' ");
                }
                if (model.ITEMTIME != null)
                {
                    strSql.Append(" and ITEMTIME='" + model.ITEMTIME + "' ");
                }
                if (model.ISMATCH != null)
                {
                    strSql.Append(" and ISMATCH='" + model.ISMATCH + "' ");
                }
                if (model.RESULTSTATUS != null)
                {
                    strSql.Append(" and RESULTSTATUS='" + model.RESULTSTATUS + "' ");
                }
                if (model.TESTITEMDATETIME != null)
                {
                    strSql.Append(" and TESTITEMDATETIME='" + model.TESTITEMDATETIME + "' ");
                }
                if (model.REPORTVALUEALL != null)
                {
                    strSql.Append(" and REPORTVALUEALL='" + model.REPORTVALUEALL + "' ");
                }
                if (model.PARITEMNAME != null)
                {
                    strSql.Append(" and PARITEMNAME='" + model.PARITEMNAME + "' ");
                }
                if (model.PARITEMSNAME != null)
                {
                    strSql.Append(" and PARITEMSNAME='" + model.PARITEMSNAME + "' ");
                }
                if (model.DISPORDER != null)
                {
                    strSql.Append(" and DISPORDER='" + model.DISPORDER + "' ");
                }
                if (model.ITEMORDER != null)
                {
                    strSql.Append(" and ITEMORDER='" + model.ITEMORDER + "' ");
                }
                if (model.UNIT != null)
                {
                    strSql.Append(" and UNIT='" + model.UNIT + "' ");
                }
                if (model.SERIALNO != null)
                {
                    strSql.Append(" and SERIALNO='" + model.SERIALNO + "' ");
                }
                if (model.ZDY1 != null)
                {
                    strSql.Append(" and ZDY1='" + model.ZDY1 + "' ");
                }
                if (model.ZDY2 != null)
                {
                    strSql.Append(" and ZDY2='" + model.ZDY2 + "' ");
                }
                if (model.ZDY3 != null)
                {
                    strSql.Append(" and ZDY3='" + model.ZDY3 + "' ");
                }
                if (model.ZDY4 != null)
                {
                    strSql.Append(" and ZDY4='" + model.ZDY4 + "' ");
                }
                if (model.ZDY5 != null)
                {
                    strSql.Append(" and ZDY5='" + model.ZDY5 + "' ");
                }
                if (model.HISORDERNO != null)
                {
                    strSql.Append(" and HISORDERNO='" + model.HISORDERNO + "' ");
                }
                if (model.FORMNO != null)
                {
                    strSql.Append(" and FORMNO=" + model.FORMNO + " ");
                }
                if (model.ReportFormID != null)
                {
                    strSql.Append(" and ReportFormID='" + model.ReportFormID + "' ");
                }
                if (model.ReportItemID != null)
                {
                    strSql.Append(" and ReportItemID=" + model.ReportItemID + " ");
                }

                strSql.Append(" order by ITEMNO");
                //strSql.Append(" len( TESTITEMNAME) ");
                ZhiFang.Common.Log.Log.Info("报告项目信息" + strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("异常信息：" + e.ToString());
                return null;
            }
        }

        #endregion

        #region IDataBase<ReportItemFull> 成员


        public DataSet GetList(int Top, Model.ReportItemFull model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM ReportItemFull where 1=1");
            if (model.TESTITEMNAME != null)
            {
                strSql.Append(" and TESTITEMNAME='" + model.TESTITEMNAME + "' ");
            }
            if (model.TESTITEMSNAME != null)
            {
                strSql.Append(" and TESTITEMSNAME='" + model.TESTITEMSNAME + "' ");
            }
            if (model.RECEIVEDATE != null)
            {
                strSql.Append(" and RECEIVEDATE='" + model.RECEIVEDATE + "' ");
            }
            if (model.SECTIONNO != null)
            {
                strSql.Append(" and SECTIONNO='" + model.SECTIONNO + "' ");
            }
            if (model.TESTTYPENO != null)
            {
                strSql.Append(" and TESTTYPENO='" + model.TESTTYPENO + "' ");
            }
            if (model.SAMPLENO != null)
            {
                strSql.Append(" and SAMPLENO='" + model.SAMPLENO + "' ");
            }
            if (model.PARITEMNO != null)
            {
                strSql.Append(" and PARITEMNO='" + model.PARITEMNO + "' ");
            }
            if (model.ITEMNO != null)
            {
                strSql.Append(" and ITEMNO='" + model.ITEMNO + "' ");
            }
            if (model.ORIGINALVALUE != null)
            {
                strSql.Append(" and ORIGINALVALUE='" + model.ORIGINALVALUE + "' ");
            }
            if (model.REPORTVALUE != null)
            {
                strSql.Append(" and REPORTVALUE='" + model.REPORTVALUE + "' ");
            }
            if (model.ORIGINALDESC != null)
            {
                strSql.Append(" and ORIGINALDESC='" + model.ORIGINALDESC + "' ");
            }
            if (model.REPORTDESC != null)
            {
                strSql.Append(" and REPORTDESC='" + model.REPORTDESC + "' ");
            }
            if (model.STATUSNO != null)
            {
                strSql.Append(" and STATUSNO='" + model.STATUSNO + "' ");
            }
            if (model.EQUIPNO != null)
            {
                strSql.Append(" and EQUIPNO='" + model.EQUIPNO + "' ");
            }
            if (model.MODIFIED != null)
            {
                strSql.Append(" and MODIFIED='" + model.MODIFIED + "' ");
            }
            if (model.REFRANGE != null)
            {
                strSql.Append(" and REFRANGE='" + model.REFRANGE + "' ");
            }
            if (model.ITEMDATE != null)
            {
                strSql.Append(" and ITEMDATE='" + model.ITEMDATE + "' ");
            }
            if (model.ITEMTIME != null)
            {
                strSql.Append(" and ITEMTIME='" + model.ITEMTIME + "' ");
            }
            if (model.ISMATCH != null)
            {
                strSql.Append(" and ISMATCH='" + model.ISMATCH + "' ");
            }
            if (model.RESULTSTATUS != null)
            {
                strSql.Append(" and RESULTSTATUS='" + model.RESULTSTATUS + "' ");
            }
            if (model.TESTITEMDATETIME != null)
            {
                strSql.Append(" and TESTITEMDATETIME='" + model.TESTITEMDATETIME + "' ");
            }
            if (model.REPORTVALUEALL != null)
            {
                strSql.Append(" and REPORTVALUEALL='" + model.REPORTVALUEALL + "' ");
            }
            if (model.PARITEMNAME != null)
            {
                strSql.Append(" and PARITEMNAME='" + model.PARITEMNAME + "' ");
            }
            if (model.PARITEMSNAME != null)
            {
                strSql.Append(" and PARITEMSNAME='" + model.PARITEMSNAME + "' ");
            }
            if (model.DISPORDER != null)
            {
                strSql.Append(" and DISPORDER='" + model.DISPORDER + "' ");
            }
            if (model.ITEMORDER != null)
            {
                strSql.Append(" and ITEMORDER='" + model.ITEMORDER + "' ");
            }
            if (model.UNIT != null)
            {
                strSql.Append(" and UNIT='" + model.UNIT + "' ");
            }
            if (model.SERIALNO != null)
            {
                strSql.Append(" and SERIALNO='" + model.SERIALNO + "' ");
            }
            if (model.ZDY1 != null)
            {
                strSql.Append(" and ZDY1='" + model.ZDY1 + "' ");
            }
            if (model.ZDY2 != null)
            {
                strSql.Append(" and ZDY2='" + model.ZDY2 + "' ");
            }
            if (model.ZDY3 != null)
            {
                strSql.Append(" and ZDY3='" + model.ZDY3 + "' ");
            }
            if (model.ZDY4 != null)
            {
                strSql.Append(" and ZDY4='" + model.ZDY4 + "' ");
            }
            if (model.ZDY5 != null)
            {
                strSql.Append(" and ZDY5='" + model.ZDY5 + "' ");
            }
            if (model.HISORDERNO != null)
            {
                strSql.Append(" and HISORDERNO='" + model.HISORDERNO + "' ");
            }
            if (model.FORMNO != null)
            {
                strSql.Append(" and FORMNO=" + model.FORMNO + " ");
            }
            if (model.ReportFormID != null)
            {
                strSql.Append(" and ReportFormID='" + model.ReportFormID + "' ");
            }
            if (model.ReportItemID != null)
            {
                strSql.Append(" and ReportItemID=" + model.ReportItemID + " ");
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from ReportItemFull");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM ReportItemFull");
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

        public int GetTotalCount(Model.ReportItemFull model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM ReportItemFull where 1=1 ");
            if (model.TESTITEMNAME != null)
            {
                strSql.Append(" and TESTITEMNAME='" + model.TESTITEMNAME + "' ");
            }
            if (model.TESTITEMSNAME != null)
            {
                strSql.Append(" and TESTITEMSNAME='" + model.TESTITEMSNAME + "' ");
            }
            if (model.RECEIVEDATE != null)
            {
                strSql.Append(" and RECEIVEDATE='" + model.RECEIVEDATE + "' ");
            }
            if (model.SECTIONNO != null)
            {
                strSql.Append(" and SECTIONNO='" + model.SECTIONNO + "' ");
            }
            if (model.TESTTYPENO != null)
            {
                strSql.Append(" and TESTTYPENO='" + model.TESTTYPENO + "' ");
            }
            if (model.SAMPLENO != null)
            {
                strSql.Append(" and SAMPLENO='" + model.SAMPLENO + "' ");
            }
            if (model.PARITEMNO != null)
            {
                strSql.Append(" and PARITEMNO='" + model.PARITEMNO + "' ");
            }
            if (model.ITEMNO != null)
            {
                strSql.Append(" and ITEMNO='" + model.ITEMNO + "' ");
            }
            if (model.ORIGINALVALUE != null)
            {
                strSql.Append(" and ORIGINALVALUE='" + model.ORIGINALVALUE + "' ");
            }
            if (model.REPORTVALUE != null)
            {
                strSql.Append(" and REPORTVALUE='" + model.REPORTVALUE + "' ");
            }
            if (model.ORIGINALDESC != null)
            {
                strSql.Append(" and ORIGINALDESC='" + model.ORIGINALDESC + "' ");
            }
            if (model.REPORTDESC != null)
            {
                strSql.Append(" and REPORTDESC='" + model.REPORTDESC + "' ");
            }
            if (model.STATUSNO != null)
            {
                strSql.Append(" and STATUSNO='" + model.STATUSNO + "' ");
            }
            if (model.EQUIPNO != null)
            {
                strSql.Append(" and EQUIPNO='" + model.EQUIPNO + "' ");
            }
            if (model.MODIFIED != null)
            {
                strSql.Append(" and MODIFIED='" + model.MODIFIED + "' ");
            }
            if (model.REFRANGE != null)
            {
                strSql.Append(" and REFRANGE='" + model.REFRANGE + "' ");
            }
            if (model.ITEMDATE != null)
            {
                strSql.Append(" and ITEMDATE='" + model.ITEMDATE + "' ");
            }
            if (model.ITEMTIME != null)
            {
                strSql.Append(" and ITEMTIME='" + model.ITEMTIME + "' ");
            }
            if (model.ISMATCH != null)
            {
                strSql.Append(" and ISMATCH='" + model.ISMATCH + "' ");
            }
            if (model.RESULTSTATUS != null)
            {
                strSql.Append(" and RESULTSTATUS='" + model.RESULTSTATUS + "' ");
            }
            if (model.TESTITEMDATETIME != null)
            {
                strSql.Append(" and TESTITEMDATETIME='" + model.TESTITEMDATETIME + "' ");
            }
            if (model.REPORTVALUEALL != null)
            {
                strSql.Append(" and REPORTVALUEALL='" + model.REPORTVALUEALL + "' ");
            }
            if (model.PARITEMNAME != null)
            {
                strSql.Append(" and PARITEMNAME='" + model.PARITEMNAME + "' ");
            }
            if (model.PARITEMSNAME != null)
            {
                strSql.Append(" and PARITEMSNAME='" + model.PARITEMSNAME + "' ");
            }
            if (model.DISPORDER != null)
            {
                strSql.Append(" and DISPORDER='" + model.DISPORDER + "' ");
            }
            if (model.ITEMORDER != null)
            {
                strSql.Append(" and ITEMORDER='" + model.ITEMORDER + "' ");
            }
            if (model.UNIT != null)
            {
                strSql.Append(" and UNIT='" + model.UNIT + "' ");
            }
            if (model.SERIALNO != null)
            {
                strSql.Append(" and SERIALNO='" + model.SERIALNO + "' ");
            }
            if (model.ZDY1 != null)
            {
                strSql.Append(" and ZDY1='" + model.ZDY1 + "' ");
            }
            if (model.ZDY2 != null)
            {
                strSql.Append(" and ZDY2='" + model.ZDY2 + "' ");
            }
            if (model.ZDY3 != null)
            {
                strSql.Append(" and ZDY3='" + model.ZDY3 + "' ");
            }
            if (model.ZDY4 != null)
            {
                strSql.Append(" and ZDY4='" + model.ZDY4 + "' ");
            }
            if (model.ZDY5 != null)
            {
                strSql.Append(" and ZDY5='" + model.ZDY5 + "' ");
            }
            if (model.PRINTTIMES != null)
            {
                if (model.PRINTTIMES == 0)
                {
                    strSql.Append(" and PRINTTIMES='" + model.PRINTTIMES + "' ");
                }
                else if (model.PRINTTIMES == 1)
                {
                    strSql.Append(" and PRINTTIMES!='" + model.PRINTTIMES + "' ");
                }

            }
            if (model.ZDY10 != null)
            {
                strSql.Append(" and ZDY10='" + model.ZDY10 + "' ");
            }
            if (model.HISORDERNO != null)
            {
                strSql.Append(" and HISORDERNO='" + model.HISORDERNO + "' ");
            }
            if (model.FORMNO != null)
            {
                strSql.Append(" and FORMNO=" + model.FORMNO + " ");
            }
            if (model.ReportFormID != null)
            {
                strSql.Append(" and ReportFormID='" + model.ReportFormID + "' ");
            }
            if (model.ReportItemID != null)
            {
                strSql.Append(" and ReportItemID=" + model.ReportItemID + " ");
            }
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

        public int AddUpdateByDataSet(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    int count = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        if (this.Exists(ds.Tables[0].Rows[i]["ReportFormID"].ToString().Trim(), ds.Tables[0].Rows[i]["ReportItemID"].ToString().Trim()))
                        {
                            count += this.UpdateByDataRow(dr);
                        }
                        else
                            count += this.AddByDataRow(dr);
                    }
                    if (count == ds.Tables[0].Rows.Count)
                        return 1;
                    else
                        return 0;
                }
                catch
                {
                    return 0;
                }
            }
            else
                return 1;
        }
        public int AddByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into ReportItemFull (");
                strSql.Append("ReportFormID,ReportItemID,TESTITEMNAME,TESTITEMSNAME,RECEIVEDATE,SECTIONNO,TESTTYPENO,SAMPLENO,PARITEMNO,ITEMNO,ORIGINALVALUE,REPORTVALUE,ORIGINALDESC,REPORTDESC,STATUSNO,EQUIPNO,MODIFIED,REFRANGE,ITEMDATE,ITEMTIME,ISMATCH,RESULTSTATUS,TESTITEMDATETIME,REPORTVALUEALL,PARITEMNAME,PARITEMSNAME,DISPORDER,ITEMORDER,UNIT,SERIALNO,ZDY1,ZDY2,ZDY3,ZDY4,ZDY5,HISORDERNO,FORMNO,TECHNICIAN,OLDSERIALNO");
                strSql.Append(") values (");
                strSql.Append("'" + dr["ReportFormID"].ToString().Trim() + "','" + dr["ReportItemID"].ToString().Trim() + "','" + dr["TESTITEMNAME"].ToString().Trim() + "','" + dr["TESTITEMSNAME"].ToString().Trim() + "','" + dr["RECEIVEDATE"].ToString().Trim() + "','" + dr["SECTIONNO"].ToString().Trim() + "','" + dr["TESTTYPENO"].ToString().Trim() + "','" + dr["SAMPLENO"].ToString().Trim() + "','" + dr["PARITEMNO"].ToString().Trim() + "','" + dr["ITEMNO"].ToString().Trim() + "','" + dr["ORIGINALVALUE"].ToString().Trim() + "','" + dr["REPORTVALUE"].ToString().Trim() + "','" + dr["ORIGINALDESC"].ToString().Trim() + "','" + dr["REPORTDESC"].ToString().Trim() + "','" + dr["STATUSNO"].ToString().Trim() + "','" + dr["EQUIPNO"].ToString().Trim() + "','" + dr["MODIFIED"].ToString().Trim() + "','" + dr["REFRANGE"].ToString().Trim() + "','" + dr["ITEMDATE"].ToString().Trim() + "','" + dr["ITEMTIME"].ToString().Trim() + "','" + dr["ISMATCH"].ToString().Trim() + "','" + dr["RESULTSTATUS"].ToString().Trim() + "','" + dr["TESTITEMDATETIME"].ToString().Trim() + "','" + dr["REPORTVALUEALL"].ToString().Trim() + "','" + dr["PARITEMNAME"].ToString().Trim() + "','" + dr["PARITEMSNAME"].ToString().Trim() + "','" + dr["DISPORDER"].ToString().Trim() + "','" + dr["ITEMORDER"].ToString().Trim() + "','" + dr["UNIT"].ToString().Trim() + "','" + dr["SERIALNO"].ToString().Trim() + "','" + dr["ZDY1"].ToString().Trim() + "','" + dr["ZDY2"].ToString().Trim() + "','" + dr["ZDY3"].ToString().Trim() + "','" + dr["ZDY4"].ToString().Trim() + "','" + dr["ZDY5"].ToString().Trim() + "','" + dr["HISORDERNO"].ToString().Trim() + "','" + dr["FORMNO"].ToString().Trim() + "','" + dr["TECHNICIAN"].ToString().Trim() + "','" + dr["OLDSERIALNO"].ToString().Trim() + "'");
                strSql.Append(") ");
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update ReportItemFull set ");

                strSql.Append(" ReportFormID = '" + dr["ReportFormID"].ToString().Trim() + "' , ");
                strSql.Append(" ReportItemID = '" + dr["ReportItemID"].ToString().Trim() + "' , ");
                strSql.Append(" TESTITEMNAME = '" + dr["TESTITEMNAME"].ToString().Trim() + "' , ");
                strSql.Append(" TESTITEMSNAME = '" + dr["TESTITEMSNAME"].ToString().Trim() + "' , ");
                strSql.Append(" RECEIVEDATE = '" + dr["RECEIVEDATE"].ToString().Trim() + "' , ");
                strSql.Append(" SECTIONNO = '" + dr["SECTIONNO"].ToString().Trim() + "' , ");
                strSql.Append(" TESTTYPENO = '" + dr["TESTTYPENO"].ToString().Trim() + "' , ");
                strSql.Append(" SAMPLENO = '" + dr["SAMPLENO"].ToString().Trim() + "' , ");
                strSql.Append(" PARITEMNO = '" + dr["PARITEMNO"].ToString().Trim() + "' , ");
                strSql.Append(" ITEMNO = '" + dr["ITEMNO"].ToString().Trim() + "' , ");
                strSql.Append(" ORIGINALVALUE = '" + dr["ORIGINALVALUE"].ToString().Trim() + "' , ");
                strSql.Append(" REPORTVALUE = '" + dr["REPORTVALUE"].ToString().Trim() + "' , ");
                strSql.Append(" ORIGINALDESC = '" + dr["ORIGINALDESC"].ToString().Trim() + "' , ");
                strSql.Append(" REPORTDESC = '" + dr["REPORTDESC"].ToString().Trim() + "' , ");
                strSql.Append(" STATUSNO = '" + dr["STATUSNO"].ToString().Trim() + "' , ");
                strSql.Append(" EQUIPNO = '" + dr["EQUIPNO"].ToString().Trim() + "' , ");
                strSql.Append(" MODIFIED = '" + dr["MODIFIED"].ToString().Trim() + "' , ");
                strSql.Append(" REFRANGE = '" + dr["REFRANGE"].ToString().Trim() + "' , ");
                strSql.Append(" ITEMDATE = '" + dr["ITEMDATE"].ToString().Trim() + "' , ");
                strSql.Append(" ITEMTIME = '" + dr["ITEMTIME"].ToString().Trim() + "' , ");
                strSql.Append(" ISMATCH = '" + dr["ISMATCH"].ToString().Trim() + "' , ");
                strSql.Append(" RESULTSTATUS = '" + dr["RESULTSTATUS"].ToString().Trim() + "' , ");
                strSql.Append(" TESTITEMDATETIME = '" + dr["TESTITEMDATETIME"].ToString().Trim() + "' , ");
                strSql.Append(" REPORTVALUEALL = '" + dr["REPORTVALUEALL"].ToString().Trim() + "' , ");
                strSql.Append(" PARITEMNAME = '" + dr["PARITEMNAME"].ToString().Trim() + "' , ");
                strSql.Append(" PARITEMSNAME = '" + dr["PARITEMSNAME"].ToString().Trim() + "' , ");
                strSql.Append(" DISPORDER = '" + dr["DISPORDER"].ToString().Trim() + "' , ");
                strSql.Append(" ITEMORDER = '" + dr["ITEMORDER"].ToString().Trim() + "' , ");
                strSql.Append(" UNIT = '" + dr["UNIT"].ToString().Trim() + "' , ");
                strSql.Append(" SERIALNO = '" + dr["SERIALNO"].ToString().Trim() + "' , ");
                strSql.Append(" ZDY1 = '" + dr["ZDY1"].ToString().Trim() + "' , ");
                strSql.Append(" ZDY2 = '" + dr["ZDY2"].ToString().Trim() + "' , ");
                strSql.Append(" ZDY3 = '" + dr["ZDY3"].ToString().Trim() + "' , ");
                strSql.Append(" ZDY4 = '" + dr["ZDY4"].ToString().Trim() + "' , ");
                strSql.Append(" ZDY5 = '" + dr["ZDY5"].ToString().Trim() + "' , ");
                strSql.Append(" HISORDERNO = '" + dr["HISORDERNO"].ToString().Trim() + "' , ");
                strSql.Append(" FORMNO = '" + dr["FORMNO"].ToString().Trim() + "' , ");
                strSql.Append(" TECHNICIAN = '" + dr["TECHNICIAN"].ToString().Trim() + "' , ");
                strSql.Append(" OLDSERIALNO = '" + dr["OLDSERIALNO"].ToString().Trim() + "'  ");
                strSql.Append(" where ReportFormID ='" + dr["ReportFormID"].ToString().Trim() + "' and ReportItemID ='" + dr["ReportItemID"].ToString().Trim() + "'  ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }

        #endregion



        public int DeleteByWhere(string Strwhere)
        {
            string str = "delete from ReportItemFull where " + Strwhere;
            return DbHelperSQL.ExecuteNonQuery(str);
        }
        
        public int BackUpReportItemFullByWhere(string Strwhere)
        {
            StringBuilder strb = new StringBuilder();
            //ZhiFang.Common.Log.Log.Debug("ReportItemFull_BackUp:");
          
            strb.Append(" insert into ReportItemFull_BackUp");
            strb.Append(" ([ReportFormID] ");
            strb.Append(" ,[ReportItemID]");
            strb.Append(" ,[TESTITEMNAME]");
            strb.Append(" ,[TESTITEMSNAME]");
            strb.Append("  ,[RECEIVEDATE]");
            strb.Append("  ,[SECTIONNO]");
            strb.Append(" ,[TESTTYPENO]");
            strb.Append("  ,[SAMPLENO]");
            strb.Append("  ,[PARITEMNO]");
            strb.Append("  ,[ITEMNO]");
            strb.Append("   ,[ORIGINALVALUE]");
            strb.Append("  ,[REPORTVALUE]");
            strb.Append("   ,[ORIGINALDESC]");
            strb.Append("   ,[REPORTDESC]");
            strb.Append("    ,[STATUSNO]");
            strb.Append("   ,[EQUIPNO]");
            strb.Append("   ,[MODIFIED]");
            strb.Append("  ,[REFRANGE]");
            strb.Append(" ,[ITEMDATE]");
            strb.Append("  ,[ITEMTIME]");
            strb.Append("  ,[ISMATCH]");
            strb.Append("   ,[RESULTSTATUS]");
            strb.Append(" ,[TESTITEMDATETIME]");
            strb.Append("   ,[REPORTVALUEALL]");
            strb.Append("   ,[PARITEMNAME]");
            strb.Append("   ,[PARITEMSNAME]");
            strb.Append("   ,[DISPORDER]");
            strb.Append("   ,[ITEMORDER]");
            strb.Append("   ,[UNIT]");
            strb.Append("  ,[SERIALNO]");
            strb.Append("  ,[ZDY1]");
            strb.Append("  ,[ZDY2]");
            strb.Append("   ,[ZDY3]");
            strb.Append(" ,[ZDY4]");
            strb.Append("  ,[ZDY5]");
            strb.Append("  ,[HISORDERNO]");
            strb.Append("  ,[FORMNO]");
            strb.Append("  ,[TECHNICIAN]");
            strb.Append("  ,[OLDSERIALNO]");
            strb.Append("   ,[PREC]");
            strb.Append("  ,[itemunit]");
            strb.Append("   ,[itemename]");
            strb.Append("  ,[secretgrade]");
            strb.Append("   ,[shortname]");
            strb.Append("  ,[shortcode]");
            strb.Append("  ,[cuegrade]");
            strb.Append("  ,[ZDY6]");
            strb.Append("  ,[ZDY7]");
            strb.Append(" ,[ZDY8]");
            strb.Append("   ,[ZDY9]");
            strb.Append("  ,[ZDY10]");
            strb.Append("    ,[curritemredo]");
            strb.Append("  ,[Barcode]");
            strb.Append("  ,[SectionName]");
            strb.Append("  ,[EquipName]");
            strb.Append("   ,[CheckType]");
            strb.Append("   ,[CheckTypeName]");
            strb.Append("   ,[ReportItemIndexID]");
            strb.Append("   ,[ReportFormIndexID])");
            strb.Append("select [ReportFormID]");
            strb.Append(" ,[ReportItemID]");
            strb.Append(" ,[TESTITEMNAME]");
            strb.Append(" ,[TESTITEMSNAME]");
            strb.Append("  ,[RECEIVEDATE]");
            strb.Append("  ,[SECTIONNO]");
            strb.Append(" ,[TESTTYPENO]");
            strb.Append("  ,[SAMPLENO]");
            strb.Append("  ,[PARITEMNO]");
            strb.Append("  ,[ITEMNO]");
            strb.Append("   ,[ORIGINALVALUE]");
            strb.Append("  ,[REPORTVALUE]");
            strb.Append("   ,[ORIGINALDESC]");
            strb.Append("   ,[REPORTDESC]");
            strb.Append("    ,[STATUSNO]");
            strb.Append("   ,[EQUIPNO]");
            strb.Append("   ,[MODIFIED]");
            strb.Append("  ,[REFRANGE]");
            strb.Append(" ,[ITEMDATE]");
            strb.Append("  ,[ITEMTIME]");
            strb.Append("  ,[ISMATCH]");
            strb.Append("   ,[RESULTSTATUS]");
            strb.Append(" ,[TESTITEMDATETIME]");
            strb.Append("   ,[REPORTVALUEALL]");
            strb.Append("   ,[PARITEMNAME]");
            strb.Append("   ,[PARITEMSNAME]");
            strb.Append("   ,[DISPORDER]");
            strb.Append("   ,[ITEMORDER]");
            strb.Append("   ,[UNIT]");
            strb.Append("  ,[SERIALNO]");
            strb.Append("  ,[ZDY1]");
            strb.Append("  ,[ZDY2]");
            strb.Append("   ,[ZDY3]");
            strb.Append(" ,[ZDY4]");
            strb.Append("  ,[ZDY5]");
            strb.Append("  ,[HISORDERNO]");
            strb.Append("  ,[FORMNO]");
            strb.Append("  ,[TECHNICIAN]");
            strb.Append("  ,[OLDSERIALNO]");
            strb.Append("   ,[PREC]");
            strb.Append("  ,[itemunit]");
            strb.Append("   ,[itemename]");
            strb.Append("  ,[secretgrade]");
            strb.Append("   ,[shortname]");
            strb.Append("  ,[shortcode]");
            strb.Append("  ,[cuegrade]");
            strb.Append("  ,[ZDY6]");
            strb.Append("  ,[ZDY7]");
            strb.Append(" ,[ZDY8]");
            strb.Append("   ,[ZDY9]");
            strb.Append("  ,[ZDY10]");
            strb.Append("    ,[curritemredo]");
            strb.Append("  ,[Barcode]");
            strb.Append("  ,[SectionName]");
            strb.Append("  ,[EquipName]");
            strb.Append("   ,[CheckType]");
            strb.Append("   ,[CheckTypeName]");
            strb.Append("   ,[ReportItemIndexID]");
            strb.Append("   ,[ReportFormIndexID]");
            strb.Append(" from  ReportItemFull where ");
            strb.Append(Strwhere);

            return DbHelperSQL.ExecuteNonQuery(strb.ToString());
        }
    }
}

