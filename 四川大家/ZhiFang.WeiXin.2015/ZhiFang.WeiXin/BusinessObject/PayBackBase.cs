using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace ZhiFang.WeiXin.BusinessObject
{
    /// <summary>
    /// 回调处理基类
    /// 主要负责接收微信支付后台发送过来的数据，对数据进行签名验证
    /// 子类在此类基础上进行派生并重写自己的回调处理过程
    /// </summary>
    public class PayBackBase
    {
        public Page page { get; set; }
        public PayBackBase(Page page)
        {
            this.page = page;
        }

        /// <summary>
        /// 接收从微信支付后台发送过来的数据并验证签名
        /// </summary>
        /// <returns>微信支付后台返回的数据</returns>
        public PayData GetNotifyData()
        {
            //接收从微信后台POST过来的数据
            System.IO.Stream s = page.Request.InputStream;
            int count = 0;
            byte[] buffer = new byte[1024];
            StringBuilder builder = new StringBuilder();
            while ((count = s.Read(buffer, 0, 1024)) > 0)
            {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            s.Flush();
            s.Close();
            s.Dispose();

            ZhiFang.Common.Log.Log.Info(this.GetType().ToString() + ".Receive 微信支付后台返回的数据 : " + builder.ToString());

            //转换数据格式并验证签名
            PayData data = new PayData();
            try
            {
                data.FromXml(builder.ToString());
            }
            catch (Exception ex)
            {
                //若签名错误，则立即返回结果给微信支付后台
                PayData res = new PayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", ex.Message);
                ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + ".Sign check error : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }

            ZhiFang.Common.Log.Log.Info(this.GetType().ToString() + ".Check sign success");
            return data;
        }

        //派生类需要重写这个方法，进行不同的回调处理
        public virtual void ProcessNotify()
        {

        }
    }
}