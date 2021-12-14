using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.WeiXin.BusinessObject
{
    public class PayToUser
    {
        public static SortedDictionary<string,object> PayToUserWeiXin(string partner_trade_no, string OpenID,string re_user_name,float amount,string desc,string check_name= "FORCE_CHECK")
        {
            //企业付款-微信支付零钱
            PayData data = new PayData();
            if (partner_trade_no == null || partner_trade_no.Trim() == "")
            {
                partner_trade_no = PayBase.GenerateOutTradeNo();
            }
            data.SetValue("partner_trade_no", partner_trade_no);
            data.SetValue("openid", OpenID);
            data.SetValue("re_user_name", re_user_name);
            data.SetValue("amount", amount.ToString());
            data.SetValue("desc", desc);
            check_name = "NO_CHECK";
            data.SetValue("check_name", check_name);

            PayData result = PayBase.PayToUserWeiXin(data);
            if (result==null)
            {
                ZhiFang.Common.Log.Log.Error("PayToUser.PayToUserWeiXin response error!");
                throw new Exception("PayToUserWeiXin response error!");
            }
            
            return result.GetValues();
        }
    }
}
