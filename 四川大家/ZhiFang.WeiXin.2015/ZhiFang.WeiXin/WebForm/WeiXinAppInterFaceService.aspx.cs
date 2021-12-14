using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Xml;
using System.Web.Security;
using System.Net;
using ZhiFang.Common.Log;
using ZhiFang.WeiXin.BusinessObject;

namespace ZhiFang.WeiXin
{
    public partial class WeiXinAppInterFaceService : BasePage
    {
        const string Token = "zhifangkeji";		//与那边填写的token一致
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string postStr = "";

                if (Request.HttpMethod.ToLower() == "post")
                {
                    //Log.Debug("post");
                    Stream s = System.Web.HttpContext.Current.Request.InputStream;
                    byte[] b = new byte[s.Length];
                    s.Read(b, 0, (int)s.Length);
                    postStr = Encoding.UTF8.GetString(b);
                    Log.Debug(postStr);
                    //Log.Debug("postStr");
                    if (!string.IsNullOrEmpty(postStr))
                    {
                        //封装请求类
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(postStr);
                        XmlElement rootElement = doc.DocumentElement;

                        XmlNode MsgType = rootElement.SelectSingleNode("MsgType");

                        RequestXMLObject requestxmlobject = new RequestXMLObject();
                        requestxmlobject.ToUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
                        requestxmlobject.FromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
                        requestxmlobject.CreateTime = rootElement.SelectSingleNode("CreateTime").InnerText;
                        requestxmlobject.MsgType = MsgType.InnerText;
                        WeiXinResponseHelp.bp=this;
                        try
                        {
                            string resxml = "";
                            switch (requestxmlobject.MsgType)
                            {
                                case "text":
                                    resxml = requestxmlobject.Content = rootElement.SelectSingleNode("Content").InnerText;
                                    resxml=WeiXinResponseHelp.ResponseText(requestxmlobject);
                                    break;
                                case "image":
                                    resxml = WeiXinResponseHelp.RequestImage(requestxmlobject);
                                    requestxmlobject.PicUrl = rootElement.SelectSingleNode("PicUrl").InnerText;
                                    break;
                                case "location":
                                    resxml = WeiXinResponseHelp.RequestLocation(requestxmlobject);
                                    requestxmlobject.Location_X = rootElement.SelectSingleNode("Location_X").InnerText;
                                    requestxmlobject.Location_Y = rootElement.SelectSingleNode("Location_Y").InnerText;
                                    requestxmlobject.Scale = rootElement.SelectSingleNode("Scale").InnerText;
                                    requestxmlobject.Label = rootElement.SelectSingleNode("Label").InnerText;
                                    break;
                                case "event":
                                    Log.Debug("Event:"+rootElement.SelectSingleNode("Event").InnerText);
                                    resxml = WeiXinResponseHelp.RequestEvent(rootElement, rootElement.SelectSingleNode("Event").InnerText);
                                    break;
                                case "voice":
                                    resxml = WeiXinResponseHelp.RequestVoice(requestxmlobject);
                                    //requestxmlobject.Content = rootElement.SelectSingleNode("Content").InnerText;
                                    requestxmlobject.MediaId = rootElement.SelectSingleNode("MediaId").InnerText;
                                    requestxmlobject.Recognition = rootElement.SelectSingleNode("Recognition").InnerText;
                                    break;
                                default: break;
                            }
                            Log.Debug(resxml);
                            Response.Write(resxml);
                        }
                        catch (Exception ex)
                        {
                            Log.Error("异常：" + ex.Message + "Struck:" + ex.StackTrace.ToString());
                        }
                    }
                }
                else
                {
                    Valid();
                }
            }
            catch (Exception ex)
            {
                Log.Debug(ex.ToString());
            }
        }

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        private bool CheckSignature()
        {
            string signature = Request.QueryString["signature"];
            string timestamp = Request.QueryString["timestamp"];
            string nonce = Request.QueryString["nonce"];
            string[] ArrTmp = { Token, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序
            string tmpStr = string.Join("", ArrTmp); 
            //tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Valid()
        {
            string echoStr = Request.QueryString["echoStr"];
            if (CheckSignature())
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    Response.Clear();
                    Response.Write(echoStr);
                    Response.End();
                }
            }
        }
    }

}
