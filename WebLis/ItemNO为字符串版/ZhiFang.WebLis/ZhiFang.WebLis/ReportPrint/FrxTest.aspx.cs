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
using AjaxPro;
using ZhiFang.Common;
using System.Text;
using ZhiFang.WebLis.Class;
using ZhiFang.IBLL.Common.BaseDictionary;

namespace ZhiFang.WebLis.ReportPrint
{
    public partial class FrxTest : BasePage
    {
        private readonly IBReportForm rfb = BLLFactory<IBReportForm>.GetBLL("ReportForm");
        private readonly Dictionary dic = BLLFactory<Dictionary>.GetBLL("Dictionary");
        private readonly IBShowFrom showform = BLLFactory<IBShowFrom>.GetBLL("ShowFrom");
        private readonly ZhiFang.IBLL.Report.IBUserReportFormDataListShowConfig iburfdlsc = BLLFactory<IBUserReportFormDataListShowConfig>.GetBLL("UserReportFormDataListShowConfig");
        private ZhiFang.Model.ReportForm rfm = new ZhiFang.Model.ReportForm();
        private SearchConditions sc = new SearchConditions();
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ZhiFang.WebLis.Ashx.ReportPrint));
            if (!IsPostBack)
            {
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
                    System.Web.UI.HtmlControls.HtmlSelect Doctor = (System.Web.UI.HtmlControls.HtmlSelect)this.Page.FindControl("Doctor");
                    Doctor.DataSource = dic.Doctor().Tables[0];
                    Doctor.DataTextField = "CName";
                    Doctor.DataValueField = "DoctorNo";
                    Doctor.DataBind();
                    Doctor.Items.Insert(0, new ListItem("", ""));
                    SelectSetValueByQuery("Doctor", Doctor);
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
                #region 开始时间
                if (base.CheckPageControlNull("StartDate"))
                {
                    System.Web.UI.HtmlControls.HtmlInputText StartDate = (System.Web.UI.HtmlControls.HtmlInputText)this.Page.FindControl("StartDate");
                    StartDate.Value = DateTime.Now.ToShortDateString();
                }
                #endregion
                #region 结束时间
                if (base.CheckPageControlNull("EndDate"))
                {
                    System.Web.UI.HtmlControls.HtmlInputText EndDate = (System.Web.UI.HtmlControls.HtmlInputText)this.Page.FindControl("EndDate");
                    EndDate.Value = DateTime.Now.AddDays(7).ToShortDateString();
                }
                #endregion
                #region 病历号
                if (base.CheckPageControlNull("PatNo"))
                {
                    System.Web.UI.HtmlControls.HtmlInputText PatNo = (System.Web.UI.HtmlControls.HtmlInputText)this.Page.FindControl("PatNo");
                    TextBoxSetValueByQuery("PatNo", PatNo);
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
            }
            this.showclasslist.InnerHtml = ZhiFang.WebLis.Class.ShowTools.ShowClassList(this.PageName.Value.Trim());

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
            if (base.CheckQueryString(a))
            {
                try
                {
                    h.Value = base.ReadQueryString(a).Trim();
                    h.Disabled = true;
                }
                catch
                {

                }
            }
        }
        protected void linkGetAllItem_Click(object sender, EventArgs e)
        {
            DateTime? StartDatet;
            DateTime? EndDatet;
            if (base.CheckPageControlNull("StartDate"))
            {
                StartDatet = Convert.ToDateTime(base.ReadForm("StartDate"));
            }
            else
            {
                StartDatet = null;
            }
            if (base.CheckPageControlNull("EndDate"))
            {
                EndDatet = Convert.ToDateTime(base.ReadForm("EndDate"));
            }
            else
            {
                EndDatet = null;
            }
            if (base.CheckPageControlNull("Dept") && base.CheckFormNullAndValue("Dept") && ZhiFang.Common.Public.PageValidate.IsNumber(base.ReadForm("Dept").Trim()))
            {
                rfm.DeptNo = Convert.ToInt32(base.ReadForm("Dept"));
            }
            if (base.CheckPageControlNull("Doctor") && base.CheckFormNullAndValue("Doctor"))
            {
                rfm.Doctor = base.ReadForm("Doctor");
            }
            if (base.CheckPageControlNull("District") && base.CheckFormNullAndValue("District") && ZhiFang.Common.Public.PageValidate.IsNumber(base.ReadForm("District").Trim()))
            {
                rfm.DistrictNo = Convert.ToInt32(base.ReadForm("District"));
            }
            if (base.CheckPageControlNull("SickType") && base.CheckFormNullAndValue("SickType") && ZhiFang.Common.Public.PageValidate.IsNumber(base.ReadForm("SickType").Trim()))
            {
                rfm.SickTypeNo = Convert.ToInt32(base.ReadForm("SickType"));
            }

            if (base.CheckQueryString("PatNo"))
            {
                rfm.PatNo = base.ReadQueryString("PatNo");
            }
            else
            {
                if (base.CheckPageControlNull("PatNo") && base.CheckFormNullAndValue("PatNo"))
                {
                    rfm.PatNo = base.ReadForm("PatNo");
                }
            }

            if (base.CheckQueryString("CName"))
            {
                rfm.CName = base.ReadQueryString("CName");
            }
            else
            {
                if (base.CheckPageControlNull("Name") && base.CheckFormNullAndValue("Name"))
                {
                    rfm.CName = base.ReadForm("Name");
                }
            }

            if (base.CheckQueryString("Bed"))
            {
                rfm.Bed = base.ReadQueryString("Bed");
            }
            else
            {
                if (base.CheckPageControlNull("Bed") && base.CheckFormNullAndValue("Bed"))
                {
                    rfm.Bed = base.ReadForm("Bed");
                }
            }

            if (base.CheckQueryString("SerialNo"))
            {
                rfm.SerialNo = base.ReadQueryString("SerialNo");
            }
            else
            {
                if (base.CheckPageControlNull("SerialNo") && base.CheckFormNullAndValue("SerialNo"))
                {
                    rfm.SerialNo = base.ReadForm("SerialNo");
                }
            }

            if (base.CheckQueryString("SectionNo"))
            {
                try
                {
                    rfm.SectionNo = Convert.ToInt32(base.ReadQueryString("SectionNo"));
                }
                catch
                {

                }
            }
            else
            {
                if (base.CheckPageControlNull("Section") && base.CheckFormNullAndValue("Section"))
                {
                    rfm.SectionNo = Convert.ToInt32(base.ReadForm("Section"));
                }
            }
            DataSet ds = rfb.GetList(rfm, StartDatet, EndDatet);
            DataTable dt = ds.Tables[0];
            this.tmpcondition.Value = sc.searchconditionsstr(rfm, StartDatet, EndDatet);
            this.tablehead.InnerHtml = ZhiFang.WebLis.Class.ShowTools.GridViewHeadShow(this.PageName.Value.Trim(), Convert.ToInt32(this.tmpclassid.Value.Trim()));
            showlistandfrom(dt, 0);
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
                        orderstr += s[i] + " " + this.OrderColumnFlag.Value.ToString().Trim() + ",";
                    }
                    orderstr = orderstr.Substring(0, orderstr.Length - 1);
                    dv.Sort = orderstr;
                }
                string[] tmpstra = ZhiFang.WebLis.Class.ShowTools.GridViewShow(this.PageName.Value.Trim(), Convert.ToInt32(this.tmpclassid.Value.Trim()), dv, pageindex);
                this.tablelist.InnerHtml = tmpstra[1];
                this.ShowFormTypeList.InnerHtml = ZhiFang.WebLis.Class.ShowTools.ShowFormTypeList(tmpstra[0].Split(',')[0], Convert.ToInt32(tmpstra[0].Split(',')[1]), this.PageName.Value.Trim());
                string aaa = showform.ShowReportForm(tmpstra[0].Split(',')[0], Convert.ToInt32(tmpstra[0].Split(',')[1]), this.PageName.Value.Trim(), 0);
                try
                {
                    aaa = aaa.Substring(aaa.IndexOf("<html"), aaa.Length - aaa.IndexOf("<html"));
                }
                catch
                {

                }
                aaa.Replace("\r\n ", " ");
                this.ShowFormHtml.InnerHtml = aaa;
                FirstFormNo.Value = tmpstra[0].Split(',')[0].ToString().Trim();
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), System.Guid.NewGuid().ToString(), "try{document.getElementById('" + tmpstra[0].Split(',')[0].ToString().Trim() + "').className='leftlist1focus';}catch(e){ alert('" + tmpstra[0].Split(',')[0].ToString().Trim() + "'); }", true);
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), System.Guid.NewGuid().ToString(), "try{document.getElementById('PIndex').value='" + pageindex + "';}catch(e){ alert('3'); }", true);
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), System.Guid.NewGuid().ToString(), "try{document.getElementById('sT1').style.display='block';}catch(e){ alert('1'); }", true);
                //ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), System.Guid.NewGuid().ToString(), "alert('"+Session.Timeout.ToString()+"')", true);
                //ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), System.Guid.NewGuid().ToString(), "alert('" + ds.Tables[0].Rows[0]["FormNo"].ToString().Trim() + ", " + ds.Tables[0].Rows[0]["SectionNo"].ToString().Trim() + "');", true);
            }
            else
            {
                this.tablelist.InnerHtml = "";
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

    }
}