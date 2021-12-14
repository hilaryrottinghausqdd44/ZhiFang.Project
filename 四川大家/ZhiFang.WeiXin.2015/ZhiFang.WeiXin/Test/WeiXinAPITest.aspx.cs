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

namespace ZhiFang.WeiXin
{
    public partial class WeiXinAPITest : System.Web.UI.Page
    {
        const string Token = "zhifangkeji";		//与微信平台那边填写的token一致
        protected void Page_Load(object sender, EventArgs e)
        {

            string postStr = "";
            if (Request.HttpMethod.ToLower() == "post")
            {
                Stream s = System.Web.HttpContext.Current.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                postStr = Encoding.UTF8.GetString(b);

                if (!string.IsNullOrEmpty(postStr))
                {
                    //封装请求类
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(postStr);
                    XmlElement rootElement = doc.DocumentElement;

                    XmlNode MsgType = rootElement.SelectSingleNode("MsgType");

                    WeiXinRequest requestXML = new WeiXinRequest();
                    requestXML.ToUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
                    requestXML.FromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
                    requestXML.CreateTime = rootElement.SelectSingleNode("CreateTime").InnerText;
                    requestXML.MsgType = MsgType.InnerText;

                    if (requestXML.MsgType == "text")
                    {
                        requestXML.Content = rootElement.SelectSingleNode("Content").InnerText;
                        requestXML.MsgId = rootElement.SelectSingleNode("MsgId").InnerText;
                    }
                    else if (requestXML.MsgType == "location")
                    {
                        requestXML.Location_X = rootElement.SelectSingleNode("Location_X").InnerText;
                        requestXML.Location_Y = rootElement.SelectSingleNode("Location_Y").InnerText;
                        requestXML.Scale = rootElement.SelectSingleNode("Scale").InnerText;
                        requestXML.Label = rootElement.SelectSingleNode("Label").InnerText;
                    }
                    else if (requestXML.MsgType == "image")
                    {
                        requestXML.PicUrl = rootElement.SelectSingleNode("PicUrl").InnerText;
                    }
                    else if (requestXML.MsgType == "event")
                    {
                        requestXML.Wxevent = rootElement.SelectSingleNode("Event").InnerText;
                        requestXML.EventKey = rootElement.SelectSingleNode("EventKey").InnerText;
                    }
                    WriteTxt("----------粉丝发送过来的消息，消息类型：" + requestXML.MsgType + "----------：" + postStr);
                    //回复消息
                    ResponseMsg(requestXML);
                }
            }
            else
            {
                Valid();
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
                    Response.Write(echoStr);
                    Response.End();
                }
            }
        }

