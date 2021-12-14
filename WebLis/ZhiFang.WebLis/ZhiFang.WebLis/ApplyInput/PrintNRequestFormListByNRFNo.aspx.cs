using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.IBLL.Report;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.BLLFactory;
using System.Data;
using ZhiFang.Model;
using System.Reflection;
using Tools;

namespace ZhiFang.WebLis.ApplyInput
{
    public partial class PrintNRequestFormListByNRFNo : ZhiFang.WebLis.Class.BasePage
    {
        public int PageSize = 25;  //单页显示最大申请单数：25
        protected string loginID = "";  //登录用户编号
        protected string loginName = "";  //登录用户姓名     
        private readonly Dictionary dic = BLLFactory<Dictionary>.GetBLL("Dictionary");
        private readonly IBBusinessLogicClientControl iblcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
        private readonly IBNRequestForm rfb = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
        private readonly IBBarCodeForm ibbcf = BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
        private readonly IBNRequestItem rib = BLLFactory<IBNRequestItem>.GetBLL("NRequestItem");
        private readonly IBCLIENTELE ibc = BLLFactory<IBCLIENTELE>.GetBLL();
        private readonly IBSickType ibst = BLLFactory<IBSickType>.GetBLL();
        ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User();
        protected void Page_Load(object sender, EventArgs e)
        {
            string WebLisFlagList = "";
            string ParaNRFNO = "";
            List<string> OutParItemNo = new List<string>();
            List<string> InParItemNo = new List<string>();
            if (!base.CheckCookies("ZhiFangUser"))
            {
                string alertStr = "未登录，请登陆后继续！";
                ZhiFang.Common.Public.ScriptStr.Alert(alertStr);
                return;
            }
            else
            {
                string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                user = new ZhiFang.WebLis.Class.User(UserId);
                user.GetOrganizationsList();
                user.GetPostList();
                try
                {
                    if (string.IsNullOrEmpty(base.ReadQueryString("ParaNRFNO")))
                    {
                        string alertStr = "未登录，请登陆后继续！";
                        ZhiFang.Common.Public.ScriptStr.Alert(alertStr);
                        return;
                    }
                    
                    if (string.IsNullOrEmpty(base.ReadQueryString("ClientNo")))
                    {
                        string alertStr = "送检单位为空！";
                        ZhiFang.Common.Public.ScriptStr.Alert(alertStr);
                        return;
                    }
                    this.Label4.Text = ibc.GetModel(long.Parse(base.ReadQueryString("ClientNo"))).CNAME;
                    ZhiFang.Common.Log.Log.Debug("ClientNo:" + base.ReadQueryString("ClientNo") + ",CNAME:" + this.Label4.Text);
                    //#region 开单时间
                    //if (string.IsNullOrEmpty(base.ReadQueryString("txtStartDate")))
                    //{
                    //    string alertStr = "开始时间为空！";
                    //    ZhiFang.Common.Public.ScriptStr.Alert(alertStr);
                    //    return;
                    //}
                    //string txtStartDate = base.ReadQueryString("txtStartDate");
                    //this.Label3.Text = txtStartDate;
                    //ZhiFang.Common.Log.Log.Debug("txtStartDate:" + base.ReadQueryString("txtStartDate"));
                    //if (string.IsNullOrEmpty(base.ReadQueryString("txtEndDate")))
                    //{
                    //    string alertStr = "结束时间为空！";
                    //    ZhiFang.Common.Public.ScriptStr.Alert(alertStr);
                    //    return;
                    //}
                    //string txtEndDate = base.ReadQueryString("txtEndDate");
                    //this.Label3.Text += "--" + txtEndDate;
                    //ZhiFang.Common.Log.Log.Debug("txtStartDate:" + base.ReadQueryString("txtStartDate")+ ",txtEndDate: " + base.ReadQueryString("txtEndDate"));
                    //#endregion
                    this.Label3.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    List<string> NRFNOlist = base.ReadQueryString("ParaNRFNO").Split(',').ToList();

                    #region 申请单状态
                    if (base.ReadQueryString("WebLisFlagList") != null && base.ReadQueryString("WebLisFlagList") != "-1")
                    {
                        WebLisFlagList = base.ReadQueryString("WebLisFlagList");
                    }
                    #endregion

                    #region 排除的明细项目
                    if (base.ReadQueryString("OutParItemNo") != null && base.ReadQueryString("OutParItemNo").Trim() != "")
                    {
                        OutParItemNo = base.ReadQueryString("OutParItemNo").Trim().Split(',').ToList();
                    }
                    #endregion

                    #region 包含的明细项目
                    if (base.ReadQueryString("InParItemNo") != null && base.ReadQueryString("InParItemNo").Trim() != "")
                    {
                        InParItemNo = base.ReadQueryString("InParItemNo").Trim().Split(',').ToList();
                    }
                    #endregion

                    PrintNRequestForm(NRFNOlist, WebLisFlagList, OutParItemNo, InParItemNo);
                }
                catch (Exception eee)
                {
                    ZhiFang.Common.Log.Log.Error("PrintNRequestFormListByNRFNo.异常："+eee.ToString());
                    Response.Write(eee.ToString());
                    Response.End();
                }
            }
        }
        public void PrintNRequestForm(List<string> NRFNOlist, string WebLisFlagList, List<string> OutParItemNo, List<string> InParItemNo)
        {
            ZhiFang.Model.NRequestForm nrf_m = new Model.NRequestForm();
            
            if (!string.IsNullOrWhiteSpace(WebLisFlagList))
            {
                nrf_m.WeblisflagList = WebLisFlagList.Trim();
            }
            if (NRFNOlist!=null && NRFNOlist.Count>0)
            {
                nrf_m.NRFNOlist = NRFNOlist;
            }
            DataTable dt = new DataTable();
            int iCount, intPageCount;
            dt = rfb.GetNRequstFormList(nrf_m, 0, 10000, out intPageCount, out iCount);
            if (dt != null)
            {
                //FillList(dt);
                FillList(dt, OutParItemNo, InParItemNo);
            }
        }

