using System;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.LIIP;
using ZhiFang.Entity.LIIP;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using System.Collections.Generic;
using ZhiFang.Entity.LIIP.ViewObject.Request;
using ZhiFang.IBLL.LIIP;
using ZhiFang.Common.Public;
using ZhiFang.IBLL.RBAC;

namespace ZhiFang.BLL.LIIP
{
    /// <summary>
    ///
    /// </summary>
    public class BSCMsg : BaseBLL<SCMsg>, ZhiFang.IBLL.LIIP.IBSCMsg
    {
        public IDAO.RBAC.IDHRDeptDao IDHRDeptDao { get; set; }
        public IDAO.RBAC.IDRBACUserDao IDRBACUserDao { get; set; }

        public IDAO.LIIP.IDCVCriticalValueEmpIdDeptLinkDao IDCVCriticalValueEmpIdDeptLinkDao { get; set; }
        public IDAO.LIIP.IDSCMsgTypeDao IDSCMsgTypeDao { get; set; }

        ZhiFang.IDAO.RBAC.IDHREmployeeDao IDHREmployeeDao { get; set; }

        IDAO.LIIP.IDBHospitalDao IDBHospitalDao { get; set; }

        IDAO.LIIP.IDBHospitalEmpLinkDao IDBHospitalEmpLinkDao { get; set; }

        IDAO.LIIP.IDIntergrateSystemSetDao IDIntergrateSystemSetDao { get; set; }

        public IDAO.LIIP.IDSCMsgHandleDao IDSCMsgHandleDao { get; set; }

        public IDAO.RBAC.IDRBACEmpRolesDao IDRBACEmpRolesDao { get; set; }

        public IDAO.RBAC.IDRBACRoleDao IDRBACRoleDao { get; set; }

        public IDAO.Common.IDBParameterDao IDBParameterDao { get; set; }


        public BaseResultDataValue AddAndGetDeptId(SCMsg entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            IList<HRDept> tmpdept = new List<HRDept>();
            bool flag = true;
            if (entity.MsgTypeCode == null || entity.MsgTypeCode.Trim() == "")
            {
                brdv.success = false;
                brdv.ErrorInfo = "消息类型代码为空！";
                ZhiFang.Common.Log.Log.Error(" BSCMsg.AddAndGetDeptId.entity.MsgTypeCode为空！ ");
            }
            var msgtypelist = IDSCMsgTypeDao.GetListByHQL(" Code='" + entity.MsgTypeCode.Trim() + "' ");
            if (msgtypelist == null || msgtypelist.Count == 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "未能找到匹配的消息类型代码！";
                ZhiFang.Common.Log.Log.Error(" BSCMsg.AddAndGetDeptId.entity.MsgTypeCode：" + entity.MsgTypeCode.Trim() + ".未能找到匹配的消息类型代码！ ");
            }
            entity.MsgTypeID = msgtypelist[0].Id;
            entity.MsgTypeName = msgtypelist[0].CName;
            entity.SystemCName = msgtypelist[0].SystemCName;
            entity.SystemCode = msgtypelist[0].SystemCode;
            entity.SystemID = msgtypelist[0].SystemID;
            if (flag && entity.RecDeptCode != null && entity.RecDeptCode.Trim() != "")//判断LIS编码
            {
                tmpdept = IDHRDeptDao.GetListByHQL(" StandCode='" + entity.RecDeptCode.Trim() + "' ");
                if (tmpdept != null && tmpdept.Count() > 0)
                {
                    brdv.ResultDataValue = tmpdept.ElementAt(0).Id.ToString();
                    entity.RecDeptID = tmpdept.ElementAt(0).Id;
                    flag = false;
                }
            }

            if (flag && entity.RecDeptCodeHIS != null && entity.RecDeptCodeHIS.Trim() != "")//判断HIS编码
            {
                tmpdept = IDHRDeptDao.GetListByHQL(" DeveCode='" + entity.RecDeptCodeHIS.Trim() + "' ");
                if (tmpdept != null && tmpdept.Count() > 0)
                {
                    brdv.ResultDataValue = tmpdept.ElementAt(0).Id.ToString();
                    entity.RecDeptID = tmpdept.ElementAt(0).Id;
                    flag = false;
                }
            }
            if (flag && entity.RecDeptName != null && entity.RecDeptName.Trim() != "")//判断部门名称
            {
                tmpdept = IDHRDeptDao.GetListByHQL(" CName='" + entity.RecDeptName.Trim() + "' ");
                if (tmpdept != null && tmpdept.Count() > 0)
                {
                    brdv.ResultDataValue = tmpdept.ElementAt(0).Id.ToString();
                    entity.RecDeptID = tmpdept.ElementAt(0).Id;
                    flag = false;
                }
            }

            //存在账号信息属性值的，以该信息找到人员ID+CName
            if (entity.SenderAccount != null && entity.SenderAccount.Trim() != "")
            {
                IList<RBACUser> tmpuser = IDRBACUserDao.GetListByHQL(" Account='" + entity.SenderAccount.Trim() + "' ");
                if (tmpuser != null && tmpuser.Count() > 0)
                {
                    entity.SenderID = tmpuser.ElementAt(0).HREmployee.Id;
                    entity.SenderName = tmpuser.ElementAt(0).HREmployee.CName;
                }
            }

            #region 填写要求时间
            int tsint = 5;
            var confirmrang = IDBParameterDao.GetListByHQL($" ParaNo='{SystemParameter_LIIP.消息要求确认时间间隔_分钟.Value.Code}'");
            if (confirmrang != null && confirmrang.Count > 0)
            {
                var ts = confirmrang.First().ParaValue;
                if (int.TryParse(ts, out int t))
                {
                    tsint = t;
                }
            }
            entity.RequireConfirmTime = DateTime.Now.AddMinutes(tsint);
            var handlerang = IDBParameterDao.GetListByHQL($" ParaNo='{SystemParameter_LIIP.消息要求处理时间间隔_分钟.Value.Code}'");
            if (handlerang != null && handlerang.Count > 0)
            {
                var ts = handlerang.First().ParaValue;
                if (int.TryParse(ts, out int t))
                {
                    tsint += t;
                }
            }
            entity.RequireAllFinishTime = DateTime.Now.AddMinutes(tsint);
            #endregion

            #region 根据小组号写入发送科室
            //var sendemp = IDHREmployeeDao.Get(entity.SenderID);
            //if (sendemp != null)
            //{
            //    entity.SendDeptID = sendemp.HRDept.Id;
            //    entity.SendDeptName = sendemp.HRDept.CName;
            //    entity.SendDeptCode = sendemp.HRDept.StandCode;//LIS编码
            //}
            //else
            //{
            //    brdv.ErrorInfo = "未能找到发送者所属科室！SenderID=" + entity.SenderID;
            //    brdv.success = false;
            //    ZhiFang.Common.Log.Log.Error(" BSCMsg.AddAndGetDeptId.entity.未能找到发送者所属科室！SenderID = " + entity.SenderID);
            //    return brdv;
            //}
            if (flag && entity.SendSectionID != null && entity.SendSectionID.ToString() != "")//判断LIS编码
            {
                tmpdept = IDHRDeptDao.GetListByHQL(" StandCode='" + entity.SendSectionID.ToString() + "' ");
                if (tmpdept != null && tmpdept.Count() > 0)
                {
                    entity.SendDeptID = tmpdept.ElementAt(0).Id;
                    entity.SendDeptName = tmpdept.ElementAt(0).CName;
                    entity.SendDeptCode = tmpdept.ElementAt(0).StandCode;
                    flag = false;
                }
            }

            if (flag && entity.SendSectionID != null && entity.SendSectionID.ToString() != "")//判断LIS编码
            {
                tmpdept = IDHRDeptDao.GetListByHQL(" DeveCode='" + entity.SendSectionID.ToString() + "' ");
                if (tmpdept != null && tmpdept.Count() > 0)
                {
                    entity.SendDeptID = tmpdept.ElementAt(0).Id;
                    entity.SendDeptName = tmpdept.ElementAt(0).CName;
                    entity.SendDeptCode = tmpdept.ElementAt(0).StandCode;
                    flag = false;
                }
            }

            if (flag && entity.SendSectionName != null && entity.SendSectionName.Trim() != "")//判断名称
            {
                tmpdept = IDHRDeptDao.GetListByHQL(" CName='" + entity.SendSectionName.Trim() + "' ");
                if (tmpdept != null && tmpdept.Count() > 0)
                {
                    entity.SendDeptID = tmpdept.ElementAt(0).Id;
                    entity.SendDeptName = tmpdept.ElementAt(0).CName;
                    entity.SendDeptCode = tmpdept.ElementAt(0).StandCode;
                    flag = false;
                }
            }
            #endregion

            brdv.success = DBDao.Save(entity);
            return brdv;
        }

