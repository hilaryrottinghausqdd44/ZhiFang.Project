using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ZhiFang.Common.Dictionary;
namespace ZhiFang.IBLL.Report
{
    public interface IBPrintFrom_Weblis : IBShowFrom
    {
        ReportFormTitle TitleFlag
        {
            set;
            get;
        }
        /// <summary>
        /// 返回单个报告打印内容
        /// </summary>
        /// <param name="FormNo">表单号</param>
        /// <returns></returns>
        List<string> PrintHtml(string FormNo, ReportFormTitle Flag);
        List<string> PrintHtml(string FormNo, ReportFormTitle Flag, string ResultFlag);
        List<string> PrintMergeEnHtml(string FormNo, ReportFormTitle Flag, string ResultFlag);
        List<string> PrintMergeHtml(string FormNo, ReportFormTitle Flag, string ResultFlag);
       
        /// <summary>
        /// 返回合并报告打印内容
        /// </summary>
        /// <param name="FormNo">表单号</param>
        /// <returns></returns>
        List<string> PrintMergeHtml(string FormNo, ReportFormTitle Flag);
        /// <summary>
        /// 返回合并报告打印内容
        /// </summary>
        /// <param name="FormNo">表单号</param>
        /// <returns></returns>
        List<string> PrintMergeEnHtml(string FormNo, ReportFormTitle Flag);

        List<string> PrintMergePdf(string FormNo, ReportFormTitle Flag);
        /// <summary>
        /// 返回单个报告打印内容
        /// </summary>
        /// <param name="FormNo">表单号</param>
        /// <returns></returns>
        List<string> PrintHtml(string FormNo);
        /// <summary>
        /// 返回合并报告打印内容
        /// </summary>
        /// <param name="FormNo">表单号</param>
        /// <returns></returns>
        List<string> PrintMergeHtml(string FormNo);
        /// <summary>
        /// 返回合并报告打印内容
        /// </summary>
        /// <param name="FormNo">表单号</param>
        /// <returns></returns>
        List<string> PrintMergeEnHtml(string FormNo);
        /// <summary>
        /// 返回报告使用模板信息
        /// </summary>
        /// <param name="FormNo">表单号</param>
        /// <returns></returns>
        Model.PrintFormat GetPrintModelInfo(string FormNo);
    }
}
