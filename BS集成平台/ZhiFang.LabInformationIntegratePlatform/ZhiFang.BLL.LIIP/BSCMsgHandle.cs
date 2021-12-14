using System;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.LIIP;
using ZhiFang.Entity.LIIP;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP.ViewObject.Request;
using System.Collections.Generic;

namespace ZhiFang.BLL.LIIP
{
    /// <summary>
    ///
    /// </summary>
    public class BSCMsgHandle : BaseBLL<SCMsgHandle>, ZhiFang.IBLL.LIIP.IBSCMsgHandle
    {
        IDSCMsgDao IDSCMsgDao { get; set; }
        IDCVCriticalValueEmpIdDeptLinkDao IDCVCriticalValueEmpIdDeptLinkDao { get; set; }
        ZhiFang.IDAO.RBAC.IDHREmployeeDao IDHREmployeeDao { get; set; }
        ZhiFang.IDAO.RBAC.IDRBACEmpRolesDao IDRBACEmpRolesDao { get; set; }
        public BaseResultDataValue Add(SCMsgHandle entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            if (entity.MsgID <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("BSCMsgHandle.Add.未能找到相关的系统消息！MsgID为空！");
                brdv.ErrorInfo = "系统消息参数错误！请检查！";
                brdv.success = false;
            }
            var scmsg = IDSCMsgDao.Get(entity.MsgID);
            if (scmsg == null)
            {
                ZhiFang.Common.Log.Log.Debug("BSCMsgHandle.Add.未能找到相关的系统消息！MsgID:" + entity.MsgID);
                brdv.ErrorInfo = "未能找到相关的系统消息！";
                brdv.success = false;
            }
            entity.SCMsg = scmsg;
            //var sendemp = IDHREmployeeDao.Get(scmsg.SenderID);
            //if (sendemp != null)
            //{
            //    entity.SCMsg.SendDeptID = sendemp.HRDept.Id;
            //    entity.SCMsg.SendDeptName = sendemp.HRDept.CName;
            //    entity.SCMsg.SendDeptCode = sendemp.HRDept.StandCode;//LIS编码
            //}
            //else
            //{
            //    //brdv.ErrorInfo = "未能找到发送者所属科室！SenderID=" + scmsg.SenderID;
            //    //brdv.success = false;
            //    ZhiFang.Common.Log.Log.Error(" BSCMsgHandle.Add.entity.未能找到发送者所属科室！SenderID = " + scmsg.SenderID);
            //    //return brdv;
            //}

            //判断确认人和部门是否有关系
            if (entity.HandlerID <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("BSCMsgHandle.Add.消息处理人不能为空！");
                brdv.ErrorInfo = "消息处理人不能为空！";
                brdv.success = false;
                return brdv;
            }

            if (entity.HandlerName == null || entity.HandlerName.Trim() == "")
            {
                ZhiFang.Common.Log.Log.Debug("BSCMsgHandle.Add.消息处理人姓名不能为空！");
                brdv.ErrorInfo = "消息处理人姓名不能为空！";
                brdv.success = false;
                return brdv;
            }

            if (entity.HandleDesc == null || entity.HandleDesc.Trim() == "")
            {
                ZhiFang.Common.Log.Log.Debug("BSCMsgHandle.Add.消息处理意见不能为空！");
                brdv.ErrorInfo = "消息处理意见不能为空！";
                brdv.success = false;
                return brdv;
            }
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZF_LIIP_CheckMsgHandlerToMsgReDept") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZF_LIIP_CheckMsgHandlerToMsgReDept").Trim() != "" && ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZF_LIIP_CheckMsgHandlerToMsgReDept").Trim() == "1")
            {
                if (scmsg.RecDeptCode != null && scmsg.RecDeptCode.Trim() != "" && scmsg.RecDeptID > 0)
                {
                    int tmplinkcount = IDCVCriticalValueEmpIdDeptLinkDao.GetListCountByHQL(" EmpID= " + entity.HandlerID + " and DeptID=" + scmsg.RecDeptID);
                    if (tmplinkcount <= 0)
                    {
                        ZhiFang.Common.Log.Log.Debug("BSCMsgHandle.Add.消息处理人不在接收部门内部,EmpID= " + entity.HandlerID + " and DeptID=" + entity.HandleDeptID);
                        brdv.ErrorInfo = "消息处理人不在接收部门内部！";
                        brdv.success = false;
                        return brdv;
                    }
                }
            }
            scmsg.HandleFlag = 1;
            scmsg.HandlingDateTime = DateTime.Now;
            if (!this.Entity.HandleTime.HasValue)
            {
                this.Entity.HandleTime = DateTime.Now;
            }
            if (!scmsg.FirstHandleDateTime.HasValue)
                scmsg.FirstHandleDateTime = DateTime.Now;

