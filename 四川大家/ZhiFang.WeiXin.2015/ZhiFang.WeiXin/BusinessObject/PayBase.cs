using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using ZhiFang.Common.Log;

namespace ZhiFang.WeiXin.BusinessObject
{
    public class PayBase : BasePage
    {
        public static string MCHID = ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("PayMCHID");
        public static string KEY = ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("PayKEY");
        public static string OrderQueryUrl = ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("PayOrderQueryUrl");
        public static string ReverseUrl = ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("PayReverseUrl");
        public static string RefundUrl = ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("PayRefundUrl");
        public static string RefundQueryUrl = ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("PayRefundQueryUrl");
        public static string DownloadBillUrl = ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("PayDownloadBillUrl");
        public static string ShortUrl = ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("PayShortUrl");
        public static string UnifiedOrderUrl = ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("PayUnifiedOrderUrl");
        public static string CloseOrderUrl = ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("PayCloseOrderUrl");
        public static string NOTIFY_URL= ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("PayNOTIFY_URL");
        public static string PayToUserWeiXinUrl = ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("PayToUserWeiXinUrl");
        public static string spbill_create_ip = ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("spbill_create_ip");
        public static int REPORT_LEVENL = 0;
        public static string SSLCERT_PATH = ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("SSLCERT_PATH");//"cert/apiclient_cert.p12";
        public static string SSLCERT_PASSWORD = ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("SSLCERT_PASSWORD");// "1409524002";

        /**
        *    
        * 查询订单
        * @param WxPayData inputObj 提交给查询订单API的参数
        * @param int timeOut 超时时间
        * @throws Exception
        * @return 成功时返回订单查询结果，其他抛异常
        */
        public static PayData OrderQuery(PayData inputObj, int timeOut = 6)
        {
            string url = PayBase.OrderQueryUrl;
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
            {
                throw new Exception("订单查询接口中，out_trade_no、transaction_id至少填一个！");
            }

            inputObj.SetValue("appid", PayBase.Appid);//公众账号ID
            inputObj.SetValue("mch_id", PayBase.MCHID);//商户号
            inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign());//签名

            string xml = inputObj.ToXml();

            var start = DateTime.Now;

            ZhiFang.Common.Log.Log.Debug("WxPayApiOrderQuery request : " + xml);
            string response = WeiXinHttpToTencentHelp.Post(xml, url, false, timeOut);//调用HTTP通信接口提交数据
            ZhiFang.Common.Log.Log.Debug("WxPayApiOrderQuery response : " + response);

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);//获得接口耗时

            //将xml格式的数据转化为对象以返回
            PayData result = new PayData();
            result.FromXml(response);

            ReportCostTime(url, timeCost, result);//测速上报

