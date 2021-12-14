using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.Entity.LIIP.ViewObject.Request;
using ZhiFang.IBLL.LIIP;
using ZhiFang.LabInformationIntegratePlatform.MessageHub;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabInformationIntegratePlatform.LISCommonWCF.MsgCommonService
{
   
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class MsgCommonService: IMsgCommonService
    {
        IBSCMsg IBSCMsg { get; set; }
        IBSCMsgHandle IBSCMsgHandle { get; set; }

        public BaseResultDataValue MsgSend(SCMsg_OTTH entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (entity != null)
            {
                try
                {
                    #region 参数验证
                    if (entity == null)
                    {
                        brdv.ErrorInfo = "参数为空！";
                        brdv.success = false;
                        return brdv;
                    }
                    if (entity.MsgTypeCode == null)
                    {
                        brdv.ErrorInfo = "参数中消息类型编码为空！";
                        brdv.success = false;
                        return brdv;
                    }

                    if (entity.RecDeptCodeHIS == null)
                    {
                        brdv.ErrorInfo = "参数中接收科室HIS编码为空！";
                        brdv.success = false;
                        return brdv;
                    }

                    if (entity.MsgContent == null)
                    {
                        brdv.ErrorInfo = "参数中消息内容体为空！";
                        brdv.success = false;
                        return brdv;
                    }

                    if (entity.RequireHandleTime == null)
                    {
                        brdv.ErrorInfo = "参数中要求处理时间为空！";
                        brdv.success = false;
                        return brdv;
                    }

                    if (entity.RequireConfirmTime == null)
                    {
                        brdv.ErrorInfo = "参数中要求确认时间为空！";
                        brdv.success = false;
                        return brdv;
                    }

                    if (entity.SenderAccount == null)
                    {
                        brdv.ErrorInfo = "参数中发送者帐号为空！";
                        brdv.success = false;
                        return brdv;
                    }

                    if (entity.SenderPWD == null)
                    {
                        brdv.ErrorInfo = "参数中发送者密码为空！";
                        brdv.success = false;
                        return brdv;
                    }

                    if (entity.SignCode == null)
                    {
                        brdv.ErrorInfo = "参数中签名为空！";
                        brdv.success = false;
                        return brdv;
                    }
                    ZhiFang.Common.Log.Log.Debug("ZhiFang.LabInformationIntegratePlatform.LISCommonWCF.MsgCommonService.ST_UDTO_AddSCMsg.IP="+ BusinessObject.Utils.IPHelper.GetClientIP() + ",参数：" + ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(entity));
                    #endregion

                    brdv = IBSCMsg.SaveAndGetDeptId_OTTH(entity);
                    if (brdv.success)
                    {
                        if (entity.MsgTypeCode == null)
                        {
                            ZhiFang.Common.Log.Log.Debug("IMService.ST_UDTO_AddSCMsg.消息类型为空！无法进行推送！");
                            brdv.ErrorInfo = "消息类型为空！无法进行推送！";
                        }
                        if (entity.MsgTypeCode.ToUpper().Trim() == ZFSCMsgType.第三方病理系统消息.Value.Code.ToUpper().Trim() ||
                             entity.MsgTypeCode.ToUpper().Trim() == ZFSCMsgType.第三方影像系统消息.Value.Code.ToUpper().Trim() ||
                             entity.MsgTypeCode.ToUpper().Trim() == ZFSCMsgType.第三方内镜系统消息.Value.Code.ToUpper().Trim() ||
                             entity.MsgTypeCode.ToUpper().Trim() == ZFSCMsgType.第三方超声系统消息.Value.Code.ToUpper().Trim() ||
                             entity.MsgTypeCode.ToUpper().Trim() == ZFSCMsgType.第三方心电图系统消息.Value.Code.ToUpper().Trim() ||
                             entity.MsgTypeCode.ToUpper().Trim() == ZFSCMsgType.第三方其它消息.Value.Code.ToUpper().Trim())
                        {
                            //医嘱科室
                            SendMessagesByDeptId_OTTH(brdv.ResultDataValue.ToString(), entity.MsgContent, entity.SCMsg.SenderID.ToString(), entity.SCMsg.SenderName, entity.SCMsg.Id.ToString(), entity.MsgTypeCode,ZFSCMsgStatus_ZF_LAB_START_CV.危急值发送.Value.Code);
                        }
                        if (entity.MsgTypeCode.ToUpper().Trim() == ZFSCMsgType.第三方病理危急值消息.Value.Code.ToUpper().Trim() ||
                             entity.MsgTypeCode.ToUpper().Trim() == ZFSCMsgType.第三方影像危急值消息.Value.Code.ToUpper().Trim() ||
                             entity.MsgTypeCode.ToUpper().Trim() == ZFSCMsgType.第三方内镜危急值消息.Value.Code.ToUpper().Trim() ||
                             entity.MsgTypeCode.ToUpper().Trim() == ZFSCMsgType.第三方心电图危急值消息.Value.Code.ToUpper().Trim() ||
                             entity.MsgTypeCode.ToUpper().Trim() == ZFSCMsgType.第三方超声危急值消息.Value.Code.ToUpper().Trim() )
                        {
                            if (brdv.ResultDataValue == null)
                            {
                                ZhiFang.Common.Log.Log.Error("MsgCommonService.MsgSend:brdv.ResultDataValue为空！");
                            }
                            if (entity == null)
                            {
                                ZhiFang.Common.Log.Log.Error("MsgCommonService.MsgSend:entity为空！");
                            }
                            if (entity.SCMsg == null)
                            {
                                ZhiFang.Common.Log.Log.Error("MsgCommonService.MsgSend:entity.SCMsg为空！");
                            }
                            //医嘱科室
                            SendMessagesByDeptId_OTTH_CV(brdv.ResultDataValue, entity.MsgContent, entity.SCMsg.SenderID.ToString(), entity.SCMsg.SenderName, entity.SCMsg.Id.ToString(), entity.MsgTypeCode, ZFSCMsgStatus_ZF_LAB_START_CV.危急值发送.Value.Code);
                        }
                        brdv.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.SCMsg.Id);
                    }
                    else
                    {
                        brdv.ResultDataValue = "";
                    }
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Error("MsgCommonService.MsgSend.异常：" + ex.ToString());
                    brdv.success = false;
                    brdv.ErrorInfo = "发送消息异常！";
                }
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return brdv;
        }

        public BaseResultDataValue MsgHandleSearch(SCMsg_OTTH_Search entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (entity != null)
            {
                try
                {
                    #region 参数验证
                    if (entity == null)
                    {
                        brdv.ErrorInfo = "参数为空！";
                        brdv.success = false;
                        return brdv;
                    }

                    if (entity.MsgTypeCode == null)
                    {
                        brdv.ErrorInfo = "参数中消息类型编码为空！";
                        brdv.success = false;
                        return brdv;
                    }

                    if (entity.SenderAccount == null)
                    {
                        brdv.ErrorInfo = "参数中发送者帐号为空！";
                        brdv.success = false;
                        return brdv;
                    }

                    if (entity.SenderPWD == null)
                    {
                        brdv.ErrorInfo = "参数中发送者密码为空！";
                        brdv.success = false;
                        return brdv;
                    }

                    if (entity.SignCode == null)
                    {
                        brdv.ErrorInfo = "参数中签名为空！";
                        brdv.success = false;
                        return brdv;
                    }
                    ZhiFang.Common.Log.Log.Debug("ZhiFang.LabInformationIntegratePlatform.LISCommonWCF.MsgCommonService.MsgHandleSearch.IP=" + BusinessObject.Utils.IPHelper.GetClientIP() + ",参数：" + ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(entity));
                    #endregion

                    SCMsg SCMsg = IBSCMsg.MsgHandleSearchById_OTTH(entity);
                    if (SCMsg == null)
                    {
                        ZhiFang.Common.Log.Log.Error("MsgHandleSearch.未能获取消息编号为:" + entity.MsgTypeCode + "的处理结果！");
                        brdv.success = false;
                        brdv.ErrorInfo = "未能获取消息编号为:" + entity.MsgTypeCode + "的处理结果！";
                        return brdv;
                    }
                    if (SCMsg.HandleFlag != 1 || SCMsg.SCMsgHandleList == null || SCMsg.SCMsgHandleList.Count <= 0)
                    {
                        ZhiFang.Common.Log.Log.Error("MsgHandleSearch.消息编号为:" + entity.MsgTypeCode + "的消息还未处理！");
                        brdv.success = false;
                        brdv.ErrorInfo = "消息编号为:" + entity.MsgTypeCode + "的消息还未处理！";
                        return brdv;
                    }
                    dynamic dobj = new System.Dynamic.ExpandoObject();

                    dobj.HandleDeptName = SCMsg.SCMsgHandleList[0].HandleDeptName;
                    dobj.Handler = SCMsg.SCMsgHandleList[0].HandlerName;
                    dobj.HandlingDateTime = SCMsg.SCMsgHandleList[0].HandleTime.HasValue ? SCMsg.SCMsgHandleList[0].HandleTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                    dobj.HandleDesc = SCMsg.SCMsgHandleList[0].HandleDesc;
                    dobj.Confirmer = SCMsg.ConfirmerName;
                    dobj.ConfirmDateTime = SCMsg.ConfirmDateTime.HasValue ? SCMsg.ConfirmDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                    brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(dobj);
                    brdv.success = true;
                    return brdv;

                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Error("MsgHandleSearch.异常：" + ex.ToString());
                    brdv.success = false;
                    brdv.ErrorInfo = "获取处理结果异常！";
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("MsgHandleSearch.错误信息：实体参数不能为空！");
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return brdv;
        }

        public string SendMessagesByDeptId_OTTH(string ToDeptId, string Message, string FormUserEmpId, string FormUserEmpName, string SCMsgId, string SCMsgTypeCode, string ZFSCMsgStatus)
        {
            try
            {
                if (ToDeptId == null || ToDeptId.Trim().Length <= 0)
                {
                    return "部门ID为空！";
                }

                if (Message == null || Message.Trim().Length <= 0)
                {
                    return "消息体为空！";
                }

                if (FormUserEmpId == null || FormUserEmpId.Trim().Length <= 0)
                {
                    return "发送者ID为空！";
                }
                //if (FormUserEmpName == null || FormUserEmpName.Trim().Length <= 0)
                //{
                //    return "发送者名称为空！";
                //}

                ZhiFang.Common.Log.Log.Debug("IMService.SendMessagesByDeptId_OTTH.ToDeptId：" + string.Join(",", ToDeptId) + ";Message:" + Message + ";FormUserEmpId:" + FormUserEmpId);
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.IBCVCriticalValueEmpIdDeptLink IBCVCriticalValueEmpIdDeptLink = (IBLL.LIIP.IBCVCriticalValueEmpIdDeptLink)context.GetObject("BCVCriticalValueEmpIdDeptLink");
                var tmplist = IBCVCriticalValueEmpIdDeptLink.SearchListByHQL(" DeptId= " + ToDeptId);
                if (tmplist != null && tmplist.Count() > 0)
                {
                    for (int i = 0; i < tmplist.Count(); i++)
                    {
                        var OnlineUserList = MainMessageHub.ClientList.ClientStationList.Where(a => a.ClientStationTypeId == 1 && a.StatusTypeId == 1);
                        //ZhiFang.Common.Log.Log.Debug("IMService.SendMessagesByDeptId_OTTH.在线人员列表：" + ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(OnlineUserList));
                        if (OnlineUserList.Count(a => a.EmpId == tmplist[i].EmpID.ToString()) > 0)
                        {
                            List<string> cidlist = OnlineUserList.Where(a => a.EmpId == tmplist[i].EmpID.ToString()).ElementAt(0).ConnectionIdList;
                            foreach (var c in cidlist)
                            {
                                MainMessageHub.clist.Client(c).ReceiveMessageByEmpId_CV(FormUserEmpId, FormUserEmpName, Message, SCMsgId, SCMsgTypeCode);
                                ZhiFang.Common.Log.Log.Debug("IMService.SendMessagesByDeptId_OTTH.ConnectionId:" + c + ";FormUserEmpId：" + FormUserEmpId + ";FormUserEmpName:" + FormUserEmpName + ";Message:" + Message + ";SCMsgId:" + SCMsgId + ";SCMsgTypeCode:" + SCMsgTypeCode);

                                MainMessageHub.clist.Client(c).ReceiveMessageByEmpId(FormUserEmpId, FormUserEmpName, Message, SCMsgId, SCMsgTypeCode, ZFSCMsgStatus_ZF_LIIP_MSG.GetStatusDic().Where(a => a.Value.Name == "LIS平台消息发送").ElementAt(0).Value.Code);
                                ZhiFang.Common.Log.Log.Debug("IMService.SendMessagesByDeptId_OTTH.ConnectionId:" + c + ";FormUserEmpId：" + FormUserEmpId + ";FormUserEmpName:" + FormUserEmpName + ";Message:" + Message + ";SCMsgId:" + SCMsgId + ";SCMsgTypeCode:" + SCMsgTypeCode + ";ZFSCMsgStatus:" + ZFSCMsgStatus_ZF_LIIP_MSG.GetStatusDic().Where(a => a.Value.Name == "LIS平台消息发送").ElementAt(0).Value.Code);
                            }
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Error("IMService.SendMessagesByDeptId_OTTH.部门ID: " + ToDeptId + "的科室的用户:EmpId=" + tmplist[i].EmpID.ToString() + "不在线！");
                        }
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("IMService.SendMessagesByDeptId_OTTH.部门ID: " + ToDeptId + "的科室没有可以通知的用户！");
                    return "部门ID:" + ToDeptId + "的科室没有可以通知的用户！";
                }

                return "发送完成！";
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("IMService.SendMessagesByDeptId_OTTH.异常：" + e.ToString());
                return "IMService.SendMessagesByDeptId_OTTH.异常：" + e.ToString();
            }
        }

        public string SendMessagesByDeptId_OTTH_CV(string ToDeptId, string Message, string FormUserEmpId, string FormUserEmpName, string SCMsgId, string SCMsgTypeCode, string ZFSCMsgStatus)
        {
            try
            {
                if (ToDeptId == null || ToDeptId.Trim().Length <= 0)
                {
                    return "部门ID为空！";
                }

                if (Message == null || Message.Trim().Length <= 0)
                {
                    return "消息体为空！";
                }

                if (FormUserEmpId == null || FormUserEmpId.Trim().Length <= 0)
                {
                    return "发送者ID为空！";
                }
                //if (FormUserEmpName == null || FormUserEmpName.Trim().Length <= 0)
                //{
                //    return "发送者名称为空！";
                //}

                ZhiFang.Common.Log.Log.Debug("IMService.SendMessagesByDeptId_OTTH_CV.ToDeptId：" + string.Join(",", ToDeptId) + ";Message:" + Message + ";FormUserEmpId:" + FormUserEmpId);
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.IBCVCriticalValueEmpIdDeptLink IBCVCriticalValueEmpIdDeptLink = (IBLL.LIIP.IBCVCriticalValueEmpIdDeptLink)context.GetObject("BCVCriticalValueEmpIdDeptLink");
                var tmplist = IBCVCriticalValueEmpIdDeptLink.SearchListByHQL(" DeptId= " + ToDeptId);
                if (tmplist != null && tmplist.Count() > 0)
                {
                    for (int i = 0; i < tmplist.Count(); i++)
                    {
                        var OnlineUserList = MainMessageHub.ClientList.ClientStationList.Where(a => a.ClientStationTypeId == 1 && a.StatusTypeId == 1);
                        //ZhiFang.Common.Log.Log.Debug("IMService.SendMessagesByDeptId_OTTH_CV.在线人员列表：" + ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(OnlineUserList));
                        if (OnlineUserList.Count(a => a.EmpId == tmplist[i].EmpID.ToString()) > 0)
                        {
                            List<string> cidlist = OnlineUserList.Where(a => a.EmpId == tmplist[i].EmpID.ToString()).ElementAt(0).ConnectionIdList;
                            foreach (var c in cidlist)
                            {
                                MainMessageHub.clist.Client(c).ReceiveMessageByEmpId_CV(FormUserEmpId, FormUserEmpName, Message, SCMsgId, ZFSCMsgType.第三方病理危急值消息.Value.Code, ZFSCMsgStatus_OTTH_PATHOLOGY_CV.GetStatusDic().Where(a=>a.Value.Name== "危急值发送").ElementAt(0).Value.Code);

                                ZhiFang.Common.Log.Log.Debug("IMService.SendMessagesByDeptId_OTTH_CV.ConnectionId:" + c + ";FormUserEmpId：" + FormUserEmpId + ";FormUserEmpName:" + FormUserEmpName + ";Message:" + Message + ";SCMsgId:" + SCMsgId + ";SCMsgTypeCode:" + SCMsgTypeCode+ ";ZFSCMsgStatus:"+ ZFSCMsgStatus_OTTH_PATHOLOGY_CV.GetStatusDic().Where(a => a.Value.Name == "危急值发送").ElementAt(0).Value.Code);

                                MainMessageHub.clist.Client(c).ReceiveMessageByEmpId(FormUserEmpId, FormUserEmpName, Message, SCMsgId, ZFSCMsgType.第三方病理危急值消息.Value.Code, ZFSCMsgStatus_OTTH_PATHOLOGY_CV.GetStatusDic().Where(a => a.Value.Name == "危急值发送").ElementAt(0).Value.Code);

                                ZhiFang.Common.Log.Log.Debug("IMService.SendMessagesByDeptId_OTTH_CV.ConnectionId:" + c + ";FormUserEmpId：" + FormUserEmpId + ";FormUserEmpName:" + FormUserEmpName + ";Message:" + Message + ";SCMsgId:" + SCMsgId + ";SCMsgTypeCode:" + SCMsgTypeCode + ";ZFSCMsgStatus:" + ZFSCMsgStatus_OTTH_PATHOLOGY_CV.GetStatusDic().Where(a => a.Value.Name == "危急值发送").ElementAt(0).Value.Code);
                            }
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Error("IMService.SendMessagesByDeptId_OTTH_CV.部门ID: " + ToDeptId + "的科室的用户:EmpId=" + tmplist[i].EmpID.ToString() + "不在线！");
                        }
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("IMService.SendMessagesByDeptId_OTTH_CV.部门ID: " + ToDeptId + "的科室没有可以通知的用户！");
                    return "部门ID:" + ToDeptId + "的科室没有可以通知的用户！";
                }

                return "发送完成！";
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("IMService.SendMessagesByDeptId_OTTH_CV.异常：" + e.ToString());
                return "IMService.SendMessagesByDeptId_OTTH_CV.异常：" + e.ToString();
            }
        }
    }
}
