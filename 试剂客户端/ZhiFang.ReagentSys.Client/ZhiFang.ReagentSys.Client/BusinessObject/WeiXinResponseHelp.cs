using System;
using System.Collections.Generic;
using ZhiFang.Common.Log;
using System.Xml;
using System.IO;
using ZhiFang.IBLL.WeiXin;
using ZhiFang.ReagentSys.Client.Common;

namespace ZhiFang.ReagentSys.Client
{
    public class WeiXinResponseHelp : System.Web.UI.Page
    {
        #region 请求
        public static BasePage bp { get; set; }
        /// <summary>
        /// 请求_事件
        /// </summary>
        /// <param name="requestxml">请求数据</param>
        /// <param name="EventType">事件类型</param>
        /// <returns></returns>
        public static string RequestEvent(XmlElement requestxml, string EventType)
        {
            string resxml = "";
            string FromUserName = requestxml.SelectSingleNode("FromUserName").InnerText;
            string ToUserName = requestxml.SelectSingleNode("ToUserName").InnerText;
            string eventkey = "";
            switch (EventType.ToLower().Trim())
            {
                case "view":
                    eventkey = requestxml.SelectSingleNode("EventKey").InnerText;
                    Log.Info("eventtype:view;eventkey:" + eventkey);
                    break;
                case "click":
                    eventkey = requestxml.SelectSingleNode("EventKey").InnerText;
                    Log.Info("eventtype:click;eventkey:" + eventkey);
                    switch (eventkey.ToLower().Trim())
                    {
                        case "ExportReport":
                            resxml = ResponseImage(FromUserName, ToUserName, "XzQirUXIgU-zNu8RHdh2u4eAXepvaZ-nmhjZuQ4gokMDA864VHikU01qMZXavwAp");
                            break;
                        case "image":
                            resxml = ResponseImage(FromUserName, ToUserName, "XzQirUXIgU-zNu8RHdh2u4eAXepvaZ-nmhjZuQ4gokMDA864VHikU01qMZXavwAp");
                            break;
                        case "location":
                            resxml = ResponseLocation(FromUserName, ToUserName, requestxml); break;
                        case "ProductList":
                            Log.Debug("ProductList");
                            Dictionary<string, WeiXinNews> newslist = new Dictionary<string, WeiXinNews>();
                            resxml = ResponseNews(FromUserName, ToUserName, newslist);
                            break;
                        case "demo":
                            Log.Debug("demo");
                            resxml = ResponseText(FromUserName, ToUserName, "实验室用户请使用演示账号登录试用：用户名lab1，密码lab1；\r\n供应商用户请使用演示账号登录试用：用户名com1，密码com1；\r\n申请开通实验室账号，供应商账号请联系我们：010-82272494-8114");
                            break;
                        default: break;
                    }
                    break;
                case "subscribe":
                    try
                    {
                        eventkey = requestxml.SelectSingleNode("EventKey").InnerText;
                        Log.Info("eventtype:subscribe;eventkey:" + eventkey);
                        OpenIdInfoResult openidinforesult = bp.GetOpenIdInfo(requestxml.SelectSingleNode("FromUserName").InnerText);
                        resxml = ResponseText(requestxml.SelectSingleNode("FromUserName").InnerText, requestxml.SelectSingleNode("ToUserName").InnerText, ZhiFang.Common.Public.ConfigHelper.GetConfigString("WelcomeStr").Replace("XXX", openidinforesult.nickname));
                        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
                        var bwxa = context.GetObject("BBWeiXinAccount") as IBBWeiXinAccount;
                        ZhiFang.Entity.WeiXin.BWeiXinAccount BWeiXinAccount = new Entity.WeiXin.BWeiXinAccount();
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
                        //#region 保存头像
                        //string iconpath = AppDomain.CurrentDomain.BaseDirectory + "\\UserIcon\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
                        //string tempiconpath = AppDomain.CurrentDomain.BaseDirectory + "\\tempUserIcon\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
                        //string exstr = openidinforesult.headimgurl.Substring(openidinforesult.headimgurl.LastIndexOf('.'), (openidinforesult.headimgurl.Length - openidinforesult.headimgurl.LastIndexOf(".") - 1));
                        //FilesHelper.CheckAndCreatDir(iconpath);
                        //FilesHelper.CheckAndCreatDir(tempiconpath);
                        //string url = openidinforesult.headimgurl;
                        //string tempiconfile = tempiconpath + openidinforesult.openid + ".jpg";
                        //string iconfile = iconpath + openidinforesult.openid + ".jpg";
                        ////Log.Debug("exstr:"+exstr);
                        ////Log.Debug("openidinforesult.headimgurl:" + openidinforesult.headimgurl);
                        //WebClient mywebclient = new WebClient();
                        //mywebclient.DownloadFile(url, tempiconfile);
                        //ImageHelp.SmallPic(tempiconfile, iconfile, 128, 128);
                        //#endregion
                        if (!bwxa.CheckWeiXinAccountByOpenID(openidinforesult.openid))
                        {
                            //bwxa.Entity.Id = ZhiFang.WeiXin.Common.GUIDHelp.GetGUIDLong();
                            bwxa.Save();
                        }
                        else
                        {
                            bwxa.UpdateContent();
                        }
                    }
                    catch (Exception esubscribe)
                    {
                        Log.Debug("关注事件：" + esubscribe.ToString());
                    }
                    break;
                case "unsubscribe":
                    eventkey = requestxml.SelectSingleNode("EventKey").InnerText;
                    Log.Info("eventtype:unsubscribe;eventkey:" + eventkey);
                    break;
                case "scancode_waitmsg":
                    eventkey = requestxml.SelectSingleNode("EventKey").InnerText;
                    Log.Info("eventtype:scancode_waitmsg;eventkey:" + eventkey);
                    switch (eventkey.ToLower().Trim())
                    {
                        case "rselfmenu_0_0":
                            ResponseText(FromUserName, ToUserName, "二维码扫描结果：" + requestxml.SelectSingleNode("ScanCodeInfo/ScanResult").InnerText);
                            break;
                        default: break;
                    }
                    break;
                case "scancode_push":
                    eventkey = requestxml.SelectSingleNode("EventKey").InnerText;
                    Log.Info("eventtype:scancode_push;eventkey:" + eventkey);
                    break;
                case "pic_sysphoto":
                    eventkey = requestxml.SelectSingleNode("EventKey").InnerText;
                    Log.Info("eventtype:pic_sysphoto;eventkey:" + eventkey);
                    break;
                case "pic_photo_or_album":
                    eventkey = requestxml.SelectSingleNode("EventKey").InnerText;
                    Log.Info("eventtype:pic_photo_or_album;eventkey:" + eventkey);
                    break;
                case "pic_weixin":
                    eventkey = requestxml.SelectSingleNode("EventKey").InnerText;
                    Log.Info("eventtype:pic_weixin;eventkey:" + eventkey);
                    break;
                case "location_select":
                    Log.Info("eventtype:location_select;");
                    break;
                case "templatesendjobfinish":
                    Log.Info("eventtype:templatesendjobfinish;");
                    break;
                default: break;
            }
            return resxml;
        }

