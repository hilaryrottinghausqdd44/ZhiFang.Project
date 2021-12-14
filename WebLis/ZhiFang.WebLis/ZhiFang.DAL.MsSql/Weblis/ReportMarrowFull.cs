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
    /// 数据访问类:ReportMarrowFull
    /// </summary>
    public partial class ReportMarrowFull : BaseDALLisDB, IDReportMarrowFull
    {
        public ReportMarrowFull(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public ReportMarrowFull()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ReportItemID", "ReportMarrowFull");
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ReportFormID, string ReportItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ReportMarrowFull");
            strSql.Append(" where ReportFormID='" + ReportFormID + "' and ReportItemID=" + ReportItemID + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.ReportMarrowFull model)
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
            if (model.ParItemNo != null)
            {
                strSql1.Append("ParItemNo,");
                strSql2.Append("" + model.ParItemNo + ",");
            }
            if (model.ItemNo != null)
            {
                strSql1.Append("ItemNo,");
                strSql2.Append("" + model.ItemNo + ",");
            }
            if (model.BloodNum != null)
            {
                strSql1.Append("BloodNum,");
                strSql2.Append("" + model.BloodNum + ",");
            }
            if (model.BloodPercent != null)
            {
                strSql1.Append("BloodPercent,");
                strSql2.Append("" + model.BloodPercent + ",");
            }
            if (model.MarrowNum != null)
            {
                strSql1.Append("MarrowNum,");
                strSql2.Append("" + model.MarrowNum + ",");
            }
            if (model.MarrowPercent != null)
            {
                strSql1.Append("MarrowPercent,");
                strSql2.Append("" + model.MarrowPercent + ",");
            }
            if (model.BloodDesc != null)
            {
                strSql1.Append("BloodDesc,");
                strSql2.Append("'" + model.BloodDesc + "',");
            }
            if (model.MarrowDesc != null)
            {
                strSql1.Append("MarrowDesc,");
                strSql2.Append("'" + model.MarrowDesc + "',");
            }
            if (model.StatusNo != null)
            {
                strSql1.Append("StatusNo,");
                strSql2.Append("" + model.StatusNo + ",");
            }
            if (model.RefRange != null)
            {
                strSql1.Append("RefRange,");
                strSql2.Append("'" + model.RefRange + "',");
            }
            if (model.EquipNo != null)
            {
                strSql1.Append("EquipNo,");
                strSql2.Append("" + model.EquipNo + ",");
            }
            if (model.IsCale != null)
            {
                strSql1.Append("IsCale,");
                strSql2.Append("" + model.IsCale + ",");
            }
            if (model.Modified != null)
            {
                strSql1.Append("Modified,");
                strSql2.Append("" + model.Modified + ",");
            }
            if (model.ItemDate != null)
            {
                strSql1.Append("ItemDate,");
                strSql2.Append("'" + model.ItemDate + "',");
            }
            if (model.ItemTime != null)
            {
                strSql1.Append("ItemTime,");
                strSql2.Append("'" + model.ItemTime + "',");
            }
            if (model.IsMatch != null)
            {
                strSql1.Append("IsMatch,");
                strSql2.Append("" + model.IsMatch + ",");
            }
            if (model.ResultStatus != null)
            {
                strSql1.Append("ResultStatus,");
                strSql2.Append("'" + model.ResultStatus + "',");
            }
            if (model.FormNo != null)
            {
                strSql1.Append("FormNo,");
                strSql2.Append("" + model.FormNo + ",");
            }
            strSql.Append("insert into ReportMarrowFull(");
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
        public int Update(ZhiFang.Model.ReportMarrowFull model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ReportMarrowFull set ");
            if (model.ParItemNo != null)
            {
                strSql.Append("ParItemNo=" + model.ParItemNo + ",");
            }
            if (model.ItemNo != null)
            {
                strSql.Append("ItemNo=" + model.ItemNo + ",");
            }
            if (model.BloodNum != null)
            {
                strSql.Append("BloodNum=" + model.BloodNum + ",");
            }
            if (model.BloodPercent != null)
            {
                strSql.Append("BloodPercent=" + model.BloodPercent + ",");
            }
            if (model.MarrowNum != null)
            {
                strSql.Append("MarrowNum=" + model.MarrowNum + ",");
            }
            if (model.MarrowPercent != null)
            {
                strSql.Append("MarrowPercent=" + model.MarrowPercent + ",");
            }
            if (model.BloodDesc != null)
            {
                strSql.Append("BloodDesc='" + model.BloodDesc + "',");
            }
            if (model.MarrowDesc != null)
            {
                strSql.Append("MarrowDesc='" + model.MarrowDesc + "',");
            }
            if (model.StatusNo != null)
            {
                strSql.Append("StatusNo=" + model.StatusNo + ",");
            }
            if (model.RefRange != null)
            {
                strSql.Append("RefRange='" + model.RefRange + "',");
            }
            if (model.EquipNo != null)
            {
                strSql.Append("EquipNo=" + model.EquipNo + ",");
            }
            if (model.IsCale != null)
            {
                strSql.Append("IsCale=" + model.IsCale + ",");
            }
            if (model.Modified != null)
            {
                strSql.Append("Modified=" + model.Modified + ",");
            }
            if (model.ItemDate != null)
            {
                strSql.Append("ItemDate='" + model.ItemDate + "',");
            }
            if (model.ItemTime != null)
            {
                strSql.Append("ItemTime='" + model.ItemTime + "',");
            }
            if (model.IsMatch != null)
            {
                strSql.Append("IsMatch=" + model.IsMatch + ",");
            }
            if (model.ResultStatus != null)
            {
                strSql.Append("ResultStatus='" + model.ResultStatus + "',");
            }
            if (model.FormNo != null)
            {
                strSql.Append("FormNo=" + model.FormNo + ",");
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
            strSql.Append("delete from ReportMarrowFull ");
            strSql.Append(" where ReportFormID='" + ReportFormID + "' and ReportItemID=" + ReportItemID + " ");
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            return rowsAffected;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.ReportMarrowFull GetModel(string ReportFormID, string ReportItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" * ");
            strSql.Append(" from ReportMarrowFull ");
            strSql.Append(" where ReportFormID='" + ReportFormID + "' and ReportItemID=" + ReportItemID + " ");
            Model.ReportMarrowFull model = new Model.ReportMarrowFull();
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
                if (ds.Tables[0].Rows[0]["ParItemNo"] != null && ds.Tables[0].Rows[0]["ParItemNo"].ToString() != "")
                {
                    model.ParItemNo = int.Parse(ds.Tables[0].Rows[0]["ParItemNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ItemNo"] != null && ds.Tables[0].Rows[0]["ItemNo"].ToString() != "")
                {
                    model.ItemNo = int.Parse(ds.Tables[0].Rows[0]["ItemNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BloodNum"] != null && ds.Tables[0].Rows[0]["BloodNum"].ToString() != "")
                {
                    model.BloodNum = int.Parse(ds.Tables[0].Rows[0]["BloodNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BloodPercent"] != null && ds.Tables[0].Rows[0]["BloodPercent"].ToString() != "")
                {
                    model.BloodPercent = decimal.Parse(ds.Tables[0].Rows[0]["BloodPercent"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MarrowNum"] != null && ds.Tables[0].Rows[0]["MarrowNum"].ToString() != "")
                {
                    model.MarrowNum = int.Parse(ds.Tables[0].Rows[0]["MarrowNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MarrowPercent"] != null && ds.Tables[0].Rows[0]["MarrowPercent"].ToString() != "")
                {
                    model.MarrowPercent = decimal.Parse(ds.Tables[0].Rows[0]["MarrowPercent"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BloodDesc"] != null && ds.Tables[0].Rows[0]["BloodDesc"].ToString() != "")
                {
                    model.BloodDesc = ds.Tables[0].Rows[0]["BloodDesc"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MarrowDesc"] != null && ds.Tables[0].Rows[0]["MarrowDesc"].ToString() != "")
                {
                    model.MarrowDesc = ds.Tables[0].Rows[0]["MarrowDesc"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StatusNo"] != null && ds.Tables[0].Rows[0]["StatusNo"].ToString() != "")
                {
                    model.StatusNo = int.Parse(ds.Tables[0].Rows[0]["StatusNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RefRange"] != null && ds.Tables[0].Rows[0]["RefRange"].ToString() != "")
                {
                    model.RefRange = ds.Tables[0].Rows[0]["RefRange"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EquipNo"] != null && ds.Tables[0].Rows[0]["EquipNo"].ToString() != "")
                {
                    model.EquipNo = int.Parse(ds.Tables[0].Rows[0]["EquipNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsCale"] != null && ds.Tables[0].Rows[0]["IsCale"].ToString() != "")
                {
                    model.IsCale = int.Parse(ds.Tables[0].Rows[0]["IsCale"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Modified"] != null && ds.Tables[0].Rows[0]["Modified"].ToString() != "")
                {
                    model.Modified = int.Parse(ds.Tables[0].Rows[0]["Modified"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ItemDate"] != null && ds.Tables[0].Rows[0]["ItemDate"].ToString() != "")
                {
                    model.ItemDate = DateTime.Parse(ds.Tables[0].Rows[0]["ItemDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ItemTime"] != null && ds.Tables[0].Rows[0]["ItemTime"].ToString() != "")
                {
                    model.ItemTime = DateTime.Parse(ds.Tables[0].Rows[0]["ItemTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsMatch"] != null && ds.Tables[0].Rows[0]["IsMatch"].ToString() != "")
                {
                    model.IsMatch = int.Parse(ds.Tables[0].Rows[0]["IsMatch"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ResultStatus"] != null && ds.Tables[0].Rows[0]["ResultStatus"].ToString() != "")
                {
                    model.ResultStatus = ds.Tables[0].Rows[0]["ResultStatus"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FormNo"] != null && ds.Tables[0].Rows[0]["FormNo"].ToString() != "")
                {
                    model.FormNo = int.Parse(ds.Tables[0].Rows[0]["FormNo"].ToString());
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
            strSql.Append(" FROM ReportMarrowFull ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetColumns()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from ReportMarrowFull ");
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
            strSql.Append(" FROM ReportMarrowFull ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /*
        */

        public DataSet GetList(ZhiFang.Model.ReportMarrowFull model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM ReportMarrowFull where 1=1 ");
                if (model.ParItemNo != null)
                {
                    strSql.Append(" and ParItemNo=" + model.ParItemNo + " ");
                }
                if (model.ItemNo != null)
                {
                    strSql.Append(" and ItemNo=" + model.ItemNo + " ");
                }
                if (model.BloodNum != null)
                {
                    strSql.Append(" and BloodNum=" + model.BloodNum + " ");
                }
                if (model.BloodPercent != null)
                {
                    strSql.Append(" and BloodPercent=" + model.BloodPercent + " ");
                }
                if (model.MarrowNum != null)
                {
                    strSql.Append(" and MarrowNum=" + model.MarrowNum + " ");
                }
                if (model.MarrowPercent != null)
                {
                    strSql.Append(" and MarrowPercent=" + model.MarrowPercent + " ");
                }
                if (model.BloodDesc != null)
                {
                    strSql.Append(" and BloodDesc='" + model.BloodDesc + "' ");
                }
                if (model.MarrowDesc != null)
                {
                    strSql.Append(" and MarrowDesc='" + model.MarrowDesc + "' ");
                }
                if (model.StatusNo != null)
                {
                    strSql.Append(" and StatusNo=" + model.StatusNo + " ");
                }
                if (model.RefRange != null)
                {
                    strSql.Append(" and RefRange='" + model.RefRange + "' ");
                }
                if (model.EquipNo != null)
                {
                    strSql.Append(" and EquipNo=" + model.EquipNo + " ");
                }
                if (model.IsCale != null)
                {
                    strSql.Append(" and IsCale=" + model.IsCale + " ");
                }
                if (model.Modified != null)
                {
                    strSql.Append(" and Modified=" + model.Modified + " ");
                }
                if (model.ItemDate != null)
                {
                    strSql.Append(" and ItemDate='" + model.ItemDate + "' ");
                }
                if (model.ItemTime != null)
                {
                    strSql.Append(" and ItemTime='" + model.ItemTime + "' ");
                }
                if (model.IsMatch != null)
                {
                    strSql.Append(" and IsMatch=" + model.IsMatch + " ");
                }
                if (model.ResultStatus != null)
                {
                    strSql.Append(" and ResultStatus='" + model.ResultStatus + "' ");
                }
                if (model.FormNo != null)
                {
                    strSql.Append(" and FormNo=" + model.FormNo + " ");
                }
                if (model.ReportFormID != null)
                {
                    strSql.Append(" and  ReportFormID='" + model.ReportFormID + "' ");
                }
                if (model.ReportItemID != null)
                {
                    strSql.Append(" and  ReportItemID=" + model.ReportItemID + " ");
                }
                Common.Log.Log.Info("报告项目信息：" + strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            catch (Exception e)
            {
                Common.Log.Log.Debug("异常信息：" + e.ToString());
                return null;
            }
        }

        #endregion

        #region IDataBase<ReportMarrowFull> 成员


        public DataSet GetList(int Top, Model.ReportMarrowFull model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + Top.ToString() + " * ");
            strSql.Append(" FROM ReportMarrowFull where 1=1 ");
            if (model.ParItemNo != null)
            {
                strSql.Append(" and ParItemNo=" + model.ParItemNo + " ");
            }
            if (model.ItemNo != null)
            {
                strSql.Append(" and ItemNo=" + model.ItemNo + " ");
            }
            if (model.BloodNum != null)
            {
                strSql.Append(" and BloodNum=" + model.BloodNum + " ");
            }
            if (model.BloodPercent != null)
            {
                strSql.Append(" and BloodPercent=" + model.BloodPercent + " ");
            }
            if (model.MarrowNum != null)
            {
                strSql.Append(" and MarrowNum=" + model.MarrowNum + " ");
            }
            if (model.MarrowPercent != null)
            {
                strSql.Append(" and MarrowPercent=" + model.MarrowPercent + " ");
            }
            if (model.BloodDesc != null)
            {
                strSql.Append(" and BloodDesc='" + model.BloodDesc + "' ");
            }
            if (model.MarrowDesc != null)
            {
                strSql.Append(" and MarrowDesc='" + model.MarrowDesc + "' ");
            }
            if (model.StatusNo != null)
            {
                strSql.Append(" and StatusNo=" + model.StatusNo + " ");
            }
            if (model.RefRange != null)
            {
                strSql.Append(" and RefRange='" + model.RefRange + "' ");
            }
            if (model.EquipNo != null)
            {
                strSql.Append(" and EquipNo=" + model.EquipNo + " ");
            }
            if (model.IsCale != null)
            {
                strSql.Append(" and IsCale=" + model.IsCale + " ");
            }
            if (model.Modified != null)
            {
                strSql.Append(" and Modified=" + model.Modified + " ");
            }
            if (model.ItemDate != null)
            {
                strSql.Append(" and ItemDate='" + model.ItemDate + "' ");
            }
            if (model.ItemTime != null)
            {
                strSql.Append(" and ItemTime='" + model.ItemTime + "' ");
            }
            if (model.IsMatch != null)
            {
                strSql.Append(" and IsMatch=" + model.IsMatch + " ");
            }
            if (model.ResultStatus != null)
            {
                strSql.Append(" and ResultStatus='" + model.ResultStatus + "' ");
            }
            if (model.FormNo != null)
            {
                strSql.Append(" and FormNo=" + model.FormNo + " ");
            }
            if (model.ReportFormID != null)
            {
                strSql.Append(" and  ReportFormID='" + model.ReportFormID + "' ");
            }
            if (model.ReportItemID != null)
            {
                strSql.Append(" and  ReportItemID=" + model.ReportItemID + " ");
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ReportMarrowFull ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        #endregion

        #region IDataBase<ReportMarrowFull> 成员

        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM ReportMarrowFull");
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

        public int GetTotalCount(Model.ReportMarrowFull model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM ReportMarrowFull where 1=1 ");
            if (model.ParItemNo != null)
            {
                strSql.Append(" and ParItemNo=" + model.ParItemNo + " ");
            }
            if (model.ItemNo != null)
            {
                strSql.Append(" and ItemNo=" + model.ItemNo + " ");
            }
            if (model.BloodNum != null)
            {
                strSql.Append(" and BloodNum=" + model.BloodNum + " ");
            }
            if (model.BloodPercent != null)
            {
                strSql.Append(" and BloodPercent=" + model.BloodPercent + " ");
            }
            if (model.MarrowNum != null)
            {
                strSql.Append(" and MarrowNum=" + model.MarrowNum + " ");
            }
            if (model.MarrowPercent != null)
            {
                strSql.Append(" and MarrowPercent=" + model.MarrowPercent + " ");
            }
            if (model.BloodDesc != null)
            {
                strSql.Append(" and BloodDesc='" + model.BloodDesc + "' ");
            }
            if (model.MarrowDesc != null)
            {
                strSql.Append(" and MarrowDesc='" + model.MarrowDesc + "' ");
            }
            if (model.StatusNo != null)
            {
                strSql.Append(" and StatusNo=" + model.StatusNo + " ");
            }
            if (model.RefRange != null)
            {
                strSql.Append(" and RefRange='" + model.RefRange + "' ");
            }
            if (model.EquipNo != null)
            {
                strSql.Append(" and EquipNo=" + model.EquipNo + " ");
            }
            if (model.IsCale != null)
            {
                strSql.Append(" and IsCale=" + model.IsCale + " ");
            }
            if (model.Modified != null)
            {
                strSql.Append(" and Modified=" + model.Modified + " ");
            }
            if (model.ItemDate != null)
            {
                strSql.Append(" and ItemDate='" + model.ItemDate + "' ");
            }
            if (model.ItemTime != null)
            {
                strSql.Append(" and ItemTime='" + model.ItemTime + "' ");
            }
            if (model.IsMatch != null)
            {
                strSql.Append(" and IsMatch=" + model.IsMatch + " ");
            }
            if (model.ResultStatus != null)
            {
                strSql.Append(" and ResultStatus='" + model.ResultStatus + "' ");
            }
            if (model.FormNo != null)
            {
                strSql.Append(" and FormNo=" + model.FormNo + " ");
            }
            if (model.ReportFormID != null)
            {
                strSql.Append(" and  ReportFormID='" + model.ReportFormID + "' ");
            }
            if (model.ReportItemID != null)
            {
                strSql.Append(" and  ReportItemID=" + model.ReportItemID + " ");
            }
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

        #endregion

        #region IDataBase<ReportMarrowFull> 成员

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
                strSql.Append("insert into ReportMarrowFull (");
                strSql.Append("ReportFormID,ReportItemID,ParItemNo,ItemNo,BloodNum,BloodPercent,MarrowNum,MarrowPercent,BloodDesc,MarrowDesc,StatusNo,RefRange,EquipNo,IsCale,Modified,ItemDate,ItemTime,IsMatch,ResultStatus,FormNo,CItemNo,ReportText");
                strSql.Append(") values (");
                strSql.Append("'" + dr["ReportFormID"].ToString().Trim() + "','" + dr["ReportItemID"].ToString().Trim() + "','" + dr["ParItemNo"].ToString().Trim() + "','" + dr["ItemNo"].ToString().Trim() + "','" + dr["BloodNum"].ToString().Trim() + "','" + dr["BloodPercent"].ToString().Trim() + "','" + dr["MarrowNum"].ToString().Trim() + "','" + dr["MarrowPercent"].ToString().Trim() + "','" + dr["BloodDesc"].ToString().Trim() + "','" + dr["MarrowDesc"].ToString().Trim() + "','" + dr["StatusNo"].ToString().Trim() + "','" + dr["RefRange"].ToString().Trim() + "','" + dr["EquipNo"].ToString().Trim() + "','" + dr["IsCale"].ToString().Trim() + "','" + dr["Modified"].ToString().Trim() + "','" + dr["ItemDate"].ToString().Trim() + "','" + dr["ItemTime"].ToString().Trim() + "','" + dr["IsMatch"].ToString().Trim() + "','" + dr["ResultStatus"].ToString().Trim() + "','" + dr["FormNo"].ToString().Trim() + "','" + dr["CItemNo"].ToString().Trim() + "','" + dr["ReportText"].ToString().Trim() + "'");
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
                strSql.Append("update ReportMarrowFull set ");

                strSql.Append(" ReportFormID = '" + dr["ReportFormID"].ToString().Trim() + "' , ");
                strSql.Append(" ReportItemID = '" + dr["ReportItemID"].ToString().Trim() + "' , ");
                strSql.Append(" ParItemNo = '" + dr["ParItemNo"].ToString().Trim() + "' , ");
                strSql.Append(" ItemNo = '" + dr["ItemNo"].ToString().Trim() + "' , ");
                strSql.Append(" BloodNum = '" + dr["BloodNum"].ToString().Trim() + "' , ");
                strSql.Append(" BloodPercent = '" + dr["BloodPercent"].ToString().Trim() + "' , ");
                strSql.Append(" MarrowNum = '" + dr["MarrowNum"].ToString().Trim() + "' , ");
                strSql.Append(" MarrowPercent = '" + dr["MarrowPercent"].ToString().Trim() + "' , ");
                strSql.Append(" BloodDesc = '" + dr["BloodDesc"].ToString().Trim() + "' , ");
                strSql.Append(" MarrowDesc = '" + dr["MarrowDesc"].ToString().Trim() + "' , ");
                strSql.Append(" StatusNo = '" + dr["StatusNo"].ToString().Trim() + "' , ");
                strSql.Append(" RefRange = '" + dr["RefRange"].ToString().Trim() + "' , ");
                strSql.Append(" EquipNo = '" + dr["EquipNo"].ToString().Trim() + "' , ");
                strSql.Append(" IsCale = '" + dr["IsCale"].ToString().Trim() + "' , ");
                strSql.Append(" Modified = '" + dr["Modified"].ToString().Trim() + "' , ");
                strSql.Append(" ItemDate = '" + dr["ItemDate"].ToString().Trim() + "' , ");
                strSql.Append(" ItemTime = '" + dr["ItemTime"].ToString().Trim() + "' , ");
                strSql.Append(" IsMatch = '" + dr["IsMatch"].ToString().Trim() + "' , ");
                strSql.Append(" ResultStatus = '" + dr["ResultStatus"].ToString().Trim() + "' , ");
                strSql.Append(" FormNo = '" + dr["FormNo"].ToString().Trim() + "' , ");
                strSql.Append(" CItemNo = '" + dr["CItemNo"].ToString().Trim() + "' , ");
                strSql.Append(" ReportText = '" + dr["ReportText"].ToString().Trim() + "'  ");
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
            string str = "delete from ReportMarrowFull where " + Strwhere;
            return DbHelperSQL.ExecuteNonQuery(str);
        }

        public int BackUpReportMarrowFullByWhere(string Strwhere)
        {
            StringBuilder strb = new StringBuilder();

            strb.Append(" insert into dbo.ReportMarrowFull_BackUp ");
            strb.Append(" ([ReportFormID] ");
            strb.Append("      ,[ReportItemID] ");
            strb.Append("  ,[ParItemNo]");
            strb.Append(" ,[ItemNo]");
            strb.Append(" ,[BloodNum]");
            strb.Append("  ,[BloodPercent]");
            strb.Append("  ,[MarrowNum]");
            strb.Append("  ,[MarrowPercent]");
            strb.Append("  ,[BloodDesc]");
            strb.Append("  ,[MarrowDesc]");
            strb.Append("  ,[StatusNo]");
            strb.Append("  ,[RefRange]");
            strb.Append("  ,[EquipNo]");
            strb.Append("  ,[IsCale]");
            strb.Append("  ,[Modified]");
            strb.Append(" ,[ItemDate]");
            strb.Append(" ,[ItemTime]");
            strb.Append(" ,[IsMatch]");
            strb.Append(" ,[ResultStatus]");
            strb.Append(" ,[FormNo]");
            strb.Append("  ,[CItemNo]");
            strb.Append("  ,[ReportText]");
            strb.Append("  ,[OrgValue]");
            strb.Append("  ,[OrgDesc]");
            strb.Append("   ,[IsPrint]");
            strb.Append("   ,[PrintOrder]");
            strb.Append("  ,[itemname]");
            strb.Append("  ,[ValueTypeNo]");
            strb.Append("   ,[ReportValue]");
            strb.Append("   ,[ReportDesc]");
            strb.Append("    ,[ReportImage]");
            strb.Append("   ,[Barcode]");
            strb.Append("  ,[EquipName]");
            strb.Append("  ,[ReceiveDate]");
            strb.Append("  ,[SectionNo]");
            strb.Append("   ,[ItemCName]");
            strb.Append("  ,[ItemEName]");
            strb.Append("   ,[ParItemCName]");
            strb.Append("   ,[ParItemEName]");
            strb.Append("  ,[TestTypeNo]");
            strb.Append("  ,[SampleNo]");
            strb.Append("  ,[ReportFormIndexID])");
            strb.Append("select  [ReportFormID]");
            strb.Append("      ,[ReportItemID] ");
            strb.Append("  ,[ParItemNo]");
            strb.Append(" ,[ItemNo]");
            strb.Append(" ,[BloodNum]");
            strb.Append("  ,[BloodPercent]");
            strb.Append("  ,[MarrowNum]");
            strb.Append("  ,[MarrowPercent]");
            strb.Append("  ,[BloodDesc]");
            strb.Append("  ,[MarrowDesc]");
            strb.Append("  ,[StatusNo]");
            strb.Append("  ,[RefRange]");
            strb.Append("  ,[EquipNo]");
            strb.Append("  ,[IsCale]");
            strb.Append("  ,[Modified]");
            strb.Append(" ,[ItemDate]");
            strb.Append(" ,[ItemTime]");
            strb.Append(" ,[IsMatch]");
            strb.Append(" ,[ResultStatus]");
            strb.Append(" ,[FormNo]");
            strb.Append("  ,[CItemNo]");
            strb.Append("  ,[ReportText]");
            strb.Append("  ,[OrgValue]");
            strb.Append("  ,[OrgDesc]");
            strb.Append("   ,[IsPrint]");
            strb.Append("   ,[PrintOrder]");
            strb.Append("  ,[itemname]");
            strb.Append("  ,[ValueTypeNo]");
            strb.Append("   ,[ReportValue]");
            strb.Append("   ,[ReportDesc]");
            strb.Append("    ,[ReportImage]");
            strb.Append("   ,[Barcode]");
            strb.Append("  ,[EquipName]");
            strb.Append("  ,[ReceiveDate]");
            strb.Append("  ,[SectionNo]");
            strb.Append("   ,[ItemCName]");
            strb.Append("  ,[ItemEName]");
            strb.Append("   ,[ParItemCName]");
            strb.Append("   ,[ParItemEName]");
            strb.Append("  ,[TestTypeNo]");
            strb.Append("  ,[SampleNo]");
            strb.Append("  ,[ReportFormIndexID] ");

            strb.Append(" from ReportMarrowFull where ");
            strb.Append(Strwhere);

            return DbHelperSQL.ExecuteNonQuery(strb.ToString());
        }
        //public DataSet GetList(string strWhere)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select * ");
        //    strSql.Append(" FROM ReportMarrowFull ");
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql.Append(" where " + strWhere);
        //    }
        //    return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        //}
    }
}

