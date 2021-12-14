using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZhiFang.IBLL.Report;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.BLLFactory;
using ZhiFang.WebLis.Class;
using System.Configuration;
using ZhiFang.Model;
using System.Text;



namespace ZhiFang.WebLis.ApplyInput
{
    public partial class SubscripList_jiayin : ZhiFang.WebLis.Class.BasePage
    {
        public int PageSize = 25;  //单页显示最大申请单数：25
        protected string loginID = "";  //登录用户编号
        protected string loginName = "";  //登录用户姓名     
        private readonly Dictionary dic = BLLFactory<Dictionary>.GetBLL("Dictionary");
        private readonly IBBusinessLogicClientControl iblcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
        private readonly IBBarCodeForm ibbcf = BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
        private readonly IBNRequestForm rfb = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
        private readonly IBNRequestItem rib = BLLFactory<IBNRequestItem>.GetBLL("NRequestItem");
        private readonly IBCLIENTELE ibc = BLLFactory<IBCLIENTELE>.GetBLL();
        ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User();
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ZhiFang.WebLis.Ashx.ApplyInput));
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ZhiFang.WebLis.ApplyInput.SubscripList_jiayin));
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
                try
                {
                    this.hiddenInputClientNo.Value = "-1";
                    if (user.OrganizationsList.Count > 0)
                    {
                        if (user.OrganizationsList.ElementAt(0).Value != null && user.OrganizationsList.ElementAt(0).Value.Count() > 0)
                        {
                            this.hiddenInputClientNo.Value = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                        }
                    }
                }
                catch
                {
                    this.hiddenInputClientNo.Value = "-1";
                }
            }
            if (!Page.IsPostBack)
            {
                Model.BusinessLogicClientControl BusinessLogicClientControl = new Model.BusinessLogicClientControl();
                BusinessLogicClientControl.Account = user.Account;
                BusinessLogicClientControl.Flag = 1;
                BusinessLogicClientControl.SelectedFlag = true;
                DataSet dsClient = iblcc.GetList(BusinessLogicClientControl);
                string strClient = "";
                if (dsClient.Tables != null && dsClient.Tables[0].Rows.Count > 0)
                {
                    strClient = dsClient.Tables[0].Rows[0]["ClientNo"].ToString();
                }
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DefaultClientFlag") == "1")
                {
                    DataSet ds = user.GetClientListByPost("", -1);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            System.Web.UI.HtmlControls.HtmlInputText hittcn = (System.Web.UI.HtmlControls.HtmlInputText)Page.FindControl("txtClientNo");
                            hittcn.Attributes.Remove("onfocus");
                            hittcn.Attributes.Remove("onkeydown");
                            hittcn.Attributes.Remove("onpropertychange");
                            hittcn.Attributes.Remove("onblur");
                            hittcn.Attributes.Add("readonly", "readonly");
                            hittcn.Attributes.Add("disabled", "disabled");

                            
                        }
                    }
                }
                ViewState["CurPage"] = "0";     //当前页码
                ViewState["PageCount"] = "0";   //总页数
                string FD = DateTime.Now.ToString("yyyy-MM-dd");
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("CPdate") == "1")
                {
                    txtStartDate.Value = FD;
                    txtEndDate.Value = FD;
                }
                else
                {
                    txtCollectStartDate.Value = FD;
                    txtCollectEndDate.Value = FD;
                }
                SearchTest(0);
            }
        }

        //public DataTable GetAllDataTable(string where, int iStartPage, int iPageSize, out int StartPage, out int iCount)
        //{
        //    string sql = "SELECT distinct BarCodeForm.BarCode as BarCode, BarCodeForm.BarCodeFormNo as BarCodeFormNo,BarCodeForm.PrintCount as PrintCount, NRequestForm.CName as CName,NRequestForm.DoctorName as DoctorName,NRequestForm.WebLisSourceOrgName as WebLisSourceOrgName,NRequestForm.ClientName as ClientName,NRequestForm.AgeUnitName as AgeUnitName, NRequestForm.Diag as Diag,GenderType.CName AS Sex, " +
        //                "NRequestForm.Age as Age, SampleType.CName AS SampleName, Convert(varchar(10),BarCodeForm.CollectDate,21)+' '+Convert(varchar(10),BarCodeForm.CollectTime,8)as CollectTime, " +
        //                "NRequestForm.OperDate+NRequestForm.OperTime as OperTime,PatDiagInfo.DiagDesc as DiagDesc " +
        //                "FROM GenderType RIGHT OUTER JOIN " +
        //                "NRequestItem INNER JOIN " +
        //                "NRequestForm ON " +
        //                "NRequestItem.NRequestFormNo = NRequestForm.NRequestFormNo LEFT OUTER JOIN " +
        //                "PatDiagInfo ON NRequestForm.DiagNo = PatDiagInfo.DiagNo LEFT OUTER JOIN " +
        //                "SampleType ON NRequestForm.SampleTypeNo = SampleType.SampleTypeNo ON  " +
        //                "GenderType.GenderNo = NRequestForm.GenderNo LEFT OUTER JOIN " +
        //                "BarCodeForm ON NRequestItem.BarCodeFormNo = BarCodeForm.BarCodeFormNo ";
        //    if (where != "")
        //    {
        //        sql += " where " + where;
        //    }

        //    try
        //    {
        //        DataSet dSet = sqldbweblis.ExecDSPages(sql, iStartPage, iPageSize, out StartPage, out iCount);

        //        if (dSet.Tables.Count > 0)
        //        {
        //            return dSet.Tables[0];
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch
        //    {
        //        StartPage = 0;
        //        iCount = 0;
        //        return null;
        //    }
        //}

        public void SearchTest(int StartPage)
        {
            ZhiFang.Model.NRequestForm nrf_m = new Model.NRequestForm();
            Model.BusinessLogicClientControl BusinessLogicClientControl = new Model.BusinessLogicClientControl();
            BusinessLogicClientControl.Account = user.Account;
            BusinessLogicClientControl.Flag = 1;
            BusinessLogicClientControl.SelectedFlag = true;
            DataSet ds = iblcc.GetList(BusinessLogicClientControl);
            if(ds.Tables[0].Rows.Count>0)
            {
                nrf_m.ClientNo = ds.Tables[0].Rows[0]["ClientNo"].ToString();
            }
            if (hiddenInputClientNo.Value.Trim() != "")
            {
                //nrf_m.ClientNo = hiddenInputClientNo.Value;
            }
            //if (hiddenClient.Value.Trim() != "")
            //{
            //    nrf_m.WebLisSourceOrgID = hiddenClient.Value;
            //}
            if (txtClientNo.Value.Trim() != "")
            {
                nrf_m.ClientName = txtClientNo.Value;
            }
            if (SelectDoctor.Value.Trim() != "")
            {
                nrf_m.DoctorName = SelectDoctor.Value;
            }
            if (txtPatientName.Text.Trim() != "")
            {
                nrf_m.CName = txtPatientName.Text;
            }
            if (txtPatientID.Text.Trim() != "")
            {
                nrf_m.PatNo = txtPatientID.Text;
            }
            if (txtStartDate.Value.Trim() != "")
            {
                nrf_m.OperDateStart = txtStartDate.Value;
            }
            if (txtEndDate.Value.Trim() != "")
            {
                nrf_m.OperDateEnd = txtEndDate.Value + " 23:59:59";
            }
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("CPdate") == "")
            {
                if (txtCollectStartDate.Value.Trim() != "")
                {
                    nrf_m.CollectDateStart = txtCollectStartDate.Value;
                }
                if (txtCollectEndDate.Value.Trim() != "")
                {
                    nrf_m.CollectDateEnd = txtCollectEndDate.Value + " 23:59:59";
                }
            }
            nrf_m.IsOnlyNoBar = false;
            if (chkOnlyNoPrintBarCode.Checked)
            {
                nrf_m.IsOnlyNoBar = true;
            }
            string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
            user = new ZhiFang.WebLis.Class.User(UserId);
            user.GetPostList();
            if (user.PostList.Exists(l => { if (l.ToUpper() == "WEBLISADMIN")return true; else return false; }))
            {

            }
            else
            {
                if (user.PostList.Exists(l => { if (l.ToUpper() == "LOGISTICSOFFICER")return true; else return false; }) || user.PostList.Exists(l => { if (l.ToUpper() == "WEBLISAPPLYINPUT")return true; else return false; }))
                {
                    DataSet dsClinent = user.GetClientListByPost("", -1); ;
                    for (int i = 0; i < dsClinent.Tables[0].Rows.Count; i++)
                    {
                        nrf_m.ClientList += " '" + dsClinent.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
                    }
                    nrf_m.ClientList = nrf_m.ClientList.Remove(nrf_m.ClientList.Length - 1);
                }
                else
                {
                    nrf_m.ClientList += " -1 ";
                }
            }
            #region string where = "";
            //if (SJDW != "")
            //{
            //    where += "NRequestForm.WebLisSourceOrgID='" + SJDW + "' and ";
            //}
            //if (doctor != "")
            //{
            //    where += "NRequestForm.DoctorName='" + doctor + "' and ";
            //}
            //if (name != "")
            //{
            //    where += "NRequestForm.CName='" + name + "' and ";
            //}
            //if (pid != "")
            //{
            //    where += "NRequestForm.PatNo='" + pid + "' and ";
            //}





            //    if (user.getDeptPosName() != null)
            //    {
            //        if (user.getDeptPosName()[0].Trim() != "山西高科")//（用户）属于中心
            //        {
            //            where += "(NRequestForm.WebLisSourceOrgID in (select WebLisSourceOrgID from CLIENTELE where GroupName='" + user.getDeptPosName()[0].Trim() + "')) and ";
            //        }
            //    }
            //    else
            //    {
            //        where += " 1=2 and ";
            //    }
            #endregion
            DataTable dt = new DataTable();
            int iCount, intPageCount;
            dt = rfb.GetAllData(nrf_m, StartPage, PageSize, out intPageCount, out iCount);

            int pCount = iCount / PageSize;
            if ((iCount % PageSize) != 0)
            {
                pCount++;
            }

            ddlNewPage.Items.Clear();
            for (int i = 0; i < pCount; i++)
            {
                ddlNewPage.Items.Add(new ListItem((i + 1).ToString(), (i + 1).ToString()));
            }

            ViewState["PageCount"] = pCount.ToString();//总页数

            for (int r = 1; r < List_Table.Rows.Count; r++)
            {
                //清空页面上原有申请单信息
                List_Table.Rows.RemoveAt(r);
            }
            labelCurPage.Text = (int.Parse(ViewState["CurPage"].ToString()) + 1).ToString();
            LinkBPrePage.Enabled = true;
            LinkBNextPage.Enabled = true;
            if (ViewState["CurPage"].ToString() == "1")
            {
                LinkBPrePage.Enabled = false;
            }
            if (ViewState["CurPage"].ToString() == ViewState["PageCount"].ToString())
            {
                LinkBNextPage.Enabled = false;
            }
            if (dt != null)
            {
                FillList(dt);
                hiddenTestCount.Value = dt.Rows.Count.ToString();
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
            int i = 0;
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    i++;
                    string barcode = dr["BarCode"].ToString().Trim();
                    System.Web.UI.HtmlControls.HtmlTableRow tr = new System.Web.UI.HtmlControls.HtmlTableRow();
                    System.Web.UI.HtmlControls.HtmlTableCell tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                    tc.InnerHtml = "<input name='checkbox' type='checkbox' id='chk_" + i + "' value='" + barcode + "' checked='checked' />";

                    tr.Cells.Add(tc);
                    tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                    tc.Align = "center";
                    tc.InnerHtml = barcode;
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
                    tc.InnerHtml = dr["Age"].ToString().Trim() + dr["AgeUnitName"].ToString().Trim();
                    tr.Cells.Add(tc);
                    tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                    tc.Align = "center";
                    tc.InnerHtml = dr["SampleName"].ToString().Trim();
                    tr.Cells.Add(tc);
                    tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                    tc.Align = "center";

                    ZhiFang.Common.Log.Log.Info(DateTime.Now + "ReportItem数据查询开始");
                    DataSet dsri = rib.GetList(new ZhiFang.Model.NRequestItem() { BarCodeFormNo = long.Parse(dr["BarCodeFormNo"].ToString().Trim()) });
                    ZhiFang.Common.Log.Log.Info(DateTime.Now + "ReportItem数据查询结束");
                    string htmltmp = "";
                    string shortCode = "";
                    if (dsri != null && dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dsri.Tables[0].Rows.Count; j++)
                        {
                            htmltmp += dsri.Tables[0].Rows[j]["CName"].ToString().Trim() + ",";
                            shortCode += dsri.Tables[0].Rows[j]["ShortCode"].ToString().Trim() + ",";
                        }
                        if (htmltmp.Length > 0)
                        {
                            htmltmp = htmltmp.Substring(0, htmltmp.Length - 1);
                        }
                        if (shortCode.Length > 0)
                        {
                            shortCode = shortCode.Substring(0, shortCode.Length - 1);
                        }
                    }
                    tc.InnerHtml = htmltmp;
                    tr.Cells.Add(tc);

                    tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                    tc.Align = "center";
                    tc.InnerHtml = dr["DoctorName"].ToString().Trim();
                    tr.Cells.Add(tc);

                    tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                    tc.Align = "center";
                    if (dr["OperTime"].ToString().Trim().Length > 0)
                    {
                        tc.InnerHtml = dr["OperTime"].ToString().Trim().Substring(0, dr["OperTime"].ToString().Trim().Length - 3);
                    }
                    else
                    {
                        tc.InnerHtml = "&nbsp;";
                    }
                    tr.Cells.Add(tc);




                    tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                    tc.Align = "center";
                    if (dr["CollectTime"].ToString().Trim().Length > 0)
                    {
                        tc.InnerHtml = dr["CollectTime"].ToString().Trim().Substring(0, dr["CollectTime"].ToString().Trim().Length - 3);
                    }
                    else
                    {
                        tc.InnerHtml = "&nbsp;";
                    }
                    tr.Cells.Add(tc);



                    tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                    tc.Align = "center";
                    tc.InnerHtml = dr["ClientName"].ToString().Trim();
                    //CLIENTELE CLIENTELE = new Model.CLIENTELE();
                    //CLIENTELE.ClIENTNO = Convert.ToInt32(dr["WebLisSourceOrgID"]);
                    //DataSet dsSourceId = ibc.GetList(CLIENTELE);
                    tr.Cells.Add(tc);
                    tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                    tc.Align = "center";
                    tc.InnerHtml = dr["WebLisSourceOrgName"].ToString().Trim();

                    tr.Cells.Add(tc);

                    tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                    tc.Align = "center";
                    string Diag = dr["Diag"].ToString().Trim();

                    Diag = ZhiFang.Tools.Tools.StringLength(Diag, 8);
                    tc.InnerHtml = Diag;
                    tr.Cells.Add(tc);



                    //打印按钮

                    tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                    tc.InnerHtml = "<a href='#' onclick='ModifyTest(\"" + barcode + "\")'>修改</a>";
                    tr.Cells.Add(tc);
                    tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                    tc.InnerHtml = "<a href='#' onclick='DeleteTest(\"" + barcode + "\")'>删除</a>";
                    tr.Cells.Add(tc);
                    string ConnectStr = ConfigurationManager.ConnectionStrings["WebLisDB"].ToString();
                    //string ModelPrint = "E//条码模板.frx";
                    if (base.ReadQueryString("BarCodeInputFlag") != "1")
                    {
                        tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                        //tc.InnerHtml = "<a href='#' id='p" + i.ToString() + "' onclick=\"PrintCurBarCode('" + barcode + "','" + dr["CName"].ToString().Trim() + "','" + dr["Sex"].ToString().Trim() + "','" + dr["Age"].ToString().Trim() + "','" + dr["AgeUnitName"].ToString().Trim() + "','','" + dr["CollectTime"].ToString().Trim().Substring(0, dr["CollectTime"].ToString().Trim().Length - 3) + "','" + htmltmp + "','" + dr["WebLisSourceOrgName"].ToString().Trim() + "','" + dr["SampleName"].ToString().Trim() + "',1,'')\">打印</a>";
                        tc.InnerHtml = "<a href='#' id='p" + i.ToString() + "' onclick=\"PrintCurBarCode('" + barcode + "','" + ConnectStr + "','" + htmltmp + "','" + shortCode + "',1,'')\">打印</a>";
                        //tc.InnerHtml = "<a href='#' id='p" + i.ToString() + "' onclick=\"PrintCurBarCode1('" + barcode + "','" + dr["CName"].ToString().Trim() +
                        //    "','" + dr["Sex"].ToString().Trim() + "','" + dr["Age"].ToString().Trim() + "','" + dr["AgeUnitName"].ToString().Trim() + "','','" +
                        //    dr["CollectTime"].ToString().Trim().Substring(0, dr["CollectTime"].ToString().Trim().Length - 3) + "','" + htmltmp + "','" + dsSourceId.Tables[0].Rows[0]["SHORTCODE"].ToString().Trim() +
                        //    "','" + dr["SampleName"].ToString().Trim() + "','" + dsClient.Tables[0].Rows[0]["SHORTCODE"].ToString().Trim() + "','" + ConnectStr + "','" + shortCode + "',1,'')\">打印</a>";
                        ZhiFang.Common.Log.Log.Info(tc.InnerHtml);
                        tr.Cells.Add(tc);
                    }
                    string BgColor = "#a3f1f5";

                    string PrintCount = dr["PrintCount"].ToString().Trim();
                    if (PrintCount != "0" && PrintCount != "NULL" && PrintCount != "")
                    {
                        BgColor = "#a2afbf";
                    }

                    tr.BgColor = BgColor;
                    tr.Attributes.Add("onmouseover", "SetRowFocus(this,'#34a6a3')");
                    tr.Attributes.Add("onmouseout", "SetRowFocus(this,'" + BgColor + "')");

                    List_Table.Rows.Add(tr);
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(DateTime.Now + ex.StackTrace + ex.ToString() + ex.Message);
            }
        }
        #endregion
        #region 保存打印成功标记
        protected void saveprintcount_onclick(object sender, EventArgs e)
        {
            IBBarCodeForm ibbf = BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
            DataSet ds = ibbf.GetList(new Model.BarCodeForm() { BarCode = this.hiddentmpBarCode.Value.ToString().Trim() });
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns.Contains("PrintCount") && ds.Tables[0].Rows[0]["PrintCount"].ToString().Trim() != "")
            {
                ibbf.UpdatePrintFlag(new Model.BarCodeForm() { PrintCount = Convert.ToInt32(ds.Tables[0].Rows[0]["PrintCount"]) + 1, BarCode = this.hiddentmpBarCode.Value.ToString().Trim() }).ToString();
            }
            else
            {
                ibbf.UpdatePrintFlag(new Model.BarCodeForm() { PrintCount = 1, BarCode = this.hiddentmpBarCode.Value.ToString().Trim() }).ToString();
            }
            if (Tools.Validate.IsNum(this.hiddentmpBarCode.Value.ToString().Trim()))
            {
                ds = ibbf.GetList(new Model.BarCodeForm() { BarCodeFormNo = Convert.ToInt64(this.hiddentmpBarCode.Value.ToString().Trim()) });
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns.Contains("PrintCount") && ds.Tables[0].Rows[0]["PrintCount"].ToString().Trim() != "")
                {
                    ibbf.UpdatePrintFlag(new Model.BarCodeForm() { PrintCount = Convert.ToInt32(ds.Tables[0].Rows[0]["PrintCount"]) + 1, BarCode = this.hiddentmpBarCode.Value.ToString().Trim() }).ToString();
                }
                else
                {
                    ibbf.UpdatePrintFlag(new Model.BarCodeForm() { PrintCount = 1, BarCode = this.hiddentmpBarCode.Value.ToString().Trim() }).ToString();
                }
            }
            ViewState["CurPage"] = "0";//当前页码
            SearchTest(0);
        }
        #endregion
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ViewState["CurPage"] = "0";//当前页码
            SearchTest(0);
        }
        protected void LinkBPrePage_Click(object sender, EventArgs e)
        {
            string CurPage = ViewState["CurPage"].ToString();       //当前页码
            string PageCount = ViewState["PageCount"].ToString();   //总页数

            try
            {
                int intCurPage = int.Parse(CurPage);
                int intPageCount = int.Parse(PageCount);
                if (intCurPage <= 0)
                {
                    return;
                }
                else
                {
                    intCurPage--;
                }
                ViewState["CurPage"] = intCurPage.ToString();
                SearchTest(intCurPage);
            }
            catch
            { }
        }
        protected void GotoNewPage_Click(object sender, EventArgs e)
        {
            string CurPage = ddlNewPage.SelectedValue;       //当前页码
            ViewState["CurPage"] = CurPage;
            try
            {
                int intCurPage = int.Parse(CurPage);
                SearchTest(intCurPage);
            }
            catch { }
        }
        protected void LinkBNextPage_Click(object sender, EventArgs e)
        {
            string CurPage = ViewState["CurPage"].ToString();       //当前页码
            string PageCount = ViewState["PageCount"].ToString();   //总页数

            try
            {
                int intCurPage = int.Parse(CurPage);
                int intPageCount = int.Parse(PageCount);
                if (intCurPage >= intPageCount)
                {
                    return;
                }
                else
                {
                    intCurPage = intCurPage + 1;
                }
                ViewState["CurPage"] = intCurPage.ToString();
                SearchTest(intCurPage);
            }
            catch
            { }
        }
        [AjaxPro.AjaxMethod]
        public bool DeletRequest(string barcode)
        {
            Model.BarCodeForm BarCodeForm = new Model.BarCodeForm();
            Model.NRequestItem NRequestItem = new Model.NRequestItem();
            Model.NRequestForm NRequestForm = new Model.NRequestForm();
            bool delCount = false;
            BarCodeForm.BarCode = barcode;
            DataSet ds = ibbcf.GetList(BarCodeForm);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string WebLisFlag = ds.Tables[0].Rows[0]["WebLisFlag"].ToString();
                if (WebLisFlag != "5")
                {
                    long BarCodeFormNo = Convert.ToInt64(ds.Tables[0].Rows[0]["BarCodeFormNo"].ToString());
                    NRequestItem.BarCodeFormNo = BarCodeFormNo;
                    DataSet dsNitem = rib.GetList(NRequestItem);
                    string NrequestItemNoList = "";
                    for (int i = 0; i < dsNitem.Tables[0].Rows.Count; i++)
                    {
                        NrequestItemNoList += "'" + dsNitem.Tables[0].Rows[i]["NRequestItemNo"].ToString().Trim() + "',";
                    }
                    if (NrequestItemNoList != "")
                    {
                        NrequestItemNoList = NrequestItemNoList.Remove(NrequestItemNoList.Length - 1);
                    }
                    long NRequestFormNo = long.Parse(dsNitem.Tables[0].Rows[0]["NRequestFormNo"].ToString());
                    NRequestForm.NRequestFormNo = NRequestFormNo;
                    int count = ibbcf.Delete(BarCodeFormNo);
                    count = rfb.Delete(NRequestFormNo);
                    bool result = rib.DeleteList(NrequestItemNoList);
                    if (count == 1 && result == true)
                    {
                        delCount = true;
                    }


                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('该申请单信息已被签收，不能删除！');</script>");
                }
            }
            return delCount;
        }
        [AjaxPro.AjaxMethod]
        public string GetBarCodeView(string barCode)
        {
            try
            {
                DataSet dsBarCode = ibbcf.GetBarCodeView(barCode);
                string str = "";
                if (dsBarCode != null && dsBarCode.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsBarCode.Tables[0].Columns.Count; i++)
                    {
                        str += dsBarCode.Tables[0].Columns[i] + ":" + dsBarCode.Tables[0].Rows[0][i].ToString() + "&";
                    }

                }
                str = str.Remove(str.LastIndexOf("&"));
                return str;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                return null;

            }
        }
        //protected void btnSearchSJDW_Click(object sender, EventArgs e)
        //{
        //    string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
        //    user = new ZhiFang.WebLis.Class.User(UserId);
        //    string flagstr=" 1=2 ";
        //    if (user.getDeptPosName() != null)
        //    {
        //        if (user.getDeptPosName()[0].Trim() != "山西高科")//（用户）属于中心
        //        {
        //            flagstr = " GroupName='" + user.getDeptPosName()[0].Trim() + "' ";
        //        }
        //        else
        //        {
        //            flagstr = " 1=1 ";
        //        }
        //    }

        //    string UserInput = hiddenUserInput.Value;
        //    string flag = "0";
        //    string sql = "select top 10 WebLisSourceOrgID,CNAME from CLIENTELE  where " + flagstr + " ";
        //    string sql1 = "select top 10 DoctorNo,CNAME from Doctor where 1=1";
        //    DataTable dt=new DataTable();

        //        if (this.hiddeneleid.Value == "inputName")
        //        {
        //            if (UserInput != "")
        //            {
        //            string where = "";
        //            where += " (WebLisSourceOrgID LIKE '%" + UserInput + "%') OR ";
        //            where += "(CNAME LIKE '%" + UserInput + "%') OR ";
        //            where += "(SHORTCODE LIKE '%" + UserInput + "%') OR ";
        //            where += "(ENAME LIKE '%" + UserInput + "%')";
        //            sql += " and ( " + where + ")";
        //            flag = "1";
        //            }
        //            dt = GetDataTable(sql);
        //            if (dt != null)
        //            {
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    string c = "#a3f1f5";
        //                    if (i == 0 && flag == "1")
        //                    {
        //                        c = "#999999";
        //                    }
        //                    System.Web.UI.HtmlControls.HtmlTableRow tr = new System.Web.UI.HtmlControls.HtmlTableRow();
        //                    System.Web.UI.HtmlControls.HtmlTableCell td = new System.Web.UI.HtmlControls.HtmlTableCell();
        //                    td.InnerHtml = dt.Rows[i]["CNAME"].ToString() + "&nbsp;";
        //                    td.Style.Add("padding", "2px");
        //                    td.Style.Add("font-size", "12px");
        //                    tr.Cells.Add(td);
        //                    tr.BgColor = c;
        //                    tr.Attributes.Add("onmouseover", "this.style.backgroundColor='#AAA';");
        //                    tr.Attributes.Add("onmousedown", "this.style.backgroundColor='" + c + "';GetTmpClient('" + dt.Rows[i]["WebLisSourceOrgID"].ToString() + "','" + dt.Rows[i]["CNAME"].ToString() + "')");
        //                    //
        //                    tr.Attributes.Add("onmouseout", "this.style.backgroundColor='#a3f1f5';");
        //                    tr.Attributes.Add("onclick", "GetTmpClient('" + dt.Rows[i]["WebLisSourceOrgID"].ToString() + "','" + dt.Rows[i]["CNAME"].ToString() + "')");
        //                    //tr.Attributes.Add("onclick", "alert('test');");
        //                    tableUserInputSJDWList.Rows.Add(tr);
        //                }
        //            }
        //        }
        //        if (this.hiddeneleid.Value == "doctor")
        //        {
        //            if (UserInput != "")
        //            {
        //                string where = "";
        //                where += "(CNAME LIKE '%" + UserInput + "%') OR ";
        //                where += "(SHORTCODE LIKE '%" + UserInput + "%')";
        //                sql1 += " and ( " + where + ")";
        //                flag = "1";
        //            }
        //            dt = GetDataTable(sql1);

        //            if (dt != null)
        //            {
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    string c = "#a3f1f5";
        //                    if (i == 0 && flag == "1")
        //                    {
        //                        c = "#999999";
        //                    }
        //                    System.Web.UI.HtmlControls.HtmlTableRow tr = new System.Web.UI.HtmlControls.HtmlTableRow();
        //                    System.Web.UI.HtmlControls.HtmlTableCell td = new System.Web.UI.HtmlControls.HtmlTableCell();
        //                    td.InnerHtml = dt.Rows[i]["CNAME"].ToString() + "&nbsp;";
        //                    td.Style.Add("padding", "2px");
        //                    td.Style.Add("font-size", "12px");
        //                    tr.Cells.Add(td);
        //                    tr.BgColor = c;
        //                    tr.Attributes.Add("onmouseover", "this.style.backgroundColor='#AAA';");
        //                    tr.Attributes.Add("onmousedown", "this.style.backgroundColor='" + c + "';GetTmpClient('" + dt.Rows[i]["DoctorNo"].ToString() + "','" + dt.Rows[i]["CNAME"].ToString() + "')");
        //                    //
        //                    tr.Attributes.Add("onmouseout", "this.style.backgroundColor='#a3f1f5';");
        //                    tr.Attributes.Add("onclick", "GetTmpClient('" + dt.Rows[i]["DoctorNo"].ToString() + "','" + dt.Rows[i]["CNAME"].ToString() + "')");
        //                    //tr.Attributes.Add("onclick", "alert('test');");
        //                    tableUserInputSJDWList.Rows.Add(tr);
        //                }
        //            }
        //        }


        //    Div_Input.Style["display"] = "";
        //    Div_Input.Style.Add("left", hiddenLeft.Value);
        //    Div_Input.Style.Add("top", hiddenTop.Value);
        //    SearchTest(0);
        //}
    }
}
