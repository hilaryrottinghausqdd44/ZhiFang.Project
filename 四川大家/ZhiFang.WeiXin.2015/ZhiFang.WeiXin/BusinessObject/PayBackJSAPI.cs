﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using ZhiFang.WeiXin.IBLL;

namespace ZhiFang.WeiXin.BusinessObject
{
    public class PayBackJSAPI : PayBackBase
    {
        public PayBackJSAPI(Page page) : base(page)
        {

        }

        public override void ProcessNotify()
        {
            PayData notifyData = GetNotifyData();

            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                PayData res = new PayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "支付结果中微信订单号不存在");
                ZhiFang.Common.Log.Log.Error(this.GetType().ToString()+".The Pay result is error : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }

            string transaction_id = notifyData.GetValue("transaction_id").ToString();

            //查询订单，判断订单真实性
            if (!QueryOrder(transaction_id))
            {
                //若订单查询失败，则立即返回结果给微信支付后台
                PayData res = new PayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "订单查询失败");
                ZhiFang.Common.Log.Log.Error(this.GetType().ToString()+".Order query failure : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }
            //查询订单成功
            else
            {
                Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
                var bosuof = context.GetObject("BOSUserOrderForm") as IBOSUserOrderForm;
                if (notifyData.GetValue("out_trade_no") != null && notifyData.GetValue("out_trade_no").ToString().Trim() != "")
                {
                    bosuof.UpdatePayStatus(long.Parse(notifyData.GetValue("out_trade_no").ToString()), notifyData.GetValue("transaction_id").ToString(), notifyData.GetValue("time_end").ToString());
                }
                PayData res = new PayData();
                res.SetValue("return_code", "SUCCESS");
                res.SetValue("return_msg", "OK");
                ZhiFang.Common.Log.Log.Info(this.GetType().ToString()+".order query success : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }
        }

        //查询订单
        private bool QueryOrder(string transaction_id)
        {
            PayData req = new PayData();
            req.SetValue("transaction_id", transaction_id);
            PayData res = PayBase.OrderQuery(req);
            if (res.GetValue("return_code").ToString() == "SUCCESS" &&
                res.GetValue("result_code").ToString() == "SUCCESS")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
