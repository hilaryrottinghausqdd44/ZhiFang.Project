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
using AjaxPro;
using System.Text;
using System.Net;
using ZhiFang.WebLis.Class;
using ZhiFang.IBLL.Report;
using ZhiFang.BLLFactory;
namespace Report.ReportPrint
{
    public partial class CreatModel : BasePage
    {
        private readonly IBReportForm rfb = BLLFactory<IBReportForm>.GetBLL("ReportForm");
        private readonly Dictionary dic = BLLFactory<Dictionary>.GetBLL("Dictionary");
        private readonly IBShowFrom showform = BLLFactory<IBShowFrom>.GetBLL("ShowFrom");
        private ZhiFang.Model.ReportForm rfm = new ZhiFang.Model.ReportForm();
        private ZhiFang.Model.ReportItem rim;
        private int PageSize = 15;
        private string CacheKey = "FromList-TechnicianPrint";
        protected void Page_Load(object sender, EventArgs e)
        {
            Common.WebClient WC = new Common.WebClient();

            WC.Encoding = Encoding.GetEncoding("GB2312");
            Response.Write(WC.RespHtml);
            //Response.Write(WC.GetHtml("http://www.sina.com.cn"));

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
                    SetValueByQuery("Dept", dept);
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
                    SetValueByQuery("Doctor", Doctor);
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
                    SetValueByQuery("District", District);
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
                    SetValueByQuery("SickType", SickType);
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
            }
        }
        private void SetValueByQuery(string a,System.Web.UI.HtmlControls.HtmlSelect h)
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
        protected void linkGetAllItem_Click(object sender, EventArgs e)
        {
            DateTime? StartDatet;
            DateTime? EndDatet;
            if (base.CheckPageControlNull("StartDate"))
            {
                StartDatet = Convert.ToDateTime(StartDate.Value);
            }
            else
            {
                StartDatet = null;
            }
            if (base.CheckPageControlNull("EndDate"))
            {
                EndDatet = Convert.ToDateTime(EndDate.Value);
            }
            else
            {
                EndDatet = null;
            }
            if (base.CheckPageControlNull("Dept"))
            {
                try
                {
                    rfm.DeptNo = Convert.ToInt32(Dept.Value);
                }
                catch
                {

                }
            }
            if (base.CheckPageControlNull("Doctor") && base.CheckFormNullAndValue("Doctor"))
            {
                rfm.Doctor = Doctor.Value;
            }
            if (base.CheckPageControlNull("District"))
            {
                try
                {
                    rfm.DistrictNo = Convert.ToInt32(District.Value);
                }
                catch
                {

                }
            }
            if (base.CheckPageControlNull("SickType"))
            {
                try
                {
                    rfm.SickTypeNo = Convert.ToInt32(SickType.Value);
                }
                catch
                {

                }
            }

            if (base.CheckQueryString("PatNo"))
            {
                rfm.PatNo = base.ReadQueryString("PatNo");
            }
            else
            {
                if (base.CheckPageControlNull("PatNo") && base.CheckFormNullAndValue("PatNo"))
                {
                    rfm.PatNo = PatNo.Value;
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
                    rfm.CName = Name.Value;
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
                    rfm.Bed = Bed.Value;
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
                    rfm.SerialNo = SerialNo.Value;
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


            DataSet ds = rfb.GetList(rfm, StartDatet, EndDatet);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataList1.DataSource = ds.Tables[0];
                DataList1.DataBind();
            }
            else
            {
                DataList1.DataSource = null;
                DataList1.DataBind();
            }
        }
    }
}
