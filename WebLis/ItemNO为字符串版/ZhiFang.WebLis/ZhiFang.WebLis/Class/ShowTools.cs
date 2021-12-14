using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI;
using System.Collections;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Report;

namespace ZhiFang.WebLis.Class
{
    public class ShowTools
    {
        private static readonly IBReportItem rib = BLLFactory<IBReportItem>.GetBLL("ReportItem");        
        /// <summary>
        /// 报告单列表html
        /// </summary>
        /// <param name="PageName">页面名称</param>
        /// <param name="Sort">序号</param>
        /// <param name="dt">数据表</param>
        /// <param name="checkboxflag">是否加checkbox列</param>
        /// <returns></returns>
        public static string[] GridViewShow(string PageName, int Sort, DataView dv, int PageIndex)
        { 
            string[] returnstr = new string[2];
            try
            {               
                ZhiFang.IBLL.Report.IBUserReportFormDataListShowConfig iburfdlsc = BLLFactory<IBUserReportFormDataListShowConfig>.GetBLL("UserReportFormDataListShowConfig");
                returnstr[0] = "0";
                SortedList<string, string[]> columns = iburfdlsc.ShowReportFormListHeadName(PageName, Sort);
                string html = "";
                string tr = "";
                string td = "";

                int pagesize = iburfdlsc.ShowReportFormListPageSize(PageName, Sort);
                int pagecount=ZhiFang.Tools.PagePaging.GetCountMaxPage(dv, pagesize);
                int preint = PageIndex-1;
                int nextint = PageIndex+1;
                DataTable tmpdt = ZhiFang.Tools.PagePaging.PresentPage(dv, PageIndex, pagesize);
                for (int i = 0; i < tmpdt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        returnstr[0] = tmpdt.Rows[i]["FormNo"].ToString().Trim() + "," + tmpdt.Rows[i]["SectionNo"].ToString().Trim();
                    }
                    string trbackgroup = "leftlist2";
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
                        td = "<td width=\"5\"><input id=\"FromNoCheckBox" + tmpdt.Rows[i]["FormNo"].ToString().Trim() + "\" name=\"FromNoCheckBox\" type=\"checkbox\" onclick=\"flag = 1;\" value=\"" + tmpdt.Rows[i]["FormNo"].ToString().Trim() + "\" /></td>" + td;
                    }
                    if ((i + 1) % 2 != 0)
                    {
                        trbackgroup = "leftlist1";

                    }
                    string tmpItemGroup = "";
                    if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportListPatItemShowFlag") == "1")
                    {
                        SortedList sl = rib.GetReportItemList_ItemGroup(tmpdt.Rows[i]["FormNo"].ToString().Trim());

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
                        tr += "<tr height='25' id='" + tmpdt.Rows[i]["FormNo"].ToString().Trim() + "' class='" + trbackgroup + "' onclick=\"ShowReprotFrom('" + tmpdt.Rows[i]["FormNo"].ToString().Trim() + "','" + tmpdt.Rows[i]["SectionNo"].ToString().Trim() + "');\" onmouseover=\" ItemList('" + tmpItemGroup.Trim() + "'," + h + ");\" onmouseout=\" CloseItemList();\">" + td + "</tr>";
                    }
                    else
                    {
                        tr += "<tr height='25' id='" + tmpdt.Rows[i]["FormNo"].ToString().Trim() + "' class='" + trbackgroup + "' onclick=\"ShowReprotFrom('" + tmpdt.Rows[i]["FormNo"].ToString().Trim() + "','" + tmpdt.Rows[i]["SectionNo"].ToString().Trim() + "');\" >" + td + "</tr>";
                    }
                    
                }

