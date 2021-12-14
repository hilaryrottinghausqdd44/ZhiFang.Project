using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    public interface IDRequestForm : IDataBase<Model.RequestForm>
    {
        /// <summary>
        /// 查询报告单个数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        int GetCountFormFull(string strWhere);
        /// <summary>
        /// 查询报告单
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        DataSet GetList_FormFull(string fields, string strWhere);
       
        /// <summary>
        /// 根据FormNo数组返回RequestForm列表
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        DataTable GetRequestFormFullList(string FormNo);

        bool UpdatePageInfo(string reportformlist, string pageCount, string pageName);

        bool UpdatePrintTimes(string[] reportformlist);

        bool UpdateClientPrintTimes(string[] reportformlist);

        /// <summary>
        /// 多项目历史对比
        /// </summary>
        /// <param name="ReportFormID"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        DataSet ResultMhistory(string ReportFromID, string PatNo, string Where);

        /// <summary>
        /// 根据病历号和日期查询报告单
        /// </summary>
        /// <param name="PatNo"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        DataSet ResultDataTimeMhistory(string PatNo, string Where);

        DataSet GetRequestFromByReportFormID(List<string> idList, string fields, string strWhere);
    }
}
