using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Report
{
    public interface IBALLReportForm
    {
        /// <summary>
        /// 返回报告单信息
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        DataTable GetFromInfo(string FormNo);
        /// <summary>
        /// 返回报告单内项目列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        DataTable GetFromItemList(string FormNo);
        /// <summary>
        /// 返回报告单所在小组信息
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        DataTable GetFromPGroupInfo(int SectionNo);
        /// <summary>
        /// 返回报告单内图片列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        DataTable GetFromGraphList(string FormNo);
    }
}