                if (preint < 0 )
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
                    tr += "<tr><td colspan=\"" + (columns.Count + 1).ToString() + "\" align='center' ><a href=\"javascript:GoPageIndex('0')\">首页</a>&nbsp;&nbsp;<a href=\"javascript:GoPageIndex('" + preint.ToString().Trim() + "')\">上一页</a>&nbsp;&nbsp;<a href=\"javascript:GoPageIndex('" + nextint.ToString().Trim() + "')\">下一页</a>&nbsp;&nbsp;<a href=\"javascript:GoPageIndex('" + pagecount + "')\">尾页</a>&nbsp;&nbsp;当前第" + (PageIndex+1).ToString() + "页&nbsp;&nbsp;共" + (pagecount+1).ToString() + "页<td><tr>";
                }
                else
                {
                    tr += "<tr><td colspan=\"" + columns.Count.ToString() + "\" align='center' ><a href=\"javascript:GoPageIndex('0')\">首页</a>&nbsp;&nbsp;<a href=\"javascript:GoPageIndex('" + preint.ToString().Trim() + "')\">上一页</a>&nbsp;&nbsp;<a href=\"javascript:GoPageIndex('" + nextint.ToString().Trim() + "')\">下一页</a>&nbsp;&nbsp;<a href=\"javascript:GoPageIndex('" + pagecount + "')\">尾页</a>&nbsp;&nbsp;当前第" + (PageIndex + 1).ToString() + "页&nbsp;&nbsp;共" + (pagecount + 1).ToString() + "页<td><tr>";
                }
                html = "<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\">" + tr + "</table>";                
                returnstr[1]=html;
                return returnstr;
            }
            catch
            {
                return returnstr;
            }
        }
        /// <summary>
        /// 报告单列表html
        /// </summary>
        /// <param name="PageName">页面名称</param>
        /// <param name="Sort">序号</param>
        /// <param name="dt">数据表</param>
        /// <param name="checkboxflag">是否加checkbox列</param>
        /// <returns></returns>
        public static string[] GridViewShow_Weblis(string PageName, int Sort, DataView dv, int PageIndex)
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
                 string tmpsectionno = "";
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
                            case "RECEIVEDATE":
                                td = td + "<td width=\"" + columns.ElementAt(j).Value[1] + "\" align=\"" + columns.ElementAt(j).Value[3] + "\"><nobr>" + Convert.ToDateTime(tmpdt.Rows[i][columns.ElementAt(j).Value[2]].ToString().Trim()).ToString("yyyy-MM-dd") + "<nobr></td>"; break;

                            default: td = td + "<td width=\"" + columns.ElementAt(j).Value[1] + "\" align=\"" + columns.ElementAt(j).Value[3] + "\">" + tmpdt.Rows[i][columns.ElementAt(j).Value[2]].ToString().Trim() + "</td>"; break;
                        }
                    }
                    if (columns.ElementAt(0).Value[4].Trim() == "0")
                    {
                        td = "<td width=\"5\"><input id=\"FromNoCheckBox" + tmpdt.Rows[i]["ReportFormID"].ToString().Trim() + "\" name=\"FromNoCheckBox\" type=\"checkbox\" onclick=\"flag = 1;\" value=\"" + tmpdt.Rows[i]["ReportFormID"].ToString().Trim() + ";" + tmpdt.Rows[i]["serialno"].ToString().Trim() + "\" /></td>" + td;
                    }
                    if (tmpserialno != tmpdt.Rows[i]["PATNO"].ToString().Trim()||tmpsectionno != tmpdt.Rows[i]["SECTIONNO"].ToString().Trim())
                    {
                        tmpserialno = tmpdt.Rows[i]["PATNO"].ToString().Trim();
                        tmpsectionno = tmpdt.Rows[i]["SECTIONNO"].ToString().Trim();
                        if (trbackgroup == "leftlist1")
                        {
                            trbackgroup = "leftlist2";
                        }
                        else
                        {
                            trbackgroup = "leftlist1";
                        }
                    }
                    //if (tmpserialno != tmpdt.Rows[i]["Serialno"].ToString().Trim())
                    //{
                    //    tmpserialno = tmpdt.Rows[i]["Serialno"].ToString().Trim();
                    //    if (trbackgroup == "leftlist1")
                    //    {
                    //        trbackgroup = "leftlist2";
                    //    }
                    //    else
                    //    {
                    //        trbackgroup = "leftlist1";
                    //    }
                    //}
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
                        tr += "<tr height='25' id='" + tmpdt.Rows[i]["ReportFormID"].ToString().Trim() + "' class='" + trbackgroup + "' onclick=\"ShowReprotFrom_Weblis('" + tmpdt.Rows[i]["ReportFormID"].ToString().Trim() + "','" + tmpdt.Rows[i]["SectionNo"].ToString().Trim() + "','" + tmpdt.Rows[i]["SectionType"].ToString().Trim() + "');\" onmouseover=\" ItemList('" + tmpItemGroup.Trim() + "'," + h + ");\" onmouseout=\" CloseItemList();\">" + td + "</tr>";
                    }
                    else
                    {
                        tr += "<tr height='25' id='" + tmpdt.Rows[i]["ReportFormID"].ToString().Trim() + "' class='" + trbackgroup + "' onclick=\"ShowReprotFrom_Weblis('" + tmpdt.Rows[i]["ReportFormID"].ToString().Trim() + "','" + tmpdt.Rows[i]["SectionNo"].ToString().Trim() + "','" + tmpdt.Rows[i]["SectionType"].ToString().Trim() + "');\" >" + td + "</tr>";
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
            catch(Exception eee)
            {
                ZhiFang.Common.Log.Log.Debug(DateTime.Now.ToString("YYYYMMDD HH:mm:ss")+eee.ToString());
                return returnstr;
            }
        }
        /// <summary>
        /// 报告单列表html
        /// </summary>
        /// <param name="PageName">页面名称</param>
        /// <param name="dt">数据表</param>
        /// <param name="checkboxflag">是否加checkbox列</param>
        /// <returns></returns>
        public static string[] GridViewShow(string PageName, DataView dv, int PageIndex)
        {
            return GridViewShow(PageName, 0, dv, PageIndex);
        }
        public static string GridViewHeadShow(string PageName, int Sort)
        {
            ZhiFang.IBLL.Report.IBUserReportFormDataListShowConfig iburfdlsc = BLLFactory<IBUserReportFormDataListShowConfig>.GetBLL("UserReportFormDataListShowConfig");
            SortedList<string, string[]> columns = iburfdlsc.ShowReportFormListHeadName(PageName, Sort);
            string html = "";
            string[] column = iburfdlsc.ShowReportFormListOrderColumn(PageName.Trim(), Sort).Split(',');

            for (int i = 0; i < columns.Count; i++)
            {
                string t="<th width=\"" + columns.ElementAt(i).Value[1] + "\" ><a style='cursor:pointer;' href=\"javascript:OrderByColumn('" + columns.ElementAt(i).Value[2] + "');\">" + columns.ElementAt(i).Value[0] + "</a></th>";
                for (int j = 0; j < column.Length; j++)
                {
                    if (column[j].Trim() == columns.ElementAt(i).Value[2].Trim())
                    {
                        t = "<th width=\"" + columns.ElementAt(i).Value[1] + "\" ><a style='cursor:pointer;' href=\"javascript:OrderByColumn('" + iburfdlsc.ShowReportFormListOrderColumn(PageName.Trim(), Sort) + "');\">" + columns.ElementAt(i).Value[0] + "</a></th>";
                    }
                }
                    html += t;
            }
            if (columns.ElementAt(0).Value[4].Trim() == "0")
            {
                html = "<tr class='tablehead' ><th width=\"5\"><input type=\"checkbox\" onclick=\"SelAll(this);\" /></th>" + html + "</tr>";
            }
            html = "<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\">" + html + "</table>";
            return html;
        }
        public static string GridViewHeadShow(string PageName)
        {
            return GridViewHeadShow(PageName, 0);
        }
        public static string ShowClassList(string PageName)
        {
            string html = "";
            string td = "";
            IBShowFrom showform = BLLFactory<IBShowFrom>.GetBLL("ShowFrom");
            SortedList al = showform.ShowClassList(PageName);
            if (al.Count > 1)
            {
                for (int i = 0; i < al.Count; i++)
                {
                    if (i == 0)
                    {
                        td += "<td id='" + i.ToString() + "' class='navbar1a2' onclick=\"ListShow(this);\" onmouseover=\"this.className='navbar1a2'\" onmouseout=\"CheckListShow(this);\" >" + al.GetByIndex(i).ToString().Trim() + "</td>";
                    }
                    else
                    {
                        td += "<td class='margin2'></td><td id='" + i.ToString() + "' class='navbar1a1' onclick=\"ListShow(this);\"  onmouseover=\"this.className='navbar1a2'\" onmouseout=\"CheckListShow(this);\" >" + al.GetByIndex(i).ToString().Trim() + "</td>";
                    }
                }
            }
            html = "<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" class=\"navbar1trtd\"><tr>" + td + "</tr></table>";
            return html;
        }
        public static string ShowFormTypeList(string FromNo, int SectionNo,string PageName)
        {
            IBShowFrom showform = BLLFactory<IBShowFrom>.GetBLL("ShowFrom");
            string html = "";
            string td = "";
            SortedList al = showform.ShowFormTypeList(FromNo, SectionNo, PageName);
            if (al.Count > 1)
            {
                for (int i = 0; i < al.Count; i++)
                {
                    if (i == 0)
                    {
                        td += "<td id='ShowFormTypeList" + i.ToString() + "' class='navbar2a2'  onclick=\"ShowReprotFromByType('" + FromNo + "','" + SectionNo + "','" + i.ToString() + "');mouseactionset(document.getElementById(document.getElementById('tmpshowtypetd').value),'navbar2a1');document.getElementById('tmpshowtypetd').value=this.id;mouseactionset(this,'navbar2a2');\" onmouseover=\"this.className='navbar2a2'\" onmouseout=\"if(this.id!=document.getElementById('tmpshowtypetd').value){this.className='navbar2a1';}else{this.className='navbar2a2';}\" >" + al.GetByIndex(i).ToString().Trim() + "</td>";
                    }
                    else
                    {
                        td += "<td id='ShowFormTypeList" + i.ToString() + "' class='navbar2a1'  onclick=\"ShowReprotFromByType('" + FromNo + "','" + SectionNo + "','" + i.ToString() + "');mouseactionset(document.getElementById(document.getElementById('tmpshowtypetd').value),'navbar2a1');document.getElementById('tmpshowtypetd').value=this.id;mouseactionset(this,'navbar2a2');\" onmouseover=\"this.className='navbar2a2'\" onmouseout=\"if(this.id!=document.getElementById('tmpshowtypetd').value){this.className='navbar2a1';}else{this.className='navbar2a2';}\" >" + al.GetByIndex(i).ToString().Trim() + "</td>";
                    }
                }
            }
            html = "<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" class='navbar2' ><tr >" + td + "</tr></table>";
            return html;
        }
    }
    public class MyTemplate : ITemplate
    {
        private string strColumnName;
        private DataControlRowType dcrtColumnType;

        public MyTemplate()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /**/
        /// <summary>
        /// 动态添加模版列
        /// </summary>
        /// <param name="strColumnName">列名</param>
        /// <param name="dcrtColumnType">列的类型</param>
        public MyTemplate(string strColumnName, DataControlRowType dcrtColumnType)
        {
            this.strColumnName = strColumnName;
            this.dcrtColumnType = dcrtColumnType;
        }
        public void InstantiateIn(Control ctlContainer)
        {
            switch (dcrtColumnType)
            {
                case DataControlRowType.Header: //列标题
                    Literal ltr = new Literal();
                    ltr.Text = "<input type='checkbox' onclick='SelAll(this);' />";
                    ctlContainer.Controls.Add(ltr);
                    break;
                case DataControlRowType.DataRow: //模版列内容——加载CheckBox 
                    CheckBox cb = new CheckBox();
                    //cb.ID = "CheckBox1";
                    cb.Checked = false;
                    cb.Attributes.Add("onclick", "alert(this.id);");
                    ctlContainer.Controls.Add(cb);
                    break;
            }
        }

    }
}
