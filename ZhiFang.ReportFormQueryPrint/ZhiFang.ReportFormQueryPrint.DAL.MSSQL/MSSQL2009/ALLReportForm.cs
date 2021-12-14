using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.ReportFormQueryPrint.DBUtility;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL2009
{
    public class ALLReportForm : IDAL.IDALLReportForm
    {

        #region IDALLReportFrom 成员

        public DataTable GetMarrowItemList(string FormNo)
        {
            ReportMarrow fm = new ReportMarrow();
            return fm.GetReportMarrowList(FormNo);
        }
        public DataTable GetMicroItemList(string FormNo)
        {
            ReportMarrow fm = new ReportMarrow();
            return fm.GetReportMarrowList(FormNo);
        }

        public DataTable GetFromInfo(string FormNo)
        {
            ReportForm rf = new ReportForm();
            //return rf.GetReportFormList(new string[]{FormNo});
            return rf.GetReportFormFullList( FormNo );
        }
        public Model.ReportForm GetFromInfoModel(string FormNo)
        {
            ReportForm rf = new ReportForm();
            //return rf.GetReportFormList(new string[]{FormNo});
            return rf.GetModel(FormNo);
        }
        public DataTable GetFromItemList(string FormNo)
        {
            ReportItem ri = new ReportItem();
            //return ri.GetReportItemList(FormNo);
            return ri.GetReportItemFullList(FormNo);
        }


        public DataTable GetReportItemList(string FormNo)
        {
            ReportItem ri = new ReportItem();
            //return ri.GetReportItemList(FormNo);
            return ri.GetReportItemList(FormNo);
        }


        /// <summary>
        /// 历史对比
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetReportValue(string[] p)
        {
            return null;
        }
        public DataTable GetFromPGroupInfo(int SectionNo)
        {
            throw new NotImplementedException();
        }
        public DataTable GetFromGraphList(string FormNo)
        {
            Model.RFGraphData rfgd_m = new Model.RFGraphData();
            rfgd_m.FormNo = FormNo;
            return new RFGraphData().GetList(rfgd_m).Tables[0];
        }
        #endregion

        #region IDALLReportForm 成员


        public DataTable GetMicroItemGroupList(string FormNo)
        {
            throw new NotImplementedException();
        }

        #endregion


        public int GetCountFormFull(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as CountNo ");
            strSql.Append(" FROM ReportForm ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            ZhiFang.Common.Log.Log.Debug(strSql.ToString());
            var counto = DbHelperSQL.GetSingle(strSql.ToString());
            int count = 0;
            if (counto != null)
            {
                count = Convert.ToInt32(counto);
            }
            return count;
        }
        public DataSet GetList_FormFull(string fields, string strWhere)
        {

            StringBuilder strSql = new StringBuilder();

            if (!string.IsNullOrEmpty(fields))
            {
                strSql.Append("select " + fields + " ");
            }
            else
            {
                strSql.Append("select * ");
            }
            strSql.Append(" FROM ReportFormQueryDataSource ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetList_ItemFull(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ReportItemQueryDataSource ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetList_MicroFull(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ReportMicroQueryDataSource ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetList_MarrowFull(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ReportMarrowQueryDataSource ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }


        public DataTable GetReportValue(string[] p, string aa)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ReportFormID"></param>
        /// <param name="PatNo"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public DataSet ResultMhistory(string ReportFormID, string PatNo, string Where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CheckDate,CheckTime,CName,SectionName,PatNo,ReportFromID,ReceiveDate ");
            strSql.Append(" FROM ReportFormQueryDataSource Where 1=1 ");
            if (!ReportFormID.Equals("") && ReportFormID != null)
            {
                strSql.Append("and ReportFormID='" + ReportFormID + "'");
            }
            if (!PatNo.Equals("") && PatNo != null)
            {
                strSql.Append("and PatNo='" + PatNo + "'");
            }
            if (Where.Trim() != "")
            {
                strSql.Append(" and " + Where);
            }
            ZhiFang.Common.Log.Log.Debug(strSql.ToString());
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 根据病历号和日期查询报告单
        /// </summary>
        /// <param name="PatNo"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public DataSet ResultDataTimeMhistory(string PatNo, string Where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ReportFormQueryDataSource Where 1=1 ");
            if (PatNo.Trim() != "")
            {
                strSql.Append("and PatNo='" + PatNo + "'");
            }
            if (Where.Trim() != "")
            {
                strSql.Append(" and " + Where);
            }
            ZhiFang.Common.Log.Log.Debug(strSql.ToString());
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}