            if (scmsg.ConfirmFlag == 0 || scmsg.ConfirmerID <= 0)
            {
                scmsg.ConfirmFlag = 1;
                scmsg.ConfirmMemo = entity.Memo;
                scmsg.ConfirmerID = entity.HandlerID;
                scmsg.ConfirmerName = entity.HandlerName;
                scmsg.ConfirmDateTime = DateTime.Now;
                scmsg.ConfirmIPAddress = entity.HandleNodeIPAddress;

                //暂时取登录帐号为医生的工号
                var emp = IDHREmployeeDao.Get(entity.HandlerID);
                if (emp != null)
                {
                    scmsg.ConfirmerCode = emp.StandCode;
                    scmsg.ConfirmerCodeHIS = emp.StandCode;
                }
            }


            if (!IDSCMsgDao.Update(scmsg))
            {
                ZhiFang.Common.Log.Log.Debug("BSCMsgHandle.Add.更新系统消息处理信息失败！MsgID:" + entity.MsgID);
                brdv.ErrorInfo = "更新系统消息处理信息失败！";
                brdv.success = false;
            }
            brdv.ResultDataValue = scmsg.SenderID.ToString();
            var flag = DBDao.Save(entity);
            ZhiFang.Common.Log.Log.Debug("BSCMsgHandle.Add.flag！MsgID:" + entity.MsgID);
            brdv.success = flag;
            return brdv;
        }

        public BaseResultDataValue AddSCMsgHandle_ZF_LAB_START_CV(SCMsgHandle entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            if (entity.MsgID <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("AddSCMsgHandle_ZF_LAB_START_CV.Add.未能找到相关的系统消息！MsgID为空！");
                brdv.ErrorInfo = "系统消息参数错误！请检查！";
                brdv.success = false;
            }
            var scmsg = IDSCMsgDao.Get(entity.MsgID);
            if (scmsg == null)
            {
                ZhiFang.Common.Log.Log.Debug("AddSCMsgHandle_ZF_LAB_START_CV.Add.未能找到相关的系统消息！MsgID:" + entity.MsgID);
                brdv.ErrorInfo = "未能找到相关的系统消息！";
                brdv.success = false;
            }
            #region 获取处理科室医生和确认科室的护士
            List<string> remsgempid = new List<string>();
            var tmplinkd = IDCVCriticalValueEmpIdDeptLinkDao.GetListByHQL(" DeptId=" + scmsg.RecDeptID + " ");
            if (tmplinkd != null && tmplinkd.Count > 0)
            {
                List<string> empidlist = new List<string>();
                tmplinkd.ToList().ForEach(a =>
                {
                    if (a.EmpID.HasValue)
                    {
                        empidlist.Add(a.EmpID.ToString());
                    }
                });


                var emplistd = IDRBACEmpRolesDao.GetListByHQL(" HREmployee.Id in (" + string.Join(",", empidlist) + ") and RBACRole.DeveCode='" + ZFSystemRole.智方_系统角色_医生.Value.Code + "' ");
                if (emplistd == null || emplistd.Count <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug($"AddSCMsgZF_LAB_START_CV.部门id：{scmsg.RecDeptID}下没有角色为DeveCode：{ZFSystemRole.智方_系统角色_医生.Value.Code}的员工！");
                }
                else
                {
                    emplistd.ToList().ForEach(a => remsgempid.Add(a.HREmployee.Id.ToString()));
                }
            }

            var tmplinkn = IDCVCriticalValueEmpIdDeptLinkDao.GetListByHQL(" DeptId=" + scmsg.ConfirmDeptID + " ");
            if (tmplinkn != null && tmplinkn.Count > 0)
            {
                List<string> empidlist = new List<string>();
                tmplinkn.ToList().ForEach(a =>
                {
                    if (a.EmpID.HasValue)
                    {
                        empidlist.Add(a.EmpID.ToString());
                    }
                });


                var emplistd = IDRBACEmpRolesDao.GetListByHQL(" HREmployee.Id in (" + string.Join(",", empidlist) + ") and RBACRole.DeveCode='" + ZFSystemRole.智方_系统角色_护士.Value.Code + "' ");
                if (emplistd == null || emplistd.Count <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug($"AddSCMsgZF_LAB_START_CV.部门id：{scmsg.RecDeptID}下没有角色为DeveCode：{ZFSystemRole.智方_系统角色_护士.Value.Code}的员工！");
                }
                else
                {
                    emplistd.ToList().ForEach(a => remsgempid.Add(a.HREmployee.Id.ToString()));
                }
            }

            brdv.ResultDataValue = string.Join(",", remsgempid);
            #endregion

            entity.SCMsg = scmsg;
            //var sendemp = IDHREmployeeDao.Get(scmsg.SenderID);
            //if (sendemp != null)
            //{
            //    entity.SCMsg.SendDeptID = sendemp.HRDept.Id;
            //    entity.SCMsg.SendDeptName = sendemp.HRDept.CName;
            //    entity.SCMsg.SendDeptCode = sendemp.HRDept.StandCode;//LIS编码
            //}
            //else
            //{
            //    //brdv.ErrorInfo = "未能找到发送者所属科室！SenderID=" + scmsg.SenderID;
            //    //brdv.success = false;
            //    ZhiFang.Common.Log.Log.Error("AddSCMsgHandle_ZF_LAB_START_CV.Add.entity.未能找到发送者所属科室！SenderID = " + scmsg.SenderID);
            //    //return brdv;
            //}

            //判断确认人和部门是否有关系
            if (entity.HandlerID <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("AddSCMsgHandle_ZF_LAB_START_CV.Add.消息处理人不能为空！");
                brdv.ErrorInfo = "消息处理人不能为空！";
                brdv.success = false;
                return brdv;
            }

            if (entity.HandlerName == null || entity.HandlerName.Trim() == "")
            {
                ZhiFang.Common.Log.Log.Debug("AddSCMsgHandle_ZF_LAB_START_CV.Add.消息处理人姓名不能为空！");
                brdv.ErrorInfo = "消息处理人姓名不能为空！";
                brdv.success = false;
                return brdv;
            }

            if (entity.HandleDesc == null || entity.HandleDesc.Trim() == "")
            {
                ZhiFang.Common.Log.Log.Debug("AddSCMsgHandle_ZF_LAB_START_CV.Add.消息处理意见不能为空！");
                brdv.ErrorInfo = "消息处理意见不能为空！";
                brdv.success = false;
                return brdv;
            }
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZF_LIIP_CheckMsgHandlerToMsgReDept") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZF_LIIP_CheckMsgHandlerToMsgReDept").Trim() != "" && ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZF_LIIP_CheckMsgHandlerToMsgReDept").Trim() == "1")
            {
                if (scmsg.RecDeptCode != null && scmsg.RecDeptCode.Trim() != "" && scmsg.RecDeptID > 0)
                {
                    int tmplinkcount = IDCVCriticalValueEmpIdDeptLinkDao.GetListCountByHQL(" EmpID= " + entity.HandlerID + " and DeptID=" + scmsg.RecDeptID);
                    if (tmplinkcount <= 0)
                    {
                        ZhiFang.Common.Log.Log.Debug("AddSCMsgHandle_ZF_LAB_START_CV.Add.消息处理人不在接收部门内部,EmpID= " + entity.HandlerID + " and DeptID=" + entity.HandleDeptID);
                        brdv.ErrorInfo = "消息处理人不在接收部门内部！";
                        brdv.success = false;
                        return brdv;
                    }
                }
            }
            scmsg.HandleFlag = 1;
            scmsg.HandlingDateTime = DateTime.Now;
            scmsg.HandleTypeID = entity.HandleTypeID;
            scmsg.HandleTypeName = entity.HandleTypeName;

