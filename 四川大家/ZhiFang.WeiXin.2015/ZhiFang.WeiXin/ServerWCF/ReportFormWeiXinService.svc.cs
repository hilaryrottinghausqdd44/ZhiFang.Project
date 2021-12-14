using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.WeiXin.ServerContract;
using ZhiFang.WeiXin.Entity;
using System.Web;
using ZhiFang.WeiXin.BusinessObject;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin
{    
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]    
    public class ReportFormWeiXinService : IReportFormWeiXinService
    {
        IBLL.IBBSearchAccountReportForm IBBSearchAccountReportForm { get; set; }
        IBLL.IBBWeiXinAccount IBBWeiXinAccount { get; set; }

        public long UpLoadRF(Entity.BSearchAccountReportForm entity)
        {
            IBBSearchAccountReportForm.Entity = entity;
            try
            {
                string collectdate = "";
                if (entity.COLLECTDATE.HasValue)
                {
                    collectdate = entity.COLLECTDATE.Value.ToString("yyyy-MM-dd");
                }
                else
                {
                    collectdate = DateTime.Now.ToString("yyyy-MM-dd");
                }
                var OpenIdList = IBBSearchAccountReportForm.SendAddWeiXinBySearchAccountDic();
                if (OpenIdList != null)
                {
                    foreach (var oi in OpenIdList)
                    {
                        Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                        string urgencycolor = "#11cd6e";
                        data.Add("first", new TemplateDataObject() { value = "您在“" + entity.HospitalName + "”的检验报告结果已完成，请注意查收！", color = "#169ADA" });
                        data.Add("keyword1", new TemplateDataObject() { value = entity.PItemName, color = "#000000" });
                        data.Add("keyword2", new TemplateDataObject() { value = entity.Name, color = "#000000" });
                        data.Add("keyword3", new TemplateDataObject() { value = collectdate, color = "#000000" });

                        data.Add("remark", new TemplateDataObject() { value = "\r\n点击查看报告详情", color = "#000000" });
                        string url ="operate=reportid" + IBBSearchAccountReportForm.Entity.ReportFormIndexID + "T" + oi.Value;
                        //string url = "";//"operate=reportid&" + IBBSearchAccountReportForm.Entity.ReportFormIndexID + "T" + oi.Value;
                        IBBWeiXinAccount.PushWeiXinMessage((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction,new List<string> { oi.Key}, data, "reportformpush", url);
                    }
                    



                    //foreach (var oi in OpenIdList)
                    //{
                    //   // var first = new TemplateDataObject() { value = "您好！您的检验报告结果已完成，请注意查收！\r\n医院名称：" + entity.HospitalName + "", color = "#169ADA" };
                    //    var first = ;
                    //    var keyword1=;
                    //    var keyword2=;
                        
                        
                    //    var remark=;
                    //    BasePage.PushMessageTemplate3Context(HttpContext.Current.Application, oi.WeiXinAccount, ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("TemplateId1"), "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx0bed94c089c503d2&redirect_uri=http%3a%2f%2f" + ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("Domain") + "%2fZhiFang.WeiXin%2fWeiXinMainRouter.aspx%3foperate%3dreportid" + IBBSearchAccountReportForm.Entity.ReportFormIndexID + "T" + oi.AccountID + "&response_type=code&scope=snsapi_base&state=123#wechat_redirect", "#cc9966",
                    //        new TemplateIdObject3() {
                    //            first = first,
                    //            keyword1 = keyword1,
                    //            keyword2 = keyword2,
                    //            keyword3 = keyword3,
                    //            remark = remark
                    //        });
                    //}
                    ////https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx0bed94c089c503d2&redirect_uri=http%3a%2f%2fweixin.qhrch.qh.cn%2fZhiFang.WeiXin%2fWeiXinMainRouter.aspx%3foperate%3dreport&response_type=code&scope=snsapi_base&state=123#wechat_redirect
                    ////https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx359def2eeed3abe6&redirect_uri=http%3a%2f%2fweixin.qhrch.qh.cn%2fZhiFang.WeiXin%2fWeiXinMainRouter.aspx%3foperate%3dreportid" + IBBSearchAccountReportForm.Entity.ReportFormIndexID + "T" + oi.AccountID + "&response_type=code&scope=snsapi_base&state=123#wechat_redirect
                    ////https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx0bed94c089c503d2&redirect_uri=http%3a%2f%2f"+"weixin.qhrch.qh.cn"+"%2fZhiFang.WeiXin%2fWeiXinMainRouter.aspx%3foperate%3dreportid" + IBBSearchAccountReportForm.Entity.ReportFormIndexID + "T" + oi.AccountID + "&response_type=code&scope=snsapi_base&state=123#wechat_redirect
                    return IBBSearchAccountReportForm.Entity.Id;                    
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("错误信息UpLoadRF：" + ex.ToString() + "@@@@@@@@@@@@@@@@@@@@@@@@@entity.ReportFormIndexID=" + entity.ReportFormIndexID + "entity.Name=" + entity.Name);
                return -1;
                //throw new Exception(ex.Message);
            }
            return -1;
        }
    }
}