        public BaseResultDataValue AddSCMsgZF_LAB_START_CV(SCMsg entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            IList<HRDept> tmpdept = new List<HRDept>();
            bool flag = true;
            if (entity.MsgTypeCode == null || entity.MsgTypeCode.Trim() == "")
            {
                brdv.success = false;
                brdv.ErrorInfo = "消息类型代码为空！";
                ZhiFang.Common.Log.Log.Error(" BSCMsg.AddSCMsgZF_LAB_START_CV.entity.MsgTypeCode为空！ ");
            }
            var msgtypelist = IDSCMsgTypeDao.GetListByHQL(" Code='" + entity.MsgTypeCode.Trim() + "' ");
            if (msgtypelist == null || msgtypelist.Count == 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "未能找到匹配的消息类型代码！";
                ZhiFang.Common.Log.Log.Error(" BSCMsg.AddSCMsgZF_LAB_START_CV.entity.MsgTypeCode：" + entity.MsgTypeCode.Trim() + ".未能找到匹配的消息类型代码！ ");
            }
            entity.MsgTypeID = msgtypelist[0].Id;
            entity.MsgTypeName = msgtypelist[0].CName;
            entity.SystemCName = msgtypelist[0].SystemCName;
            entity.SystemCode = msgtypelist[0].SystemCode;
            entity.SystemID = msgtypelist[0].SystemID;
            #region 接收科室（处理科室）
            if (flag && entity.RecDeptCode != null && entity.RecDeptCode.Trim() != "")//判断LIS编码
            {
                if (string.IsNullOrEmpty(entity.ConfirmDeptCode))//兼容没有传递确认科室的版本
                {
                    entity.ConfirmDeptCode = entity.RecDeptCode;
                }
                tmpdept = IDHRDeptDao.GetListByHQL(" StandCode='" + entity.RecDeptCode.Trim() + "' ");
                if (tmpdept != null && tmpdept.Count() > 0)
                {
                    entity.RecDeptID = tmpdept.ElementAt(0).Id;
                    flag = false;
                }
            }

            if (flag && entity.RecDeptCodeHIS != null && entity.RecDeptCodeHIS.Trim() != "")//判断HIS编码
            {
                if (string.IsNullOrEmpty(entity.ConfirmDeptCodeHIS))//兼容没有传递确认科室的版本
                {
                    entity.ConfirmDeptCodeHIS = entity.RecDeptCode;
                }
                tmpdept = IDHRDeptDao.GetListByHQL(" DeveCode='" + entity.RecDeptCodeHIS.Trim() + "' ");
                if (tmpdept != null && tmpdept.Count() > 0)
                {
                    entity.RecDeptID = tmpdept.ElementAt(0).Id;
                    flag = false;
                }
            }
            if (flag && entity.RecDeptName != null && entity.RecDeptName.Trim() != "")//判断部门名称
            {
                if (string.IsNullOrEmpty(entity.ConfirmDeptName))//兼容没有传递确认科室的版本
                {
                    entity.ConfirmDeptName = entity.RecDeptName;
                }
                tmpdept = IDHRDeptDao.GetListByHQL(" CName='" + entity.RecDeptName.Trim() + "' ");
                if (tmpdept != null && tmpdept.Count() > 0)
                {
                    entity.RecDeptID = tmpdept.ElementAt(0).Id;
                    flag = false;
                }
            }
            #endregion

            flag = true;
            #region 确认科室
            if (flag && entity.ConfirmDeptCode != null && entity.ConfirmDeptCode.Trim() != "")//判断LIS编码
            {
                tmpdept = IDHRDeptDao.GetListByHQL(" StandCode='" + entity.ConfirmDeptCode.Trim() + "' ");
                if (tmpdept != null && tmpdept.Count() > 0)
                {
                    entity.ConfirmDeptID = tmpdept.ElementAt(0).Id;
                    flag = false;
                }
            }

            if (flag && entity.ConfirmerCodeHIS != null && entity.ConfirmerCodeHIS.Trim() != "")//判断HIS编码
            {
                tmpdept = IDHRDeptDao.GetListByHQL(" DeveCode='" + entity.ConfirmerCodeHIS.Trim() + "' ");
                if (tmpdept != null && tmpdept.Count() > 0)
                {
                    entity.ConfirmDeptID = tmpdept.ElementAt(0).Id;
                    flag = false;
                }
            }
            if (flag && entity.ConfirmDeptName != null && entity.ConfirmDeptName.Trim() != "")//判断部门名称
            {
                tmpdept = IDHRDeptDao.GetListByHQL(" CName='" + entity.ConfirmDeptName.Trim() + "' ");
                if (tmpdept != null && tmpdept.Count() > 0)
                {
                    entity.ConfirmDeptID = tmpdept.ElementAt(0).Id;
                    flag = false;
                }
            }
            #endregion

            #region 获取处理科室医生和确认科室的护士
            List<string> remsgempid = new List<string>();
            var tmplinkd = IDCVCriticalValueEmpIdDeptLinkDao.GetListByHQL(" DeptId=" + entity.RecDeptID + " ");
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
                    ZhiFang.Common.Log.Log.Debug($"AddSCMsgZF_LAB_START_CV.部门id：{entity.RecDeptID}下没有角色为DeveCode：{ZFSystemRole.智方_系统角色_医生.Value.Code}的员工！");
                }
                else
                {
                    emplistd.ToList().ForEach(a => remsgempid.Add(a.HREmployee.Id.ToString()));
                }
            }

