using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZhiFang.WeiXin.BusinessObject
{
    public class OpenIdListResult : ErrResult
    {
        public string next_openid { get; set; }
        public int total { get; set; }
        public int count { get; set; }
        public Openid data { get; set; }
        public override string ToString()
        {
            string dataopenid = "";
            if (this.data != null && this.data.openid.Length > 0)
            {
                for (int i = 0; i < this.data.openid.Length; i++)
                {
                    dataopenid += data.openid[i] + ";";
                }
            }
            return dataopenid;
        }
    }
    public class Openid
    {
        public string[] openid { get; set; }
    }
}