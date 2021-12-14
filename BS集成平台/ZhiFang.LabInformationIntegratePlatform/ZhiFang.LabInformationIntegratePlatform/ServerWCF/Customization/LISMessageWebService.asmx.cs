using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ZhiFang.Entity.LIIP.ViewObject.Request;
using ZhiFang.IBLL.LIIP;
using ZhiFang.IBLL.RBAC;

namespace ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization
{
    /// <summary>
    /// LISMessageWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://ZhiFang.LabInformationIntegratePlatform/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class LISMessageWebService : System.Web.Services.WebService
    {
        IBSCMsg IBSCMsg { get; set; }
        [WebMethod]
        public bool SendMessage(string MessageBody, out string SCMsgId, out string ErrorInfo)
        {
            SCMsgId = "";
            ErrorInfo = "";
            //bool flag = false;
            try
            {
                ZhiFang.Common.Log.Log.Debug("LISMessageWebService.SendMessage.MessageBody:" + MessageBody + ",IP=" + ZhiFang.LabInformationIntegratePlatform.BusinessObject.Utils.IPHelper.GetClientIP());
                //MessageBody = "{"SenderID":"1234567890" ,"SenderName":"张三" ,"SenderAccount":"P1" ,"SendSectionID":"111" ,"SendSectionName":"生化小组" ,"MsgTypeID":"1" ,"MsgTypeName":"报告延迟" ,"MsgTypeCode":"ReportDelay" ,"RecLabID":"1001" ,"RecLabName":"社区医院" ,"RecLabCode":"1001" ,"LabBarCode":"1001" ,"CenterBarCode":"1001" ,"MsgContent":"" ,"SystemID":"1001" ,"SystemCName":"智方_检验之星" ,"SystemCode":"ZF_LabStar" }";
                LISSendMessageVO lismsg = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<LISSendMessageVO>(MessageBody);

                #region 参数检查
                if (lismsg.CenterBarCode == null || lismsg.CenterBarCode == "")
                {
                    ErrorInfo = "中心实验室条码为空！";
                    return false;
                }

                if (lismsg.LabBarCode == null || lismsg.LabBarCode == "")
                {
                    ErrorInfo = "送检单位条码为空！";
                    return false;
                }

                if (lismsg.MsgContent == null || lismsg.MsgContent == "")
                {
                    ErrorInfo = "消息内容为空！";
                    return false;
                }

                if (lismsg.MsgTypeCode == null || lismsg.MsgTypeCode == "")
                {
                    ErrorInfo = "消息类型代码为空！";
                    return false;
                }

                if (!lismsg.MsgTypeID.HasValue)
                {
                    ErrorInfo = "消息类型ID为空！";
                    return false;
                }

                if (lismsg.MsgTypeName == null || lismsg.MsgTypeName == "")
                {
                    ErrorInfo = "消息类型名称为空！";
                    return false;
                }

                if (lismsg.SenderAccount == null || lismsg.SenderAccount == "")
                {
                    ErrorInfo = "发送者账号为空！";
                    return false;
                }

                if (!lismsg.SendSectionID.HasValue)
                {
                    ErrorInfo = "消息发送小组ID为空！";
                    return false;
                }

                //if (lismsg.SendSectionName == null || lismsg.SendSectionName == "")
                //{
                //    ErrorInfo = "消息发送小组名称为空！";
                //    return false;
                //}

                if (lismsg.SystemCName == null || lismsg.SystemCName == "")
                {
                    ErrorInfo = "所属系统名称为空！";
                    return false;
                }

                if (lismsg.SystemCode == null || lismsg.SystemCode == "")
                {
                    ErrorInfo = "所属系统代码为空！";
                    return false;
                }

                if (!lismsg.SystemID.HasValue)
                {
                    ErrorInfo = "所属系统ID为空！";
                    return false;
                }

                if (!lismsg.RecLabID.HasValue)
                {
                    ErrorInfo = "接收机构编码为空！";
                    return false;
                }

                //if (lismsg.RecLabName == null || lismsg.RecLabName == "")
                //{
                //    ErrorInfo = "接收机构名称为空！";
                //    return false;
                //}

                if (!lismsg.SenderID.HasValue)
                {
                    ErrorInfo = "发送者ID为空！";
                    return false;
                }

                if (lismsg.SenderName == null || lismsg.SenderName == "")
                {
                    ErrorInfo = "发送者名称为空！";
                    return false;
                }
                #endregion

                IApplicationContext context = ContextRegistry.GetContext();
                IBSCMsg IBSCMsg = (IBSCMsg)context.GetObject("BSCMsg");
                if (IBSCMsg.LISSendMessage(lismsg))
                {
                    string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZF_LIIP_SendMSGToHISInterface_Url");
                    string MsgTypeCode = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZF_LIIP_SendToHISInterface_MsgTypeCode");
                    if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(url))
                    {
                        var MsgTypeCodelist = MsgTypeCode.Split(',').ToList();
                        if (MsgTypeCodelist.Count(a => a == lismsg.MsgTypeCode) > 0)
                        {
                            HISInterfaceVO tmp = new HISInterfaceVO();
                            tmp.SenderID = lismsg.SenderID;
                            tmp.SenderName = lismsg.SenderName;
                            tmp.SenderAccount = lismsg.SenderAccount;
                            tmp.SendSectionID = lismsg.SendSectionID;
                            tmp.SendSectionName = lismsg.SendSectionName;
                            tmp.MsgTypeID = lismsg.MsgTypeID;
                            tmp.MsgTypeName = lismsg.MsgTypeName;
                            tmp.MsgTypeCode = lismsg.MsgTypeCode;
                            tmp.RecLabID = lismsg.RecLabID;
                            tmp.RecLabName = lismsg.RecLabName;
                            tmp.RecLabCode = lismsg.RecLabCode;
                            tmp.CenterBarCode = lismsg.CenterBarCode;
                            tmp.LabBarCode = lismsg.LabBarCode;
                            tmp.MsgContent = lismsg.MsgContent;
                            tmp.SystemID = lismsg.SystemID;
                            tmp.SystemCName = lismsg.SystemCName;
                            tmp.SystemCode = lismsg.SystemCode;
                            ZhiFang.Common.Log.Log.Error("LISMessageWebService.SendMessage.HISInterfaceVO.tmp：" + ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tmp));
                            var result = ZhiFang.LIIP.Common.RestfullHelper.InvkerRestServicePost(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tmp), "JSON", url+ "?plugName=DaJiaCallCenter&action=ReportDelay", 5000);
                            ZhiFang.Common.Log.Log.Error("LISMessageWebService.SendMessage.HISInterfaceVO.result：" + result);
                        }
                    }
                    SCMsgId = lismsg.SCMsgID.ToString();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("LISMessageWebService.SendMessage.异常：" + e.ToString());
                ErrorInfo = e.ToString();
                return false;
            }
        }

    }

    public class HISInterfaceVO
    {
        public long? SenderID { get; set; }
        public string SenderName { get; set; }
        public string SenderAccount { get; set; }
        public long? SendSectionID { get; set; }
        public string SendSectionName { get; set; }
        public long? MsgTypeID { get; set; }
        public string MsgTypeName { get; set; }
        public string MsgTypeCode { get; set; }
        public long? RecLabID { get; set; }
        public string RecLabName { get; set; }
        public string RecLabCode { get; set; }
        public string CenterBarCode { get; set; }
        public string LabBarCode { get; set; }
        public string MsgContent { get; set; }
        public long? SystemID { get; set; }
        public string SystemCName { get; set; }
        public string SystemCode { get; set; }

    }

}
