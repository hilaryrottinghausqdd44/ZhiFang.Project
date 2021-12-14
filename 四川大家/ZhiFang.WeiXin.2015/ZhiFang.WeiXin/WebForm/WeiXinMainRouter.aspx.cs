using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.Entity.RBAC;
using ZhiFang.WeiXin.BusinessObject;
using ZhiFang.WeiXin.IBLL;

namespace ZhiFang.WeiXin
{
    public partial class WeiXinMainRouter : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string weixinversion = "";
            if (ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("WeiXinVersion") != null && ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("WeiXinVersion").Trim() != "")
            {
                weixinversion = ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("WeiXinVersion") + "_";
            }
            string registerURL = "../web_app/" + weixinversion + "ui/register.html";
            string IndexURL = "../web_app/" + weixinversion + "ui/mall/index.html?type=1";
            string ReportFormInfoURL = "../web_app/" + weixinversion + "ui/mall/patient/report/info.html";
            string loginURL = "../web_app/" + weixinversion + "ui/login.html";
            string DoctorHelpURL = "../web_app/" + weixinversion + "ui/doctorhelp.html";
            string DoctorEntryURL = "../web_app/" + weixinversion + "ui/mall/index.html?type=2";
            string DoctorAccountBindURL = "../web_app/" + weixinversion + "ui/mall/index.html?type=2";
            string ShoppingCartURL = "../web_app/" + weixinversion + "ui/shoppingcart.html";
            //string PayTestExportURL = "../PayTest/PayForm.aspx";
            string PayTestJSAPIURL = "../PayTest/PayJSAPIForm.aspx";
            string PayTestExportURL = "../PayTest/PayForm.aspx";
            string OrderFormPushURL = "../web_app/" + weixinversion + "ui/mall/patient/orderd/info.html";
            string ReportSearchURL = "../web_app/" + weixinversion + "ui/mall/patient/report/search/list.html";
            string UserInfoQRCodeURL = "../web_app/" + weixinversion + "ui/mall/patient/user/barcode.html";
            string UserOrderSpendURL = "../web_app/" + weixinversion + "ui/mall/patient/order/list.html?statusStr=2";

