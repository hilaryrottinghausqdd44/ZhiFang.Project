using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.Common.Log;
using System.Data;
using ZhiFang.IBLL.Report;
using ZhiFang.BLLFactory;
using System.Xml;
using System.Collections;

namespace ZhiFang.WebLis.ReportPrint
{
    public partial class PrintPDFList1 : System.Web.UI.Page
    {
        private readonly IBReportFormFull rffb = BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
        private static readonly IBReportItem rib = BLLFactory<IBReportItem>.GetBLL("ReportItem");     
        public string filep = "";
        private readonly ZhiFang.IBLL.Report.IBUserReportFormDataListShowConfig iburfdlsc = BLLFactory<IBUserReportFormDataListShowConfig>.GetBLL("UserReportFormDataListShowConfig");
        string formNo = "";
        protected int iCount = 0;
        protected XmlNodeList NodeTdTitleList;
        protected XmlNodeList NodeTrBodyList;
        protected DataSet ds = new DataSet();
        protected string hhh = "";
        protected int hPageBegins = 1;
        protected int hPageSize = 15;
        protected int iRecordCount = 1;
        protected string[] filepath;
        protected List<String> strRtn;
        protected string path = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["reportfile"] != null)
            {
                filep= Request.QueryString["reportfile"].ToString();
            }
            if (Request.QueryString["RepoertFormID"] != null)
            {
                 formNo= Request.QueryString["RepoertFormID"].ToString();
            }
            filepath = filep.Split(',');
            path = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir");
            string[] reportFormID = formNo.Split(',');
            strRtn = new List<String>();
            string[] strB=null;
            for (int i = 0; i < reportFormID.Length; i++)
            {
                 strB = reportFormID[i].Split(';');
                strRtn.Add(strB[0]);
            }
            ds = rffb.GetReportFormInfo(strRtn);
            if (filepath.Length > 1)
            {
                string str = filepath[0].Split('\\')[1];
                string str2 = filepath[1].Split('\\')[1].Replace(".pdf", "");
                if (str.IndexOf(str2) > -1)
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].Rows[0].ItemArray);
                    ds.Tables[0].Rows[0]["ReportFormID"] = strB[0] + "_" + strB[1];
                }
            }
            iCount = ds.Tables[0].Rows.Count;
            string t = iburfdlsc.ShowReportFormListOrderColumn(this.PageName.Value.Trim(), Convert.ToInt32(this.tmpclassid.Value.Trim()));
            this.OrderColumn.Value = t;
            this.tablehead.InnerHtml = ZhiFang.WebLis.Class.ShowTools.GridViewHeadShow(this.PageName.Value.Trim(), Convert.ToInt32(this.tmpclassid.Value.Trim()));
            showlistandfrom(ds.Tables[0], 0);
        }
        /// <summary>
        /// 判断是显示还是隐藏
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public bool JudgeShowOrHidden(string t1, string t2)
        {
            bool result = false;
            try
            {
                if (t1.ToLower().IndexOf(t2.ToLower()) > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                //OA.ZCommon.Log.RecordLog.writeFile(OA.ZCommon.Log.LogTypeEnum.Show, t2 + "||" + result);
            }
            catch (Exception ex)
            {
                Log.Info(ex.ToString());
               // OA.ZCommon.Log.RecordLog.writeFile(OA.ZCommon.Log.LogTypeEnum.Show, "出错了:" + ex.Message);
            }
            return result;
        }
        protected string RetrieveTableName(XmlNode myNode)
        {

            string TableName = "";
            while (myNode.Name.ToUpper() != "TABLES")
            {
                if (myNode.Name.ToUpper() == "TABLE")
                    TableName = "/" + myNode.Attributes.GetNamedItem("EName").InnerXml + TableName;
                myNode = myNode.ParentNode;
            }
            if (TableName.Length > 0)
                TableName = TableName.Substring(1);

            return TableName;
        }
        protected void showlistandfrom(DataTable dt, int pageindex)
        {
            if (dt.Rows.Count > 0)
            {
                Session["tmpdata1"] = dt;
                DataView dv = dt.DefaultView;
                if (this.OrderColumn.Value.Trim() != "")
                {
                    string orderstr = "";
                    string[] s = this.OrderColumn.Value.Trim().Split(',');
                    for (int i = 0; i < s.Length; i++)
                    {
                        if (dt.Columns.Contains(s[i] + "Sort"))
                        {
                            orderstr += s[i] + "Sort" + " " + this.OrderColumnFlag.Value.ToString().Trim() + ",";
                        }
                        else
                        {
                            orderstr += s[i] + " " + this.OrderColumnFlag.Value.ToString().Trim() + ",";
                        }
                    }
                    orderstr = orderstr.Substring(0, orderstr.Length - 1);
                    dv.Sort = orderstr;
                }
                string[] tmpstra = GridViewShow_Weblis(this.PageName.Value.Trim(), Convert.ToInt32(this.tmpclassid.Value.Trim()), dv, pageindex);
                this.tablelist.InnerHtml = tmpstra[1];
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), System.Guid.NewGuid().ToString(), "try{document.getElementById('" + tmpstra[0].Split(',')[0].ToString().Trim() + "').className='leftlist1focus';}catch(e){ alert('" + tmpstra[0].Split(',')[0].ToString().Trim() + "'); }", true);
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), System.Guid.NewGuid().ToString(), "try{document.getElementById('PIndex').value='" + pageindex + "';}catch(e){}", true);
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), System.Guid.NewGuid().ToString(), "try{document.getElementById('sT1').style.display='block';}catch(e){ }", true);
            }
            else
            {
                this.tablelist.InnerHtml = "";
                ZhiFang.Common.Log.Log.Debug(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "dt为空或无记录！");
            }
        }
        public string[] GridViewShow_Weblis(string PageName, int Sort, DataView dv, int PageIndex)
        {
            string[] returnstr = new string[2];
            try
            {
                returnstr[0] = "0";
                ZhiFang.IBLL.Report.IBUserReportFormDataListShowConfig iburfdlsc = BLLFactory<IBUserReportFormDataListShowConfig>.GetBLL("UserReportFormDataListShowConfig");
                SortedList<string, string[]> columns = iburfdlsc.ShowReportFormListHeadName(PageName, Sort);
                string html = "";
                string tr = "";
                string td = "";
                string tmpserialno = "";
                string trbackgroup = "leftlist2";
                int pagesize = iburfdlsc.ShowReportFormListPageSize(PageName, Sort);
                int pagecount = ZhiFang.Tools.PagePaging.GetCountMaxPage(dv, pagesize);
                int preint = PageIndex - 1;
                int nextint = PageIndex + 1;
                DataTable tmpdt = ZhiFang.Tools.PagePaging.PresentPage(dv, PageIndex, pagesize);
                for (int i = 0; i < tmpdt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        returnstr[0] = tmpdt.Rows[i]["ReportFormID"].ToString().Trim() + "," + tmpdt.Rows[i]["SectionNo"].ToString().Trim() + "," + tmpdt.Rows[i]["SectionType"].ToString().Trim();
                    }

                    td = "";
                    for (int j = 0; j < columns.Count; j++)
                    {
                        switch (columns.ElementAt(j).Value[2].Trim())
                        {
                            case "PrintTimes":
                                {
                                    try
                                    {
                                        if (Convert.ToInt32(tmpdt.Rows[i][columns.ElementAt(j).Value[2]].ToString().Trim()) > 0)
                                        {
                                            td = td + "<td width=\"" + columns.ElementAt(j).Value[1] + "\" align=\"" + columns.ElementAt(j).Value[3] + "\"><img src='" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + "/Images/print_pic.png'/>" + tmpdt.Rows[i][columns.ElementAt(j).Value[2]].ToString().Trim() + "</td>";
                                        }
                                        else
                                        {
                                            td = td + "<td width=\"" + columns.ElementAt(j).Value[1] + "\" align=\"" + columns.ElementAt(j).Value[3] + "\">&nbsp;</td>";
                                        }
                                    }
                                    catch
                                    {
                                        td = td + "<td width=\"" + columns.ElementAt(j).Value[1] + "\" align=\"" + columns.ElementAt(j).Value[3] + "\">&nbsp;</td>";
                                    }
                                    break;
                                }
                            default: td = td + "<td width=\"" + columns.ElementAt(j).Value[1] + "\" align=\"" + columns.ElementAt(j).Value[3] + "\"><nobr>" + tmpdt.Rows[i][columns.ElementAt(j).Value[2]].ToString().Trim() + "<nobr></td>"; break;
                        }
                    }
                    if (columns.ElementAt(0).Value[4].Trim() == "0")
                    {
                        td = "<td width=\"5\"><input id=\"FromNoCheckBox" + tmpdt.Rows[i]["ReportFormID"].ToString().Trim() + "\" name=\"FromNoCheckBox\" type=\"checkbox\" onclick=\"flag = 1;\" value=\"" + tmpdt.Rows[i]["ReportFormID"].ToString().Trim() + ";" + tmpdt.Rows[i]["serialno"].ToString().Trim() + "\" /></td>" + td;
                    }
                    if (tmpserialno != tmpdt.Rows[i]["Serialno"].ToString().Trim())
                    {
                        tmpserialno = tmpdt.Rows[i]["Serialno"].ToString().Trim();
                        if (trbackgroup == "leftlist1")
                        {
                            trbackgroup = "leftlist2";
                        }
                        else
                        {
                            trbackgroup = "leftlist1";
                        }
                    }
                    string tmpItemGroup = "";
                    if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportListPatItemShowFlag") == "1")
                    {
                        SortedList sl = rib.GetReportItemList_ItemGroup(tmpdt.Rows[i]["ReportFormID"].ToString().Trim());

                        if (sl.GetValueList().Count > 0)
                        {
                            foreach (var a in sl.GetValueList())
                            {
                                tmpItemGroup += a.ToString().Trim() + ",";
                            }
                            tmpItemGroup = tmpItemGroup.Substring(0, tmpItemGroup.LastIndexOf(','));
                        }
                        int h = 0;
                        if (i >= tmpdt.Rows.Count / 2)
                        {
                            h = 1;
                        }
                        string file = "../" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + tmpdt.Rows[i]["ReportFormID"].ToString().Trim().Replace(" ", "").Replace(":", "");
                        ZhiFang.Common.Log.Log.Info(file.Replace("\\", "/"));
                        tr += "<tr height='25' id='" + tmpdt.Rows[i]["ReportFormID"].ToString().Trim() + "' class='" + trbackgroup + "' onclick=\"showpdf('" + filepath[i].Replace("../", "") + "');\" onmouseout=\" CloseItemList();\">" + td + "</tr>";
                    }
                    else
                    {
                      
                        tr += "<tr height='25' id='" + tmpdt.Rows[i]["ReportFormID"].ToString().Trim() + "' class='" + trbackgroup + "' onclick=\"showpdf('" + "../" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "/" + tmpdt.Rows[i]["ReportFormID"].ToString().Trim().Replace(" ", "").Replace(":", "")+".pdf" +"');\" >" + td + "</tr>";
                    }

                }

                if (preint < 0)
                {
                    preint = 0;
                }
                else
                {
                    if (preint > pagecount)
                    {
                        preint = pagecount;
                    }
                }
                if (nextint > pagecount)
                {
                    nextint = pagecount;
                }
                else
                {
                    if (nextint < 0)
                    {
                        nextint = 0;
                    }
                }

                if (columns.ElementAt(0).Value[4].Trim() == "0")
                {
                    tr += "<tr><td colspan=\"" + (columns.Count + 1).ToString() + "\" align='center' ><a href=\"javascript:GoPageIndex('0')\">首页</a>&nbsp;&nbsp;<a href=\"javascript:GoPageIndex('" + preint.ToString().Trim() + "')\">上一页</a>&nbsp;&nbsp;<a href=\"javascript:GoPageIndex('" + nextint.ToString().Trim() + "')\">下一页</a>&nbsp;&nbsp;<a href=\"javascript:GoPageIndex('" + pagecount + "')\">尾页</a>&nbsp;&nbsp;当前第" + (PageIndex + 1).ToString() + "页&nbsp;&nbsp;共" + (pagecount + 1).ToString() + "页<td><tr>";
                }
                else
                {
                    tr += "<tr><td colspan=\"" + columns.Count.ToString() + "\" align='center' ><a href=\"javascript:GoPageIndex('0')\">首页</a>&nbsp;&nbsp;<a href=\"javascript:GoPageIndex('" + preint.ToString().Trim() + "')\">上一页</a>&nbsp;&nbsp;<a href=\"javascript:GoPageIndex('" + nextint.ToString().Trim() + "')\">下一页</a>&nbsp;&nbsp;<a href=\"javascript:GoPageIndex('" + pagecount + "')\">尾页</a>&nbsp;&nbsp;当前第" + (PageIndex + 1).ToString() + "页&nbsp;&nbsp;共" + (pagecount + 1).ToString() + "页<td><tr>";
                }
                html = "<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\">" + tr + "</table>";
                returnstr[1] = html;
                return returnstr;
            }
            catch (Exception eee)
            {
                ZhiFang.Common.Log.Log.Debug(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + eee.ToString());
                return returnstr;
            }
        }
    }
}