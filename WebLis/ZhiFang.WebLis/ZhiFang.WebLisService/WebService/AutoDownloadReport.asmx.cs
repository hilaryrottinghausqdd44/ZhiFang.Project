using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Data;
using System.Text;
using ECDS.Common;


namespace ZhiFang.WebLisService.WebService
{
    /// <summary>
    /// AutoDownloadReport 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class AutoDownloadReport : System.Web.Services.WebService
    {

        /// <summary>
        /// 获取某送检单位的报告
        /// </summary>
        /// <param name="clientno">送检单位编号</param>
        /// <param name="topNum">最大数目</param>
        /// <returns></returns>
        [WebMethod]
        public DataSet  GetReportForm(string clientno, string topNum, string StartDate, string EndDate)
        {
            Log.Info(String.Format("自动查询报告开始clientno={0},topNum={1},StartDate={2},EndDate={3}", clientno, topNum, StartDate, EndDate));
            StringBuilder sb = new StringBuilder();
            sb.Append("select ");
            if (topNum != "")
            {
                sb.Append(" top " + topNum + " ");
            }
            sb.Append(" *,checkdate+checktime as checkdateTime  from ReportFormFull ");
            sb.Append(" where clientno=" + clientno);
            sb.Append(" and  AreaSendFlag=0");

            if (StartDate.Length > 0)
            {
                sb.Append(" and ReceiveDate>='" + StartDate + "'");
            }
            if (EndDate.Length > 0)
            {
                sb.Append(" and ReceiveDate<='" + EndDate + "'");
            }

            SqlServerDB sqlDB = new SqlServerDB();
            DataSet ds = sqlDB.ExecDS(sb.ToString());
            Log.Info(String.Format("自动查询报告:{0}", sb.ToString()));

            return ds;
        }

        /// <summary>
        /// 获取某报告的项目
        /// </summary>
        /// <param name="ReportFormID"></param>
        /// <returns></returns>
        [WebMethod]
        public DataSet GetReportItem(string ReportFormID)
        {
            Log.Info(String.Format("自动查询报告项目:ReportFormID={0}", ReportFormID));
            StringBuilder sb = new StringBuilder();
            sb.Append("select  *  from ReportItemFull ");
            sb.Append(" where ReportFormID='" + ReportFormID+"'");
            SqlServerDB sqlDB = new SqlServerDB();
            DataSet ds = sqlDB.ExecDS(sb.ToString());
            Log.Info(String.Format("自动查询报告项目:{0}", sb.ToString()));
            return ds;
        }

        /// <summary>
        /// 更新报告标志
        /// </summary>
        /// <param name="ReportFormID"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        [WebMethod]
        public int UpdateAreaFlag(string ReportFormID, int flag)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("update ReportFormFull set AreaSendFlag=" + flag + ",AreaSendTime=getdate() ");
            sb.Append(" where ReportFormID='" + ReportFormID+"'");
            SqlServerDB sqlDB = new SqlServerDB();
            int r = sqlDB.ExecuteNonQuery(sb.ToString());
            Log.Info(String.Format("更新报告标志:{0}", sb.ToString()));
            return r;
        }
    }
}
