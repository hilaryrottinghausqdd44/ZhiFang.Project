using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ZhiFang.Common.Log;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;

namespace ZhiFang.LIIP.WeiXin.Mini
{
    public class WeiXinMiniBasePage : System.Web.UI.Page
    {
        //微信通用
        static string appid = ZhiFang.Common.Public.ConfigHelper.GetConfigString("wxmini_appid");
        static string appsecret = ZhiFang.Common.Public.ConfigHelper.GetConfigString("wxmini_secret");

        public static string GetTokenUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential";
        public static string auth_code2Session_URL = "https://api.weixin.qq.com/sns/jscode2session";

        //static string GetOpenIdListUrl = "https://api.weixin.qq.com/cgi-bin/user/get";
        //static string GetOpenIdUrl = "https://api.weixin.qq.com/cgi-bin/user/info";
        //static string GetUserAuthorizeTokenUrl = ZhiFang.Common.Public.ConfigHelper.GetConfigString("GetUserAuthorizeTokenUrl");
        //static string GetUserAuthorizeTokenRefreshUrl = ZhiFang.Common.Public.ConfigHelper.GetConfigString("GetUserAuthorizeTokenRefreshUrl");
        //static string GetJSAPITokenUrl = ZhiFang.Common.Public.ConfigHelper.GetConfigString("GetJSAPITokenUrl");
        //static string PushMsgUrl = ZhiFang.Common.Public.ConfigHelper.GetConfigString("PushMsgUrl");

        //微信公众号模版消息
        static string SetBusinessUrl = ZhiFang.Common.Public.ConfigHelper.GetConfigString("SetBusinessUrl");
        static string GetBusinessTemplateIdUrl = ZhiFang.Common.Public.ConfigHelper.GetConfigString("GetBusinessTemplateIdUrl");
        static string PushMessageTemplateContextUrl = ZhiFang.Common.Public.ConfigHelper.GetConfigString("PushMessageTemplateContextUrl");
        static string GetMediaUrl = ZhiFang.Common.Public.ConfigHelper.GetConfigString("GetMediaUrl");
        static string GetPermanentMediaListUrl = ZhiFang.Common.Public.ConfigHelper.GetConfigString("GetPermanentMediaListUrl");
        static string GetPermanentMediaFileUrl= ZhiFang.Common.Public.ConfigHelper.GetConfigString("GetPermanentMediaFileUrl");