        #region 向页面填充申请单信息
        /// <summary>
        /// 向页面填充申请单信息 
        /// </summary>
        /// <param name="dt"></param>
        public void FillList(DataTable dt)
        {
            //List_Table
            int k = 0;
            int i = 0;
            //dt.DefaultView.Sort = " OperDate asc ";
            //DataView dv= dt.DefaultView;

            string orderstr = "LABUPLOADDATE";
            #region 排序字段
            if (base.ReadQueryString("OrderField") != null && base.ReadQueryString("OrderField").Trim() != "")
            {
                orderstr = base.ReadQueryString("OrderField");
            }
            #endregion
            var drlist = dt.AsEnumerable().OrderBy(a => a[orderstr]);

            foreach (DataRow dr in drlist)
            {
                i++;
                string b = rfb.GetBarCodeByNRequestFormNo(dr["NRequestFormNo"].ToString().Trim());
                var barcode = "";
                foreach (var a in b.Split(','))
                {
                    barcode += a + "</br>";
                    k++;
                }
                //barcode = barcode.Substring(0, barcode.Length - barcode.LastIndexOf("</br>"));
                System.Web.UI.HtmlControls.HtmlTableRow tr = new System.Web.UI.HtmlControls.HtmlTableRow();
                System.Web.UI.HtmlControls.HtmlTableCell tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTable table = new System.Web.UI.HtmlControls.HtmlTable();

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = i.ToString();
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                if (dr["OperTime"].ToString().Trim().Length > 0)
                {
                    tc.InnerHtml = Convert.ToDateTime(dr["OperTime"].ToString().Trim()).ToShortDateString();
                }
                else
                {
                    tc.InnerHtml = "&nbsp;";
                }
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = barcode;
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = dr["TestTypeName"].ToString().Trim();
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                if (dr["jztype"].ToString().Trim().Length > 0)
                {
                    SickType st = ibst.GetModel(int.Parse(dr["jztype"].ToString().Trim()));
                    if (st != null)
                    {
                        tc.InnerHtml = st.CName;
                    }
                    else
                    {
                        tc.InnerHtml = "&nbsp;";
                    }
                    //if (dr["jztype"].ToString().Trim() == "1")
                    //{
                    //    tc.InnerHtml = "住院";
                    //}
                    //if (dr["jztype"].ToString().Trim() == "2")
                    //{
                    //    tc.InnerHtml = "门诊";
                    //}
                    //if (dr["jztype"].ToString().Trim() == "3")
                    //{
                    //    tc.InnerHtml = "体检";
                    //}
                }
                else
                {
                    tc.InnerHtml = "&nbsp;";
                }
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = dr["PatNo"].ToString().Trim();
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = dr["CName"].ToString().Trim();
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = dr["Sex"].ToString().Trim();
                tr.Cells.Add(tc);
                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                if (dr["Age"].ToString().Trim() == "200")
                {
                    tc.InnerHtml = "成人";
                }
                else
                {
                    tc.InnerHtml = dr["Age"].ToString().Trim() + dr["AgeUnitName"].ToString().Trim();
                }
                tr.Cells.Add(tc);
                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                //tc.InnerHtml = dr["SampleName"].ToString().Trim();
                tc.InnerHtml = dr["DoctorName"].ToString().Trim();
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                DataSet dsri = new DataSet();
                string htmltmp = "";
                try
                {
                    dsri = rib.GetList(new ZhiFang.Model.NRequestItem() { NRequestFormNo = long.Parse(dr["NRequestFormNo"].ToString().Trim()) });

                    DataTable ndt = new DataTable();
                    if (dsri != null && dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                    {
                        //去重复列
                        //for (int d = 0; d < dsri.Tables[0].Rows.Count; d++)
                        //{ 
                        //ndt = GetDistinctTable(dsri.Tables[0], dsri.Tables[0].Columns[d].ColumnName);
                        //去掉重复行
                        ndt = GetDistinctTable(dsri.Tables[0], "");

                        //}
                        //DataTable dtt = sqldbweblis.ExecDT("select dbo.TestItem.CName from dbo.TestItem INNER JOIN dbo.NRequestItem ON dbo.TestItem.ItemNo = dbo.NRequestItem.ParItemNo where BarCodeFormNo=" + dr["BarCodeFormNo"].ToString().Trim());
                        //string htmltmp = "";
                        DataSet nds = new DataSet();
                        nds.Tables.Add(ndt);
                        List<string> itemcname = new List<string>();
                        for (int j = 0; j < nds.Tables[0].Rows.Count; j++)
                        {
                            itemcname.Add(nds.Tables[0].Rows[j]["CName"].ToString().Trim());
                            //htmltmp += nds.Tables[0].Rows[j]["CName"].ToString().Trim() + ",</br>";
                        }
                        if (itemcname.Count > 0)
                        {
                            htmltmp = string.Join(",</br>", itemcname.ToArray());
                        }
                    }
                }
                catch (Exception e)
                {
                    ZhiFang.Common.Log.Log.Error(e.StackTrace + e.ToString());
                }

                tc.InnerHtml = htmltmp;

                tr.Cells.Add(tc);



                //tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                //tc.Align = "center";
                //tc.InnerHtml = dr["WebLisSourceOrgName"].ToString().Trim();
                //tr.Cells.Add(tc);


                //tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                //tc.Align = "center";
                //tc.InnerHtml = dr["ClientName"].ToString().Trim();
                //tr.Cells.Add(tc);

                //tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                //tc.Align = "center";
                //string Diag = dr["Diag"].ToString().Trim();

                //Diag = ZhiFang.Tools.Tools.StringLength(Diag, 8);
                //tc.InnerHtml = Diag;
                //tr.Cells.Add(tc);
                tr.Style.Add("font-size", "12px");
                tr.Style.Add("padding-top", "2px");
                List_Table.Rows.Add(tr);
                System.Web.UI.HtmlControls.HtmlTableRow trline = new System.Web.UI.HtmlControls.HtmlTableRow();
                System.Web.UI.HtmlControls.HtmlTableCell tcline = new System.Web.UI.HtmlControls.HtmlTableCell();
                tcline.ColSpan = 10;
                tcline.InnerHtml = "<hr />";
                trline.Cells.Add(tcline);
                List_Table.Rows.Add(trline);
            }
            //this.Label6.Text = dt.Rows.Count.ToString();//获取总条数
            this.Label6.Text = k.ToString();//同上
            //this.Label6.Text = 3.ToString();//测试
        }
        /// <summary>
        /// 获取对固定列不重复的新DataTable
        /// </summary>
        /// <param name="dt">含有重复数据的DataTable</param>
        /// <param name="colName">需要验证重复的列名</param>
        /// <returns>新的DataTable，colName列不重复，表格式保持不变</returns>
        private DataTable GetDistinctTable(DataTable dt, string colName)
        {
            try
            {
                colName = "CName";
                DataView dv = dt.DefaultView;
                DataTable dtCardNo = dv.ToTable(true, colName);
                DataTable Pointdt = new DataTable();
                Pointdt = dv.ToTable();
                Pointdt.Clear();
                var query = from t in dt.AsEnumerable()
                            group t by new { t1 = t.Field<string>("CName") } into m
                            select new
                            {
                                CName = m.Key.t1
                            };
                Pointdt = DataTableHelper.CopyToDataTablea(query);
                return Pointdt;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.StackTrace + e.ToString());
                return null;
            }
        }
        /// <summary>
        /// 向页面填充申请单信息 
        /// </summary>
        /// <param name="dt"></param>
        public void FillList(DataTable dt, List<string> OutParItemNo, List<string> InParItemNo)
        {
            //List_Table
            int k = 0;
            int i = 0;
            //dt.DefaultView.Sort = " OperDate asc ";
            //DataView dv= dt.DefaultView;
            List<string> listbarcode = new List<string>();
            string orderstr = "LABUPLOADDATE";
            #region 排序字段
            if (base.ReadQueryString("OrderField") != null && base.ReadQueryString("OrderField").Trim() != "")
            {
                orderstr = base.ReadQueryString("OrderField");
            }
            #endregion
            var drlist = dt.AsEnumerable().OrderBy(a => a[orderstr]);

            foreach (DataRow dr in drlist)
            {
                var b = rfb.GetBarCodeListByNRequestFormNo(dr["NRequestFormNo"].ToString().Trim());
                var barcode = "";
                foreach (var a in b)
                {
                    #region 组套项目包含验证
                    if (OutParItemNo.Count > 0)
                    {
                        bool flag = false;
                        foreach (var opi in OutParItemNo)
                        {
                            //ZhiFang.Common.Log.Log.Debug("1.opi=" + opi);
                            if (a.ItemNo.IndexOf(opi) >= 0)
                            {
                                //ZhiFang.Common.Log.Log.Debug("2.opi=" + opi + ",ItemNo = " + a.ItemNo);
                                flag = true;
                                break;
                            }
                        }
                        if (flag)
                        {
                            //ZhiFang.Common.Log.Log.Debug("3");
                            continue;
                        }
                    }
                    if (InParItemNo.Count > 0)
                    {
                        bool flag = false;
                        foreach (var opi in InParItemNo)
                        {
                            //ZhiFang.Common.Log.Log.Debug("4.opi=" + opi);
                            if (a.ItemNo.IndexOf(opi) >= 0)
                            {
                                //ZhiFang.Common.Log.Log.Debug("5.opi=" + opi + ",ItemNo = " + a.ItemNo);
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            // ZhiFang.Common.Log.Log.Debug("6");
                            continue;
                        }
                    }
                    #endregion
                    barcode += a.BarCode + "</br>";
                    if (!listbarcode.Contains(a.BarCode))
                    {
                        listbarcode.Add(a.BarCode);
                    }
                    k++;
                }
                if (barcode == "")//一个条码没有则跳出
                    continue;
                //barcode = barcode.Substring(0, barcode.Length - barcode.LastIndexOf("</br>"));
                System.Web.UI.HtmlControls.HtmlTableRow tr = new System.Web.UI.HtmlControls.HtmlTableRow();
                System.Web.UI.HtmlControls.HtmlTableCell tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTable table = new System.Web.UI.HtmlControls.HtmlTable();
                i++;
                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = i.ToString();
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                if (dr["OperTime"].ToString().Trim().Length > 0)
                {
                    tc.InnerHtml = Convert.ToDateTime(dr["OperTime"].ToString().Trim()).ToShortDateString();
                }
                else
                {
                    tc.InnerHtml = "&nbsp;";
                }
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = barcode;
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = dr["TestTypeName"].ToString().Trim();
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                if (dr["jztype"].ToString().Trim().Length > 0)
                {
                    SickType st = ibst.GetModel(int.Parse(dr["jztype"].ToString().Trim()));
                    if (st != null)
                    {
                        tc.InnerHtml = st.CName;
                    }
                    else
                    {
                        tc.InnerHtml = "&nbsp;";
                    }
                }
                else
                {
                    tc.InnerHtml = "&nbsp;";
                }
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = dr["PatNo"].ToString().Trim();
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = dr["CName"].ToString().Trim();
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = dr["Sex"].ToString().Trim();
                tr.Cells.Add(tc);
                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                if (dr["Age"].ToString().Trim() == "200")
                {
                    tc.InnerHtml = "成人";
                }
                else
                {
                    tc.InnerHtml = dr["Age"].ToString().Trim() + dr["AgeUnitName"].ToString().Trim();
                }
                tr.Cells.Add(tc);
                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                //tc.InnerHtml = dr["SampleName"].ToString().Trim();
                tc.InnerHtml = dr["DoctorName"].ToString().Trim();
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = dr["PersonID"].ToString().Trim();
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                DataSet dsri = new DataSet();
                string htmltmp = "";
                try
                {
                    dsri = rib.GetList(new ZhiFang.Model.NRequestItem() { NRequestFormNo = long.Parse(dr["NRequestFormNo"].ToString().Trim()) });
                    if (dsri != null && dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                    {

                        //去重复列
                        //for (int d = 0; d < dsri.Tables[0].Rows.Count; d++)
                        //{ 
                        //ndt = GetDistinctTable(dsri.Tables[0], dsri.Tables[0].Columns[d].ColumnName);
                        //去掉重复行
                        //ndt = GetDistinctTable(dsri.Tables[0], "");

                        //}
                        //DataTable dtt = sqldbweblis.ExecDT("select dbo.TestItem.CName from dbo.TestItem INNER JOIN dbo.NRequestItem ON dbo.TestItem.ItemNo = dbo.NRequestItem.ParItemNo where BarCodeFormNo=" + dr["BarCodeFormNo"].ToString().Trim());
                        //string htmltmp = "";
                        List<string> itemcname = new List<string>();
                        //for (int aaa=0; aaa < dsri.Tables[0].Columns.Count; aaa++)
                        //{
                        //    ZhiFang.Common.Log.Log.Debug("dsri.Tables[0].Columns:" + dsri.Tables[0].Columns[aaa].ColumnName);
                        //}
                        for (int j = 0; j < dsri.Tables[0].Rows.Count; j++)
                        {
                            if (OutParItemNo.Count > 0)
                            {
                                ZhiFang.Common.Log.Log.Debug("1.OutParItemNo:" + string.Join(",", OutParItemNo) + ",ItemNo:" + dsri.Tables[0].Rows[j]["ItemNo"].ToString().Trim());
                                if (OutParItemNo.Contains(dsri.Tables[0].Rows[j]["ItemNo"].ToString().Trim()))
                                {
                                    ZhiFang.Common.Log.Log.Debug("2.OutParItemNo:" + string.Join(",", OutParItemNo) + ",ItemNo:" + dsri.Tables[0].Rows[j]["ItemNo"].ToString().Trim());
                                    continue;
                                }
                            }
                            if (InParItemNo.Count > 0)
                            {
                                ZhiFang.Common.Log.Log.Debug("1.InParItemNo:" + string.Join(",", InParItemNo) + ",ItemNo:" + dsri.Tables[0].Rows[j]["ItemNo"].ToString().Trim());
                                if (!InParItemNo.Contains(dsri.Tables[0].Rows[j]["ItemNo"].ToString().Trim()))
                                {
                                    ZhiFang.Common.Log.Log.Debug("2.InParItemNo:" + string.Join(",", InParItemNo) + ",ItemNo:" + dsri.Tables[0].Rows[j]["ItemNo"].ToString().Trim());
                                    continue;
                                }
                            }
                            if (!itemcname.Contains(dsri.Tables[0].Rows[j]["CName"].ToString().Trim()))
                            {
                                itemcname.Add(dsri.Tables[0].Rows[j]["CName"].ToString().Trim());
                            }
                        }
                        if (itemcname.Count > 0)
                        {
                            htmltmp = string.Join(",</br>", itemcname.ToArray());
                        }
                    }
                }
                catch (Exception e)
                {
                    ZhiFang.Common.Log.Log.Error(e.StackTrace + e.ToString());
                }

                tc.InnerHtml = htmltmp;

                tr.Cells.Add(tc);



                //tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                //tc.Align = "center";
                //tc.InnerHtml = dr["WebLisSourceOrgName"].ToString().Trim();
                //tr.Cells.Add(tc);


                //tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                //tc.Align = "center";
                //tc.InnerHtml = dr["ClientName"].ToString().Trim();
                //tr.Cells.Add(tc);

                //tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                //tc.Align = "center";
                //string Diag = dr["Diag"].ToString().Trim();

                //Diag = ZhiFang.Tools.Tools.StringLength(Diag, 8);
                //tc.InnerHtml = Diag;
                //tr.Cells.Add(tc);
                tr.Style.Add("font-size", "12px");
                tr.Style.Add("padding-top", "2px");
                List_Table.Rows.Add(tr);
                System.Web.UI.HtmlControls.HtmlTableRow trline = new System.Web.UI.HtmlControls.HtmlTableRow();
                System.Web.UI.HtmlControls.HtmlTableCell tcline = new System.Web.UI.HtmlControls.HtmlTableCell();
                tcline.ColSpan = 12;
                tcline.InnerHtml = "<hr />";
                trline.Cells.Add(tcline);
                List_Table.Rows.Add(trline);
            }
            BarCodeList.Text = string.Join(",", listbarcode);
            //this.Label6.Text = dt.Rows.Count.ToString();//获取总条数
            this.Label6.Text = k.ToString();//同上
            //this.Label6.Text = 3.ToString();//测试
        }
        #endregion
    }
}