        public static string RequestVoice(RequestXMLObject requestxmlobject)
        {
            string resxml = "";
            switch (requestxmlobject.Recognition)
            {
                case "谷丙转氨酶":
                    resxml = "<xml><ToUserName><![CDATA[" + requestxmlobject.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestxmlobject.ToUserName + "]]></FromUserName><CreateTime>" + DateTimeHelp.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[谷丙转氨酶ALT　正常范围是　0-40 Iu/L ]]></Content><FuncFlag>0</FuncFlag></xml>";
                    break;
                case "肝功能检查":
                    resxml = "<xml><ToUserName><![CDATA[" + requestxmlobject.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestxmlobject.ToUserName + "]]></FromUserName><CreateTime>" + DateTimeHelp.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[肝功能检查项目通常包括：谷丙转氨酶、谷草转氨酶、总蛋白、白蛋白、球蛋白、白蛋白/球蛋白以及总胆红素的检查。]]></Content><FuncFlag>0</FuncFlag></xml>";
                    break;
                case "检验报告单":
                    resxml = "<xml><ToUserName><![CDATA[" + requestxmlobject.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestxmlobject.ToUserName + "]]></FromUserName><CreateTime>" + DateTimeHelp.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[image]]></MsgType><Image><MediaId><![CDATA[0TEURKZ7ZZ5zTsk6p_Eh3cBOaYHMzmeicBjiR91CvOiQgOD7HlU3qFtliA-GUASf]]></MediaId></Image></xml>";
                    break;
                case "坐标":
                    resxml = "<xml><ToUserName><![CDATA[" + requestxmlobject.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestxmlobject.ToUserName + "]]></FromUserName><CreateTime>" + DateTimeHelp.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[您当前的坐标是：纬度 39.961246,经度：116.379471]]></Content><FuncFlag>0</FuncFlag></xml>";
                    break;
                case "产品列表":
                    Log.Debug("ProductList");
                    int size = 10;
                    resxml = "<xml><ToUserName><![CDATA[" + requestxmlobject.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestxmlobject.ToUserName + "]]></FromUserName><CreateTime>" + DateTimeHelp.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[news]]></MsgType><Content><![CDATA[]]></Content><ArticleCount>" + size + "</ArticleCount><Articles>";
                    Log.Debug("判断存在目录");
                    if (Directory.Exists(@"E:\WebSite\demo.zhifang.com.cn\ZhiFang.WeiXin\Images\"))
                    {
                        Log.Info("存在目录");
                        string[] piclist = Directory.GetFiles(@"E:\WebSite\demo.zhifang.com.cn\ZhiFang.WeiXin\Images\");
                        foreach (var a in piclist)
                        {
                            Log.Debug(a.Substring(a.LastIndexOf('\\') + 1, a.Length - a.LastIndexOf('\\') - 1));
                            resxml += "<item><Title><![CDATA[智方新产品]]></Title><Description><![CDATA[智方新产品图标]]></Description><PicUrl><![CDATA[" + "http://demo.zhifang.com.cn/ZhiFang.WeiXin/Images/" + a.Substring(a.LastIndexOf('\\') + 1, a.Length - a.LastIndexOf('\\') - 1) + "]]></PicUrl><Url><![CDATA[http://demo.zhifang.com.cn]]></Url></item>";
                            Log.Debug(a);
                        }
                    }
                    resxml += "</Articles></xml>";
                    Log.Debug(resxml.ToString());
                    break;
                case "报告帮助":
                    resxml = "<xml><ToUserName><![CDATA[" + requestxmlobject.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestxmlobject.ToUserName + "]]></FromUserName><CreateTime>" + DateTimeHelp.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[voice]]></MsgType><Voice><MediaId><![CDATA[" + requestxmlobject.MediaId + "]]></MediaId></Voice><FuncFlag>0</FuncFlag></xml>";
                    Log.Debug(resxml.ToString());
                    break;
                case "视频图像":
                    resxml = "<xml><ToUserName><![CDATA[" + requestxmlobject.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestxmlobject.ToUserName + "]]></FromUserName><CreateTime>" + DateTimeHelp.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[video]]></MsgType><Video><MediaId><![CDATA[vSX6FwJn45Q-1IxvGTOzl0G86_82uVrVrrjZR8kJom4PyYImSWUS5b-ZiYZ6bZ7u]]></MediaId><Title><![CDATA[视屏图像]]></Title><Description><![CDATA[开会]]></Description></Video><FuncFlag>0</FuncFlag></xml>";
                    Log.Debug(resxml.ToString());
                    break;
                default:
                    resxml = "<xml><ToUserName><![CDATA[" + requestxmlobject.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestxmlobject.ToUserName + "]]></FromUserName><CreateTime>" + DateTimeHelp.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[您的语音输入：" + requestxmlobject.Recognition + "]]></Content><FuncFlag>0</FuncFlag></xml>";
                    break;
            }
            return resxml;
        }

        public static string RequestLocation(RequestXMLObject requestxmlobject)
        {
            string resxml = "";
            string city = GetMapInfo(requestxmlobject.Location_X, requestxmlobject.Location_Y);
            if (city == "0")
            {
                resxml = "<xml><ToUserName><![CDATA[" + requestxmlobject.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestxmlobject.ToUserName + "]]></FromUserName><CreateTime>" + DateTimeHelp.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[Sorry，没有找到" + city + " 的相关产品信息]]></Content><FuncFlag>0</FuncFlag></xml>";
            }
            else
            {
                resxml = "<xml><ToUserName><![CDATA[" + requestxmlobject.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestxmlobject.ToUserName + "]]></FromUserName><CreateTime>" + DateTimeHelp.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[您的所在城市： " + city + "]]></Content><FuncFlag>0</FuncFlag></xml>";
            }
            return resxml;
        }

        public static string RequestText(XmlElement requestxml)
        {
            try
            {
                //string resxml = "";
                string FromUserName = requestxml.SelectSingleNode("FromUserName").InnerText;
                string ToUserName = requestxml.SelectSingleNode("ToUserName").InnerText;
                string requestxmlobject = requestxml.SelectSingleNode("Content").InnerText;
                return ResponseText(FromUserName, ToUserName, "请输入“手机号:xxx”或“身份证号:xxx”进行绑定或直接输入“条码号:xxx”进行查询！");
            }
            catch (Exception ex)
            {
                Log.Debug(ex.ToString());
                return null;
            }
        }

        public static string RequestImage(RequestXMLObject requestxmlobject)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 响应

        public static string ResponseImage(string FromUserName, string ToUserName, string MediaId)
        {
            string resxml = "<xml><ToUserName><![CDATA[" + FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + ToUserName + "]]></FromUserName><CreateTime>" + DateTimeHelp.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[image]]></MsgType><Image><MediaId><![CDATA[" + MediaId + "]]></MediaId></Image></xml>";
            return resxml;
        }

        public static string ResponseNews(string FromUserName, string ToUserName, Dictionary<string, WeiXinNews> newslist)
        {
            if (newslist != null && newslist.Count > 0)
            {
                string resxml = "<xml><ToUserName><![CDATA[" + FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + ToUserName + "]]></FromUserName><CreateTime>" + DateTimeHelp.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[news]]></MsgType><Content><![CDATA[]]></Content><ArticleCount>" + newslist.Count + "</ArticleCount><Articles>";
                foreach (var n in newslist)
                {
                    resxml += "<item><Title><![CDATA[" + n.Key + "]]></Title><Description><![CDATA[" + n.Value.Description + "]]></Description><PicUrl><![CDATA[" + n.Value.PicUrl + "]]></PicUrl><Url><![CDATA[" + n.Value.Url + "]]></Url></item>";
                }
                resxml += "</Articles></xml>";
                return resxml;
            }
            else
            {
                return ResponseText(FromUserName, ToUserName, "未找到相关的新闻！");
            }
        }
        public static string ResponseNews(string FromUserName, string ToUserName, List<WeiXinNews> newslist)
        {
            ZhiFang.Common.Log.Log.Debug("ResponseNews.FromUserName:" + FromUserName);
            ZhiFang.Common.Log.Log.Debug("ResponseNews.ToUserName:" + ToUserName);
            if (newslist != null && newslist.Count > 0)
            {
                string resxml = "<xml><ToUserName><![CDATA[" + FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + ToUserName + "]]></FromUserName><CreateTime>" + DateTimeHelp.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[news]]></MsgType><Content><![CDATA[]]></Content><ArticleCount>" + newslist.Count + "</ArticleCount><Articles>";
                foreach (var n in newslist)
                {
                    resxml += "<item><Title><![CDATA[" + n.Title + "]]></Title><Description><![CDATA[" + n.Description + "]]></Description><PicUrl><![CDATA[" + n.PicUrl + "]]></PicUrl><Url><![CDATA[" + n.Url + "]]></Url></item>";
                    //resxml += "<item><Title><![CDATA[ n.Title ]]></Title><Description><![CDATA[ n.Description ]]></Description><PicUrl><![CDATA[ n.PicUrl ]]></PicUrl><Url><![CDATA[ n.Url ]]></Url></item>";
                }
                resxml += "</Articles></xml>";
                return resxml;
            }
            else
            {
                return ResponseText(FromUserName, ToUserName, "未找到相关的新闻！");
            }
        }

        public static string ResponseText(string FromUserName, string ToUserName, string msg)
        {
            string resxml = "<xml><ToUserName><![CDATA[" + FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + ToUserName + "]]></FromUserName><CreateTime>" + DateTimeHelp.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + msg + "]]></Content><FuncFlag>0</FuncFlag></xml>";
            return resxml;
        }

        public static string ResponseLocation(string FromUserName, string ToUserName, XmlElement requestxml)
        {
            string resxml = "";
            //string city = GetMapInfo(requestxmlobject.Location_X, requestxmlobject.Location_Y);
            //if (city == "0")
            //{
            //    resxml = "<xml><ToUserName><![CDATA[" + requestxmlobject.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestxmlobject.ToUserName + "]]></FromUserName><CreateTime>" + DateTimeHelp.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[Sorry，没有找到" + city + " 的相关产品信息]]></Content><FuncFlag>0</FuncFlag></xml>";
            //}
            //else
            //{
            resxml = ResponseText(FromUserName, ToUserName, "您的坐标：纬度 " + requestxml.SelectSingleNode("Location_X").InnerText + " ，经度：" + requestxml.SelectSingleNode("Location_Y").InnerText);
            //}
            return resxml;
        }

        //public static string ResponseHandle(RequestXMLObject requestxmlobject)
        //{
        //    string resxml = "";
        //    string defalutresponse = "请输入正确的指令！输入Help查看指令列表。";
        //    ZhiFang.Common.Log.Log.Debug("requestxmlobject.Content=" + requestxmlobject.Content);
        //    if (requestxmlobject.Content == null || requestxmlobject.Content.Trim() == "")
        //    {
        //        resxml = ResponseText(requestxmlobject.FromUserName, requestxmlobject.ToUserName, defalutresponse);
        //        return resxml;
        //    }
        //    string requestcontent = requestxmlobject.Content;
        //    if (requestcontent.Trim().ToUpper() == "HELP")
        //    {
        //        resxml = ResponseText(requestxmlobject.FromUserName, requestxmlobject.ToUserName, "行业新闻->HY:标题内容查询条件.\r\n智方新闻->ZF:标题内容查询条件.\r\n知识库->ZSK:标题内容查询条件.\r\n");
        //        return resxml;
        //    }
        //    string MainOrder = "";
        //    string SeachKey = "";
        //    requestcontent=requestcontent.Replace("：", ":");//处理中文
        //    if (requestcontent.IndexOf(":") > 1)
        //    {
        //        MainOrder = requestcontent.Split(':')[0];
        //        SeachKey = requestcontent.Split(':')[1];
        //    }
        //    else
        //    {
        //        resxml = ResponseText(requestxmlobject.FromUserName, requestxmlobject.ToUserName, defalutresponse);
        //        return resxml;
        //    }
        //    Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        //    var bffile = context.GetObject("BFFile") as IBFFile;
        //    var bwxa = context.GetObject("BBWeiXinAccount") as IBBWeiXinAccount;
        //    long WeiXinAccountId;
        //    bool LoginInputPasswordFlag;
        //    long? EmpId;
        //    if (bwxa.CheckWeiXinAccountByOpenID(requestxmlobject.FromUserName, out WeiXinAccountId, out LoginInputPasswordFlag, out EmpId))
        //    {
        //        string HQL = "";
        //        EntityList<FFile> ffilelist = new EntityList<FFile>();
        //        switch (MainOrder.ToUpper())
        //        {
        //            case "HY":
        //                //ZhiFang.Common.Log.Log.Debug("ResponseHandle.SeachKey:" + " ffile.Title like '%" + SeachKey + "%' ");
        //                ffilelist = bffile.SearchFFileReadingUserListByHQLAndEmployeeID(WeiXinNewsDictreeList.行业新闻.Id.ToString(), " ffile.IsUse=1 and ffile.Title like '%" + SeachKey + "%' ", true, 1, 7, EmpId.Value.ToString(), "");
        //                break;
        //            case "ZF":
        //                ffilelist = bffile.SearchFFileReadingUserListByHQLAndEmployeeID(WeiXinNewsDictreeList.智方新闻.Id.ToString(), "  ffile.IsUse=1 and ffile.Title like '%" + SeachKey + "%' ", true, 1, 7, EmpId.Value.ToString(), "");
        //                break;
        //            case "ZSK":
        //                ffilelist = bffile.SearchFFileReadingUserListByHQLAndEmployeeID(WeiXinNewsDictreeList.知识库.Id.ToString(), "  ffile.IsUse=1 and ffile.Title like '%" + SeachKey + "%' ", true, 1, 7, EmpId.Value.ToString(), ""); break;
        //            default: resxml = ResponseText(requestxmlobject.FromUserName, requestxmlobject.ToUserName, defalutresponse);
        //                return resxml;
        //                break;
        //        }
        //        resxml = ResponseNews(requestxmlobject.FromUserName, requestxmlobject.ToUserName, ConvertWeiXinNews(ffilelist.list.ToList(),WeiXinNewsDictreeList.GetDictreeList()[MainOrder.ToUpper()], SeachKey));
        //    }
        //    #region
        //    //if (requestxmlobject.Content.IndexOf("手机号") >= 0)
        //    //{
        //    //    resxml = "<xml><ToUserName><![CDATA[" + requestxmlobject.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestxmlobject.ToUserName + "]]></FromUserName><CreateTime>" + DateTimeHelp.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[image]]></MsgType><Image><MediaId><![CDATA[0TEURKZ7ZZ5zTsk6p_Eh3cBOaYHMzmeicBjiR91CvOiQgOD7HlU3qFtliA-GUASf]]></MediaId></Image></xml>";

        //    //}
        //    //else
        //    //{
        //    //    if (requestxmlobject.Content.IndexOf("身份证号") >= 0)
        //    //    {
        //    //        resxml = "<xml><ToUserName><![CDATA[" + requestxmlobject.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestxmlobject.ToUserName + "]]></FromUserName><CreateTime>" + DateTimeHelp.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[image]]></MsgType><Image><MediaId><![CDATA[_bKvHpHh81jXbZ_eO1A1_yUjiEAczMsJ24lTw-CERPJzAaY6RNcxxXyatms7SG5n]]></MediaId></Image></xml>";
        //    //    }
        //    //    else
        //    //    {
        //    //        if (requestxmlobject.Content.IndexOf("条码号") >= 0)
        //    //        {
        //    //            resxml = "<xml><ToUserName><![CDATA[" + requestxmlobject.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestxmlobject.ToUserName + "]]></FromUserName><CreateTime>" + DateTimeHelp.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[image]]></MsgType><Image><MediaId><![CDATA[QkU6k7AlQXc6FG8Ap99DcnLb2QIEwaA9pSLIgQhbcFvzU3cmiE2BTusVNGBeNz6z]]></MediaId></Image></xml>";
        //    //        }
        //    //        else
        //    //        {
        //    //            if (requestxmlobject.Content.IndexOf("产品列表") >= 0)
        //    //            {
        //    //                int size = 10;
        //    //                resxml = "<xml><ToUserName><![CDATA[" + requestxmlobject.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestxmlobject.ToUserName + "]]></FromUserName><CreateTime>" + DateTimeHelp.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[news]]></MsgType><Content><![CDATA[]]></Content><ArticleCount>" + size + "</ArticleCount><Articles>";
        //    //                Log.Debug("判断存在目录");
        //    //                if (Directory.Exists(@"E:\WebSite\demo.zhifang.com.cn\ZhiFang.WeiXin\Images\"))
        //    //                {
        //    //                    Log.Info("存在目录");
        //    //                    string[] piclist = Directory.GetFiles(@"E:\WebSite\demo.zhifang.com.cn\ZhiFang.WeiXin\Images\");
        //    //                    foreach (var a in piclist)
        //    //                    {
        //    //                        Log.Debug(a.Substring(a.LastIndexOf('\\') + 1, a.Length - a.LastIndexOf('\\') - 1));
        //    //                        resxml += "<item><Title><![CDATA[智方新产品]]></Title><Description><![CDATA[智方新产品图标]]></Description><PicUrl><![CDATA[" + "http://demo.zhifang.com.cn/ZhiFang.WeiXin/Images/" + a.Substring(a.LastIndexOf('\\') + 1, a.Length - a.LastIndexOf('\\') - 1) + "]]></PicUrl><Url><![CDATA[http://demo.zhifang.com.cn]]></Url></item>";
        //    //                        Log.Debug(a);
        //    //                    }
        //    //                }
        //    //                resxml += "</Articles></xml>"; ;
        //    //            }
        //    //            else
        //    //            {
        //    //                if (requestxmlobject.Content.IndexOf("视频图像") >= 0)
        //    //                {
        //    //                    resxml = "<xml><ToUserName><![CDATA[" + requestxmlobject.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestxmlobject.ToUserName + "]]></FromUserName><CreateTime>" + DateTimeHelp.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[video]]></MsgType><Video><MediaId><![CDATA[vSX6FwJn45Q-1IxvGTOzl0G86_82uVrVrrjZR8kJom4PyYImSWUS5b-ZiYZ6bZ7u]]></MediaId><Title><![CDATA[视屏图像]]></Title><Description><![CDATA[开会]]></Description></Video><FuncFlag>0</FuncFlag></xml>";
        //    //                    Log.Debug(resxml.ToString());
        //    //                }
        //    //                else
        //    //                {
        //    //                    resxml = "<xml><ToUserName><![CDATA[" + requestxmlobject.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestxmlobject.ToUserName + "]]></FromUserName><CreateTime>" + DateTimeHelp.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("WelcomeStrInput") + "]]></Content><FuncFlag>0</FuncFlag></xml>";
        //    //                }
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    //resxml = "<xml><ToUserName><![CDATA[oQsZuwiOK4KzXLizd5RgOecYTAiI]]></ToUserName><FromUserName><![CDATA[CDATA[gh_50183000621e]]></FromUserName><CreateTime>12345678</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[你好]]></Content></xml>";
        //    #endregion
        //    ZhiFang.Common.Log.Log.Debug("resxml=" + resxml);
        //    return resxml;

        //}
        #endregion

        private static string GetMapInfo(string p, string p_2)
        {
            throw new NotImplementedException();
        }

        //private static List<WeiXinNews> ConvertWeiXinNews(List<ZhiFang.Entity.ProjectProgressMonitorManage.FFile> ffilelist, WeiXinNewsDictree dictree,string searchkey)
        //{
        //    string tmpurlpath = HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/";
        //    List<WeiXinNews> weixinnewslist = new List<WeiXinNews>();
        //    if (ffilelist != null && ffilelist != null && ffilelist.Count > 0)
        //    {
        //        foreach (var ffile in ffilelist)
        //        {
        //            WeiXinNews wxn = new WeiXinNews();
        //            wxn.Title = ffile.WeiXinTitle;
        //            wxn.Description = ffile.WeiXinDigest;
        //            if (ffile.ThumbnailsPath != null && ffile.ThumbnailsPath.Trim() != "")
        //            {
        //                wxn.PicUrl = "http://" + tmpurlpath + ffile.ThumbnailsPath.Replace("\\\\", "/").Replace("\\", "/");
        //            }
        //            else
        //            {
        //                wxn.PicUrl = "http://" + tmpurlpath + "Images/LOGO.jpg";
        //            }
        //            //ZhiFang.Common.Log.Log.Debug("ThumbnailsPath:" + wxn.PicUrl);
        //            if (ffile.IsSyncWeiXin.HasValue && ffile.IsSyncWeiXin.Value)
        //            {
        //                wxn.Url = ffile.WeiXinUrl;
        //            }
        //            else
        //            {
        //                wxn.Url = "http://" + tmpurlpath + "WeiXin/" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("WeiXinVersion") + "_webapp/ui/News/info.html?id=" + ffile.Id;
        //            }
        //            weixinnewslist.Add(wxn);
        //        }
        //        #region 添加更多
        //        WeiXinNews wxntmp = new WeiXinNews();
        //        wxntmp.Title = "点击查看更多";
        //        wxntmp.Description = "点击查看更多";
        //        wxntmp.PicUrl = "http://" + tmpurlpath + "Images/LOGO.jpg";
        //        string url = @"https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + ConfigHelper.GetConfigString("appid") + "&redirect_uri=" + HttpUtility.UrlEncode("http://" + tmpurlpath + "WeiXin/WeiXinMainRouter.aspx?operate=NEWSLIST&dictreeid=" + dictree.Id + "&dictreename=" + dictree.Name + "&searchkey=" + searchkey) + "&response_type=code&scope=snsapi_base&state=STATE#wechat_redirect";
        //        ZhiFang.Common.Log.Log.Debug("ConvertWeiXinNews.tmpurl:" + url);
        //        wxntmp.Url = url;
        //            //"http://" + tmpurlpath + "WeiXin/" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("WeiXinVersion") + "_webapp/ui/News/list.html?dictreeid=" + dictree.Id + "&dictreename=" + dictree.Name+"&searchkey="+ searchkey;
        //        #endregion

        //        weixinnewslist.Add(wxntmp);
        //        return weixinnewslist;
        //    }
        //    return null;
        //}
    }
    public class WeiXinNews
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PicUrl { get; set; }
        public string Url { get; set; }
    }
    public class WeiXinNewsDictree
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
    }
    public static class WeiXinNewsDictreeList
    {
        public static WeiXinNewsDictree 行业新闻 = new WeiXinNewsDictree() { Id = 5592236043558159468, Name = "行业新闻", Key = "HY" };
        public static WeiXinNewsDictree 智方新闻 = new WeiXinNewsDictree() { Id = 4885070131071422767, Name = "智方新闻", Key = "ZF" };
        public static WeiXinNewsDictree 知识库 = new WeiXinNewsDictree() { Id = 5133187353604336821, Name = "知识库", Key = "ZSK" };
        public static Dictionary<string, WeiXinNewsDictree> GetDictreeList()
        {
            Dictionary<string, WeiXinNewsDictree> dic = new Dictionary<string, WeiXinNewsDictree>();
            dic.Add(WeiXinNewsDictreeList.行业新闻.Key, WeiXinNewsDictreeList.行业新闻);
            dic.Add(WeiXinNewsDictreeList.智方新闻.Key, WeiXinNewsDictreeList.智方新闻);
            dic.Add(WeiXinNewsDictreeList.知识库.Key, WeiXinNewsDictreeList.知识库);
            return dic;
        }
    }
}