        //模版消息
        //static WCF_WebClient wc = new WCF_WebClient();
        public static string Appid
        {
            get {
                if (string.IsNullOrEmpty(appid))
                {
                    throw new Exception("程序异常！小程序APPID未配置");
                }
                return appid; }
            set { appid = value; }
        }
        public static string Appsecret
        {
            get {
                if (string.IsNullOrEmpty(appsecret))
                {
                    throw new Exception("程序异常！小程序secret未配置");
                }
                return appsecret; }
            set { appsecret = value; }
        }
        public void GetToken()
        {
            try
            {
                GetTokenP(this.Application, appid, appsecret);
            }
            catch (Exception ex)
            {
                Log.Debug("WeiXinMiniBasePage.GetToken异常:" + ex.ToString(), ex);
                throw ex;
            }
        }
        public static void GetTokenP(HttpApplicationState application, string appid, string appsecret)
        {
            try
            {
                if (application.Get("tokentime") == null || Convert.ToDateTime(application.Get("tokentime").ToString()) <= DateTime.Now.AddMinutes(20))
                {
                    Log.Debug("WeiXinMiniBasePage.GetTokenP.获取TokenP.开始");
                    string TokenResult = ZhiFang.LIIP.Common.RestfullHelper.InvkerRestServiceByGet(GetTokenUrl + "&appid=" + appid.Trim() + "&secret=" + appsecret.Trim() + "&lang=zh_CN").ToString();

                    JsonSerializerSettings jss = new JsonSerializerSettings();
                    jss.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    TokenResult tr = (TokenResult)JsonConvert.DeserializeObject(TokenResult, typeof(TokenResult), jss);
                    if (tr.errcode == 0)
                    {
                        Log.Debug("WeiXinMiniBasePage.GetTokenP.获取成功！TokenP_token:" + tr.access_token);
                        application.Set("token", tr.access_token);
                        application["tokentime"] = DateTime.Now.AddHours(2).ToString();
                        Log.Debug("WeiXinMiniBasePage.GetTokenP.获取设置成功！token=" + tr.access_token+"tokentime = " + DateTime.Now.AddHours(2).ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Debug("WeiXinMiniBasePage.GetTokenP.GetTokenP异常:" + ex.ToString(), ex);
                throw ex;
            }
        }
        

        public static Auth_code2SessionResult Auth_code2Session(string code)
        {
            try
            {
                Log.Debug("WeiXinMiniBasePage.Auth_code2Session.开始！");
                string url = $"{auth_code2Session_URL}?appid={ Appid}&secret={Appsecret}&js_code={code}&grant_type=authorization_code";
                Log.Debug("WeiXinMiniBasePage.Auth_code2Session.Url:" + url);
                string str_userauthorizetoken = ZhiFang.LIIP.Common.RestfullHelper.InvkerRestServiceByGet(url).ToString();
                Log.Debug("WeiXinMiniBasePage.Auth_code2Session.str_userauthorizetoken:" + str_userauthorizetoken);

                Auth_code2SessionResult auth_code2Sessionresult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<Auth_code2SessionResult>(str_userauthorizetoken);
                if (auth_code2Sessionresult.errcode == 0)
                {
                    return auth_code2Sessionresult;
                }
                else
                {
                    Log.Debug($"WeiXinMiniBasePage.Auth_code2Session.errmsg:{auth_code2Sessionresult.errmsg},errcode:{auth_code2Sessionresult.errcode}");
                    switch (auth_code2Sessionresult.errcode)
                    {
                        case -1:
                            throw new Exception("程序异常！小程序系统繁忙，此时请开发者稍候再试！"); 
                        case 40029:
                            throw new Exception("程序异常！程序异常！code 无效!");
                        case 45011:
                            throw new Exception("程序异常！小程序频率限制，每个用户每分钟100次.");
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug($"WeiXinMiniBasePage.Auth_code2Session.异常：{ex.ToString()}");
                throw new Exception("程序异常！");
            }
        }

        
        
        /// <summary>
        /// 推送模板消息5
        /// </summary>
        /// <param name="application">application</param>
        /// <param name="touser">接收者OpenID</param>
        /// <param name="template_id">模板ID</param>
        /// <param name="url">链接地址</param>
        /// <param name="topcolor">标题颜色</param>
        /// <param name="data">数据内容</param>
        /// <returns></returns>
        public static PushMessageTemplateResult PushMessageTemplate5Context(HttpApplicationState application, string touser, string template_id, string url, string topcolor, TemplateIdObject5 data)
        {
            try
            {
                GetTokenP(application, appid, appsecret);
                string msg;
                string datastr = "";
                if (data != null)
                {
                    datastr = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(data);
                }
                return PushMessageTemplateAction(application, touser, template_id, url, topcolor, datastr, out msg);
            }
            catch (Exception ex)
            {
                Log.Debug("WeiXinMiniBasePage.PushMessageTemplate5Context.推送模版消息异常:" + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 推送模板消息
        /// </summary>
        /// <param name="application">application</param>
        /// <param name="touser">接收者OpenID</param>
        /// <param name="template_id">模板ID</param>
        /// <param name="url">链接地址</param>
        /// <param name="topcolor">标题颜色</param>
        /// <param name="data">数据内容</param>
        /// <returns></returns>
        public static PushMessageTemplateResult PushMessageTemplateContext(HttpApplicationState application, string touser, string template_id, string url, string topcolor, Dictionary<string, TemplateDataObject> data)
        {
            try
            {
                TemplateDataObject first = null;
                TemplateDataObject keyword1 = null;
                TemplateDataObject keyword2 = null;
                TemplateDataObject keyword3 = null;
                TemplateDataObject keyword4 = null;
                TemplateDataObject keyword5 = null;
                TemplateDataObject remark = null;
                if (data.Keys.Contains("first") && data["first"] != null)
                {
                    first = data["first"];
                }
                if (data.Keys.Contains("keyword1") && data["keyword1"] != null)
                {
                    keyword1 = data["keyword1"];
                }
                if (data.Keys.Contains("keyword2") && data["keyword2"] != null)
                {
                    keyword2 = data["keyword2"];
                }
                if (data.Keys.Contains("keyword3") && data["keyword3"] != null)
                {
                    keyword3 = data["keyword3"];
                }
                if (data.Keys.Contains("keyword4") && data["keyword4"] != null)
                {
                    keyword4 = data["keyword4"];
                }
                if (data.Keys.Contains("keyword5") && data["keyword5"] != null)
                {
                    keyword5 = data["keyword5"];
                }
                if (data.Keys.Contains("remark") && data["remark"] != null)
                {
                    remark = data["remark"];
                }
                return PushMessageTemplateContext(application, touser, template_id, url, topcolor, first, keyword1, keyword2, keyword3, keyword4, keyword5, remark);
            }
            catch (Exception ex)
            {
                Log.Debug("WeiXinMiniBasePage.PushMessageTemplateContext.推送模版消息异常:" + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 推送模板消息
        /// </summary>
        /// <param name="application">application</param>
        /// <param name="touser">接收者OpenID</param>
        /// <param name="template_id">模板ID</param>
        /// <param name="url">链接地址</param>
        /// <param name="topcolor">标题颜色</param>
        /// <param name="data">数据内容</param>
        /// <returns></returns>
        public static PushMessageTemplateResult PushMessageTemplateContext(HttpApplicationState application, string WeiXinAccount, string TemplateId, string url, string topcolor, TemplateDataObject first, TemplateDataObject keyword1, TemplateDataObject keyword2, TemplateDataObject keyword3, TemplateDataObject keyword4, TemplateDataObject keyword5, TemplateDataObject remark)
        {
            try
            {
                TemplateIdObject5 to = new TemplateIdObject5();
                if (first != null)
                {
                    to.first = first;
                }
                if (keyword1 != null)
                {
                    to.keyword1 = keyword1;
                }
                if (keyword2 != null)
                {
                    to.keyword2 = keyword2;
                }
                if (keyword3 != null)
                {
                    to.keyword3 = keyword3;
                }
                if (keyword4 != null)
                {
                    to.keyword4 = keyword4;
                }
                if (keyword5 != null)
                {
                    to.keyword5 = keyword5;
                }
                if (remark != null)
                {
                    to.remark = remark;
                }

                PushMessageTemplateResult pmtr = PushMessageTemplate5Context(HttpContext.Current.Application, WeiXinAccount, TemplateId, url, topcolor, to);
                return pmtr;
            }
            catch (Exception ex)
            {
                Log.Debug("PushMessageTemplateContext.消息推送异常:" + ex.ToString());
                return null;
            }
        }

        #region 微信消息推送
        public static void PushWeiXinMessageAction(string openid, string templateid, string color, string url, Dictionary<string, TemplateDataObject> data)
        {
            if (ConfigHelper.GetConfigString("PushMessageFlag") == "1")
            {
                string tid = (templateid != null && templateid.Trim() != "") ? templateid : "r0zTjCUo_93wlQPydX2mwpXEak8UVcwO-PsTzdxLqjI";
                string c = (color != null && color.Trim() != "") ? color : "#336699";
                string u = "";
                if (url != null && url.Trim() != "" )
                {
                    if (url.Trim().IndexOf("http:") < 0)
                    {
                        u = @"https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + ConfigHelper.GetConfigString("appid") + "&redirect_uri=" + HttpUtility.UrlEncode("http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/" + url) + "&response_type=code&scope=snsapi_base&state=STATE#wechat_redirect";
                        ZhiFang.Common.Log.Log.Debug("WeiXinMiniBasePage.PushWeiXinMessageAction.url=" + url + ";u=" + u);
                        //u="https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxb10e2abd68b58220redirect_uri=http%3A%2F%2Fdemo.zhifang.com.cn%2FZhiFang.OA%2FWeiXin%2FWeiXinMainRouter.aspx%3Foperate%3DIndexresponse_type=codescope=snsapi_basestate=STATE#wechat_redirect"
                        //u = url;
                    }
                    else
                    {
                        u = url;
                    }
                }
                else
                {
                    u = "";
                }
                
                PushMessageTemplateContext(HttpContext.Current.Application, openid, tid, u, c, data);
            }
        }
        #endregion

        public static PushMessageTemplateResult PushMessageTemplateAction(HttpApplicationState application, string touser, string template_id, string url, string topcolor, string datastr, out string msg)
        {
            msg = "";
            string jsonstr = "{\"touser\":\"" + touser + "\",\"template_id\":\"" + template_id + "\",\"url\":\"" + url + "\",\"topcolor\":\"" + topcolor + "\",\"data\": " + datastr + "}";
            string strresult = ZhiFang.LIIP.Common.RestfullHelper.InvkerRestServicePost(jsonstr, "JSON", PushMessageTemplateContextUrl + "?access_token=" + application["token"].ToString(), 10);
            try
            {
                PushMessageTemplateResult PushMessageTemplateResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<PushMessageTemplateResult>(strresult);
                if (PushMessageTemplateResult.errcode == 0)
                {
                    Log.Debug("WeiXinMiniBasePage.PushMessageTemplateAction.推送模版消息成功！: {\"PushMessageTemplateResult.msgid\":\"" + PushMessageTemplateResult.msgid + "\"}");
                    return PushMessageTemplateResult;
                }
                else
                {
                    if (GetErrorInfoDicList().Keys.Contains(PushMessageTemplateResult.errcode.ToString().Trim()))
                    {
                        Log.Debug("WeiXinMiniBasePage.PushMessageTemplateAction.推送模版消息 失败！: {\"PushMessageTemplateResult.msgid\":\"" + PushMessageTemplateResult.msgid + "\",\"PushMessageTemplateResult.errmsg\":\"" + PushMessageTemplateResult.errmsg + "\",\"PushMessageTemplateResult.errcode\":\"" + PushMessageTemplateResult.errcode + "\"}" + GetErrorInfoDicList()[PushMessageTemplateResult.errcode.ToString()].ToString());
                    }
                    else
                    {
                        Log.Debug("WeiXinMiniBasePage.PushMessageTemplateAction.推送模版消息 失败！: {\"PushMessageTemplateResult.msgid\":\"" + PushMessageTemplateResult.msgid + "\",\"PushMessageTemplateResult.errmsg\":\"" + PushMessageTemplateResult.errmsg + "\",\"PushMessageTemplateResult.errcode\":\"" + PushMessageTemplateResult.errcode + "\"}");
                    }
                    return PushMessageTemplateResult;
                }

            }
            catch (Exception e)
            {
                msg = e.Message;
                Log.Debug("WeiXinMiniBasePage.PushMessageTemplateAction.推送模版消息异常: " + e.ToString());
                return null;
            }
        }
        public static Dictionary<string, string> ErrorInfoDic ;
        public static Dictionary<string, string> GetErrorInfoDicList()
        {

            if (ErrorInfoDic==null)
            {
                ErrorInfoDic = new Dictionary<string, string>();
                ErrorInfoDic.Add("-1", "系统繁忙");
                ErrorInfoDic.Add("0", "请求成功");
                ErrorInfoDic.Add("40001", "验证失败");
                ErrorInfoDic.Add("40002", "不合法的凭证类型");
                ErrorInfoDic.Add("40003", "不合法的OpenID");
                ErrorInfoDic.Add("40004", "不合法的媒体文件类型");
                ErrorInfoDic.Add("40005", "不合法的文件类型");
                ErrorInfoDic.Add("40006", "不合法的文件大小");
                ErrorInfoDic.Add("40007", "不合法的媒体文件id");
                ErrorInfoDic.Add("40008", "不合法的消息类型");
                ErrorInfoDic.Add("40009", "不合法的图片文件大小");
                ErrorInfoDic.Add("40010", "不合法的语音文件大小");
                ErrorInfoDic.Add("40011", "不合法的视频文件大小");
                ErrorInfoDic.Add("40012", "不合法的缩略图文件大小");
                ErrorInfoDic.Add("40013", "不合法的APPID");
                ErrorInfoDic.Add("41001", "缺少access_token参数");
                ErrorInfoDic.Add("41002", "缺少appid参数");
                ErrorInfoDic.Add("41003", "缺少refresh_token参数");
                ErrorInfoDic.Add("41004", "缺少secret参数");
                ErrorInfoDic.Add("41005", "缺少多媒体文件数据");
                ErrorInfoDic.Add("41006", "access_token超时");
                ErrorInfoDic.Add("42001", "需要GET请求");
                ErrorInfoDic.Add("43002", "需要POST请求");
                ErrorInfoDic.Add("43003", "需要HTTPS请求");
                ErrorInfoDic.Add("44001", "多媒体文件为空");
                ErrorInfoDic.Add("44002", "POST的数据包为空");
                ErrorInfoDic.Add("44003", "图文消息内容为空");
                ErrorInfoDic.Add("45001", "多媒体文件大小超过限制");
                ErrorInfoDic.Add("45002", "消息内容超过限制");
                ErrorInfoDic.Add("45003", "标题字段超过限制");
                ErrorInfoDic.Add("45004", "描述字段超过限制");
                ErrorInfoDic.Add("45005", "链接字段超过限制");
                ErrorInfoDic.Add("45006", "图片链接字段超过限制");
                ErrorInfoDic.Add("45007", "语音播放时间超过限制");
                ErrorInfoDic.Add("45008", "图文消息超过限制");
                ErrorInfoDic.Add("45009", "接口调用超过限制");
                ErrorInfoDic.Add("45001", "不存在媒体数据");
                ErrorInfoDic.Add("47001", "解析JSON/XML内容错误");
            }
            return ErrorInfoDic;
        }

        #region 暂时不用
        /*
         public static int GetJSAPITokenP(HttpApplicationState application)
        {
            try
            {
                GetTokenP(application, appid, appsecret);
                if (application.Get("jsapi_tickettime") == null || Convert.ToDateTime(application.Get("jsapi_tickettime").ToString()) <= DateTime.Now)
                {
                    Log.Debug("WeiXinMiniBasePage.GetJSAPITokenP.获取JSAPIToken");
                    string apiurl = GetJSAPITokenUrl + "?access_token=" + application.Get("token").ToString().Trim() + "&type=jsapi";
                    Log.Debug("WeiXinMiniBasePage.GetJSAPITokenP.获取JSAPIToken:" + apiurl);
                    string TokenResult = ZhiFang.LIIP.Common.RestfullHelper.InvkerRestServiceByGet(apiurl).ToString();
                    Log.Debug("WeiXinMiniBasePage.GetJSAPITokenP.获取JSAPIToken.TokenResult:" + TokenResult);
                    JsonSerializerSettings jss = new JsonSerializerSettings();
                    jss.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    JSAPITokenResult tr = (JSAPITokenResult)JsonConvert.DeserializeObject(TokenResult, typeof(JSAPITokenResult), jss);
                    if (tr.errcode == 0)
                    {
                        Log.Debug("WeiXinMiniBasePage.GetJSAPITokenP.JSAPIToken=" + tr.ticket + ";jsapi_tickettime=" + DateTime.Now.AddHours(2).ToString());
                        application["jsapi_ticket"] = tr.ticket;
                        application["jsapi_tickettime"] = DateTime.Now.AddMinutes(100).ToString();
                        return Convert.ToInt32((DateTime.Now.AddMinutes(100) - DateTime.Now).TotalSeconds);
                    }
                    else
                    {
                        Log.Debug("WeiXinMiniBasePage.GetJSAPITokenP.GetJSAPIToken异常errmsg:" + tr.errmsg);
                        return 0;
                    }
                }
                else
                {
                    return Convert.ToInt32((Convert.ToDateTime(application["jsapi_tickettime"].ToString()) - DateTime.Now).TotalSeconds);
                }
            }
            catch (Exception ex)
            {
                Log.Debug("WeiXinMiniBasePage.GetJSAPITokenP.GetJSAPIToken异常:" + ex.ToString(), ex);
                return -1;
            }
        }
        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        public static string GetSignature(HttpApplicationState application, string noncestr, string timestamp, string url, out int expires_in)
        {
            try
            {
                expires_in = GetJSAPITokenP(application);
                if (expires_in > 0)
                {
                    string jsapi_ticketh = "jsapi_ticket=" + application["jsapi_ticket"].ToString();
                    string noncestrh = "noncestr=" + noncestr;
                    string timestamph = "timestamp=" + timestamp;
                    string urlh = "url=" + url;
                    string[] ArrTmp = { jsapi_ticketh, noncestrh, timestamph, urlh };
                    Array.Sort(ArrTmp);     //字典排序
                    string tmpStr = string.Join("&", ArrTmp);
                    Log.Debug("WeiXinMiniBasePage.GetSignature.HashPasswordForStoringInConfigFile:" + tmpStr);
                    tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
                    Log.Debug("WeiXinMiniBasePage.GetSignature.Signature:" + tmpStr);
                    return tmpStr;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                Log.Debug("WeiXinMiniBasePage.GetSignature.异常:" + ex.ToString());
                expires_in = -1;
                return null;
            }
        }
        public OpenIdInfoResult GetOpenIdInfo(string openid)
        {
            this.GetToken();
            Log.Debug("WeiXinMiniBasePage.GetOpenIdInfo.获取GetOpenIdInfo");

            string apiurl = GetOpenIdUrl + "?access_token=" + Application["token"].ToString() + "&openid=" + openid + "&lang=zh_CN";
            Log.Debug("WeiXinMiniBasePage.GetOpenIdInfo.获取GetOpenIdInfo:" + apiurl);

            string OpenIdListResult = ZhiFang.LIIP.Common.RestfullHelper.InvkerRestServiceByGet(apiurl).ToString();
            Log.Debug("WeiXinMiniBasePage.GetOpenIdInfo.获取GetOpenIdInfo.OpenIdListResult:" + OpenIdListResult);

            JsonSerializerSettings jss = new JsonSerializerSettings();
            jss.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            OpenIdInfoResult openidinfo = (OpenIdInfoResult)JsonConvert.DeserializeObject(OpenIdListResult, typeof(OpenIdInfoResult), jss);
            if (openidinfo.errcode == 0)
            {
                return openidinfo;
            }
            return null;
        }
        public UserAuthorizeToken GetUserAuthorizeToken(string code)
        {
            try
            {
                Log.Debug("WeiXinMiniBasePage.GetUserAuthorizeToken.开始！");
                string url = GetUserAuthorizeTokenUrl + "?appid=" + appid + "&secret=" + appsecret + "&code=" + code + "&grant_type=authorization_code";
                Log.Debug("WeiXinMiniBasePage.GetUserAuthorizeToken.Url:" + url);
                string str_userauthorizetoken = ZhiFang.LIIP.Common.RestfullHelper.InvkerRestServiceByGet(url).ToString();
                Log.Debug("WeiXinMiniBasePage.GetUserAuthorizeToken.str_userauthorizetoken:" + str_userauthorizetoken);
                JsonSerializerSettings jss = new JsonSerializerSettings();
                jss.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                UserAuthorizeToken userauthorizetoken = (UserAuthorizeToken)JsonConvert.DeserializeObject(str_userauthorizetoken, typeof(UserAuthorizeToken), jss);
                if (userauthorizetoken.errcode == 0)
                {
                    return userauthorizetoken;
                }
                else
                {
                    Log.Debug("WeiXinMiniBasePage.GetUserAuthorizeToken.errmsg:" + userauthorizetoken.errmsg + "@@@errcode:" + userauthorizetoken.errcode);
                }
                return null;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("WeiXinMiniBasePage.GetUserAuthorizeToken.异常："+ex.ToString());
                return null;
            }
        }

        public OpenIdListResult GetOpenIdList(string next_openid)
        {
            this.GetToken();
            string OpenIdListResult = ZhiFang.LIIP.Common.RestfullHelper.InvkerRestServiceByGet(GetOpenIdListUrl + "?access_token=" + Application["token"].ToString() + "&next_openid=" + next_openid).ToString();
            JsonSerializerSettings jss = new JsonSerializerSettings();
            jss.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            OpenIdListResult openidlist = (OpenIdListResult)JsonConvert.DeserializeObject(OpenIdListResult, typeof(OpenIdListResult), jss);
            if (openidlist.errcode == 0)
            {
                return openidlist;
            }
            return null;
        }
        /// <summary>
        /// 文本消息推送
        /// </summary>
        /// <param name="application">application</param>
        /// <param name="OpenId">OpenId</param>
        /// <param name="text">text</param>
        /// <returns>推送结果</returns>
        public static bool PushTextToWeiXinOpenId(HttpApplicationState application, string OpenId, string text)
        {
            try
            {
                GetTokenP(application, appid, appsecret);
                string msg;
                WCF_WebClient wc = new WCF_WebClient();
                wc.PostData(PushMsgUrl + "?access_token=" + application["token"].ToString(), "{\"touser\":\"" + OpenId + "\",\"msgtype\":\"text\",\"text\":{\"content\":\"" + text + "\"}}", "UTF-8", "UTF-8", out msg);
                if (msg == string.Empty)
                {
                    Log.Debug("消息推送成功！:{\"touser\":\"" + OpenId + "\",\"msgtype\":\"text\",\"text\":{\"content\":\"" + text + "\"}}");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Debug("消息推送异常:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 设置行业
        /// </summary>
        /// <param name="application">application</param>
        /// <param name="industry_id1">行业1</param>
        /// <param name="industry_id2">行业2</param>
        /// <returns></returns>
        public static bool SetBusinessUrlId(HttpApplicationState application, string industry_id1, string industry_id2)
        {
            try
            {
                GetTokenP(application, appid, appsecret);
                string msg;
                WCF_WebClient wc = new WCF_WebClient();
                wc.PostData(SetBusinessUrl + "?access_token=" + application["token"].ToString(), "{\"industry_id1\":\"" + industry_id1 + "\",\"industry_id2\":\"" + industry_id2 + "\"}", "UTF-8", "UTF-8", out msg);
                if (msg == string.Empty)
                {
                    Log.Debug("设置行业成功！: {\"industry_id1\":\"" + industry_id1 + "\",\"industry_id2\":\"" + industry_id2 + "\"}");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Debug("设置行业异常:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 获取消息模版ID
        /// </summary>
        /// <param name="application">application</param>
        /// <param name="template_id_short">消息模版代码</param>
        /// <returns></returns>
        public static GetTemplateIdResult GetBusinessTemplateId(HttpApplicationState application, string template_id_short)
        {
            try
            {
                GetTokenP(application, appid, appsecret);
                string msg;
                WCF_WebClient wc = new WCF_WebClient();
                string strresult = wc.PostData(GetBusinessTemplateIdUrl + "?access_token=" + application["token"].ToString(), "{\"template_id_short\":\"" + template_id_short + "\"}", "UTF-8", "UTF-8", out msg);
                if (msg == string.Empty)
                {
                    JsonSerializerSettings jss = new JsonSerializerSettings();
                    jss.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    GetTemplateIdResult BusinessTemplateId = (GetTemplateIdResult)JsonConvert.DeserializeObject(strresult, typeof(GetTemplateIdResult), jss);
                    if (BusinessTemplateId.errcode == 0)
                    {
                        Log.Debug("设置消息模版成功！: {\"template_id_short\":\"" + template_id_short + "\"}");
                        return BusinessTemplateId;
                    }
                    Log.Debug("设置消息模版错误！errmsg: " + BusinessTemplateId.errmsg);
                    return null;
                }
                else
                {
                    Log.Debug("设置消息模版错误！msg: " + msg);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log.Debug("设置消息模版异常:" + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 推送模板消息1
        /// </summary>
        /// <param name="application">application</param>
        /// <param name="touser">接收者OpenID</param>
        /// <param name="template_id">模板ID</param>
        /// <param name="url">链接地址</param>
        /// <param name="topcolor">标题颜色</param>
        /// <param name="data">数据内容</param>
        /// <returns></returns>
        public static PushMessageTemplateResult PushMessageTemplate1Context(HttpApplicationState application, string touser, string template_id, string url, string topcolor, TemplateIdObject1 data)
        {
            try
            {
                GetTokenP(application, appid, appsecret);
                string msg;
                string datastr = "";
                if (data != null)
                {
                    datastr = ZhiFang.ProjectProgressMonitorManage.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(data);
                }
                //Log.Debug("@@@@@@@@@@@@@@@@datastr:" + datastr);
                return PushMessageTemplateAction(application, touser, template_id, url, topcolor, datastr, out msg);
            }
            catch (Exception ex)
            {
                Log.Debug("推送模版消息异常:" + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 推送模板消息2
        /// </summary>
        /// <param name="application">application</param>
        /// <param name="touser">接收者OpenID</param>
        /// <param name="template_id">模板ID</param>
        /// <param name="url">链接地址</param>
        /// <param name="topcolor">标题颜色</param>
        /// <param name="data">数据内容</param>
        /// <returns></returns>
        public static PushMessageTemplateResult PushMessageTemplate2Context(HttpApplicationState application, string touser, string template_id, string url, string topcolor, TemplateIdObject2 data)
        {
            try
            {
                GetTokenP(application, appid, appsecret);
                string msg;
                string datastr = "";
                if (data != null)
                {
                    datastr = ZhiFang.ProjectProgressMonitorManage.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(data);
                }
                //Log.Debug("@@@@@@@@@@@@@@@@datastr:" + datastr);
                return PushMessageTemplateAction(application, touser, template_id, url, topcolor, datastr, out msg);
            }
            catch (Exception ex)
            {
                Log.Debug("推送模版消息异常:" + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 推送模板消息3
        /// </summary>
        /// <param name="application">application</param>
        /// <param name="touser">接收者OpenID</param>
        /// <param name="template_id">模板ID</param>
        /// <param name="url">链接地址</param>
        /// <param name="topcolor">标题颜色</param>
        /// <param name="data">数据内容</param>
        /// <returns></returns>
        public static PushMessageTemplateResult PushMessageTemplate3Context(HttpApplicationState application, string touser, string template_id, string url, string topcolor, TemplateIdObject3 data)
        {
            try
            {
                GetTokenP(application, appid, appsecret);
                string msg;
                string datastr = "";
                if (data != null)
                {
                    datastr = ZhiFang.ProjectProgressMonitorManage.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(data);
                }
                //Log.Debug("@@@@@@@@@@@@@@@@datastr:" + datastr);
                return PushMessageTemplateAction(application, touser, template_id, url, topcolor, datastr, out msg);
            }
            catch (Exception ex)
            {
                Log.Debug("推送模版消息异常:" + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 推送模板消息4
        /// </summary>
        /// <param name="application">application</param>
        /// <param name="touser">接收者OpenID</param>
        /// <param name="template_id">模板ID</param>
        /// <param name="url">链接地址</param>
        /// <param name="topcolor">标题颜色</param>
        /// <param name="data">数据内容</param>
        /// <returns></returns>
        public static PushMessageTemplateResult PushMessageTemplate4Context(HttpApplicationState application, string touser, string template_id, string url, string topcolor, TemplateIdObject4 data)
        {
            try
            {
                GetTokenP(application, appid, appsecret);
                string msg;
                string datastr = "";
                if (data != null)
                {
                    datastr = ZhiFang.ProjectProgressMonitorManage.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(data);
                }
                //Log.Debug("@@@@@@@@@@@@@@@@datastr:" + datastr);
                return PushMessageTemplateAction(application, touser, template_id, url, topcolor, datastr, out msg);
            }
            catch (Exception ex)
            {
                Log.Debug("推送模版消息异常:" + ex.ToString());
                return null;
            }
        }
        public UserAuthorizeRefreshToken GetUserAuthorizeRefreshToken(string refresh_token)
        {
            try
            {
                Log.Debug("WeiXinMiniBasePage.GetUserAuthorizeRefreshToken.开始！");
                string url = GetUserAuthorizeTokenRefreshUrl + "?appid=" + appid + "&grant_type=refresh_token&refresh_token=" + refresh_token;
                Log.Debug("WeiXinMiniBasePage.GetUserAuthorizeRefreshToken.Url:" + url);
                string str_userauthorizerefreshtoken = ZhiFang.LIIP.Common.RestfullHelper.InvkerRestServiceByGet(url).ToString();
                Log.Debug("WeiXinMiniBasePage.GetUserAuthorizeRefreshToken.str_userauthorizerefreshtoken:" + str_userauthorizerefreshtoken);
                JsonSerializerSettings jss = new JsonSerializerSettings();
                jss.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                UserAuthorizeRefreshToken userauthorizerefreshtoken = (UserAuthorizeRefreshToken)JsonConvert.DeserializeObject(str_userauthorizerefreshtoken, typeof(UserAuthorizeRefreshToken), jss);
                if (userauthorizerefreshtoken.errcode == 0)
                {
                    return userauthorizerefreshtoken;
                }
                return null;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("WeiXinMiniBasePage.GetUserAuthorizeRefreshToken.异常："+ex.ToString());
                return null;
            }
        }
        
        /// <summary>  
        /// 下载保存多媒体文件,返回多媒体保存路径  
        /// </summary>  
        /// <param name="ACCESS_TOKEN"></param>  
        /// <param name="MEDIA_ID"></param>  
        /// <returns></returns>
        public static string GetMultimedia(HttpApplicationState application, string MEDIA_ID)
        {
            string file = string.Empty;
            string content = string.Empty;
            string strpath = string.Empty;
            string savepath = string.Empty;
            try
            {
                GetTokenP(application, appid, appsecret);
                string stUrl = GetMediaUrl + "?access_token=" + application["token"].ToString() + "&media_id=" + MEDIA_ID;
                ZhiFang.Common.Log.Log.Debug("GetMultimedia.stUrl=" + stUrl);
                savepath = System.AppDomain.CurrentDomain.BaseDirectory + "ServerMedia\\WorkLogImage\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\";
                file = savepath + MEDIA_ID + ".jpg";
                ZhiFang.Common.Log.Log.Debug("GetMultimedia.file=" + file);
                ZhiFang.Common.Public.FilesHelper.CheckAndCreatDir(savepath);
                string outmessage;
                if (wc.GetFile(stUrl, file, out outmessage))
                {
                    return file;
                }
                else
                {
                    Log.Debug("下载保存多媒体文件消息异常.outmessage:" + outmessage);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log.Debug("下载保存多媒体文件消息异常:" + ex.ToString());
                return null;
            }
        }

        /// <summary>  
        /// 获取素材列表  
        /// </summary>  
        /// <param name="ACCESS_TOKEN"></param>  
        /// <returns></returns>
        public static PermanentMediaList GetPermanentMediaList(HttpApplicationState application, PermanentMediaType type,int offset,int count)
        {            
            try
            {
                GetTokenP(application, appid, appsecret); 
                string stUrl = GetPermanentMediaListUrl + "?access_token=" + application["token"].ToString();
                ZhiFang.Common.Log.Log.Debug("GetPermanentMediaList.stUrl=" + stUrl);
                string msg;

                string PermanentMediaList = wc.PostData(stUrl, "{\"type\":\"" + type.ToString() + "\",\"offset\":\""+ offset + "\",\"count\":\"" + count + "\"}", "UTF-8", "UTF-8", out msg).ToString();
                //ZhiFang.Common.Log.Log.Debug("GetPermanentMediaList.PermanentMediaList=" + PermanentMediaList);
                JsonSerializerSettings jss = new JsonSerializerSettings();
                jss.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                PermanentMediaList permanentmedialist = (PermanentMediaList)JsonConvert.DeserializeObject(PermanentMediaList, typeof(PermanentMediaList), jss);
                if (permanentmedialist.errcode == 0)
                {
                    return permanentmedialist;
                }
                Log.Debug("GetPermanentMediaList.获取素材列表错误！errmsg: " + permanentmedialist.errmsg);
                return null;
            }
            catch (Exception ex)
            {
                Log.Debug("GetPermanentMediaList.获取素材列表错误:" + ex.ToString());
                return null;
            }
        }

        /// <summary>  
        /// 下载保存永久素材文件,返回保存路径  
        /// </summary>  
        /// <param name="ACCESS_TOKEN"></param>  
        /// <param name="MEDIA_ID"></param>  
        /// <returns></returns>
        public static string GetPermanentMediaFile(HttpApplicationState application, string MEDIA_ID)
        {
            string file = string.Empty;
            string content = string.Empty;
            string strpath = string.Empty;
            string savepath = string.Empty;
            try
            {
                GetTokenP(application, appid, appsecret);
                string stUrl = GetPermanentMediaFileUrl + "?access_token=" + application["token"].ToString();
                //ZhiFang.Common.Log.Log.Debug("GetPermanentMediaFile.stUrl=" + stUrl);
                savepath = SysPublicSet.FFileNews.ThumbServerPath;
                file = savepath + MEDIA_ID + ".jpg";
                //ZhiFang.Common.Log.Log.Debug("GetPermanentMediaFile.file=" + file);
                if (ZhiFang.Common.Public.FilesHelper.CheckDirFile(savepath, MEDIA_ID + ".jpg"))
                {
                    return file;
                }
                else
                {
                    wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    // Upload the input string using the HTTP 1.0 POST method.  
                    byte[] byteArray = System.Text.Encoding.ASCII.GetBytes("{\"media_id\":\""+ MEDIA_ID + "\"}");
                    // 此处返回的是一个文件  
                    byte[] byteResult = wc.UploadData(stUrl, "POST", byteArray);

                    //if (wc.PostData(.GetFile(stUrl, file, out outmessage))
                    if(FilesHelper.CreatDirFile(savepath, MEDIA_ID + ".jpg", byteResult))
                    {
                        return file;
                    }
                    else
                    {
                        Log.Debug("GetPermanentMediaFile.下载保存多媒体文件消息异常." );
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Debug("GetPermanentMediaFile.下载保存多媒体文件消息异常:" + ex.ToString());
                return null;
            }
        }
        */
        #endregion
    }

}