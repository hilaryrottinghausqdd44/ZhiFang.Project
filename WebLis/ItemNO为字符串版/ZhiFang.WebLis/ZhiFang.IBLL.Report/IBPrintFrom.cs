using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ZhiFang.IBLL.Report
{
    public interface IBPrintFrom : IBShowFrom
    {        
        /// <summary>
        /// HTML读取模板 
        /// </summary>
        /// <param name="Fromno"></param>
        /// <param name="sectionno"></param>
        /// <param name="clientno"></param>
        /// <param name="speciallyitemno"></param>
        /// <param name="printno"></param>
        /// <returns></returns>
        ArrayList HtmlRepotrForm(string Fromno, int sectionno, int clientno, out int printno);
        /// <summary>
        /// 返回单个表单打印内容
        /// </summary>
        /// <param name="FormNo">表单号</param>
        /// <returns></returns>
        string PrintHtml(string FormNo);
        /// <summary>
        /// 返回报告使用模板信息
        /// </summary>
        /// <param name="FormNo">表单号</param>
        /// <returns></returns>
        Model.PrintFormat GetPrintModelInfo(string FormNo);
    }
}
