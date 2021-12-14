using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.WeiXin.BusinessObject;
using ZhiFang.WeiXin.Common;
using ZhiFang.WeiXin.IBLL;

namespace ZhiFang.WeiXin.WebForm
{
    public partial class PayForm : BasePage
    {
        public static string wxJsApiParam { get; set; } //H5调起JS API参数
        public  string UOFID { get; set; } //订单ID
        public  string total_fee { get; set; } //订单金额
        protected void Page_Load(object sender, EventArgs e)
        {
            ZhiFang.Common.Log.Log.Info(this.GetType().ToString() + "缴费页面 page load");
            if (!IsPostBack)
            {
                string openid= Cookie.CookieHelper.Read(WeiXin.Entity.SysDicCookieSession.UserOpenID);
                string UOFID = Request.QueryString["UOFID"];
                //检测是否给当前页面传递了相关参数
                if (string.IsNullOrEmpty(openid) || string.IsNullOrEmpty(UOFID))
                {
                    Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面传参出错,请返回重试" + "</span>");
                    ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + "缴费页面.页面传参出错,请返回重试,This page have not get params, cannot be inited, exit...");
                    return;
                }
                this.UOFID = UOFID;
                ZhiFang.Common.Log.Log.Info(this.GetType().ToString() + "缴费页面.页面传参:page load:openid=" + openid + ",UOFID=" + UOFID);
                Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
                var bosuof = context.GetObject("BOSUserOrderForm") as IBOSUserOrderForm;
                var bosdof = context.GetObject("BOSDoctorOrderForm") as IBOSDoctorOrderForm;
                var uof=bosuof.Get(long.Parse(UOFID));
                var dof = bosdof.Get(uof.DOFID.Value);
                //若传递了相关参数，则调统一下单接口，获得后续相关接口的入口参数
                PayJsApi jsApiPay = new PayJsApi(this);
                jsApiPay.openid = openid;
                //jsApiPay.total_fee = int.Parse((uof.Price * 100).ToString());
                jsApiPay.total_fee = int.Parse(((uof.Price+uof.CollectionPrice) * 100).ToString());//添加采样费用
                jsApiPay.body = "医嘱单-医生：" + uof.DoctorName + ";";
                jsApiPay.body += (dof.HospitalName != null && dof.HospitalName != "") ? "医院:" + dof.HospitalName + ";" : "";
                jsApiPay.body += (dof.DeptName != null && dof.DeptName != "") ? "科室：" + dof.DeptName+";":"";
                jsApiPay.attach = !string.IsNullOrEmpty(ZhiFang.Common.Public.ConfigHelper.GetConfigString("DefaultPlatFormName"))? ZhiFang.Common.Public.ConfigHelper.GetConfigString("DefaultPlatFormName") : "检验中心检验单";
                this.total_fee=(jsApiPay.total_fee*0.01).ToString();
                //JSAPI支付预处理
                try
                {
                    PayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult(UOFID.ToString());
                    wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数                    
                    ZhiFang.Common.Log.Log.Debug(this.GetType().ToString() + "缴费页面.wxJsApiParam : " + wxJsApiParam);
                    //在页面上显示订单信息
                    //Response.Write("<span style='color:#00CD00;font-size:20px'>订单详情：</span><br/>");
                    //Response.Write("<span style='color:#00CD00;font-size:20px'>" + unifiedOrderResult.ToPrintStr() + "</span>");
                    if (unifiedOrderResult.GetValue("return_code").ToString().ToUpper() == "SUCCESS")
                    {
                        if (unifiedOrderResult.GetValue("result_code").ToString().ToUpper() == "SUCCESS")
                        {
                            bosuof.UpdateUnifiedOrder(long.Parse(UOFID), unifiedOrderResult.GetValue("prepay_id").ToString(), unifiedOrderResult.GetValue("result_code").ToString(), unifiedOrderResult.GetValue("return_msg").ToString());
                        }
                        else
                        {
                            unifiedOrderResult.GetValue("result_code").ToString();
                            unifiedOrderResult.GetValue("return_msg").ToString();
                            bosuof.UpdateUnifiedOrderError(long.Parse(UOFID), unifiedOrderResult.GetValue("result_code").ToString(), unifiedOrderResult.GetValue("return_msg").ToString(), unifiedOrderResult.GetValue("err_code").ToString(), unifiedOrderResult.GetValue("err_code_des").ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + "缴费页面.提交订单失败，请返回重试 : " + ex.ToString());
                    Response.Write("<span style='color:#FF0000;font-size:20px'>" + "提交订单失败，请返回重试!</span>");
                    Response.End();
                }
            }
        }
    }
}