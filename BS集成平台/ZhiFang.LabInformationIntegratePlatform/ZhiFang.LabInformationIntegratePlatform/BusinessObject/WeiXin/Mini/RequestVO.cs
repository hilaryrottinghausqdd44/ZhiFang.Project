using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ZhiFang.LIIP.WeiXin.Mini
{
    public class WeiXinEventDic
    {
        public static Dictionary<string, List<string>> EventTypeDic()
        {
            Dictionary<string, List<string>> EventType = new Dictionary<string, List<string>>();
            EventType.Add("view", new List<string>() { });
            EventType.Add("click", new List<string>() { "ExportReport", "image", "location", "ProductList" });
            EventType.Add("location", new List<string>() { });
            EventType.Add("subscribe", new List<string>() { });
            EventType.Add("unsubscribe", new List<string>() { });
            EventType.Add("scancode_waitmsg", new List<string>() { "rselfmenu_0_0" });
            EventType.Add("scancode_push", new List<string>() { "rselfmenu_0_1" });
            EventType.Add("pic_sysphoto", new List<string>() { "rselfmenu_1_0" });
            EventType.Add("pic_photo_or_album", new List<string>() { "rselfmenu_1_1" });
            EventType.Add("pic_weixin", new List<string>() { "rselfmenu_1_2" });
            EventType.Add("location_select", new List<string>() { });
            return EventType;
        }
    }
    [DataContract]
    public class WeiXinMiniClientUserInfo
    {
        [DataMember]
        public string errMsg { get; set; }
        [DataMember]
        public string rawData { get; set; }
        [DataMember]
        public UserInfo userInfo { get; set; }
        [DataMember]
        public string signature { get; set; }
        [DataMember]
        public string encryptedData { get; set; }
        [DataMember]
        public string iv { get; set; }
        [DataMember]
        public string Code { get; set; }
        //       {
        //"errMsg": "getUserInfo:ok",
        //"rawData": "{\"nickName\":\"大千世界\",\"gender\":1,\"language\":\"zh_CN\",\"city\":\"Ningbo\",\"province\":\"Zhejiang\",\"country\":\"China\",\"avatarUrl\":\"https://thirdwx.qlogo.cn/mmopen/vi_32/mnrI5Hicbe5SUCViaWZg91ibXdEY4RmxwPSOnVSVicflS5D5x1tZE0tia9cEnacrr7zKjaxfTqg1vDMUYhU1toFiaF6g/132\"}",
        //"userInfo": {
        //	"nickName": "大千世界",
        //	"gender": 1,
        //	"language": "zh_CN",
        //	"city": "Ningbo",
        //	"province": "Zhejiang",
        //	"country": "China",
        //	"avatarUrl": "https://thirdwx.qlogo.cn/mmopen/vi_32/mnrI5Hicbe5SUCViaWZg91ibXdEY4RmxwPSOnVSVicflS5D5x1tZE0tia9cEnacrr7zKjaxfTqg1vDMUYhU1toFiaF6g/132"
        //},
        //"signature": "f7cbf33c94871107961a1f8ed1f827162106e3c0",
        //"encryptedData": "KLVh4Npcdl6n2aHoHKb6hfGjOMc9tT2cYNIRF2awpv3zU2wqdIKB8yMBzyFpkphgkhy/73Yx82TQWBCZKeTX4PvX05BaHShayfH3C2SJaYxZNZUEZfmXt2K/+x0kPJMLish5NbnM8PtnWdPOqM6b7xLJCIeQAP8yqOx2M8JTMH8cmFqbtxxQl9vb+ye08BORqYhdbcCPwHV3YptJuIhf9GrXhRPq+H4aYVCjF6qQLv/UHPT9lGcLtEkSJ/D56F+HdYuXY2MguZljjS4TH2ENfolcvK2y1fMpEpGd001I6Nj8SIx6AtkhJEUtktlLWYkIVAu39+kMqb/88mpriN90OZJjp7za844018DXIK+VVwbrXRPV/DjF4lvGvAn09gy8bxrN5keAnjB/hC/iF1sVXq5uUuhaPQOkVSvhyMPq95ahIeASMxClctC9+DnrEKkLPu7fqe/vIqrkgy6elv9lsQXCKHMN8brJF9H3F2CIpZsp7b65pdhfI5Y1K9SV5+7v",
        //"iv": "uN/ljUY7+JoCfimy5arqAg=="
        //}
    }
    [DataContract]
    public class UserInfo
    {
        [DataMember]
        public int subscribe { get; set; }
        [DataMember]
        public string openid { get; set; }
        [DataMember]
        public string nickName { get; set; }
        [DataMember]
        public int gender { get; set; }
        [DataMember]
        public string language { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string province { get; set; }
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public string avatarUrl { get; set; }
        [DataMember]
        public long subscribe_time { get; set; }
        public override string ToString()
        {
            string dataopenid = "";
            string gender = "未知";
            if (this.gender == 1)
            {
                gender = "男";
            }
            if (this.gender == 2)
            {
                gender = "女";
            }
            dataopenid += "sex=" + gender + ";";
            dataopenid += "openid=" + openid + ";nickname=" + nickName + ";language=" + language + ";city=" + city + ";province=" + province + ";country=" + country + ";headimgurl=" + avatarUrl + ";subscribe_time=" + subscribe_time + ";";
            return dataopenid;
        }
    }
}