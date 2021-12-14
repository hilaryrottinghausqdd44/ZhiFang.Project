using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.Common.Public;
using System.Data;
using ZhiFang.WebLis.Class;
using System.Xml;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.Model;

namespace ZhiFang.WebLis
{
    public partial class LoginNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(Request.Url.Host);
            Cookie.CookieHelper.Remove("ZhiFangUser");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            WSRBAC.WSRbac wsrbac = null;
            try
            {
                wsrbac = new WSRBAC.WSRbac();
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("Login.ImageButton1_Click:" + ex.ToString());
            }
            string descr = "";
            if (wsrbac != null)
            {
                bool a = wsrbac.Login(this.textUserid.Text.Trim(), this.textPassword.Text.Trim(), out descr);
                if (a)
                {
                    Cookie.CookieHelper.Write("ZhiFangUser", this.textUserid.Text.Trim());
                    Cookie.CookieHelper.Write("ZhiFangPwd", this.textPassword.Text.Trim());
                    IBLL.Common.BaseDictionary.IBLocationbarCodePrintPamater LocationBarCodePrint = BLLFactory<IBLocationbarCodePrintPamater>.GetBLL();
                    LocationbarCodePrintPamater LocationbarCodePrintPamater = LocationBarCodePrint.GetAdminPara();
                    if (LocationbarCodePrintPamater != null)
                    {
                        Cookie.CookieHelper.Write("BarcodeModel", LocationbarCodePrintPamater.ParaMeter);
                    }
                    //if (ConfigHelper.GetConfigBool("ReportFormFileType"))
                    //{

                    Cookie.CookieHelper.Write("ReportFormFileType", ConfigHelper.GetConfigString("ReportFormFileType"));
                    //}
                    //ReportFormFileType
                    //Cookie.CookieHelper.WriteWithDomain("ZhiFangUser", this.textUserid.Text.Trim(), HttpContext.Current.Request.Url.Host);
                    string postlist = wsrbac.getPostInfoListByUser(this.textUserid.Text.Trim());
                    string position = "";
                    if (postlist != null && postlist.Trim() != "")
                    {
                        foreach (string post in postlist.Split(','))
                        {
                            position += post + ",";
                        }
                    }
                    if (position != "")
                    {
                        Cookie.CookieHelper.Write("ZhiFangUserPosition", position);
                    }
                    try
                    {
                        string userinfostr = wsrbac.getUserInfo(this.textUserid.Text.Trim());
                        DataSet ds = ZhiFang.Common.Public.XmlToData.CXmlToDataSet(userinfostr);

                        Cookie.CookieHelper.WriteWithDomain("ZhiFangUserID", ds.Tables[0].Rows[0]["ID"].ToString(), HttpContext.Current.Request.Url.Host);

                        #region 得到部门信息
                        string deptxml = wsrbac.getUserOrgInfo(ds.Tables[0].Rows[0]["Account"].ToString());
                        DataSet deptds = ZhiFang.Common.Public.XmlToData.CXmlToDataSet(deptxml);
                        string strsn = "0";
                        if (deptds != null && deptds.Tables.Count > 0 && deptds.Tables[0].Rows.Count > 0)
                        {  //的到层级关系
                            for (int i = 0; i < deptds.Tables[0].Rows.Count; i++)
                            {
                                if (deptds.Tables[0].Rows[i]["SN"].ToString() != "01Root" && (deptds.Tables[0].Rows[i]["SN"].ToString().Length) >= (Convert.ToInt32(strsn) + 2))
                                {
                                    strsn = deptds.Tables[0].Rows[i]["SN"].ToString();
                                }
                            }
                            for (int i = 0; i < deptds.Tables[0].Rows.Count; i++)
                            {
                                if (deptds.Tables[0].Rows[i]["SN"].ToString() != "01Root" && strsn.Length == deptds.Tables[0].Rows[i]["sn"].ToString().Length)
                                {
                                    //单位
                                    Cookie.CookieHelper.Write("ZhiFangdeptname", deptds.Tables[0].Rows[i]["tooltip"].ToString());
                                    Cookie.CookieHelper.Write("ZhiFangdeptCode", deptds.Tables[0].Rows[i]["orgcode"].ToString());
                                    break;
                                }
                            }
                        }
                        #endregion
                    }
                    catch
                    {

                    }

                    if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("HideTree") == "0")
                    {
                        Response.Write("<script>location.href='Main/Main.aspx';</script>");
                    }
                    else
                        Response.Write("<script>location.href='Main/Main_foshan.aspx';</script>");

                }
                else
                {
                    this.Label1.Text = descr;
                }
            }
            else
            {
                this.Label1.Text = "无法连接权限系统！请检查配置文件中权限系统的路径是否正确！";
            }
        }
    }
}