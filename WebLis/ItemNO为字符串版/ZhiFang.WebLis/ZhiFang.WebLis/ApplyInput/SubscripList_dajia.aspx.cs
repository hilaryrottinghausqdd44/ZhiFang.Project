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
using System.ComponentModel;

namespace ZhiFang.WebLis.ApplyInput
{
    public partial class SubscripList_dajia : ZhiFang.WebLis.Class.BasePage
    {
        public int PageSize = 25;  //单页显示最大申请单数：25
        protected string loginID = "";  //登录用户编号
        protected string loginName = "";  //登录用户姓名     
        private readonly Dictionary dic = BLLFactory<Dictionary>.GetBLL("Dictionary");
        private readonly IBBusinessLogicClientControl iblcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
        private readonly IBNRequestForm rfb = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
        private readonly IBBarCodeForm ibbcf = BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
        private readonly IBNRequestItem rib = BLLFactory<IBNRequestItem>.GetBLL("NRequestItem");
        ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User();
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ZhiFang.WebLis.Ashx.ApplyInput));
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ZhiFang.WebLis.ApplyInput.SubscripList_dajia));
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
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DefaultClientFlag") == "1")
                {
                    DataSet ds = user.GetClientListByPost("", -1);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            System.Web.UI.HtmlControls.HtmlInputText hittcn = (System.Web.UI.HtmlControls.HtmlInputText)Page.FindControl("inputName");
                            hittcn.Attributes.Remove("onfocus");
                            hittcn.Attributes.Remove("onkeydown");
                            hittcn.Attributes.Remove("onpropertychange");
                            hittcn.Attributes.Remove("onblur");
                            hittcn.Attributes.Add("readonly", "readonly");
                            hittcn.Attributes.Add("disabled", "disabled");
                            hittcn.Attributes.Add("value", ds.Tables[0].Rows[0]["CName"].ToString().Trim());
                            hiddenClient.Value = dt.Rows[0]["WebLisSourceOrgID"].ToString().Trim();
                        }
                    }
                }
                ViewState["CurPage"] = "0";     //当前页码
                ViewState["PageCount"] = "0";   //总页数
                string FD = DateTime.Now.ToString("yyyy-MM-dd");
                //txtCollectStartDate.Value = FD;
                //txtCollectEndDate.Value = FD;
                txtStartDate.Value = FD;
                txtEndDate.Value = FD;
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
            if (hiddenInputClientNo.Value.Trim() != "")
            {
                nrf_m.ClientNo = hiddenInputClientNo.Value;
            }
            if (hiddenClient.Value.Trim() != "")
            {
                nrf_m.WebLisSourceOrgID = hiddenClient.Value;
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
            if (txtCollectStartDate.Value.Trim() != "")
            {
                nrf_m.CollectDateStart = txtCollectStartDate.Value;
            }
            if (txtCollectEndDate.Value.Trim() != "")
            {
                nrf_m.CollectDateEnd = txtCollectEndDate.Value + " 23:59:59";
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
                    DataSet ds = user.GetClientListByPost("", -1); ;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        nrf_m.ClientList += " '" + ds.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
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
            dt = rfb.GetNRequstFormList(nrf_m, StartPage, PageSize, out intPageCount, out iCount);

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
            if (ViewState["CurPage"].ToString() == "0")
            {
                LinkBPrePage.Enabled = false;
            }
            if ((int.Parse(ViewState["CurPage"].ToString()) + 1).ToString() == ViewState["PageCount"].ToString())
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
            foreach (DataRow dr in dt.Rows)
            {
                i++;
                string b = rfb.GetBarCodeByNRequestFormNo(dr["NRequestFormNo"].ToString().Trim());
                var barcode = "";
                foreach(var a in b.Split(','))
                {
                    barcode += a+"</br>";
                }
                //barcode = barcode.Substring(0, barcode.Length - barcode.LastIndexOf("</br>"));
                System.Web.UI.HtmlControls.HtmlTableRow tr = new System.Web.UI.HtmlControls.HtmlTableRow();
                System.Web.UI.HtmlControls.HtmlTableCell tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.InnerHtml = "<input name='checkbox' type='checkbox' id='chk_" + i + "' value='" + b + "' checked='checked' />";

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
                        for (int j = 0; j < nds.Tables[0].Rows.Count; j++)
                        {


                            htmltmp += nds.Tables[0].Rows[j]["CName"].ToString().Trim() + ",";
                        }
                        if (htmltmp.Length > 0)
                        {
                            htmltmp = htmltmp.Substring(0, htmltmp.Length - 1);
                        }
                    }
                }
                catch (Exception e)
                {
                    ZhiFang.Common.Log.Log.Error(e.StackTrace + e.ToString());
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
                tc.InnerHtml = dr["WebLisSourceOrgName"].ToString().Trim();
                tr.Cells.Add(tc);
                //tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                //tc.Align = "center";
                //tc.InnerHtml = dr["ClientName"].ToString().Trim();
                //tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                string Diag = dr["Diag"].ToString().Trim();

                Diag = ZhiFang.Tools.Tools.StringLength(Diag, 8);
                tc.InnerHtml = Diag;
                tr.Cells.Add(tc);



                //打印按钮

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.InnerHtml = "<a href='#' onclick='ModifyTest(\"" + dr["NRequestFormNo"].ToString().Trim() + "\")'>修改</a>";
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.InnerHtml = "<a href='#' onclick='DeleteNRequestForm(\"" + dr["NRequestFormNo"].ToString().Trim() + "\")'>删除</a>";
                tr.Cells.Add(tc);
                if (base.ReadQueryString("BarCodeInputFlag") != "1")
                {
                    tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                    tc.InnerHtml = "<a href='#' id='p" + i.ToString() + "' onclick=\"PrintCurBarCode('" + b + "','" + dr["CName"].ToString().Trim() + "','" + dr["Sex"].ToString().Trim() + "','" + dr["Age"].ToString().Trim() + "','" + dr["AgeUnitName"].ToString().Trim() + "','','" + dr["CollectTime"].ToString().Trim().Substring(0, dr["CollectTime"].ToString().Trim().Length - 3) + "','" + htmltmp + "','" + dr["WebLisSourceOrgName"].ToString().Trim() + "','" + dr["SampleName"].ToString().Trim() + "',1,'')\">打印</a>";
                    tr.Cells.Add(tc);
                }
                string BgColor = "#a3f1f5";

                tr.BgColor = BgColor;
                tr.Attributes.Add("onmouseover", "SetRowFocus(this,'#34a6a3')");
                tr.Attributes.Add("onmouseout", "SetRowFocus(this,'" + BgColor + "')");

                List_Table.Rows.Add(tr);
            }
        }
        #endregion


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

                //foreach (DataRow row in dtCardNo.Rows)
                //{
                //    Pointdt.Rows.Add(row.ItemArray);
                //}
                var query = from t in dt.AsEnumerable()
                            group t by new { t1 = t.Field<string>("CName") } into m
                            select new
                            {
                                CName = m.Key.t1
                            };
                Pointdt = aa.CopyToDataTablea(query);
                 
              
                
                //for (int i = 0; i < dtCardNo.Rows.Count; i++)
                //{
                //    //string dtcol = "";
                //    //try
                //    //{
                //    //    dtcol = dtCardNo.Rows[i][0].ToString();
                //    //}
                //    //catch (Exception)
                //    //{

                //    //}
                //    DataRow dr = dt.Select(colName + "='" + dtCardNo.Rows[i][0].ToString() + "'")[0];
                //    if (i + 1 == dtCardNo.Rows.Count)
                //    {
                //        Pointdt.Rows.Add(dr.ItemArray);
                //        break;
                //    }
                //    else
                //        continue;
                //}

               return Pointdt;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.StackTrace + e.ToString());
                return null;
            }
        }
       

        #region 保存打印成功标记
        protected void saveprintcount_onclick(object sender, EventArgs e)
        {
            IBBarCodeForm ibbf = BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
            DataSet ds = ibbf.GetList(new Model.BarCodeForm() { BarCode = this.hiddentmpBarCode.Value.ToString().Trim() });
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns.Contains("PrintCount") && ds.Tables[0].Rows[0]["PrintCount"].ToString().Trim() != "")
            {
                ibbf.UpdateByBarCode(new Model.BarCodeForm() { PrintCount = Convert.ToInt32(ds.Tables[0].Rows[0]["PrintCount"]) + 1, BarCode = this.hiddentmpBarCode.Value.ToString().Trim() }).ToString();
            }
            else
            {
                ibbf.UpdateByBarCode(new Model.BarCodeForm() { PrintCount = 1, BarCode = this.hiddentmpBarCode.Value.ToString().Trim() }).ToString();
            }
            if (Tools.Validate.IsNum(this.hiddentmpBarCode.Value.ToString().Trim()))
            {
                ds = ibbf.GetList(new Model.BarCodeForm() { BarCodeFormNo = int.Parse(this.hiddentmpBarCode.Value.ToString().Trim()) });
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns.Contains("PrintCount") && ds.Tables[0].Rows[0]["PrintCount"].ToString().Trim() != "")
                {
                    ibbf.UpdateByBarCode(new Model.BarCodeForm() { PrintCount = Convert.ToInt32(ds.Tables[0].Rows[0]["PrintCount"]) + 1, BarCodeFormNo = int.Parse(this.hiddentmpBarCode.Value.ToString().Trim()) }).ToString();
                }
                else
                {
                    ibbf.UpdateByBarCode(new Model.BarCodeForm() { PrintCount = 1, BarCodeFormNo = int.Parse(this.hiddentmpBarCode.Value.ToString().Trim()) }).ToString();
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
            string CurPage = (Int32.Parse(ddlNewPage.SelectedValue)-1).ToString();       //当前页码
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
        public bool DeletRequest(string NRequestFormNo)
        {
            try
            {
                ZhiFang.Common.Log.Log.Info("删除申请单NRequestFormNo：" + NRequestFormNo);
                Model.BarCodeForm BarCodeForm = new Model.BarCodeForm();
                Model.NRequestItem NRequestItem = new Model.NRequestItem();
                Model.NRequestForm NRequestForm = new Model.NRequestForm();
                string[] barcodelist = rfb.GetBarCodeByNRequestFormNo(NRequestFormNo).Split(',');
                bool barcodeflag = true;
                foreach (var barcode in barcodelist)
                {
                    BarCodeForm.BarCode = barcode;
                    DataSet ds = ibbcf.GetList(BarCodeForm);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        ZhiFang.Common.Log.Log.Info("条码条数：" + ds.Tables[0].Rows.Count);
                        string WebLisFlag = ds.Tables[0].Rows[0]["WebLisFlag"].ToString();
                        ZhiFang.Common.Log.Log.Info("申请单状态：" + WebLisFlag);
                        if (WebLisFlag == "5")
                        {
                            barcodeflag = false;
                            break;
                        }
                    }
                }
                if (barcodeflag)
                {
                    rfb.Delete(long.Parse(NRequestFormNo));
                    rib.DeleteList_ByNRequestFormNo(long.Parse(NRequestFormNo));
                    foreach (var barcode in barcodelist)
                    {
                        int count = ibbcf.Delete(long.Parse(barcode));
                    }
                    return true;
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("该申请单信息已被签收，不能删除！：" + NRequestFormNo);
                    return false;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Info("删除申请单异常！异常信息：" + e.ToString());
                return false;
            }
        }
    }
}

public static class aa
{
    public static DataTable CopyToDataTablea<T>(this IEnumerable<T> array)
    {
        var ret = new DataTable();
        foreach (PropertyDescriptor dp in TypeDescriptor.GetProperties(typeof(T)))
            ret.Columns.Add(dp.Name, dp.PropertyType);
        foreach (T item in array)
        {
            var Row = ret.NewRow();
            foreach (PropertyDescriptor dp in TypeDescriptor.GetProperties(typeof(T)))
                Row[dp.Name] = dp.GetValue(item);
            ret.Rows.Add(Row);
        }
        return ret;
    }
}