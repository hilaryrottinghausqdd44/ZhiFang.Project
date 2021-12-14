using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.ReportFormQueryPrint.DBUtility;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter
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
            ReportMicro rm = new ReportMicro();
            return rm.GetReportMicroFullList(FormNo);
        }
        public DataTable GetMicroItemGroupList(string FormNo)
        {
            ReportMicro rm = new ReportMicro();
            return rm.GetReportMicroGroupList(FormNo);
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
        /// 历史对比1
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetReportValue(string[] p)
        {
            ReportForm rf = new ReportForm();
            return rf.GetReportValue(p);
        }

        /// <summary>
        /// 历史对比2
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetReportValue(string[] p,string aa)
        {
            ReportForm rf = new ReportForm();
            return rf.GetReportValue(p,aa);
        }
        public DataTable GetFromPGroupInfo(int SectionNo)
        {
            throw new NotImplementedException();
        }
        public DataTable GetFromGraphList(string FormNo)
        {
            try
            {
                Model.RFGraphData rfgd_m = new Model.RFGraphData();
                rfgd_m.FormNo = FormNo;
                string[] p = FormNo.Split(';');
                if (p.Length >= 4)
                {
                    try
                    {
                        rfgd_m.ReceiveDate = DateTime.Parse(p[0]);
                        rfgd_m.SectionNo = Int32.Parse(p[1]);
                        rfgd_m.TestTypeNo = Int32.Parse(p[2]);
                        rfgd_m.SampleNo = p[3];
                        return new RFGraphData().GetList(rfgd_m).Tables[0];
                        //strSql.Append(" where ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "' ");
                    }
                    catch
                    {
                        return new DataTable();
                    }
                }
                else
                {
                    return new DataTable();
                    //strSql.Append(" where 1=2 ");
                }
                return new RFGraphData().GetList(rfgd_m).Tables[0];
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.Message);
                return new DataTable();
            }
        }
        public int GetCountFormFull(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as CountNo ");
            strSql.Append(" FROM ReportFormQueryDataSource ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where ActiveFlag=1 and " + strWhere + "   ");
            }
            else
            {
                strSql.Append(" where ActiveFlag=1 ");
            }
            ZhiFang.Common.Log.Log.Debug(strSql.ToString());
            var counto = DbHelperSQL.GetSingle(strSql.ToString());
            int count = 0;
            if(counto!=null )
            {
                try
                {
                    count = int.Parse(counto.ToString());
                }
                catch (Exception e)
                {
                    ZhiFang.Common.Log.Log.Error("GetCountFormFull异常："+e.ToString());
                    count = 0;
                }
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
                strSql.Append(" where ActiveFlag=1 and " + strWhere + "   ");
            }
            else
            {
                strSql.Append(" where ActiveFlag=1 ");
            }
            ZhiFang.Common.Log.Log.Debug("GetList_FormFull.fields:" + fields + ";strWhere:" + strWhere);
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
       /// <summary>
       /// 
       /// </summary>
       /// <param name="ReportFormID"></param>
       /// <param name="PatNo"></param>
       /// <param name="Where"></param>
       /// <returns></returns>
        public DataSet ResultMhistory(string ReportFormID, string PatNo, string Where) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CheckDate,CheckTime,CName,SectionName,PatNo,ReportFormID,ReceiveDate ");
            strSql.Append(" FROM ReportFormQueryDataSource Where 1=1 ");
            if (ReportFormID.Trim() != "") {
                strSql.Append("and ReportFormID='" + ReportFormID + "'");
            }
            if (!"".Equals(PatNo) && null != PatNo)
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
            strSql.Append("select ReportFormID,SecretType ");
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

        public DataSet SampleStateTailAfter(string ReportFormID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select operator,AffirmTime, (case when AffirmTime is null then '未完成' else '已完成' end ) as operatorexplain,collecter,CONVERT(varchar, Collectdate, 23) + ' ' + CONVERT(varchar, CollectTime, 24) as Collectdate,  (case when Collectdate is null then '未完成' else '已完成' end ) as collecterexplain,NurseSender,NurseSendTime,(case when NurseSendTime is null then '未完成' else '已完成' end ) as NurseSenderexplain,NurseSendCarrier,arrivetime,(case when arrivetime is null then '未完成' else '已完成' end ) as NurseSendCarrierexplain,incepter,inceptTime,(case when inceptTime is null then '未完成' else '已完成' end ) as incepterexplain,Technician,CONVERT(varchar,Receivedate, 23) + ' ' + CONVERT(varchar,ReceiveTime, 24) as Receivedate,(case when Receivedate is null then '未完成' else '已完成' end ) as Technicianexplain,Technician,CONVERT(varchar,Testdate, 23) + ' ' + CONVERT(varchar,TestTime, 24) as Testdate,(case when Testdate is null then '未完成' else '已完成' end ) as Testexplain,checker,CONVERT(varchar,Checkdate, 23) + ' ' + CONVERT(varchar,CheckTime, 24) as Checkdate,(case when Checkdate is null then '未完成' else '已完成' end ) as checkerexplain ");
            strSql.Append(" FROM ReportFormQueryDataSource Where 1=1 ");
            if (ReportFormID.Trim() != "")
            {
                strSql.Append("and ReportFormID='" + ReportFormID + "'");
            }
            else {
                ZhiFang.Common.Log.Log.Debug("SampleStateTailAfter.ReportFormID为空！");
                return new DataSet();
            }
           
            ZhiFang.Common.Log.Log.Debug("SampleStateTailAfter.sql:"+strSql.ToString());
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion
    }
}