            if (!this.Entity.HandleTime.HasValue)
            {
                this.Entity.HandleTime = DateTime.Now;
            }
            if (!scmsg.FirstHandleDateTime.HasValue)
                scmsg.FirstHandleDateTime = DateTime.Now;

            if (scmsg.ConfirmFlag == 0 || scmsg.ConfirmerID <= 0)
            {
                scmsg.ConfirmFlag = 1;
                scmsg.ConfirmMemo = entity.Memo;
                scmsg.ConfirmerID = entity.HandlerID;
                scmsg.ConfirmerName = entity.HandlerName;
                scmsg.ConfirmDateTime = DateTime.Now;
                scmsg.ConfirmIPAddress = entity.HandleNodeIPAddress;

                //暂时取登录帐号为医生的工号
                var emp = IDHREmployeeDao.Get(entity.HandlerID);
                if (emp != null)
                {
                    scmsg.ConfirmerCode = emp.StandCode;
                    scmsg.ConfirmerCodeHIS = emp.StandCode;
                }
            }

            if (scmsg.ConfirmDateTime.HasValue)
            {
                scmsg.HandleUseTime = long.Parse(Math.Round((DateTime.Now - scmsg.ConfirmDateTime.Value).TotalSeconds, 0).ToString());
            }
            if (scmsg.DataAddTime.HasValue)
            {
                scmsg.ConfirmHandleUseTime = long.Parse(Math.Round((DateTime.Now - scmsg.DataAddTime.Value).TotalSeconds, 0).ToString());
            }
            if (scmsg.DataAddTime.HasValue)
            {
                scmsg.ConfirmHandleUseTime = long.Parse(Math.Round((DateTime.Now - scmsg.DataAddTime.Value).TotalSeconds, 0).ToString());
            }

            if (entity.HandleTypeID > 0)
            {
                scmsg.HandleTypeID = entity.HandleTypeID;
            }
            if (!string.IsNullOrEmpty(entity.HandleTypeName))
            {
                scmsg.HandleTypeName = entity.HandleTypeName;
            }

            if (!IDSCMsgDao.Update(scmsg))
            {
                ZhiFang.Common.Log.Log.Debug("AddSCMsgHandle_ZF_LAB_START_CV.Add.更新系统消息处理信息失败！MsgID:" + entity.MsgID);
                brdv.ErrorInfo = "更新系统消息处理信息失败！";
                brdv.success = false;
            }
            var flag = DBDao.Save(entity);
            ZhiFang.Common.Log.Log.Debug("AddSCMsgHandle_ZF_LAB_START_CV.Add.flag！MsgID:" + entity.MsgID);
            brdv.success = flag;
            return brdv;
        }
    }
}