        /// <summary>
        /// 回复消息(微信信息返回)
        /// </summary>
        /// <param name="weixinXML"></param>
        private void ResponseMsg(WeiXinRequest requestXML)
        {
            string resxml = "<xml><ToUserName><![CDATA[" + requestXML.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestXML.ToUserName + "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime>";
            try
            {
                if (requestXML.MsgType == "text")
                {
                    int count = 0;
                    if (requestXML.Content.Trim() == "单图")
                    {
                        count = 1;
                        resxml += "<MsgType><![CDATA[news]]></MsgType><ArticleCount>" + count + "</ArticleCount><Articles>";
                        resxml += "<item><Title><![CDATA[这里是标题]]></Title><Description><![CDATA[这里是描述，当是单图文的时候描述才会展示]]></Description><PicUrl><![CDATA[http://imgsrc.baidu.com/product_name/pic/item/32374836acaf2edd470a31508d1001e93801934a.jpg]]></PicUrl><Url><![CDATA[http://www.baidu.com]]></Url></item>";//URL是点击之后跳转去那里，这里跳转到百度
                        resxml += "</Articles><FuncFlag>0</FuncFlag></xml>";
                    }
                    else if (requestXML.Content.Trim() == "多图")
                    {
                        count = 10;
                        resxml += "<MsgType><![CDATA[news]]></MsgType><ArticleCount>" + count + "</ArticleCount><Articles>";
                        for (int i = 0; i < count; i++)
                        {
                            resxml += "<item><Title><![CDATA[这里是标题" + (i + 1) + "]]></Title><Description><![CDATA[这里是描述，当是单图文的时候描述才会展示]]></Description><PicUrl><![CDATA[http://imgsrc.baidu.com/product_name/pic/item/32374836acaf2edd470a31508d1001e93801934a.jpg]]></PicUrl><Url><![CDATA[http://www.baidu.com]]></Url></item>";//URL是点击之后跳转去那里，这里跳转到百度
                        }
                        resxml += "</Articles><FuncFlag>0</FuncFlag></xml>";
                    }
                    else
                    {
                        resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[你发过来的是文字类型消息\n哈哈]]></Content><FuncFlag>0</FuncFlag></xml>";
                    }
                }
                else if (requestXML.MsgType == "location")
                {
                    resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[你发过来的是地理消息\n哈哈]]></Content><FuncFlag>0</FuncFlag></xml>";
                }
                else if (requestXML.MsgType == "image")
                {
                    resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[你发一张图片过来我怎么识别啊，你以为我有眼睛啊\n哈哈]]></Content><FuncFlag>0</FuncFlag></xml>";
                }
                else if (requestXML.MsgType == "event")
                {
                    if (requestXML.Wxevent == "unsubscribe")
                    {
                        //取消关注

                    }
                    else
                    {
                        //新增关注
                        resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[谢谢你关注我。]]></Content><FuncFlag>0</FuncFlag></xml>";
                    }
                }
            }
            catch (Exception ex)
            {
                WriteTxt("异常：" + ex.Message);
            }
            WriteTxt("返回给粉丝的消息：" + resxml);
            Response.Write(resxml);
            Response.End();
        }



        /// <summary>
        /// unix时间转换为datetime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        private DateTime UnixTimeToTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// datetime转换为unixtime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// 记录bug，以便调试
        /// </summary>
        /// 
        /// <returns></returns>
        public bool WriteTxt(string str)
        {
            try
            {
                FileStream fs = new FileStream(Server.MapPath("/bugLog.txt"), FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                //开始写入
                sw.WriteLine(str);
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}

//微信请求类
/// <summary>
/// 微信请求类
/// </summary>
public class WeiXinRequest
{
    private string toUserName;
    /// <summary>
    /// 消息接收方微信号，一般为公众平台账号微信号
    /// </summary>
    public string ToUserName
    {
        get { return toUserName; }
        set { toUserName = value; }
    }

    private string fromUserName;
    /// <summary>
    /// 消息发送方微信号
    /// </summary>
    public string FromUserName
    {
        get { return fromUserName; }
        set { fromUserName = value; }
    }

    private string createTime;
    /// <summary>
    /// 创建时间
    /// </summary>
    public string CreateTime
    {
        get { return createTime; }
        set { createTime = value; }
    }

    private string msgType;
    /// <summary>
    /// 信息类型 地理位置:location,文本消息:text,消息类型:image，音频：voice，视频：video,取消关注：Action
    /// </summary>
    public string MsgType
    {
        get { return msgType; }
        set { msgType = value; }
    }

    private string content;
    /// <summary>
    /// 信息内容
    /// </summary>
    public string Content
    {
        get { return content; }
        set { content = value; }
    }

    private string msgId;
    /// <summary>
    /// 消息ID（文本）
    /// </summary>
    public string MsgId
    {
        get { return msgId; }
        set { msgId = value; }
    }

    private string wxevent;
    /// <summary>
    /// 取消关注时的Event节点
    /// </summary>
    public string Wxevent
    {
        get { return wxevent; }
        set { wxevent = value; }
    }

    private string eventKey;
    /// <summary>
    /// 取消关注时的EventKey节点
    /// </summary>
    public string EventKey
    {
        get { return eventKey; }
        set { eventKey = value; }
    }


    private string location_X;
    /// <summary>
    /// 地理位置纬度
    /// </summary>
    public string Location_X
    {
        get { return location_X; }
        set { location_X = value; }
    }

    private string location_Y;
    /// <summary>
    /// 地理位置经度
    /// </summary>
    public string Location_Y
    {
        get { return location_Y; }
        set { location_Y = value; }
    }

    private string scale;
    /// <summary>
    /// 地图缩放大小
    /// </summary>
    public string Scale
    {
        get { return scale; }
        set { scale = value; }
    }

    private string label;
    /// <summary>
    /// 地理位置信息
    /// </summary>
    public string Label
    {
        get { return label; }
        set { label = value; }
    }

    private string picUrl;
    /// <summary>
    /// 图片链接，开发者可以用HTTP GET获取
    /// </summary>
    public string PicUrl
    {
        get { return picUrl; }
        set { picUrl = value; }
    }
}