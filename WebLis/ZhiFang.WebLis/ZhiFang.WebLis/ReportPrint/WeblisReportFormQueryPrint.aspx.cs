using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ZhiFang.IBLL.Report;
using ZhiFang.BLLFactory;
//using AjaxPro;
using ZhiFang.Common;
using System.Text;
using ZhiFang.WebLis.Class;
using System.Xml;
using ZhiFang.IBLL.Common.BaseDictionary;
using Common;

namespace ZhiFang.WebLis.ReportPrint
{
    public partial class WeblisReportFormQueryPrint : BasePage
    {
        private readonly IBReportFormFull rffb = BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
        private readonly Dictionary dic = BLLFactory<Dictionary>.GetBLL("Dictionary");
        private readonly IBShowFrom showform = BLLFactory<IBShowFrom>.GetBLL("ShowFrom");
        private readonly ZhiFang.IBLL.Report.IBUserReportFormDataListShowConfig iburfdlsc = BLLFactory<IBUserReportFormDataListShowConfig>.GetBLL("UserReportFormDataListShowConfig");
        private ZhiFang.Model.ReportFormFull rffm = new ZhiFang.Model.ReportFormFull();
        private SearchConditions sc = new SearchConditions();
        private readonly IBBusinessLogicClientControl iblcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
           
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ZhiFang.WebLis.Ashx.ReportPrint));
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ZhiFang.WebLis.ReportPrint.WeblisReportFormQueryPrint));
            if (!IsPostBack)
            {
                //if (Request.Cookies["ZhiFangUser"] != null)
                //{
                    #region 科　　室
                    if (base.CheckPageControlNull("Dept"))
                    {
                        System.Web.UI.HtmlControls.HtmlSelect dept = (System.Web.UI.HtmlControls.HtmlSelect)this.Page.FindControl("Dept");
                        dept.DataSource = dic.Department().Tables[0];
                        dept.DataTextField = "CName";
                        dept.DataValueField = "DeptNo";
                        dept.DataBind();
                        dept.Items.Insert(0, new ListItem("", ""));
                        SelectSetValueByQuery("Dept", dept);
                    }
                    #endregion
                    #region 开单医生
                    if (base.CheckPageControlNull("Doctor"))
                    {
                        try
                        {
                            System.Web.UI.HtmlControls.HtmlSelect Doctor = (System.Web.UI.HtmlControls.HtmlSelect)this.Page.FindControl("Doctor");
                            Doctor.DataSource = dic.Doctor().Tables[0];
                            Doctor.DataTextField = "CName";
                            Doctor.DataValueField = "DoctorNo";
                            Doctor.DataBind();
                            Doctor.Items.Insert(0, new ListItem("", ""));
                            SelectSetValueByQuery("Doctor", Doctor);
                        }
                        catch
                        {
                            System.Web.UI.HtmlControls.HtmlInputText Doctor = (System.Web.UI.HtmlControls.HtmlInputText)this.Page.FindControl("Doctor");
                            TextBoxSetValueByQuery("Doctor", Doctor);
                        }

                    }
                    #endregion
                    #region 病　　区
                    if (base.CheckPageControlNull("District"))
                    {
                        System.Web.UI.HtmlControls.HtmlSelect District = (System.Web.UI.HtmlControls.HtmlSelect)this.Page.FindControl("District");
                        District.DataSource = dic.District().Tables[0];
                        District.DataTextField = "CName";
                        District.DataValueField = "DistrictNo";
                        District.DataBind();
                        District.Items.Insert(0, new ListItem("", ""));
                        SelectSetValueByQuery("District", District);
                    }
                    #endregion
                    #region 就诊类型
                    if (base.CheckPageControlNull("SickType"))
                    {
                        System.Web.UI.HtmlControls.HtmlSelect SickType = (System.Web.UI.HtmlControls.HtmlSelect)this.Page.FindControl("SickType");
                        SickType.DataSource = dic.SickType().Tables[0];
                        SickType.DataTextField = "CName";
                        SickType.DataValueField = "SickTypeNo";
                        SickType.DataBind();
                        SickType.Items.Insert(0, new ListItem("", ""));
                        SelectSetValueByQuery("SickType", SickType);
                    }
                    #endregion
                    #region 送检开始时间
                    if (base.CheckPageControlNull("StartDate"))
                    {
                        System.Web.UI.HtmlControls.HtmlInputText StartDate = (System.Web.UI.HtmlControls.HtmlInputText)this.Page.FindControl("StartDate");
                        if (ConfigHelper.GetConfigInt("SearchBeforeDayNum") == null)
                        {
                            StartDate.Value = DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            StartDate.Value = DateTime.Now.AddDays(ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value).ToString("yyyy-MM-dd");
                        }
                        TextBoxSetValueByQuery("StartDate", StartDate);
                    }
                    #endregion
                    #region 送检结束时间
                    if (base.CheckPageControlNull("EndDate"))
                    {
                        System.Web.UI.HtmlControls.HtmlInputText EndDate = (System.Web.UI.HtmlControls.HtmlInputText)this.Page.FindControl("EndDate");
                        EndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                        TextBoxSetValueByQuery("EndDate", EndDate);
                    }
                    #endregion
                    #region 报告开始时间
                    if (base.CheckPageControlNull("CheckStartDate"))
                    {
                        System.Web.UI.HtmlControls.HtmlInputText CheckStartDate = (System.Web.UI.HtmlControls.HtmlInputText)this.Page.FindControl("CheckStartDate");
                        if (ConfigHelper.GetConfigInt("SearchBeforeDayNum") == null)
                        {
                            CheckStartDate.Value = DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            CheckStartDate.Value = DateTime.Now.AddDays(ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value).ToString("yyyy-MM-dd");
                        }
                        TextBoxSetValueByQuery("CheckStartDate", CheckStartDate);
                    }
                    #endregion
                    #region 报告结束时间
                    if (base.CheckPageControlNull("CheckEndDate"))
                    {
                        System.Web.UI.HtmlControls.HtmlInputText CheckEndDate = (System.Web.UI.HtmlControls.HtmlInputText)this.Page.FindControl("CheckEndDate");
                        CheckEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                        TextBoxSetValueByQuery("CheckEndDate", CheckEndDate);
                    }
                    #endregion
                    #region 病历号
                    if (base.CheckPageControlNull("PatNo"))
                    {
                        System.Web.UI.HtmlControls.HtmlInputText PatNo = (System.Web.UI.HtmlControls.HtmlInputText)this.Page.FindControl("PatNo");
                        TextBoxSetValueByQuery("PatNo", PatNo);
                    }
                    #endregion
                    #region 条码号
                    if (base.CheckPageControlNull("barcode"))
                    {
                        System.Web.UI.HtmlControls.HtmlInputText barcode = (System.Web.UI.HtmlControls.HtmlInputText)this.Page.FindControl("barcode");
                        TextBoxSetValueByQuery("barcode", barcode);
                    }
                    #endregion
                    #region 病人姓名
                    if (base.CheckPageControlNull("Name"))
                    {
                        System.Web.UI.HtmlControls.HtmlInputText Name = (System.Web.UI.HtmlControls.HtmlInputText)this.Page.FindControl("Name");
                        TextBoxSetValueByQuery("Name", Name);
                    }
                    #endregion
                    #region 申请单号
                    if (base.CheckPageControlNull("SerialNo"))
                    {
                        System.Web.UI.HtmlControls.HtmlInputText SerialNo = (System.Web.UI.HtmlControls.HtmlInputText)this.Page.FindControl("SerialNo");
                        TextBoxSetValueByQuery("SerialNo", SerialNo);
                    }
                    #endregion
                    #region 小    组
                    if (base.CheckPageControlNull("Section"))
                    {
                        IBPGroup pg = ZhiFang.BLLFactory.BLLFactory<IBPGroup>.GetBLL("PGroup");
                        System.Web.UI.HtmlControls.HtmlSelect Section = (System.Web.UI.HtmlControls.HtmlSelect)this.Page.FindControl("Section");
                        Section.DataSource = pg.GetAllList().Tables[0];
                        Section.DataTextField = "CName";
                        Section.DataValueField = "SectionNo";
                        Section.DataBind();
                        Section.Items.Insert(0, new ListItem("", ""));
                        SelectSetValueByQuery("Section", Section);
                    }
                    #endregion
                    #region 打印标记
                    if (base.CheckPageControlNull("PrintFlag"))
                    {
                        System.Web.UI.HtmlControls.HtmlInputCheckBox PrintFlag = (System.Web.UI.HtmlControls.HtmlInputCheckBox)this.Page.FindControl("PrintFlag");                       
                    }
                    #endregion
                    if (base.CheckPageControlNull("Client"))
                    {
                        if (base.CheckCookies("ZhiFangUser"))
                        {
                            User u = new User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
                            // User u = new User(base.ReadCookies("ZhiFangUser"));
                            ZhiFang.Common.Log.Log.Info("公司名称：" + u.CompanyName + "用户账号：" + u.Account);
                            System.Web.UI.HtmlControls.HtmlSelect Client = (System.Web.UI.HtmlControls.HtmlSelect)this.Page.FindControl("Client");
                            DataSet ds = iblcc.GetClientList_DataSet(new ZhiFang.Model.BusinessLogicClientControl() { Account = u.Account.Trim(), SelectedFlag = true });
                            //DataSet ds = u.GetClientListByPost("",-1);
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                Client.DataSource = ds;
                                Client.DataTextField = "CName";
                                Client.DataValueField = "ClientNo";
                                Client.DataBind();
                                Client.Items.Insert(0, new ListItem("", ""));
                                SelectSetValueByQuery("ClientNo", Client);
                            }
                        }
                    }
                //}
                //else
                //{
                //    Response.End();
                //}
            }
            //this.showclasslist.InnerHtml = Report.Class.ShowTools.ShowClassList(this.PageName.Value.Trim());

        }

        private void SelectSetValueByQuery(string a, System.Web.UI.HtmlControls.HtmlSelect h)
        {
            if (base.CheckQueryString(a))
            {
                try
                {
                    foreach (ListItem l in h.Items)
                    {
                        l.Selected = false;
                        if (l.Value == base.ReadQueryString(a).Trim())
                        {
                            l.Selected = true;
                            break;
                        }
                    }
                    h.Disabled = true;
                }
                catch
                {

                }
            }
        }

        private void TextBoxSetValueByQuery(string a, System.Web.UI.HtmlControls.HtmlInputText h)
        {
            if (base.CheckQueryString(a) && base.ReadQueryString(a).Split(',').Length == 2)
            {
                try
                {
                    if (base.ReadQueryString(a).Split(',')[1] == "0")
                    {
                        h.Value = base.ReadQueryString(a).Split(',')[0].Trim();
                        h.Disabled = true;
                    }
                    else
                    {
                        if (base.ReadQueryString(a).Split(',')[1] == "1")
                        {
                            h.Value = base.ReadQueryString(a).Split(',')[0].Trim();
                            //h.Disabled = false;
                        }
                    }
                }
                catch
                {

                }
            }
        }

        protected void linkGetAllItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (base.CheckFormNullAndValue("SelectDateClass"))
                {
                    if (base.ReadForm("SelectDateClass") == "Receive")
                    {
                        if (base.CheckPageControlNull("StartDate"))
                        {
                            rffm.Startdate = Convert.ToDateTime(base.ReadForm("StartDate"));
                        }
                        else
                        {
                            rffm.Startdate = null;
                        }

                        if (base.CheckPageControlNull("EndDate"))
                        {
                            rffm.Enddate = Convert.ToDateTime(base.ReadForm("EndDate") + " 23:59:59");
                        }
                        else
                        {
                            rffm.Enddate = null;
                        }
                    }
                    if (base.ReadForm("SelectDateClass") == "Check")
                    {
                        if (base.CheckPageControlNull("CheckStartDate"))
                        {
                            rffm.CheckStartDate = Convert.ToDateTime(base.ReadForm("CheckStartDate"));
                        }
                        else
                        {
                            rffm.CheckStartDate = null;
                        }
                        if (base.CheckPageControlNull("CheckEndDate"))
                        {
                            rffm.CheckEndDate = Convert.ToDateTime(base.ReadForm("CheckEndDate") + " 23:59:59");
                        }
                        else
                        {
                            rffm.CheckEndDate = null;
                        }
                    }
                }
                else
                {
                    if (base.CheckPageControlNull("StartDate"))
                    {
                        rffm.Startdate = Convert.ToDateTime(base.ReadForm("StartDate"));
                    }
                    else
                    {
                        rffm.Startdate = null;
                    }
                    if (base.CheckPageControlNull("EndDate"))
                    {
                        rffm.Enddate = Convert.ToDateTime(base.ReadForm("EndDate"));
                    }
                    else
                    {
                        rffm.Enddate = null;
                    }
                    if (base.CheckPageControlNull("CheckStartDate"))
                    {
                        rffm.CheckStartDate = Convert.ToDateTime(base.ReadForm("CheckStartDate"));
                    }
                    else
                    {
                        rffm.CheckStartDate = null;
                    }
                    if (base.CheckPageControlNull("CheckEndDate"))
                    {
                        rffm.CheckEndDate = Convert.ToDateTime(base.ReadForm("CheckEndDate"));
                    }
                    else
                    {
                        rffm.CheckEndDate = null;
                    }
                }
                if (base.CheckPageControlNull("Dept") && base.CheckFormNullAndValue("Dept") && ZhiFang.Common.Public.PageValidate.IsNumber(base.ReadForm("Dept").Trim()))
                {
                    rffm.DEPTNO = Convert.ToInt32(base.ReadForm("Dept"));
                }
                if (base.CheckPageControlNull("barcode") && base.CheckFormNullAndValue("barcode"))
                {
                    rffm.BarCode = base.ReadForm("barcode");
                }
                if (base.CheckPageControlNull("Doctor") && base.CheckFormNullAndValue("Doctor"))
                {
                    rffm.DOCTOR = base.ReadForm("Doctor");
                }
                if (base.CheckPageControlNull("District") && base.CheckFormNullAndValue("District") && ZhiFang.Common.Public.PageValidate.IsNumber(base.ReadForm("District").Trim()))
                {
                    rffm.DISTRICTNO = base.ReadForm("District");
                }
                //if (base.CheckPageControlNull("SickType") && base.CheckFormNullAndValue("SickType") && ZhiFang.Common.Public.PageValidate.IsNumber(base.ReadForm("SickType").Trim()))
                //{
                //    rffm.SICKTYPENO = base.ReadForm("SickType");
                //}
                if (base.CheckQueryString("SickType") && Tools.Validate.IsInt(base.ReadQueryString("SickType")))
                {
                    rffm.SICKTYPENO = base.ReadQueryString("SickType");
                }
                else
                {
                    if (base.CheckFormNullAndValue("SickType") && Tools.Validate.IsInt(base.ReadForm("SickType")))
                    {
                        rffm.SICKTYPENO = base.ReadForm("SickType");
                    }
                }
                if (base.CheckPageControlNull("PatNo") && base.CheckFormNullAndValue("PatNo"))
                {
                    rffm.PATNO = base.ReadForm("PatNo");
                }
                if (base.CheckQueryString("CName"))
                {
                    rffm.CNAME = base.ReadQueryString("CName");
                }
                else
                {
                    if (base.CheckPageControlNull("Name") && base.CheckFormNullAndValue("Name"))
                    {
                        rffm.CNAME = base.ReadForm("Name");
                    }
                }

                if (base.CheckQueryString("Bed"))
                {
                    rffm.BED = base.ReadQueryString("Bed");
                }
                else
                {
                    if (base.CheckPageControlNull("Bed") && base.CheckFormNullAndValue("Bed"))
                    {
                        rffm.BED = base.ReadForm("Bed");
                    }
                }

                if (base.CheckQueryString("SerialNo"))
                {
                    rffm.SERIALNO = base.ReadQueryString("SerialNo");
                }
                else
                {
                    if (base.CheckPageControlNull("SerialNo") && base.CheckFormNullAndValue("SerialNo"))
                    {
                        rffm.SERIALNO = base.ReadForm("SerialNo");
                    }
                }

                if (base.CheckQueryString("SectionNo"))
                {
                    try
                    {
                        rffm.SECTIONNO = base.ReadQueryString("SectionNo");
                    }
                    catch
                    {

                    }
                }
                else
                {
                    if (base.CheckPageControlNull("Section") && base.CheckFormNullAndValue("Section"))
                    {
                        rffm.SECTIONNO = base.ReadForm("Section");
                    }
                }

                if (base.CheckQueryString("SampleNo"))
                {
                    try
                    {
                        rffm.SAMPLENO = base.ReadQueryString("SampleNo");
                    }
                    catch
                    {

                    }
                }
                else
                {
                    if (base.CheckPageControlNull("SampleNo") && base.CheckFormNullAndValue("SampleNo"))
                    {
                        rffm.SAMPLENO = base.ReadForm("SampleNo");
                    }
                }

                if (base.CheckQueryString("PrintFlag") && ZhiFang.Tools.Validate.IsInt(base.ReadQueryString("PrintFlag")))
                {
                    rffm.PRINTTEXEC = base.ReadQueryString("PrintFlag");
                }
                else
                {
                    //if (base.CheckPageControlNull("PrintFlagTrue"))
                    //{
                    if (base.CheckFormNullAndValue("PrintFlag") && ZhiFang.Tools.Validate.IsInt(base.ReadForm("PrintFlag")))
                    {
                        rffm.PRINTTEXEC = base.ReadForm("PrintFlag");
                    }
                    // }
                }
                if (base.CheckQueryString("LikeSearchFlag") && Tools.Validate.IsInt(base.ReadQueryString("LikeSearchFlag")))
                {
                    rffm.LIKESEARCH = base.ReadQueryString("LikeSearchFlag");
                }
                else
                {
                    if (base.CheckFormNullAndValue("LikeSearchFlag") && Tools.Validate.IsInt(base.ReadForm("LikeSearchFlag")))
                    {
                        rffm.LIKESEARCH = base.ReadForm("LikeSearchFlag");
                    }
                }
                if (base.CheckPageControlNull("Client"))
                {
                    if (!base.CheckFormNullAndValue("Client"))
                    {
                        User u = new User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
                        u.GetPostList();
                        DataSet dsc = new DataSet();
                        if (u.PostList.Exists(l => { if (l.ToUpper() == "LOGISTICSOFFICER")return true; else return false; }))
                        {
                            dsc = iblcc.GetClientList_DataSet(new ZhiFang.Model.BusinessLogicClientControl() { Account = u.Account.Trim(), SelectedFlag = true });
                            for (int i = 0; i < dsc.Tables[0].Rows.Count; i++)
                            {
                                rffm.ClientList += " '" + dsc.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
                            }
                            rffm.ClientList = rffm.ClientList.Remove(rffm.ClientList.Length - 1);
                        }
                    }
                    else
                    {
                        rffm.CLIENTNO = base.ReadForm("Client");
                    }
                }
                if (base.CheckPageControlNull("WeblisSourceOrgId"))
                {
                    if (!base.CheckFormNullAndValue("WeblisSourceOrgId"))
                    {
                        User u = new User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
                        u.GetPostList();
                        DataSet dsc = new DataSet();
                        if (u.PostList.Exists(l => { if (l.ToUpper() == "LOGISTICSOFFICER")return true; else return false; }))
                        {
                            dsc = iblcc.GetClientList_DataSet(new ZhiFang.Model.BusinessLogicClientControl() { Account = u.Account.Trim(), SelectedFlag = true });
                            for (int i = 0; i < dsc.Tables[0].Rows.Count; i++)
                            {
                                rffm.WeblisSourceOrgList += " '" + dsc.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
                            }
                            rffm.WeblisSourceOrgList = rffm.WeblisSourceOrgList.Remove(rffm.WeblisSourceOrgList.Length - 1);
                        }
                    }
                    else
                    {
                        rffm.WeblisSourceOrgId = base.ReadForm("WeblisSourceOrgId");
                    }
                }
                if (base.CheckQueryString("ZDY1"))
                {
                    rffm.ZDY1 = base.ReadQueryString("ZDY1");
                }
                else
                {
                    if (base.CheckFormNullAndValue("ZDY1"))
                    {
                        rffm.ZDY1 = base.ReadForm("ZDY1");
                    }
                }
                if (base.CheckQueryString("ZDY2"))
                {
                    rffm.ZDY2 = base.ReadQueryString("ZDY2");
                }
                else
                {
                    if (base.CheckFormNullAndValue("ZDY2"))
                    {
                        rffm.ZDY2 = base.ReadForm("ZDY2");
                    }
                }
                if (base.CheckQueryString("ZDY3"))
                {
                    rffm.ZDY3 = base.ReadQueryString("ZDY3");
                }
                else
                {
                    if (base.CheckFormNullAndValue("ZDY3"))
                    {
                        rffm.ZDY3 = base.ReadForm("ZDY3");
                    }
                }
                if (base.CheckQueryString("ZDY4"))
                {
                    rffm.ZDY4 = base.ReadQueryString("ZDY4");
                }
                else
                {
                    if (base.CheckFormNullAndValue("ZDY4"))
                    {
                        rffm.ZDY4 = base.ReadForm("ZDY4");
                    }
                }
                if (base.CheckQueryString("ZDY5"))
                {
                    rffm.ZDY5 = base.ReadQueryString("ZDY5");
                }
                else
                {
                    if (base.CheckFormNullAndValue("ZDY5"))
                    {
                        rffm.ZDY5 = base.ReadForm("ZDY5");
                    }
                }
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigInt("WeblisDBSQLFlag") == 1 && System.Configuration.ConfigurationManager.AppSettings["WeblisModelName"] != null && System.Configuration.ConfigurationManager.AppSettings["WeblisModelName"].ToString().Trim() != "")
                {
                    //rffm.RBACSQL = getRBACSql();
                    ZhiFang.Common.Log.Log.Debug(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "RBACSQL" + rffm.RBACSQL);
                    ZhiFang.Common.Log.Log.Debug(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "WeblisModelName" + System.Configuration.ConfigurationManager.AppSettings["WeblisModelName"].ToString());
                }
                //rffm.FormStateNo = 3;
                string t = iburfdlsc.ShowReportFormListOrderColumn(this.PageName.Value.Trim(), Convert.ToInt32(this.tmpclassid.Value.Trim()));
                this.OrderColumn.Value = t;
                DataSet ds = rffb.GetList(rffm); 
                DataTable dt = ds.Tables[0];
                this.tmpcondition.Value = sc.searchconditionsstr(rffm);
                this.tablehead.InnerHtml = ZhiFang.WebLis.Class.ShowTools.GridViewHeadShow(this.PageName.Value.Trim(), Convert.ToInt32(this.tmpclassid.Value.Trim()));
                showlistandfrom(dt, 0);
            }
            catch (Exception eee)
            {
                ZhiFang.Common.Log.Log.Debug(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + eee.ToString());
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string t = iburfdlsc.ShowReportFormListOrderColumn(this.PageName.Value.Trim(), Convert.ToInt32(this.tmpclassid.Value.Trim()));
            this.OrderColumn.Value = t;
            this.OrderColumnFlag.Value = "asc";
            if (Session["tmpdata"] == null)
            {

            }
            if (Session["tmpdata"] != null)
            {
                DataTable dt = (DataTable)Session["tmpdata"];
                this.tablehead.InnerHtml = ZhiFang.WebLis.Class.ShowTools.GridViewHeadShow(this.PageName.Value.Trim(), Convert.ToInt32(this.tmpclassid.Value.Trim()));
                showlistandfrom(dt, 0);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (Session["tmpdata"] == null)
            {

            }
            if (Session["tmpdata"] != null)
            {
                DataTable dt = (DataTable)Session["tmpdata"];
                //this.tablehead.InnerHtml = Report.Class.ShowTools.GridViewHeadShow(this.PageName.Value.Trim(), Convert.ToInt32(this.tmpclassid.Value.Trim()), this.CheckBoxFlag.Value);
                showlistandfrom(dt, Convert.ToInt32(this.PIndex.Value.Trim()));
            }
        }

        protected void showlistandfrom(DataTable dt, int pageindex)
        {
            if (dt.Rows.Count > 0)
            {
                Session["tmpdata"] = dt;
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
                string[] tmpstra = ZhiFang.WebLis.Class.ShowTools.GridViewShow_Weblis(this.PageName.Value.Trim(), Convert.ToInt32(this.tmpclassid.Value.Trim()), dv, pageindex);
                this.tablelist.InnerHtml = tmpstra[1];
                //this.ShowFormTypeList.InnerHtml = ShowTools.ShowFormTypeList(tmpstra[0].Split(',')[0], Convert.ToInt32(tmpstra[0].Split(',')[1]), this.PageName.Value.Trim());
                string aaa = showform.ShowReportForm_Weblis(tmpstra[0].Split(',')[0], Convert.ToInt32(tmpstra[0].Split(',')[1]), this.PageName.Value.Trim(), 0, Convert.ToInt32(tmpstra[0].Split(',')[2]));
                try
                {
                    aaa = aaa.Substring(aaa.IndexOf("<html"), aaa.Length - aaa.IndexOf("<html"));
                }
                catch
                {

                }
                aaa.Replace("\r\n ", " ");
                this.ShowFormHtml.InnerHtml = aaa;
                FirstFormNo.Value = tmpstra[0].Split(',')[0].ToString().Trim() + ";" + tmpstra[0].Split(',')[1].ToString().Trim();
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), System.Guid.NewGuid().ToString(), "try{document.getElementById('" + tmpstra[0].Split(',')[0].ToString().Trim() + "').className='leftlist1focus';}catch(e){ alert('" + tmpstra[0].Split(',')[0].ToString().Trim() + "'); }", true);
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), System.Guid.NewGuid().ToString(), "try{document.getElementById('PIndex').value='" + pageindex + "';}catch(e){ alert('3'); }", true);
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), System.Guid.NewGuid().ToString(), "try{document.getElementById('sT1').style.display='block';}catch(e){ alert('1'); }", true);
                //ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), System.Guid.NewGuid().ToString(), "alert('"+Session.Timeout.ToString()+"')", true);
                //ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), System.Guid.NewGuid().ToString(), "alert('" + ds.Tables[0].Rows[0]["FormNo"].ToString().Trim() + ", " + ds.Tables[0].Rows[0]["SectionNo"].ToString().Trim() + "');", true);
            }
            else
            {
                this.tablelist.InnerHtml = "";
                ZhiFang.Common.Log.Log.Debug(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "dt为空或无记录！");
            }
        }
               
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (Session["tmpdata"] == null)
            {

            }
            if (Session["tmpdata"] != null)
            {
                DataTable dt = (DataTable)Session["tmpdata"];
                //this.tablehead.InnerHtml = Report.Class.ShowTools.GridViewHeadShow(this.PageName.Value.Trim(), Convert.ToInt32(this.tmpclassid.Value.Trim()), this.CheckBoxFlag.Value);
                showlistandfrom(dt, 0);
            }
        }
        [AjaxPro.AjaxMethod]
        public string aaa()
        {
            return "aaa";
        }
    }
}

