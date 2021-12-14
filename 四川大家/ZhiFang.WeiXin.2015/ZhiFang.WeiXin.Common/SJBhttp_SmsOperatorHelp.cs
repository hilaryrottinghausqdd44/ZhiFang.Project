using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Common.Log;

namespace ZhiFang.WeiXin.Common
{
    public class SJBhttp_SmsOperatorHelp
    {
        private static SJBhttp.SmsOperator _instance = null;
        // Creates an syn object.
        private static readonly object SynObject = new object();

        SJBhttp_SmsOperatorHelp()
        {
        }

        public static SJBhttp.SmsOperator Instance
        {
            get
            {
                // Double-Checked Locking
                if (null == _instance)
                {
                    lock (SynObject)
                    {
                        if (null == _instance)
                        {
                            _instance = SJBhttp.SmsOperator.getInstance();
                        }
                    }
                }
                return _instance;
            }
        }

        public static bool SendMessage_Vaild(string MobileCode,out string VaildCode)
        {
            VaildCode=null;
            try
            {
                string[] phone = new string[101];
                phone[0] = MobileCode;
                //phone[1] = "13311057870";
                //phone[2] = "13520061160";
                if (ConfigHelper.GetConfigString("SJBhttpVaildModule") != string.Empty || ConfigHelper.GetConfigString("SJBhttpVaildModule").Trim().Split(',').Length>1)
                {
                    string[] SJBhttpVaildModule = ConfigHelper.GetConfigString("SJBhttpVaildModule").Trim().Split(',');
                    VaildCode = ZhiFang.WeiXin.Common.GUIDHelp.RandomInt(6);
                    SJBhttp.Res ret = Instance.sendMulSms(ConfigHelper.GetConfigString("SJBhttpReg"), ConfigHelper.GetConfigString("SJBhttpPWD"), null, SJBhttpVaildModule[0] + VaildCode + SJBhttpVaildModule[1], phone);

                    //SJBhttp.Res ret = Instance.sendMulSms("101100-SJB-ZFKJ-846810", "SMCDAFDG", null, "智方科技提醒您该吃饭了！", phone);
                    Log.Debug("服务器返回结果标示：" + ret.result + "\r\n");
                    Log.Debug("服务器返回结果描述：" + ret.des + "\r\n");
                    return true;
                }
                else
                {
                    Log.Debug("验证模版没有配置或配置错误！\r\n");
                    return false;
                }
            }
            catch (Exception e)
            {
                Log.Debug("发送手机消息异常：" + e.ToString() + "\r\n");
                return false;
            }
        }

        public static bool SendMessageTest()
        {
            try
            {
                string[] phone = new string[101];
                phone[0] = "13488652018";
                //phone[1] = "13311057870";
                //phone[2] = "13520061160";
                SJBhttp.Res ret = Instance.sendMulSms("101100-SJB-ZFKJ-846810", "SMCDAFDG", null, "智方科技提醒您该吃饭了！", phone);
                Log.Debug("服务器返回结果标示：" + ret.result + "\r\n");
                Log.Debug("服务器返回结果描述：" + ret.des + "\r\n");
                return true;
            }
            catch (Exception e)
            {
                Log.Debug("发送手机消息异常：" + e.ToString() + "\r\n");
                return false;
            }
        }
    }
}
