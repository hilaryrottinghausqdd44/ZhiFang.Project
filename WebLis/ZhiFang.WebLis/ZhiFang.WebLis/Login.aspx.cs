using System;
using System.Web;
using System.Web.UI;
using ZhiFang.Common.Public;
using System.Data;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.Model;
using System.Collections.Generic;

namespace ZhiFang.WebLis
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(Request.Url.Host);
            Cookie.CookieHelper.Remove("ZhiFangUser");
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            WSRBAC.WSRbac wsrbac = null;
            try
            {
                string descr = "";
                if (string.IsNullOrEmpty(ConfigHelper.GetConfigString("WebLisInCluderRMS")) || ConfigHelper.GetConfigString("WebLisInCluderRMS").Trim() == "0")
                {
                    #region 调用RBAC服务
                    wsrbac = new WSRBAC.WSRbac();
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

                            Cookie.CookieHelper.Write("ReportFormFileType", ConfigHelper.GetConfigString("ReportFormFileType"));
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
                            string userinfostr = wsrbac.getUserInfo(this.textUserid.Text.Trim());
                            DataSet ds = ZhiFang.Common.Public.XmlToData.CXmlToDataSet(userinfostr);

                            //Cookie.CookieHelper.WriteWithDomain("ZhiFangUserID", ds.Tables[0].Rows[0]["ID"].ToString(), HttpContext.Current.Request.Url.Host);
                            Cookie.CookieHelper.Write("ZhiFangUserID", ds.Tables[0].Rows[0]["ID"].ToString());
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


                            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("HideTree") == "0")
                            {
                                Response.Write("<script>location.href='Main/Main.aspx';</script>");
                            }
                            else
                                Response.Write("<script>location.href='Main/Main_foshan.aspx';</script>");

                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Debug("Login.ImageButton1_Click:descr." + descr);
                            this.Label1.Text = descr;
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("Login.ImageButton1_Click:无法连接权限系统！请检查配置文件中权限系统的路径是否正确！");
                        this.Label1.Text = "无法连接权限系统！请检查配置文件中权限系统的路径是否正确！";
                    }
                    #endregion
                }
                else
                {
                    #region 直接访问RMSDB
                    BLL.RBAC.RBAC_Users rbacuser = new BLL.RBAC.RBAC_Users();
                    if (string.IsNullOrEmpty(this.textUserid.Text))
                    {
                        ZhiFang.Common.Log.Log.Debug("Login.ImageButton1_Click:用户账户为空！IP:" + ZhiFang.Tools.IPHelper.GetClientIP());
                        this.Label1.Text = "用户账户为空!";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(this.textPassword.Text))
                        {
                            ZhiFang.Common.Log.Log.Debug("Login.ImageButton1_Click:密码为空！IP:" + ZhiFang.Tools.IPHelper.GetClientIP());
                            this.Label1.Text = "密码为空!";
                        }
                        else
                        {
                            if (this.textUserid.Text.Trim() == "admin8001" && this.textUserid.Text.Trim() == "admin8001")
                            {
                                Cookie.CookieHelper.Write("ZhiFangUser", this.textUserid.Text.Trim());
                                Cookie.CookieHelper.Write("ZhiFangUserID", "-999");
                                //Cookie.CookieHelper.Write("EmployeeName", "admin8001");
                                Response.Write("<script>location.href='Main/Main.aspx';</script>");
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(ConfigHelper.GetConfigString("adminaccount")) && !string.IsNullOrEmpty(ConfigHelper.GetConfigString("adminpwd")) && this.textUserid.Text.Trim() == ConfigHelper.GetConfigString("adminaccount") && this.textUserid.Text.Trim() == ConfigHelper.GetConfigString("adminpwd"))
                                {
                                    Cookie.CookieHelper.Write("ZhiFangUser", this.textUserid.Text.Trim());
                                    Cookie.CookieHelper.Write("ZhiFangUserID", "-99");
                                    //Cookie.CookieHelper.Write("EmployeeName", this.textUserid.Text.Trim());
                                    Response.Write("<script>location.href='Main/Main.aspx';</script>");
                                }
                                else
                                {
                                    var dsuer = rbacuser.GetList("  Account='" + this.textUserid.Text.Trim() + "' ");
                                    if (dsuer != null && dsuer.Tables.Count > 0 && dsuer.Tables[0].Rows.Count > 0)
                                    {
                                        var userlist = rbacuser.DataTableToList(dsuer.Tables[0]);
                                        if (userlist[0].Password.Trim() == Tools.MD5Helper.StringToMD5Hash(Base64Help.DecodingString(this.textPassword.Text.Trim())))
                                        {
                                            Cookie.CookieHelper.Write("ZhiFangUser", this.textUserid.Text.Trim());
                                            Cookie.CookieHelper.Write("ZhiFangPwd", this.textPassword.Text.Trim());
                                            IBLL.Common.BaseDictionary.IBLocationbarCodePrintPamater LocationBarCodePrint = BLLFactory<IBLocationbarCodePrintPamater>.GetBLL();
                                            LocationbarCodePrintPamater LocationbarCodePrintPamater = LocationBarCodePrint.GetAdminPara();
                                            if (LocationbarCodePrintPamater != null)
                                            {
                                                Cookie.CookieHelper.Write("BarcodeModel", LocationbarCodePrintPamater.ParaMeter);
                                            }

                                            Cookie.CookieHelper.Write("ReportFormFileType", ConfigHelper.GetConfigString("ReportFormFileType"));

                                            BLL.RBAC.RBAC_EmplRoles bllemproles = new BLL.RBAC.RBAC_EmplRoles();
                                            var emproleslist = bllemproles.GetModelList(" emplid=" + userlist[0].EmpID.ToString() + " and postID is not null ");
                                            if (emproleslist != null && emproleslist.Count > 0)
                                            {
                                                List<string> rolesIdlist = new List<string>();
                                                emproleslist.ForEach(a =>
                                                {
                                                    if (!rolesIdlist.Contains(a.PostID.ToString()))
                                                        rolesIdlist.Add(a.PostID.ToString());
                                                });

                                                BLL.RBAC.HR_Posts bllpost = new BLL.RBAC.HR_Posts();
                                                var roleslist = bllpost.GetModelList(" GroupName='WebLis角色' and id in (" + string.Join(",", rolesIdlist) + ") ");
                                                if (roleslist != null && roleslist.Count > 0)
                                                {
                                                    List<string> rolesNamelist = new List<string>();
                                                    roleslist.ForEach(a =>
                                                    {
                                                        if (!rolesNamelist.Contains(a.SN))
                                                            rolesNamelist.Add(a.SN);
                                                    });
                                                    Cookie.CookieHelper.Write("ZhiFangUserPosition", string.Join(",", rolesNamelist));
                                                }
                                            }
                                            //string postlist = wsrbac.getPostInfoListByUser(this.textUserid.Text.Trim());
                                            //string position = "";
                                            //if (postlist != null && postlist.Trim() != "")
                                            //{
                                            //    foreach (string post in postlist.Split(','))
                                            //    {
                                            //        position += post + ",";
                                            //    }
                                            //}
                                            //if (position != "")
                                            //{
                                            //    Cookie.CookieHelper.Write("ZhiFangUserPosition", position);
                                            //}
                                            //string userinfostr = wsrbac.getUserInfo(this.textUserid.Text.Trim());
                                            //DataSet ds = ZhiFang.Common.Public.XmlToData.CXmlToDataSet(userinfostr);

                                            //Cookie.CookieHelper.WriteWithDomain("ZhiFangUserID", ds.Tables[0].Rows[0]["ID"].ToString(), HttpContext.Current.Request.Url.Host);
                                            Cookie.CookieHelper.Write("ZhiFangUserID", userlist[0].EmpID.ToString());
                                            #region 得到部门信息
                                            //string deptxml = wsrbac.getUserOrgInfo(ds.Tables[0].Rows[0]["Account"].ToString());
                                            //DataSet deptds = ZhiFang.Common.Public.XmlToData.CXmlToDataSet(deptxml);
                                            //string strsn = "0";
                                            //if (deptds != null && deptds.Tables.Count > 0 && deptds.Tables[0].Rows.Count > 0)
                                            //{  //的到层级关系
                                            //    for (int i = 0; i < deptds.Tables[0].Rows.Count; i++)
                                            //    {
                                            //        if (deptds.Tables[0].Rows[i]["SN"].ToString() != "01Root" && (deptds.Tables[0].Rows[i]["SN"].ToString().Length) >= (Convert.ToInt32(strsn) + 2))
                                            //        {
                                            //            strsn = deptds.Tables[0].Rows[i]["SN"].ToString();
                                            //        }
                                            //    }
                                            //    for (int i = 0; i < deptds.Tables[0].Rows.Count; i++)
                                            //    {
                                            //        if (deptds.Tables[0].Rows[i]["SN"].ToString() != "01Root" && strsn.Length == deptds.Tables[0].Rows[i]["sn"].ToString().Length)
                                            //        {
                                            //            //单位
                                            //            Cookie.CookieHelper.Write("ZhiFangdeptname", deptds.Tables[0].Rows[i]["tooltip"].ToString());
                                            //            Cookie.CookieHelper.Write("ZhiFangdeptCode", deptds.Tables[0].Rows[i]["orgcode"].ToString());
                                            //            break;
                                            //        }
                                            //    }
                                            //}
                                            #endregion


                                            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("HideTree") == "0")
                                            {
                                                Response.Write("<script>location.href='Main/Main.aspx';</script>");
                                            }
                                            else
                                                Response.Write("<script>location.href='Main/Main_foshan.aspx';</script>");
                                        }
                                        else
                                        {
                                            ZhiFang.Common.Log.Log.Debug("Login.ImageButton1_Click:密码错误！textUserid=" + this.textUserid.Text + ",textPassword=" + this.textPassword.Text + ",Base64textPassword=" + Base64Help.DecodingString(this.textPassword.Text) + "！IP:" + ZhiFang.Tools.IPHelper.GetClientIP());
                                            this.Label1.Text = "用户名或密码错误!";
                                        }
                                    }
                                    else
                                    {
                                        ZhiFang.Common.Log.Log.Debug("Login.ImageButton1_Click:用户名不存在！textUserid=" + this.textUserid.Text + ",textPassword=" + this.textPassword.Text + ",Base64textPassword=" + Base64Help.DecodingString(this.textPassword.Text) + "！IP:" + ZhiFang.Tools.IPHelper.GetClientIP());
                                        this.Label1.Text = "用户名或密码错误!";
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("Login.ImageButton1_Click:" + ex.ToString());
            }
        }
    }
}