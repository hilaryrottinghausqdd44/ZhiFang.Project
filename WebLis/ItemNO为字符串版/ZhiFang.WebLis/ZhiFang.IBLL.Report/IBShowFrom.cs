using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZhiFang.IBLL.Report
{
    public interface IBShowFrom
    {        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FromNo"></param>
        /// <param name="SectionNo"></param>
        /// <returns></returns>
        string ShowReportForm(string FromNo, int SectionNo,string PageName,int ShowType);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FromNo"></param>
        /// <param name="SectionNo"></param>
        /// <returns></returns>
        string ShowReportForm_Weblis(string FromNo, int SectionNo, string PageName, int ShowType, int sectiontype);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FromNo"></param>
        /// <param name="SectionNo"></param>
        /// <param name="PageName"></param>
        /// <param name="ShowType"></param>
        /// <returns></returns>
        string ShowReportFormFrx(string FromNo, int SectionNo, string PageName, int ShowType);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FromNo"></param>
        /// <param name="ShowReportFormName"></param>
        /// <returns></returns>
        string ShowReportForm(string FromNo, string ShowReportFormName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="printformatno"></param>
        /// <returns></returns>
        int GetPrintFormatNo(int printformatno);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pgm"></param>
        /// <returns></returns>
        SortedList ShowFormTypeList(Model.PGroup pgm);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pgm"></param>
        /// <returns></returns>
        SortedList ShowFormTypeList(Model.PGroup pgm, string PageName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FromNo"></param>
        /// <param name="SectionNo"></param>
        /// <returns></returns>
        SortedList ShowFormTypeList(string FromNo, int SectionNo);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pgm"></param>
        /// <returns></returns>
        SortedList ShowFormTypeList(string FromNo, int SectionNo, string PageName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pgm"></param>
        /// <returns></returns>
        SortedList ShowClassList(string PageName);

        /// <summary>
        /// 读取xml配置
        /// </summary>
        /// <param name="FromNo"></param>
        /// <param name="PageName"></param>
        /// <param name="ShowType"></param>
        /// <param name="SectionType"></param>
        /// <returns></returns>
        string ShowReportXML(string FromNo, string PageName, string ShowType, string SectionType, out int ShowFunction);

        /// <summary>
        /// 读取Data配置
        /// </summary>
        /// <param name="dsrf"></param>
        /// <param name="PageName"></param>
        /// <param name="ShowType"></param>
        /// <param name="SectionType"></param>
        /// <returns></returns>
        string ShowReportSqlDB(string FromNo, DataTable dtrf, DataTable dtTalbe, string SectionType, out int TemplateType);
        /// <summary>
        /// 判断大组
        /// </summary>
        /// <param name="FromNo">报告单号</param>
        /// <param name="SectionType">大组类型</param>
        /// <param name="ShowModel">模版地址</param>
        /// <param name="dsrf">信息表</param>
        /// <param name="dtTable">常规信息</param>
        /// <returns>表名称</returns>
        string CheckSupergroup(string FromNo, int SectionType,  out DataTable dtrf, out DataTable dtTable, out DataTable dtGraph);

        /// <summary>
        /// XSLT
        /// </summary>
        /// <param name="dtrf"></param>
        /// <param name="dtTable"></param>
        /// <param name="ShowModel"></param>
        /// <returns></returns>
        string ShowReportXSLT(DataTable dtrf, DataTable dtTable, string ShowName, string ShowModel);

        /// <summary>
        /// FRX
        /// </summary>
        /// <param name="dsrf"></param>
        /// <param name="dsDb"></param>
        /// <returns></returns>
        string ShowReportFRX(string FromNo, DataTable dtrf, DataTable dtTable, string ShowName, string ShowModel);        

        //DataTable GerReportFormAndItemData(string fromNo, int sectiontype, out DataTable dsrf_Out);

        //string GetTemplatePath(DataTable dtrf, DataTable dtri, string pageName, int showType, int sectiontype);
    }
}
