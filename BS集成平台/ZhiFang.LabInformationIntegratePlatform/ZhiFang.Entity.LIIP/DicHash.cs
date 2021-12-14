using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Policy;
using System.Collections;

namespace ZhiFang.Entity.LIIP
{
    public static class DicCookieSession
    {
        /// <summary>
        /// OpenID用于通知（微信生成）
        /// </summary>
        public static string WeiXinOpenID1 = "100001";//WeiXinOpenID
        /// <summary>
        /// 微信关注用户列表中的ID（GUID）
        /// </summary>
        public static string WeiXinUserID = "100002";//WeiXinAccountID
        public static string WeiXinAdminFlag = "101010";//WeiXinAccountID

        public static string WeiXinAdminFlagvalue = ZhiFang.Common.Public.SecurityHelp.MD5Encrypt(WeiXinAdminFlag, "ZFWEIXIN");//WeiXinAdminFlagvalue

        /// <summary>
        ///WeiXinMiniOpenID
        /// </summary>
        public static string WeiXinMiniOpenID = "200001";//WeiXinOpenID
        /// <summary>
        /// WeiXinMiniUserID
        /// </summary>
        public static string WeiXinMiniUserID = "200002";//WeiXinAccountID
        /// <summary>
        /// WeiXinMinisession_key,只能存在Session里
        /// </summary>
        public static string WeiXinMinisession_key = "200003";//session_key,只能存在Session里

        public static string WeiXinMiniAdminFlag = "201010";//WeiXinAccountID

        public static string IPAddress = "900001";
        #region 登录验证码
        public static string ValidateCode="900091";        
        public static string ValidateCodeDateTime = "900093";
        public static string MaxValidateErrorCountDateTime = "900094";
        public static string ValidateErrorCount = "900095";
        #endregion
        //public static List<string> keylist=new List<string> { WeiXinOpenID,WeiXinUserID,}
    }
} 