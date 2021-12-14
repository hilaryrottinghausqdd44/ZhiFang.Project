using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.WebLis.Class;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.BLLFactory;
using System.Data;

namespace ZhiFang.WebLis.SetItemMerge
{
    public partial class ClientClassify : BasePage
    {
        private readonly IBClientProfile ibcp = BLLFactory<IBClientProfile>.GetBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ZhiFang.WebLis.SetItemMerge.ClientClassify));
            DataList1.DataSource = ibcp.GetAllList();
            DataList1.DataBind();
        }
        [AjaxPro.AjaxMethod()]
        public string[] ShowClientList(string ClientProfileCName, int PageIndex, int PageSize, int PageCol)
        {
            try
            {
                string[] aaa = new string[2];
                if (PageIndex < 0)
                {
                    return new string[2] { "", "" };
                }
                if (PageSize <= 0)
                {
                    return new string[2] { "", "" };
                }
                if (PageCol <= 0)
                {
                    return new string[2] { "", "" };
                }
                Model.ClientProfile ClientProfile = new Model.ClientProfile();
                ClientProfile.ClientProfileCName = ClientProfileCName;
                DataSet ds = ibcp.GetList(ClientProfile);
                string tr = "";
                string td = "";
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (i % PageCol != 0)
                        {
                            string cssstr = "border-top:#0099cc solid 0px;border-bottom:#0099cc solid 1px; ";
                            if ((i + 1) % PageCol != 0)
                            {
                                cssstr += "border-right:#0099cc solid 1px;";
                            }
                            else
                            {
                                cssstr += "border-right:#0099cc solid 0px;";
                            }
                            td += "<td align='center' style=\"background-color:#ffffff;width:" + Convert.ToInt32(100 / PageCol) + "%;" + cssstr + "\">" + ds.Tables[0].Rows[i]["CName"].ToString().Trim() + "</td>";
                        }
                        else
                        {
                            tr += "<tr height=\"25\">" + td + "</tr>";
                            td = "<td align='center'  style=\"background-color:#ffffff;width:" + Convert.ToInt32(100 / PageCol) + "%;border-right:#0099cc solid 1px;border-bottom:#0099cc solid 1px;\">" + ds.Tables[0].Rows[i]["CName"].ToString().Trim() + "</td>";
                        }
                    }
                    tr += "<tr height=\"25\">" + td + "</tr>";
                    aaa[0] = "<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"background-color:#0099cc\">" + tr + "</table>";
                }
                return aaa;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                return new string[2] { "程序运行错误！", "" };
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            DataList1.DataSource = ibcp.GetAllList();
            DataList1.DataBind();
        }
    }
}