            try
            {
                string opstr = "";
                string actionurl = loginURL;
                string openid = "";
                ZhiFang.Common.Log.Log.Debug("Router:1");
                #region 写参数日志
                foreach (var t in HttpContext.Current.Request.QueryString.AllKeys)
                {
                    ZhiFang.Common.Log.Log.Info("querystring:key=" + t.ToString() + ",value=" + HttpContext.Current.Request.QueryString[t].ToString());
                }
                #endregion

                ZhiFang.Common.Log.Log.Debug("Router:2,code:" + HttpContext.Current.Request.QueryString["code"].ToString());

                #region 获取身份信息
                UserAuthorizeToken uat = base.GetUserAuthorizeToken(HttpContext.Current.Request.QueryString["code"].ToString());
                if (uat == null)
                {
                    Response.Write("未能获取身份信息！");
                    Response.End();
                }
                ZhiFang.Common.Log.Log.Debug("Router:21uat.openid:" + uat.openid);
                openid = uat.openid;
                ZhiFang.Common.Log.Log.Debug("Router:21openid:" + openid);
                var userinfo = base.GetOpenIdInfo(openid);
                if (userinfo == null)
                {
                    Response.Write("未能获取用户信息！");
                    Response.End();
                }
                ZhiFang.Common.Log.Log.Debug("Router:22");
                ZhiFang.WeiXin.Common.Cookie.CookieHelper.Write(DicCookieSession.WeiXinOpenID, uat.openid);
                ZhiFang.WeiXin.Common.Cookie.CookieHelper.Write(Entity.SysDicCookieSession.UserOpenID, uat.openid);
                ZhiFang.Common.Log.Log.Debug("Router:33" + Request.Url.AbsolutePath);
                Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
                #endregion
                var bwxa = context.GetObject("BBWeiXinAccount") as IBBWeiXinAccount;
                ZhiFang.Common.Log.Log.Debug("Router:3");
                if (HttpContext.Current.Request.QueryString["operate"] != null && HttpContext.Current.Request.QueryString["operate"].ToString().Trim() != "")
                {
                    opstr = HttpContext.Current.Request.QueryString["operate"].ToString().Trim();
                }
                switch (opstr.ToUpper())
                {
                    case "INDEX": actionurl = IndexURL; break;
                    case "SHOPPINGCART": actionurl = ShoppingCartURL; break;
                    case "DOCTHELP": actionurl = DoctorHelpURL; break;
                    case "DOCTENTRY": actionurl = DoctorEntryURL; break;
                    case "BARCODE": actionurl = IndexURL + "#" + HttpContext.Current.Request.QueryString["operate"].ToString(); break;
                    case "REPORT": actionurl = IndexURL + "#" + HttpContext.Current.Request.QueryString["operate"].ToString(); break;
                    case "ACCOUNT": actionurl = IndexURL + "#" + HttpContext.Current.Request.QueryString["operate"].ToString(); break;
                    case "PATIENT": actionurl = IndexURL + "#" + HttpContext.Current.Request.QueryString["operate"].ToString(); break;
                    case "PAYTESTEXPORT": actionurl = PayTestExportURL + "?openid=" + uat.openid + "&total_fee=" + ZhiFang.Common.Public.Base64Help.EncodingString("1") + "&"; break;
                    case "PAYTESTJSAPI": actionurl = PayTestJSAPIURL + "#" + HttpContext.Current.Request.QueryString["operate"].ToString(); break;
                    case "ORDERFORMPUSH": actionurl = OrderFormPushURL + "?hasbutton=1&id="+ HttpContext.Current.Request.QueryString["id"].ToString().Trim(); break;
                    case "REPORTSEARCH": actionurl = ReportSearchURL; break;
                    case "USERINFOQRCODE": actionurl = UserInfoQRCodeURL; break;
                    case "USERORDERSPEND": actionurl = UserOrderSpendURL; break;
                    default: break;
                }
                ZhiFang.WeiXin.Common.Cookie.CookieHelper.Write(DicCookieSession.WeiXinAppID, ZhiFang.Common.Public.ConfigHelper.GetConfigString("appid"));
                if (HttpContext.Current.Request.QueryString["operate"] != null && HttpContext.Current.Request.QueryString["operate"].ToString().Trim() != "" & HttpContext.Current.Request.QueryString["operate"].ToString().Trim().IndexOf("reportid") >= 0)
                {
                    //ZhiFang.Common.Log.Log.Debug("Router:7");
                    string operatestr = HttpContext.Current.Request.QueryString["operate"].ToString().Trim();
                    var ol = operatestr.Replace("reportid", "").Split('T');
                    actionurl = ReportFormInfoURL + "?id=" + ol[0] + "&MemberId=" + ol[1];
                }
                ZhiFang.Common.Log.Log.Debug("actionurl:" + actionurl);
                if (!bwxa.CheckWeiXinAccountByOpenID(uat.openid) && opstr.ToUpper() != "DOCTORACCOUNTBIND")
                {
                    ZhiFang.Common.Log.Log.Debug("Router:4");
                    ZhiFang.WeiXin.Entity.BWeiXinAccount BWeiXinAccount = new Entity.BWeiXinAccount();
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
                    bwxa.Save();
                    ZhiFang.WeiXin.Common.Cookie.CookieHelper.Write(DicCookieSession.WeiXinUserID, bwxa.Entity.Id.ToString());
                    ZhiFang.WeiXin.Common.Cookie.CookieHelper.Write(Entity.SysDicCookieSession.UserWeiXinAccountID, bwxa.Entity.Id.ToString());
                    Response.Redirect(IndexURL + "#" + HttpContext.Current.Request.QueryString["operate"].ToString(), false);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("Router:51");
                    if (opstr.ToUpper() == "DOCTORACCOUNTBIND")
                    {
                        //ZhiFang.Common.Log.Log.Debug("Router:52");
                        Response.Redirect(IndexURL + "#" + HttpContext.Current.Request.QueryString["operate"].ToString(), false);
                    }
                    else
                    {
                        //ZhiFang.Common.Log.Log.Debug("Router:53");
                        ZhiFang.WeiXin.Entity.BWeiXinAccount BWeiXinAccount;
                        bool LoginInputPasswordFlag;

                        if (bwxa.CheckWeiXinAccountMobileCodeByOpenID(uat.openid, out BWeiXinAccount, out LoginInputPasswordFlag))
                        {
                            ZhiFang.Common.Log.Log.Debug("WeiXinMainRouter,openid:" + openid + "@WeiXinAccountId:" + BWeiXinAccount.Id);
                            ZhiFang.WeiXin.Common.Cookie.CookieHelper.Write(DicCookieSession.WeiXinUserID, BWeiXinAccount.Id.ToString());
                            ZhiFang.WeiXin.Common.Cookie.CookieHelper.Write(Entity.SysDicCookieSession.UserWeiXinAccountID, BWeiXinAccount.Id.ToString());
                            ZhiFang.WeiXin.Common.Cookie.CookieHelper.Write(Entity.SysDicCookieSession.UserName,userinfo.nickname);
                            ZhiFang.WeiXin.Common.Cookie.CookieHelper.Write(Entity.SysDicCookieSession.ReadAgreement, (BWeiXinAccount.ReadAgreement.HasValue)?"1":"0");
                            //ZhiFang.Common.Log.Log.Debug("Router:6");
                            //if (LoginInputPasswordFlag)
                            //{
                            //    Response.Redirect(loginURL, false);
                            //}
                            if (opstr.ToUpper().IndexOf("DOCT") >= 0)
                            {
                                var bbda = context.GetObject("BBDoctorAccount") as IBBDoctorAccount;
                                long DoctorAccountID;
                                string HospitalCode;
                                long HospitalID;
                                string HospitalName;
                                string Name;
                                string BonusPercent;
                                long AreaID;
                                long DoctorAccountType;
                                bool tmpflag = bbda.CheckBDoctorAccountByWeiXinUserID(BWeiXinAccount.Id, out DoctorAccountID, out HospitalCode, out HospitalID, out HospitalName, out Name, out BonusPercent, out AreaID,out DoctorAccountType);
                                if (tmpflag)
                                {
                                    Common.Cookie.CookieHelper.Write(Entity.SysDicCookieSession.DoctorId, DoctorAccountID.ToString());
                                    Common.Cookie.CookieHelper.Write(Entity.SysDicCookieSession.DoctorHospital, HospitalID.ToString());
                                    Common.Cookie.CookieHelper.Write(Entity.SysDicCookieSession.DoctorHospitalCode, HospitalCode.ToString());
                                    Common.Cookie.CookieHelper.Write(Entity.SysDicCookieSession.DoctorHospitalName, HospitalName.ToString());
                                    Common.Cookie.CookieHelper.Write(Entity.SysDicCookieSession.DoctorName, Name.ToString());
                                    Common.Cookie.CookieHelper.Write(Entity.SysDicCookieSession.DoctorOpenID, uat.openid);
                                    Common.Cookie.CookieHelper.Write(Entity.SysDicCookieSession.DoctorWeiXinAccountID, BWeiXinAccount.Id.ToString());
                                    //ZhiFang.Common.Log.Log.Debug("AreaID:" + AreaID);
                                    Common.Cookie.CookieHelper.Write(Entity.SysDicCookieSession.AreaID, AreaID.ToString());
                                    Common.Cookie.CookieHelper.Write(Entity.SysDicCookieSession.DoctorAccountType, DoctorAccountType.ToString());
                                    int tmpd=0;
                                    if (BonusPercent!=null && BonusPercent.Trim()!="" && BonusPercent.Trim() != "0")
                                    {
                                        tmpd = int.Parse(BonusPercent);                                    
                                    }
                                    else
                                    {
                                        IBBParameter IBBParameter= context.GetObject("BBParameter") as IBBParameter;
                                        if (IBBParameter.GetCache(ZhiFang.WeiXin.BLL.BParameterParaNoClass.BonusPercent.Key.ToString()) != null && IBBParameter.GetCache(ZhiFang.WeiXin.BLL.BParameterParaNoClass.BonusPercent.Key.ToString()).ToString().Trim() != "")
                                        {
                                            ZhiFang.Common.Log.Log.Debug("AddUserOrderFormConfirmByOrderFormId,系统参数,医生的咨询费比率未配置！"+ IBBParameter.GetCache(ZhiFang.WeiXin.BLL.BParameterParaNoClass.BonusPercent.Key.ToString()).ToString());
                                            tmpd = int.Parse(IBBParameter.GetCache(ZhiFang.WeiXin.BLL.BParameterParaNoClass.BonusPercent.Key.ToString()).ToString());
                                        }
                                        else
                                        {
                                            ZhiFang.Common.Log.Log.Debug("AddUserOrderFormConfirmByOrderFormId,系统参数,医生的咨询费比率未配置！");
                                            //throw new Exception("系统参数,医生的咨询费比率未配置！");
                                        }
                                    }
                                    Common.Cookie.CookieHelper.Write(Entity.SysDicCookieSession.DoctorBonusPercent, tmpd.ToString());
                                }
                                else
                                {
                                    Common.Cookie.CookieHelper.Remove(Entity.SysDicCookieSession.DoctorId);
                                    Common.Cookie.CookieHelper.Remove(Entity.SysDicCookieSession.DoctorHospital);
                                    Common.Cookie.CookieHelper.Remove(Entity.SysDicCookieSession.DoctorHospitalCode);
                                    Common.Cookie.CookieHelper.Remove(Entity.SysDicCookieSession.DoctorHospitalName);
                                    Common.Cookie.CookieHelper.Remove(Entity.SysDicCookieSession.DoctorName);
                                    Common.Cookie.CookieHelper.Remove(Entity.SysDicCookieSession.DoctorOpenID);
                                    Common.Cookie.CookieHelper.Remove(Entity.SysDicCookieSession.DoctorWeiXinAccountID);
                                    Common.Cookie.CookieHelper.Remove(Entity.SysDicCookieSession.AreaID);
                                    Common.Cookie.CookieHelper.Remove(Entity.SysDicCookieSession.DoctorBonusPercent);
                                }
                                ZhiFang.Common.Log.Log.Debug("WeiXinMainRouter:Entity.SysDicCookieSession.DoctorId:" + Entity.SysDicCookieSession.DoctorId + "@value:" + Common.Cookie.CookieHelper.Read(Entity.SysDicCookieSession.DoctorId));
                            }
                            
                            Response.Redirect(actionurl, false);

                        }
                        else
                        {
                            //ZhiFang.Common.Log.Log.Debug("Router:9");
                            Response.Redirect(registerURL, false);
                        }
                    }
                }
                //Response.Write(userinfo.nickname);
                //Response.Write("<img src='" + userinfo.headimgurl + "'>");
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("Router:" + ex.ToString());
                Response.Write("程序运行错误！请联系管理员！");
            }
        }
    }

}