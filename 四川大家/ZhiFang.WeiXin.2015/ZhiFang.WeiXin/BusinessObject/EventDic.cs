using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZhiFang.WeiXin.BusinessObject
{
    public class EventDic
    {
        public static Dictionary<string ,List<string>> EventTypeDic()
        {
            Dictionary<string ,List<string>> EventType=new Dictionary<string,List<string>>();
            EventType.Add("view",new List<string>(){});
            EventType.Add("click", new List<string>() {"ExportReport","image","location","ProductList"});
            EventType.Add("location", new List<string>() { });
            EventType.Add("subscribe",new List<string>(){});
            EventType.Add("unsubscribe",new List<string>(){});
            EventType.Add("scancode_waitmsg", new List<string>() { "rselfmenu_0_0" });
            EventType.Add("scancode_push", new List<string>() { "rselfmenu_0_1" });
            EventType.Add("pic_sysphoto", new List<string>() { "rselfmenu_1_0" });
            EventType.Add("pic_photo_or_album", new List<string>() { "rselfmenu_1_1" });
            EventType.Add("pic_weixin", new List<string>() { "rselfmenu_1_2" });
            EventType.Add("location_select",new List<string>(){});
            return EventType;
        }
    }
}