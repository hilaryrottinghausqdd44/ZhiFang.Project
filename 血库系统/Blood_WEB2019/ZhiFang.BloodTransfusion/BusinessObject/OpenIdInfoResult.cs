using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZhiFang.BloodTransfusion
{
    public class OpenIdInfoResult : ErrResult
    {
        public int subscribe { get; set; }
        public string openid { get; set; }
        public string nickname { get; set; }
        public int sex { get; set; }
        public string language { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        public long subscribe_time { get; set; }
        public override string ToString()
        {
            string dataopenid = "";
            string isguanzhu = "未订阅";
            if (Convert.ToBoolean(subscribe))
            {
                isguanzhu = "已订阅";
            }
            dataopenid += "subscribe=" + isguanzhu + ";";

            string gender = "未知";
            if (sex==1)
            {
                gender = "男";
            }
            if (sex == 2)
            {
                gender = "女";
            }
            dataopenid += "sex=" + gender + ";";
            dataopenid += "openid=" + openid + ";nickname=" + nickname + ";language=" + language + ";city=" + city + ";province=" + province + ";country=" + country + ";headimgurl=" + headimgurl + ";subscribe_time=" + subscribe_time + ";";
            return dataopenid;
        }
    }
}