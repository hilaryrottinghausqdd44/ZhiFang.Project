using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using ZhiFang.Common.Log;
using System.Xml;
using ZhiFang.Digitlab.ReagentSys.BusinessObject;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Common.Public;
using ZhiFang.Digitlab.IBLL.Business;

namespace ZhiFang.Digitlab.ReagentSys.WeiXin
{
    public partial class WeiXinMainRouter : ZhiFang.Digitlab.ReagentSys.BusinessObject.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string weixinversion = "";
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("WeiXinVersion") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("WeiXinVersion").Trim() != "")
            {
                weixinversion = ZhiFang.Common.Public.ConfigHelper.GetConfigString("WeiXinVersion") + "_";
            }
            string registerURL = "../webapp/ui/register.html";
            string IndexURL =  "../webapp/ui/index.html";
            string loginURL = "../webapp/ui/register.html";
            string NewsURL = "../webapp/ui/News/NewsInfo.html";
            string NewsListURL = "../webapp/ui/News/List.html";
            string ORDERINFOPURCHASINGCENTERURL = "../webapp/rea/order/entry.html?type=0";
            string ORDERINFOCOMPURL = "../webapp/rea/order/edit.html?type=1";
            string ORDERCONFIRMURL = "../webapp/rea/order/show/info.html?";
            try
            {
                string opstr = "";
                string actionurl = loginURL;
                if (HttpContext.Current.Request.QueryString["operate"] != null && HttpContext.Current.Request.QueryString["operate"].ToString().Trim() != "")
                {
                    opstr = HttpContext.Current.Request.QueryString["operate"].ToString().Trim();
                }
                switch (opstr.ToUpper())
                {
                    case "INDEX": actionurl = IndexURL; break;
                    case "NEWS": actionurl = NewsURL + "?ffileid=" + HttpContext.Current.Request.QueryString["ffileid"].ToString().Trim(); break;
                    case "NEWSLIST": actionurl = NewsListURL + "?dictreeid=" + HttpContext.Current.Request.QueryString["dictreeid"].ToString().Trim() + "&dictreename" + HttpContext.Current.Request.QueryString["dictreeid"].ToString().Trim(); break;
                    case "ORDERINFOPURCHASINGCENTER": actionurl = ORDERINFOPURCHASINGCENTERURL + "&id=" + HttpContext.Current.Request.QueryString["id"].ToString().Trim(); break;
                    case "ORDERINFOCOMP": actionurl = ORDERINFOCOMPURL + "&id=" + HttpContext.Current.Request.QueryString["id"].ToString().Trim(); break;
                    case "ORDERCONFIRM": actionurl = ORDERCONFIRMURL + "&id=" + HttpContext.Current.Request.QueryString["id"].ToString().Trim(); break;
                    default: break;
                }
                string openid = "";
                ZhiFang.Common.Log.Log.Debug("Router:1");
                foreach (var t in HttpContext.Current.Request.QueryString.AllKeys)
                {
                    ZhiFang.Common.Log.Log.Info("querystring:key=" + t.ToString() + ",value=" + HttpContext.Current.Request.QueryString[t].ToString());
                }
                ZhiFang.Common.Log.Log.Debug("Router:2,code:" + HttpContext.Current.Request.QueryString["code"].ToString());
                UserAuthorizeToken uat = base.GetUserAuthorizeToken(HttpContext.Current.Request.QueryString["code"].ToString());
                if (uat == null)
                {
                    Response.Write("未能获取身份信息！");
                    Response.End();
                }
                ZhiFang.Common.Log.Log.Debug("Router:21uat.openid" + uat.openid);
                openid = uat.openid;
                ZhiFang.Common.Log.Log.Debug("Router:21openid:" + openid);
                var userinfo = base.GetOpenIdInfo(openid);
                if (userinfo == null)
                {
                    Response.Write("未能获取用户信息！");
                    Response.End();
                }
                ZhiFang.Common.Log.Log.Debug("Router:22");
                ZhiFang.Common.Public.Cookie.CookieHelper.Write(ZhiFang.Digitlab.Entity.DicCookieSession.WeiXinOpenID, openid, 3600);
                ZhiFang.Common.Public.SessionHelper.SetSessionValue(ZhiFang.Digitlab.Entity.DicCookieSession.WeiXinOpenID, openid);
                ZhiFang.Common.Log.Log.Debug("Router:33" + Request.Url.AbsolutePath);
                Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
                var bwxa = context.GetObject("BBWeiXinAccount") as IBBWeiXinAccount;
                ZhiFang.Common.Log.Log.Debug("Router:3");
                if (!bwxa.CheckWeiXinAccountByOpenID(openid))
                {
                    ZhiFang.Common.Log.Log.Debug("Router:4");
                    BWeiXinAccount BWeiXinAccount = new BWeiXinAccount();
                    BWeiXinAccount.UserName = userinfo.nickname;
                    BWeiXinAccount.WeiXinAccount = userinfo.openid;
                    BWeiXinAccount.SexID = userinfo.sex;
                    BWeiXinAccount.CountryName = userinfo.country;
                    BWeiXinAccount.ProvinceName = userinfo.province;
                    BWeiXinAccount.CityName = userinfo.city;
                    if (userinfo.language != null)
                    {
                        BWeiXinAccount.Language = userinfo.language;
                    }
                    bwxa.Entity = BWeiXinAccount;
                    ZhiFang.Common.Log.Log.Debug("Router:5");
                    bwxa.Add();
                    ZhiFang.Common.Log.Log.Debug("Router:6");
                    ZhiFang.Common.Public.Cookie.CookieHelper.Write(ZhiFang.Digitlab.Entity.DicCookieSession.WeiXinUserID, bwxa.Entity.Id.ToString());
                    Response.Redirect(registerURL, true);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("Router:5");
                    long WeiXinAccountId;
                    bool LoginInputPasswordFlag;
                    long? EmpId;
                    if (bwxa.CheckWeiXinAccountByOpenID(openid, out WeiXinAccountId, out LoginInputPasswordFlag, out EmpId))
                    {
                        ZhiFang.Common.Log.Log.Debug("Router:6");
                        if (EmpId.HasValue)
                        {
                            ZhiFang.Common.Log.Log.Debug("Router:7");
                            Cookie.CookieHelper.Write(ZhiFang.Digitlab.Entity.DicCookieSession.WeiXinUserID, WeiXinAccountId.ToString());
                            var IBHREmployee = context.GetObject("BHREmployee") as ZhiFang.Digitlab.IBLL.RBAC.IBHREmployee;
                            ZhiFang.Digitlab.Entity.HREmployee emp = IBHREmployee.Get(EmpId.Value);
                            if (emp != null)
                            {
                                //Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.LabID, "");
                                //Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.IsLabFlag, "");

                                //SessionHelper.SetSessionValue(SysPublicSet.SysDicCookieSession.LabID, emp.LabID.ToString());//实验室ID
                                //Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.LabID, emp.LabID.ToString());//实验室ID
                                //if (emp.LabID > 0)
                                //    Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.IsLabFlag, "1");

                                if (emp.RBACUserList != null && emp.RBACUserList.Count > 0)
                                {
                                    SessionHelper.SetSessionValue(DicCookieSession.UserAccount, emp.RBACUserList[0].Account);//员工账户名
                                    SessionHelper.SetSessionValue(DicCookieSession.UseCode, emp.RBACUserList[0].UseCode);//员工代码
                                    Cookie.CookieHelper.Write(DicCookieSession.UserID, emp.RBACUserList[0].Id.ToString());
                                    Cookie.CookieHelper.Write(DicCookieSession.UserAccount, emp.RBACUserList[0].Account);
                                    Cookie.CookieHelper.Write(DicCookieSession.UseCode, emp.RBACUserList[0].UseCode);
                                }

                                //Cookie.CookieHelper.Write("000500", "4794031815009582380"); // 模块ID
                                SessionHelper.SetSessionValue(DicCookieSession.EmployeeID, emp.Id); //员工ID
                                SessionHelper.SetSessionValue(DicCookieSession.EmployeeName, emp.CName);//员工姓名 
                                SessionHelper.SetSessionValue(DicCookieSession.EmployeeUseCode, emp.UseCode);//员工代码                                 

                                //员工时间戳
                                //SessionHelper.SetSessionValue(rbacUser.HREmployee.Id.ToString(), rbacUser.HREmployee.DataTimeStamp);

                                Cookie.CookieHelper.Write(DicCookieSession.EmployeeID, emp.Id.ToString());// 员工ID
                                Cookie.CookieHelper.Write(DicCookieSession.EmployeeName, emp.CName);// 员工姓名
                                Cookie.CookieHelper.Write(DicCookieSession.EmployeeUseCode, emp.UseCode);// 员工代码

                                if (emp.HRDept != null)
                                {
                                    SessionHelper.SetSessionValue(DicCookieSession.HRDeptID, emp.HRDept.Id);//部门ID
                                    SessionHelper.SetSessionValue(DicCookieSession.HRDeptName, emp.HRDept.CName);//部门名称
                                    Cookie.CookieHelper.Write(DicCookieSession.HRDeptID, emp.HRDept.Id.ToString());//部门ID
                                    Cookie.CookieHelper.Write(DicCookieSession.HRDeptName, emp.HRDept.CName);//部门名称
                                    Cookie.CookieHelper.Write(DicCookieSession.HRDeptCode, emp.HRDept.UseCode);//部门名称
                                }
                            }
                            else
                            {
                                ZhiFang.Common.Log.Log.Debug("WeiXinMainRouter.Page_Load.Emp为空!");
                            }
                            ZhiFang.Common.Log.Log.Debug("Router:6");
                            if (!LoginInputPasswordFlag)
                            {
                                //ZhiFang.Common.Log.Log.Debug("Router:8");
                                Response.Redirect(actionurl, false);
                            }
                            else
                            {

                                Response.Redirect(loginURL, false);
                            }
                        }
                        else
                        {
                            //ZhiFang.Common.Log.Log.Debug("Router:9");
                            Response.Redirect(registerURL, false);
                        }
                    }
                    else
                    {
                        //ZhiFang.Common.Log.Log.Debug("Router:9");
                        Response.Redirect(registerURL, false);
                    }
                }
                //Response.Write(userinfo.nickname);
                //Response.Write("<img src='" + userinfo.headimgurl + "'>");
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("Router异常:" + ex.ToString());
                Response.Write("程序运行错误！请联系管理员！");
            }
        }
    }
}