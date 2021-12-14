using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using System.Web;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.Entity.LIIP.ViewObject.Request;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.LIIP;
using ZhiFang.LabInformationIntegratePlatform.MessageHub;
using ZhiFang.LabInformationIntegratePlatform.ServerContract;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabInformationIntegratePlatform.ServerWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class IMService : ServerContract.IIMService
    {
        IBSCIMInfomationContent IBSCIMInfomationContent { get; set; }

        IBSCMsg IBSCMsg { get; set; }

        IBSCMsgHandle IBSCMsgHandle { get; set; }

        IBSCMsgPhraseDic IBSCMsgPhraseDic { get; set; }

        IBSCMsgType IBSCMsgType { get; set; }

        IBCVCriticalValueEmpIdDeptLink IBCVCriticalValueEmpIdDeptLink { get; set; }

        IBLL.RBAC.IBHRDept IBHRDept { get; set; }

        ZhiFang.IBLL.RBAC.IBRBACUser IBRBACUser { get; set; }

        ZhiFang.IBLL.Common.IBBParameter IBBParameter { get; set; }


        #region SCIMInfomationContent_即时通讯
        //Add  SCIMInfomationContent
        public BaseResultDataValue ST_UDTO_AddSCIMInfomationContent(SCIMInfomationContent entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBSCIMInfomationContent.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBSCIMInfomationContent.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        //Update  SCIMInfomationContent
        //public BaseResultBool ST_UDTO_UpdateSCIMInfomationContent(SCIMInfomationContent entity)
        //{
        //    BaseResultBool baseResultBool = new BaseResultBool();
        //    if (entity != null)
        //    {
        //        entity.DataUpdateTime = DateTime.Now;
        //        IBSCIMInfomationContent.Entity = entity;
        //        try
        //        {
        //            baseResultBool.success = IBSCIMInfomationContent.Edit();
        //            if (baseResultBool.success)
        //            {
        //                //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            baseResultBool.success = false;
        //            baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
        //            //throw new Exception(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        baseResultBool.success = false;
        //        baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
        //    }
        //    return baseResultBool;
        //}
        ////Update  SCIMInfomationContent
        //public BaseResultBool ST_UDTO_UpdateSCIMInfomationContentByField(SCIMInfomationContent entity, string fields)
        //{
        //    BaseResultBool baseResultBool = new BaseResultBool();
        //    if (entity != null)
        //    {
        //        if (!string.IsNullOrEmpty(fields))
        //            fields = fields + ",DataUpdateTime";
        //        entity.DataUpdateTime = DateTime.Now;
        //        IBSCIMInfomationContent.Entity = entity;
        //        try
        //        {
        //            if ((fields != null) && (fields.Length > 0))
        //            {
        //                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCIMInfomationContent.Entity, fields);
        //                if (tempArray != null)
        //                {
        //                    baseResultBool.success = IBSCIMInfomationContent.Update(tempArray);
        //                    if (baseResultBool.success)
        //                    {
        //                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                baseResultBool.success = false;
        //                baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
        //                //baseResultBool.success = IBSCIMInfomationContent.Edit();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            baseResultBool.success = false;
        //            baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
        //            //throw new Exception(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        baseResultBool.success = false;
        //        baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
        //    }
        //    return baseResultBool;
        //}
        //Delele  SCIMInfomationContent

        //public BaseResultBool ST_UDTO_DelSCIMInfomationContent(long id)
        //{
        //    BaseResultBool baseResultBool = new BaseResultBool();
        //    try
        //    {
        //        IBSCIMInfomationContent.Entity = IBSCIMInfomationContent.Get(id);
        //        if (IBSCIMInfomationContent.Entity != null)
        //        {
        //            long labid = IBSCIMInfomationContent.Entity.LabID;
        //            string entityName = IBSCIMInfomationContent.Entity.GetType().Name;
        //            baseResultBool.success = IBSCIMInfomationContent.RemoveByHQL(id);
        //            if (baseResultBool.success)
        //            {
        //                //IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
        //            }
        //        }
        //        else
        //        {
        //            baseResultBool.success = false;
        //            baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        baseResultBool.success = false;
        //        baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return baseResultBool;
        //}

        public BaseResultBool ST_UDTO_SCIMInfomationContentReRead(long id)
        {
            BaseResultBool BaseResultBool = new BaseResultBool();
            try
            {
                if (id <= 0)
                {
                    BaseResultBool.success = false;
                    BaseResultBool.ErrorInfo = "错误信息：参数错误！";
                    return BaseResultBool;
                }

                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID) == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID).Trim() == "")
                {
                    BaseResultBool.success = false;
                    BaseResultBool.ErrorInfo = "错误信息：人员信息无法获取！请登录后重试！";
                }

                BaseResultBool = IBSCIMInfomationContent.SCIMInfomationContentReRead(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID).ToString(), ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName).ToString(), id);
                if (BaseResultBool.success)
                {
                    //后续消息websocket推送处理
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SCIMInfomationContentReRead.异常：" + ex.ToString());
                BaseResultBool.success = false;
                BaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }

            return BaseResultBool;
        }

        public BaseResultBool ST_UDTO_SCIMInfomationContentReBack(long id)
        {
            BaseResultBool BaseResultBool = new BaseResultBool();
            try
            {
                if (id <= 0)
                {
                    BaseResultBool.success = false;
                    BaseResultBool.ErrorInfo = "错误信息：参数错误！";
                    return BaseResultBool;
                }

                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID) == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID).Trim() == "")
                {
                    BaseResultBool.success = false;
                    BaseResultBool.ErrorInfo = "错误信息：人员信息无法获取！请登录后重试！";
                }

                BaseResultBool = IBSCIMInfomationContent.SCIMInfomationContentReBack(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID).ToString(), ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName).ToString(), id);
                if (BaseResultBool.success)
                {
                    //后续消息websocket推送处理
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SCIMInfomationContentReBack.异常：" + ex.ToString());
                BaseResultBool.success = false;
                BaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }

            return BaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchSCIMInfomationContent(SCIMInfomationContent entity)
        {
            EntityList<SCIMInfomationContent> entityList = new EntityList<SCIMInfomationContent>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBSCIMInfomationContent.Entity = entity;
                try
                {
                    entityList.list = IBSCIMInfomationContent.Search();
                    entityList.count = IBSCIMInfomationContent.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<SCIMInfomationContent>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchSCIMInfomationContentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<SCIMInfomationContent> entityList = new EntityList<SCIMInfomationContent>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBSCIMInfomationContent.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBSCIMInfomationContent.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<SCIMInfomationContent>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchSCIMInfomationContentById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBSCIMInfomationContent.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<SCIMInfomationContent>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region SCMsg_业务消息
        //Add  SCMsg
        public BaseResultDataValue ST_UDTO_AddSCMsg(SCMsg entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBSCMsg.Entity = entity;
                try
                {
                    if (entity.MsgTypeCode == null)
                    {
                        ZhiFang.Common.Log.Log.Debug("IMService.ST_UDTO_AddSCMsg.消息类型为空！无法进行推送！");
                        baseResultDataValue.ErrorInfo = "消息类型为空！无法进行推送！";
                        baseResultDataValue.success = false;
                        return baseResultDataValue;
                    }
                    else
                    {
                        switch (entity.MsgTypeCode.ToUpper().Trim())
                        {
                            case "ZF_LAB_START_CV"://危急值
                                baseResultDataValue = IBSCMsg.AddSCMsgZF_LAB_START_CV(entity);
                                if (baseResultDataValue.success)
                                {
                                    List<string> tmpempid = baseResultDataValue.ResultDataValue.Split(',').ToList();

                                    //医嘱科室
                                    SendMessagesByEmpIdList_CV(tmpempid, entity.MsgContent, entity.SenderID, entity.SenderName, entity.Id, entity.MsgTypeCode, ZFSCMsgStatus_ZF_LAB_START_CV.危急值发送.Value.Code);
                                    //检验科室（小组）(以后要加配置开关，是否通知发送者部门所有人)
                                    SendMessagesByDeptId_CV(entity.SendDeptID.ToString(), entity.MsgContent, entity.SenderID, entity.SenderName, entity.Id, entity.MsgTypeCode, ZFSCMsgStatus_ZF_LAB_START_CV.危急值发送.Value.Code);
                                }
                                baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                                break;
                            case "ZF_PRE_REJECTION"://LIS前处理拒收
                                SendMessagesByDeptId_CV(baseResultDataValue.ResultDataValue.ToString(), entity.MsgContent, entity.SenderID, entity.SenderName, entity.Id, entity.MsgTypeCode, entity.MsgTypeCode);
                                break;
                            default:
                                break;
                        }

                    }


                    //if (baseResultDataValue.success)
                    //{

                    //    //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    //    if (entity.MsgTypeCode == null)
                    //    {
                    //        ZhiFang.Common.Log.Log.Debug("IMService.ST_UDTO_AddSCMsg.消息类型为空！无法进行推送！");
                    //        baseResultDataValue.ErrorInfo = "消息类型为空！无法进行推送！";
                    //    }
                    //    switch (entity.MsgTypeCode.ToUpper().Trim())
                    //    {
                    //        case "ZF_LAB_START_CV":
                    //            //医嘱科室
                    //            SendMessagesByDeptId_CV(baseResultDataValue.ResultDataValue.ToString(), entity.MsgContent, entity.SenderID, entity.SenderName, entity.Id, entity.MsgTypeCode, ZFSCMsgStatus_ZF_LAB_START_CV.危急值发送.Value.Code);
                    //            //检验科室（小组）(以后要加配置开关，是否通知发送者部门所有人)
                    //            SendMessagesByDeptId_CV(entity.SendDeptID.ToString(), entity.MsgContent, entity.SenderID, entity.SenderName, entity.Id, entity.MsgTypeCode, ZFSCMsgStatus_ZF_LAB_START_CV.危急值发送.Value.Code);
                    //            break;
                    //        case "ZF_PRE_REJECTION":
                    //            SendMessagesByDeptId_CV(baseResultDataValue.ResultDataValue.ToString(), entity.MsgContent, entity.SenderID, entity.SenderName, entity.Id, entity.MsgTypeCode, entity.MsgTypeCode);
                    //            break;
                    //        default:
                    //            break;
                    //    }
                    //    baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                    //}
                    //else
                    //{
                    //    baseResultDataValue.ResultDataValue = "";
                    //}
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_AddSCMsg.异常：" + ex.ToString());
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.ToString();
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  SCMsg
        public BaseResultBool ST_UDTO_UpdateSCMsg(SCMsg entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            //if (entity != null)
            //{
            //    entity.DataUpdateTime = DateTime.Now;
            //    IBSCMsg.Entity = entity;
            //    try
            //    {
            //        baseResultBool.success = IBSCMsg.Edit();
            //        if (baseResultBool.success)
            //        {
            //            //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        baseResultBool.success = false;
            //        baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
            //        //throw new Exception(ex.Message);
            //    }
            //}
            //else
            //{
            //    baseResultBool.success = false;
            //    baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            //}
            return baseResultBool;
        }
        //Update  SCMsg
        public BaseResultBool ST_UDTO_UpdateSCMsgByField(SCMsg entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBSCMsg.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCMsg.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBSCMsg.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBSCMsg.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }

        public BaseResultBool ST_UDTO_SCMsgByWarningUpload(SCMsg entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBSCMsg.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCMsg.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBSCMsg.SCMsgByWarningUpload(tempArray);
                            return baseResultBool;
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBSCMsg.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SCMsgByConfirm(SCMsg entity)
        {
            BaseResultDataValue brbv = new BaseResultDataValue();

            string EmpId = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
            string EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            if (string.IsNullOrEmpty(EmpId))
            {
                brbv.success = false;
                brbv.ErrorInfo = "无法获取登录者信息！";
                return brbv;
            }
            if (entity == null)
            {
                brbv.success = false;
                brbv.ErrorInfo = "错误信息：实体参数不能为空！";
                return brbv;
            }

            try
            {
                entity.ConfirmIPAddress = BusinessObject.Utils.IPHelper.GetClientIP();
              
                if (entity.MsgTypeCode == null)
                {
                    ZhiFang.Common.Log.Log.Debug("IMService.ST_UDTO_SCMsgByConfirm.消息类型为空！无法进行推送！");
                    brbv.ErrorInfo = "消息类型为空！无法进行推送！";
                    brbv.success = false;
                    return brbv;
                }
                else
                {
                    switch (entity.MsgTypeCode.ToUpper().Trim())
                    {
                        case "ZF_LAB_START_CV"://危急值
                            BaseResultDataValue brdv = IBSCMsg.SCMsgByConfirm(entity);
                            if (brdv.success)
                            {
                                SendMessagesByDeptId_CV(entity.SendDeptID.ToString(), entity.MsgContent, long.Parse(EmpId), entity.Id, ZFSCMsgType.LIS危急值.Value.Code, ZFSCMsgStatus_ZF_LAB_START_CV.危急值确认.Value.Code);
                                List<string> tmpempid = brdv.ResultDataValue.Split(',').ToList();

                                SendMessagesByEmpIdList_CV(tmpempid, entity.MsgContent, long.Parse(EmpId), entity.Id, ZFSCMsgType.LIS危急值.Value.Code, ZFSCMsgStatus_ZF_LAB_START_CV.危机值确认撤销.Value.Code);

                                //SendMessagesByDeptId_CV(entity.RecDeptID.ToString(), entity.MsgContent, long.Parse(Empid), entity.Id, ZFSCMsgType.LIS危急值.Value.Code, ZFSCMsgStatus_ZF_LAB_START_CV.危机值确认撤销.Value.Code);
                            }
                            break;
                        case "ZF_PRE_REJECTION"://LIS前处理拒收

                            break;
                        default:
                            break;
                    }
                }

                //brb = IBSCMsg.SCMsgByConfirm(entity);
                //if (brb.success)
                //{
                //    //通知发送人，消息已经被确认
                //    //SendMessagesByEmpId_CV(entity.SenderID.ToString(), entity.MsgContent, Empid, entity.Id.ToString(), ZFSCMsgType.危急值确认.Value.Code);



                //    //通知发送部门，消息已经被确认
                //    if (entity.SendDeptID.HasValue && entity.SendDeptID.Value > 0)
                //    {
                //        SendMessagesByDeptId_CV(entity.SendDeptID.ToString(), entity.MsgContent, long.Parse(Empid), entity.Id, ZFSCMsgType.LIS危急值.Value.Code, ZFSCMsgStatus_ZF_LAB_START_CV.危急值确认.Value.Code);

                //    }

                //    //通知接收者，消息已经被他人确认
                //    if (entity.RecDeptID > 0)
                //    {

                //        SendMessagesByDeptId_CV(entity.RecDeptID.ToString(), entity.MsgContent, long.Parse(Empid), entity.Id, ZFSCMsgType.LIS危急值.Value.Code, ZFSCMsgStatus_ZF_LAB_START_CV.危机值确认撤销.Value.Code);
                //    }
                //}
                return brbv;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SCMsgByConfirm.异常：" + ex.ToString());
                brbv.success = false;
                brbv.ErrorInfo = "错误信息：" + ex.Message;
                return brbv;
            }
        }

        public BaseResultDataValue ST_UDTO_SCMsgByConfirmNotify(SCMsg entity)
        {
            BaseResultDataValue brbv = new BaseResultDataValue();

            string EmpId = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
            string EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            if (string.IsNullOrEmpty(EmpId))
            {
                brbv.success = false;
                brbv.ErrorInfo = "无法获取登录者信息！IP:" + BusinessObject.Utils.IPHelper.GetClientIP();
                return brbv;
            }
            if (entity == null)
            {
                brbv.success = false;
                brbv.ErrorInfo = "错误信息：实体参数不能为空！";
                return brbv;
            }
            try
            {
                //entity.NotifyDoctorByEmpID = long.Parse(EmpId);
               // entity.NotifyDoctorByEmpName = EmpName;
                entity.NotifyDoctorLastDateTime = DateTime.Now;
                if (entity.MsgTypeCode == null)
                {
                    ZhiFang.Common.Log.Log.Debug("IMService.ST_UDTO_SCMsgByConfirmNotify.消息类型为空！无法进行推送！IP:" + BusinessObject.Utils.IPHelper.GetClientIP());
                    brbv.ErrorInfo = "消息类型为空！无法进行推送！";
                    brbv.success = false;
                    return brbv;
                }
                else
                {
                    switch (entity.MsgTypeCode.ToUpper().Trim())
                    {
                        case "ZF_LAB_START_CV"://危急值
                            BaseResultDataValue brdv = IBSCMsg.SCMsgByConfirmNotify(entity);
                            if (brdv.success)
                            {
                                List<string> tmpempid = brdv.ResultDataValue.Split(',').ToList();

                                SendMessagesByEmpIdList_CV(tmpempid, entity.MsgContent, long.Parse(EmpId), entity.Id, ZFSCMsgType.LIS危急值.Value.Code, ZFSCMsgStatus_ZF_LAB_START_CV.危机值确认撤销.Value.Code);

                                //SendMessagesByDeptId_CV(entity.RecDeptID.ToString(), entity.MsgContent, long.Parse(Empid), entity.Id, ZFSCMsgType.LIS危急值.Value.Code, ZFSCMsgStatus_ZF_LAB_START_CV.危机值确认撤销.Value.Code);
                            }
                            break;
                        case "ZF_PRE_REJECTION"://LIS前处理拒收

                            break;
                        default:
                            break;
                    }
                }
                return brbv;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SCMsgByConfirmNotify.异常：" + ex.ToString());
                brbv.success = false;
                brbv.ErrorInfo = "程序异常！";
                return brbv;
            }
        }

        public BaseResultDataValue ST_UDTO_SCMsgByTimeOutReSend(SCMsg entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (entity != null)
            {
                //IBSCMsg.Entity = entity;
                try
                {
                    entity.ConfirmIPAddress = BusinessObject.Utils.IPHelper.GetClientIP();
                    brdv = IBSCMsg.SCMsgByTimeOutReSend(entity);
                    if (brdv.success)
                    {
                        #region 暂时不用通知
                        //通知之前的部门。
                        //通知接收部门。

                        //通知发送部门，消息已经被确认
                        SendMessagesByDeptId_CV(entity.SendDeptID.ToString(), entity.MsgContent, entity.SenderID, entity.Id, ZFSCMsgType.LIS危急值.Value.Code, ZFSCMsgStatus_ZF_LAB_START_CV.危急值超时重发.Value.Code);

                        //通知接收者，消息已经被他人确认
                        SendMessagesByDeptId_CV(entity.RecDeptID.ToString(), entity.MsgContent, entity.SenderID, entity.Id, ZFSCMsgType.LIS危急值.Value.Code, ZFSCMsgStatus_ZF_LAB_START_CV.危急值超时重发.Value.Code);

                        #endregion
                    }
                    return brdv;
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SCMsgByTimeOutReSend.异常:" + ex.ToString());
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return brdv;
        }

        //Delele  SCMsg
        public BaseResultBool ST_UDTO_DelSCMsg(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            //try
            //{
            //    IBSCMsg.Entity = IBSCMsg.Get(id);
            //    if (IBSCMsg.Entity != null)
            //    {
            //        long labid = IBSCMsg.Entity.LabID;
            //        string entityName = IBSCMsg.Entity.GetType().Name;
            //        baseResultBool.success = IBSCMsg.RemoveByHQL(id);
            //        if (baseResultBool.success)
            //        {
            //            //IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
            //        }
            //    }
            //    else
            //    {
            //        baseResultBool.success = false;
            //        baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    baseResultBool.success = false;
            //    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
            //    //throw new Exception(ex.Message);
            //}
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchSCMsg(SCMsg entity)
        {
            EntityList<SCMsg> entityList = new EntityList<SCMsg>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBSCMsg.Entity = entity;
                try
                {
                    entityList.list = IBSCMsg.Search();
                    entityList.count = IBSCMsg.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<SCMsg>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchSCMsgByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityList<SCMsg> entityList = new EntityList<SCMsg>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBSCMsg.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBSCMsg.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        brdv.ResultDataValue = pop.GetObjectListPlanish<SCMsg>(entityList);
                    }
                    else
                    {
                        brdv.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                ZhiFang.Common.Log.Log.Error("IMService.ST_UDTO_SearchSCMsgByHQL.程序异常:" + ex.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue ST_UDTO_SearchSCMsgAndSCMsgHandleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<SCMsg> entityList = new EntityList<SCMsg>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBSCMsg.SearchSCMsgAndSCMsgHandleByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBSCMsg.SearchSCMsgAndSCMsgHandleByHQL(where, "",page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<SCMsg>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchSCMsgAndSCMsgHandleByHQL.序列化异常：" + ex.ToString());
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchSCMsgAndSCMsgHandleByHQL.异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchSCMsgByHQLAndLabCode(int page, int limit, string fields, string where, bool isPlanish)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityList<SCMsg> entityList = new EntityList<SCMsg>();

            try
            {
                string Empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                if (Empid == null || Empid.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "无法获取登录者信息！";
                    return brdv;
                }

                entityList = IBSCMsg.SearchSCMsgByHQLAndLabCode(where, page, limit, Empid);

                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        brdv.ResultDataValue = pop.GetObjectListPlanish<SCMsg>(entityList);
                    }
                    else
                    {
                        brdv.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                ZhiFang.Common.Log.Log.Error("IMService.ST_UDTO_SearchSCMsgByHQLAndLabCode.程序异常:" + ex.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue ST_UDTO_SearchSCMsgById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBSCMsg.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<SCMsg>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_BatchConfirmMsg(List<string> idlist)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string EmpId = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
            string EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            if (EmpId == null || EmpId.Trim() == "")
            {
                brdv.success = false;
                brdv.ErrorInfo = "无法获取登录者信息！";
                return brdv;
            }

            ZhiFang.Common.Log.Log.Debug("ST_UDTO_BatchConfirmMsg.idlist:" + idlist + ",EmpId:" + EmpId + ",EmpName:" + EmpName);

            try
            {
                string where = (idlist == null || idlist.Count <= 0) ? " " : " Id in (" + string.Join(",", idlist) + ") ";
                string ip = ZhiFang.LabInformationIntegratePlatform.BusinessObject.Utils.IPHelper.GetClientIP();
                brdv.ResultDataValue = IBSCMsg.BatchConfirmMsg(where, EmpId, ip);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                ZhiFang.Common.Log.Log.Error("ST_UDTO_BatchConfirmMsg.异常：" + ex.ToString());
            }
            return brdv;
        }
        #endregion

        #region SCMsgHandle_业务消息处理
        //Add  SCMsgHandle
        public BaseResultDataValue ST_UDTO_AddSCMsgHandle(SCMsgHandle entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string Empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
            if (Empid == null || Empid.Trim() == "")
            {
                brdv.success = false;
                brdv.ErrorInfo = "无法获取登录者信息！";
                return brdv;
            }
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                entity.HandleNodeIPAddress = ZhiFang.LabInformationIntegratePlatform.BusinessObject.Utils.IPHelper.GetClientIP();
                entity.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                IBSCMsgHandle.Entity = entity;
                try
                {
                    if (string.IsNullOrEmpty(entity.MsgTypeCode))
                    {
                        entity.MsgTypeCode = "";

                    }
                    switch (entity.MsgTypeCode.ToUpper().Trim())
                    {
                        case "ZF_LAB_START_CV"://危急值
                            brdv = IBSCMsgHandle.AddSCMsgHandle_ZF_LAB_START_CV(entity);
                            if (brdv.success)
                            {
                                List<string> tmpempid = brdv.ResultDataValue.Split(',').ToList();
                                //对于其他处理者进行撤销动作
                                SendMessagesByEmpIdList_CV(tmpempid, entity.HandleDesc, long.Parse(Empid), entity.MsgID, ZFSCMsgType.LIS危急值.Value.Code, ZFSCMsgStatus_ZF_LAB_START_CV.危机值处理撤销.Value.Code);

                                //通知发送者部门（小组）已经处理
                                if (entity.SCMsg != null && entity.SCMsg.SendSectionID.HasValue && entity.SCMsg.SendDeptID.HasValue && entity.SCMsg.SendDeptID.Value > 0)
                                {
                                    ZhiFang.Common.Log.Log.Debug($"ST_UDTO_AddSCMsgHandle.发送者小组ID:{entity.SCMsg.SendSectionID.Value}");
                                    SendMessagesByDeptId_CV(entity.SCMsg.SendDeptID.Value.ToString(), entity.HandleDesc, long.Parse(Empid), entity.MsgID, ZFSCMsgType.LIS危急值.Value.Code, ZFSCMsgStatus_ZF_LAB_START_CV.危急值处理.Value.Code);

                                }
                                else
                                {
                                    ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddSCMsgHandle.未能找到发送者小组ID！");
                                }

                                brdv.ResultDataValue = "{\"SCMsgID\":\"" + entity.SCMsg.Id + "\",\"SCMsgHandleID\":\"" + entity.Id + "\"}";

                                //调用接口
                                brdv = SC_MSGHandleToHISInterface(brdv, entity.SCMsg.Id, entity.Id, entity.HandlerPWD);
                                return brdv;
                            }
                            else
                            {
                                brdv.success = false;
                                brdv.ErrorInfo = brdv.ErrorInfo + "错误信息：新增处理失败！";
                                brdv.ResultCode = "-1";
                                return brdv;
                            }
                            break;
                        case "ZF_PRE_REJECTION"://LIS前处理拒收

                            break;
                        default:
                            brdv = IBSCMsgHandle.Add(entity);
                            if (brdv.success)
                            {
                                brdv.ResultDataValue = "{\"SCMsgID\":\"" + entity.SCMsg.Id + "\",\"SCMsgHandleID\":\"" + entity.Id + "\"}";
                                //通知发送者已经处理
                                //SendMessagesByEmpId_CV(brdv.ResultDataValue.ToString(), entity.HandleDesc, Empid, entity.MsgID.ToString(), ZFSCMsgType.危急值处理.Value.Code);

                                //通知发送者部门（小组）已经处理
                                if (entity.SCMsg != null && entity.SCMsg.SendSectionID.HasValue && entity.SCMsg.SendDeptID.HasValue && entity.SCMsg.SendDeptID.Value > 0)
                                {
                                    ZhiFang.Common.Log.Log.Debug($"ST_UDTO_AddSCMsgHandle.发送者小组ID:{entity.SCMsg.SendSectionID.Value}");
                                    SendMessagesByDeptId_CV(entity.SCMsg.SendDeptID.Value.ToString(), entity.HandleDesc, long.Parse(Empid), entity.MsgID, ZFSCMsgType.LIS危急值.Value.Code, ZFSCMsgStatus_ZF_LAB_START_CV.危急值处理.Value.Code);

                                }
                                else
                                {
                                    ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddSCMsgHandle.未能找到发送者小组ID！");
                                }

                                //对于其他处理者进行撤销动作
                                SendMessagesByDeptId_CV(entity.HandleDeptID.ToString(), entity.HandleDesc, long.Parse(Empid), entity.MsgID, ZFSCMsgType.LIS危急值.Value.Code, ZFSCMsgStatus_ZF_LAB_START_CV.危机值处理撤销.Value.Code);

                                //调用接口
                                brdv = SC_MSGHandleToHISInterface(brdv, entity.SCMsg.Id, entity.Id, entity.HandlerPWD);
                                return brdv;
                            }
                            else
                            {
                                brdv.success = false;
                                brdv.ErrorInfo = brdv.ErrorInfo + "错误信息：新增处理失败！";
                                brdv.ResultCode = "-1";
                                return brdv;
                            }
                            break;
                    }

                }
                catch (Exception ex)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：" + ex.Message;
                    brdv.ResultCode = "0";
                    ZhiFang.Common.Log.Log.Error($"ST_UDTO_AddSCMsgHandle.异常:{ex.ToString()}");
                    return brdv;
                }
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：实体参数不能为空！";
                brdv.ResultCode = "0";
            }
            return brdv;
        }
        //Update  SCMsgHandle
        public BaseResultBool ST_UDTO_UpdateSCMsgHandle(SCMsgHandle entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            //if (entity != null)
            //{
            //    entity.DataUpdateTime = DateTime.Now;
            //    IBSCMsgHandle.Entity = entity;
            //    try
            //    {
            //        baseResultBool.success = IBSCMsgHandle.Edit();
            //        if (baseResultBool.success)
            //        {
            //            //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        baseResultBool.success = false;
            //        baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
            //        //throw new Exception(ex.Message);
            //    }
            //}
            //else
            //{
            //    baseResultBool.success = false;
            //    baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            //}
            return baseResultBool;
        }
        //Update  SCMsgHandle
        public BaseResultBool ST_UDTO_UpdateSCMsgHandleByField(SCMsgHandle entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            //if (entity != null)
            //{
            //    if (!string.IsNullOrEmpty(fields))
            //        fields = fields + ",DataUpdateTime";
            //    entity.DataUpdateTime = DateTime.Now;
            //    IBSCMsgHandle.Entity = entity;
            //    try
            //    {
            //        if ((fields != null) && (fields.Length > 0))
            //        {
            //            string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCMsgHandle.Entity, fields);
            //            if (tempArray != null)
            //            {
            //                baseResultBool.success = IBSCMsgHandle.Update(tempArray);
            //                if (baseResultBool.success)
            //                {
            //                    //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
            //                }
            //            }
            //        }
            //        else
            //        {
            //            baseResultBool.success = false;
            //            baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
            //            //baseResultBool.success = IBSCMsgHandle.Edit();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        baseResultBool.success = false;
            //        baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
            //        //throw new Exception(ex.Message);
            //    }
            //}
            //else
            //{
            //    baseResultBool.success = false;
            //    baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            //}
            return baseResultBool;
        }
        //Delele  SCMsgHandle
        public BaseResultBool ST_UDTO_DelSCMsgHandle(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            //try
            //{
            //    IBSCMsgHandle.Entity = IBSCMsgHandle.Get(id);
            //    if (IBSCMsgHandle.Entity != null)
            //    {
            //        long labid = IBSCMsgHandle.Entity.LabID;
            //        string entityName = IBSCMsgHandle.Entity.GetType().Name;
            //        baseResultBool.success = IBSCMsgHandle.RemoveByHQL(id);
            //        if (baseResultBool.success)
            //        {
            //            //IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
            //        }
            //    }
            //    else
            //    {
            //        baseResultBool.success = false;
            //        baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    baseResultBool.success = false;
            //    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
            //    //throw new Exception(ex.Message);
            //}
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchSCMsgHandle(SCMsgHandle entity)
        {
            EntityList<SCMsgHandle> entityList = new EntityList<SCMsgHandle>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBSCMsgHandle.Entity = entity;
                try
                {
                    entityList.list = IBSCMsgHandle.Search();
                    entityList.count = IBSCMsgHandle.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<SCMsgHandle>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchSCMsgHandleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<SCMsgHandle> entityList = new EntityList<SCMsgHandle>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBSCMsgHandle.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBSCMsgHandle.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<SCMsgHandle>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchSCMsgHandleById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBSCMsgHandle.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<SCMsgHandle>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region SCMsgPhraseDic_业务消息短语
        //Add  SCMsgPhraseDic
        public BaseResultDataValue ST_UDTO_AddSCMsgPhraseDic(SCMsgPhraseDic entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBSCMsgPhraseDic.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBSCMsgPhraseDic.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  SCMsgPhraseDic
        public BaseResultBool ST_UDTO_UpdateSCMsgPhraseDic(SCMsgPhraseDic entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBSCMsgPhraseDic.Entity = entity;
                try
                {
                    baseResultBool.success = IBSCMsgPhraseDic.Edit();
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  SCMsgPhraseDic
        public BaseResultBool ST_UDTO_UpdateSCMsgPhraseDicByField(SCMsgPhraseDic entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBSCMsgPhraseDic.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCMsgPhraseDic.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBSCMsgPhraseDic.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBSCMsgPhraseDic.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  SCMsgPhraseDic
        public BaseResultBool ST_UDTO_DelSCMsgPhraseDic(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBSCMsgPhraseDic.Entity = IBSCMsgPhraseDic.Get(id);
                if (IBSCMsgPhraseDic.Entity != null)
                {
                    long labid = IBSCMsgPhraseDic.Entity.LabID;
                    string entityName = IBSCMsgPhraseDic.Entity.GetType().Name;
                    baseResultBool.success = IBSCMsgPhraseDic.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
                    }
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchSCMsgPhraseDic(SCMsgPhraseDic entity)
        {
            EntityList<SCMsgPhraseDic> entityList = new EntityList<SCMsgPhraseDic>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBSCMsgPhraseDic.Entity = entity;
                try
                {
                    entityList.list = IBSCMsgPhraseDic.Search();
                    entityList.count = IBSCMsgPhraseDic.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<SCMsgPhraseDic>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchSCMsgPhraseDicByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<SCMsgPhraseDic> entityList = new EntityList<SCMsgPhraseDic>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBSCMsgPhraseDic.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBSCMsgPhraseDic.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<SCMsgPhraseDic>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchSCMsgPhraseDicById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBSCMsgPhraseDic.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<SCMsgPhraseDic>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region SCMsgType_业务消息类型
        //Add  SCMsgType
        public BaseResultDataValue ST_UDTO_AddSCMsgType(SCMsgType entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBSCMsgType.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBSCMsgType.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  SCMsgType
        public BaseResultBool ST_UDTO_UpdateSCMsgType(SCMsgType entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBSCMsgType.Entity = entity;
                try
                {
                    baseResultBool.success = IBSCMsgType.Edit();
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  SCMsgType
        public BaseResultBool ST_UDTO_UpdateSCMsgTypeByField(SCMsgType entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBSCMsgType.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCMsgType.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBSCMsgType.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBSCMsgType.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  SCMsgType
        public BaseResultBool ST_UDTO_DelSCMsgType(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBSCMsgType.Entity = IBSCMsgType.Get(id);
                if (IBSCMsgType.Entity != null)
                {
                    long labid = IBSCMsgType.Entity.LabID;
                    string entityName = IBSCMsgType.Entity.GetType().Name;
                    baseResultBool.success = IBSCMsgType.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
                    }
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchSCMsgType(SCMsgType entity)
        {
            EntityList<SCMsgType> entityList = new EntityList<SCMsgType>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBSCMsgType.Entity = entity;
                try
                {
                    entityList.list = IBSCMsgType.Search();
                    entityList.count = IBSCMsgType.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<SCMsgType>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchSCMsgTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<SCMsgType> entityList = new EntityList<SCMsgType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBSCMsgType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBSCMsgType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<SCMsgType>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchSCMsgTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBSCMsgType.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<SCMsgType>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region CVCriticalValueEmpIdDeptLink
        //Add  CVCriticalValueEmpIdDeptLink
        public BaseResultDataValue ST_UDTO_AddCVCriticalValueEmpIdDeptLink(CVCriticalValueEmpIdDeptLink entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBCVCriticalValueEmpIdDeptLink.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBCVCriticalValueEmpIdDeptLink.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  CVCriticalValueEmpIdDeptLink
        public BaseResultBool ST_UDTO_UpdateCVCriticalValueEmpIdDeptLink(CVCriticalValueEmpIdDeptLink entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBCVCriticalValueEmpIdDeptLink.Entity = entity;
                try
                {
                    baseResultBool.success = IBCVCriticalValueEmpIdDeptLink.Edit();
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  CVCriticalValueEmpIdDeptLink
        public BaseResultBool ST_UDTO_UpdateCVCriticalValueEmpIdDeptLinkByField(CVCriticalValueEmpIdDeptLink entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBCVCriticalValueEmpIdDeptLink.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBCVCriticalValueEmpIdDeptLink.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBCVCriticalValueEmpIdDeptLink.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBCVCriticalValueEmpIdDeptLink.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  CVCriticalValueEmpIdDeptLink
        public BaseResultBool ST_UDTO_DelCVCriticalValueEmpIdDeptLink(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBCVCriticalValueEmpIdDeptLink.Entity = IBCVCriticalValueEmpIdDeptLink.Get(id);
                if (IBCVCriticalValueEmpIdDeptLink.Entity != null)
                {
                    long labid = IBCVCriticalValueEmpIdDeptLink.Entity.LabID;
                    string entityName = IBCVCriticalValueEmpIdDeptLink.Entity.GetType().Name;
                    baseResultBool.success = IBCVCriticalValueEmpIdDeptLink.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
                    }
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchCVCriticalValueEmpIdDeptLink(CVCriticalValueEmpIdDeptLink entity)
        {
            EntityList<CVCriticalValueEmpIdDeptLink> entityList = new EntityList<CVCriticalValueEmpIdDeptLink>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBCVCriticalValueEmpIdDeptLink.Entity = entity;
                try
                {
                    entityList.list = IBCVCriticalValueEmpIdDeptLink.Search();
                    entityList.count = IBCVCriticalValueEmpIdDeptLink.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<CVCriticalValueEmpIdDeptLink>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchCVCriticalValueEmpIdDeptLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<CVCriticalValueEmpIdDeptLink> entityList = new EntityList<CVCriticalValueEmpIdDeptLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBCVCriticalValueEmpIdDeptLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBCVCriticalValueEmpIdDeptLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<CVCriticalValueEmpIdDeptLink>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchCVCriticalValueEmpIdDeptLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBCVCriticalValueEmpIdDeptLink.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<CVCriticalValueEmpIdDeptLink>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        public string SendMessagesByDeptId_CV(string ToDeptId, string Message, long FormUserEmpId, long SCMsgId, string SCMsgTypeCode, string SCMsgTypeCodeStatus)
        {
            try
            {
                if (FormUserEmpId <= 0)
                {
                    return "发送者ID为空！";
                }
                string FormUserEmpName = "";
                if (MainMessageHub.ClientList.ClientStationList.Count(a => a.EmpId == FormUserEmpId.ToString()) > 0)
                {
                    FormUserEmpName = MainMessageHub.ClientList.ClientStationList.Where(a => a.EmpId.ToString() == FormUserEmpId.ToString()).ElementAt(0).EmpName;
                }
                return SendMessagesByDeptId_CV(ToDeptId, Message, FormUserEmpId, FormUserEmpName, SCMsgId, SCMsgTypeCode, SCMsgTypeCodeStatus);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("IMService.SendMessagesByDeptId_CV.异常：" + e.ToString());
                return "IMService.SendMessagesByDeptId_CV.异常：" + e.ToString();
            }
        }

        public string SendMessagesByEmpId_CV(string ToEmpId, string Message, long FormUserEmpId, long SCMsgId, string SCMsgTypeCode, string SCMsgTypeCodeStatus)
        {
            try
            {
                if (FormUserEmpId <= 0)
                {
                    return "发送者ID为空！";
                }
                string FormUserEmpName = "";
                if (MainMessageHub.ClientList.ClientStationList.Count(a => a.EmpId.ToString() == FormUserEmpId.ToString()) > 0)
                {
                    FormUserEmpName = MainMessageHub.ClientList.ClientStationList.Where(a => a.EmpId.ToString() == FormUserEmpId.ToString()).ElementAt(0).EmpName;
                }

                if (MainMessageHub.ClientList.ClientStationList.Count(a => a.EmpId == ToEmpId) > 0)
                {
                    List<string> cidlist = MainMessageHub.ClientList.ClientStationList.Where(a => a.EmpId == ToEmpId).ElementAt(0).ConnectionIdList;
                    foreach (var c in cidlist)
                    {
                        MainMessageHub.clist.Client(c).ReceiveMessageByEmpId_CV(FormUserEmpId.ToString(), FormUserEmpName, Message, SCMsgId.ToString(), SCMsgTypeCode);
                        ZhiFang.Common.Log.Log.Debug("IMService.SendMessagesByEmpId_CV.ConnectionId:" + c + ";FormUserEmpId：" + FormUserEmpId + ";FormUserEmpName:" + FormUserEmpName + ";Message:" + Message + ";SCMsgId:" + SCMsgId + ";SCMsgTypeCode:" + SCMsgTypeCode);
                        MainMessageHub.clist.Client(c).ReceiveMessageByEmpId(FormUserEmpId.ToString(), FormUserEmpName, Message, SCMsgId.ToString(), SCMsgTypeCode, SCMsgTypeCodeStatus);
                        ZhiFang.Common.Log.Log.Debug("IMService.SendMessagesByEmpId_CV.ConnectionId:" + c + ";FormUserEmpId：" + FormUserEmpId + ";FormUserEmpName:" + FormUserEmpName + ";Message:" + Message + ";SCMsgId:" + SCMsgId + ";SCMsgTypeCode:" + SCMsgTypeCode + ";SCMsgTypeCodeStatus:" + SCMsgTypeCodeStatus);
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("IMService.SendMessagesByEmpId_CV.用户:ToEmpId=" + ToEmpId + "不在线！");
                }

                return "发送完成！";
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("IMService.SendMessagesByEmpId_CV.异常：" + e.ToString());
                return "IMService.SendMessagesByEmpId_CV.异常：" + e.ToString();
            }
        }

        public string SendMessagesByDeptId_CV(string ToDeptId, string Message, long FormUserEmpId, string FormUserEmpName, long SCMsgId, string SCMsgTypeCode, string SCMsgTypeCodeStatus)
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

                if (FormUserEmpId <= 0)
                {
                    return "发送者ID为空！";
                }
                //if (FormUserEmpName == null || FormUserEmpName.Trim().Length <= 0)
                //{
                //    return "发送者名称为空！";
                //}

                ZhiFang.Common.Log.Log.Debug("IMService.SendMessagesByDeptId_CV.ToDeptId：" + string.Join(",", ToDeptId) + ";Message:" + Message + ";FormUserEmpId:" + FormUserEmpId);
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.IBCVCriticalValueEmpIdDeptLink IBCVCriticalValueEmpIdDeptLink = (IBLL.LIIP.IBCVCriticalValueEmpIdDeptLink)context.GetObject("BCVCriticalValueEmpIdDeptLink");
                var tmplist = IBCVCriticalValueEmpIdDeptLink.SearchListByHQL(" DeptId= " + ToDeptId);
                if (tmplist != null && tmplist.Count() > 0)
                {
                    var OnlineUserList = MainMessageHub.ClientList.ClientStationList.Where(a => a.ClientStationTypeId == 1 && a.StatusTypeId == 1);
                    ZhiFang.Common.Log.Log.Debug("IMService.SendMessagesByDeptId_CV.在线人员列表：" + ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(OnlineUserList));
                    for (int i = 0; i < tmplist.Count(); i++)
                    {
                        if (OnlineUserList.Count(a => a.EmpId == tmplist[i].EmpID.ToString()) > 0)
                        {
                            List<string> cidlist = OnlineUserList.Where(a => a.EmpId == tmplist[i].EmpID.ToString()).ElementAt(0).ConnectionIdList;
                            foreach (var c in cidlist)
                            {
                                MainMessageHub.clist.Client(c).ReceiveMessageByEmpId_CV(FormUserEmpId.ToString(), FormUserEmpName, Message, SCMsgId.ToString(), SCMsgTypeCode);
                                ZhiFang.Common.Log.Log.Debug("IMService.SendMessagesByDeptId_CV.ConnectionId:" + c + ";FormUserEmpId：" + FormUserEmpId + ";FormUserEmpName:" + FormUserEmpName + ";Message:" + Message + ";SCMsgId:" + SCMsgId + ";SCMsgTypeCode:" + SCMsgTypeCode);
                                MainMessageHub.clist.Client(c).ReceiveMessageByEmpId(FormUserEmpId.ToString(), FormUserEmpName, Message, SCMsgId.ToString(), SCMsgTypeCode, SCMsgTypeCodeStatus);
                                ZhiFang.Common.Log.Log.Debug("IMService.SendMessagesByDeptId_CV.ConnectionId:" + c + ";FormUserEmpId：" + FormUserEmpId + ";FormUserEmpName:" + FormUserEmpName + ";Message:" + Message + ";SCMsgId:" + SCMsgId + ";SCMsgTypeCode:" + SCMsgTypeCode + ";SCMsgTypeCodeStatus:" + SCMsgTypeCodeStatus);
                            }
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Error("IMService.SendMessagesByDeptId_CV.部门ID: " + ToDeptId + "的科室的用户:EmpId=" + tmplist[i].EmpID.ToString() + "不在线！");
                        }
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("IMService.SendMessagesByDeptId_CV.部门ID: " + ToDeptId + "的科室没有可以通知的用户！");
                    return "部门ID:" + ToDeptId + "的科室没有可以通知的用户！";
                }

                return "发送完成！";
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("IMService.SendMessagesByDeptId_CV.异常：" + e.ToString());
                return "IMService.SendMessagesByDeptId_CV.异常：" + e.ToString();
            }
        }


        public string SendMessagesByEmpIdList_CV(List<string> ToEmpIdList, string Message, long FormUserEmpId, long SCMsgId, string SCMsgTypeCode, string SCMsgTypeCodeStatus)
        {
            try
            {
                if (FormUserEmpId <= 0)
                {
                    return "发送者ID为空！";
                }
                string FormUserEmpName = "";
                if (MainMessageHub.ClientList.ClientStationList.Count(a => a.EmpId == FormUserEmpId.ToString()) > 0)
                {
                    FormUserEmpName = MainMessageHub.ClientList.ClientStationList.Where(a => a.EmpId.ToString() == FormUserEmpId.ToString()).ElementAt(0).EmpName;
                }
                return SendMessagesByEmpIdList_CV(ToEmpIdList, Message, FormUserEmpId, FormUserEmpName, SCMsgId, SCMsgTypeCode, SCMsgTypeCodeStatus);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("IMService.SendMessagesByEmpIdList_CV.异常：" + e.ToString());
                return "IMService.SendMessagesByEmpIdList_CV.异常：" + e.ToString();
            }
        }


        public string SendMessagesByEmpIdList_CV(List<string> ToEmpIdList, string Message, long FormUserEmpId, string FormUserEmpName, long SCMsgId, string SCMsgTypeCode, string SCMsgTypeCodeStatus)
        {
            try
            {
                if (ToEmpIdList == null || ToEmpIdList.Count <= 0)
                {
                    return "员工ID为空！";
                }

                if (Message == null || Message.Trim().Length <= 0)
                {
                    return "消息体为空！";
                }

                if (FormUserEmpId <= 0)
                {
                    return "发送者ID为空！";
                }
                //if (FormUserEmpName == null || FormUserEmpName.Trim().Length <= 0)
                //{
                //    return "发送者名称为空！";
                //}

                ZhiFang.Common.Log.Log.Debug("IMService.SendMessagesByEmpIdList_CV.ToEmpIdList：" + string.Join(",", ToEmpIdList) + ";Message:" + Message + ";FormUserEmpId:" + FormUserEmpId);
                if (ToEmpIdList != null && ToEmpIdList.Count() > 0)
                {
                    var OnlineUserList = MainMessageHub.ClientList.ClientStationList.Where(a => a.ClientStationTypeId == 1 && a.StatusTypeId == 1);
                    ZhiFang.Common.Log.Log.Debug("IMService.SendMessagesByEmpIdList_CV.在线人员列表：" + ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(OnlineUserList));
                    for (int i = 0; i < ToEmpIdList.Count(); i++)
                    {
                        if (OnlineUserList.Count(a => a.EmpId == ToEmpIdList[i].ToString()) > 0)
                        {
                            List<string> cidlist = OnlineUserList.Where(a => a.EmpId == ToEmpIdList[i].ToString()).ElementAt(0).ConnectionIdList;
                            foreach (var c in cidlist)
                            {
                                MainMessageHub.clist.Client(c).ReceiveMessageByEmpId_CV(FormUserEmpId.ToString(), FormUserEmpName, Message, SCMsgId.ToString(), SCMsgTypeCode);
                                ZhiFang.Common.Log.Log.Debug("IMService.SendMessagesByEmpIdList_CV.ConnectionId:" + c + ";ToUserEmpId："+ ToEmpIdList[i].ToString() + ";FormUserEmpId：" + FormUserEmpId + ";FormUserEmpName:" + FormUserEmpName + ";Message:" + Message + ";SCMsgId:" + SCMsgId + ";SCMsgTypeCode:" + SCMsgTypeCode);
                                MainMessageHub.clist.Client(c).ReceiveMessageByEmpId(FormUserEmpId.ToString(), FormUserEmpName, Message, SCMsgId.ToString(), SCMsgTypeCode, SCMsgTypeCodeStatus);
                                ZhiFang.Common.Log.Log.Debug("IMService.SendMessagesByEmpIdList_CV.ConnectionId:" + c + ";ToUserEmpId：" + ToEmpIdList[i].ToString() + ";FormUserEmpId：" + FormUserEmpId + ";FormUserEmpName:" + FormUserEmpName + ";Message:" + Message + ";SCMsgId:" + SCMsgId + ";SCMsgTypeCode:" + SCMsgTypeCode + ";SCMsgTypeCodeStatus:" + SCMsgTypeCodeStatus);
                            }
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Error("IMService.SendMessagesByEmpIdList_CV.用户:EmpId=" + ToEmpIdList[i].ToString() + "不在线！");
                        }
                    }
                }

                return "发送完成！";
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("IMService.SendMessagesByEmpIdList_CV.异常：" + e.ToString());
                return "IMService.SendMessagesByEmpIdList_CV.异常：" + e.ToString();
            }
        }

        public string SendMessagesByEmpId_IM(List<string> ToEmpId, string Message, long FormUserEmpId, string FormUserEmpName, long SCMsgId, string SCMsgTypeCodeStatus)
        {
            try
            {
                if (ToEmpId == null || ToEmpId.Count <= 0)
                {
                    return "接收者ID为空！";
                }

                if (Message == null || Message.Trim().Length <= 0)
                {
                    return "消息体为空！";
                }

                if (FormUserEmpId <= 0)
                {
                    return "发送者ID为空！";
                }
                //if (FormUserEmpName == null || FormUserEmpName.Trim().Length <= 0)
                //{
                //    return "发送者名称为空！";
                //}

                ZhiFang.Common.Log.Log.Debug("IMService.SendMessagesByEmpId_IM.ToEmpId：" + string.Join(",", ToEmpId) + ";Message:" + Message + ";FormUserEmpId:" + FormUserEmpId);

                for (int i = 0; i < ToEmpId.Count(); i++)
                {
                    if (MainMessageHub.ClientList.ClientStationList.Count(a => a.EmpId == ToEmpId[i].Trim()) > 0)
                    {
                        List<string> cidlist = MainMessageHub.ClientList.ClientStationList.Where(a => a.EmpId == ToEmpId[i].Trim()).ElementAt(0).ConnectionIdList;
                        foreach (var c in cidlist)
                        {
                            MainMessageHub.clist.Client(c).ReceiveMessageByEmpId(FormUserEmpId, FormUserEmpName, Message, SCMsgId, ZFSCMsgType.LIS平台即时通讯系统消息.Value.Code, SCMsgTypeCodeStatus);
                            ZhiFang.Common.Log.Log.Debug("IMService.SendMessagesByEmpId_IM.ConnectionId:" + c + ";FormUserEmpId：" + FormUserEmpId + ";FormUserEmpName:" + FormUserEmpName + ";Message:" + Message + ";SCMsgId:" + SCMsgId + ";SCMsgTypeCodeStatus:" + SCMsgTypeCodeStatus);
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Error("IMService.SendMessagesByEmpId_IM.EmpId: " + ToEmpId[i].Trim() + "的用户不在线！");
                    }
                }

                return "发送完成！";
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("IMService.SendMessagesByEmpId_IM.异常：" + e.ToString());
                return "IMService.SendMessagesByEmpId_IM.异常：" + e.ToString();
            }
        }

        public string GetOnlineUserList(int flag)
        {
            try
            {
                var OnlineUserList = MainMessageHub.ClientList.ClientStationList.Where(a => a.ClientStationTypeId == flag && a.StatusTypeId == 1);

                //IApplicationContext context = ContextRegistry.GetContext();
                //object clientlist = context.GetObject("SignaIRClientList");
                //var ClientList = (SignaIRClientList)clientlist;
                //var OnlineUserList = ClientList.ClientList.Where(a => a.ClientStationTypeId == flag && a.StatusTypeId == 1);

                return ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(OnlineUserList)+"@"+ HttpContext.Current.Application["aaa"];
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("IMService:ZhiFang.LabInformationIntegratePlatform.ServerContract.IIMService.GetOnlineUserList,异常：" + e.ToString());
                return "获取在线人员列表异常！";
            }
        }

        public BaseResultDataValue GetUserMsgByPWD(string Account, string PWD, bool flag)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityList<SCMsg> entityList = new EntityList<SCMsg>();

            try
            {
                if (Account == null || Account.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "参数不合法！";
                    return brdv;
                }

                if (PWD == null || PWD.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "参数不合法！";
                    return brdv;
                }
                string PWDbase = PWD;//ZhiFang.Common.Public.Base64Help.DecodingString(PWD);
                ZhiFang.Common.Log.Log.Debug("GetUserMsgByPWD.Account:" + Account + ",PWD:" + PWD + ",PWDbase:" + PWDbase + ",IP:" + BusinessObject.Utils.IPHelper.GetClientIP() + ",flag:" + flag);
                string where = "1=1";
                if (!flag)
                {
                    where += " and (ConfirmFlag=null or  ConfirmFlag=0) ";
                }
                entityList = IBSCMsg.SearchSCMsgByHQLAndAccountPWD(where, 1, 10000, Account, PWDbase);
                brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                ZhiFang.Common.Log.Log.Error("GetUserMsgByPWD.异常：" + ex.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue GetLabMsgByLabCodeList(string LabCodeList, bool flag)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityList<SCMsg> entityList = new EntityList<SCMsg>();

            try
            {
                if (LabCodeList == null || LabCodeList.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "参数不合法！";
                    return brdv;
                }


                ZhiFang.Common.Log.Log.Debug("GetLabMsgByLabCodeList.LabCodeList:" + LabCodeList + ",IP:" + BusinessObject.Utils.IPHelper.GetClientIP() + ",flag:" + flag);
                string where = "1=1";
                if (!flag)
                {
                    where += " and (ConfirmFlag=null or  ConfirmFlag=0) ";
                }
                entityList = IBSCMsg.SearchLabMsgByLabCodeList(where, 1, 10000, LabCodeList);
                brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                ZhiFang.Common.Log.Log.Error("GetUserMsgByPWD.异常：" + ex.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue ST_UDTO_ReSendSCMsgHandleToHISInterface(long SCMsgID, long SCMsgHandleID, string PWD)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string Empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
            if (Empid == null || Empid.Trim() == "")
            {
                brdv.success = false;
                brdv.ErrorInfo = "无法获取登录者信息！";
                return brdv;
            }
            ZhiFang.LabInformationIntegratePlatform.BusinessObject.Utils.IPHelper.GetClientIP();
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_ReSendSCMsgHandleToHISInterface.调用：Empid：" + Empid + ",SCMsgID:" + SCMsgID + ",SCMsgHandleID:" + SCMsgHandleID + ",PWD:" + PWD?.ToString() + ",IP:" + BusinessObject.Utils.IPHelper.GetClientIP());
            brdv.ResultDataValue = "{\"SCMsgID\":\"" + SCMsgID + "\",\"SCMsgHandleID\":\"" + SCMsgHandleID + "\"}";
            brdv = SC_MSGHandleToHISInterface(brdv, SCMsgID, SCMsgHandleID, PWD);
            return brdv;
        }

        protected BaseResultDataValue SC_MSGHandleToHISInterface(BaseResultDataValue brdv, long SCMsgID, long SCMsgHandleID, string PWD)
        {
            var bp = IBBParameter.GetParameterByParaNo(SystemParameter_LIIP.危急值消息处理是否调用接口服务.Value.Code);
            var urlkey = ConfigHelper.GetConfigString(Entity.LIIP.SystemParameter_LIIP.HIS接口地址.Value.Code);
            if (bp != null && bp.ParaValue != null && bp.ParaValue != "" && bp.ParaValue.Trim() == "1" && urlkey != null && urlkey.Trim() != "")
            {
                try
                {
                    HISInterFaceVO hisinterfacevo = new HISInterFaceVO();
                    hisinterfacevo.action = "CVResult";
                    hisinterfacevo.data = "{\"SCMsgID\":\"" + SCMsgID + "\",\"SCMsgHandleID\":\"" + SCMsgHandleID + "\",\"HandlerPWD\":\"" + PWD + "\"}";
                    ZhiFang.Common.Log.Log.Debug("SC_MSGHandleToHISInterface.hisinterfacevo:" + ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(hisinterfacevo));
                    var resultstr = ZhiFang.LIIP.Common.RestfullHelper.InvkerRestServicePost(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(hisinterfacevo), "TEXT", urlkey, 10);
                    ZhiFang.Common.Log.Log.Debug("SC_MSGHandleToHISInterface.调用接口返回值:" + resultstr);
                    var resultroot = JsonConvert.DeserializeObject(resultstr) as JObject;
                    if (resultroot["success"].ToString().Trim().ToUpper() == "TRUE")
                    {
                        brdv.success = true;
                        brdv.ErrorInfo = "";
                        brdv.ResultCode = "1";
                        return brdv;
                    }
                    else
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "错误信息！调用HIS接口失败！";
                        brdv.ResultCode = "-2";
                        return brdv;
                    }
                }
                catch (Exception e)
                {
                    ZhiFang.Common.Log.Log.Debug("SC_MSGHandleToHISInterface.调用接口异常:" + e.ToString());
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息！调用HIS接口失败！";
                    brdv.ResultCode = "-2";
                    return brdv;
                }
            }
            return brdv;
        }

       


    }
}
