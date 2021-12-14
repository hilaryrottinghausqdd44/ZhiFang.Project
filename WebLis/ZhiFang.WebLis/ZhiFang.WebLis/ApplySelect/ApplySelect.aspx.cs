using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.IBLL.Report;
using ZhiFang.BLLFactory;
using System.Data;
using System.Text;
using ZhiFang.WebLis.Class;
using Common;
using ZhiFang.IBLL.Common.BaseDictionary;

namespace ZhiFang.WebLis.ApplySelect
{
    public partial class ApplySelect : BasePage
    {
        private readonly IBBarCodeForm bbcf = BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
        private readonly IBNRequestItem rib = BLLFactory<IBNRequestItem>.GetBLL("NRequestItem");
        private readonly IBNRequestForm rfb = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
        ZhiFang.Model.NRequestForm nrf_m = new Model.NRequestForm();
        ZhiFang.Model.BarCodeForm bcf = new Model.BarCodeForm();
        public DataSet dsclient = new DataSet();
        private readonly IBBusinessLogicClientControl iblcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 送检开始时间
                System.Web.UI.HtmlControls.HtmlInputText StartDate = (System.Web.UI.HtmlControls.HtmlInputText)this.Page.FindControl("StartDate");
                if (ConfigHelper.GetConfigInt("SearchBeforeDayNum") == null)
                {
                    txtStartDate.Value = DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd");
                }
                else
                {
                    txtStartDate.Value = DateTime.Now.AddDays(ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value).ToString("yyyy-MM-dd");
                }
                TextBoxSetValueByQuery("StartDate", StartDate);
                #endregion
                #region 送检结束时间
                System.Web.UI.HtmlControls.HtmlInputText EndDate = (System.Web.UI.HtmlControls.HtmlInputText)this.Page.FindControl("EndDate");
                txtEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                TextBoxSetValueByQuery("EndDate", EndDate);
                #endregion
                #region 送检单位
                if (base.CheckCookies("ZhiFangUser"))
                {
                    User u = new User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
                    ZhiFang.Common.Log.Log.Info("公司名称：" + u.CompanyName + "用户账号：" + u.Account);
                    DataSet ds = u.GetClientListByPost("", -1);
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
                #endregion
                BindClient(txtStartDate.Value, txtEndDate.Value, Client.Value);
                BindGridView(dsclient.Tables[0].Rows[0]["WebLisSourceOrgId"].ToString());
            }
        }
        public void BindClient(string txtStartDate, string txtEndDate, string ClientNo)
        {           
            bcf.CollectDateStart = txtStartDate;
            bcf.CollectDateEnd = txtEndDate;
            bcf.WebLisSourceOrgId = ClientNo;
            dsclient = bbcf.GetWeblisOrgName(bcf);
            GridView1.DataSource = dsclient.Tables[0];
            GridView1.DataBind();
        }
        public void BindGridView(string ClientNo)
        {            
            DataSet dsAll = new DataSet();
            bcf.WebLisSourceOrgId = ClientNo;
            dsAll = bbcf.GetAllList(bcf);
            GridView2.DataSource = dsAll.Tables[0];
            GridView2.DataBind();
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ZhiFang.Model.NRequestForm nrf_m = new Model.NRequestForm();
            if (txtStartDate.Value.Trim() != "")
            {
                nrf_m.OperDateStart = txtStartDate.Value;
            }
            if (txtEndDate.Value.Trim() != "")
            {
                nrf_m.OperDateEnd = txtEndDate.Value + " 23:59:59";
            }
            if (Client.Value.Trim() != "")
            {
                nrf_m.ClientNo = Client.Value;
            }
            BindClient(txtStartDate.Value, txtEndDate.Value, Client.Value);
            BindGridView(dsclient.Tables[0].Rows[0]["WebLisSourceOrgId"].ToString());
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView(dsclient.Tables[0].Rows[0]["WebLisSourceOrgId"].ToString());
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            BindGridView(dsclient.Tables[0].Rows[0]["WebLisSourceOrgId"].ToString());
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='SkyBlue'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
                e.Row.Attributes.Add("onclick", "document.getElementById('ClientNo').value=" + e.Row.Cells[1].Text.ToString() + ";document.getElementById('Button1').click();");
            }

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string str = GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsAll = new DataSet();
            bcf.WebLisSourceOrgId = GridView1.SelectedValue.ToString();
            dsAll = bbcf.GetAllList(bcf);
            GridView2.DataSource = dsAll.Tables[0];
            GridView2.DataBind();
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='SkyBlue'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bcf.CollectDateStart = txtStartDate.Value;
            bcf.CollectDateEnd = txtEndDate.Value;
            bcf.WebLisSourceOrgId = ClientNo.Value;
            BindGridView(ClientNo.Value);
            
        }
      
    }
}