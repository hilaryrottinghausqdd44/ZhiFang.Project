using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ZhiFang.Model;
using ZhiFang.IBLL.Report;
using ZhiFang.BLLFactory;
using System.Data;
using ZhiFang.BLL.Report.Print;

namespace ZhiFang.WebLis.ServiceWCF.Other
{
    /// <summary>
    /// PersonSearch_PKI 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class PersonSearch_PKI : System.Web.Services.WebService
    {

        [WebMethod]
        /// <summary>
        /// 获取报告查询列表
        /// </summary>
        /// <param name="reportformfull"></param>
        /// <param name="wherestr"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public BaseResultDataValue SelectReportListByPerson_Barcode_Name(string Barcode, string Name, int page, int rows, string sort, string order)
        {
            IBReportFormFull ibrff = BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
            Tools.ListTool LT = new Tools.ListTool();
            BaseResultDataValue brdv = new BaseResultDataValue();
            //EntityListEasyUI<UiReportForm_N> easyui = new EntityListEasyUI<UiReportForm_N>();
            EntityListEasyUI<Model.ReportFormFull> easyui = new EntityListEasyUI<Model.ReportFormFull>();
            try
            {
                string wheresql = "";
                #region 验证

                if (Barcode == null || Barcode == string.Empty || Barcode.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "Barcode错误";
                    return brdv;
                }
                Barcode = ZhiFang.Tools.Validate.ReplaceWhereString(Barcode);

                if (Name == null || Name == string.Empty || Name.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "Name错误";
                    return brdv;
                }
                Name = ZhiFang.Tools.Validate.ReplaceWhereString(Name);
                ZhiFang.Common.Log.Log.Debug("IP:" + HttpContext.Current.Request.UserHostAddress + "@计算机名：" + System.Net.Dns.GetHostName() + "调用ZhiFang.WebLis.ServiceWCF.Other.SelectReportListByPerson_Barcode_Name参数：Barcode=" + Barcode + ";Name=" + Name + ";page=" + page + ";rows=" + rows + ";sort=" + sort + ";order=" + order);
                wheresql = " SerialNo='" + Barcode.Trim() + "' and CName='" + Name.Trim() + "' ";
                //默认五千条
                int ReportSelMaxNum = 5000;
                var max = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportSelMaxNum").Trim();

                if (max != null || max != "")
                {
                    ReportSelMaxNum = Convert.ToInt32(max);
                }

                try
                {
                    if (ibrff.Count(wheresql) > ReportSelMaxNum)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "返回记录数大于配置的条数";
                        return brdv;
                    }
                }
                catch (Exception e)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = e.ToString();
                    Model.ReportFormFull model = new ReportFormFull();
                    //brdv = SelectReportList2(model, page.ToString(), rows.ToString());
                    return brdv;
                }
                #endregion
                string sqlsort = "";
                if (sort != null && sort.Trim() != "" && sort.Trim().ToLower() != "null")
                {
                    sqlsort = " order by " + sort.Replace(";", "").Replace("'", "");
                    if (order != null && order.Trim() != "" && order.Trim().ToLower() != "null")
                    {
                        sqlsort += " " + order.Replace(";", "").Replace("'", "");

                    }
                }
                else
                {
                    sqlsort = "  order by RECEIVEDATE desc ";
                }
                ZhiFang.Common.Log.Log.Debug("SelectReportListByPerson_Barcode_Name.wherestr+sqlsort=" + wheresql + sqlsort);
                DataSet ds = ibrff.GetList(wheresql + sqlsort);
                if (ds == null)
                {
                    return null;
                }
                DataTable tempDt = ds.Tables[0];
                List<Model.ReportFormFull> reportFormFullList = ibrff.DataTableToList(tempDt);
                List<Model.ReportFormFull> Result = LT.Pagination<Model.ReportFormFull>(page, rows, reportFormFullList);
                easyui.rows = Result;
                easyui.total = reportFormFullList.Count;

                brdv.success = true;
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(easyui);
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
                ZhiFang.Common.Log.Log.Error("ZhiFang.WebLis.ServiceWCF.Other.SelectReportListByPerson_Barcode_Name异常：" + e.ToString());
            }
            return brdv;
        }

        [WebMethod]
        /// <summary>
        /// 报告打印
        /// </summary>
        /// <param name="reportformId">报告单ID</param>
        /// <param name="reportformtitle">报告抬头</param>
        /// <param name="reportformfiletype">报告文件类型（预留）</param>
        /// <param name="printtype">打印类型（预留）</param>
        /// <returns></returns>
        public BaseResultDataValue ReportPrint(string reportformId, string reportformtitle, string reportformfiletype, string printtype)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            #region 验证
            if (reportformId == null || reportformId == "")
                return null;
            if (printtype == null || printtype == "")
                return null;
            if (reportformtitle == null || reportformtitle == "")
                return null;
            #endregion

            #region 打印报告

            try
            {
                List<string> ReportFormContextList = null;
                string title = "0";
                switch (reportformtitle.ToUpper())
                {
                    case "CENTER": title = "0"; break;
                    case "CLIENT": title = "1"; break;
                    case "BATCH": title = "2"; break;
                    case "MENZHEN": title = "3"; break;
                    case "ZHUYUAN": title = "4"; break;
                    case "TIJIAN": title = "5"; break;
                    default: title = "0"; break;
                }
                //报告上传过程中提前生成的报告
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("isUpLoadGeneReport") == "1")
                {
                    ZhiFang.Common.Log.Log.Info("isUpLoadGeneReport：1");
                    ZhiFang.BLL.Report.ReportFormFull rffb = new ZhiFang.BLL.Report.ReportFormFull();
                    Model.ReportFormFull rff_m = new Model.ReportFormFull();
                    rff_m.ReportFormID = reportformId; //"_138723_25_1_9_2011-04-21 00:00:00";
                    DataSet dsrf = rffb.GetList(rff_m);
                    if (dsrf != null && dsrf.Tables[0].Rows.Count > 0)
                        ReportFormContextList = PrintReportFormCommon.FindReportFormFiles(dsrf.Tables[0].Rows[0], (ZhiFang.Common.Dictionary.ReportFormTitle)Convert.ToInt32(title), title);
                }
                else
                    ReportFormContextList = this.PrintHtml(reportformId.Split(','), title, reportformfiletype);


                string aa = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(ReportFormContextList);

                brdv.success = true;
                brdv.ResultDataValue = aa;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
                brdv.ResultDataValue = "";
            }
            #endregion
            /**/
            return brdv;
        }
        public List<string> PrintHtml(string[] ReportFormID, string title, string reportformfiletype)
        {
            IBPrintReportForm ibprf = BLLFactory<IBPrintReportForm>.GetBLL("Print.PrintReportForm");
            List<string> tmp;
            List<string> tmplist = new List<string>();
            tmp = ibprf.PrintReportFormHtml(ReportFormID.ToList<string>(), (ZhiFang.Common.Dictionary.ReportFormTitle)Convert.ToInt32(title), (ZhiFang.Common.Dictionary.ReportFormFileType)Enum.Parse(typeof(ZhiFang.Common.Dictionary.ReportFormFileType), reportformfiletype));
            if (tmp != null)
            {
                for (int j = 0; j < tmp.Count; j++)
                {
                    tmplist.Add(tmp[j]);
                }
            }

            List<string> tmphtml = new List<string>();
            string appurl = System.Web.HttpContext.Current.Request.ApplicationPath.ToString();
            string applocalroot = System.AppDomain.CurrentDomain.BaseDirectory;//获取程序根目录
            //string imagesurl2 = imagesurl1.Replace(tmpRootDir, ""); //转换成相对路径
            //imagesurl2 = imagesurl2.Replace(@"\", @"/");
            //return imagesurl2;

            for (int i = 0; i < tmplist.Count; i++)
            {
                tmphtml.Add(tmplist[i].Replace(applocalroot, "").Replace(@"\", @"/"));
                //tmphtml.Add(appurl + tmplist[i].Replace(applocalroot, "").Replace(@"\", @"/"));
                //tmphtml.Add("../" + tmplist[i].Substring(tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), tmplist[i].Length - tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))));
            }
            return tmphtml;
        }
    }
}
