using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace ZhiFang.WeiXin.BusinessObject
{
    public class PayJsApi
    {
        /// <summary>
        /// 保存页面对象，因为要在类的方法中使用Page的Request对象
        /// </summary>
        private BasePage page { get; set; }

        /// <summary>
        /// openid用于调用统一下单接口
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        public string body { get; set; }

        /// <summary>
        /// 附加数据
        /// </summary>
        public string attach { get; set; }

        /// <summary>
        /// access_token用于获取收货地址js函数入口参数
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 商品金额，用于统一下单
        /// </summary>
        public int total_fee { get; set; }

        /// <summary>
        /// 统一下单接口返回结果
        /// </summary>
        public PayData unifiedOrderResult { get; set; }

        public PayJsApi(BasePage page)
        {
            this.page = page;
        }


        /**
        * 
        * 网页授权获取用户基本信息的全部过程
        * 详情请参看网页授权获取用户基本信息：http://mp.weixin.qq.com/wiki/17/c0f37d5704f0b64713d5d2c37b468d75.html
        * 第一步：利用url跳转获取code
        * 第二步：利用code去获取openid和access_token
        * 
        */
        public void GetOpenidAndAccessToken()
        {
            if (!string.IsNullOrEmpty(page.Request.QueryString["code"]))
            {
                //获取code码，以获取openid和access_token
                string code = page.Request.QueryString["code"];
                ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".Get code : " + code);
                page.GetUserAuthorizeToken(code);
            }
            else
            {
                //构造网页授权获取code的URL
                string host = page.Request.Url.Host;
                string path = page.Request.Path;
                string redirect_uri = HttpUtility.UrlEncode("http://" + host + path);
                PayData data = new PayData();
                data.SetValue("appid", PayBase.Appid);
                data.SetValue("redirect_uri", redirect_uri);
                data.SetValue("response_type", "code");
                data.SetValue("scope", "snsapi_base");
                data.SetValue("state", "STATE" + "#wechat_redirect");
                string url = "https://open.weixin.qq.com/connect/oauth2/authorize?" + data.ToUrl();
                ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".Will Redirect to URL : " + url);
                try
                {
                    //触发微信返回code码         
                    page.Response.Redirect(url);//Redirect函数会抛出ThreadAbortException异常，不用处理这个异常
                }
                catch (System.Threading.ThreadAbortException ex)
                {
                }
            }
        }

        /// <summary>
        /// 调用统一下单，获得下单结果.失败时抛异常WxPayException
        /// </summary>
        /// <returns>统一下单结果</returns>
        public PayData GetUnifiedOrderResult(string out_trade_no)
        {
            //统一下单
            PayData data = new PayData();
            data.SetValue("body", body);
            data.SetValue("attach", attach);
            if (out_trade_no == null || out_trade_no.Trim() == "")
            {
                out_trade_no = PayBase.GenerateOutTradeNo();
            }
            data.SetValue("out_trade_no", out_trade_no);
            data.SetValue("total_fee", total_fee);
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
            //data.SetValue("goods_tag", "test");
            data.SetValue("trade_type", "JSAPI");
            data.SetValue("openid", openid);

            PayData result = PayBase.UnifiedOrder(data);
            if (!result.IsSet("appid") || !result.IsSet("prepay_id") || result.GetValue("prepay_id").ToString() == "")
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + ".UnifiedOrder response error!");
                throw new Exception("UnifiedOrder response error!");
            }

            unifiedOrderResult = result;
            return result;
        }

        /// <summary>
        /// 从统一下单成功返回的数据中获取微信浏览器调起jsapi支付所需的参数，
        ///微信浏览器调起JSAPI时的输入参数格式如下：
        ///{
        ///  "appId" : "wx2421b1c4370ec43b",     //公众号名称，由商户传入     
        ///   "timeStamp":" 1395712654",         //时间戳，自1970年以来的秒数     
        ///   "nonceStr" : "e61463f8efa94090b1f366cccfbbb444", //随机串     
        ///   "package" : "prepay_id=u802345jgfjsdfgsdg888",     
        ///   "signType" : "MD5",         //微信签名方式:    
        ///  "paySign" : "70EA570631E4BB79628FBCA90534C63FF7FADD89" //微信签名 
        /// }
        /// </summary>
        /// <returns>return string 微信浏览器调起JSAPI时的输入参数，json格式可以直接做参数用.更详细的说明请参考网页端调起支付API：pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=7_7</returns>
        public string GetJsApiParameters()
        {
            ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".JsApiPay::GetJsApiParam is processing...");

            PayData jsApiParam = new PayData();
            jsApiParam.SetValue("appId", unifiedOrderResult.GetValue("appid"));
            jsApiParam.SetValue("timeStamp", PayBase.GenerateTimeStamp());
            jsApiParam.SetValue("nonceStr", PayBase.GenerateNonceStr());
            jsApiParam.SetValue("package", "prepay_id=" + unifiedOrderResult.GetValue("prepay_id"));
            jsApiParam.SetValue("signType", "MD5");
            jsApiParam.SetValue("paySign", jsApiParam.MakeSign());

            string parameters = jsApiParam.ToJson();

            ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".Get jsApiParam : " + parameters);
            return parameters;
        }

        /**
        * 
        * 获取收货地址js函数入口参数,详情请参考收货地址共享接口：http://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=7_9
        * @return string 共享收货地址js函数需要的参数，json格式可以直接做参数使用
        */
        public string GetEditAddressParameters()
        {
            string parameter = "";
            try
            {
                string host = page.Request.Url.Host;
                string path = page.Request.Path;
                string queryString = page.Request.Url.Query;
                //这个地方要注意，参与签名的是网页授权获取用户信息时微信后台回传的完整url
                string url = "http://" + host + path + queryString;

                //构造需要用SHA1算法加密的数据
                PayData signData = new PayData();
                signData.SetValue("appid", PayBase.Appid);
                signData.SetValue("url", url);
                signData.SetValue("timestamp", PayBase.GenerateTimeStamp());
                signData.SetValue("noncestr", PayBase.GenerateNonceStr());
                signData.SetValue("accesstoken", access_token);
                string param = signData.ToUrl();

                ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".SHA1 encrypt param : " + param);
                //SHA1加密
                string addrSign = FormsAuthentication.HashPasswordForStoringInConfigFile(param, "SHA1");
                ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".SHA1 encrypt result : " + addrSign);

                //获取收货地址js函数入口参数
                PayData afterData = new PayData();
                afterData.SetValue("appId", PayBase.Appid);
                afterData.SetValue("scope", "jsapi_address");
                afterData.SetValue("signType", "sha1");
                afterData.SetValue("addrSign", addrSign);
                afterData.SetValue("timeStamp", signData.GetValue("timestamp"));
                afterData.SetValue("nonceStr", signData.GetValue("noncestr"));

                //转为json格式
                parameter = afterData.ToJson();
                ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + ".GetEditAddressParameters Get EditAddressParam : " + parameter);
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + ".GetEditAddressParameters异常：" + ex.ToString());
                throw new Exception(ex.ToString());
            }

            return parameter;
        }
    }
}
