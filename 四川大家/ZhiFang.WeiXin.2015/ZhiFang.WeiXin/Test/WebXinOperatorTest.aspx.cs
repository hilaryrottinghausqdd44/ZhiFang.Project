using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.WeiXin.BusinessObject;
using ZhiFang.Common.Log;
using System.Net;
using Newtonsoft.Json;
using ZhiFang.WeiXin.IBLL;

namespace ZhiFang.WeiXin
{
    public partial class WebXinOperatorTest : BasePage
    {
        //WCF_WebClient wc = new WCF_WebClient();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //////https://api.weixin.qq.com/cgi-bin/user/info?access_token=ACCESS_TOKEN&openid=OPENID&lang=zh_CN
            //    string LoginFlag = wc.GetData_WcfRest("https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + this.Token.InnerText + "&openid="+this.TextBox3.Text+"&lang=zh_CN").ToString();
            //UserInfo.InnerText = LoginFlag;
            string msg;
            string result=WeiXinHttpToTencentHelp.Post("{\"touser\":\"" + this.TextBox3.Text + "\",\"msgtype\":\"text\",\"text\":{\"content\":\"" + this.TextBox2.Text + "\"}}", "JSON", "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Application["token"].ToString(), false, 20, false);
            //wc.PostData("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Application["token"].ToString(), "{\"touser\":\"" + this.TextBox3.Text + "\",\"msgtype\":\"text\",\"text\":{\"content\":\"" + this.TextBox2.Text + "\"}}", "UTF-8", "UTF-8", out msg);
            SendResult.InnerText = result;


            OpenIdInfoResult openidinfo = this.GetOpenIdInfo(this.TextBox3.Text);
            UserInfo.InnerText = openidinfo.ToString();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            OpenIdInfoResult openidinfo = this.GetOpenIdInfo(this.TextBox3.Text);
            UserInfo.InnerText = openidinfo.ToString();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            OpenIdListResult openidlist = this.GetOpenIdList(null);
            UserList.InnerText = "count=" + openidlist.count + ";total=" + openidlist.total + ";data=" + openidlist.ToString();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            this.GetToken();
            this.Token.InnerText = Application["token"].ToString();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            string usernamelist = "";
            OpenIdListResult openidlist = this.GetOpenIdList(null);

            string[] userinfolist = openidlist.data.openid;

            for (int i = 0; i < userinfolist.Length; i++)
            {
                OpenIdInfoResult openidinfo = this.GetOpenIdInfo(userinfolist[i]);
                usernamelist += openidinfo.ToString() + "<br>";
            }
            UserNameList.InnerText = usernamelist;
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            base.GetToken();
            string mediaid = UploadMultimedia(Application["token"].ToString(), "image");
            Response.Write(mediaid);
            string msg;
            string result = WeiXinHttpToTencentHelp.Post("{\"touser\":\"" + this.TextBox3.Text + "\",\"msgtype\":\"image\",\"image\":{\"media_id\":\"" + mediaid + "\"}}", "JSON", "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Application["token"].ToString(), false, 20, false);

            //wc.PostData("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Application["token"].ToString(), "{\"touser\":\"" + this.TextBox3.Text + "\",\"msgtype\":\"image\",\"image\":{\"media_id\":\"" + mediaid + "\"}}", "UTF-8", "UTF-8", out msg);
            SendResult.InnerText = result;
        }


        /// <summary>  
        /// 上传多媒体文件,返回 MediaId  
        /// </summary>  
        /// <param name="ACCESS_TOKEN"></param>  
        /// <param name="Type"></param>  
        /// <returns></returns>  
        public string UploadMultimedia(string ACCESS_TOKEN, string Type)
        {
            string result = "";
            string wxurl = "http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token=" + ACCESS_TOKEN + "&type=" + Type;
            string filepath = Server.MapPath("ReportFormFiles") + "\\1.1.jpg"; //(本地服务器的地址)
            Log.Debug("上传路径:" + filepath);
            WebClient myWebClient = new WebClient();
            myWebClient.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                byte[] responseArray = myWebClient.UploadFile(wxurl, "POST", filepath);
                result = System.Text.Encoding.Default.GetString(responseArray, 0, responseArray.Length);
                Log.Debug("上传result:" + result);

                JsonSerializerSettings jss = new JsonSerializerSettings();
                jss.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                MediaUploadResult mediauploadresult = (MediaUploadResult)JsonConvert.DeserializeObject(result, typeof(MediaUploadResult), jss);
                if (mediauploadresult.errcode == 0)
                {
                    return mediauploadresult.media_id;
                }
            }
            catch (Exception ex)
            {
                result = "Error:" + ex.Message;
            }
            Log.Debug("上传MediaId:" + result);
            return result;
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
                var bwxa = context.GetObject("BBWeiXinAccount") as IBBWeiXinAccount;
                var userlist = bwxa.TakeAll();
                foreach (var u in userlist)
                {
                    OpenIdInfoResult openidinforesult = this.GetOpenIdInfo(u.WeiXinAccount);

                    ZhiFang.WeiXin.Entity.BWeiXinAccount BWeiXinAccount = new Entity.BWeiXinAccount();
                    BWeiXinAccount.UserName = openidinforesult.nickname;
                    BWeiXinAccount.WeiXinAccount = openidinforesult.openid;
                    BWeiXinAccount.SexID = openidinforesult.sex;
                    BWeiXinAccount.CountryName = openidinforesult.country;
                    BWeiXinAccount.ProvinceName = openidinforesult.province;
                    BWeiXinAccount.CityName = openidinforesult.city;
                    BWeiXinAccount.AddTime = DateTime.Now;
                    BWeiXinAccount.HeadImgUrl = openidinforesult.headimgurl;
                    if (openidinforesult.language != null)
                    {
                        BWeiXinAccount.Language = openidinforesult.language;
                    }
                    bwxa.Entity = BWeiXinAccount;
                    bwxa.UpdateContent();
                }

                //tempBaseResultBool.success = IBBWeiXinAccount.GetUserIcon();
            }
            catch (Exception ex)
            {
                Response.Write("错误信息：" + ex.Message);
                Log.Error("GetUserIcon异常：" + ex.Message + "GetUserIcon:" + ex.StackTrace.ToString());
            }
        }
    }
}