            var tmplinkn = IDCVCriticalValueEmpIdDeptLinkDao.GetListByHQL(" DeptId=" + entity.ConfirmDeptID + " ");
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

                ZhiFang.Common.Log.Log.Debug($"AddSCMsgZF_LAB_START_CV.empidlist：{string.Join(",", empidlist)}");
                var emplistd = IDRBACEmpRolesDao.GetListByHQL(" HREmployee.Id in (" + string.Join(",", empidlist) + ") and RBACRole.DeveCode='" + ZFSystemRole.智方_系统角色_护士.Value.Code + "' ");
                ZhiFang.Common.Log.Log.Debug($"AddSCMsgZF_LAB_START_CV.emplistdHQL： HREmployee.Id in (" + string.Join(", ", empidlist) + ") and RBACRole.DeveCode = '" + ZFSystemRole.智方_系统角色_护士.Value.Code + "' ");
                if (emplistd == null || emplistd.Count <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug($"AddSCMsgZF_LAB_START_CV.部门id：{entity.RecDeptID}下没有角色为DeveCode：{ZFSystemRole.智方_系统角色_护士.Value.Code}的员工！");
                }
                else
                {

                    emplistd.ToList().ForEach(a =>
                    {
                        remsgempid.Add(a.HREmployee.Id.ToString());
                        ZhiFang.Common.Log.Log.Debug($"AddSCMsgZF_LAB_START_CV.remsgempid：{a.HREmployee.Id.ToString()}");
                    });
                }
            }

            brdv.ResultDataValue = string.Join(",", remsgempid);

            #endregion

            //存在账号信息属性值的，以该信息找到人员ID+CName
            if (entity.SenderAccount != null && entity.SenderAccount.Trim() != "")
            {
                IList<RBACUser> tmpuser = IDRBACUserDao.GetListByHQL(" Account='" + entity.SenderAccount.Trim() + "' ");
                if (tmpuser != null && tmpuser.Count() > 0)
                {
                    entity.SenderID = tmpuser.ElementAt(0).HREmployee.Id;
                    entity.SenderName = tmpuser.ElementAt(0).HREmployee.CName;
                }
            }

            #region 根据小组号写入发送科室
            //var sendemp = IDHREmployeeDao.Get(entity.SenderID);
            //if (sendemp != null)
            //{
            //    entity.SendDeptID = sendemp.HRDept.Id;
            //    entity.SendDeptName = sendemp.HRDept.CName;
            //    entity.SendDeptCode = sendemp.HRDept.StandCode;//LIS编码
            //}
            //else
            //{
            //    brdv.ErrorInfo = "未能找到发送者所属科室！SenderID=" + entity.SenderID;
            //    brdv.success = false;
            //    ZhiFang.Common.Log.Log.Error(" BSCMsg.AddAndGetDeptId.entity.未能找到发送者所属科室！SenderID = " + entity.SenderID);
            //    return brdv;
            //}
            flag = true;
            if (flag && entity.SendSectionID != null && entity.SendSectionID.ToString() != "")//判断LIS编码
            {
                tmpdept = IDHRDeptDao.GetListByHQL(" StandCode='" + entity.SendSectionID.ToString() + "' ");
                if (tmpdept != null && tmpdept.Count() > 0)
                {
                    entity.SendDeptID = tmpdept.ElementAt(0).Id;
                    entity.SendDeptName = tmpdept.ElementAt(0).CName;
                    entity.SendDeptCode = tmpdept.ElementAt(0).StandCode;
                    flag = false;
                }
            }

            if (flag && entity.SendSectionID != null && entity.SendSectionID.ToString() != "")//判断LIS编码
            {
                tmpdept = IDHRDeptDao.GetListByHQL(" DeveCode='" + entity.SendSectionID.ToString() + "' ");
                if (tmpdept != null && tmpdept.Count() > 0)
                {
                    entity.SendDeptID = tmpdept.ElementAt(0).Id;
                    entity.SendDeptName = tmpdept.ElementAt(0).CName;
                    entity.SendDeptCode = tmpdept.ElementAt(0).StandCode;
                    flag = false;
                }
            }

            if (flag && entity.SendSectionName != null && entity.SendSectionName.Trim() != "")//判断名称
            {
                tmpdept = IDHRDeptDao.GetListByHQL(" CName='" + entity.SendSectionName.Trim() + "' ");
                if (tmpdept != null && tmpdept.Count() > 0)
                {
                    entity.SendDeptID = tmpdept.ElementAt(0).Id;
                    entity.SendDeptName = tmpdept.ElementAt(0).CName;
                    entity.SendDeptCode = tmpdept.ElementAt(0).StandCode;
                    flag = false;
                }
            }
            #endregion

            #region 填写要求时间
            int tsint = 5;
            var confirmrang = IDBParameterDao.GetListByHQL($" ParaNo='{SystemParameter_LIIP.消息要求确认时间间隔_分钟.Value.Code}'");
            if (confirmrang != null && confirmrang.Count > 0)
            {
                var ts = confirmrang.First().ParaValue;
                if (int.TryParse(ts, out int t))
                {
                    tsint = t;
                }
            }
            entity.RequireConfirmTime = DateTime.Now.AddMinutes(tsint);
            var handlerang = IDBParameterDao.GetListByHQL($" ParaNo='{SystemParameter_LIIP.消息要求处理时间间隔_分钟.Value.Code}'");
            if (handlerang != null && handlerang.Count > 0)
            {
                var ts = handlerang.First().ParaValue;
                if (int.TryParse(ts, out int t))
                {
                    tsint += t;
                }
            }
            entity.RequireAllFinishTime = DateTime.Now.AddMinutes(tsint);
            #endregion
            brdv.success = DBDao.Save(entity);
            return brdv;
        }

        public BaseResultDataValue SaveAndGetDeptId_OTTH(SCMsg_OTTH entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //ZhiFang.Common.Log.Log.Debug("SaveAndGetDeptId_OTTH.1");
            #region 身份验证
            var scmsg = new SCMsg();

            scmsg.RecSickTypeID = (long.TryParse(entity.SickTypeID, out long SickTypeID)) ? SickTypeID : 0;

            scmsg.RecSickTypeName = entity.SickTypeName;
            //ZhiFang.Common.Log.Log.Debug("SaveAndGetDeptId_OTTH.2");
            if (entity.SenderAccount != null && entity.SenderAccount.Trim() != "")
            {
                IList<RBACUser> tmpuser = IDRBACUserDao.GetListByHQL(" Account='" + entity.SenderAccount.Trim() + "' ");
                if (tmpuser.Count <= 0)
                {
                    throw new Exception("\"" + entity.SenderAccount.Trim() + "\"账户不存在！");
                }
                if (!tmpuser[0].IsUse.HasValue || !tmpuser[0].IsUse.Value)
                {
                    throw new Exception("\"" + entity.SenderAccount.Trim() + "\"账户被禁用！");
                }
                if (!(tmpuser[0].HREmployee.IsUse.HasValue && tmpuser[0].HREmployee.IsUse.Value && tmpuser[0].HREmployee.IsEnabled == 1))
                {
                    throw new Exception("\"" + entity.SenderAccount.Trim() + "\"人员被禁用！");
                }
                if (tmpuser != null && tmpuser.Count() > 0)
                {
                    string pwd = SecurityHelp.MD5Encrypt(entity.SenderPWD.Trim(), SecurityHelp.PWDMD5Key);
                    if (pwd.Trim() == tmpuser.ElementAt(0).PWD)
                    {
                        scmsg.SenderID = tmpuser.ElementAt(0).HREmployee.Id;
                        scmsg.SenderName = tmpuser.ElementAt(0).HREmployee.CName;
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("AddAndGetDeptId_OTTH.用户名密码错误!帐号：" + entity.SenderAccount + ",密码：" + entity.SenderPWD);
                        throw new Exception("用户名密码错误！");
                    }
                }
            }
            #endregion
            //ZhiFang.Common.Log.Log.Debug("SaveAndGetDeptId_OTTH.3");
            #region 身份和消息类型验证,暂时不验证
            #endregion

            if (DateTime.TryParse(entity.RequireConfirmTime, out DateTime RequireConfirmTime))
            {
                scmsg.RequireConfirmTime = RequireConfirmTime;
            }

            if (DateTime.TryParse(entity.RequireHandleTime, out DateTime RequireHandleTime))
            {
                scmsg.RequireHandleTime = RequireHandleTime;
            }
            scmsg.DataAddTime = DateTime.Now;
            scmsg.DataUpdateTime = DateTime.Now;
            //ZhiFang.Common.Log.Log.Debug("SaveAndGetDeptId_OTTH.4");
            IList<HRDept> tmpdept = new List<HRDept>();
            bool flag = true;
            if (entity.MsgTypeCode == null || entity.MsgTypeCode.Trim() == "")
            {
                brdv.success = false;
                brdv.ErrorInfo = "消息类型代码为空！";
                ZhiFang.Common.Log.Log.Error(" BSCMsg.AddAndGetDeptId.entity.MsgTypeCode为空！ ");
            }
            //ZhiFang.Common.Log.Log.Debug("SaveAndGetDeptId_OTTH.5");
            var msgtypelist = IDSCMsgTypeDao.GetListByHQL(" Code='" + entity.MsgTypeCode.Trim() + "' ");
            if (msgtypelist == null || msgtypelist.Count == 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "未能找到匹配的消息类型代码！";
                ZhiFang.Common.Log.Log.Error(" BSCMsg.AddAndGetDeptId.entity.MsgTypeCode：" + entity.MsgTypeCode.Trim() + ".未能找到匹配的消息类型代码！ ");
                return brdv;
            }
            //ZhiFang.Common.Log.Log.Debug("SaveAndGetDeptId_OTTH.6");
            scmsg.MsgContent = entity.MsgContent;
            scmsg.MsgTypeCode = entity.MsgTypeCode;
            scmsg.MsgTypeID = msgtypelist[0].Id;
            scmsg.MsgTypeName = msgtypelist[0].CName;
            scmsg.SystemCName = msgtypelist[0].SystemCName;
            scmsg.SystemCode = msgtypelist[0].SystemCode;
            scmsg.SystemID = msgtypelist[0].SystemID;
            scmsg.RecDeptCodeHIS = entity.RecDeptCodeHIS;
            scmsg.ConfirmerCodeHIS = entity.RecDeptCodeHIS;
            scmsg.IsUse = true;


            //ZhiFang.Common.Log.Log.Debug("SaveAndGetDeptId_OTTH.7");
            if (flag && entity.RecDeptCodeHIS != null && entity.RecDeptCodeHIS.Trim() != "")//判断HIS编码
            {
                tmpdept = IDHRDeptDao.GetListByHQL(" DeveCode='" + entity.RecDeptCodeHIS.Trim() + "' ");
                if (tmpdept != null && tmpdept.Count() > 0)
                {
                    brdv.ResultDataValue = tmpdept.ElementAt(0).Id.ToString();
                    scmsg.RecDeptID = tmpdept.ElementAt(0).Id;
                    scmsg.RecDeptName = tmpdept.ElementAt(0).CName;
                    scmsg.RecDeptCode = tmpdept.ElementAt(0).StandCode;
                    flag = false;
                }
            }
            //ZhiFang.Common.Log.Log.Debug("SaveAndGetDeptId_OTTH.8");
            var sendemp = IDHREmployeeDao.Get(scmsg.SenderID);
            if (sendemp != null)
            {
                scmsg.SendDeptID = sendemp.HRDept.Id;
                scmsg.SendDeptName = sendemp.HRDept.CName;
                scmsg.SendDeptCode = sendemp.HRDept.StandCode;//LIS编码
            }
            else
            {
                brdv.ErrorInfo = "未能找到发送者所属科室！SenderID=" + scmsg.SenderID;
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error(" BSCMsg.AddAndGetDeptId.entity.未能找到发送者所属科室！SenderID = " + scmsg.SenderID);
                return brdv;
            }
            //ZhiFang.Common.Log.Log.Debug("SaveAndGetDeptId_OTTH.9");
            brdv.success = DBDao.Save(scmsg);
            entity.SCMsg = scmsg;//外部发送消息会用到这个实体类。
            //ZhiFang.Common.Log.Log.Debug("SaveAndGetDeptId_OTTH.10");
            return brdv;
        }

        public SCMsg MsgHandleSearchById_OTTH(SCMsg_OTTH_Search entity)
        {

            #region 身份验证
            if (entity.SenderAccount != null && entity.SenderAccount.Trim() != "")
            {
                IList<RBACUser> tmpuser = IDRBACUserDao.GetListByHQL(" Account='" + entity.SenderAccount.Trim() + "' ");
                if (tmpuser.Count <= 0)
                {
                    throw new Exception("\"" + entity.SenderAccount.Trim() + "\"账户不存在！");
                }
                if (!tmpuser[0].IsUse.HasValue || !tmpuser[0].IsUse.Value)
                {
                    throw new Exception("\"" + entity.SenderAccount.Trim() + "\"账户被禁用！");
                }
                if (!(tmpuser[0].HREmployee.IsUse.HasValue && tmpuser[0].HREmployee.IsUse.Value && tmpuser[0].HREmployee.IsEnabled == 1))
                {
                    throw new Exception("\"" + entity.SenderAccount.Trim() + "\"人员被禁用！");
                }
                if (tmpuser != null && tmpuser.Count() > 0)
                {
                    string pwd = SecurityHelp.MD5Encrypt(entity.SenderPWD.Trim(), SecurityHelp.PWDMD5Key);
                    if (pwd.Trim() != tmpuser.ElementAt(0).PWD)
                    {
                        ZhiFang.Common.Log.Log.Debug("AddAndGetDeptId_OTTH.用户名密码错误!帐号：" + entity.SenderAccount + ",密码：" + entity.SenderPWD);
                        throw new Exception("用户名密码错误！");
                    }
                }
            }
            #endregion

            SCMsg SCMsg = DBDao.Get(long.Parse(entity.MsgCode));
            var handlelist = IDSCMsgHandleDao.GetListByHQL(" MsgID= " + entity.MsgCode);
            SCMsg.SCMsgHandleList = new List<SCMsgHandle>();
            if (handlelist != null && handlelist.Count > 0)
                SCMsg.SCMsgHandleList = handlelist.ToList();

            return SCMsg;
        }

        public string BatchConfirmMsg(string where, string empid, string ip)
        {
            var emp = IDHREmployeeDao.Get(long.Parse(empid));
            if (emp == null)
            {
                throw new Exception("登录者没有人员信息！");
            }

            IList<BHospitalEmpLink> BHospitalEmpLinklist = IDBHospitalEmpLinkDao.GetListByHQL(" EmpID=" + empid + " ");
            if (BHospitalEmpLinklist == null || BHospitalEmpLinklist.Count == 0)
            {
                throw new Exception("登录者没有查看机构消息通知权限！");
            }

            List<long> Hospital = new List<long>();
            BHospitalEmpLinklist.ToList().ForEach(a =>
            {
                if (a.HospitalID.HasValue)
                {
                    Hospital.Add(a.HospitalID.Value);
                }
            });

            #region 准备参数
            List<string> para = new List<string>();

            para.Add("ConfirmerID=" + empid);
            para.Add("ConfirmerName='" + emp.CName + "' ");
            para.Add("ConfirmerCode='" + emp.StandCode + "' ");
            if (ip != null && ip.Trim() != "")
            {
                para.Add("ConfirmIPAddress='" + ip + "' ");
            }

            //if (Entity.ConfirmMemo != null && Entity.ConfirmMemo.Trim() != "")
            //{
            //    para.Add("ConfirmMemo='" + Entity.ConfirmMemo + "' ");
            //}
            //if (Entity.ConfirmNotifyDoctorCode != null && Entity.ConfirmNotifyDoctorCode.Trim() != "")
            //{
            //    para.Add("ConfirmNotifyDoctorCode='" + Entity.ConfirmNotifyDoctorCode + "' ");
            //}

            //if (Entity.ConfirmNotifyDoctorCodeHIS != null && Entity.ConfirmNotifyDoctorCodeHIS.Trim() != "")
            //{
            //    para.Add("ConfirmNotifyDoctorCodeHIS='" + Entity.ConfirmNotifyDoctorCodeHIS + "' ");
            //}

            //if (Entity.ConfirmNotifyDoctorID > 0)
            //{
            //    para.Add("ConfirmNotifyDoctorID=" + Entity.ConfirmNotifyDoctorID + " ");
            //}

            //if (Entity.ConfirmNotifyDoctorName != null && Entity.ConfirmNotifyDoctorName.Trim() != "")
            //{
            //    para.Add("ConfirmNotifyDoctorName='" + Entity.ConfirmNotifyDoctorName + "' ");
            //}
            para.Add("ConfirmDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            para.Add("ConfirmFlag=1");
            #endregion
            if (where == null || where.Trim() == "")
            {
                where = "RecLabID in (" + string.Join(",", Hospital) + ") and (ConfirmFlag=null or ConfirmFlag=0) ";
            }
            else
            {
                where = where + " and RecLabID in (" + string.Join(",", Hospital) + ") and (ConfirmFlag=null or ConfirmFlag=0) ";
            }

            return (DBDao.UpdateByHql(" update SCMsg set  " + string.Join(",", para) + "  where " + where) > 0).ToString();
        }

        public bool LISSendMessage(LISSendMessageVO lismsg)
        {
            SCMsg entity = new SCMsg();

            #region 验证系统编码
            IList<IntergrateSystemSet> isslist = IDIntergrateSystemSetDao.GetListByHQL(" SystemCode='" + lismsg.SystemCode.Trim() + "' ");
            if (isslist != null && isslist.Count() > 0)
            {
                entity.SystemID = isslist[0].Id;
            }
            else
            {
                throw new Exception("未找到编码为'" + lismsg.SystemCode.Trim() + "'的系统设置！");
            }
            #endregion

            #region 验证消息编码
            IList<SCMsgType> mtclist = IDSCMsgTypeDao.GetListByHQL(" Code='" + lismsg.MsgTypeCode.Trim() + "' ");
            if (mtclist != null && mtclist.Count() > 0)
            {
                entity.MsgTypeID = mtclist[0].Id;
                entity.MsgTypeName = mtclist[0].CName;
                entity.MsgTypeCode = mtclist[0].Code;
            }
            else
            {
                IDSCMsgTypeDao.SaveByEntity(new SCMsgType()
                {
                    Id = lismsg.MsgTypeID.Value,
                    CName = lismsg.MsgTypeName,
                    Code = lismsg.MsgTypeCode,
                    Url = "{ZF_LIIP}/ui/layui/views/msg/card/form.html",
                    SystemID = isslist[0].Id,
                    SystemCode = isslist[0].SystemCode,
                    SystemCName = isslist[0].SystemName,
                    IsUse = true
                });
                ;
                entity.MsgTypeID = lismsg.MsgTypeID.Value;
                entity.MsgTypeName = lismsg.MsgTypeName;
                entity.MsgTypeCode = lismsg.MsgTypeCode;
            }

            #endregion


            entity.CenterBarCode = lismsg.CenterBarCode;
            entity.LabBarCode = lismsg.LabBarCode;
            entity.MsgContent = lismsg.MsgContent;
            entity.RecLabCode = lismsg.RecLabCode;
            entity.SenderAccount = lismsg.SenderAccount;
            entity.SendSectionID = lismsg.SendSectionID;
            entity.SendSectionName = lismsg.SendSectionName;
            entity.SystemCName = lismsg.SystemCName;
            entity.SystemCode = lismsg.SystemCode;


            entity.IsUse = true;
            entity.Id = GUIDHelp.GetGUIDLong();
            lismsg.SCMsgID = entity.Id;
            if (lismsg.SenderAccount != null && lismsg.SenderAccount.Trim() != "")
            {
                IList<RBACUser> tmpuser = IDRBACUserDao.GetListByHQL(" Account='" + lismsg.SenderAccount.Trim() + "' ");
                if (tmpuser != null && tmpuser.Count() > 0)
                {
                    entity.SenderID = tmpuser.ElementAt(0).HREmployee.Id;
                    entity.SenderName = tmpuser.ElementAt(0).HREmployee.CName;
                    entity.CreatorID = tmpuser.ElementAt(0).HREmployee.Id;
                    entity.CreatorName = tmpuser.ElementAt(0).HREmployee.CName;
                }
            }

            if (lismsg.RecLabCode != null && lismsg.RecLabCode.Trim() != "")
            {
                IList<BHospital> tmphospital = IDBHospitalDao.GetListByHQL(" HospitalCode='" + lismsg.RecLabCode.Trim() + "' ");
                if (tmphospital != null && tmphospital.Count() > 0)
                {
                    entity.RecLabID = tmphospital.ElementAt(0).Id;
                    entity.RecLabName = tmphospital.ElementAt(0).Name;
                }
            }
            return DBDao.Save(entity);
        }

        public BaseResultDataValue SCMsgByConfirm(SCMsg Entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (Entity == null)
            {
                brdv.ErrorInfo = "公共消息数据错误！";
                brdv.success = false;
                return brdv;
            }
            var tmpentity = DBDao.Get(Entity.Id);
            Entity.MsgContent = tmpentity.MsgContent;

            //var sendemp = IDHREmployeeDao.Get(tmpentity.SenderID);
            //if (sendemp != null)
            //{
            //    Entity.SendDeptID = sendemp.HRDept.Id;
            //    Entity.SendDeptName = sendemp.HRDept.CName;
            //    Entity.SendDeptCode = sendemp.HRDept.StandCode;//LIS编码
            //}
            //else
            //{
            //    //brb.ErrorInfo = "未能找到发送者所属科室！SenderID=" + Entity.SenderID;
            //    //brb.success = false;
            //    ZhiFang.Common.Log.Log.Error(" BSCMsg.SCMsgByConfirm.entity.未能找到发送者所属科室！SenderID = " + Entity.SenderID);
            //    //return brb;
            //}

            //判断确认人和部门是否有关系
            if (Entity.ConfirmerID <= 0)
            {
                brdv.ErrorInfo = "消息确认人不能为空！";
                brdv.success = false;
                return brdv;
            }
            //if (Entity.ConfirmerName==null || Entity.ConfirmerName.Trim()!="")
            if (Entity.ConfirmerName == null || Entity.ConfirmerName.Trim() == "")
            {
                brdv.ErrorInfo = "消息确认人姓名不能为空！";
                brdv.success = false;
                return brdv;
            }
            if (Entity.RecDeptCode != null && Entity.RecDeptCode.Trim() != "" && Entity.RecDeptID > 0)
            {
                int tmplinkcount = IDCVCriticalValueEmpIdDeptLinkDao.GetListCountByHQL(" EmpID= " + Entity.ConfirmerID + " and DeptID=" + Entity.RecDeptID);
                if (tmplinkcount <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug("SCMsgByConfirm.EmpID= " + Entity.ConfirmerID + " and DeptID=" + Entity.RecDeptID);
                    brdv.ErrorInfo = "消息确认人不在接收部门内部！";
                    brdv.success = false;
                    return brdv;
                }
            }

            #region 获取处理科室医生和确认科室的护士
            List<string> remsgempid = new List<string>();
            var tmplinkd = IDCVCriticalValueEmpIdDeptLinkDao.GetListByHQL(" DeptId=" + tmpentity.RecDeptID + " ");
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
                    ZhiFang.Common.Log.Log.Debug($"AddSCMsgZF_LAB_START_CV.部门id：{tmpentity.RecDeptID}下没有角色为DeveCode：{ZFSystemRole.智方_系统角色_医生.Value.Code}的员工！");
                }
                else
                {
                    emplistd.ToList().ForEach(a => remsgempid.Add(a.HREmployee.Id.ToString()));
                }
            }

            var tmplinkn = IDCVCriticalValueEmpIdDeptLinkDao.GetListByHQL(" DeptId=" + tmpentity.ConfirmDeptID + " ");
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
                    ZhiFang.Common.Log.Log.Debug($"AddSCMsgZF_LAB_START_CV.部门id：{tmpentity.RecDeptID}下没有角色为DeveCode：{ZFSystemRole.智方_系统角色_护士.Value.Code}的员工！");
                }
                else
                {
                    emplistd.ToList().ForEach(a => remsgempid.Add(a.HREmployee.Id.ToString()));
                }
            }

            brdv.ResultDataValue = string.Join(",", remsgempid);

            #endregion

            #region 准备参数
            List<string> para = new List<string>();
            if (Entity.ConfirmerID > 0)
            {
                para.Add("ConfirmerID=" + Entity.ConfirmerID);
                para.Add("NotifyDoctorByEmpID=" + Entity.ConfirmerID);
            }

            if (Entity.ConfirmerName != null && Entity.ConfirmerName.Trim() != "")
            {
                para.Add("ConfirmerName='" + Entity.ConfirmerName + "' ");
                para.Add("NotifyDoctorByEmpName='" + Entity.ConfirmerName + "' ");
            }

            if (Entity.ConfirmerCode != null && Entity.ConfirmerCode.Trim() != "")
            {
                para.Add("ConfirmerCode='" + Entity.ConfirmerCode + "' ");
            }

            if (Entity.ConfirmerCodeHIS != null && Entity.ConfirmerCodeHIS.Trim() != "")
            {
                para.Add("ConfirmerCodeHIS='" + Entity.ConfirmerCodeHIS + "' ");
            }

            if (Entity.ConfirmIPAddress != null && Entity.ConfirmIPAddress.Trim() != "")
            {
                para.Add("ConfirmIPAddress='" + Entity.ConfirmIPAddress + "' ");
            }

            if (Entity.ConfirmMemo != null && Entity.ConfirmMemo.Trim() != "")
            {
                para.Add("ConfirmMemo='" + Entity.ConfirmMemo + "' ");
            }

            if (Entity.ConfirmNotifyDoctorName != null && Entity.ConfirmNotifyDoctorName.Trim() != "")
            {
                para.Add("ConfirmNotifyDoctorName='" + Entity.ConfirmNotifyDoctorName + "' ");
            }

            if (Entity.ConfirmNotifyDoctorCode != null && Entity.ConfirmNotifyDoctorCode.Trim() != "")
            {
                para.Add("ConfirmNotifyDoctorCode='" + Entity.ConfirmNotifyDoctorCode + "' ");
            }

            if (Entity.ConfirmNotifyDoctorCodeHIS != null && Entity.ConfirmNotifyDoctorCodeHIS.Trim() != "")
            {
                para.Add("ConfirmNotifyDoctorCodeHIS='" + Entity.ConfirmNotifyDoctorCodeHIS + "' ");
            }

            if (Entity.ConfirmNotifyDoctorID > 0)
            {
                para.Add("ConfirmNotifyDoctorID=" + Entity.ConfirmNotifyDoctorID + " ");
            }

            if (Entity.DataAddTime.HasValue)
            {
                para.Add("ConfirmUseTime=" + Math.Round((DateTime.Now - Entity.DataAddTime.Value).TotalSeconds, 0).ToString() + " ");
            }

            //para.Add("ConfirmDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            para.Add("NotifyDoctorLastDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            #region 计算要求处理时间
            int tsint = 5;
            var bplist = IDBParameterDao.GetListByHQL($" ParaNo='{SystemParameter_LIIP.消息要求处理时间间隔_分钟.Value.Code}'");  
            if (bplist != null && bplist.Count > 0)
            {
                var ts = bplist.First().ParaValue;
                if (int.TryParse(ts, out int t))
                {
                    tsint = t;
                }
            }

            para.Add("RequireHandleTime='" + DateTime.Now.AddMinutes(tsint).ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            para.Add("LastRequireHandleTime='" + DateTime.Now.AddMinutes(tsint).ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            #endregion

            if (Entity.NotifyDoctorCount.HasValue)
                para.Add("NotifyDoctorCount=NotifyDoctorCount+1 ");
            else
                para.Add("NotifyDoctorCount=1 ");

            para.Add("ConfirmDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            para.Add("ConfirmFlag=1");
            #endregion

            brdv.success = (DBDao.UpdateByHql(" update SCMsg set  " + string.Join(",", para) + "  where Id=" + Entity.Id) > 0);
            return brdv;
        }

        public BaseResultDataValue SCMsgByConfirmNotify(SCMsg Entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (Entity == null)
            {
                brdv.ErrorInfo = "公共消息数据错误！";
                brdv.success = false;
                return brdv;
            }
            var tmpentity = DBDao.Get(Entity.Id);
            if (tmpentity == null)
            {
                brdv.ErrorInfo = "消息数据错误不存在！";
                brdv.success = false;
                return brdv;
            }


            if (Entity.NotifyDoctorByEmpID <= 0)
            {
                brdv.ErrorInfo = "消息提醒通知人不能为空！";
                brdv.success = false;
                return brdv;
            }
            if (string.IsNullOrEmpty(Entity.NotifyDoctorByEmpName))
            {
                brdv.ErrorInfo = "消息提醒通知人姓名不能为空！";
                brdv.success = false;
                return brdv;
            }
            //if (Entity.RecDeptCode != null && Entity.RecDeptCode.Trim() != "" && Entity.RecDeptID > 0)
            //{
            //    int tmplinkcount = IDCVCriticalValueEmpIdDeptLinkDao.GetListCountByHQL(" EmpID= " + Entity.ConfirmerID + " and DeptID=" + Entity.RecDeptID);
            //    if (tmplinkcount <= 0)
            //    {
            //        ZhiFang.Common.Log.Log.Debug("SCMsgByConfirm.EmpID= " + Entity.ConfirmerID + " and DeptID=" + Entity.RecDeptID);
            //        brdv.ErrorInfo = "消息确认人不在接收部门内部！";
            //        brdv.success = false;
            //        return brdv;
            //    }
            //}

            #region 获取处理科室医生和确认科室的护士
            List<string> remsgempid = new List<string>();
            var tmplinkd = IDCVCriticalValueEmpIdDeptLinkDao.GetListByHQL(" DeptId=" + tmpentity.RecDeptID + " ");
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
                    ZhiFang.Common.Log.Log.Debug($"AddSCMsgZF_LAB_START_CV.部门id：{tmpentity.RecDeptID}下没有角色为DeveCode：{ZFSystemRole.智方_系统角色_医生.Value.Code}的员工！");
                }
                else
                {
                    emplistd.ToList().ForEach(a => remsgempid.Add(a.HREmployee.Id.ToString()));
                }
            }

            var tmplinkn = IDCVCriticalValueEmpIdDeptLinkDao.GetListByHQL(" DeptId=" + tmpentity.ConfirmDeptID + " ");
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
                    ZhiFang.Common.Log.Log.Debug($"AddSCMsgZF_LAB_START_CV.部门id：{tmpentity.RecDeptID}下没有角色为DeveCode：{ZFSystemRole.智方_系统角色_护士.Value.Code}的员工！");
                }
                else
                {
                    emplistd.ToList().ForEach(a => remsgempid.Add(a.HREmployee.Id.ToString()));
                }
            }

            brdv.ResultDataValue = string.Join(",", remsgempid);

            #endregion

            #region 准备参数
            List<string> para = new List<string>();

            para.Add("NotifyDoctorByEmpID=" + Entity.NotifyDoctorByEmpID);
            para.Add("NotifyDoctorByEmpName='" + Entity.NotifyDoctorByEmpName + "' ");
            para.Add("NotifyDoctorLastDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            if (tmpentity.NotifyDoctorCount.HasValue)
                para.Add("NotifyDoctorCount=NotifyDoctorCount+1 ");
            else
                para.Add("NotifyDoctorCount=1 ");

            int tsint = 5;
            var bplist = IDBParameterDao.GetListByHQL($" ParaNo='{SystemParameter_LIIP.消息要求处理时间间隔_分钟.Value.Code}'");
            if (bplist != null && bplist.Count > 0)
            {
                var ts = bplist.First().ParaValue;
                if (int.TryParse(ts, out int t))
                {
                    tsint = t;
                }
            }
            para.Add("LastRequireHandleTime='" + DateTime.Now.AddMinutes(tsint).ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            #endregion

            brdv.success = (DBDao.UpdateByHql(" update SCMsg set  " + string.Join(",", para) + "  where Id=" + Entity.Id) > 0);
            return brdv;
        }

        public BaseResultDataValue SCMsgByTimeOutReSend(SCMsg Entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (Entity == null)
            {
                brdv.ErrorInfo = "公共消息数据错误！";
                brdv.success = false;
                return brdv;
            }

            var tmpentity = DBDao.Get(Entity.Id);
            brdv.ResultDataValue = tmpentity.RecDeptID.ToString();
            if (tmpentity == null)
            {
                ZhiFang.Common.Log.Log.Debug("SCMsgByTimeOutReSend.公共消息数据不存在！entity.Id=" + Entity.Id);
                brdv.ErrorInfo = "公共消息数据不存在！";
                brdv.success = false;
                return brdv;
            }

            //var sendemp = IDHREmployeeDao.Get(tmpentity.SenderID);
            //if (sendemp != null)
            //{
            //    Entity.SendDeptID = sendemp.HRDept.Id;
            //    Entity.SendDeptName = sendemp.HRDept.CName;
            //    Entity.SendDeptCode = sendemp.HRDept.StandCode;//LIS编码
            //}
            //else
            //{
            //    brdv.ErrorInfo = "未能找到发送者所属科室！SenderID=" + Entity.SenderID;
            //    brdv.success = false;
            //    ZhiFang.Common.Log.Log.Error(" BSCMsg.SCMsgByTimeOutReSend.entity.未能找到发送者所属科室！SenderID = " + Entity.SenderID);
            //    return brdv;
            //}

            #region 准备参数
            List<string> para = new List<string>();

            if (Entity.TimeOutCallUserID > 0)
            {
                para.Add("TimeOutCallUserID=" + Entity.TimeOutCallUserID);
            }

            if (Entity.TimeOutCallUserName != null && Entity.TimeOutCallUserName.Trim() != "")
            {
                para.Add("TimeOutCallUserName='" + Entity.TimeOutCallUserName.Trim() + "'");
            }

            if (Entity.TimeOutCallRecUserID > 0)
            {
                para.Add("TimeOutCallRecUserID=" + Entity.TimeOutCallRecUserID);
            }

            if (Entity.TimeOutCallRecUserName != null && Entity.TimeOutCallRecUserName.Trim() != "")
            {
                para.Add("TimeOutCallRecUserName='" + Entity.TimeOutCallRecUserName.Trim() + "'");
            }
            para.Add("TimeOutCallDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            para.Add("TimeOutCallFlag=1");

            #endregion

            brdv.success = (DBDao.UpdateByHql(" update SCMsg set  " + string.Join(",", para) + "  where Id=" + Entity.Id) > 0);
            return brdv;
        }

        public bool SCMsgByWarningUpload(string[] tempArray)
        {
            if (this.Entity != null && this.Entity.RecDeptID > 0 && this.Entity.RecDeptPhoneCode != null && this.Entity.RecDeptPhoneCode.Trim() != "")
            {
                IDHRDeptDao.UpdateByHql(" update HRDept set Tel='" + this.Entity.RecDeptPhoneCode.Trim() + "' where Id=" + Entity.RecDeptID);
            }
            return base.Update(tempArray);
        }

        public EntityList<SCMsg> SearchLabMsgByLabCodeList(string where, int page, int limit, string labCodeList)
        {
            IList<BHospital> BHospitallist = IDBHospitalDao.GetListByHQL(" HospitalCode in ('" + string.Join("','", labCodeList.Trim()) + "') ");
            if (BHospitallist == null || BHospitallist.Count == 0)
            {
                return null;
            }

            List<long> Hospital = new List<long>();
            BHospitallist.ToList().ForEach(a =>
            {
                Hospital.Add(a.Id);
            });
            return DBDao.GetListByHQL(where + " and RecLabID in (" + string.Join(",", Hospital) + ") ", " MsgTypeID ASC, DataAddTime DESC", page, limit);
        }

        public EntityList<SCMsg> SearchSCMsgByHQLAndAccountPWD(string where, int page, int limit, string account, string PWDbase)
        {
            long empid = 0;
            IList<RBACUser> tempRBACUser = IDRBACUserDao.SearchRBACUserByUserAccount(account);
            if (tempRBACUser.Count <= 0)
            {
                throw new Exception("\"" + account + "\"账户不存在！");
            }
            if (!tempRBACUser[0].IsUse.HasValue || !tempRBACUser[0].IsUse.Value)
            {
                throw new Exception("\"" + account + "\"账户被禁用！");
            }
            if (!(tempRBACUser[0].HREmployee.IsUse.HasValue && tempRBACUser[0].HREmployee.IsUse.Value && tempRBACUser[0].HREmployee.IsEnabled == 1))
            {
                throw new Exception("\"" + account + "\"人员被禁用！");
            }
            PWDbase = SecurityHelp.MD5Encrypt(PWDbase, SecurityHelp.PWDMD5Key);
            if ((tempRBACUser[0].Account == account) && (tempRBACUser[0].PWD == PWDbase) && (!tempRBACUser[0].AccLock))
            {
                empid = tempRBACUser.ElementAt(0).HREmployee.Id;
            }

            IList<BHospitalEmpLink> BHospitalEmpLinklist = IDBHospitalEmpLinkDao.GetListByHQL(" EmpID=" + empid + " ");
            if (BHospitalEmpLinklist == null || BHospitalEmpLinklist.Count == 0)
            {
                return null;
            }

            List<long> Hospital = new List<long>();
            BHospitalEmpLinklist.ToList().ForEach(a =>
            {
                if (a.HospitalID.HasValue)
                {
                    Hospital.Add(a.HospitalID.Value);
                }
            });
            return DBDao.GetListByHQL(where + " and RecLabID in (" + string.Join(",", Hospital) + ") ", " MsgTypeID ASC, DataAddTime DESC", page, limit);
        }

        public EntityList<SCMsg> SearchSCMsgByHQLAndLabCode(string where, int page, int limit, string empid)
        {
            IList<BHospitalEmpLink> BHospitalEmpLinklist = IDBHospitalEmpLinkDao.GetListByHQL(" EmpID=" + empid + " ");
            if (BHospitalEmpLinklist == null || BHospitalEmpLinklist.Count == 0)
            {
                return null;
            }

            List<long> Hospital = new List<long>();
            BHospitalEmpLinklist.ToList().ForEach(a =>
            {
                if (a.HospitalID.HasValue)
                {
                    Hospital.Add(a.HospitalID.Value);
                }
            });
            return DBDao.GetListByHQL(where + " and RecLabID in (" + string.Join(",", Hospital) + ") ", " MsgTypeID ASC, DataAddTime DESC", page, limit);
        }

        public EntityList<SCMsg> SearchSCMsgAndSCMsgHandleByHQL(string where, string Order, int page, int limit)
        {
            EntityList<SCMsg> entitylist = new EntityList<SCMsg>();
            if (!string.IsNullOrEmpty(Order))
            {
                entitylist = base.SearchListByHQL(where, Order, page, limit);
            }
            else
            {
                entitylist = base.SearchListByHQL(where, page, limit);
            }
            if (entitylist != null && entitylist.list != null && entitylist.list.Count > 0)
            {
                List<string> msgidlist = new List<string>();
                entitylist.list.ToList().ForEach(a =>
                {
                    msgidlist.Add(a.Id.ToString());
                });

                var handlelist = IDSCMsgHandleDao.GetListByHQL(" MsgID in (" + string.Join(",", msgidlist) + ") ");
                if (handlelist != null && handlelist.Count > 0)
                {
                    entitylist.list.ToList().ForEach(a =>
                    {
                        if (handlelist.Count(b => b.MsgID == a.Id) > 0)
                        {
                            a.SCMsgHandle = handlelist.Where(b => b.MsgID == a.Id).First();
                        }
                    });
                }
            }


            return entitylist;
        }


    }
}