            return result;
        }


        /**
        * 
        * 撤销订单API接口
        * @param WxPayData inputObj 提交给撤销订单API接口的参数，out_trade_no和transaction_id必填一个
        * @param int timeOut 接口超时时间
        * @throws Exception
        * @return 成功时返回API调用结果，其他抛异常
        */
        public static PayData Reverse(PayData inputObj, int timeOut = 6)
        {
            string url = PayBase.ReverseUrl;
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
            {
                throw new Exception("撤销订单API接口中，参数out_trade_no和transaction_id必须填写一个！");
            }

            inputObj.SetValue("appid", PayBase.Appid);//公众账号ID
            inputObj.SetValue("mch_id", PayBase.MCHID);//商户号
            inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign());//签名
            string xml = inputObj.ToXml();

            var start = DateTime.Now;//请求开始时间

            ZhiFang.Common.Log.Log.Debug("WxPayApi.Reverse request : " + xml);

            string response = WeiXinHttpToTencentHelp.Post(xml, url, true, timeOut);

            ZhiFang.Common.Log.Log.Debug("WxPayApi.Reverse response : " + response);

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);

            PayData result = new PayData();
            result.FromXml(response);

            ReportCostTime(url, timeCost, result);//测速上报

            return result;
        }


        /**
        * 
        * 申请退款
        * @param WxPayData inputObj 提交给申请退款API的参数
        * @param int timeOut 超时时间
        * @throws Exception
        * @return 成功时返回接口调用结果，其他抛异常
        */
        public static PayData Refund(PayData inputObj, int timeOut = 6)
        {
            string url = PayBase.RefundUrl;
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
            {
                throw new Exception("退款申请接口中，out_trade_no、transaction_id至少填一个！");
            }
            else if (!inputObj.IsSet("out_refund_no"))
            {
                throw new Exception("退款申请接口中，缺少必填参数out_refund_no！");
            }
            else if (!inputObj.IsSet("total_fee"))
            {
                throw new Exception("退款申请接口中，缺少必填参数total_fee！");
            }
            else if (!inputObj.IsSet("refund_fee"))
            {
                throw new Exception("退款申请接口中，缺少必填参数refund_fee！");
            }
            else if (!inputObj.IsSet("op_user_id"))
            {
                throw new Exception("退款申请接口中，缺少必填参数op_user_id！");
            }

            inputObj.SetValue("appid", PayBase.Appid);//公众账号ID
            inputObj.SetValue("mch_id", PayBase.MCHID);//商户号
            inputObj.SetValue("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign());//签名

            string xml = inputObj.ToXml();
            var start = DateTime.Now;

            ZhiFang.Common.Log.Log.Debug("WxPayApi.Refund request : " + xml);
            string response = WeiXinHttpToTencentHelp.Post(xml, url, true, timeOut);//调用HTTP通信接口提交数据到API
            ZhiFang.Common.Log.Log.Debug("WxPayApi.Refund response : " + response);

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);//获得接口耗时

            //将xml格式的结果转换为对象以返回
            PayData result = new PayData();
            result.FromXml(response);

            ReportCostTime(url, timeCost, result);//测速上报

            return result;
        }


        /**
	    * 
	    * 查询退款
	    * 提交退款申请后，通过该接口查询退款状态。退款有一定延时，
	    * 用零钱支付的退款20分钟内到账，银行卡支付的退款3个工作日后重新查询退款状态。
	    * out_refund_no、out_trade_no、transaction_id、refund_id四个参数必填一个
	    * @param WxPayData inputObj 提交给查询退款API的参数
	    * @param int timeOut 接口超时时间
	    * @throws Exception
	    * @return 成功时返回，其他抛异常
	    */
        public static PayData RefundQuery(PayData inputObj, int timeOut = 6)
        {
            string url = PayBase.RefundQueryUrl;
            //检测必填参数
            if (!inputObj.IsSet("out_refund_no") && !inputObj.IsSet("out_trade_no") &&
                !inputObj.IsSet("transaction_id") && !inputObj.IsSet("refund_id"))
            {
                throw new Exception("退款查询接口中，out_refund_no、out_trade_no、transaction_id、refund_id四个参数必填一个！");
            }

            inputObj.SetValue("appid", PayBase.Appid);//公众账号ID
            inputObj.SetValue("mch_id", PayBase.MCHID);//商户号
            inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign());//签名

            string xml = inputObj.ToXml();

            var start = DateTime.Now;//请求开始时间

            ZhiFang.Common.Log.Log.Debug("WxPayApi.RefundQuery request : " + xml);
            string response = WeiXinHttpToTencentHelp.Post(xml, url, false, timeOut);//调用HTTP通信接口以提交数据到API
            ZhiFang.Common.Log.Log.Debug("WxPayApi.RefundQuery response : " + response);

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);//获得接口耗时

            //将xml格式的结果转换为对象以返回
            PayData result = new PayData();
            result.FromXml(response);

            ReportCostTime(url, timeCost, result);//测速上报

            return result;
        }

        /***
       * 退款查询完整业务流程逻辑
       * @param refund_id 微信退款单号（优先使用）
       * @param out_refund_no 商户退款单号
       * @param transaction_id 微信订单号
       * @param out_trade_no 商户订单号
       * @return 退款查询结果（xml格式）
       */
        public static string RunRefundQuery(string refund_id, string out_refund_no, string transaction_id, string out_trade_no)
        {
            Log.Info("RunRefundQuery is processing...");

            PayData data = new PayData();
            if (!string.IsNullOrEmpty(refund_id))
            {
                data.SetValue("refund_id", refund_id);//微信退款单号，优先级最高
            }
            else if (!string.IsNullOrEmpty(out_refund_no))
            {
                data.SetValue("out_refund_no", out_refund_no);//商户退款单号，优先级第二
            }
            else if (!string.IsNullOrEmpty(transaction_id))
            {
                data.SetValue("transaction_id", transaction_id);//微信订单号，优先级第三
            }
            else
            {
                data.SetValue("out_trade_no", out_trade_no);//商户订单号，优先级最低
            }

            PayData result = RefundQuery(data);//提交退款查询给API，接收返回数据

            Log.Info("RunRefundQuery process complete, result : " + result.ToXml());
            return result.ToPrintStr();
        }

        /***
        * 申请退款完整业务流程逻辑
        * @param transaction_id 微信订单号（优先使用）
        * @param out_trade_no 商户订单号
        * @param total_fee 订单总金额
        * @param refund_fee 退款金额
        * @return 退款结果（xml格式）
        */
        public static string RunRefundApply(string transaction_id, string out_trade_no, string total_fee, string refund_fee, string out_refund_no)
        {
            Log.Info("RunRefundApply is processing...");

            PayData data = new PayData();
            if (!string.IsNullOrEmpty(transaction_id))//微信订单号存在的条件下，则已微信订单号为准
            {
                data.SetValue("transaction_id", transaction_id);
            }
            else//微信订单号不存在，才根据商户订单号去退款
            {
                data.SetValue("out_trade_no", out_trade_no);
            }

            data.SetValue("total_fee", int.Parse(total_fee));//订单总金额
            data.SetValue("refund_fee", int.Parse(refund_fee));//退款金额
            data.SetValue("out_refund_no", out_refund_no);//随机生成商户退款单号
            data.SetValue("op_user_id", MCHID);//操作员，默认为商户号

            PayData result = Refund(data);//提交退款申请给API，接收返回数据

            Log.Info("RunRefundApply process complete, result : " + result.ToXml());
            return result.ToPrintStr();
        }
        /***
       * 申请退款完整业务流程逻辑
       * @param transaction_id 微信订单号（优先使用）
       * @param out_trade_no 商户订单号
       * @param total_fee 订单总金额
       * @param refund_fee 退款金额
       * @return 退款结果（xml格式）
       */
        public static SortedDictionary<string, object> RunRefundApplyList(string transaction_id, string out_trade_no, string total_fee, string refund_fee, string out_refund_no)
        {
            Log.Info("RunRefundApply is processing...");

            PayData data = new PayData();
            if (!string.IsNullOrEmpty(transaction_id))//微信订单号存在的条件下，则已微信订单号为准
            {
                data.SetValue("transaction_id", transaction_id);
            }
            else//微信订单号不存在，才根据商户订单号去退款
            {
                data.SetValue("out_trade_no", out_trade_no);
            }

            data.SetValue("total_fee", int.Parse(total_fee));//订单总金额
            data.SetValue("refund_fee", int.Parse(refund_fee));//退款金额
            data.SetValue("out_refund_no", out_refund_no);//随机生成商户退款单号
            data.SetValue("op_user_id", MCHID);//操作员，默认为商户号

            PayData result = Refund(data);//提交退款申请给API，接收返回数据

            Log.Info("RunRefundApply process complete, result : " + result.ToXml());
            return result.GetValues();
        }

        /**
        * 下载对账单
        * @param WxPayData inputObj 提交给下载对账单API的参数
        * @param int timeOut 接口超时时间
        * @throws Exception
        * @return 成功时返回，其他抛异常
        */
        public static PayData DownloadBill(PayData inputObj, int timeOut = 6)
        {
            string url = PayBase.DownloadBillUrl;
            //检测必填参数
            if (!inputObj.IsSet("bill_date"))
            {
                throw new Exception("对账单接口中，缺少必填参数bill_date！");
            }

            inputObj.SetValue("appid", PayBase.Appid);//公众账号ID
            inputObj.SetValue("mch_id", PayBase.MCHID);//商户号
            inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign());//签名

            string xml = inputObj.ToXml();

            ZhiFang.Common.Log.Log.Debug("WxPayApi.DownloadBill request : " + xml);
            string response = WeiXinHttpToTencentHelp.Post(xml, url, false, timeOut);//调用HTTP通信接口以提交数据到API
            ZhiFang.Common.Log.Log.Debug("WxPayApi.DownloadBill result : " + response);

            PayData result = new PayData();
            //若接口调用失败会返回xml格式的结果
            if (response.Substring(0, 5) == "<xml>")
            {
                result.FromXml(response);
            }
            //接口调用成功则返回非xml格式的数据
            else
                result.SetValue("result", response);

            return result;
        }


        /**
	    * 
	    * 转换短链接
	    * 该接口主要用于扫码原生支付模式一中的二维码链接转成短链接(weixin://wxpay/s/XXXXXX)，
	    * 减小二维码数据量，提升扫描速度和精确度。
	    * @param WxPayData inputObj 提交给转换短连接API的参数
	    * @param int timeOut 接口超时时间
	    * @throws Exception
	    * @return 成功时返回，其他抛异常
	    */
        public static PayData ConvertToShortUrl(PayData inputObj, int timeOut = 6)
        {
            string url = PayBase.ShortUrl;
            //检测必填参数
            if (!inputObj.IsSet("long_url"))
            {
                throw new Exception("需要转换的URL，签名用原串，传输需URL encode！");
            }

            inputObj.SetValue("appid", PayBase.Appid);//公众账号ID
            inputObj.SetValue("mch_id", PayBase.MCHID);//商户号
            inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串	
            inputObj.SetValue("sign", inputObj.MakeSign());//签名
            string xml = inputObj.ToXml();

            var start = DateTime.Now;//请求开始时间

            ZhiFang.Common.Log.Log.Debug("WxPayApi.ShortUrl request : " + xml);
            string response = WeiXinHttpToTencentHelp.Post(xml, url, false, timeOut);
            ZhiFang.Common.Log.Log.Debug("WxPayApi.ShortUrl response : " + response);

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);

            PayData result = new PayData();
            result.FromXml(response);
            ReportCostTime(url, timeCost, result);//测速上报

            return result;
        }


        /**
        * 
        * 统一下单
        * @param WxPaydata inputObj 提交给统一下单API的参数
        * @param int timeOut 超时时间
        * @throws Exception
        * @return 成功时返回，其他抛异常
        */
        public static PayData UnifiedOrder(PayData inputObj, int timeOut = 6)
        {
            string url = PayBase.UnifiedOrderUrl;
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no"))
            {
                throw new Exception("缺少统一支付接口必填参数out_trade_no！");
            }
            else if (!inputObj.IsSet("body"))
            {
                throw new Exception("缺少统一支付接口必填参数body！");
            }
            else if (!inputObj.IsSet("total_fee"))
            {
                throw new Exception("缺少统一支付接口必填参数total_fee！");
            }
            else if (!inputObj.IsSet("trade_type"))
            {
                throw new Exception("缺少统一支付接口必填参数trade_type！");
            }

            //关联参数
            if (inputObj.GetValue("trade_type").ToString() == "JSAPI" && !inputObj.IsSet("openid"))
            {
                throw new Exception("统一支付接口中，缺少必填参数openid！trade_type为JSAPI时，openid为必填参数！");
            }
            if (inputObj.GetValue("trade_type").ToString() == "NATIVE" && !inputObj.IsSet("product_id"))
            {
                throw new Exception("统一支付接口中，缺少必填参数product_id！trade_type为NATIVE时，product_id为必填参数！");
            }

            //异步通知url未设置，则使用配置文件中的url
            if (!inputObj.IsSet("notify_url"))
            {
                inputObj.SetValue("notify_url", PayBase.NOTIFY_URL);//异步通知url
            }

            inputObj.SetValue("appid", PayBase.Appid);//公众账号ID
            inputObj.SetValue("mch_id", PayBase.MCHID);//商户号
            //inputObj.SetValue("spbill_create_ip", PayBase.IP);//终端ip	  	    
            inputObj.SetValue("spbill_create_ip", System.Web.HttpContext.Current.Request.UserHostAddress);
            inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串

            //签名
            //inputObj.SetValue("sign", inputObj.MakeSign());
            string xml = inputObj.ToXmlAddSign(inputObj.MakeSign());
            
            
           
            var start = DateTime.Now;

            ZhiFang.Common.Log.Log.Debug("WxPayApi.UnfiedOrder request : " + xml);
            string response = WeiXinHttpToTencentHelp.Post(xml, url, false, timeOut);
            ZhiFang.Common.Log.Log.Debug("WxPayApi.UnfiedOrder response : " + response);

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);

            PayData result = new PayData();
            result.FromXml(response);

            ReportCostTime(url, timeCost, result);//测速上报

            return result;
        }


        /**
	    * 
	    * 关闭订单
	    * @param WxPayData inputObj 提交给关闭订单API的参数
	    * @param int timeOut 接口超时时间
	    * @throws Exception
	    * @return 成功时返回，其他抛异常
	    */
        public static PayData CloseOrder(PayData inputObj, int timeOut = 6)
        {
            string url = PayBase.CloseOrderUrl;
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no"))
            {
                throw new Exception("关闭订单接口中，out_trade_no必填！");
            }

            inputObj.SetValue("appid", PayBase.Appid);//公众账号ID
            inputObj.SetValue("mch_id", PayBase.MCHID);//商户号
            inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串		
            inputObj.SetValue("sign", inputObj.MakeSign());//签名
            string xml = inputObj.ToXml();

            var start = DateTime.Now;//请求开始时间

            string response = WeiXinHttpToTencentHelp.Post(xml, url, false, timeOut);

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);

            PayData result = new PayData();
            result.FromXml(response);

            ReportCostTime(url, timeCost, result);//测速上报

            return result;
        }


        /**
	    * 
	    * 测速上报
	    * @param string interface_url 接口URL
	    * @param int timeCost 接口耗时
	    * @param WxPayData inputObj参数数组
	    */
        private static void ReportCostTime(string interface_url, int timeCost, PayData inputObj)
        {
            //如果不需要进行上报
            if (PayBase.REPORT_LEVENL == 0)
            {
                return;
            }

            //如果仅失败上报
            if (PayBase.REPORT_LEVENL == 1 && inputObj.IsSet("return_code") && inputObj.GetValue("return_code").ToString() == "SUCCESS" &&
             inputObj.IsSet("result_code") && inputObj.GetValue("result_code").ToString() == "SUCCESS")
            {
                return;
            }

            //上报逻辑
            PayData data = new PayData();
            data.SetValue("interface_url", interface_url);
            data.SetValue("execute_time_", timeCost);
            //返回状态码
            if (inputObj.IsSet("return_code"))
            {
                data.SetValue("return_code", inputObj.GetValue("return_code"));
            }
            //返回信息
            if (inputObj.IsSet("return_msg"))
            {
                data.SetValue("return_msg", inputObj.GetValue("return_msg"));
            }
            //业务结果
            if (inputObj.IsSet("result_code"))
            {
                data.SetValue("result_code", inputObj.GetValue("result_code"));
            }
            //错误代码
            if (inputObj.IsSet("err_code"))
            {
                data.SetValue("err_code", inputObj.GetValue("err_code"));
            }
            //错误代码描述
            if (inputObj.IsSet("err_code_des"))
            {
                data.SetValue("err_code_des", inputObj.GetValue("err_code_des"));
            }
            //商户订单号
            if (inputObj.IsSet("out_trade_no"))
            {
                data.SetValue("out_trade_no", inputObj.GetValue("out_trade_no"));
            }
            //设备号
            if (inputObj.IsSet("device_info"))
            {
                data.SetValue("device_info", inputObj.GetValue("device_info"));
            }

            try
            {
                Report(data);
            }
            catch (Exception ex)
            {
                //不做任何处理
            }
        }


        /**
	    * 
	    * 测速上报接口实现
	    * @param WxPayData inputObj 提交给测速上报接口的参数
	    * @param int timeOut 测速上报接口超时时间
	    * @throws Exception
	    * @return 成功时返回测速上报接口返回的结果，其他抛异常
	    */
        public static PayData Report(PayData inputObj, int timeOut = 1)
        {
            string url = "https://api.mch.weixin.qq.com/payitil/report";
            //检测必填参数
            if (!inputObj.IsSet("interface_url"))
            {
                throw new Exception("接口URL，缺少必填参数interface_url！");
            }
            if (!inputObj.IsSet("return_code"))
            {
                throw new Exception("返回状态码，缺少必填参数return_code！");
            }
            if (!inputObj.IsSet("result_code"))
            {
                throw new Exception("业务结果，缺少必填参数result_code！");
            }
            if (!inputObj.IsSet("user_ip"))
            {
                throw new Exception("访问接口IP，缺少必填参数user_ip！");
            }
            if (!inputObj.IsSet("execute_time_"))
            {
                throw new Exception("接口耗时，缺少必填参数execute_time_！");
            }

            inputObj.SetValue("appid", PayBase.Appid);//公众账号ID
            inputObj.SetValue("mch_id", PayBase.MCHID);//商户号
            //inputObj.SetValue("user_ip", PayBase.IP);//终端ip
            inputObj.SetValue("spbill_create_ip", System.Web.HttpContext.Current.Request.UserHostAddress);
            inputObj.SetValue("time", DateTime.Now.ToString("yyyyMMddHHmmss"));//商户上报时间	 
            inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign());//签名
            string xml = inputObj.ToXml();

            ZhiFang.Common.Log.Log.Info("WxPayApi.Report request : " + xml);

            string response = WeiXinHttpToTencentHelp.Post(xml, url, false, timeOut);

            ZhiFang.Common.Log.Log.Info("WxPayApi.Report response : " + response);

            PayData result = new PayData();
            result.FromXml(response);
            return result;
        }

        /**
        * 根据当前系统时间加随机序列来生成订单号(用户的订单号，以后用订单ID来替换String(32))
         * @return 订单号
        */
        public static string GenerateOutTradeNo()
        {
            var ran = new Random();
            return string.Format("{0}{1}{2}", PayBase.MCHID, DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
        }

        /**
        * 生成时间戳，标准北京时间，时区为东八区，自1970年1月1日 0点0分0秒以来的秒数
         * @return 时间戳
        */
        public static string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /**
        * 生成随机串，随机串包含字母或数字
        * @return 随机串
        */
        public static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        public static Dictionary<string, string> GetUnifiedOrderErrorCodeList()
        {
            Dictionary<string, string> ErrorInfoDic = new Dictionary<string, string>();
            if (ErrorInfoDic.Count <= 0)
            {
                ErrorInfoDic.Add("NOAUTH", "商户无此接口权限");
                ErrorInfoDic.Add("NOTENOUGH", "余额不足");
                ErrorInfoDic.Add("ORDERPAID", "商户订单已支付");

                ErrorInfoDic.Add("ORDERCLOSED", "订单已关闭");
                ErrorInfoDic.Add("SYSTEMERROR", "系统错误");
                ErrorInfoDic.Add("APPID_NOT_EXIST", "APPID不存在");

                ErrorInfoDic.Add("MCHID_NOT_EXIST", "MCHID不存在");
                ErrorInfoDic.Add("APPID_MCHID_NOT_MATCH", "appid和mch_id不匹配");
                ErrorInfoDic.Add("LACK_PARAMS", "缺少必要的请求参数");

                ErrorInfoDic.Add("OUT_TRADE_NO_USED", "商户订单号重复");
                ErrorInfoDic.Add("SIGNERROR", "签名错误");
                ErrorInfoDic.Add("XML_FORMAT_ERROR", "XML格式错误");

                ErrorInfoDic.Add("REQUIRE_POST_METHOD", "请使用post方法");
                ErrorInfoDic.Add("POST_DATA_EMPTY", "post数据为空");
                ErrorInfoDic.Add("NOT_UTF8", "编码格式错误");
            }
            return ErrorInfoDic;
        }

        /**
       *    
       * 企业付款接口
       * @param WxPayData inputObj 提交给企业付款接口API的参数
       * @param int timeOut 超时时间
       * @throws Exception
       * @return 成功时返回企业付款数据，其他抛异常
       */
        public static PayData PayToUserWeiXin(PayData inputObj, int timeOut = 6)
        {
            string url = PayBase.PayToUserWeiXinUrl;

            //检测必填参数
            if (!inputObj.IsSet("partner_trade_no"))
            {
                throw new Exception("企业付款接口中，缺少必填参数partner_trade_no！");
            }
            else if (!inputObj.IsSet("openid"))
            {
                throw new Exception("企业付款接口中，缺少必填参数openid！");
            }
            else if (!inputObj.IsSet("check_name"))
            {
                throw new Exception("企业付款接口中，缺少必填参数check_name！");
            }
            else if (!inputObj.IsSet("re_user_name"))
            {
                throw new Exception("企业付款接口中，缺少必填参数re_user_name！");
            }
            else if (!inputObj.IsSet("amount"))
            {
                throw new Exception("企业付款接口中，缺少必填参数amount！");
            }
            else if (!inputObj.IsSet("desc"))
            {
                throw new Exception("企业付款接口中，缺少必填参数desc！");
            }

            inputObj.SetValue("mch_appid", PayBase.Appid);//公众账号ID
            inputObj.SetValue("mchid", PayBase.MCHID);//商户号
            inputObj.SetValue("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));//随机字符串
            inputObj.SetValue("spbill_create_ip", PayBase.spbill_create_ip);//服务器IP
            inputObj.SetValue("sign", inputObj.MakeSign());//签名

            string xml = inputObj.ToXml();
            var start = DateTime.Now;

            ZhiFang.Common.Log.Log.Debug("WxPayApi.PayToUserWeiXin request : " + xml);
            string response = WeiXinHttpToTencentHelp.Post(xml, url, true, timeOut);//调用HTTP通信接口提交数据到API
            ZhiFang.Common.Log.Log.Debug("WxPayApi.PayToUserWeiXin response : " + response);

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);//获得接口耗时

            //将xml格式的结果转换为对象以返回
            PayData result = new PayData();
            result.FromXml(response);

            ReportCostTime(url, timeCost, result);//测速上报

            return result;
        }
    }
    public class PayData
    {
        public PayData()
        {

        }

        //采用排序的Dictionary的好处是方便对数据包进行签名，不用再签名之前再做一次排序
        private SortedDictionary<string, object> m_values = new SortedDictionary<string, object>();

        /**
        * 设置某个字段的值
        * @param key 字段名
         * @param value 字段值
        */
        public void SetValue(string key, object value)
        {
            m_values[key] = value;
        }

        /**
        * 根据字段名获取某个字段的值
        * @param key 字段名
         * @return key对应的字段值
        */
        public object GetValue(string key)
        {
            object o = null;
            m_values.TryGetValue(key, out o);
            return o;
        }

        /**
         * 判断某个字段是否已设置
         * @param key 字段名
         * @return 若字段key已被设置，则返回true，否则返回false
         */
        public bool IsSet(string key)
        {
            object o = null;
            m_values.TryGetValue(key, out o);
            if (null != o)
                return true;
            else
                return false;
        }

        /**
        * @将Dictionary转成xml
        * @return 经转换得到的xml串
        * @throws Exception
        **/

        public string ToXml()
        {
            //数据为空时不能转化为xml格式
            if (0 == m_values.Count)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + "WxPayData数据为空!");
                throw new Exception("WxPayData数据为空!");
            }

            string xml = "<xml>";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                //字段值不能为null，会影响后续流程
                if (pair.Value == null)
                {
                    ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + "WxPayData内部含有值为null的字段!pair.Key:"+ pair.Key);
                    throw new Exception("WxPayData内部含有值为null的字段!");
                }

                if (pair.Value.GetType() == typeof(int))
                {
                    xml += "<" + pair.Key + ">" + pair.Value + "</" + pair.Key + ">";
                }
                else if (pair.Value.GetType() == typeof(string))
                {
                    xml += "<" + pair.Key + ">" + "<![CDATA[" + pair.Value + "]]></" + pair.Key + ">";
                }
                else//除了string和int类型不能含有其他数据类型
                {
                    ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + "WxPayData字段数据类型错误!");
                    throw new Exception("WxPayData字段数据类型错误!");
                }
            }
            xml += "</xml>";
            return xml;
        }
        public string ToXmlAddSign(string Sign)
        {
            //数据为空时不能转化为xml格式
            if (0 == m_values.Count)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + "WxPayData数据为空!");
                throw new Exception("WxPayData数据为空!");
            }

            string xml = "<xml>";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                //字段值不能为null，会影响后续流程
                if (pair.Value == null)
                {
                    ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + "WxPayData内部含有值为null的字段!pair.Key:"+ pair.Key);
                    throw new Exception("WxPayData内部含有值为null的字段!");
                }

                if (pair.Value.GetType() == typeof(int))
                {
                    xml += "<" + pair.Key + ">" + pair.Value + "</" + pair.Key + ">";
                }
                else if (pair.Value.GetType() == typeof(string))
                {
                    xml += "<" + pair.Key + ">" + "<![CDATA[" + pair.Value + "]]></" + pair.Key + ">";
                }
                else//除了string和int类型不能含有其他数据类型
                {
                    ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + "WxPayData字段数据类型错误!");
                    throw new Exception("WxPayData字段数据类型错误!");
                }
            }
            xml += "<sign>" + Sign + "</sign>";
            xml += "</xml>";
            return xml;
        }

        /**
        * @将xml转为WxPayData对象并返回对象内部的数据
        * @param string 待转换的xml串
        * @return 经转换得到的Dictionary
        * @throws Exception
        */
        public SortedDictionary<string, object> FromXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + "将空的xml串转换为WxPayData不合法!");
                throw new Exception("将空的xml串转换为WxPayData不合法!");
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                m_values[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
            }

            try
            {
                //2015-06-29 错误是没有签名
                if (m_values["return_code"] != "SUCCESS")
                {
                    return m_values;
                }
                CheckSign();//验证签名,不通过会抛异常
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return m_values;
        }

        /**
        * @Dictionary格式转化成url参数格式
        * @ return url格式串, 该串不包含sign字段值
        */
        public string ToUrl()
        {
            string buff = "";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                if (pair.Value == null)
                {
                    ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + "WxPayData内部含有值为null的字段!pair.Key:"+ pair.Key);
                    throw new Exception("WxPayData内部含有" + pair.Key + "值为null的字段!");
                }

                if (pair.Key != "sign" && pair.Value.ToString() != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            return buff;
        }


        /**
        * @Dictionary格式化成Json
         * @return json串数据
        */
        public string ToJson()
        {
            string jsonStr =  JsonMapper.ToJson(m_values);
            return jsonStr;
        }

        /**
        * @values格式化成能在Web页面上显示的结果（因为web页面上不能直接输出xml格式的字符串）
        */
        public string ToPrintStr()
        {
            string str = "";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                if (pair.Value == null)
                {
                    ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + "WxPayData内部含有值为null的字段!pair.Key:"+ pair.Key);
                    throw new Exception("WxPayData内部含有值为null的字段!");
                }

                str += string.Format("{0}={1}<br>", pair.Key, pair.Value.ToString());
            }
            ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + "Print in Web Page : " + str);
            return str;
        }

        /**
        * @生成签名，详见签名生成算法
        * @return 签名, sign字段不参加签名
        */
        public string MakeSign()
        {
            //转url格式
            string str = ToUrl();
            ZhiFang.Common.Log.Log.Debug("MakeSign.ToUrl1:"+str);
            //在string后加入API KEY
            str += "&key=" + PayBase.KEY;
            ZhiFang.Common.Log.Log.Debug("MakeSign.ToUrl2:" + str);
            //MD5加密
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }
            //所有字符转为大写
            return sb.ToString().ToUpper();
        }

        /**
        * 
        * 检测签名是否正确
        * 正确返回true，错误抛异常
        */
        public bool CheckSign()
        {
            //如果没有设置签名，则跳过检测
            if (!IsSet("sign"))
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + "WxPayData签名存在但不合法!");
                throw new Exception("WxPayData签名存在但不合法!");
            }
            //如果设置了签名但是签名为空，则抛异常
            else if (GetValue("sign") == null || GetValue("sign").ToString() == "")
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + "WxPayData签名存在但不合法!");
                throw new Exception("WxPayData签名存在但不合法!");
            }

            //获取接收到的签名
            string return_sign = GetValue("sign").ToString();

            //在本地计算新的签名
            string cal_sign = MakeSign();

            if (cal_sign == return_sign)
            {
                return true;
            }

            ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + "WxPayData签名验证错误!");
            throw new Exception("WxPayData签名验证错误!");
        }

        /**
        * @获取Dictionary
        */
        public SortedDictionary<string, object> GetValues()
        {
            return m_values;
        }
    }
   
}
