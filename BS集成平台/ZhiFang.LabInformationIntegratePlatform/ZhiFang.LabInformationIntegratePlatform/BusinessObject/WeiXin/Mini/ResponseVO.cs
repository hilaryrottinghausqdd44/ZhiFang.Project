using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZhiFang.LIIP.WeiXin.Mini
{
    public class TokenResult : ErrResult
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }
    public class JSAPITokenResult : ErrResult
    {
        public string ticket { get; set; }
        public int expires_in { get; set; }
    }
    public class UserAuthorizeToken : ErrResult
    {
        /// <summary>
        /// 网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// access_token接口调用凭证超时时间，单位（秒）
        /// </summary>
        public int expires_in { get; set; }
        /// <summary>
        /// 用户刷新access_token
        /// </summary>
        public string refresh_token { get; set; }
        /// <summary>
        /// 用户唯一标识，请注意，在未关注公众号时，用户访问公众号的网页，也会产生一个用户和公众号唯一的OpenID
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 用户授权的作用域，使用逗号（,）分隔 
        /// </summary>
        public string scope { get; set; }
        public override string ToString()
        {
            string str = "access_token:" + access_token + ";expires_in:" + expires_in + ";refresh_token:" + refresh_token + ";openid:" + openid + ";scope:" + scope + ";";
            return str;
        }
    }
    public class UserAuthorizeRefreshToken : UserAuthorizeToken
    {

    }
    public class ErrResult
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string errmsg { get; set; }
    }

    public class GetTemplateIdResult : ErrResult
    {
        public string template_id { get; set; }
    }
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
            if (sex == 1)
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
    public class PermanentMediaList : ErrResult
    {
        public int total_count { get; set; }
        public int item_count { get; set; }
        public PermanentMedia[] item { get; set; }

    }
    public class PermanentMedia
    {
        public string media_id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public PermanentMediaNewsContentItem content { get; set; }
        public string update_time { get; set; }

    }
    public class PermanentMediaNewsContentItem
    {
        public PermanentMediaNewsContent[] news_item { get; set; }
    }
    public class PermanentMediaNewsContent
    {
        public string title { get; set; }
        public string thumb_media_id { get; set; }
        public string thumb_media_Url { get; set; }
        public string show_cover_pic { get; set; }
        public string author { get; set; }
        public string digest { get; set; }
        public string content { get; set; }
        public string url { get; set; }
        public string content_source_url { get; set; }
    }
    public class PushMessageTemplateResult : ErrResult
    {
        public long msgid { get; set; }
    }

    /// <summary>
    /// 登录凭证校验结果
    /// </summary>
    public class Auth_code2SessionResult : ErrResult
    {
        public string openid { get; set; }
        /// <summary>
        /// 会话密钥
        /// </summary>
        public string session_key { get; set; }
        /// <summary>
        /// 用户在开放平台的唯一标识符，在满足 UnionID 下发条件的情况下会返回，详见 UnionID 机制说明。
        /// </summary>
        public string unionid { get; set; } 
    }
}