using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.OA;
using ZhiFang.Entity.OA;
using ZhiFang.BLL.Base;
using ZhiFang.ProjectProgressMonitorManage.Common;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.OA.ViewObject.Response;
using ZhiFang.Entity.RBAC.ViewObject.Response;
using ZhiFang.Entity.Base;
using System.Data;
using System.Reflection;
using System.Collections;
using ZhiFang.IBLL.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using System.IO;
using ZhiFang.IBLL.OA;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Request;
using System.Diagnostics;

namespace ZhiFang.BLL.OA
{
    /// <summary>
    /// 员工考勤事件日志表
    /// </summary>
    public class BATEmpAttendanceEventLog : BaseBLL<ATEmpAttendanceEventLog>, ZhiFang.IBLL.OA.IBATEmpAttendanceEventLog
    {
        public IBBParameter IBBParameter { get; set; }
        public ZhiFang.IDAO.RBAC.IDHREmployeeDao IDHREmployeeDao { get; set; }
        public ZhiFang.IDAO.OA.IDATApproveStatusDao IDATApproveStatusDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDHRDeptDao IDHRDeptDao { get; set; }
        public IBBWeiXinAccount IBBWeiXinAccount { set; get; }

        public IBATHolidaySetting IBATHolidaySetting { set; get; }
        public IBATEmpAttendanceEventParaSettings IBATEmpAttendanceEventParaSettings { set; get; }

        public ZhiFang.IDAO.RBAC.IDRBACUserDao IDRBACUserDao { get; set; }

        public bool AddSignIn()
        {
            if (Entity != null)
            {
                this.Entity.ATEventTypeID = ATTypeId.P签到;
                this.Entity.ATEventTypeName = "签到";
                this.Entity.ATEventDateCode = DateTime.Now.ToString("yyyy-MM-dd");
                //处理签到时间与考勤设置上班时间是正常还是迟到
                ATEmpAttendanceEventLogCheckSignInTime(null);

                #region 原来代码
                //if (DateTime.Now <= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 09:00:00"))
                //{
                //    this.Entity.ATEventSubTypeID = ATTypeId.P签到;
                //    this.Entity.ATEventSubTypeName = "签到正常";
                //}
                //else
                //{
                //    this.Entity.ATEventSubTypeID = ATTypeId.迟到;
                //    this.Entity.ATEventSubTypeName = "签到迟到";
                //} 
                #endregion

                //是否脱岗判断                
                this.Entity.IsOffsite = false;
                //处理签到地点与考勤设置的考勤地点是否脱岗
                ATEmpAttendanceEventLogCheckPostion(null, ATEventPostionType.ATEventPostion);
                bool a = DBDao.Save(this.Entity);
                return a;
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("AddSignIn.Entity为空");
                return false;
            }
        }

        public bool AddSignOut()
        {
            if (Entity != null)
            {
                this.Entity.ATEventTypeID = ATTypeId.P签退;
                this.Entity.ATEventTypeName = "签退";
                //处理签退时间与考勤设置下班时间是正常还是早退
                ATEmpAttendanceEventLogCheckSignOutTime(null);

                #region 原来代码
                //if (DateTime.Now >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 18:00:00"))
                //{
                //    this.Entity.ATEventSubTypeID = ATTypeId.P签退;
                //    this.Entity.ATEventSubTypeName = "签退正常";
                //}
                //else
                //{
                //    this.Entity.ATEventSubTypeID = ATTypeId.早退;
                //    this.Entity.ATEventSubTypeName = "签退早退";
                //} 
                #endregion

                this.Entity.ATEventDateCode = DateTime.Now.ToString("yyyy-MM-dd");
                //是否脱岗判断                
                this.Entity.IsOffsite = false;
                //处理签到地点与考勤设置的考勤地点是否脱岗
                ATEmpAttendanceEventLogCheckPostion(null, ATEventPostionType.ATEventPostion);
                bool a = DBDao.Save(this.Entity);
                return a;
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("AddSignOut.Entity为空");
                return false;
            }
        }

        public bool AddUploadPostion()
        {
            if (Entity != null)
            {
                this.Entity.ATEventTypeID = ATTypeId.P上报位置;
                this.Entity.ATEventTypeName = "上报位置";
                this.Entity.ATEventSubTypeID = ATTypeId.P上报位置;
                this.Entity.ATEventSubTypeName = "上报位置";

                this.Entity.ATEventDateCode = DateTime.Now.ToString("yyyy-MM-dd");
                //是否脱岗判断                
                this.Entity.IsOffsite = false;
                bool a = DBDao.Save(this.Entity);
                return a;
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("AddUploadPostion.Entity为空");
                return false;
            }
        }
        /// <summary>
        /// 员工考勤上报位置并与考勤设置进行验证,判断是否可以上报事件
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue AddUploadPostionAndCheck()
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (Entity != null)
            {
                this.Entity.ATEventTypeID = ATTypeId.P上报位置;
                this.Entity.ATEventTypeName = "上报位置";
                this.Entity.ATEventSubTypeID = ATTypeId.P上报位置;
                this.Entity.ATEventSubTypeName = "上报位置";

                this.Entity.ATEventDateCode = DateTime.Now.ToString("yyyy-MM-dd");
                //是否脱岗判断                
                this.Entity.IsOffsite = false;
                bool isCheck = false;
                #region 处理签到地点与考勤设置的考勤地点是否脱岗
                ZhiFang.Common.Log.Log.Debug("AddUploadPostionAndCheck上报位置判断处理开始:");
                ATEmpAttendanceEventParaSettings empSetting = null;
                empSetting = IBATEmpAttendanceEventParaSettings.SearchATEmpAttendanceEventParaSettingsByEmpId(long.Parse(Entity.ApplyID.ToString()));
                if (empSetting != null)
                {
                    #region 判断当前时间是在设置时间一到设置时间五的哪一个范围内
                    DateTime setDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                    DateTime curDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                    //上报时间范围(分钟)
                    int setMinute = Math.Abs(empSetting.TimingTimeRange);
                    //上报地点类型
                    int postionType = 0;
                    DateTime setTime = setDate;
                    ZhiFang.Common.Log.Log.Error("empSetting.考勤设置的上报时间范围为:" + setMinute + "分钟");
                    if (empSetting.TimingOneTime.HasValue)
                    {
                        DateTime tempTime = DateTime.Parse(empSetting.TimingOneTime.Value.ToString("HH:mm"));
                        setTime = setTime.AddHours(tempTime.Hour).AddMinutes(tempTime.Minute);
                        TimeSpan subDate = curDate.Subtract(setTime);
                        //当前时间是否在设置时间一的范围内
                        double totalMinutes = Math.Abs(subDate.TotalMinutes);
                        ZhiFang.Common.Log.Log.Error("当前时间减设置时间一的分钟为:" + totalMinutes);
                        if (totalMinutes <= setMinute)
                        {
                            postionType = 1;
                            isCheck = true;
                        }
                    }
                    if (isCheck == false && empSetting.TimingTwoTime.HasValue)
                    {
                        DateTime tempTime = DateTime.Parse(empSetting.TimingTwoTime.Value.ToString("HH:mm"));
                        setTime = setDate;
                        setTime = setTime.AddHours(tempTime.Hour).AddMinutes(tempTime.Minute);
                        TimeSpan subDate = curDate.Subtract(setTime);
                        //当前时间是否在设置时间二的范围内
                        double totalMinutes = Math.Abs(subDate.TotalMinutes);
                        ZhiFang.Common.Log.Log.Error("当前时间减设置时间二的分钟为:" + totalMinutes);
                        if (totalMinutes <= setMinute)
                        {
                            postionType = 2;
                            isCheck = true;
                        }
                    }
                    if (isCheck == false && empSetting.TimingThreeTime.HasValue)
                    {
                        DateTime tempTime = DateTime.Parse(empSetting.TimingThreeTime.Value.ToString("HH:mm"));
                        setTime = setDate;
                        setTime = setTime.AddHours(tempTime.Hour).AddMinutes(tempTime.Minute);
                        TimeSpan subDate = curDate.Subtract(setTime);
                        //当前时间是否在设置时间三的范围内
                        double totalMinutes = Math.Abs(subDate.TotalMinutes);
                        ZhiFang.Common.Log.Log.Error("当前时间减设置时间三的分钟为:" + totalMinutes);
                        if (totalMinutes <= setMinute)
                        {
                            postionType = 3;
                            isCheck = true;
                        }
                    }
                    if (isCheck == false && empSetting.TimingFourTime.HasValue)
                    {
                        DateTime tempTime = DateTime.Parse(empSetting.TimingFourTime.Value.ToString("HH:mm"));
                        setTime = setDate;
                        setTime = setTime.AddHours(tempTime.Hour).AddMinutes(tempTime.Minute);
                        TimeSpan subDate = curDate.Subtract(setTime);
                        //当前时间是否在设置时间四的范围内
                        double totalMinutes = Math.Abs(subDate.TotalMinutes);
                        ZhiFang.Common.Log.Log.Error("当前时间减设置时间四的分钟为:" + totalMinutes);
                        if (totalMinutes <= setMinute)
                        {
                            postionType = 4;
                            isCheck = true;
                        }
                    }
                    if (isCheck == false && empSetting.TimingFiveTime.HasValue)
                    {
                        DateTime tempTime = DateTime.Parse(empSetting.TimingFiveTime.Value.ToString("HH:mm"));
                        setTime = setDate;
                        setTime = setTime.AddHours(tempTime.Hour).AddMinutes(tempTime.Minute);
                        TimeSpan subDate = curDate.Subtract(setTime);
                        //当前时间是否在设置时间五的范围内
                        double totalMinutes = Math.Abs(subDate.TotalMinutes);
                        ZhiFang.Common.Log.Log.Error("当前时间减设置时间五的分钟为:" + totalMinutes);
                        if (totalMinutes <= setMinute)
                        {
                            postionType = 5;
                            isCheck = true;
                        }
                    }
                    #endregion
                    ZhiFang.Common.Log.Log.Debug("AddUploadPostionAndCheck上报位置类型为:" + (ATEventPostionType)postionType);
                    if (isCheck)
                        ATEmpAttendanceEventLogCheckPostion(empSetting, (ATEventPostionType)postionType);
                    if (isCheck)
                    {
                        tempBaseResultDataValue.success = DBDao.Save(this.Entity);
                        if (tempBaseResultDataValue.success == false)
                            tempBaseResultDataValue.ErrorInfo = "保存上报位置失败!";
                    }
                    else
                    {
                        tempBaseResultDataValue.ErrorInfo = "当前上报时间点不符合考勤设置的上报时点";
                    }
                    ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo);
                }
                else
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = this.Entity.ApplyName + "考勤设置未设置上报位置信息!";
                    ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo);
                }
                #endregion
            }
            else
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "AddUploadPostionAndCheck.Entity为空";
                ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo);
            }
            return tempBaseResultDataValue;
        }
        public ZhiFang.Entity.OA.ViewObject.Response.SignInfo GetSignInfoBydtcode(string dtcode, long empid)
        {
            ZhiFang.Entity.OA.ViewObject.Response.SignInfo signinfo = new ZhiFang.Entity.OA.ViewObject.Response.SignInfo();
            signinfo.WeekInfo = DateTimeHelp.GetDateWeek(Convert.ToDateTime(dtcode));
            signinfo.ATEventDateCode = dtcode;

            if (Convert.ToDateTime(dtcode).DayOfWeek != DayOfWeek.Saturday && Convert.ToDateTime(dtcode).DayOfWeek != DayOfWeek.Sunday)
                signinfo.IsWorkDay = true;
            else
                signinfo.IsWorkDay = false;
            var list = DBDao.GetListByHQL(" ATEventDateCode = '" + dtcode + "' and ATEventTypeID in (" + ATTypeId.P签到 + "," + ATTypeId.P签退 + ") and  ApplyID=" + empid);
            if (list != null && list.Count > 0)
            {
                var signin = list.Where(s => s.ATEventTypeID == ATTypeId.P签到);
                if (signin.Count() > 0)
                {
                    signinfo.SignInId = signin.ElementAt(0).Id.ToString();
                    signinfo.SignInTime = signin.ElementAt(0).DataAddTime.Value.ToString("HH:mm");
                    signinfo.SignInMemo = signin.ElementAt(0).Memo;
                    if (signin.ElementAt(0).ATEventSubTypeID == ATTypeId.迟到)
                        signinfo.SignInType = signin.ElementAt(0).ATEventSubTypeName;
                    signinfo.SignInIsOffsite = signin.ElementAt(0).IsOffsite;
                }

                var signout = list.Where(s => s.ATEventTypeID == ATTypeId.P签退);
                if (signout.Count() > 0)
                {
                    signinfo.SignOutId = signout.ElementAt(0).Id.ToString();
                    signinfo.SignOutTime = signout.ElementAt(0).DataAddTime.Value.ToString("HH:mm");
                    signinfo.SignOutMemo = signout.ElementAt(0).Memo;
                    if (signout.ElementAt(0).ATEventSubTypeID == ATTypeId.早退)
                        signinfo.SignOutType = signout.ElementAt(0).ATEventSubTypeName;
                    signinfo.SignOutIsOffsite = signout.ElementAt(0).IsOffsite;
                }
            }
            return signinfo;
        }

        public IList<SignLog> SearchATEmpSignLog(string sd, string ed, int type, long empid)
        {
            List<SignLog> sllist = new List<SignLog>();
            var list = DBDao.GetListByHQL(" DataAddTime >= '" + sd + "' and DataAddTime<='" + ed + " 23:59:59' and  ATEventTypeID in (" + ATTypeId.P签到 + "," + ATTypeId.P签退 + ") and  ApplyID=" + empid + " order by ATEventDateCode desc ");
            DateTime sdt = Convert.ToDateTime(sd);
            DateTime edt = Convert.ToDateTime(ed);

            //获取该月份的所有工作日期的集合
            Dictionary<string, DayOfWeek> allWorkDays = IBATHolidaySetting.GetAllWorkDaysOfOneMonth(edt.Year, edt.Month);

            while (edt >= sdt)
            {
                var signinfoday = list.Where(a => a.ATEventDateCode == edt.ToString("yyyy-MM-dd"));
                ZhiFang.Entity.OA.ViewObject.Response.SignLog signlog = new ZhiFang.Entity.OA.ViewObject.Response.SignLog();
                signlog.WeekInfo = DateTimeHelp.GetDateWeek(edt);
                signlog.ATEventDateCode = edt.ToString("yyyy-MM-dd");
                //是否工作日处理
                if (allWorkDays.ContainsKey(edt.ToString("yyyy-MM-dd")))
                    signlog.IsWorkDay = true;
                else
                    signlog.IsWorkDay = false;

                //if (edt.DayOfWeek != DayOfWeek.Saturday && edt.DayOfWeek != DayOfWeek.Sunday)
                //    signlog.IsWorkDay = true;
                //else
                //    signlog.IsWorkDay = false;

                if (signinfoday.Count() > 0)
                {
                    var signin = signinfoday.Where(s => s.ATEventTypeID == ATTypeId.P签到);
                    if (signin.Count() > 0)
                    {
                        signlog.SignInId = signin.ElementAt(0).Id.ToString();
                        signlog.SignInTime = signin.ElementAt(0).DataAddTime.Value.ToString("HH:mm");
                        signlog.SignInMemo = signin.ElementAt(0).Memo;
                        if (signin.ElementAt(0).ATEventSubTypeID == ATTypeId.迟到)
                            signlog.SignInType = signin.ElementAt(0).ATEventSubTypeName;
                        signlog.SignInIsOffsite = signin.ElementAt(0).IsOffsite;
                        signlog.SigninATEventLogPostion = signin.ElementAt(0).ATEventLogPostion;
                        signlog.SigninATEventLogPostionName = signin.ElementAt(0).ATEventLogPostionName;
                    }

                    var signout = signinfoday.Where(s => s.ATEventTypeID == ATTypeId.P签退);
                    if (signout.Count() > 0)
                    {
                        signlog.SignOutId = signout.ElementAt(0).Id.ToString();
                        signlog.SignOutTime = signout.ElementAt(0).DataAddTime.Value.ToString("HH:mm");
                        signlog.SignOutMemo = signout.ElementAt(0).Memo;
                        if (signout.ElementAt(0).ATEventSubTypeID == ATTypeId.早退)
                            signlog.SignOutType = signout.ElementAt(0).ATEventSubTypeName;
                        signlog.SignOutIsOffsite = signout.ElementAt(0).IsOffsite;
                        signlog.SignoutATEventLogPostion = signout.ElementAt(0).ATEventLogPostion;
                        signlog.SignoutATEventLogPostionName = signout.ElementAt(0).ATEventLogPostionName;
                    }
                }
                sllist.Add(signlog);
                edt = edt.AddDays(-1);
            }
            return sllist;
        }

        public IList<SignLog> SearchATEmpSignLogByLimit(string ed, int limit, int type, long empid)
        {
            if (limit <= 0)
                limit = 1;
            DateTime edt = Convert.ToDateTime(ed);
            DateTime sdt = edt.AddDays(-1 * (limit - 1));//当天传1
            ZhiFang.Common.Log.Log.Debug("SearchATEmpSignLogByLimit,ed:" + ed + "@sdt:" + sdt.ToString("yyyy-MM-dd") + "@edt:" + edt.ToString("yyyy-MM-dd") + "@a:" + (-1 * (limit - 1)));
            return SearchATEmpSignLog(sdt.ToString("yyyy-MM-dd"), ed, type, empid);
        }

        public IList<ATEmpAttendanceEventLog> GetInfoBydtcode(string dtcode, string empid)
        {
            var list = DBDao.GetListByHQL(" ATEventDateCode = '" + dtcode + "' and  ApplyID=" + empid);
            if (list != null && list.Count > 0)
            {
                return list;
            }
            return null;
        }
        /// <summary>
        /// 获取部门管理者
        /// </summary>
        /// <param name="DeptId"></param>
        /// <returns></returns>
        public HREmployee GetATEmpAttendanceEventApproveByDeptId(long DeptId)
        {
            IList<HREmployee> hremplist = IDHREmployeeDao.GetListByHQL(" HRDept.Id=" + DeptId + " ");
            if (hremplist != null && hremplist.Count > 0)
            {
                var emplist = hremplist.Where(a => a.RBACEmpRoleList.Count(r => r.RBACRole.UseCode == "R1003") > 0);
                if (emplist.Count() > 0)
                {
                    return emplist.ElementAt(0);
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取直属领导
        /// </summary>
        /// <param name="DeptId"></param>
        /// <returns></returns>
        public HREmployee GetATEmpAttendanceEventApproveByEmpId(long EmpId)
        {
            HREmployee emp = IDHREmployeeDao.Get(EmpId);
            if (emp.ManagerID.HasValue && emp.ManagerID.Value > 0)
            {
                HREmployee manageemp = IDHREmployeeDao.Get(emp.ManagerID.Value);
                if (manageemp != null)
                {
                    return manageemp;
                }
            }
            return null;
        }

        /// <summary>
        /// 计算相隔天数刨去节假日
        /// </summary>
        /// <param name="sd">开始时间</param>
        /// <param name="ed">结束时间</param>
        /// <returns></returns>
        public float GetATEmpAttendanceEventDayCount(string sd, string ed)
        {
            DateTime dts = Convert.ToDateTime(sd);
            DateTime dte = Convert.ToDateTime(ed);

            //获取该月份的所有工作日期的集合
            //Dictionary<string, DayOfWeek> allWorkDays = IBATHolidaySetting.GetAllWorkDaysOfOneMonth(dte.Year, dte.Month);

            if (dts > dte)
            {
                throw new Exception("开始时间大于结束时间！");
            }
            if (dts.ToString("yyyy-MM-dd").Trim() == dte.ToString("yyyy-MM-dd").Trim())
            {
                if (dts.DayOfWeek != DayOfWeek.Sunday && dts.DayOfWeek != DayOfWeek.Saturday)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                DateTime tdts = dts.Date;
                DateTime tdte = dte.Date;
                int i = 1;
                while (tdts < tdte)
                {
                    tdts = tdts.AddDays(1);
                    if (tdts.DayOfWeek != DayOfWeek.Sunday && tdts.DayOfWeek != DayOfWeek.Saturday)
                        i++;
                }
                return i;
            }
        }

        public Double GetATEmpAttendanceEventHourCount(string sd, string ed)
        {
            DateTime dts = Convert.ToDateTime(sd);
            DateTime dte = Convert.ToDateTime(ed);
            if (dts > dte)
            {
                throw new Exception("开始时间大于结束时间！");
            }
            return (dte - dts).TotalHours;
        }

        /// <summary>
        /// 请假,外出,加班,出差申请时,不能补申请当前月一号之前(小于当前月2号01 23:59:59),跨年
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BaseResultBool AddATEmpAttendanceEventCheck(ATEmpAttendanceEventLog entity)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.success = false;
            tempBaseResultBool.BoolFlag = false;
            if (entity == null)
            {
                tempBaseResultBool.ErrorInfo = "申请数据为空！";
                return tempBaseResultBool;
            }
            if (!entity.StartDateTime.HasValue)
            {
                tempBaseResultBool.ErrorInfo = "申请开始时间为空！";
                return tempBaseResultBool;
            }
            if (entity.StartDateTime.HasValue && entity.EndDateTime.HasValue)
            {
                if (DateTime.Compare(entity.StartDateTime.Value, entity.EndDateTime.Value) > 0)
                {
                    tempBaseResultBool.ErrorInfo = "申请开始时间大于结束时间！";
                    return tempBaseResultBool;
                }
            }

            if (entity.StartDateTime.Value < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01 00:00:00")))
            {
                if (DateTime.Now.Day > 1)
                {
                    tempBaseResultBool.ErrorInfo = "当前时间不能提交" + DateTime.Now.ToString("yyyy-MM-01") + "前的申请！";
                    return tempBaseResultBool;
                }
            }
            tempBaseResultBool.success = true;
            tempBaseResultBool.BoolFlag = true;
            return tempBaseResultBool;
        }

        /// <summary>
        /// 请假并验证是否允许申请
        /// </summary>
        /// <param name="pushWeiXinMessageAction"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BaseResultBool AddAndCheckATEmpAttendanceEventleaveevent(SysWeiXinTemplate.PushWeiXinMessage
 pushWeiXinMessageAction, ATEmpAttendanceEventLog entity)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            entity.ATEventTypeID = ATTypeId.P请假;
            entity.ATEventTypeName = "请假";
            tempBaseResultBool = AddATEmpAttendanceEventCheck(entity);

            if (tempBaseResultBool.BoolFlag)
            {
                tempBaseResultBool.BoolFlag = AddATEmpAttendanceEventleaveevent(pushWeiXinMessageAction, entity);
            }
            else
            {
                tempBaseResultBool.BoolFlag = false;
            }
            tempBaseResultBool.success = tempBaseResultBool.BoolFlag;
            return tempBaseResultBool;
        }

        public bool AddATEmpAttendanceEventleaveevent(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, ATEmpAttendanceEventLog entity)
        {
            entity.ATEventTypeID = ATTypeId.P请假;
            entity.ATEventTypeName = "请假";

            if (DBDao.Save(entity))
            {
                if (entity.ApproveID != null)
                {
                    Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                    data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你好！" + entity.ApplyName + "向你提交了一个新'请假'申请。" });
                    data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = entity.ApplyName });
                    data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = entity.ATEventSubTypeName });
                    string tmpdaterange = "";
                    if (entity.StartDateTime.HasValue && entity.EndDateTime.HasValue)
                    {
                        tmpdaterange = entity.StartDateTime.Value.ToString("yyyy-MM-dd HH:mm") + "至" + entity.EndDateTime.Value.ToString("yyyy-MM-dd HH:mm");
                    }
                    data.Add("keyword3", new TemplateDataObject() { color = "#000000", value = tmpdaterange });
                    data.Add("keyword4", new TemplateDataObject() { color = "#000000", value = entity.Memo });
                    data.Add("remark", new TemplateDataObject() { color = "#1d73cd", value = "请登录微信OA审批。" });
                    IBBWeiXinAccount.PushWeiXinMessage(pushWeiXinMessageAction, new List<long>() { entity.ApproveID.Value }, data, "atleave", "");
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 新增员工考勤外出事件并验证是否允许申请
        /// </summary>
        /// <param name="pushWeiXinMessageAction"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BaseResultBool AddAndCheckATEmpAttendanceEventEgressevent(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, ATEmpAttendanceEventLog entity)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            entity.ATEventDateCode = entity.StartDateTime.Value.ToString("yyyy-MM-dd");
            entity.ATEventTypeID = ATTypeId.P外出;
            entity.ATEventTypeName = "外出";
            entity.ATEventSubTypeID = ATTypeId.P外出;
            entity.ATEventSubTypeName = "外出";
            tempBaseResultBool = AddATEmpAttendanceEventCheck(entity);
            if (tempBaseResultBool.BoolFlag)
            {
                tempBaseResultBool.BoolFlag = AddATEmpAttendanceEventEgressevent(pushWeiXinMessageAction, entity);
            }
            else
            {
                tempBaseResultBool.BoolFlag = false;
            }
            tempBaseResultBool.success = tempBaseResultBool.BoolFlag;
            return tempBaseResultBool;
        }
        public bool AddATEmpAttendanceEventEgressevent(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, ATEmpAttendanceEventLog entity)
        {
            entity.ATEventDateCode = entity.StartDateTime.Value.ToString("yyyy-MM-dd");
            entity.ATEventTypeID = ATTypeId.P外出;
            entity.ATEventTypeName = "外出";
            entity.ATEventSubTypeID = ATTypeId.P外出;
            entity.ATEventSubTypeName = "外出";
            if (entity.StartDateTime.HasValue && entity.EndDateTime.HasValue)
            {
                int range = (entity.EndDateTime.Value - entity.StartDateTime.Value).Hours;
                if (range == 0)
                {
                    range = 1;
                }
                else
                {
                    if (range <= 0)
                    {
                        range = 0;
                    }
                }
                entity.EvenLength = range;
            }
            if (DBDao.Save(entity))
            {
                if (entity.ApproveID != null)
                {
                    Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                    data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你好！" + entity.ApplyName + "向你提交了一个新'外出'申请。" });
                    data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = entity.ApplyName });
                    string tmpmemo = "";
                    if (entity.Memo != null && entity.Memo.Trim() != "")
                    {
                        tmpmemo = "(" + entity.Memo + ")";
                    }
                    data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = entity.ATEventSubTypeName + tmpmemo });
                    string tmpdaterange = "";
                    if (entity.StartDateTime.HasValue && entity.EndDateTime.HasValue)
                    {
                        tmpdaterange = entity.StartDateTime.Value.ToString("yyyy-MM-dd") + "时间" + entity.StartDateTime.Value.ToString("HH:mm") + "至" + entity.EndDateTime.Value.ToString("HH:mm");
                    }
                    data.Add("remark", new TemplateDataObject() { color = "#1d73cd", value = "外出日期：" + tmpdaterange + "." + "请登录微信OA审批。" });
                    IBBWeiXinAccount.PushWeiXinMessage(pushWeiXinMessageAction, new List<long>() { entity.ApproveID.Value }, data, "ategress", "");
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 新增员工考勤出差事件并验证是否允许申请
        /// </summary>
        /// <param name="pushWeiXinMessageAction"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BaseResultBool AddAndCheckATEmpAttendanceEventTripevent(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, ATEmpAttendanceEventLog entity)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            entity.ATEventDateCode = entity.StartDateTime.Value.ToString("yyyy-MM-dd");
            entity.ATEventTypeID = ATTypeId.P出差;
            entity.ATEventTypeName = "出差";
            entity.ATEventSubTypeID = ATTypeId.P出差;
            entity.ATEventSubTypeName = "出差";
            tempBaseResultBool = AddATEmpAttendanceEventCheck(entity);
            if (tempBaseResultBool.BoolFlag)
            {
                tempBaseResultBool.BoolFlag = AddATEmpAttendanceEventTripevent(pushWeiXinMessageAction, entity);
            }
            else
            {
                tempBaseResultBool.BoolFlag = false;
            }
            tempBaseResultBool.success = tempBaseResultBool.BoolFlag;
            return tempBaseResultBool;
        }
        public bool AddATEmpAttendanceEventTripevent(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, ATEmpAttendanceEventLog entity)
        {
            entity.ATEventDateCode = entity.StartDateTime.Value.ToString("yyyy-MM-dd");
            entity.ATEventTypeID = ATTypeId.P出差;
            entity.ATEventTypeName = "出差";
            entity.ATEventSubTypeID = ATTypeId.P出差;
            entity.ATEventSubTypeName = "出差";
            if (DBDao.Save(entity))
            {
                if (entity.ApproveID != null)
                {
                    Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                    data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你好！" + entity.ApplyName + "向你提交了一个新'出差'申请。" });
                    data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = entity.ApplyName });
                    string tmpdaterange = "";
                    if (entity.StartDateTime.HasValue && entity.EndDateTime.HasValue)
                    {
                        tmpdaterange = entity.StartDateTime.Value.ToString("yyyy-MM-dd") + "至" + entity.EndDateTime.Value.ToString("yyyy-MM-dd");
                    }
                    data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = tmpdaterange });
                    string tmppostionrange = "";
                    if (entity.EventStatPostion != null && entity.EventStatPostion.Trim() != "" && entity.EventDestinationPostion != null && entity.EventDestinationPostion.Trim() != "")
                    {
                        tmppostionrange = entity.EventStatPostion + "至" + entity.EventDestinationPostion;
                    }
                    data.Add("keyword3", new TemplateDataObject() { color = "#000000", value = tmppostionrange });
                    data.Add("keyword4", new TemplateDataObject() { color = "#000000", value = entity.Memo });
                    data.Add("keyword5", new TemplateDataObject() { color = "#000000", value = "待定" });
                    data.Add("remark", new TemplateDataObject() { color = "#1d73cd", value = "请登录微信OA审批。" });

                    IBBWeiXinAccount.PushWeiXinMessage(pushWeiXinMessageAction, new List<long>() { entity.ApproveID.Value }, data, "attrip", "");
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 新增员工考勤加班事件并验证是否允许申请
        /// </summary>
        /// <param name="pushWeiXinMessageAction"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BaseResultBool AddAndCheckATEmpAttendanceEventOvertimeevent(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, ATEmpAttendanceEventLog entity)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            entity.ATEventDateCode = entity.StartDateTime.Value.ToString("yyyy-MM-dd");
            entity.ATEventTypeID = ATTypeId.P加班;
            entity.ATEventTypeName = "加班";
            entity.ATEventSubTypeID = ATTypeId.P加班;
            entity.ATEventSubTypeName = "加班";
            tempBaseResultBool = AddATEmpAttendanceEventCheck(entity);
            if (tempBaseResultBool.BoolFlag)
            {
                tempBaseResultBool.BoolFlag = AddATEmpAttendanceEventOvertimeevent(pushWeiXinMessageAction, entity);
            }
            else
            {
                tempBaseResultBool.BoolFlag = false;
            }
            tempBaseResultBool.success = tempBaseResultBool.BoolFlag;
            return tempBaseResultBool;
        }
        public bool AddATEmpAttendanceEventOvertimeevent(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, ATEmpAttendanceEventLog entity)
        {
            entity.ATEventDateCode = entity.StartDateTime.Value.ToString("yyyy-MM-dd");
            entity.ATEventTypeID = ATTypeId.P加班;
            entity.ATEventTypeName = "加班";
            entity.ATEventSubTypeID = ATTypeId.P加班;
            entity.ATEventSubTypeName = "加班";
            if (DBDao.Save(entity))
            {
                if (entity.ApproveID != null)
                {
                    Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                    data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你好！" + entity.ApplyName + "向你提交了一个新'加班'申请。" });
                    data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = entity.ApplyName });
                    string tmpmemo = "";
                    if (entity.Memo != null && entity.Memo.Trim() != "")
                    {
                        tmpmemo = "(" + entity.Memo + ")";
                    }
                    data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = entity.ATEventSubTypeName + tmpmemo });
                    string tmpdaterange = "";
                    if (entity.StartDateTime.HasValue && entity.EndDateTime.HasValue)
                    {
                        tmpdaterange = entity.StartDateTime.Value.ToString("yyyy-MM-dd HH:mm") + "至" + entity.EndDateTime.Value.ToString("yyyy-MM-dd HH:mm");
                    }
                    data.Add("remark", new TemplateDataObject() { color = "#1d73cd", value = "加班时间：" + tmpdaterange + "." + "请登录微信OA审批。" });
                    IBBWeiXinAccount.PushWeiXinMessage(pushWeiXinMessageAction, new List<long>() { entity.ApproveID.Value }, data, "atovertime", "");
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public IList<SignLogEmpList> SearchATOtherSignLogByLimit(string ed, int limit, int type, long deptid, long empid, long otherempid)
        {
            if (limit <= 0)
                limit = 1;
            DateTime edt = Convert.ToDateTime(ed);
            DateTime sdt = edt.AddDays(-1 * (limit - 1));//当天传1
            ZhiFang.Common.Log.Log.Debug("SearchATOtherSignLogByLimit,ed:" + ed + "@sdt:" + sdt.ToString("yyyy-MM-dd") + "@edt:" + edt.ToString("yyyy-MM-dd") + "@a:" + (-1 * (limit - 1)));
            HREmployee emp = IDHREmployeeDao.Get(empid);
            IList<HREmployee> hremplist = null;
            if (emp != null && emp.RBACEmpRoleList.Count(r => r.RBACRole.UseCode == "R1001") > 0)
            {
                hremplist = IDHREmployeeDao.GetListByHQL(" 1=1 ");//取平台客户管理员取全部的员工                
            }
            else
            {
                hremplist = IDHREmployeeDao.GetListByHQL(" ManagerID=" + empid + " ");//取直接管理的员工                
            }
            if (hremplist != null && hremplist.Count > 0)
            {
                if (otherempid > 0)
                {
                    if (hremplist.Count(a => a.Id == otherempid) > 0)
                    {
                        ZhiFang.Common.Log.Log.Debug("1");
                        return SearchATEmpSignLog(sdt.ToString("yyyy-MM-dd"), ed, type, new long[1] { otherempid });
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("2");
                        return new List<SignLogEmpList>();
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("3");
                    return SearchATEmpSignLog(sdt.ToString("yyyy-MM-dd"), ed, type, hremplist.ToArray());
                }
            }
            return new List<SignLogEmpList>();
        }
        public IList<SignLogEmpList> SearchATEmpSignLog(string sd, string ed, int type, HREmployee[] empids)
        {
            string empidsstr = "0";
            foreach (var emp in empids)
            {
                empidsstr += "," + emp.Id;
            }
            ZhiFang.Common.Log.Log.Debug("SearchATEmpSignLog.empidsstr:" + empidsstr);
            List<SignLogEmpList> signloglistlist = new List<SignLogEmpList>();
            var list = DBDao.GetListByHQL(" DataAddTime >= '" + sd + "' and DataAddTime<='" + ed + " 23:59:59' and  ATEventTypeID in (" + ATTypeId.P签到 + "," + ATTypeId.P签退 + ") and  ApplyID in (" + empidsstr + ") order by ATEventDateCode desc ");
            DateTime sdt = Convert.ToDateTime(sd);
            DateTime edt = Convert.ToDateTime(ed);
            //获取该月份的所有工作日期的集合
            Dictionary<string, DayOfWeek> allWorkDays = IBATHolidaySetting.GetAllWorkDaysOfOneMonth(edt.Year, edt.Month);

            while (edt >= sdt)
            {
                var signinfoday = list.Where(a => a.ATEventDateCode == edt.ToString("yyyy-MM-dd"));
                SignLogEmpList SignLogList = new SignLogEmpList();
                SignLogList.WeekInfo = DateTimeHelp.GetDateWeek(edt);
                SignLogList.ATEventDateCode = edt.ToString("yyyy-MM-dd");

                //是否工作日处理
                if (allWorkDays.ContainsKey(edt.ToString("yyyy-MM-dd")))
                    SignLogList.IsWorkDay = true;
                else
                    SignLogList.IsWorkDay = false;

                //if (edt.DayOfWeek != DayOfWeek.Saturday && edt.DayOfWeek != DayOfWeek.Sunday)
                //    SignLogList.IsWorkDay = true;
                //else
                //    SignLogList.IsWorkDay = false;

                SignLogList.SignLogL = new List<SignLog>();
                for (int i = 0; i < empids.Length; i++)
                {
                    SignLog sle = new SignLog();
                    sle.EmpId = empids[i].Id.ToString();
                    sle.EmpName = empids[i].CName;
                    sle.HeadImgUrl = empids[i].PicFile;
                    if (signinfoday.Count() > 0)
                    {
                        var signin = signinfoday.Where(s => s.ATEventTypeID == ATTypeId.P签到 && s.ApplyID == empids[i].Id);
                        if (signin.Count() > 0)
                        {
                            sle.SignInId = signin.ElementAt(0).Id.ToString();
                            sle.SignInTime = signin.ElementAt(0).DataAddTime.Value.ToString("HH:mm");
                            sle.SignInMemo = signin.ElementAt(0).Memo;
                            if (signin.ElementAt(0).ATEventSubTypeID == ATTypeId.迟到)
                                sle.SignInType = signin.ElementAt(0).ATEventSubTypeName;
                            sle.SignInIsOffsite = signin.ElementAt(0).IsOffsite;
                            sle.SigninATEventLogPostion = signin.ElementAt(0).ATEventLogPostion;
                            sle.SigninATEventLogPostionName = signin.ElementAt(0).ATEventLogPostionName;
                        }

                        var signout = signinfoday.Where(s => s.ATEventTypeID == ATTypeId.P签退 && s.ApplyID == empids[i].Id);
                        if (signout.Count() > 0)
                        {
                            sle.SignOutId = signout.ElementAt(0).Id.ToString();
                            sle.SignOutTime = signout.ElementAt(0).DataAddTime.Value.ToString("HH:mm");
                            sle.SignOutMemo = signout.ElementAt(0).Memo;
                            if (signout.ElementAt(0).ATEventSubTypeID == ATTypeId.早退)
                                sle.SignOutType = signout.ElementAt(0).ATEventSubTypeName;
                            sle.SignOutIsOffsite = signout.ElementAt(0).IsOffsite;
                            sle.SignoutATEventLogPostion = signout.ElementAt(0).ATEventLogPostion;
                            sle.SignoutATEventLogPostionName = signout.ElementAt(0).ATEventLogPostionName;
                        }
                    }
                    SignLogList.SignLogL.Add(sle);
                }
                signloglistlist.Add(SignLogList);
                edt = edt.AddDays(-1);
            }
            return signloglistlist;
        }

        public IList<SignLogEmpList> SearchATEmpSignLog(string sd, string ed, int type, long[] empids)
        {
            var empidso = IDHREmployeeDao.GetListByHQL(" Id in(" + string.Join(",", empids) + ") ");
            if (empidso != null && empidso.Count > 0)
                return SearchATEmpSignLog(sd, ed, type, empidso.ToArray());
            else
                return new List<SignLogEmpList>();
        }

        public IList<ATEmpApplyAllLog> SearchATEmpApplyAllLogByLimit(string sd, string ed, int pageindex, int limit, string apsid, int type, long empid)
        {
            List<ATEmpApplyAllLog> atmaallist = new List<ATEmpApplyAllLog>();
            string HQL = " 1=1 ";
            if (sd != null && sd.Trim() != "")
            {
                HQL += " and DataAddTime >= '" + sd + "'";
            }
            if (ed != null && ed.Trim() != "")
            {
                HQL += " and DataAddTime<='" + ed + " 23:59:59'";
            }
            if (apsid != null && apsid.Trim() != "")
            {
                HQL += " and  ( ATEventTypeID=" + apsid + ")";
            }
            else
            {
                HQL += " and  ATEventTypeID in (" + ATTypeId.P请假 + "," + ATTypeId.P外出 + "," + ATTypeId.P出差 + "," + ATTypeId.P加班 + ") ";
            }

            if (empid > 0)
            {
                HQL += " and  ( ApplyID=" + empid + ")";
            }
            HQL += " order by DataAddTime desc ";

            ZhiFang.Common.Log.Log.Debug("SearchATEmpApplyAllLogByLimit.HQL" + HQL);
            var entitylist = DBDao.GetListByHQL(HQL, pageindex, limit);

            for (int i = 0; i < entitylist.list.Count; i++)
            {
                ATEmpApplyAllLog atmyapplyalllog = new ATEmpApplyAllLog();

                atmyapplyalllog.ATEmpAttendanceEventLogId = entitylist.list[i].Id.ToString();

                atmyapplyalllog.DataAddTime = entitylist.list[i].DataAddTime.Value.ToString("yyyy-MM-dd HH:mm");
                atmyapplyalllog.Memo = entitylist.list[i].Memo;
                atmyapplyalllog.EvenLength = Math.Round(entitylist.list[i].EvenLength, 1);
                atmyapplyalllog.ATEventTypeID = entitylist.list[i].ATEventTypeID.ToString();
                atmyapplyalllog.ATEventTypeName = entitylist.list[i].ATEventTypeName;
                atmyapplyalllog.ATEventSubTypeID = entitylist.list[i].ATEventSubTypeID.ToString();
                atmyapplyalllog.ATEventSubTypeName = entitylist.list[i].ATEventSubTypeName;

                if (entitylist.list[i].ATEventTypeID == ATTypeId.P请假 || entitylist.list[i].ATEventTypeID == ATTypeId.P出差)
                {
                    atmyapplyalllog.StartDateTime = entitylist.list[i].StartDateTime.Value.ToString("yyyy-MM-dd");
                    atmyapplyalllog.EndDateTime = entitylist.list[i].EndDateTime.Value.ToString("yyyy-MM-dd");
                    atmyapplyalllog.EvenLengthUnit = "天";
                }
                if (entitylist.list[i].ATEventTypeID == ATTypeId.P外出 || entitylist.list[i].ATEventTypeID == ATTypeId.P加班)
                {
                    atmyapplyalllog.StartDateTime = entitylist.list[i].StartDateTime.Value.ToString("MM-dd HH:mm");
                    atmyapplyalllog.EndDateTime = entitylist.list[i].EndDateTime.Value.ToString("MM-dd HH:mm");
                    atmyapplyalllog.EvenLengthUnit = "小时";
                }

                atmyapplyalllog.ApproveStatusID = (entitylist.list[i].ATApproveStatus != null) ? entitylist.list[i].ATApproveStatus.Id : 0;
                atmyapplyalllog.ApproveStatusName = entitylist.list[i].ApproveStatusName;
                atmyapplyalllog.ApproveID = entitylist.list[i].ApproveID.ToString();
                atmyapplyalllog.ApproveName = entitylist.list[i].ApproveName;
                atmyapplyalllog.ApproveDateTime = (entitylist.list[i].ApproveDateTime.HasValue) ? entitylist.list[i].ApproveDateTime.Value.ToString("yyyy-MM-dd HH:mm") : "";
                atmyapplyalllog.ApproveMemo = entitylist.list[i].ApproveMemo;

                atmaallist.Add(atmyapplyalllog);
            }
            return atmaallist;
        }

        public IList<ATEmpApplyAllLog> SearchATMyApprovalAllLogByEmpId(string sd, string ed, int pageindex, int limit, string apsid, string typeidlist, long empid)
        {
            List<ATEmpApplyAllLog> atmaallist = new List<ATEmpApplyAllLog>();
            string HQL = " 1=1 ";
            if (sd != null && sd.Trim() != "")
            {
                HQL += " and DataAddTime >= '" + sd + "'";
            }
            if (ed != null && ed.Trim() != "")
            {
                HQL += " and DataAddTime<='" + ed + " 23:59:59'";
            }
            if (apsid != null && apsid.Trim() != "")
            {
                HQL += " and  ( ATEventTypeID=" + apsid + ")";
            }
            else
            {
                HQL += " and  ATEventTypeID in (" + ATTypeId.P请假 + "," + ATTypeId.P外出 + "," + ATTypeId.P出差 + "," + ATTypeId.P加班 + ")";
            }

            if (typeidlist != null && typeidlist.Trim() != "")
            {
                HQL += " and  ( ApproveStatusID in (" + typeidlist + "))";
            }
            else
            {
                HQL += " and  ( ApproveStatusID= null or ApproveStatusID=0)";
            }

            if (empid > 0)
            {
                HQL += " and  ( ApproveID=" + empid + ")";
            }
            HQL += " order by DataAddTime desc ";

            ZhiFang.Common.Log.Log.Debug("SearchATMyApprovalAllLogByEmpId.HQL" + HQL);
            var entitylist = DBDao.GetListByHQL(HQL, pageindex, limit);

            for (int i = 0; i < entitylist.list.Count; i++)
            {
                ATEmpApplyAllLog atmyapplyalllog = new ATEmpApplyAllLog();

                atmyapplyalllog.ATEmpAttendanceEventLogId = entitylist.list[i].Id.ToString();

                atmyapplyalllog.DataAddTime = entitylist.list[i].DataAddTime.Value.ToString("yyyy-MM-dd HH:mm");
                atmyapplyalllog.Memo = entitylist.list[i].Memo;
                atmyapplyalllog.EvenLength = Math.Round(entitylist.list[i].EvenLength, 1);
                atmyapplyalllog.ATEventTypeID = entitylist.list[i].ATEventTypeID.ToString();
                atmyapplyalllog.ATEventTypeName = entitylist.list[i].ATEventTypeName;
                atmyapplyalllog.ATEventSubTypeID = entitylist.list[i].ATEventSubTypeID.ToString();
                atmyapplyalllog.ATEventSubTypeName = entitylist.list[i].ATEventSubTypeName;

                if (entitylist.list[i].ATEventTypeID == ATTypeId.P请假 || entitylist.list[i].ATEventTypeID == ATTypeId.P出差)
                {
                    atmyapplyalllog.StartDateTime = entitylist.list[i].StartDateTime.Value.ToString("yyyy-MM-dd");
                    atmyapplyalllog.EndDateTime = entitylist.list[i].EndDateTime.Value.ToString("yyyy-MM-dd");
                    atmyapplyalllog.EvenLengthUnit = "天";
                }
                if (entitylist.list[i].ATEventTypeID == ATTypeId.P外出 || entitylist.list[i].ATEventTypeID == ATTypeId.P加班)
                {
                    atmyapplyalllog.StartDateTime = entitylist.list[i].StartDateTime.Value.ToString("MM-dd HH:mm");
                    atmyapplyalllog.EndDateTime = entitylist.list[i].EndDateTime.Value.ToString("MM-dd HH:mm");
                    atmyapplyalllog.EvenLengthUnit = "小时";
                }

                atmyapplyalllog.ApproveStatusID = (entitylist.list[i].ATApproveStatus != null) ? entitylist.list[i].ATApproveStatus.Id : 0;
                atmyapplyalllog.ApproveStatusName = entitylist.list[i].ApproveStatusName;
                atmyapplyalllog.ApproveID = entitylist.list[i].ApproveID.ToString();
                atmyapplyalllog.ApproveName = entitylist.list[i].ApproveName;

                atmyapplyalllog.ApplyEmp = new EmpInfo() { EmpId = entitylist.list[i].ApplyID.HasValue ? entitylist.list[i].ApplyID.Value.ToString() : "", EmpName = entitylist.list[i].ApplyName, HeadImgUrl = IDHREmployeeDao.Get(entitylist.list[i].ApplyID.Value).PicFile };
                atmyapplyalllog.ApproveDateTime = (entitylist.list[i].ApproveDateTime.HasValue) ? entitylist.list[i].ApproveDateTime.Value.ToString("yyyy-MM-dd HH:mm") : "";
                atmyapplyalllog.ApproveMemo = entitylist.list[i].ApproveMemo;

                atmaallist.Add(atmyapplyalllog);
            }
            return atmaallist;
        }

        public bool ApprovalATApplyEventLog(string memo, string[] eventlogids, int type, long empid)
        {
            try
            {
                string ApproveStatusName = "";
                var at = IDATApproveStatusDao.Get(type);
                if (at != null)
                    ApproveStatusName = at.Name;
                for (int i = 0; i < eventlogids.Length; i++)
                {
                    //string HQL = "update ATEmpAttendanceEventLog as ateael set ateael.ATApproveStatus.Id='" + type + "' ,ateael.ApproveDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ,ateael.ApproveMemo='" + memo + "' ,ateael.ATApproveStatus.Id='" + type + "' ,ateael.ApproveStatusName='" + ApproveStatusName + "' , where   ateael.Id=" + eventlogids[i] + " and ateael.ApproveID=" + empid + " ";

                    string HQL = "update ATEmpAttendanceEventLog set ApproveDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ,ApproveMemo='" + memo + "' ,ATApproveStatus.Id='" + type + "',ApproveStatusName='" + ApproveStatusName + "'  where   Id=" + eventlogids[i] + " and ApproveID=" + empid + " ";
                    //ZhiFang.Common.Log.Log.Debug("ApprovalATApplyEventLog批量审批。HQL=" + HQL);
                    int flag = DBDao.UpdateByHql(HQL);
                    if (flag <= 0)
                    {
                        ZhiFang.Common.Log.Log.Debug("ApprovalATApplyEventLog批量审批未更新数据集。HQL=" + HQL);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("ApprovalATApplyEventLog批量审批未异常：" + ex.ToString());
                return false;

            }
        }

        public bool ApprovalATApplyEventLog(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, string memo, string[] eventlogids, int type, long empid)
        {
            try
            {
                string ApproveStatusName = "";
                var at = IDATApproveStatusDao.Get(type);
                if (at != null)
                    ApproveStatusName = at.Name;
                for (int i = 0; i < eventlogids.Length; i++)
                {
                    //string HQL = "update ATEmpAttendanceEventLog as ateael set ateael.ATApproveStatus.Id='" + type + "' ,ateael.ApproveDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ,ateael.ApproveMemo='" + memo + "' ,ateael.ATApproveStatus.Id='" + type + "' ,ateael.ApproveStatusName='" + ApproveStatusName + "' , where   ateael.Id=" + eventlogids[i] + " and ateael.ApproveID=" + empid + " ";

                    string HQL = "update ATEmpAttendanceEventLog set ApproveDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ,ApproveMemo='" + memo + "' ,ATApproveStatus.Id='" + type + "',ApproveStatusName='" + ApproveStatusName + "'  where   Id=" + eventlogids[i] + " and ApproveID=" + empid + " ";
                    //ZhiFang.Common.Log.Log.Debug("ApprovalATApplyEventLog批量审批。HQL=" + HQL);
                    int flag = DBDao.UpdateByHql(HQL);
                    if (flag <= 0)
                    {
                        ZhiFang.Common.Log.Log.Debug("ApprovalATApplyEventLog批量审批未更新数据集。HQL=" + HQL);
                    }
                    else
                    {
                        ATEmpAttendanceEventLog entity = DBDao.Get(long.Parse(eventlogids[i]));
                        if (entity.ApplyID.HasValue)
                        {
                            IList<ZhiFang.Entity.OA.BWeiXinPushMessageTemplate> bwxpmtl = null;
                            Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                            string syscode = "";
                            string statusstr = type == 1 ? "通过" : "打回";
                            if (entity.ATEventTypeID == ATTypeId.P请假)
                            {
                                #region 请假审批
                                syscode = "atleave";
                                //bwxpmtl = IDBWeiXinPushMessageTemplateDao.GetListByHQL(" Shortcode='" + SysWeiXinTemplate.请假审批提醒 + "' ");
                                data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你好！你的'请假'申请已被" + entity.ApproveName + "审批为'" + statusstr + "'状态。 " });
                                data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = entity.ApplyName });
                                data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = entity.ATEventSubTypeName });
                                string tmpdaterange = "";
                                if (entity.StartDateTime.HasValue && entity.EndDateTime.HasValue)
                                {
                                    tmpdaterange = entity.StartDateTime.Value.ToString("yyyy-MM-dd HH:mm") + "至" + entity.EndDateTime.Value.ToString("yyyy-MM-dd HH:mm");
                                }
                                data.Add("keyword3", new TemplateDataObject() { color = "#000000", value = tmpdaterange });
                                data.Add("keyword4", new TemplateDataObject() { color = "#000000", value = entity.Memo });
                                data.Add("remark", new TemplateDataObject() { color = "#1d73cd", value = "请登录微信OA查看。" });
                                #endregion
                            }
                            if (entity.ATEventTypeID == ATTypeId.P外出)
                            {
                                #region 外出审批
                                syscode = "ategress";
                                //bwxpmtl = IDBWeiXinPushMessageTemplateDao.GetListByHQL(" Shortcode='" + SysWeiXinTemplate.考勤提醒 + "' ");
                                data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你好！你的'外出'申请已被" + entity.ApproveName + "审批为'" + statusstr + "'状态。 " });
                                data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = entity.ApplyName });
                                string tmpmemo = "";
                                if (entity.Memo != null && entity.Memo.Trim() != "")
                                {
                                    tmpmemo = "(" + entity.Memo + ")";
                                }
                                data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = entity.ATEventSubTypeName + tmpmemo });
                                string tmpdaterange = "";
                                if (entity.StartDateTime.HasValue && entity.EndDateTime.HasValue)
                                {
                                    tmpdaterange = entity.StartDateTime.Value.ToString("yyyy-MM-dd") + "时间" + entity.StartDateTime.Value.ToString("HH:mm") + "至" + entity.EndDateTime.Value.ToString("HH:mm");
                                }
                                data.Add("remark", new TemplateDataObject() { color = "#1d73cd", value = "外出日期：" + tmpdaterange + "." + "请登录微信OA查看。" });
                                #endregion
                            }
                            if (entity.ATEventTypeID == ATTypeId.P出差)
                            {
                                #region 出差审批
                                syscode = "attrip";
                                //bwxpmtl = IDBWeiXinPushMessageTemplateDao.GetListByHQL(" Shortcode='" + SysWeiXinTemplate.出差审批通知 + "' ");
                                data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你好！你的'出差'申请已被" + entity.ApproveName + "审批为'" + statusstr + "'状态。 " });
                                data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = entity.ApplyName });
                                string tmpdaterange = "";
                                if (entity.StartDateTime.HasValue && entity.EndDateTime.HasValue)
                                {
                                    tmpdaterange = entity.StartDateTime.Value.ToString("yyyy-MM-dd") + "至" + entity.EndDateTime.Value.ToString("yyyy-MM-dd");
                                }
                                data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = tmpdaterange });
                                string tmppostionrange = "";
                                if (entity.EventStatPostion != null && entity.EventStatPostion.Trim() != "" && entity.EventDestinationPostion != null && entity.EventDestinationPostion.Trim() != "")
                                {
                                    tmppostionrange = entity.EventStatPostion + "至" + entity.EventDestinationPostion;
                                }
                                data.Add("keyword3", new TemplateDataObject() { color = "#000000", value = tmppostionrange });
                                data.Add("keyword4", new TemplateDataObject() { color = "#000000", value = entity.Memo });
                                data.Add("keyword5", new TemplateDataObject() { color = "#000000", value = "待定" });
                                data.Add("remark", new TemplateDataObject() { color = "#1d73cd", value = "请登录微信OA查看。" });
                                #endregion
                            }
                            if (entity.ATEventTypeID == ATTypeId.P加班)
                            {
                                #region 加班审批
                                syscode = "atovertime";
                                //bwxpmtl = IDBWeiXinPushMessageTemplateDao.GetListByHQL(" Shortcode='" + SysWeiXinTemplate.考勤提醒 + "' ");
                                data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你好！你的'加班'申请已被" + entity.ApproveName + "审批为'" + statusstr + "'状态。 " });
                                data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = entity.ApplyName });
                                string tmpmemo = "";
                                if (entity.Memo != null && entity.Memo.Trim() != "")
                                {
                                    tmpmemo = "(" + entity.Memo + ")";
                                }
                                data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = entity.ATEventSubTypeName + tmpmemo });
                                string tmpdaterange = "";
                                if (entity.StartDateTime.HasValue && entity.EndDateTime.HasValue)
                                {
                                    tmpdaterange = entity.StartDateTime.Value.ToString("yyyy-MM-dd HH:mm") + "至" + entity.EndDateTime.Value.ToString("yyyy-MM-dd HH:mm");
                                }
                                data.Add("remark", new TemplateDataObject() { color = "#1d73cd", value = "加班时间：" + tmpdaterange + "." + "请登录微信OA查看。" });
                                #endregion
                            }
                            IBBWeiXinAccount.PushWeiXinMessage(pushWeiXinMessageAction, new List<long>() { entity.ApplyID.Value }, data, syscode, "");
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("ApprovalATApplyEventLog批量审批未异常：" + ex.ToString());
                return false;

            }
        }
        /// <summary>
        /// 获取人员月考勤
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="monthCode"></param>
        /// <param name="OwnEmpId"></param>
        /// <returns></returns>
        public ATEmpMonthLog GetEmpMonthLog(long EmpId, string monthCode, long ManagerEmpId)
        {
            DateTime firstDate = Convert.ToDateTime(monthCode + "-01");
            int days = DateTime.DaysInMonth(firstDate.Year, firstDate.Month);

            ATEmpMonthLog ateml = new ATEmpMonthLog();
            ateml.MonthCode = monthCode;
            ateml.StartDate = firstDate.ToString("yyyy-MM-dd");
            string endDate = firstDate.AddDays(days - 1).ToString("yyyy-MM-dd");
            ateml.EndDate = endDate;

            #region 签到签退
            IList<ATEmpAttendanceEventLog> SigninoutList = DBDao.GetListByHQL(" ApplyID=" + EmpId + " and  ATEventTypeID in (" + ATTypeId.P签到 + "," + ATTypeId.P签退 + ")  and DataAddTime>='" + ateml.StartDate + "' and DataAddTime<='" + ateml.EndDate + " 23:59:59' ");
            ateml.SignList = new List<SignInfo>();

            if (SigninoutList != null && SigninoutList.Count > 0)
            {
                //获取该月份的所有工作日期的集合
                Dictionary<string, DayOfWeek> allWorkDays = IBATHolidaySetting.GetAllWorkDaysOfOneMonth(firstDate.Year, firstDate.Month);
                //获取员工考勤的考勤设置信息
                ateml.ATEmpAttendanceEventParaSettings = IBATEmpAttendanceEventParaSettings.SearchATEmpAttendanceEventParaSettingsByEmpId(EmpId);
                for (int i = 0; i < days; i++)
                {
                    DateTime edt = firstDate.AddDays(i);
                    var signinfoday = SigninoutList.Where(a => a.ATEventDateCode == edt.ToString("yyyy-MM-dd"));
                    ZhiFang.Entity.OA.ViewObject.Response.SignLog signlog = new ZhiFang.Entity.OA.ViewObject.Response.SignLog();
                    signlog.WeekInfo = DateTimeHelp.GetDateWeek(edt);
                    signlog.ATEventDateCode = edt.ToString("yyyy-MM-dd");

                    //是否工作日处理
                    if (allWorkDays.ContainsKey(edt.ToString("yyyy-MM-dd")))
                        signlog.IsWorkDay = true;
                    else
                        signlog.IsWorkDay = false;

                    #region 原来的
                    //if (edt.DayOfWeek != DayOfWeek.Saturday && edt.DayOfWeek != DayOfWeek.Sunday && edt.ToString("yyyy-MM-dd") != DateTime.Now.ToString("yyyy-MM-dd"))
                    //    signlog.IsWorkDay = true;
                    //else
                    //    signlog.IsWorkDay = false; 
                    #endregion

                    if (signinfoday.Count() > 0)
                    {
                        #region 签到签退
                        var signin = signinfoday.Where(s => s.ATEventTypeID == ATTypeId.P签到);
                        if (signin.Count() > 0)
                        {
                            signlog.SignInId = signin.ElementAt(0).Id.ToString();
                            signlog.SignInTime = signin.ElementAt(0).DataAddTime.Value.ToString("HH:mm");
                            signlog.SignInMemo = signin.ElementAt(0).Memo;
                            if (signin.ElementAt(0).ATEventSubTypeID == ATTypeId.迟到)
                                signlog.SignInType = signin.ElementAt(0).ATEventSubTypeName;
                            signlog.SignInIsOffsite = signin.ElementAt(0).IsOffsite;
                            signlog.SigninATEventLogPostion = signin.ElementAt(0).ATEventLogPostion;
                            signlog.SigninATEventLogPostionName = signin.ElementAt(0).ATEventLogPostionName;
                        }

                        var signout = signinfoday.Where(s => s.ATEventTypeID == ATTypeId.P签退);
                        if (signout.Count() > 0)
                        {
                            signlog.SignOutId = signout.ElementAt(0).Id.ToString();
                            signlog.SignOutTime = signout.ElementAt(0).DataAddTime.Value.ToString("HH:mm");
                            signlog.SignOutMemo = signout.ElementAt(0).Memo;
                            if (signout.ElementAt(0).ATEventSubTypeID == ATTypeId.早退)
                                signlog.SignOutType = signout.ElementAt(0).ATEventSubTypeName;
                            signlog.SignOutIsOffsite = signout.ElementAt(0).IsOffsite;
                            signlog.SignoutATEventLogPostion = signout.ElementAt(0).ATEventLogPostion;
                            signlog.SignoutATEventLogPostionName = signout.ElementAt(0).ATEventLogPostionName;
                        }
                    }
                    ateml.SignList.Add(signlog);
                    #endregion
                }
            }
            #endregion

            IList<ATEmpAttendanceEventLog> ApproveList = DBDao.GetListByHQL(" ApplyID=" + EmpId + " and  ATEventTypeID in (" + ATTypeId.P请假 + "," + ATTypeId.P外出 + "," + ATTypeId.P出差 + "," + ATTypeId.P加班 + ")  and (StartDateTime<='" + ateml.EndDate + "  23:59:59'  and EndDateTime>='" + ateml.StartDate + "' )");
            if (ApproveList != null && ApproveList.Count > 0)
            {
                //获取该月份的所有日期的集合
                Dictionary<string, DayOfWeek> allDates = IBATHolidaySetting.GetAllDatesOfOneMonth(firstDate.Year, firstDate.Month);

                #region LeaveList请假
                var LeaveList = ApproveList.Where(s => s.ATEventTypeID == ATTypeId.P请假 && s.StartDateTime <= DateTime.Parse(ateml.EndDate).AddHours(23).AddMinutes(59).AddSeconds(59) && s.EndDateTime >= DateTime.Parse(ateml.StartDate) && s.ApplyID == EmpId);

                if (LeaveList.Count() > 0)
                {
                    ateml.LeaveList = new List<ATEmpApplyAllLog>();
                    ateml.LeaveList = GetLeaveList(firstDate, days, allDates, LeaveList, null);
                }
                #endregion
                #region EgressList外出
                var EgressList = ApproveList.Where(s => s.ATEventTypeID == ATTypeId.P外出);
                if (EgressList.Count() > 0)
                {
                    ateml.EgressList = new List<ATEmpApplyAllLog>();
                    ateml.EgressList = GetEgressList(EgressList, null);
                }
                #endregion
                #region TripList出差
                var TripList = ApproveList.Where(s => s.ATEventTypeID == ATTypeId.P出差 && s.ApplyID == EmpId);
                if (TripList.Count() > 0)
                {
                    ateml.TripList = new List<ATEmpApplyAllLog>();
                    ateml.TripList = GetTripList(firstDate, days, allDates, TripList, null);
                }
                #endregion
                #region OvertimeList加班
                var OvertimeList = ApproveList.Where(s => s.ATEventTypeID == ATTypeId.P加班);
                if (OvertimeList.Count() > 0)
                {
                    ateml.OvertimeList = new List<ATEmpApplyAllLog>();
                    ateml.OvertimeList = GetOvertimeList(OvertimeList, null);
                }
                #endregion
            }
            return ateml;
        }

        public List<ATEmpListWeekLog> GetATEmpListWeekLog(long type, string StartDate, string EndDate, long EmpId, string EmpName, long HRDeptID, string HRDeptName)
        {
            List<ATEmpListWeekLog> atelwllist = new List<ATEmpListWeekLog>();
            DateTime sdt = Convert.ToDateTime(StartDate);
            DateTime edt = Convert.ToDateTime(EndDate);
            if (sdt.AddDays(6).ToString("yyyy-MM-dd") != edt.ToString("yyyy-MM-dd"))
            {
                ZhiFang.Common.Log.Log.Debug("GetATEmpListWeekLog.参数StartDate:" + StartDate + ",EndDate:" + EndDate + ".开始时间和结束时间间隔不为一周！");
                throw new Exception("GetATEmpListWeekLog.参数StartDate:" + StartDate + ",EndDate:" + EndDate + ".开始时间和结束时间间隔不为一周！");
            }
            #region 我的周考勤
            if (type == 0)
            {
                IList<ATEmpAttendanceEventLog> SigninoutList = DBDao.GetListByHQL(" ApplyID=" + EmpId + " and  ATEventTypeID in (" + ATTypeId.P签到 + "," + ATTypeId.P签退 + ")  and DataAddTime>='" + StartDate + "' and DataAddTime<='" + EndDate + " 23:59:59' ");
                IList<ATEmpAttendanceEventLog> ApproveList = DBDao.GetListByHQL(" ApplyID=" + EmpId + " and  ATEventTypeID in (" + ATTypeId.P请假 + "," + ATTypeId.P外出 + "," + ATTypeId.P出差 + "," + ATTypeId.P加班 + ")  and (StartDateTime<='" + EndDate + "  23:59:59'  and EndDateTime>='" + StartDate + "' )");
                ATEmpListWeekLog atelwl = GetEmpATEmpListWeekLog(StartDate, EndDate, EmpId, EmpName, HRDeptID, HRDeptName, sdt, SigninoutList, ApproveList);
                atelwllist.Add(atelwl);
            }
            #endregion
            #region 部门周考勤
            if (type == 1)
            {
                IList<HRDept> pdeptlist = IDHRDeptDao.GetListByHQL(" IsUse=true and ManagerID=" + EmpId.ToString());

                if (pdeptlist != null && pdeptlist.Count > 0)
                {
                    List<long> deptidlist = IDHRDeptDao.GetSubDeptIdListByDeptId(pdeptlist[0].Id);
                    deptidlist.Add(pdeptlist[0].Id);
                    IList<HRDept> alldeptlist = IDHRDeptDao.GetListByHQL(" IsUse=true and Id in (" + string.Join(",", deptidlist.ToArray()) + ") ");
                    for (int i = 0; i < alldeptlist.Count; i++)
                    {
                        IList<HREmployee> emplist = alldeptlist[i].HREmployeeList;
                        if (emplist != null && emplist.Count > 0)
                        {
                            string empidlist = " 0 ";
                            foreach (var emp in emplist)
                            {
                                if (emp.IsUse.HasValue && emp.IsUse.Value)
                                    empidlist += "," + emp.Id;
                            }
                            IList<ATEmpAttendanceEventLog> SigninoutAllList = DBDao.GetListByHQL(" ApplyID in (" + empidlist + ") and  ATEventTypeID in (" + ATTypeId.P签到 + "," + ATTypeId.P签退 + ")  and DataAddTime>='" + StartDate + "' and DataAddTime<='" + EndDate + " 23:59:59' ");
                            IList<ATEmpAttendanceEventLog> ApproveAllList = DBDao.GetListByHQL(" ApplyID in (" + empidlist + ")  and  ATEventTypeID in (" + ATTypeId.P请假 + "," + ATTypeId.P外出 + "," + ATTypeId.P出差 + "," + ATTypeId.P加班 + ")  and (StartDateTime<='" + EndDate + "  23:59:59'  and EndDateTime>='" + StartDate + "' )");
                            for (int j = 0; j < emplist.Count; j++)
                            {
                                if (emplist[j].IsUse.HasValue && emplist[j].IsUse.Value)
                                {
                                    ATEmpListWeekLog atelwl = GetEmpATEmpListWeekLog(StartDate, EndDate, emplist[j].Id, emplist[j].CName, alldeptlist[i].Id, alldeptlist[i].CName, sdt, SigninoutAllList, ApproveAllList);
                                    atelwllist.Add(atelwl);
                                }
                            }
                        }
                    }
                }
            }
            #endregion
            #region 公司周考勤
            if (type == 2)
            {
                if (true)//权限判定，暂定为True
                {
                    IList<HRDept> deptlist = IDHRDeptDao.GetListByHQL(" IsUse=true ");
                    if (deptlist != null && deptlist.Count > 0)
                    {
                        for (int i = 0; i < deptlist.Count; i++)
                        {
                            IList<HREmployee> emplist = deptlist[i].HREmployeeList;
                            if (emplist != null && emplist.Count > 0)
                            {
                                string empidlist = " 0 ";
                                foreach (var emp in emplist)
                                {
                                    if (emp.IsUse.HasValue && emp.IsUse.Value)
                                        empidlist += "," + emp.Id;
                                }
                                IList<ATEmpAttendanceEventLog> SigninoutAllList = DBDao.GetListByHQL(" ApplyID in (" + empidlist + ") and  ATEventTypeID in (" + ATTypeId.P签到 + "," + ATTypeId.P签退 + ")  and DataAddTime>='" + StartDate + "' and DataAddTime<='" + EndDate + " 23:59:59' ");
                                IList<ATEmpAttendanceEventLog> ApproveAllList = DBDao.GetListByHQL(" ApplyID in (" + empidlist + ")  and  ATEventTypeID in (" + ATTypeId.P请假 + "," + ATTypeId.P外出 + "," + ATTypeId.P出差 + "," + ATTypeId.P加班 + ")  and (StartDateTime<='" + EndDate + "  23:59:59'  and EndDateTime>='" + StartDate + "' )");
                                for (int j = 0; j < emplist.Count; j++)
                                {
                                    if (emplist[j].IsUse.HasValue && emplist[j].IsUse.Value)
                                    {
                                        ATEmpListWeekLog atelwl = GetEmpATEmpListWeekLog(StartDate, EndDate, emplist[j].Id, emplist[j].CName, deptlist[i].Id, deptlist[i].CName, sdt, SigninoutAllList, ApproveAllList);
                                        atelwllist.Add(atelwl);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion
            return atelwllist;
        }

        private ATEmpListWeekLog GetEmpATEmpListWeekLog(string StartDate, string EndDate, long EmpId, string EmpName, long HRDeptID, string HRDeptName, DateTime sdt, IList<ATEmpAttendanceEventLog> SigninoutList, IList<ATEmpAttendanceEventLog> ApproveList)
        {
            ATEmpListWeekLog atelwl = new ATEmpListWeekLog();
            atelwl.DeptId = HRDeptID;
            atelwl.DeptName = HRDeptName;
            atelwl.EmpId = EmpId;
            atelwl.EmpName = EmpName;
            atelwl.StartDate = StartDate;
            atelwl.EndDate = EndDate;

            //获取该月份的所有工作日期的集合
            Dictionary<string, DayOfWeek> allWorkDays = IBATHolidaySetting.GetAllWorkDaysOfOneMonth(sdt.Year, sdt.Month);
            //获取员工考勤的考勤设置信息
            atelwl.ATEmpAttendanceEventParaSettings = IBATEmpAttendanceEventParaSettings.SearchATEmpAttendanceEventParaSettingsByEmpId(EmpId);

            for (int i = 0; i < 7; i++)
            {
                ATEmpDayLog atempdaylog = new ATEmpDayLog();
                DateTime tmpdts = sdt.AddDays(i);
                DateTime tmpdte = sdt.AddDays(i).AddHours(23).AddMinutes(59).AddSeconds(59);
                #region 签到签退
                if (SigninoutList != null && SigninoutList.Count > 0)
                {
                    var signinfoday = SigninoutList.Where(a => a.ATEventDateCode == tmpdts.ToString("yyyy-MM-dd") && a.ApplyID == EmpId);
                    atempdaylog.SignList = new SignLog();

                    atempdaylog.SignList.WeekInfo = DateTimeHelp.GetDateWeek(tmpdts);
                    atempdaylog.SignList.ATEventDateCode = tmpdts.ToString("yyyy-MM-dd");

                    if (signinfoday.Count() > 0)
                    {
                        var signin = signinfoday.Where(s => s.ATEventTypeID == ATTypeId.P签到);
                        if (signin.Count() > 0)
                        {
                            //是否工作日处理
                            if (allWorkDays.ContainsKey(signin.ElementAt(0).DataAddTime.Value.ToString("yyyy-MM-dd")))
                                atempdaylog.SignList.IsWorkDay = true;
                            else
                                atempdaylog.SignList.IsWorkDay = false;

                            atempdaylog.SignList.SignInId = signin.ElementAt(0).Id.ToString();
                            atempdaylog.SignList.SignInTime = signin.ElementAt(0).DataAddTime.Value.ToString("HH:mm");
                            atempdaylog.SignList.SignInMemo = signin.ElementAt(0).Memo;
                            if (signin.ElementAt(0).ATEventSubTypeID == ATTypeId.迟到)
                                atempdaylog.SignList.SignInType = signin.ElementAt(0).ATEventSubTypeName;
                            atempdaylog.SignList.SignInIsOffsite = signin.ElementAt(0).IsOffsite;
                            atempdaylog.SignList.SigninATEventLogPostion = signin.ElementAt(0).ATEventLogPostion;
                            atempdaylog.SignList.SigninATEventLogPostionName = signin.ElementAt(0).ATEventLogPostionName;
                        }

                        var signout = signinfoday.Where(s => s.ATEventTypeID == ATTypeId.P签退);
                        if (signout.Count() > 0)
                        {
                            //是否工作日处理
                            if (allWorkDays.ContainsKey(signout.ElementAt(0).DataAddTime.Value.ToString("yyyy-MM-dd")))
                                atempdaylog.SignList.IsWorkDay = true;
                            else
                                atempdaylog.SignList.IsWorkDay = false;

                            atempdaylog.SignList.SignOutId = signout.ElementAt(0).Id.ToString();
                            atempdaylog.SignList.SignOutTime = signout.ElementAt(0).DataAddTime.Value.ToString("HH:mm");
                            atempdaylog.SignList.SignOutMemo = signout.ElementAt(0).Memo;
                            if (signout.ElementAt(0).ATEventSubTypeID == ATTypeId.早退)
                                atempdaylog.SignList.SignOutType = signout.ElementAt(0).ATEventSubTypeName;
                            atempdaylog.SignList.SignOutIsOffsite = signout.ElementAt(0).IsOffsite;
                            atempdaylog.SignList.SignoutATEventLogPostion = signout.ElementAt(0).ATEventLogPostion;
                            atempdaylog.SignList.SignoutATEventLogPostionName = signout.ElementAt(0).ATEventLogPostionName;
                        }
                    }
                }
                #endregion
                #region 考勤申请
                if (ApproveList != null && ApproveList.Count > 0)
                {
                    #region LeaveList请假
                    var LeaveList = ApproveList.Where(s => s.ATEventTypeID == ATTypeId.P请假 && s.StartDateTime <= tmpdte && s.EndDateTime >= tmpdts && s.ApplyID == EmpId);
                    if (LeaveList.Count() > 0)
                    {
                        atempdaylog.LeaveList = new List<ATEmpApplyAllLog>();
                        for (int j = 0; j < LeaveList.Count(); j++)
                        {
                            ATEmpApplyAllLog atmyapplyalllog = new ATEmpApplyAllLog();
                            atmyapplyalllog.ATEmpAttendanceEventLogId = LeaveList.ElementAt(j).Id.ToString();
                            atmyapplyalllog.DataAddTime = LeaveList.ElementAt(j).DataAddTime.Value.ToString("yyyy-MM-dd HH:mm");
                            atmyapplyalllog.Memo = LeaveList.ElementAt(j).Memo;
                            atmyapplyalllog.EvenLength = Math.Round(LeaveList.ElementAt(j).EvenLength, 1);
                            atmyapplyalllog.ATEventTypeID = LeaveList.ElementAt(j).ATEventTypeID.ToString();
                            atmyapplyalllog.ATEventTypeName = LeaveList.ElementAt(j).ATEventTypeName;
                            atmyapplyalllog.ATEventSubTypeID = LeaveList.ElementAt(j).ATEventSubTypeID.ToString();
                            atmyapplyalllog.ATEventSubTypeName = LeaveList.ElementAt(j).ATEventSubTypeName;

                            atmyapplyalllog.StartDateTime = LeaveList.ElementAt(j).StartDateTime.Value.ToString("yyyy-MM-dd");
                            atmyapplyalllog.EndDateTime = LeaveList.ElementAt(j).EndDateTime.Value.ToString("yyyy-MM-dd");
                            atmyapplyalllog.EvenLengthUnit = "天";

                            atmyapplyalllog.ApproveStatusID = (LeaveList.ElementAt(j).ATApproveStatus != null) ? LeaveList.ElementAt(j).ATApproveStatus.Id : 0;
                            atmyapplyalllog.ApproveStatusName = LeaveList.ElementAt(j).ApproveStatusName;
                            atmyapplyalllog.ApproveID = LeaveList.ElementAt(j).ApproveID.ToString();
                            atmyapplyalllog.ApproveName = LeaveList.ElementAt(j).ApproveName;
                            atmyapplyalllog.ApproveDateTime = (LeaveList.ElementAt(j).ApproveDateTime.HasValue) ? LeaveList.ElementAt(j).ApproveDateTime.Value.ToString("yyyy-MM-dd HH:mm") : "";
                            atmyapplyalllog.ApproveMemo = LeaveList.ElementAt(j).ApproveMemo;

                            atempdaylog.LeaveList.Add(atmyapplyalllog);
                        }
                    }
                    #endregion
                    #region EgressList外出
                    var EgressList = ApproveList.Where(s => s.ATEventTypeID == ATTypeId.P外出 && s.StartDateTime <= tmpdte && s.EndDateTime >= tmpdts && s.ApplyID == EmpId);
                    if (EgressList.Count() > 0)
                    {
                        atempdaylog.EgressList = new List<ATEmpApplyAllLog>();
                        atempdaylog.EgressList = GetEgressList(EgressList, null);
                    }
                    #endregion
                    #region TripList出差
                    var TripList = ApproveList.Where(s => s.ATEventTypeID == ATTypeId.P出差 && s.StartDateTime <= tmpdte && s.EndDateTime >= tmpdts && s.ApplyID == EmpId);
                    if (TripList.Count() > 0)
                    {
                        atempdaylog.TripList = new List<ATEmpApplyAllLog>();
                        for (int j = 0; j < TripList.Count(); j++)
                        {
                            ATEmpApplyAllLog atmyapplyalllog = new ATEmpApplyAllLog();
                            atmyapplyalllog.ATEmpAttendanceEventLogId = TripList.ElementAt(j).Id.ToString();
                            atmyapplyalllog.DataAddTime = TripList.ElementAt(j).DataAddTime.Value.ToString("yyyy-MM-dd HH:mm");
                            atmyapplyalllog.Memo = TripList.ElementAt(j).Memo;
                            atmyapplyalllog.EvenLength = Math.Round(TripList.ElementAt(j).EvenLength, 1);
                            atmyapplyalllog.ATEventTypeID = TripList.ElementAt(j).ATEventTypeID.ToString();
                            atmyapplyalllog.ATEventTypeName = TripList.ElementAt(j).ATEventTypeName;
                            atmyapplyalllog.ATEventSubTypeID = TripList.ElementAt(j).ATEventSubTypeID.ToString();
                            atmyapplyalllog.ATEventSubTypeName = TripList.ElementAt(j).ATEventSubTypeName;

                            atmyapplyalllog.StartDateTime = TripList.ElementAt(j).StartDateTime.Value.ToString("yyyy-MM-dd");
                            atmyapplyalllog.EndDateTime = TripList.ElementAt(j).EndDateTime.Value.ToString("yyyy-MM-dd");
                            atmyapplyalllog.EvenLengthUnit = "天";

                            atmyapplyalllog.ApproveStatusID = (TripList.ElementAt(j).ATApproveStatus != null) ? TripList.ElementAt(j).ATApproveStatus.Id : 0;
                            atmyapplyalllog.ApproveStatusName = TripList.ElementAt(j).ApproveStatusName;
                            atmyapplyalllog.ApproveID = TripList.ElementAt(j).ApproveID.ToString();
                            atmyapplyalllog.ApproveName = TripList.ElementAt(j).ApproveName;
                            atmyapplyalllog.ApproveDateTime = (TripList.ElementAt(j).ApproveDateTime.HasValue) ? TripList.ElementAt(j).ApproveDateTime.Value.ToString("yyyy-MM-dd HH:mm") : "";
                            atmyapplyalllog.ApproveMemo = TripList.ElementAt(j).ApproveMemo;

                            atempdaylog.TripList.Add(atmyapplyalllog);
                        }
                    }
                    #endregion
                    #region OvertimeList加班
                    var OvertimeList = ApproveList.Where(s => s.ATEventTypeID == ATTypeId.P加班 && s.StartDateTime <= tmpdte && s.EndDateTime >= tmpdts && s.ApplyID == EmpId);
                    if (OvertimeList.Count() > 0)
                    {
                        atempdaylog.OvertimeList = new List<ATEmpApplyAllLog>();
                        atempdaylog.OvertimeList = GetOvertimeList(OvertimeList, null);

                    }
                    #endregion
                }
                #endregion
                switch (tmpdts.DayOfWeek)
                {
                    case DayOfWeek.Monday: atelwl.Monday = atempdaylog; break;
                    case DayOfWeek.Tuesday: atelwl.Tuesday = atempdaylog; break;
                    case DayOfWeek.Wednesday: atelwl.Wednesday = atempdaylog; break;
                    case DayOfWeek.Thursday: atelwl.Thursday = atempdaylog; break;
                    case DayOfWeek.Friday: atelwl.Friday = atempdaylog; break;
                    case DayOfWeek.Saturday: atelwl.Saturday = atempdaylog; break;
                    case DayOfWeek.Sunday: atelwl.Sunday = atempdaylog; break;
                }
            }

            return atelwl;
        }

        #region 考勤签到签退的处理

        /// <summary>
        /// 员工的考勤签到或签退时的地点及时间判断处理
        /// </summary>
        /// <param name="entity">员工的考勤信息</param>
        /// <returns></returns>
        public BaseResultBool AddATEmpAttendanceEventLogCheck()
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.BoolFlag = true;
            BaseResultBool tempBaseResultATEventLogPostion = tempBaseResultBool;
            BaseResultBool tempBaseResultSignInTime = tempBaseResultBool;
            BaseResultBool tempBaseResultSignOutTime = tempBaseResultBool;
            if (Entity != null)
            {
                //获取员工的考勤设置信息信息
                ATEmpAttendanceEventParaSettings empSetting = IBATEmpAttendanceEventParaSettings.SearchATEmpAttendanceEventParaSettingsByEmpId(long.Parse(Entity.ApplyID.ToString()));
                if (empSetting == null)
                {
                    tempBaseResultBool.BoolFlag = false;
                    tempBaseResultBool.BoolInfo = "获取" + Entity.ApplyName + "的考勤设置信息为空!";
                }
                else
                {
                    //员工的考勤地点处理
                    if (Entity.ATEventTypeID == ATTypeId.P签到 || Entity.ATEventTypeID == ATTypeId.P签退)
                        tempBaseResultATEventLogPostion = ATEmpAttendanceEventLogCheckPostion(empSetting, ATEventPostionType.ATEventPostion);

                    //员工的考勤时间处理
                    if (Entity.ATEventTypeID == ATTypeId.P签到)
                        tempBaseResultSignInTime = ATEmpAttendanceEventLogCheckSignInTime(empSetting);
                    if (Entity.ATEventTypeID == ATTypeId.P签退)
                        tempBaseResultSignOutTime = ATEmpAttendanceEventLogCheckSignOutTime(empSetting);

                    if (tempBaseResultBool.BoolFlag == true && tempBaseResultATEventLogPostion.BoolFlag == false)
                    {
                        tempBaseResultBool = tempBaseResultATEventLogPostion;
                    }
                    else if (tempBaseResultBool.BoolFlag == true && tempBaseResultSignInTime.BoolFlag == false)
                    {
                        tempBaseResultBool = tempBaseResultSignInTime;
                    }
                    else if (tempBaseResultBool.BoolFlag == true && tempBaseResultSignOutTime.BoolFlag == false)
                    {
                        tempBaseResultBool = tempBaseResultSignOutTime;
                    }
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("AddATEmpAttendanceEventLogCheck.Entity为空");
                tempBaseResultBool.BoolFlag = false;
                tempBaseResultBool.BoolInfo = "AddATEmpAttendanceEventLogCheck.Entity为空";
            }
            tempBaseResultBool.ErrorInfo = tempBaseResultBool.BoolInfo;
            return tempBaseResultBool;
        }
        /// <summary>
        /// 员工签到签退的考勤地点是否正确或脱岗处理
        /// </summary>
        /// <param name="empSetting">员工的考勤设置信息</param>
        /// <param name="enumType">考勤地点类型</param>
        /// <returns></returns>
        public BaseResultBool ATEmpAttendanceEventLogCheckPostion(ATEmpAttendanceEventParaSettings empSetting, ATEventPostionType enumType)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.BoolFlag = true;
            if (Entity != null)
            {
                this.Entity.IsOffsite = false;
                if (empSetting == null)
                {
                    empSetting = IBATEmpAttendanceEventParaSettings.SearchATEmpAttendanceEventParaSettingsByEmpId(long.Parse(Entity.ApplyID.ToString()));
                }
                string info = "";
                double latEmp = 0, lngEmp = 0, latSet = 0, lngSet = 0;
                //计算两坐标之间的地点范围(米)
                double distance = 0;
                //设置的地点范围(米)
                double distanceSet = 0;
                if (Entity.ATEventTypeID == ATTypeId.P签到)
                {
                    info = "签到";
                }
                else if (tempBaseResultBool.BoolFlag == true && Entity.ATEventTypeID == ATTypeId.P签退)
                {
                    info = "签退";
                }

                #region 员工当前的考勤地点坐标信息
                string atEventLogPostion = "";
                atEventLogPostion = Entity.ATEventLogPostion;

                string[] tempEmpPostionArr = null;

                if (!String.IsNullOrEmpty(atEventLogPostion))
                {
                    tempEmpPostionArr = atEventLogPostion.Split(',');
                }
                else
                {
                    tempBaseResultBool.BoolFlag = false;
                    tempBaseResultBool.BoolInfo = Entity.ApplyName + "的" + info + "考勤地点坐标信息为空!";
                    ZhiFang.Common.Log.Log.Error(tempBaseResultBool.BoolInfo);
                }

                if (tempEmpPostionArr != null && tempEmpPostionArr.Length == 2)
                {
                    latEmp = double.Parse(tempEmpPostionArr[0]);
                    lngEmp = double.Parse(tempEmpPostionArr[1]);
                }
                else
                {
                    tempBaseResultBool.BoolFlag = false;
                    tempBaseResultBool.BoolInfo = Entity.ApplyName + "的" + info + "考勤地点坐标信息异常!坐标信息为" + Entity.ATEventLogPostion;
                    ZhiFang.Common.Log.Log.Error(tempBaseResultBool.BoolInfo);
                }
                #endregion

                #region 员工的考勤设置地点坐标信息处理
                if (tempBaseResultBool.BoolFlag == true)
                {
                    string[] tempSetPostionArr = null;
                    if (empSetting != null)
                    {
                        string atEventPostion = "";
                        atEventPostion = empSetting.ATEventPostion;
                        switch (enumType)
                        {
                            case ATEventPostionType.ATEventPostion:
                                atEventPostion = empSetting.ATEventPostion;
                                break;
                            case ATEventPostionType.TimingOnePostion:
                                atEventPostion = empSetting.TimingOnePostion;
                                break;
                            case ATEventPostionType.TimingTwoPostion:
                                atEventPostion = empSetting.TimingOnePostion;
                                break;
                            case ATEventPostionType.TimingThreePostion:
                                atEventPostion = empSetting.TimingOnePostion;
                                break;
                            case ATEventPostionType.TimingFourPostion:
                                atEventPostion = empSetting.TimingOnePostion;
                                break;
                            case ATEventPostionType.TimingFivePostion:
                                atEventPostion = empSetting.TimingOnePostion;
                                break;
                            default:
                                atEventPostion = empSetting.ATEventPostion;
                                break;
                        }
                        distanceSet = empSetting.ATEventPostionRange;
                        //如果设置的地点范围值小于或等于0,不用进行两坐标的计算
                        if (distanceSet > 0)
                        {
                            if (!String.IsNullOrEmpty(atEventPostion))
                            {
                                tempSetPostionArr = atEventPostion.Split(',');
                            }
                            else
                            {
                                tempBaseResultBool.BoolFlag = false;
                                tempBaseResultBool.BoolInfo = "获取" + Entity.ApplyName + "的考勤设置地点坐标信息为空!";
                                ZhiFang.Common.Log.Log.Error(tempBaseResultBool.BoolInfo);
                            }
                            if (tempBaseResultBool.BoolFlag == true)
                            {
                                if (tempSetPostionArr != null && tempSetPostionArr.Length == 2)
                                {
                                    latSet = double.Parse(tempSetPostionArr[0]);
                                    lngSet = double.Parse(tempSetPostionArr[1]);
                                }
                                else
                                {
                                    tempBaseResultBool.BoolFlag = false;
                                    tempBaseResultBool.BoolInfo = "获取" + Entity.ApplyName + "的考勤设置地点坐标信息异常!考勤设置地点坐标信息为" + atEventPostion;
                                    ZhiFang.Common.Log.Log.Error(tempBaseResultBool.BoolInfo);
                                }
                            }
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Debug("ATEmpAttendanceEventLogCheckPostion:" + Entity.ApplyName + "考勤设置地点范围为" + distanceSet);
                        }
                    }
                    else
                    {
                        tempBaseResultBool.BoolFlag = false;
                        tempBaseResultBool.BoolInfo = "获取" + Entity.ApplyName + "的考勤设置信息为空!";
                        ZhiFang.Common.Log.Log.Debug(tempBaseResultBool.BoolInfo);
                    }
                }
                #endregion

                #region 两个坐标进行计算
                if (tempBaseResultBool.BoolFlag == true)
                {
                    if (latEmp > 0 && lngEmp > 0 && latSet > 0 && lngSet > 0)
                    {
                        distance = GetDistance(latEmp, lngEmp, latSet, lngSet);
                        //是否脱岗,当前考勤地点与考勤设置地点的距离>考勤设置地点范围
                        if (distanceSet > 0 && distance > distanceSet)
                        {
                            this.Entity.IsOffsite = true;
                            tempBaseResultBool.BoolFlag = false;
                            double result = distance - distanceSet;
                            tempBaseResultBool.BoolInfo = Entity.ApplyName + "考勤脱岗,当前" + info + "考勤地点超出考勤设置地点范围" + String.Format("{0:N2}", result) + "米!";
                            ZhiFang.Common.Log.Log.Error("ATEmpAttendanceEventLogCheckPostion." + tempBaseResultBool.BoolInfo);
                        }
                    }
                }
                #endregion
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("ATEmpAttendanceEventLogCheckPostion.Entity为空");
                tempBaseResultBool.BoolFlag = false;
                tempBaseResultBool.BoolInfo = "ATEmpAttendanceEventLogCheckPostion.Entity为空";
            }
            tempBaseResultBool.ErrorInfo = tempBaseResultBool.BoolInfo;
            return tempBaseResultBool;
        }

        /// <summary>
        /// 处理签到时间与考勤设置上班时间是正常还是迟到
        /// 只有参数设置类型为固定时间才作是否异常判断
        /// </summary>
        /// <param name="empSetting">员工的考勤设置信息,可为</param>
        /// <returns></returns>
        public BaseResultBool ATEmpAttendanceEventLogCheckSignInTime(ATEmpAttendanceEventParaSettings empSetting)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.BoolFlag = true;
            if (Entity != null)
            {
                if (Entity.ATEventTypeID == ATTypeId.P签到)
                {
                    if (empSetting == null)
                    {
                        empSetting = IBATEmpAttendanceEventParaSettings.SearchATEmpAttendanceEventParaSettingsByEmpId(long.Parse(Entity.ApplyID.ToString()));
                    }
                    string info = "签到";
                    if (empSetting != null)
                    {
                        #region 员工的签到时间处理
                        //只有参数设置类型为固定时间才作是否异常判断
                        if (empSetting.ATEventParaSettingsType == 0)
                        {
                            ZhiFang.Common.Log.Log.Debug("ATEmpAttendanceEventLogCheckSignInTime:" + Entity.ApplyName + "考勤设置类型为固定时间");
                            #region 固定时间
                            string curSignInTimeStr = "", setSignInTimeStr = "";
                            DateTime curSignInTime = DateTime.MinValue, setSignInTime = DateTime.MinValue;
                            if (empSetting.SignInTime == null)
                            {
                                ZhiFang.Common.Log.Log.Debug("ATEmpAttendanceEventLogCheckSignInTime:考勤设置的签到时间为空,按签到时间按09:00:00处理");
                                if (DateTime.Now <= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 09:00:00"))
                                {
                                    this.Entity.ATEventSubTypeID = ATTypeId.P签到;
                                    this.Entity.ATEventSubTypeName = "签到正常";
                                }
                                else
                                {
                                    this.Entity.ATEventSubTypeID = ATTypeId.迟到;
                                    this.Entity.ATEventSubTypeName = "签到迟到";
                                }
                                tempBaseResultBool.BoolFlag = false;
                                tempBaseResultBool.BoolInfo = "获取" + Entity.ApplyName + "的考勤设置" + info + "时间为空!";
                            }
                            else
                            {
                                setSignInTimeStr = empSetting.SignInTime.ToString();
                            }

                            if (tempBaseResultBool.BoolFlag == true)
                            {
                                curSignInTimeStr = DateTime.Now.ToString("HH:mm");
                                setSignInTimeStr = DateTime.Parse(setSignInTimeStr).ToString("HH:mm");
                                curSignInTime = Convert.ToDateTime(curSignInTimeStr);
                                setSignInTime = Convert.ToDateTime(setSignInTimeStr);
                            }

                            if (curSignInTime != DateTime.MinValue && setSignInTime != DateTime.MinValue)
                            {
                                //员工的实际签到时间与员工考勤设置的签到时间进行比较处理
                                if (DateTime.Compare(curSignInTime, setSignInTime) > 0)
                                {
                                    Entity.ATEventSubTypeID = ATTypeId.迟到;
                                    Entity.ATEventSubTypeName = "签到迟到";
                                    tempBaseResultBool.BoolFlag = false;
                                    tempBaseResultBool.BoolInfo = Entity.ApplyName + "的" + info + "时间" + curSignInTimeStr + "为迟到,考勤设置的签到时间为" + setSignInTimeStr + "";
                                }
                                else
                                {
                                    this.Entity.ATEventSubTypeID = ATTypeId.P签到;
                                    this.Entity.ATEventSubTypeName = "签到正常";
                                }
                            }
                            else
                            {
                                ZhiFang.Common.Log.Log.Debug("ATEmpAttendanceEventLogCheckSignInTime:" + Entity.ApplyName + "的处理考勤设置的签到时间异常, 为" + setSignInTime.ToString() + "按签到时间按09:00:00处理");
                                if (DateTime.Now <= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 09:00:00"))
                                {
                                    this.Entity.ATEventSubTypeID = ATTypeId.P签到;
                                    this.Entity.ATEventSubTypeName = "签到正常";
                                }
                                else
                                {
                                    this.Entity.ATEventSubTypeID = ATTypeId.迟到;
                                    this.Entity.ATEventSubTypeName = "签到迟到";
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            //弹性时间
                            ZhiFang.Common.Log.Log.Debug("ATEmpAttendanceEventLogCheckSignInTime:" + Entity.ApplyName + "考勤设置类型为弹性时间");
                            this.Entity.ATEventSubTypeID = ATTypeId.P签到;
                            this.Entity.ATEventSubTypeName = "签到正常";
                        }
                        #endregion
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("ATEmpAttendanceEventLogCheckSignInTime:" + "获取" + Entity.ApplyName + "的考勤设置信息为空!签到时间按固定时间09:00:00处理");
                        if (DateTime.Now <= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 09:00:00"))
                        {
                            this.Entity.ATEventSubTypeID = ATTypeId.P签到;
                            this.Entity.ATEventSubTypeName = "签到正常";
                        }
                        else
                        {
                            this.Entity.ATEventSubTypeID = ATTypeId.迟到;
                            this.Entity.ATEventSubTypeName = "签到迟到";
                        }
                        tempBaseResultBool.BoolFlag = false;
                        tempBaseResultBool.BoolInfo = "获取" + Entity.ApplyName + "的考勤设置信息为空!";
                    }
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("ATEmpAttendanceEventLogCheckSignInTime.Entity为空");
                tempBaseResultBool.BoolFlag = false;
                tempBaseResultBool.BoolInfo = "ATEmpAttendanceEventLogCheckSignInTime.Entity为空";
            }
            tempBaseResultBool.ErrorInfo = tempBaseResultBool.BoolInfo;
            return tempBaseResultBool;
        }

        /// <summary>
        /// 处理签退时间与考勤设置下班时间是正常还是早退
        /// 区分固定时间和弹性时间处理
        /// </summary>
        /// <param name="empSetting">员工的考勤设置信息,可为空</param>
        /// <returns></returns>
        public BaseResultBool ATEmpAttendanceEventLogCheckSignOutTime(ATEmpAttendanceEventParaSettings empSetting)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.BoolFlag = true;
            if (Entity != null)
            {
                if (Entity.ATEventTypeID == ATTypeId.P签退)
                {
                    if (empSetting == null)
                    {
                        empSetting = IBATEmpAttendanceEventParaSettings.SearchATEmpAttendanceEventParaSettingsByEmpId(long.Parse(Entity.ApplyID.ToString()));
                    }
                    string info = "签退";

                    if (empSetting != null)
                    {
                        #region 员工的签退时间处理
                        string sTimeStr = "", setTimeStr = "";
                        DateTime setSignOutTime = DateTime.MinValue;

                        #region 弹性时间 获取员工当天签退的签到信息
                        if (tempBaseResultBool.BoolFlag == true)
                        {
                            //如果参数设置类型为弹性时间
                            if (empSetting.ATEventParaSettingsType == 1)
                            {
                                //获取员工当天签退的签到信息
                                string hqlWhere = " ApplyID=" + Entity.ApplyID + "  and ATEventDateCode='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and ATEventTypeID=" + ATTypeId.P签到;
                                //ZhiFang.Common.Log.Log.Debug("获取" + Entity.ApplyName + "当天签退的签到信息HQL:" + hqlWhere);
                                IList<ATEmpAttendanceEventLog> atemlList = DBDao.GetListByHQL(hqlWhere);
                                if (atemlList.Count > 0)
                                {
                                    sTimeStr = DateTime.Parse(atemlList[0].DataAddTime.ToString()).ToString("HH:mm");
                                    ZhiFang.Common.Log.Log.Debug(Entity.ApplyName + "当天的签到时间:" + sTimeStr);
                                }
                                else
                                {
                                    ZhiFang.Common.Log.Log.Debug("ATEmpAttendanceEventLogCheckSignOutTime:" + Entity.ApplyName + "在" + DateTime.Now.ToString("yyyy-MM-dd") + "上午未签到! 签退按固定时间18:00:00处理");
                                    if (DateTime.Now >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 18:00:00"))
                                    {
                                        this.Entity.ATEventSubTypeID = ATTypeId.P签退;
                                        this.Entity.ATEventSubTypeName = "签退正常";
                                    }
                                    else
                                    {
                                        this.Entity.ATEventSubTypeID = ATTypeId.早退;
                                        this.Entity.ATEventSubTypeName = "签退早退";
                                    }
                                    tempBaseResultBool.BoolFlag = false;
                                    tempBaseResultBool.BoolInfo = Entity.ApplyName + "在" + DateTime.Now.ToString("yyyy-MM-dd") + "上午未签到!";
                                }
                            }
                        }
                        #endregion

                        if (tempBaseResultBool.BoolFlag == true)
                        {
                            //如果参数设置类型为弹性时间
                            if (empSetting.ATEventParaSettingsType == 1)
                            {
                                //ZhiFang.Common.Log.Log.Debug("ATEmpAttendanceEventLogCheckSignOutTime:" + Entity.ApplyName + "考勤设置类型为弹性时间");
                                #region 弹性时间类型的签退时间处理
                                //签到时间
                                DateTime signTime = DateTime.Now;
                                if (!String.IsNullOrEmpty(sTimeStr))
                                    signTime = Convert.ToDateTime(sTimeStr);

                                //签退时间
                                setTimeStr = DateTime.Now.ToString("HH:mm");
                                setSignOutTime = Convert.ToDateTime(setTimeStr);

                                //签退时间-签到时间==实际的工作时长
                                TimeSpan ts = setSignOutTime.Subtract(signTime);
                                double empWorkLong = ts.TotalHours;//24.0
                                //如果弹性时间的工作时长小于考勤设置里的工作时长
                                if (empWorkLong < empSetting.EmpWorkLong)
                                {
                                    this.Entity.ATEventSubTypeID = ATTypeId.早退;
                                    this.Entity.ATEventSubTypeName = "签退早退";
                                    tempBaseResultBool.BoolFlag = false;
                                    tempBaseResultBool.BoolInfo = Entity.ApplyName + "的签退时间" + setTimeStr + "为早退,考勤设置的弹性时间工作时长为" + empSetting.EmpWorkLong + "小时,当前的工作时长为" + String.Format("{0:N1}", empWorkLong) + "小时";
                                }
                                else
                                {
                                    this.Entity.ATEventSubTypeID = ATTypeId.P签退;
                                    this.Entity.ATEventSubTypeName = "签退正常";
                                }
                                #endregion
                            }
                            else
                            {
                                #region 固定时间类型的签退时间处理
                                ZhiFang.Common.Log.Log.Debug("ATEmpAttendanceEventLogCheckSignOutTime:" + Entity.ApplyName + "考勤设置类型为固定时间");
                                if (empSetting.SignOutTime == null)
                                {
                                    ZhiFang.Common.Log.Log.Debug("ATEmpAttendanceEventLogCheckSignOutTime:" + Entity.ApplyName + "考勤设置签退时间为空,签退按固定时间18:00:00处理");
                                    if (DateTime.Now >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 18:00:00"))
                                    {
                                        this.Entity.ATEventSubTypeID = ATTypeId.P签退;
                                        this.Entity.ATEventSubTypeName = "签退正常";
                                    }
                                    else
                                    {
                                        this.Entity.ATEventSubTypeID = ATTypeId.早退;
                                        this.Entity.ATEventSubTypeName = "签退早退";
                                    }
                                    tempBaseResultBool.BoolFlag = false;
                                    tempBaseResultBool.BoolInfo = "获取" + Entity.ApplyName + "的考勤设置" + info + "时间为空!";
                                }
                                else
                                {
                                    setTimeStr = empSetting.SignOutTime.ToString();
                                }
                                //考勤设置的签退时间不为空
                                if (tempBaseResultBool.BoolFlag == true)
                                {
                                    setTimeStr = DateTime.Parse(setTimeStr).ToString("HH:mm");

                                    //当前签退时间
                                    sTimeStr = DateTime.Now.ToString("HH:mm");
                                    DateTime signOutTime = Convert.ToDateTime(sTimeStr);
                                    //考勤设置的签退时间
                                    setSignOutTime = Convert.ToDateTime(setTimeStr);
                                    if (DateTime.Compare(signOutTime, setSignOutTime) < 0)
                                    {
                                        this.Entity.ATEventSubTypeID = ATTypeId.早退;
                                        this.Entity.ATEventSubTypeName = "签退早退";
                                        tempBaseResultBool.BoolFlag = false;
                                        tempBaseResultBool.BoolInfo = Entity.ApplyName + "签退时间" + sTimeStr + "为早退,考勤设置的签退时间为" + setTimeStr;
                                    }
                                    else
                                    {
                                        this.Entity.ATEventSubTypeID = ATTypeId.P签退;
                                        this.Entity.ATEventSubTypeName = "签退正常";
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("ATEmpAttendanceEventLogCheckSignOutTime:" + Entity.ApplyName + "考勤设置信息为空,签退时间按固定时间18:00:00处理");
                        if (DateTime.Now >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 18:00:00"))
                        {
                            this.Entity.ATEventSubTypeID = ATTypeId.P签退;
                            this.Entity.ATEventSubTypeName = "签退正常";
                        }
                        else
                        {
                            this.Entity.ATEventSubTypeID = ATTypeId.早退;
                            this.Entity.ATEventSubTypeName = "签退早退";
                        }
                        tempBaseResultBool.BoolFlag = false;
                        tempBaseResultBool.BoolInfo = "获取" + Entity.ApplyName + "的考勤设置信息为空!";
                    }
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("ATEmpAttendanceEventLogCheckSignOutTime.Entity为空");
                tempBaseResultBool.BoolFlag = false;
                tempBaseResultBool.BoolInfo = "ATEmpAttendanceEventLogCheckSignOutTime.Entity为空";
            }
            tempBaseResultBool.ErrorInfo = tempBaseResultBool.BoolInfo;
            return tempBaseResultBool;
        }

        /// <summary>
        /// 获取两个火星坐标之间的距离
        /// </summary>
        /// <param name="latEmp">第一个坐标的X</param>
        /// <param name="lngEmp">第一个坐标的Y</param>
        /// <param name="latSet">第二个坐标的X</param>
        /// <param name="lngSet">第二个坐标的Y</param>
        /// <returns>两个坐标之间的距离(单位为米)</returns>
        public double GetDistance(double latEmp, double lngEmp, double latSet, double lngSet)
        {
            try
            {
                var b = Math.PI / 180;
                var c = Math.Sin((latSet - latEmp) * b / 2);
                var d = Math.Sin((lngSet - lngEmp) * b / 2);
                var a = c * c + d * d * Math.Cos(latEmp * b) * Math.Cos(latSet * b);
                var distance = 12756274 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                distance = Math.Abs(distance);
                ZhiFang.Common.Log.Log.Debug("计算两个坐标之间的距离结果为:" + distance);
                return distance;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("计算两个坐标之间的距离出错:" + ex.Message);
                return 0;
            }
        }
        #endregion

        #region 考勤统计
        /// <summary>
        /// 获取公司所有员工的考勤统计信息
        /// </summary>
        /// <param name="monthCode">统计月的年月</param>
        /// <param name="wagesDays">统计月的工资日总天数</param>
        /// <param name="punch">每天的签到打卡次数</param>
        /// <returns></returns>
        public IList<ATEmpMonthLogCount> GetAllMonthLogCountList(string monthCode, int wagesDays, int punch)
        {
            if (punch < 1)
            {
                punch = 1;
            }
            DateTime firstDate = Convert.ToDateTime(monthCode + "-01");
            int days = DateTime.DaysInMonth(firstDate.Year, firstDate.Month);

            //获取统计月的所有日期集合信息
            Dictionary<string, DayOfWeek> allDates = IBATHolidaySetting.GetAllDatesOfOneMonth(firstDate.Year, firstDate.Month);
            //公司的所有员工月考勤统计信息
            IList<ATEmpMonthLogCount> atemCount = new List<ATEmpMonthLogCount>();

            //公司统计月的所有考勤登记信息
            IList<ATEmpAttendanceEventLog> atemlAllList = new List<ATEmpAttendanceEventLog>();

            //公司统计月的(出差,请假,外出,加班)考勤登记信息
            IList<ATEmpAttendanceEventLog> atemlOtherList = new List<ATEmpAttendanceEventLog>();
            //两日期相减得到的天数
            double subtractDays = 0;
            //两日期相减得到的小时数
            double subtractHours = 0;
            double evenLength = 0;
            //统计月已经计算过请假天数的日期集合
            IList<string> leaveDates = new List<string>();
            //获取该月份的所有工作日期的集合
            Dictionary<string, DayOfWeek> allWorkDays = IBATHolidaySetting.GetAllWorkDaysOfOneMonth(firstDate.Year, firstDate.Month);

            //无签到记录或无签退记录的工作日集合
            Dictionary<string, long?> noSignOfWorkDays = new Dictionary<string, long?>();
            //无签到记录的工作日集合
            Dictionary<string, long?> noSignInOfWorkDays = new Dictionary<string, long?>();
            //无签退记录的工作日集合
            Dictionary<string, long?> noSignOutOfWorkDays = new Dictionary<string, long?>();
            //补签卡记录的工作日集合
            IList<string> fillCardsOfWorkDays = new List<string>();

            #region 公司所有的员工信息及空考勤统计信息
            string empidlist = " 0 ";

            if (true)//权限判定，暂定为True
            {
                IList<HRDept> deptlist = IDHRDeptDao.GetListByHQL(" IsUse=true ");
                //Common.Log.Log.Debug("deptlist获取的记录行数:" + deptlist.Count);
                if (deptlist != null && deptlist.Count > 0)
                {
                    for (int i = 0; i < deptlist.Count; i++)
                    {
                        IList<HREmployee> emplist = deptlist[i].HREmployeeList;
                        if (emplist != null && emplist.Count > 0)
                        {
                            foreach (var emp in emplist)
                            {
                                if (emp.IsUse.HasValue && emp.IsUse.Value)
                                {
                                    empidlist += "," + emp.Id;
                                    ATEmpMonthLogCount model = new ATEmpMonthLogCount();
                                    model.EmpID = emp.Id;
                                    //model.EmpNo = emp.UseCode;
                                    model.EmpName = emp.CName;
                                    model.DeptName = emp.HRDept.CName;
                                    model.IsFullAttendance = "";
                                    atemCount.Add(model);
                                }

                            }
                        }
                    }
                }
            }
            #endregion

            #region 签到签退处理
            string hqlWhere1 = " ApplyID in(" + empidlist + ")  and DataAddTime>='" + firstDate.ToString("yyyy-MM-dd") + "' and DataAddTime<='" + firstDate.AddDays(days - 1).ToString("yyyy-MM-dd") + " 23:59:59' ";
            atemlAllList = DBDao.GetListByHQL(hqlWhere1);
            if (atemlAllList.Count() > 0)
            {
                //循环公司员工的考勤统计处理
                for (int i = 0; i < atemCount.Count; i++)
                {
                    subtractDays = 0;
                    subtractHours = 0;
                    //工资日(暂时由前台传入)
                    atemCount[i].WagesDays = wagesDays;

                    //签到次数
                    var signin = atemlAllList.Where(s => (s.ApplyID == atemCount[i].EmpID && s.ATEventTypeID == ATTypeId.P签到));
                    if (signin != null)
                    {
                        #region 无签到记录的工作日集合
                        foreach (var tempItem in allWorkDays)
                        {
                            var tempList = signin.Where(s => s.DataAddTime.Value.ToString("yyyy-MM-dd") == tempItem.Key && s.ApplyID == atemCount[i].EmpID);
                            if (tempList == null || tempList.Count() < 1)
                            {
                                string keyValue = tempItem.Key + ";" + atemCount[i].EmpID;
                                if (!noSignInOfWorkDays.ContainsKey(keyValue))
                                    noSignInOfWorkDays.Add(keyValue, atemCount[i].EmpID);

                                if (!noSignOfWorkDays.ContainsKey(keyValue))
                                    noSignOfWorkDays.Add(keyValue, atemCount[i].EmpID);
                            }
                        }

                        #endregion
                        var empIDNoSignInOfWorkDays = noSignInOfWorkDays.Where(s => s.Value == atemCount[i].EmpID);
                        atemCount[i].SignInCount = (allWorkDays.Count() - empIDNoSignInOfWorkDays.Count()) * punch;
                        //签到天数=(统计月的工作日天数-无签到记录的工作日天数)
                        atemCount[i].SignInDays = allWorkDays.Count() - empIDNoSignInOfWorkDays.Count();
                    }

                    //迟到次数
                    var late = atemlAllList.Where(s => (s.ApplyID == atemCount[i].EmpID && s.ATEventTypeID == ATTypeId.P签到 && s.ATEventSubTypeID == ATTypeId.迟到));
                    if (late != null)
                        atemCount[i].LateCount = late.Count();

                    //签退次数
                    var signOut = atemlAllList.Where(s => (s.ApplyID == atemCount[i].EmpID && s.ATEventTypeID == ATTypeId.P签退));
                    if (signOut != null)
                    {
                        #region 无签退记录的工作日集合
                        foreach (var tempItem in allWorkDays)
                        {
                            var tempList = signOut.Where(s => s.DataAddTime.Value.ToString("yyyy-MM-dd") == tempItem.Key);
                            if (tempList == null || tempList.Count() < 1)
                            {
                                string keyValue = tempItem.Key + ";" + atemCount[i].EmpID;
                                if (!noSignOutOfWorkDays.ContainsKey(keyValue))
                                    noSignOutOfWorkDays.Add(keyValue, atemCount[i].EmpID);

                                if (!noSignOfWorkDays.ContainsKey(keyValue))
                                    noSignOfWorkDays.Add(keyValue, atemCount[i].EmpID);
                            }
                        }

                        #endregion
                        var empIDNoSignOutOfWorkDays = noSignOutOfWorkDays.Where(s => s.Value == atemCount[i].EmpID);
                        atemCount[i].SignOutCount = (allWorkDays.Count() - empIDNoSignOutOfWorkDays.Count()) * punch;
                        //签退天数=统计月的工作日天数-无签退记录的工作日天数
                        atemCount[i].SignOutDays = (allWorkDays.Count() - empIDNoSignOutOfWorkDays.Count());
                    }

                    //早退次数
                    var leaveEarly = atemlAllList.Where(s => (s.ApplyID == atemCount[i].EmpID && s.ATEventTypeID == ATTypeId.P签退 && s.ATEventSubTypeID == ATTypeId.早退));
                    if (leaveEarly != null)
                        atemCount[i].LeaveEarlyCount = leaveEarly.Count();

                }
            }
            #endregion

            #region 请假及其他处理
            //开始时间<=11.30 and 结束时间>=11.1
            string hqlWhere = " ApplyID in(" + empidlist + ")  and  ATEventTypeID in (" + ATTypeId.P请假 + "," + ATTypeId.P外出 + "," + ATTypeId.P出差 + "," + ATTypeId.P加班 + ")  and (StartDateTime<='" + firstDate.AddDays(days - 1).ToString("yyyy-MM-dd") + "  23:59:59'  and EndDateTime>='" + firstDate.ToString("yyyy-MM-dd") + "' )";
            atemlOtherList = DBDao.GetListByHQL(hqlWhere);
            if (atemlOtherList.Count() > 0)
            {
                //循环公司员工的考勤统计处理
                for (int i = 0; i < atemCount.Count; i++)
                {
                    //工资日(暂时由前台传入)
                    atemCount[i].WagesDays = wagesDays;
                    #region 请假处理

                    #region 补签卡
                    subtractDays = 0;
                    subtractHours = 0;
                    leaveDates.Clear();
                    var fillCardsLeave = atemlOtherList.Where(s => (s.ApplyID == atemCount[i].EmpID && s.ATEventTypeID == ATTypeId.P请假 && s.ATEventSubTypeID == ATTypeId.补签卡));
                    //计算该员工当月的补签卡总天数
                    if (fillCardsLeave != null)
                    {
                        for (int j = 0; j < fillCardsLeave.Count(); j++)
                        {
                            evenLength = 0;
                            evenLength = GetEvenLength(firstDate, allDates, allWorkDays, fillCardsLeave.ElementAt(j), wagesDays, ref leaveDates, false);
                            subtractDays = subtractDays + evenLength;
                        }
                        foreach (var item in leaveDates)
                        {
                            NoSignOfWorkDaysRemove(item, atemCount[i].EmpID, allWorkDays, ref noSignOfWorkDays, ref noSignInOfWorkDays, ref noSignOutOfWorkDays);
                        }
                        subtractDays = (subtractDays > days ? days : subtractDays);
                        atemCount[i].FillCardsDays = subtractDays;

                    }
                    #endregion

                    #region 签到签退二次处理
                    var empIDNoSignInOfWorkDays = noSignInOfWorkDays.Where(s => s.Value == atemCount[i].EmpID);
                    atemCount[i].SignInCount = (allWorkDays.Count() - empIDNoSignInOfWorkDays.Count()) * punch;
                    //签到天数=(统计月的工作日天数-无签到记录的工作日天数)
                    atemCount[i].SignInDays = allWorkDays.Count() - empIDNoSignInOfWorkDays.Count();
                    //if (empIDNoSignInOfWorkDays.Count() > 0)
                    //{
                    //    ZhiFang.Common.Log.Log.Debug(atemCount[i].EmpName + monthCode + "无签到记录的天数有:" + empIDNoSignInOfWorkDays.Count());
                    //}
                    var empIDNoSignOutOfWorkDays = noSignOutOfWorkDays.Where(s => s.Value == atemCount[i].EmpID);
                    atemCount[i].SignOutCount = (allWorkDays.Count() - empIDNoSignOutOfWorkDays.Count()) * punch;
                    //签退天数=统计月的工作日天数-无签退记录的工作日天数
                    atemCount[i].SignOutDays = (allWorkDays.Count() - empIDNoSignOutOfWorkDays.Count());

                    #endregion

                    #region 事假,按天计算
                    subtractDays = 0;
                    subtractHours = 0;
                    leaveDates.Clear();
                    var jobLeave = atemlOtherList.Where(s => (s.ApplyID == atemCount[i].EmpID && s.ATEventTypeID == ATTypeId.P请假 && s.ATEventSubTypeID == ATTypeId.事假));
                    //计算该员工当月的事假总天数
                    if (jobLeave != null)
                    {
                        for (int j = 0; j < jobLeave.Count(); j++)
                        {
                            evenLength = 0;
                            evenLength = GetEvenLength(firstDate, allDates, allWorkDays, jobLeave.ElementAt(j), wagesDays, ref leaveDates, false);
                            subtractDays = subtractDays + evenLength;
                        }
                        foreach (var item in leaveDates)
                        {
                            NoSignOfWorkDaysRemove(item, atemCount[i].EmpID, allWorkDays, ref noSignOfWorkDays, ref noSignInOfWorkDays, ref noSignOutOfWorkDays);
                        }
                        subtractDays = (subtractDays > days ? days : subtractDays);
                        atemCount[i].JobLeaveDays = subtractDays;
                    }
                    #endregion
                    #region 病假,按天计算
                    subtractDays = 0;
                    subtractHours = 0;
                    leaveDates.Clear();
                    var sickLeave = atemlOtherList.Where(s => (s.ApplyID == atemCount[i].EmpID && s.ATEventTypeID == ATTypeId.P请假 && s.ATEventSubTypeID == ATTypeId.病假));
                    if (sickLeave != null)
                    {
                        //计算该员工当月的病假总天数
                        for (int j = 0; j < sickLeave.Count(); j++)
                        {
                            evenLength = 0;
                            evenLength = GetEvenLength(firstDate, allDates, allWorkDays, sickLeave.ElementAt(j), wagesDays, ref leaveDates, false);
                            subtractDays = subtractDays + evenLength;
                        }
                        foreach (var item in leaveDates)
                        {
                            NoSignOfWorkDaysRemove(item, atemCount[i].EmpID, allWorkDays, ref noSignOfWorkDays, ref noSignInOfWorkDays, ref noSignOutOfWorkDays);
                        }
                        subtractDays = (subtractDays > days ? days : subtractDays);
                        atemCount[i].SickLeaveDays = subtractDays;
                    }
                    #endregion
                    #region 婚假,按天计算
                    subtractDays = 0;
                    subtractHours = 0;
                    leaveDates.Clear();
                    var marriageLeave = atemlOtherList.Where(s => (s.ApplyID == atemCount[i].EmpID && s.ATEventTypeID == ATTypeId.P请假 && s.ATEventSubTypeID == ATTypeId.婚假));
                    if (marriageLeave != null)
                    {
                        //计算该员工当月的婚假总天数
                        for (int j = 0; j < marriageLeave.Count(); j++)
                        {
                            evenLength = 0;
                            evenLength = GetEvenLength(firstDate, allDates, allWorkDays, marriageLeave.ElementAt(j), wagesDays, ref leaveDates, false);
                            subtractDays = subtractDays + evenLength;
                        }
                        foreach (var item in leaveDates)
                        {
                            NoSignOfWorkDaysRemove(item, atemCount[i].EmpID, allWorkDays, ref noSignOfWorkDays, ref noSignInOfWorkDays, ref noSignOutOfWorkDays);
                        }
                        subtractDays = (subtractDays > days ? days : subtractDays);
                        atemCount[i].MarriageLeaveDays = subtractDays;
                    }
                    #endregion
                    #region 产假
                    subtractDays = 0;
                    subtractHours = 0;
                    leaveDates.Clear();
                    var maternityLeave = atemlOtherList.Where(s => (s.ApplyID == atemCount[i].EmpID && s.ATEventTypeID == ATTypeId.P请假 && s.ATEventSubTypeID == ATTypeId.产假));
                    if (maternityLeave != null)
                    {
                        //计算该员工当月的产假总天数
                        for (int j = 0; j < maternityLeave.Count(); j++)
                        {
                            evenLength = 0;
                            evenLength = GetEvenLength(firstDate, allDates, allWorkDays, maternityLeave.ElementAt(j), wagesDays, ref leaveDates, false);
                            subtractDays = subtractDays + evenLength;
                        }
                        foreach (var item in leaveDates)
                        {
                            NoSignOfWorkDaysRemove(item, atemCount[i].EmpID, allWorkDays, ref noSignOfWorkDays, ref noSignInOfWorkDays, ref noSignOutOfWorkDays);
                        }
                        subtractDays = (subtractDays > days ? days : subtractDays);
                        atemCount[i].MaternityLeaveDays = subtractDays;
                    }
                    #endregion
                    #region 护理假,按天计算
                    subtractDays = 0;
                    subtractHours = 0;
                    leaveDates.Clear();
                    var careLeave = atemlOtherList.Where(s => (s.ApplyID == atemCount[i].EmpID && s.ATEventTypeID == ATTypeId.P请假 && s.ATEventSubTypeID == ATTypeId.护理假));
                    if (careLeave != null)
                    {
                        //计算该员工当月的护理假总天数
                        for (int j = 0; j < careLeave.Count(); j++)
                        {
                            evenLength = 0;
                            evenLength = GetEvenLength(firstDate, allDates, allWorkDays, careLeave.ElementAt(j), wagesDays, ref leaveDates, false);
                            subtractDays = subtractDays + evenLength;
                        }
                        foreach (var item in leaveDates)
                        {
                            NoSignOfWorkDaysRemove(item, atemCount[i].EmpID, allWorkDays, ref noSignOfWorkDays, ref noSignInOfWorkDays, ref noSignOutOfWorkDays);
                        }
                        subtractDays = (subtractDays > days ? days : subtractDays);
                        atemCount[i].CareLeaveDays = subtractDays;
                    }
                    #endregion
                    #region 丧假,按天计算
                    subtractDays = 0;
                    subtractHours = 0;
                    leaveDates.Clear();
                    var bBereavementLeave = atemlOtherList.Where(s => (s.ApplyID == atemCount[i].EmpID && s.ATEventTypeID == ATTypeId.P请假 && s.ATEventSubTypeID == ATTypeId.丧假));
                    if (bBereavementLeave != null)
                    {
                        //计算该员工当月的丧假总天数
                        for (int j = 0; j < bBereavementLeave.Count(); j++)
                        {
                            evenLength = 0;
                            evenLength = GetEvenLength(firstDate, allDates, allWorkDays, bBereavementLeave.ElementAt(j), wagesDays, ref leaveDates, false);
                            subtractDays = subtractDays + evenLength;
                        }
                        foreach (var item in leaveDates)
                        {
                            NoSignOfWorkDaysRemove(item, atemCount[i].EmpID, allWorkDays, ref noSignOfWorkDays, ref noSignInOfWorkDays, ref noSignOutOfWorkDays);
                        }
                        subtractDays = (subtractDays > days ? days : subtractDays);
                        atemCount[i].BereavementLeaveDays = subtractDays;
                    }
                    #endregion
                    #region 调休,按天计算
                    subtractDays = 0;
                    subtractHours = 0;
                    leaveDates.Clear();
                    var adjustTheBreak = atemlOtherList.Where(s => (s.ApplyID == atemCount[i].EmpID && s.ATEventTypeID == ATTypeId.P请假 && s.ATEventSubTypeID == ATTypeId.调休));
                    if (adjustTheBreak.Count() > 0)
                    {
                        //计算该员工当月的调休总天数
                        for (int j = 0; j < adjustTheBreak.Count(); j++)
                        {
                            evenLength = 0;
                            evenLength = GetEvenLength(firstDate, allDates, allWorkDays, adjustTheBreak.ElementAt(j), wagesDays, ref leaveDates, false);
                            subtractDays = subtractDays + evenLength;
                        }
                        foreach (var item in leaveDates)
                        {
                            NoSignOfWorkDaysRemove(item, atemCount[i].EmpID, allWorkDays, ref noSignOfWorkDays, ref noSignInOfWorkDays, ref noSignOutOfWorkDays);
                        }
                        subtractDays = (subtractDays > days ? days : subtractDays);
                        atemCount[i].AdjustTheBreakDays = subtractDays;
                    }
                    #endregion
                    #region 年假,按天计算
                    subtractDays = 0;
                    subtractHours = 0;
                    leaveDates.Clear();
                    var annualLeave = atemlOtherList.Where(s => (s.ApplyID == atemCount[i].EmpID && s.ATEventTypeID == ATTypeId.P请假 && s.ATEventSubTypeID == ATTypeId.年假));
                    if (annualLeave.Count() > 0)
                    {
                        //计算该员工当月的年假总天数
                        for (int j = 0; j < annualLeave.Count(); j++)
                        {
                            evenLength = 0;
                            evenLength = GetEvenLength(firstDate, allDates, allWorkDays, annualLeave.ElementAt(j), wagesDays, ref leaveDates, false);
                            subtractDays = subtractDays + evenLength;
                        }
                        foreach (var item in leaveDates)
                        {
                            NoSignOfWorkDaysRemove(item, atemCount[i].EmpID, allWorkDays, ref noSignOfWorkDays, ref noSignInOfWorkDays, ref noSignOutOfWorkDays);
                        }
                        subtractDays = double.Parse(String.Format("{0:F2}", subtractDays), System.Globalization.NumberStyles.Float);
                        subtractDays = (subtractDays > days ? days : subtractDays);
                        atemCount[i].AnnualLeaveDays = subtractDays;
                    }
                    #endregion

                    #endregion
                    #region 外出,按记录行(过滤相同的开始时间)计算
                    subtractDays = 0;
                    subtractHours = 0;
                    leaveDates.Clear();
                    var EgressList = atemlOtherList.Where(s => s.ApplyID == atemCount[i].EmpID && s.ATEventTypeID == ATTypeId.P外出);

                    if (EgressList.Count() > 0)
                    {
                        //某一员工的外出总天数计算
                        for (int k = 0; k < EgressList.Count(); k++)
                        {
                            string curDate = EgressList.ElementAt(k).StartDateTime.Value.ToString("yyyy-MM-dd");
                            if (!leaveDates.Contains(curDate))
                            {
                                leaveDates.Add(curDate);
                                subtractDays = subtractDays + 1;
                            }
                        }
                        subtractDays = (subtractDays > days ? days : subtractDays);
                        atemCount[i].EgressDays = subtractDays;
                    }
                    #endregion
                    #region 加班,按小时计算
                    subtractDays = 0;
                    subtractHours = 0;
                    var overtimeList = atemlOtherList.Where(s => s.ApplyID == atemCount[i].EmpID && s.ATEventTypeID == ATTypeId.P加班);
                    if (overtimeList.Count() > 0)
                    {
                        //某一员工的加班总小时计算
                        for (int k = 0; k < overtimeList.Count(); k++)
                        {
                            subtractHours = subtractHours + overtimeList.ElementAt(k).EvenLength;
                        }
                        //将加班的总小时数换算为天数(工作日每天为8小时),String.Format("{0:F2}",  System.Globalization.NumberStyles.Float
                        if (subtractHours > 0)
                        {
                            subtractDays = this.ChangeHoursToDays(subtractHours);
                            subtractDays = double.Parse(String.Format("{0:F2}", subtractDays), System.Globalization.NumberStyles.Float);
                        }
                        atemCount[i].OvertimeDays = subtractDays;
                    }
                    #endregion
                    #region 出差,按天计算

                    #region 出差天数,只统计统计月的日期范围内的工作日出差天数
                    subtractDays = 0;
                    subtractHours = 0;
                    leaveDates.Clear();

                    var tripList = atemlOtherList.Where(s => (s.ApplyID == atemCount[i].EmpID && s.ATEventTypeID == ATTypeId.P出差 && s.ATEventSubTypeID == ATTypeId.P出差));
                    if (tripList.Count() > 0)
                    {
                        for (int j = 0; j < tripList.Count(); j++)
                        {
                            evenLength = 0;
                            evenLength = GetEvenLength(firstDate, allDates, allWorkDays, tripList.ElementAt(j), wagesDays, ref leaveDates, true);
                            subtractDays = subtractDays + evenLength;
                        }
                        subtractDays = (subtractDays > days ? days : subtractDays);
                        atemCount[i].TripDays = subtractDays;
                    }
                    #endregion

                    //出差存休(按实际出差天数来统计)
                    subtractDays = 0;
                    subtractHours = 0;
                    leaveDates.Clear();
                    if (tripList.Count() > 0)
                    {
                        for (int j = 0; j < tripList.Count(); j++)
                        {
                            evenLength = 0;
                            evenLength = GetEvenLength(firstDate, allDates, allWorkDays, tripList.ElementAt(j), wagesDays, ref leaveDates, false);
                            subtractDays = subtractDays + evenLength;
                        }
                        subtractDays = (subtractDays > days ? days : subtractDays);
                        atemCount[i].TravelHoliday = subtractDays;
                    }
                    #endregion

                    #region 旷工
                    var empIDNoSignOfWorkDays = noSignOfWorkDays.Where(s => s.Value == atemCount[i].EmpID);
                    //if (empIDNoSignOfWorkDays.Count() > 0)
                    //{
                    //    ZhiFang.Common.Log.Log.Debug(atemCount[i].EmpName + monthCode + "无签到或签退记录的天数有:" + empIDNoSignOfWorkDays.Count());
                    //}
                    //旷工=工资日-签到天数-事假-病假-婚假-产假-护理假-丧假-调休-年假-出差-外出
                    //atemCount[i].AbsenteeismDays = atemCount[i].WagesDays - atemCount[i].SignInDays - atemCount[i].JobLeaveDays - atemCount[i].SickLeaveDays - atemCount[i].MarriageLeaveDays - atemCount[i].MaternityLeaveDays - atemCount[i].CareLeaveDays - atemCount[i].BereavementLeaveDays - atemCount[i].AdjustTheBreakDays - atemCount[i].AnnualLeaveDays - atemCount[i].TripDays - atemCount[i].EgressDays;
                    atemCount[i].AbsenteeismDays = empIDNoSignOfWorkDays.Count();
                    if (atemCount[i].AbsenteeismDays < 0)
                    {
                        atemCount[i].AbsenteeismDays = 0;
                    }
                    #endregion

                    //入职缺勤天数(暂不需要统计)
                    //离职缺勤天数(不需要统计)

                    //未打卡(次数)=(工资日-签到天数)*punch;一天打卡几次由前台传入,
                    atemCount[i].NotPunch = (atemCount[i].WagesDays - atemCount[i].SignInDays) * punch;
                    if (atemCount[i].NotPunch < 0)
                    {
                        atemCount[i].NotPunch = 0;
                    }

                    #region 缺勤天数
                    //缺勤天数=工资日-签到天数-事假-病假-婚假-产假-护理假-丧假--调休-年假-出差-外出
                    //atemCount[i].DaysOfAbsence = atemCount[i].WagesDays - atemCount[i].SignInDays - atemCount[i].JobLeaveDays - atemCount[i].SickLeaveDays - atemCount[i].MarriageLeaveDays - atemCount[i].MaternityLeaveDays - atemCount[i].CareLeaveDays - atemCount[i].BereavementLeaveDays - atemCount[i].AdjustTheBreakDays - atemCount[i].AnnualLeaveDays - atemCount[i].TripDays - atemCount[i].EgressDays;
                    atemCount[i].DaysOfAbsence = empIDNoSignOfWorkDays.Count();
                    if (atemCount[i].DaysOfAbsence < 0)
                    {
                        atemCount[i].DaysOfAbsence = 0;
                    }
                    #endregion
                    #region 在公司日
                    //在公司日=工资日-事假-旷工-病假-婚假-产假-护理假-丧假-调休-年假-出差-外出
                    atemCount[i].CompanyDays = atemCount[i].WagesDays - atemCount[i].JobLeaveDays - atemCount[i].AbsenteeismDays - atemCount[i].SickLeaveDays - atemCount[i].MarriageLeaveDays - atemCount[i].MaternityLeaveDays - atemCount[i].CareLeaveDays - atemCount[i].BereavementLeaveDays - atemCount[i].AdjustTheBreakDays - atemCount[i].AnnualLeaveDays - atemCount[i].TripDays - atemCount[i].EgressDays;
                    if (atemCount[i].CompanyDays < 0)
                    {
                        atemCount[i].CompanyDays = 0;
                    }
                    #endregion
                    #region 是否全勤处理(暂不需要统计和显示)
                    //只要有请假记录,是否全勤为否

                    //事假大于0
                    atemCount[i].IsFullAttendance = (atemCount[i].JobLeaveDays > 0 ? "否" : atemCount[i].IsFullAttendance);
                    //病假大于0
                    atemCount[i].IsFullAttendance = (atemCount[i].SickLeaveDays > 0 ? "否" : atemCount[i].IsFullAttendance);
                    //婚假大于0
                    atemCount[i].IsFullAttendance = (atemCount[i].MarriageLeaveDays > 0 ? "否" : atemCount[i].IsFullAttendance);
                    //产假大于0
                    atemCount[i].IsFullAttendance = (atemCount[i].CareLeaveDays > 0 ? "否" : atemCount[i].IsFullAttendance);
                    //护理假大于0
                    atemCount[i].IsFullAttendance = (atemCount[i].BereavementLeaveDays > 0 ? "否" : atemCount[i].IsFullAttendance);
                    //丧假大于0
                    atemCount[i].IsFullAttendance = (atemCount[i].AnnualLeaveDays > 0 ? "否" : atemCount[i].IsFullAttendance);
                    //年假大于0
                    atemCount[i].IsFullAttendance = (atemCount[i].AnnualLeaveDays > 0 ? "否" : atemCount[i].IsFullAttendance);

                    //如果是否全勤不为否,值为"是"
                    if (atemCount[i].IsFullAttendance != "否")
                        atemCount[i].IsFullAttendance = "是";
                    #endregion

                }
            }
            #endregion

            return atemCount;
        }
        private void NoSignOfWorkDaysRemove(string date, long? empID, Dictionary<string, DayOfWeek> allWorkDays, ref Dictionary<string, long?> noSignOfWorkDays, ref Dictionary<string, long?> noSignInOfWorkDays, ref Dictionary<string, long?> noSignOutOfWorkDays)
        {
            //无签到记录或无签退记录的工作日天数的请假天数排除处理
            foreach (var tempItem in allWorkDays)
            {
                string keyValue = date + ";" + empID;
                if (noSignInOfWorkDays.ContainsKey(keyValue))
                    noSignInOfWorkDays.Remove(keyValue);

                if (noSignOutOfWorkDays.ContainsKey(keyValue))
                    noSignOutOfWorkDays.Remove(keyValue);

                if (noSignOfWorkDays.ContainsKey(keyValue))
                    noSignOfWorkDays.Remove(keyValue);
            }
        }

        /// <summary>
        /// 获取请假的实际请假天数
        /// </summary>
        /// <param name="firstDate">统计月的第一天</param>
        /// <param name="ateml"></param>
        /// <param name="allDates">获取统计月的所有日期集合信息</param>
        /// /// <param name="allWorkDays">获取统计月的所有工作日集合信息</param>
        /// <param name="entity">某一员工统计月的某一请假类型的集合</param>
        /// <param name="entity">出差是按工作日还是按自然日统计</param>
        /// <returns></returns>
        private double GetEvenLength(DateTime firstDate, Dictionary<string, DayOfWeek> allDates, Dictionary<string, DayOfWeek> allWorkDays, ATEmpAttendanceEventLog entity, int wagesDays, ref IList<string> leaveDates, bool isCountAllTrip)
        {
            double evenLength = 0;
            //当前日期范围内在统计月的请假日期集合
            //IList<string> curLeaveDates = new List<string>();

            string startDate = firstDate.ToString("yyyy-MM-dd");
            int days = DateTime.DaysInMonth(firstDate.Year, firstDate.Month);
            string endDate = firstDate.AddDays(days - 1).ToString("yyyy-MM-dd");
            int leaveDatesType = 0;
            string startDay = entity.StartDateTime.Value.ToString("dd");
            DateTime startDateTime = DateTime.Parse(entity.StartDateTime.Value.ToString("yyyy-MM-dd"));
            DateTime endDateTime = DateTime.Parse(entity.EndDateTime.Value.ToString("yyyy-MM-dd"));
            leaveDatesType = GetLeaveDatesType(firstDate, allWorkDays, entity);
            //Common.Log.Log.Debug(entity.ApplyName + entity.ATEventSubTypeName + ",leaveDatesType:" + leaveDatesType);
            //事假,病假,年假,出差天数是按统计月的工作日计算;在统计婚假、丧假、产假、护理假是按统计月的自然日计算
            switch (leaveDatesType)
            {
                case 1:
                    #region 日期范围开始时间及结束时间都在统计月
                    switch (entity.ATEventSubTypeID)
                    {
                        case ATTypeId.事假:

                        case ATTypeId.病假:

                        case ATTypeId.年假:
                            evenLength = 0;
                            for (DateTime temp = startDateTime; temp <= endDateTime; temp = temp.AddDays(1))
                            {
                                string curdate = temp.ToString("yyyy-MM-dd");
                                if (allWorkDays.ContainsKey(curdate) && !leaveDates.Contains(curdate))
                                {
                                    leaveDates.Add(curdate);
                                    evenLength = evenLength + 1;
                                }
                            }
                            break;
                        default:
                            if (isCountAllTrip)
                            {
                                evenLength = 0;
                                for (DateTime temp = startDateTime; temp <= endDateTime; temp = temp.AddDays(1))
                                {
                                    string curdate = temp.ToString("yyyy-MM-dd");
                                    if (allWorkDays.ContainsKey(curdate) && !leaveDates.Contains(curdate))
                                    {
                                        leaveDates.Add(curdate);
                                        evenLength = evenLength + 1;
                                    }
                                }
                            }
                            else
                            {
                                evenLength = 0;
                                for (DateTime temp = startDateTime; temp <= endDateTime; temp = temp.AddDays(1))
                                {
                                    string curdate = temp.ToString("yyyy-MM-dd");
                                    if (!leaveDates.Contains(curdate))
                                    {
                                        leaveDates.Add(curdate);
                                        evenLength = evenLength + 1;
                                    }
                                }
                            }
                            break;
                    }

                    #endregion
                    break;
                case 2:
                    #region 日期范围开始时间在统计月,结束时间不在(大于)统计月
                    switch (entity.ATEventSubTypeID)
                    {
                        case ATTypeId.事假:
                        case ATTypeId.病假:
                        case ATTypeId.年假:
                            evenLength = 0;
                            for (DateTime temp = startDateTime; temp <= firstDate.AddDays(days - 1); temp = temp.AddDays(1))
                            {
                                string curdate = temp.ToString("yyyy-MM-dd");
                                if (allWorkDays.ContainsKey(curdate) && !leaveDates.Contains(curdate))
                                {
                                    leaveDates.Add(curdate);
                                    evenLength = evenLength + 1;
                                }
                            }
                            break;
                        default:
                            if (isCountAllTrip)
                            {
                                evenLength = 0;
                                for (DateTime temp = startDateTime; temp <= firstDate.AddDays(days - 1); temp = temp.AddDays(1))
                                {
                                    string curdate = temp.ToString("yyyy-MM-dd");
                                    if (allWorkDays.ContainsKey(curdate) && !leaveDates.Contains(curdate))
                                    {
                                        leaveDates.Add(curdate);
                                        evenLength = evenLength + 1;
                                    }
                                }
                            }
                            else
                            {
                                evenLength = 0;
                                for (DateTime temp = startDateTime; temp <= firstDate.AddDays(days - 1); temp = temp.AddDays(1))
                                {
                                    string curdate = temp.ToString("yyyy-MM-dd");
                                    if (!leaveDates.Contains(curdate))
                                    {
                                        leaveDates.Add(curdate);
                                        evenLength = evenLength + 1;
                                    }
                                }
                            }
                            break;
                    }
                    #endregion
                    break;
                case 3:
                    #region 日期范围开始时间不在(小于)统计月,结束时间在统计月
                    startDay = "01";
                    switch (entity.ATEventSubTypeID)
                    {
                        case ATTypeId.事假:
                        case ATTypeId.病假:
                        case ATTypeId.年假:
                            evenLength = 0;
                            for (DateTime temp = firstDate; temp <= endDateTime; temp = temp.AddDays(1))
                            {
                                string curdate = temp.ToString("yyyy-MM-dd");
                                if (allWorkDays.ContainsKey(curdate) && !leaveDates.Contains(curdate))
                                {
                                    leaveDates.Add(curdate);
                                    evenLength = evenLength + 1;
                                }
                            }
                            break;
                        default:
                            if (isCountAllTrip)
                            {
                                evenLength = 0;
                                for (DateTime temp = firstDate; temp <= endDateTime; temp = temp.AddDays(1))
                                {
                                    string curdate = temp.ToString("yyyy-MM-dd");
                                    if (allWorkDays.ContainsKey(curdate) && !leaveDates.Contains(curdate))
                                    {
                                        leaveDates.Add(curdate);
                                        evenLength = evenLength + 1;
                                    }
                                }
                            }
                            else
                            {
                                evenLength = 0;
                                for (DateTime temp = firstDate; temp <= endDateTime; temp = temp.AddDays(1))
                                {
                                    string curdate = temp.ToString("yyyy-MM-dd");
                                    if (!leaveDates.Contains(curdate))
                                    {
                                        leaveDates.Add(curdate);
                                        evenLength = evenLength + 1;
                                    }
                                }
                            }
                            break;
                    }
                    #endregion
                    break;
                case 4:
                    switch (entity.ATEventSubTypeID)
                    {
                        case ATTypeId.事假:
                            foreach (var item in allWorkDays)
                            {
                                if (!leaveDates.Contains(item.Key))
                                {
                                    leaveDates.Add(item.Key);
                                }
                            }
                            evenLength = wagesDays;
                            break;
                        case ATTypeId.病假:
                            foreach (var item in allWorkDays)
                            {
                                if (!leaveDates.Contains(item.Key))
                                {
                                    leaveDates.Add(item.Key);
                                }
                            }
                            evenLength = wagesDays;
                            break;
                        case ATTypeId.年假:
                            foreach (var item in allWorkDays)
                            {
                                if (!leaveDates.Contains(item.Key))
                                {
                                    leaveDates.Add(item.Key);
                                }
                            }
                            evenLength = 15;
                            break;
                        case ATTypeId.P出差:
                            if (isCountAllTrip)
                            {
                                evenLength = wagesDays;
                                foreach (var item in allWorkDays)
                                {
                                    if (!leaveDates.Contains(item.Key))
                                    {
                                        leaveDates.Add(item.Key);
                                    }
                                }
                            }
                            else
                            {
                                foreach (var item in allDates)
                                {
                                    if (!leaveDates.Contains(item.Key))
                                    {
                                        leaveDates.Add(item.Key);
                                    }
                                }
                                evenLength = days;
                            }
                            break;
                        default:
                            foreach (var item in allDates)
                            {
                                if (!leaveDates.Contains(item.Key))
                                {
                                    leaveDates.Add(item.Key);
                                }
                            }
                            evenLength = days;
                            break;
                    }
                    break;
                default:
                    break;
            }
            switch (entity.ATEventSubTypeID)
            {
                case ATTypeId.年假:
                    evenLength = evenLength > 15 ? 15 : evenLength;
                    break;
                default:
                    break;
            }
            return evenLength;
        }
        /// <summary>
        /// 导出并保存获取到公司所有员工的考勤统计信息为Excel文件
        /// </summary>
        /// <param name="monthCode"></param>
        /// <param name="wagesDays"></param>
        /// <param name="punch"></param>
        public FileStream GetExportExcelOfAllMonthLogCount(string monthCode, int wagesDays, int punch, string fileName)
        {
            FileStream fileStream = null;
            //Common.Log.Log.Debug("GetAllMonthLogCount开始:");
            DataTable dtSource = null;
            //公司的所有员工月考勤统计信息
            IList<ATEmpMonthLogCount> atemCount = new List<ATEmpMonthLogCount>();

            atemCount = this.GetAllMonthLogCountList(monthCode, wagesDays, punch);
            dtSource = ExportDTtoExcelHelp.ExportExcelOfAllMonthLogToDataTable<ATEmpMonthLogCount>(atemCount);
            //Common.Log.Log.Debug("GetAllMonthLogCount获取的DataTable记录行数:" + dtSource.Rows.Count);
            string strHeaderText = monthCode + "公司员工考勤统计";
            string filePath = "", basePath = "";

            //basePath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ExcelExportSavePath").Trim();
            //一级保存路径
            basePath = (string)IBBParameter.GetCache(BParameterParaNo.ExcelExportSavePath.ToString());
            //Common.Log.Log.Debug("Excel文件导出后保存路径(需要为物理路径)为:" + basePath);
            if (String.IsNullOrEmpty(basePath))
            {
                basePath = "ExcelExport";
            }
            monthCode = monthCode.Replace("/", "-");
            monthCode = monthCode.Trim();
            //AllMonthLogCount为二级保存路径,作分类用
            basePath = basePath + "\\" + "AllMonthLogCount\\";
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);
            filePath = basePath + fileName;
            //Common.Log.Log.Debug("Excel文件导出保存路径及名称为:" + filePath);
            try
            {
                bool result = MyNPOIHelper.ExportDTtoExcel(dtSource, strHeaderText, filePath);
                if (result)
                {
                    fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                }
            }
            catch (Exception ee)
            {
                Common.Log.Log.Debug("Excel文件导出失败:" + ee.Message);
                throw ee;
            }
            return fileStream;
        }

        #endregion

        #region 员工考勤明细
        /// <summary>
        /// 获取员工考勤统计清单信息(不包含打卡签到签退)
        /// </summary>
        /// /// <param name="searchType">查询类型</param>
        /// <param name="attypeId">考勤事件类型Id</param>
        /// <param name="deptId">部门id</param>
        /// <param name="isGetSubDept">是否获取子部门的员工信息</param>
        /// <param name="empId">员工id字符串,如123,232,1233</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public EntityList<ATEmpApplyAllLog> GetATEmpAttendanceEventLogDetailList(long searchType, string attypeId, string deptId, bool isGetSubDept, string empId, string startDate, string endDate, long approveStatusID, int page, int limit, string sort)
        {
            IList<ATEmpApplyAllLog> atemCount = new List<ATEmpApplyAllLog>();
            EntityList<ATEmpApplyAllLog> tempEntityList = new EntityList<ATEmpApplyAllLog>();
            //部门员工处理
            string empidlist = GetEmpIdByDeptId(deptId, isGetSubDept, empId, false);
            StringBuilder strbHql = new StringBuilder();

            strbHql.Append(" ATEventTypeID=" + searchType + " ");
            //请假类型明细
            if (!String.IsNullOrEmpty(attypeId))
            {
                strbHql.Append(" and ATEventSubTypeID in (" + attypeId + ")");//
            }
            //审批状态
            if (approveStatusID > 0)
            {
                strbHql.Append(" and ApproveStatusID=" + approveStatusID);
            }

            if (!String.IsNullOrEmpty(empidlist))
            {
                strbHql.Append(" and ApplyID in(" + empidlist + ")");
            }
            if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
            {
                strbHql.Append("  and (StartDateTime<='" + endDate + "  23:59:59'  and EndDateTime>='" + startDate + "' )");
            }
            var tempList = DBDao.GetListByHQL(strbHql.ToString(), page, limit);
            tempEntityList.count = tempList.count;

            //获取员工帐户信息
            IList<RBACUser> rbacuserList = new List<RBACUser>();
            IList<ATEmpAttendanceEventLog> ApproveList = tempList.list;
            //部门及员工都没有传入时,需要从结果集里过滤取出员工id信息
            if (String.IsNullOrEmpty(empidlist))
            {
                //申请人Id滤重
                empidlist = GetEmpIdStr(ApproveList);
            }
            if (ApproveList != null && ApproveList.Count > 0)
            {
                if (!String.IsNullOrEmpty(empidlist))
                    rbacuserList = IDRBACUserDao.GetListByHQL("rbacuser.HREmployee.Id in(" + empidlist + ")");

                DateTime dtStart = DateTime.Parse(startDate);
                DateTime dtEnd = DateTime.Parse(endDate);

                DateTime firstDate = Convert.ToDateTime(dtStart.Year + "-" + dtStart.Month + "-01");
                TimeSpan ts = dtEnd - dtStart;
                int days = ts.Days;
                //获取日期的集合
                Dictionary<string, DayOfWeek> allDates = IBATHolidaySetting.GetSomeDatesOfDates(dtStart, dtEnd);

                switch (searchType)
                {
                    case ATTypeId.P请假:
                        var LeaveList = ApproveList.Where(s => s.ATEventTypeID == ATTypeId.P请假 && s.StartDateTime <= dtEnd.AddHours(23).AddMinutes(59).AddSeconds(59) && s.EndDateTime >= dtStart);
                        #region LeaveList请假
                        if (LeaveList.Count() > 0)
                        {
                            atemCount = GetATEmpApplyAllLogList(LeaveList, rbacuserList);
                        }
                        #endregion

                        break;
                    case ATTypeId.P外出:
                        #region EgressList外出
                        var EgressList = ApproveList.Where(s => s.ATEventTypeID == ATTypeId.P外出);
                        if (EgressList.Count() > 0)
                        {
                            atemCount = GetEgressList(EgressList, rbacuserList);
                        }
                        #endregion

                        break;
                    case ATTypeId.P出差:
                        #region TripList出差
                        var TripList = ApproveList.Where(s => s.ATEventTypeID == ATTypeId.P出差);
                        if (TripList.Count() > 0)
                        {
                            atemCount = GetATEmpApplyAllLogList(TripList, rbacuserList);
                        }
                        #endregion
                        break;
                    case ATTypeId.P加班:
                        #region OvertimeList加班
                        var OvertimeList = ApproveList.Where(s => s.ATEventTypeID == ATTypeId.P加班);
                        if (OvertimeList.Count() > 0)
                        {
                            atemCount = GetOvertimeList(OvertimeList, rbacuserList);
                        }
                        #endregion
                        break;
                    default:
                        break;
                }
            }
            if (atemCount.Count > 0)
            {
                atemCount = atemCount.OrderByDescending(s => s.DataAddTime).ThenBy(s => s.ATEventSubTypeID).ToList();
                //分页处理
                if (limit < atemCount.Count)
                {
                    int startIndex = limit * (page - 1);
                    int endIndex = limit;
                    var list = atemCount.Skip(startIndex).Take(endIndex);
                    if (list != null)
                    {
                        list = list.OrderByDescending(s => s.ATEventDateCode).ThenBy(s => s.HRDeptCName);
                        tempEntityList.list = list.ToList();
                    }
                }
                else
                {
                    tempEntityList.list = atemCount;
                }
                tempEntityList.count = atemCount.Count;
            }
            else
            {
                tempEntityList.list = new List<ATEmpApplyAllLog>();
                tempEntityList.count = 0;
            }
            return tempEntityList;
        }

        /// <summary>
        /// 统计时处理请假及出差用
        /// </summary>
        /// <param name="rbacuserList"></param>
        /// <param name="LeaveList"></param>
        /// <returns></returns>
        private IList<ATEmpApplyAllLog> GetATEmpApplyAllLogList(IEnumerable<ATEmpAttendanceEventLog> LeaveList, IList<RBACUser> rbacuserList)
        {
            IList<ATEmpApplyAllLog> atemCount = new List<ATEmpApplyAllLog>();
            ATEmpApplyAllLog defaultApplyAllLog = null;
            for (int j = 0; j < LeaveList.Count(); j++)
            {
                long? applyId = LeaveList.ElementAt(j).ApplyID;
                defaultApplyAllLog = GetDafaultATEmpApplyAllLog(DateTime.Parse(LeaveList.ElementAt(0).StartDateTime.Value.ToString("yyyy-MM-dd")), LeaveList.ElementAt(j));
                defaultApplyAllLog.ATEventDateCode = LeaveList.ElementAt(j).ATEventDateCode;
                defaultApplyAllLog.EventStatPostion = LeaveList.ElementAt(j).EventStatPostion;
                defaultApplyAllLog.EventDestinationPostion = LeaveList.ElementAt(j).EventDestinationPostion;
                defaultApplyAllLog.TransportationName = LeaveList.ElementAt(j).TransportationName;
                //事假,病假,年假是按工作日处理,在统计时,只统计事假,病假,年假的请假日期范围内的工作日天数
                bool isWorkDays = false;
                if (LeaveList.ElementAt(j).ATEventTypeID == ATTypeId.P出差)
                {
                    isWorkDays = true;
                }
                if (isWorkDays == false && LeaveList.ElementAt(j).ATEventSubTypeID == ATTypeId.事假 || LeaveList.ElementAt(j).ATEventSubTypeID == ATTypeId.病假 || LeaveList.ElementAt(j).ATEventSubTypeID == ATTypeId.年假)
                {
                    isWorkDays = true;
                }
                if (isWorkDays)
                {
                    //获取某个日期范围内所有工作日期的集合
                    List<string> listDates = IBATHolidaySetting.GetAllWorkDaysOfDates(DateTime.Parse(LeaveList.ElementAt(j).StartDateTime.Value.ToString("yyyy-MM-dd")), DateTime.Parse(LeaveList.ElementAt(j).EndDateTime.Value.ToString("yyyy-MM-dd") + "  23:59:59"));
                    //天数以工作日为准
                    defaultApplyAllLog.EvenLength = listDates.Count();
                }
                else
                {
                    //天数
                    defaultApplyAllLog.EvenLength = Math.Round(LeaveList.ElementAt(j).EvenLength, 1);
                }
                defaultApplyAllLog.EvenLengthUnit = "天";
                defaultApplyAllLog.StartDateTime = LeaveList.ElementAt(j).StartDateTime.Value.ToString("yyyy-MM-dd");
                defaultApplyAllLog.EndDateTime = LeaveList.ElementAt(j).EndDateTime.Value.ToString("yyyy-MM-dd");
                defaultApplyAllLog.Memo = LeaveList.ElementAt(j).Memo;
                GetApplyOfRBACUser(rbacuserList, ref defaultApplyAllLog, applyId);
                atemCount.Add(defaultApplyAllLog);
            }
            return atemCount;
        }

        /// <summary>
        /// 导出并保存员工考勤统计清单信息为Excel文件(不包含打卡签到签退)
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="attypeId"></param>
        /// <param name="deptId"></param>
        /// <param name="isGetSubDept"></param>
        /// <param name="empId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="approveStatusID"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public FileStream GetExportExcelOfATEmpAttendanceEventLogDetail(long searchType, string attypeId, string deptId, bool isGetSubDept, string empId, string startDate, string endDate, long approveStatusID, ref string fileName)
        {
            FileStream fileStream = null;
            //员工考勤统计清单信息
            IList<ATEmpApplyAllLog> atemCount = new List<ATEmpApplyAllLog>();
            var tempEntityList = this.GetATEmpAttendanceEventLogDetailList(searchType, attypeId, deptId, isGetSubDept, empId, startDate, endDate, approveStatusID, 1, 100000, "");
            if (tempEntityList != null)
                atemCount = tempEntityList.list;
            if (atemCount != null && atemCount.Count > 0)
            {
                DataTable dtSource = null;
                dtSource = ExportDTtoExcelHelp.ExportExcelOfATEmpAttendanceEventLogDetailToDataTable<ATEmpApplyAllLog>(atemCount, searchType);
                //Common.Log.Log.Debug("GetAllMonthLogCount获取的DataTable记录行数:" + dtSource.Rows.Count);
                string strHeaderText = "员工考勤统计清单信息";
                switch (searchType)
                {
                    case ATTypeId.P请假:
                        fileName = "请假清单.xlsx";
                        strHeaderText = "员工考勤统计请假清单";
                        break;
                    case ATTypeId.P外出:
                        fileName = "外出清单.xlsx";
                        strHeaderText = "员工考勤统计外出清单";
                        break;
                    case ATTypeId.P出差:
                        fileName = "出差清单.xlsx";
                        strHeaderText = "员工考勤统计出差清单";
                        break;
                    case ATTypeId.P加班:
                        fileName = "加班清单.xlsx";
                        strHeaderText = "员工考勤统计加班清单";
                        break;
                    default:
                        break;
                }
                string filePath = "", basePath = "";
                //一级保存路径
                basePath = (string)IBBParameter.GetCache(BParameterParaNo.ExcelExportSavePath.ToString());
                //Common.Log.Log.Debug("Excel文件导出后保存路径(需要为物理路径)为:" + basePath);
                if (String.IsNullOrEmpty(basePath))
                {
                    basePath = "ExcelExport";
                }
                //ATEmpAttendanceEventLogDetail为二级保存路径,作分类用
                basePath = basePath + "\\" + "ATEmpAttendanceEventLogDetail\\";
                filePath = basePath + DateTime.Now.ToString("yyMMddhhmmss") + fileName;
                //Common.Log.Log.Debug("Excel文件导出保存路径及名称为:" + filePath);
                try
                {
                    if (!Directory.Exists(basePath))
                        Directory.CreateDirectory(basePath);
                    //单元格字体颜色的处理
                    Dictionary<string, short> cellFontStyleList = new Dictionary<string, short>();
                    //cellFontStyleList.Add("签到迟到", NPOI.HSSF.Util.HSSFColor.Red.Index);
                    //cellFontStyleList.Add("签退早退", NPOI.HSSF.Util.HSSFColor.Red.Index);
                    cellFontStyleList.Add("未打卡", NPOI.HSSF.Util.HSSFColor.Red.Index);
                    cellFontStyleList.Add("补签卡", NPOI.HSSF.Util.HSSFColor.Green.Index);

                    fileStream = NPOIExportDTtoExcel.ExportDTtoExcellHelp(dtSource, strHeaderText, filePath, cellFontStyleList);
                    //bool result = MyNPOIHelper.ExportDTtoExcel(dtSource, strHeaderText, filePath);
                    if (fileStream != null)
                    {
                        fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    }
                }
                catch (Exception ee)
                {
                    Common.Log.Log.Error("员工考勤统计清单信息导出失败:" + ee.Message);
                    throw ee;
                }
            }
            return fileStream;
        }
        /// <summary>
        /// 获取员工考勤统计打卡清单信息
        /// </summary>
        /// <param name="filterType">过滤类型,如1,2(1:包含打卡且考勤正常的人;2:包含打卡但有异常(迟到、早退、旷工)的人)</param>
        /// <param name="deptId">部门id</param>
        /// <param name="isGetSubDept">是否获取子部门的员工信息</param>
        /// <param name="empId">员工id字符串,如123,232,1233</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public EntityList<SignInfoExport> GetATEmpSignInfoDetailList(string filterType, string deptId, bool isGetSubDept, string empId, string startDate, string endDate, int page, int limit, string sort)
        {
            //ZhiFang.Common.Log.Log.Debug("获取员工考勤统计打卡清单信息开始:" + DateTime.Now.ToString());
            IList<SignInfoExport> atemCount = new List<SignInfoExport>();
            EntityList<SignInfoExport> tempEntityList = new EntityList<SignInfoExport>();
            bool normal = true, abnormal = true, disable = false;
            StringBuilder signInfoHql = new StringBuilder();

            //获取符合统计条件的员工
            string empidlist = GetEmpIdByDeptId(deptId, isGetSubDept, empId, disable);
            if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
                signInfoHql.Append("(DataAddTime<='" + endDate + "  23:59:59'  and DataAddTime>='" + startDate + "')");
            if (!String.IsNullOrEmpty(empidlist))
                signInfoHql.Append(" and ApplyID in(" + empidlist.ToString() + ")");

            if (!String.IsNullOrEmpty(filterType))
            {
                StringBuilder ateventTypeIDHql = new StringBuilder();
                string[] tempArr = filterType.Trim().Split(',');
                if (tempArr.Contains("1"))
                {
                    ateventTypeIDHql.Append("or (ATEventSubTypeID=" + ATTypeId.P签到 + " or ATEventSubTypeID=" + ATTypeId.P签退 + ") ");
                }
                else
                {
                    normal = false;
                }
                if (tempArr.Contains("2"))
                {
                    ateventTypeIDHql.Append("or (ATEventSubTypeID=" + ATTypeId.迟到 + " or ATEventSubTypeID=" + ATTypeId.早退 + ") ");
                }
                else
                {
                    abnormal = false;
                }
                if (ateventTypeIDHql.ToString().Length > 0)
                {
                    signInfoHql.Append(" and (" + ateventTypeIDHql.ToString().Trim().Substring(2).Trim() + ")");
                }
            }
            string attypeId = ATTypeId.P签到 + "," + ATTypeId.P签退;
            signInfoHql.Append(" and ATEventTypeID in (" + attypeId + ")");
            //ZhiFang.Common.Log.Log.Debug("signInfoHql:" + signInfoHql.ToString().Substring(2));
            //签到签退所有记录
            var signInfoEntityList = DBDao.GetListByHQL(signInfoHql.ToString() + "  ", " DataAddTime ", 0, 0);
            IList<ATEmpAttendanceEventLog> signInfoList = new List<ATEmpAttendanceEventLog>();
            if (signInfoEntityList != null && signInfoEntityList.list != null)
            {
                signInfoList = signInfoEntityList.list;
            }
            //补签卡记录
            StringBuilder strbHql = new StringBuilder();
            strbHql.Append(" (ATEventSubTypeID=" + ATTypeId.补签卡 + ")");
            if (!String.IsNullOrEmpty(empidlist))
            {
                strbHql.Append(" and ApplyID in (" + empidlist.ToString() + ")");
            }
            if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
            {
                strbHql.Append("  and (StartDateTime<='" + endDate + "  23:59:59'  and EndDateTime>='" + startDate + "')");
            }
            var fillCardsLeaveList = DBDao.GetListByHQL(strbHql.ToString());

            //包含打卡但有异常的旷工处理需要用的请假数据(请假及出差,外出,调休)
            IList<ATEmpAttendanceEventLog> leaveAndTripEgressList = new List<ATEmpAttendanceEventLog>();
            if (abnormal == true)
            {
                StringBuilder strbLeaveHql = new StringBuilder();
                strbLeaveHql.Append(" (ATEventTypeID in(" + ATTypeId.P请假 + "," + ATTypeId.P出差 + "," + ATTypeId.P外出 + ") and ATEventSubTypeID!=" + ATTypeId.补签卡 + ")");
                if (!String.IsNullOrEmpty(empidlist))
                {
                    strbLeaveHql.Append(" and ApplyID in (" + empidlist.ToString() + ")");
                }
                if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
                {
                    strbLeaveHql.Append("  and (StartDateTime<='" + endDate + "  23:59:59'  and EndDateTime>='" + startDate + "')");
                }
                var tempLeaveList = DBDao.GetListByHQL(strbLeaveHql.ToString());
                if (tempLeaveList != null)
                {
                    leaveAndTripEgressList = tempLeaveList.ToList();
                }
            }

            var ApproveList = signInfoList.Concat(fillCardsLeaveList);
            //申请人Id滤重
            if (String.IsNullOrEmpty(empidlist) && ApproveList.Count() > 0)
            {
                empidlist = GetEmpIdStr(ApproveList.ToList());
            }

            //获取员工帐户信息
            IList<RBACUser> rbacuserList = new List<RBACUser>();
            if (!String.IsNullOrEmpty(empidlist) && ApproveList != null && ApproveList.Count() > 0)
            {
                rbacuserList = IDRBACUserDao.GetListByHQL("rbacuser.HREmployee.Id in(" + empidlist.ToString() + ")");
                DateTime dtStart = DateTime.Parse(startDate);
                DateTime dtEnd = DateTime.Parse(endDate + "  23:59:59");
                //获取统计日期范围内所有工作日期的集合
                List<string> listWorkDates = IBATHolidaySetting.GetAllWorkDaysOfDates(dtStart, dtEnd);
                listWorkDates = listWorkDates.OrderByDescending(s => s.ToString()).ToList();

                //获取统计日期范围内所有日期的集合
                List<string> listAllDates = this.GetAllDayStr(dtStart, dtEnd, new List<string>());
                listAllDates = listAllDates.OrderByDescending(s => s.ToString()).ToList();

                try
                {
                    #region 遍历统计员工在统计范围日期的打卡处理
                    var empidArr = empidlist.Split(',');
                    IList<ATEmpAttendanceEventLog> empFillCardsLeave = new List<ATEmpAttendanceEventLog>();
                    //该员工的工作日旷工的集合(可能包含请假,出差,外出的工作日)
                    IList<string> empAbsenteeismDates = new List<string>();
                    string[] empAbsenteeismDateStr = new string[] { };

                    //该员工的统计日期范围内的补签卡集合
                    IList<string> empFillCardsDates = new List<string>();
                    string[] empFillCardsDatesStr = new string[] { };

                    foreach (var applyID in empidArr)
                    {
                        empAbsenteeismDates.Clear();
                        empAbsenteeismDateStr = listWorkDates.ToArray();
                        empAbsenteeismDates = empAbsenteeismDateStr.ToList();

                        empFillCardsDates.Clear();
                        empFillCardsDatesStr = listAllDates.ToArray();
                        empFillCardsDates = empFillCardsDatesStr.ToList();

                        var tempFillCardsLeave = fillCardsLeaveList.Where(s => s.ApplyID == long.Parse(applyID) && s.ATEventSubTypeID == ATTypeId.补签卡).ToList();
                        empFillCardsLeave.Clear();
                        if (tempFillCardsLeave != null)
                            empFillCardsLeave = tempFillCardsLeave.ToList();
                        #region 员工某一天的打卡数据
                        var tempempsigninfolist = signInfoList.Where(s => s.ApplyID == long.Parse(applyID));
                        var empsigninfogroupdate = tempempsigninfolist.GroupBy(a => a.ATEventDateCode);
                        foreach (var signinfo in empsigninfogroupdate)
                        {
                            //tmpdate.Remove(Convert.ToDateTime(signinfo.Key));
                            SignInfoExport signinfoexp = CreateSignInfoExport(rbacuserList, applyID);
                            signinfoexp.ATEventDateCode = signinfo.Key;
                            signinfoexp.WeekInfo = DateTimeHelp.week[Convert.ToInt32(Convert.ToDateTime(signinfo.Key).DayOfWeek)];
                            signinfoexp.EmpId = applyID;

                            for (int i = 0; i < signinfo.Count(); i++)
                            {
                                if (signinfo.ElementAt(i).ATEventTypeID == ATTypeId.P签到)
                                {
                                    signinfoexp.EmpName = signinfo.ElementAt(i).ApplyName;
                                    signinfoexp.SignInId = signinfo.ElementAt(i).Id.ToString();
                                    signinfoexp.SignInTime = signinfo.ElementAt(i).DataAddTime.Value.ToString("HH:mm");
                                    signinfoexp.SigninATEventLogPostionName = signinfo.ElementAt(i).ATEventLogPostionName;
                                    signinfoexp.SignInMemo = signinfo.ElementAt(i).Memo;
                                    signinfoexp.SignInType = signinfo.ElementAt(i).ATEventSubTypeName;
                                    signinfoexp.SignInSubTypeID = signinfo.ElementAt(i).ATEventSubTypeID;
                                    //signinfoexp.SignInIsOffsite = signinfo.ElementAt(i).IsOffsite;
                                }
                                if (signinfo.ElementAt(i).ATEventTypeID == ATTypeId.P签退)
                                {
                                    signinfoexp.EmpName = signinfo.ElementAt(i).ApplyName;
                                    signinfoexp.SignOutId = signinfo.ElementAt(i).Id.ToString();
                                    signinfoexp.SignOutTime = signinfo.ElementAt(i).DataAddTime.Value.ToString("HH:mm");
                                    signinfoexp.SignoutATEventLogPostionName = signinfo.ElementAt(i).ATEventLogPostionName;
                                    signinfoexp.SignOutMemo = signinfo.ElementAt(i).Memo;
                                    signinfoexp.SignOutType = signinfo.ElementAt(i).ATEventSubTypeName;
                                    signinfoexp.SignOutSubTypeID = signinfo.ElementAt(i).ATEventSubTypeID;
                                    //signinfoexp.SignOutIsOffsite = signinfo.ElementAt(i).IsOffsite;
                                }

                                if (String.IsNullOrEmpty(signinfoexp.SignInId))
                                {
                                    signinfoexp.SignInType = "未打卡";
                                    signinfoexp.SignInTime = "未打卡";
                                }
                                if (String.IsNullOrEmpty(signinfoexp.SignOutId))
                                {
                                    signinfoexp.SignOutType = "未打卡";
                                    signinfoexp.SignOutTime = "未打卡";
                                }
                            }
                            bool isExecAdd = false;
                            //包含打卡正常
                            if (normal == true && signinfoexp.SignInSubTypeID == ATTypeId.P签到 && signinfoexp.SignOutSubTypeID == ATTypeId.P签退)
                            {
                                isExecAdd = true;
                            }
                            //包含打卡但异常(迟到、早退、旷工)
                            if (isExecAdd == false && abnormal == true)
                            {
                                if (signinfoexp.SignInSubTypeID == ATTypeId.迟到 || signinfoexp.SignInSubTypeID == 0)
                                    isExecAdd = true;
                                if (signinfoexp.SignOutSubTypeID == ATTypeId.早退 || signinfoexp.SignOutSubTypeID == 0)
                                    isExecAdd = true;
                            }
                            if (isExecAdd)
                            {
                                //签到及签退都没打卡为旷工
                                if (signinfoexp.SignInTime == "未打卡" && signinfoexp.SignOutTime == "未打卡")
                                {
                                    signinfoexp.SignInType = "旷工";
                                    signinfoexp.SignOutType = "旷工";
                                    signinfoexp.SignInTime = "旷工"; ;
                                    signinfoexp.SignOutTime = "旷工";
                                }
                                if (!String.IsNullOrEmpty(signinfoexp.Account))
                                    atemCount.Add(signinfoexp);
                                //有签到或签退记录后,旷工日期集合需要减去该日期
                                empAbsenteeismDates.Remove(signinfoexp.ATEventDateCode);
                                //有签到和签退记录后,补签卡日期集合需要减去该日期
                                if (!String.IsNullOrEmpty(signinfoexp.SignInId) && !String.IsNullOrEmpty(signinfoexp.SignOutId))
                                    empFillCardsDates.Remove(signinfoexp.ATEventDateCode);
                            }
                        }
                        #endregion
                        #region 员工补签卡日期处理
                        if (empFillCardsLeave != null && empFillCardsLeave.Count() > 0 && empFillCardsDates.Count > 0)
                        {
                            //Stopwatch watch = new Stopwatch();//实例化一个计时器
                            //watch.Reset();
                            //watch.Start();
                            foreach (var date in empFillCardsDates)
                            {
                                foreach (var fillCard in empFillCardsLeave)
                                {
                                    DateTime tempStart = DateTime.Parse(fillCard.StartDateTime.Value.ToString("yyyy-MM-dd"));
                                    DateTime tempEnd = DateTime.Parse(fillCard.EndDateTime.Value.ToString("yyyy-MM-dd"));
                                    if (IsInDate(DateTime.Parse(date), tempStart, tempEnd))
                                    {
                                        SignInfoExport signinfo = CreateSignInfoExport(rbacuserList, applyID);
                                        signinfo.ATEventDateCode = date;
                                        signinfo.WeekInfo = DateTimeHelp.week[Convert.ToInt32(DateTime.Parse(date).DayOfWeek)];
                                        signinfo.EmpName = fillCard.ApplyName;
                                        if (fillCard.ApplyID.HasValue)
                                            signinfo.EmpId = fillCard.ApplyID.Value.ToString();

                                        if (String.IsNullOrEmpty(signinfo.SignInTime))
                                            signinfo.SignInTime = "补签卡";
                                        if (String.IsNullOrEmpty(signinfo.SignOutTime))
                                            signinfo.SignOutTime = "补签卡";
                                        signinfo.SignInType = "补签卡";
                                        signinfo.SignOutType = "补签卡";
                                        //有签到或签退记录后,旷工日期集合需要减去该日期
                                        empAbsenteeismDates.Remove(signinfo.ATEventDateCode);

                                        //是否已经存在集合中(只有签到或签退的情况)
                                        var tempList = atemCount.Where(s => s.EmpId == applyID && s.ATEventDateCode == date);
                                        if (tempList == null || tempList.Count() == 0)
                                        {
                                            atemCount.Add(signinfo);
                                        }
                                        else if (tempList != null && tempList.Count() == 1)
                                        {
                                            int indexOf = atemCount.IndexOf(tempList.ElementAt(0));
                                            atemCount[indexOf].SignInType = "补签卡";
                                            atemCount[indexOf].SignOutType = "补签卡";
                                            if (String.IsNullOrEmpty(atemCount[indexOf].SignInId))
                                            {
                                                atemCount[indexOf].SignInTime = "补签卡";
                                            }
                                            if (String.IsNullOrEmpty(atemCount[indexOf].SignOutId))
                                            {
                                                atemCount[indexOf].SignOutTime = "补签卡";
                                            }
                                        }
                                    }
                                }
                            }
                            //watch.Stop();//结束计时
                            //string time = watch.ElapsedMilliseconds.ToString();
                            //ZhiFang.Common.Log.Log.Debug("Empid:" + applyID + ",补签卡处理时间:" + time);
                        }
                        #endregion
                        #region 包含打卡但异常的人--工作日旷工处理
                        if (abnormal == true)
                        {
                            #region 该员工工作日旷工处理
                            //Stopwatch watch = new Stopwatch();//实例化一个计时器
                            //watch.Reset();
                            //watch.Start();
                            #region 未打卡的工作日是不是已经请假,出差,外出的处理
                            var tempLeave = leaveAndTripEgressList.Where(s => s.ApplyID == long.Parse(applyID));
                            if (tempLeave != null && tempLeave.Count() > 0)
                            {
                                foreach (var leaveAndTripEgress in tempLeave)
                                {
                                    DateTime tempStart = DateTime.Parse(leaveAndTripEgress.StartDateTime.Value.ToString("yyyy-MM-dd"));
                                    DateTime tempEnd = DateTime.Parse(leaveAndTripEgress.EndDateTime.Value.ToString("yyyy-MM-dd") + "  23:59:59");
                                    if (tempEnd.CompareTo(dtEnd) > 0)
                                        tempEnd = dtEnd;

                                    DateTime dtDay = tempStart;
                                    for (dtDay = tempStart; dtDay.CompareTo(tempEnd) <= 0; dtDay = dtDay.AddDays(1))
                                    {
                                        string curdate = dtDay.ToString("yyyy-MM-dd");
                                        if (listWorkDates.Contains(curdate))
                                        {
                                            //有考勤记录后,旷工日期集合需要减去该日期
                                            empAbsenteeismDates.Remove(curdate);
                                            SignInfoExport signinfo = CreateSignInfoExport(rbacuserList, applyID);
                                            switch (leaveAndTripEgress.ATEventTypeID)
                                            {
                                                case ATTypeId.P外出:
                                                    signinfo.SignInTime = leaveAndTripEgress.StartDateTime.Value.ToString("MM-dd HH:mm");
                                                    signinfo.SignOutTime = leaveAndTripEgress.EndDateTime.Value.ToString("MM-dd HH:mm");
                                                    break;
                                                default:
                                                    signinfo.SignInTime = leaveAndTripEgress.StartDateTime.Value.ToString("yyyy.MM.dd");
                                                    signinfo.SignOutTime = leaveAndTripEgress.EndDateTime.Value.ToString("yyyy.MM.dd");
                                                    break;
                                            }
                                            signinfo.SignInType = leaveAndTripEgress.ATEventSubTypeName;
                                            signinfo.SignOutType = leaveAndTripEgress.ATEventSubTypeName;
                                            signinfo.WeekInfo = DateTimeHelp.week[Convert.ToInt32(DateTime.Parse(curdate).DayOfWeek)];
                                            signinfo.ATEventDateCode = curdate;
                                            //是否已经存在集合中
                                            var tempList = atemCount.Where(s => s.EmpId == applyID && s.ATEventDateCode == curdate);
                                            if (tempList == null || tempList.Count() == 0)
                                            {
                                                if (!String.IsNullOrEmpty(signinfo.Account))
                                                    atemCount.Add(signinfo);
                                            }
                                            else if (tempList != null && tempList.Count() == 1)
                                            {
                                                int indexOf = atemCount.IndexOf(tempList.ElementAt(0));
                                                atemCount[indexOf].SignInType = signinfo.SignInType;
                                                atemCount[indexOf].SignOutType = signinfo.SignOutType;
                                                atemCount[indexOf].SignInTime = signinfo.SignInTime;
                                                atemCount[indexOf].SignOutTime = signinfo.SignOutTime;
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region 员工的工作日旷工处理
                            if (empAbsenteeismDates.Count > 0)
                            {
                                foreach (var date in empAbsenteeismDates)
                                {
                                    SignInfoExport signinfo = CreateSignInfoExport(rbacuserList, applyID);
                                    signinfo.SignInTime = "旷工";
                                    signinfo.SignInType = "旷工";
                                    signinfo.SignOutTime = "旷工";
                                    signinfo.SignOutType = "旷工";
                                    signinfo.WeekInfo = DateTimeHelp.week[Convert.ToInt32(DateTime.Parse(date).DayOfWeek)];
                                    signinfo.ATEventDateCode = date;
                                    if (!String.IsNullOrEmpty(signinfo.Account))
                                        atemCount.Add(signinfo);
                                }
                            }
                            #endregion

                            //watch.Stop();//结束计时
                            //string time = watch.ElapsedMilliseconds.ToString();
                            //ZhiFang.Common.Log.Log.Debug("Empid:" + applyID + ",工作日旷工处理时间:" + time);
                            #endregion
                        }
                        #endregion
                    }
                    #endregion
                }
                catch (Exception ee)
                {
                    ZhiFang.Common.Log.Log.Error("处理获取员工考勤统计打卡清单信息出错:" + ee.StackTrace);
                    throw ee;
                }
            }
            if (atemCount.Count > 0)
            {
                atemCount = atemCount.OrderByDescending(s => s.ATEventDateCode).ThenBy(s => s.HRDeptCName).ToList();
                //分页处理
                if (limit < atemCount.Count)
                {
                    int startIndex = limit * (page - 1);
                    int endIndex = limit;
                    var list = atemCount.Skip(startIndex).Take(endIndex);
                    if (list != null)
                    {
                        tempEntityList.list = list.ToList();
                    }
                }
                else
                {
                    tempEntityList.list = atemCount;
                }
                tempEntityList.count = atemCount.Count;
            }
            else
            {
                tempEntityList.list = new List<SignInfoExport>();
                tempEntityList.count = 0;
            }
            ZhiFang.Common.Log.Log.Debug("获取员工考勤统计打卡清单信息结束,总记录数为:" + tempEntityList.count + "," + DateTime.Now.ToString());
            return tempEntityList;
        }
        /// <summary>
        /// 创建打卡清单entity
        /// </summary>
        /// <param name="rbacuserList"></param>
        /// <param name="applyID"></param>
        /// <returns></returns>
        private SignInfoExport CreateSignInfoExport(IList<RBACUser> rbacuserList, string applyID)
        {
            SignInfoExport signinfo = new SignInfoExport();
            if (rbacuserList != null)
            {
                var rbacuser = rbacuserList.Where(a => a.HREmployee.Id == long.Parse(applyID));
                if (rbacuser != null && rbacuser.Count() > 0)
                {
                    signinfo.Account = rbacuser.ElementAt(0).Account;
                    if (rbacuser.ElementAt(0).HREmployee != null)
                        signinfo.EmpName = rbacuser.ElementAt(0).HREmployee.CName;
                    if (rbacuser.ElementAt(0).HREmployee != null && rbacuser.ElementAt(0).HREmployee.HRDept != null)
                        signinfo.HRDeptCName = rbacuser.ElementAt(0).HREmployee.HRDept.CName;
                    if (rbacuser.ElementAt(0).HREmployee != null && rbacuser.ElementAt(0).HREmployee.HRPosition != null)
                        signinfo.HRPositionCName = rbacuser.ElementAt(0).HREmployee.HRPosition.CName;
                }
            }
            return signinfo;
        }

        /// <summary>
        /// 获取导出员工考勤统计打卡清单信息
        /// </summary>
        /// <param name="filterType">过滤类型,如1,2(1:包含打卡且考勤正常的人;2:包含打卡但有异常(迟到、早退、旷工)的人)</param>
        /// <param name="deptId">部门id</param>
        /// <param name="isGetSubDept">是否获取子部门的员工信息</param>
        /// <param name="empId">员工id字符串,如123,232,1233</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public FileStream GetExportExcelOfATEmpSignInfoDetail(string filterType, string deptId, bool isGetSubDept, string empId, string startDate, string endDate, ref string fileName)
        {
            FileStream fileStream = null;
            //员工考勤统计清单信息
            IList<SignInfoExport> atemCount = new List<SignInfoExport>();
            var tempEntityList = this.GetATEmpSignInfoDetailList(filterType, deptId, isGetSubDept, empId, startDate, endDate, 1, 100000, "");
            if (tempEntityList != null)
            {
                atemCount = tempEntityList.list;
            }
            if (atemCount != null && atemCount.Count > 0)
            {
                DataTable dtSource = null;
                dtSource = ExportDTtoExcelHelp.ExportExcelOfATEmpSignInfoDetailToDataTable<SignInfoExport>(atemCount);
                string strHeaderText = "员工考勤统计打卡清单信息";
                fileName = "员工考勤统计打卡清单.xlsx";

                string filePath = "", basePath = "";
                //一级保存路径
                basePath = (string)IBBParameter.GetCache(BParameterParaNo.ExcelExportSavePath.ToString());
                if (String.IsNullOrEmpty(basePath))
                {
                    basePath = "ExcelExport";
                }
                //ATEmpSignInfoDetail为二级保存路径,作分类用
                basePath = basePath + "\\" + "ATEmpSignInfoDetail\\";
                filePath = basePath + DateTime.Now.ToString("yyMMddhhmmss") + fileName;
                try
                {
                    if (!Directory.Exists(basePath))
                        Directory.CreateDirectory(basePath);
                    //单元格字体颜色的处理
                    Dictionary<string, short> cellFontStyleList = new Dictionary<string, short>();
                    cellFontStyleList.Add("签到迟到", NPOI.HSSF.Util.HSSFColor.Red.Index);
                    cellFontStyleList.Add("签退早退", NPOI.HSSF.Util.HSSFColor.Red.Index);
                    cellFontStyleList.Add("未打卡", NPOI.HSSF.Util.HSSFColor.Red.Index);
                    cellFontStyleList.Add("旷工", NPOI.HSSF.Util.HSSFColor.Red.Index);
                    cellFontStyleList.Add("补签卡", NPOI.HSSF.Util.HSSFColor.Green.Index);

                    fileStream = NPOIExportDTtoExcel.ExportDTtoExcellHelp(dtSource, strHeaderText, filePath, cellFontStyleList);
                    if (fileStream != null)
                    {
                        fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    }
                }
                catch (Exception ee)
                {
                    Common.Log.Log.Error("员工考勤统计打卡清单导出失败:" + ee.Message);
                    throw ee;
                }
            }
            return fileStream;
        }
        #endregion

        #region 考勤类型单项处理
        private string GetEmpIdStr(IList<ATEmpAttendanceEventLog> ApproveList)
        {
            string empidStr = "";
            //var applyIDList = ApproveList.GroupBy(p => new { p.ApplyID }).Where(x => x.Count() > 1).ToList();
            var applyIDList = ApproveList.GroupBy(p => new { p.ApplyID }).ToList();
            if (applyIDList != null)
            {
                StringBuilder strb = new StringBuilder();
                foreach (var item in applyIDList)
                {
                    string idStr = item.Key.ApplyID.Value.ToString() + ",";
                    if (!strb.ToString().Contains(idStr))
                        strb.Append(idStr);
                }
                empidStr = strb.ToString().TrimEnd(',');
            }

            return empidStr;
        }

        /// <summary>
        /// 处理请假记录
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="days"></param>
        /// <param name="allDates"></param>
        /// <param name="LeaveList"></param>
        /// <returns></returns>
        private List<ATEmpApplyAllLog> GetLeaveList(DateTime firstDate, int days, Dictionary<string, DayOfWeek> allDates, IEnumerable<ATEmpAttendanceEventLog> LeaveList, IList<RBACUser> rbacuserList)
        {
            string curDate = "";
            DateTime tempDate = new DateTime();
            //当前日期范围内在统计月的请假日期集合
            IList<string> curLeaveDatesList = new List<string>();
            List<ATEmpApplyAllLog> atemCount = new List<ATEmpApplyAllLog>();
            ATEmpApplyAllLog defaultApplyAllLog = null;
            for (int j = 0; j < LeaveList.Count(); j++)
            {
                int leaveDatesType = GetLeaveDatesType(firstDate, allDates, LeaveList.ElementAt(j));
                long? applyId = LeaveList.ElementAt(j).ApplyID;
                defaultApplyAllLog = GetDafaultATEmpApplyAllLog(firstDate, LeaveList.ElementAt(j));
                defaultApplyAllLog.EvenLength = Math.Round(LeaveList.ElementAt(j).EvenLength, 1);
                defaultApplyAllLog.EvenLengthUnit = "天";
                switch (leaveDatesType)
                {
                    case 2:
                        defaultApplyAllLog.StartDateTime = LeaveList.ElementAt(j).StartDateTime.Value.ToString("yyyy-MM-dd");
                        defaultApplyAllLog.EndDateTime = firstDate.AddDays(days - 1).ToString("yyyy-MM-dd");
                        defaultApplyAllLog.Memo = LeaveList.ElementAt(j).Memo + "  实际请假开始时间:" + LeaveList.ElementAt(j).StartDateTime.Value.ToString("yyyy-MM-dd") + ",结束时间:" +
                        LeaveList.ElementAt(j).EndDateTime.Value.ToString("yyyy-MM-dd");

                        tempDate = DateTime.Parse(defaultApplyAllLog.StartDateTime);
                        for (int i = DateTime.Parse(defaultApplyAllLog.StartDateTime).Day; i <= firstDate.AddDays(days - 1).Day; i++)
                        {
                            curDate = tempDate.ToString("yyyy-MM-dd");
                            if (!curLeaveDatesList.Contains(curDate))
                            {
                                curLeaveDatesList.Add(curDate);
                                GetApplyOfRBACUser(rbacuserList, ref defaultApplyAllLog, applyId);
                                atemCount.Add(defaultApplyAllLog);
                            }
                            tempDate = tempDate.AddDays(1);
                        }
                        break;
                    case 3:
                        defaultApplyAllLog.EndDateTime = LeaveList.ElementAt(j).EndDateTime.Value.ToString("yyyy-MM-dd");
                        defaultApplyAllLog.Memo = LeaveList.ElementAt(j).Memo + "  实际请假开始时间:" + LeaveList.ElementAt(j).StartDateTime.Value.ToString("yyyy-MM-dd") + ",结束时间:" +
                        LeaveList.ElementAt(j).EndDateTime.Value.ToString("yyyy-MM-dd");
                        tempDate = firstDate;
                        for (int i = firstDate.Day; i <= DateTime.Parse(defaultApplyAllLog.EndDateTime).Day; i++)
                        {
                            curDate = tempDate.ToString("yyyy-MM-dd");
                            if (!curLeaveDatesList.Contains(curDate))
                            {
                                curLeaveDatesList.Add(curDate);
                                GetApplyOfRBACUser(rbacuserList, ref defaultApplyAllLog, applyId);
                                atemCount.Add(defaultApplyAllLog);
                            }
                            tempDate = tempDate.AddDays(1);
                        }
                        break;
                    case 4:
                        defaultApplyAllLog.EndDateTime = firstDate.AddDays(days - 1).ToString("yyyy-MM-dd");
                        defaultApplyAllLog.Memo = LeaveList.ElementAt(j).Memo + "  实际请假开始时间:" + LeaveList.ElementAt(j).StartDateTime.Value.ToString("yyyy-MM-dd") + ",结束时间:" +
                        LeaveList.ElementAt(j).EndDateTime.Value.ToString("yyyy-MM-dd");
                        tempDate = firstDate;
                        for (int i = firstDate.Day; i <= firstDate.AddDays(days - 1).Day; i++)
                        {
                            curDate = tempDate.ToString("yyyy-MM-dd");
                            if (!curLeaveDatesList.Contains(curDate))
                            {
                                curLeaveDatesList.Add(curDate);
                                GetApplyOfRBACUser(rbacuserList, ref defaultApplyAllLog, applyId);
                                atemCount.Add(defaultApplyAllLog);
                            }
                            tempDate = tempDate.AddDays(1);
                        }
                        break;
                    default:
                        #region 日期范围开始时间及结束时间都在统计月
                        defaultApplyAllLog.StartDateTime = LeaveList.ElementAt(j).StartDateTime.Value.ToString("yyyy-MM-dd");
                        defaultApplyAllLog.EndDateTime = LeaveList.ElementAt(j).EndDateTime.Value.ToString("yyyy-MM-dd");
                        tempDate = DateTime.Parse(LeaveList.ElementAt(j).StartDateTime.Value.ToString("yyyy-MM-dd"));
                        for (int i = DateTime.Parse(defaultApplyAllLog.StartDateTime).Day; i <= DateTime.Parse(defaultApplyAllLog.EndDateTime).Day; i++)
                        {
                            curDate = tempDate.ToString("yyyy-MM-dd");
                            //ZhiFang.Common.Log.Log.Debug("LeaveList.curDate:" + curDate);
                            if (!curLeaveDatesList.Contains(curDate))
                            {
                                curLeaveDatesList.Add(curDate);
                                GetApplyOfRBACUser(rbacuserList, ref defaultApplyAllLog, applyId);
                                atemCount.Add(defaultApplyAllLog);
                            }
                            tempDate = tempDate.AddDays(1);
                        }
                        #endregion
                        break;
                }
            }
            return atemCount;
        }
        private List<ATEmpApplyAllLog> GetEgressList(IEnumerable<ATEmpAttendanceEventLog> EgressList, IList<RBACUser> rbacuserList)
        {
            EgressList = EgressList.OrderBy(a => a.ApplyID).ThenBy(a => a.DataAddTime);
            List<ATEmpApplyAllLog> atemCount = new List<ATEmpApplyAllLog>();
            for (int i = 0; i < EgressList.Count(); i++)
            {
                ATEmpApplyAllLog atmyapplyalllog = new ATEmpApplyAllLog();
                atmyapplyalllog.DataAddTime = EgressList.ElementAt(i).DataAddTime.Value.ToString("yyyy-MM-dd HH:mm");
                atmyapplyalllog.ATEmpAttendanceEventLogId = EgressList.ElementAt(i).Id.ToString();
                atmyapplyalllog.Memo = EgressList.ElementAt(i).Memo;
                atmyapplyalllog.EvenLength = Math.Round(EgressList.ElementAt(i).EvenLength, 1);
                atmyapplyalllog.ATEventTypeID = EgressList.ElementAt(i).ATEventTypeID.ToString();
                atmyapplyalllog.ATEventTypeName = EgressList.ElementAt(i).ATEventTypeName;
                atmyapplyalllog.ATEventSubTypeID = EgressList.ElementAt(i).ATEventSubTypeID.ToString();
                atmyapplyalllog.ATEventSubTypeName = EgressList.ElementAt(i).ATEventSubTypeName;

                atmyapplyalllog.StartDateTime = EgressList.ElementAt(i).StartDateTime.Value.ToString("MM-dd HH:mm");
                atmyapplyalllog.EndDateTime = EgressList.ElementAt(i).EndDateTime.Value.ToString("MM-dd HH:mm");
                atmyapplyalllog.EvenLengthUnit = "小时";

                atmyapplyalllog.ApproveStatusID = (EgressList.ElementAt(i).ATApproveStatus != null) ? EgressList.ElementAt(i).ATApproveStatus.Id : 0;
                atmyapplyalllog.ApproveStatusName = EgressList.ElementAt(i).ApproveStatusName;
                atmyapplyalllog.ApproveID = EgressList.ElementAt(i).ApproveID.ToString();
                atmyapplyalllog.ApproveName = EgressList.ElementAt(i).ApproveName;
                atmyapplyalllog.ApproveDateTime = (EgressList.ElementAt(i).ApproveDateTime.HasValue) ? EgressList.ElementAt(i).ApproveDateTime.Value.ToString("yyyy-MM-dd HH:mm") : "";
                atmyapplyalllog.ApproveMemo = EgressList.ElementAt(i).ApproveMemo;
                long? applyId = EgressList.ElementAt(i).ApplyID;
                GetApplyOfRBACUser(rbacuserList, ref atmyapplyalllog, applyId);
                atemCount.Add(atmyapplyalllog);
            }
            return atemCount;
        }
        /// <summary>
        /// 出差
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="days"></param>
        /// <param name="allDates"></param>
        /// <param name="TripList"></param>
        /// <param name="rbacuserList"></param>
        /// <returns></returns>
        private List<ATEmpApplyAllLog> GetTripList(DateTime firstDate, int days, Dictionary<string, DayOfWeek> allDates, IEnumerable<ATEmpAttendanceEventLog> TripList, IList<RBACUser> rbacuserList)
        {
            TripList = TripList.OrderBy(a => a.ApplyID).ThenBy(a => a.DataAddTime);
            List<ATEmpApplyAllLog> atemCount = new List<ATEmpApplyAllLog>();
            //当前日期范围内在统计月的请假日期集合
            IList<string> curTripDatesList = new List<string>();
            ATEmpApplyAllLog defaultApplyAllLog = null;
            string curDate = "";
            DateTime tempDate = new DateTime();
            for (int j = 0; j < TripList.Count(); j++)
            {
                int leaveDatesType = GetLeaveDatesType(firstDate, allDates, TripList.ElementAt(j));
                long? applyId = TripList.ElementAt(j).ApplyID;
                defaultApplyAllLog = GetDafaultATEmpApplyAllLog(firstDate, TripList.ElementAt(j));
                defaultApplyAllLog.ATEventDateCode = TripList.ElementAt(j).ATEventDateCode;
                defaultApplyAllLog.EventStatPostion = TripList.ElementAt(j).EventStatPostion;
                defaultApplyAllLog.EventDestinationPostion = TripList.ElementAt(j).EventDestinationPostion;
                defaultApplyAllLog.TransportationName = TripList.ElementAt(j).TransportationName;

                defaultApplyAllLog.EvenLength = Math.Round(TripList.ElementAt(j).EvenLength, 1);
                defaultApplyAllLog.EvenLengthUnit = "天";

                switch (leaveDatesType)
                {
                    case 2:
                        defaultApplyAllLog.StartDateTime = TripList.ElementAt(j).StartDateTime.Value.ToString("yyyy-MM-dd");
                        defaultApplyAllLog.EndDateTime = firstDate.AddDays(days - 1).ToString("yyyy-MM-dd");
                        tempDate = DateTime.Parse(defaultApplyAllLog.StartDateTime);
                        for (int i = DateTime.Parse(defaultApplyAllLog.StartDateTime).Day; i <= firstDate.AddDays(days - 1).Day; i++)
                        {
                            curDate = tempDate.ToString("yyyy-MM-dd");
                            //ZhiFang.Common.Log.Log.Debug("TripList.curDate:" + curDate);
                            if (!curTripDatesList.Contains(curDate))
                            {
                                curTripDatesList.Add(curDate);
                                GetApplyOfRBACUser(rbacuserList, ref defaultApplyAllLog, applyId);
                                atemCount.Add(defaultApplyAllLog);
                            }
                            tempDate = tempDate.AddDays(1);
                        }
                        break;
                    case 3:
                        defaultApplyAllLog.StartDateTime = firstDate.ToString("yyyy-MM-dd");
                        defaultApplyAllLog.EndDateTime = TripList.ElementAt(j).EndDateTime.Value.ToString("yyyy-MM-dd");
                        tempDate = firstDate;
                        for (int i = firstDate.Day; i <= DateTime.Parse(defaultApplyAllLog.EndDateTime).Day; i++)
                        {
                            curDate = tempDate.ToString("yyyy-MM-dd");
                            if (!curTripDatesList.Contains(curDate))
                            {
                                curTripDatesList.Add(curDate);
                                GetApplyOfRBACUser(rbacuserList, ref defaultApplyAllLog, applyId);
                                atemCount.Add(defaultApplyAllLog);
                            }
                            tempDate = tempDate.AddDays(1);
                        }
                        break;
                    case 4:
                        defaultApplyAllLog.StartDateTime = firstDate.ToString("yyyy-MM-dd");
                        defaultApplyAllLog.EndDateTime = firstDate.AddDays(days - 1).ToString("yyyy-MM-dd");
                        tempDate = firstDate;
                        for (int i = firstDate.Day; i <= firstDate.AddDays(days - 1).Day; i++)
                        {
                            curDate = tempDate.ToString("yyyy-MM-dd");
                            if (!curTripDatesList.Contains(curDate))
                            {
                                curTripDatesList.Add(curDate);
                                GetApplyOfRBACUser(rbacuserList, ref defaultApplyAllLog, applyId);
                                atemCount.Add(defaultApplyAllLog);
                            }
                            tempDate = tempDate.AddDays(1);
                        }
                        break;
                    default:
                        #region 日期范围开始时间及结束时间都在统计月
                        defaultApplyAllLog.StartDateTime = TripList.ElementAt(j).StartDateTime.Value.ToString("yyyy-MM-dd");
                        defaultApplyAllLog.EndDateTime = TripList.ElementAt(j).EndDateTime.Value.ToString("yyyy-MM-dd");

                        tempDate = DateTime.Parse(TripList.ElementAt(j).StartDateTime.Value.ToString("yyyy-MM-dd"));
                        for (int i = DateTime.Parse(defaultApplyAllLog.StartDateTime).Day; i <= DateTime.Parse(defaultApplyAllLog.EndDateTime).Day; i++)
                        {
                            curDate = tempDate.ToString("yyyy-MM-dd");
                            if (!curTripDatesList.Contains(curDate))
                            {
                                curTripDatesList.Add(curDate);
                                GetApplyOfRBACUser(rbacuserList, ref defaultApplyAllLog, applyId);
                                atemCount.Add(defaultApplyAllLog);
                            }
                            tempDate = tempDate.AddDays(1);
                        }
                        #endregion
                        break;
                }
            }
            return atemCount;
        }
        private List<ATEmpApplyAllLog> GetOvertimeList(IEnumerable<ATEmpAttendanceEventLog> OvertimeList, IList<RBACUser> rbacuserList)
        {
            List<ATEmpApplyAllLog> atemCount = new List<ATEmpApplyAllLog>();
            OvertimeList = OvertimeList.OrderBy(a => a.ApplyID).ThenBy(a => a.DataAddTime);
            for (int i = 0; i < OvertimeList.Count(); i++)
            {
                ATEmpApplyAllLog atmyapplyalllog = new ATEmpApplyAllLog();
                atmyapplyalllog.ATEmpAttendanceEventLogId = OvertimeList.ElementAt(i).Id.ToString();
                atmyapplyalllog.DataAddTime = OvertimeList.ElementAt(i).DataAddTime.Value.ToString("yyyy-MM-dd HH:mm");
                atmyapplyalllog.Memo = OvertimeList.ElementAt(i).Memo;
                atmyapplyalllog.EvenLength = Math.Round(OvertimeList.ElementAt(i).EvenLength, 1);
                atmyapplyalllog.ATEventTypeID = OvertimeList.ElementAt(i).ATEventTypeID.ToString();
                atmyapplyalllog.ATEventTypeName = OvertimeList.ElementAt(i).ATEventTypeName;
                atmyapplyalllog.ATEventSubTypeID = OvertimeList.ElementAt(i).ATEventSubTypeID.ToString();
                atmyapplyalllog.ATEventSubTypeName = OvertimeList.ElementAt(i).ATEventSubTypeName;

                atmyapplyalllog.StartDateTime = OvertimeList.ElementAt(i).StartDateTime.Value.ToString("MM-dd HH:mm");
                atmyapplyalllog.EndDateTime = OvertimeList.ElementAt(i).EndDateTime.Value.ToString("MM-dd HH:mm");
                atmyapplyalllog.EvenLengthUnit = "小时";

                atmyapplyalllog.ApproveStatusID = (OvertimeList.ElementAt(i).ATApproveStatus != null) ? OvertimeList.ElementAt(i).ATApproveStatus.Id : 0;
                atmyapplyalllog.ApproveStatusName = OvertimeList.ElementAt(i).ApproveStatusName;
                atmyapplyalllog.ApproveID = OvertimeList.ElementAt(i).ApproveID.ToString();
                atmyapplyalllog.ApproveName = OvertimeList.ElementAt(i).ApproveName;
                atmyapplyalllog.ApproveDateTime = (OvertimeList.ElementAt(i).ApproveDateTime.HasValue) ? OvertimeList.ElementAt(i).ApproveDateTime.Value.ToString("yyyy-MM-dd HH:mm") : "";
                atmyapplyalllog.ApproveMemo = OvertimeList.ElementAt(i).ApproveMemo;
                atmyapplyalllog.EmpName = OvertimeList.ElementAt(i).ApplyName;

                long? applyId = OvertimeList.ElementAt(i).ApplyID;
                GetApplyOfRBACUser(rbacuserList, ref atmyapplyalllog, applyId);
                atemCount.Add(atmyapplyalllog);
            }
            return atemCount;
        }
        /// <summary>
        /// 统计明细里的申请人姓名,工号,所属部门,职务
        /// </summary>
        /// <param name="rbacuserList"></param>
        /// <param name="atmyapplyalllog"></param>
        /// <param name="applyId"></param>
        private void GetApplyOfRBACUser(IList<RBACUser> rbacuserList, ref ATEmpApplyAllLog atmyapplyalllog, long? applyId)
        {
            if (rbacuserList != null && applyId.HasValue)
            {
                var rbacuser = rbacuserList.Where(a => a.HREmployee.Id == applyId);
                if (rbacuser != null && rbacuser.Count() > 0)
                {
                    atmyapplyalllog.Account = rbacuser.ElementAt(0).Account;
                    if (rbacuser.ElementAt(0).HREmployee != null && rbacuser.ElementAt(0).HREmployee.HRDept != null)
                    {
                        atmyapplyalllog.EmpName = rbacuser.ElementAt(0).HREmployee.CName;
                        atmyapplyalllog.ApplyEmp = new EmpInfo() { EmpId = rbacuser.ElementAt(0).HREmployee.Id.ToString(), EmpName = rbacuser.ElementAt(0).HREmployee.CName, HeadImgUrl = IDHREmployeeDao.Get(rbacuser.ElementAt(0).HREmployee.Id).PicFile };
                    }
                    if (rbacuser.ElementAt(0).HREmployee != null && rbacuser.ElementAt(0).HREmployee.HRDept != null)
                        atmyapplyalllog.HRDeptCName = rbacuser.ElementAt(0).HREmployee.HRDept.CName;
                    if (rbacuser.ElementAt(0).HREmployee != null && rbacuser.ElementAt(0).HREmployee.HRPosition != null)
                        atmyapplyalllog.HRPositionCName = rbacuser.ElementAt(0).HREmployee.HRPosition.CName;
                }
            }
        }
        #endregion

        #region others
        /// <summary>
        /// 获取部门员工ID字符串
        /// </summary>
        /// <param name="deptId">部门id</param>
        /// <param name="isGetSubDept">是否包含子部门员工</param>
        /// <param name="empId">外部传入的员工id字符串</param>
        /// <param name="disable">是否包含已停用员工</param>
        /// <returns></returns>
        private string GetEmpIdByDeptId(string deptId, bool isGetSubDept, string empId, bool disable)
        {
            StringBuilder empidlist = new StringBuilder();
            if (!String.IsNullOrEmpty(empId))
            {
                empidlist.Append(empId + ",");
            }
            IList<long> pdeptlist = new List<long>();
            long id = 0;
            if (!String.IsNullOrEmpty(deptId))
            {
                id = long.Parse(deptId);
                if (isGetSubDept)
                {
                    pdeptlist = IDHRDeptDao.GetSubDeptIdListByDeptId(id);
                }
                if (!pdeptlist.Contains(id))
                    pdeptlist.Add(id);
            }
            IList<HRDept> alldeptlist = new List<HRDept>();
            if (pdeptlist.Count > 0)
                alldeptlist = IDHRDeptDao.GetListByHQL(" IsUse=true and Id in (" + string.Join(",", pdeptlist.ToArray()) + ") ");
            for (int i = 0; i < alldeptlist.Count; i++)
            {
                IList<HREmployee> emplist = alldeptlist[i].HREmployeeList;
                if (emplist != null && emplist.Count > 0)
                {
                    foreach (var emp in emplist)
                    {
                        string tempId = emp.Id + ",";
                        //包含已停用员工
                        if (disable)
                        {
                            if (!empidlist.ToString().Contains(tempId))
                                empidlist.Append(tempId);
                        }
                        else
                        {
                            if (emp.IsUse.HasValue && emp.IsUse.Value)
                                if (!empidlist.ToString().Contains(tempId))
                                    empidlist.Append(tempId);
                        }
                    }
                }
            }
            return empidlist.ToString().TrimEnd(',');
        }
        /// <summary>
        /// 获取统计月的请假或出差时的默认考勤信息
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private ATEmpApplyAllLog GetDafaultATEmpApplyAllLog(DateTime firstDate, ATEmpAttendanceEventLog entity)
        {
            ATEmpApplyAllLog defaultApplyAllLog = new ATEmpApplyAllLog();
            int days = DateTime.DaysInMonth(firstDate.Year, firstDate.Month);
            defaultApplyAllLog.ATEmpAttendanceEventLogId = entity.Id.ToString();
            defaultApplyAllLog.DataAddTime = entity.DataAddTime.Value.ToString("yyyy-MM-dd HH:mm");
            defaultApplyAllLog.Memo = entity.Memo;
            defaultApplyAllLog.EvenLength = Math.Round(entity.EvenLength, 1);
            defaultApplyAllLog.ATEventTypeID = entity.ATEventTypeID.ToString();
            defaultApplyAllLog.ATEventTypeName = entity.ATEventTypeName;
            defaultApplyAllLog.ATEventSubTypeID = entity.ATEventSubTypeID.ToString();
            defaultApplyAllLog.ATEventSubTypeName = entity.ATEventSubTypeName;
            defaultApplyAllLog.ATEventDateCode = entity.ATEventDateCode;
            defaultApplyAllLog.EmpName = entity.ApplyName;
            defaultApplyAllLog.StartDateTime = firstDate.ToString("yyyy-MM-dd");// LeaveList.ElementAt(0).StartDateTime.Value.ToString("yyyy-MM-dd");
            defaultApplyAllLog.EndDateTime = firstDate.AddDays(days - 1).ToString("yyyy-MM-dd");//  LeaveList.ElementAt(0).EndDateTime.Value.ToString("yyyy-MM-dd");
            defaultApplyAllLog.EvenLengthUnit = "天";

            defaultApplyAllLog.ApproveStatusID = (entity.ATApproveStatus != null) ? entity.ATApproveStatus.Id : 0;
            defaultApplyAllLog.ApproveStatusName = entity.ApproveStatusName;
            defaultApplyAllLog.ApproveID = entity.ApproveID.ToString();
            defaultApplyAllLog.ApproveName = entity.ApproveName;
            defaultApplyAllLog.ApproveDateTime = (entity.ApproveDateTime.HasValue) ? entity.ApproveDateTime.Value.ToString("yyyy-MM-dd HH:mm") : "";
            defaultApplyAllLog.ApproveMemo = entity.ApproveMemo;
            return defaultApplyAllLog;
        }
        /// <summary>
        /// 获取日期范围里的开始时间及结束日期在统计月的类型
        /// 1.日期范围开始时间及结束时间都在统计月
        /// 2.日期范围开始时间在统计月,结束时间不在(大于)统计月
        /// 3.日期范围开始时间不在(小于)统计月,结束时间在统计月
        /// 4.日期范围开始时间(小于)统计月及结束时间都不在(大于)统计月,但日期范围包含统计月在内
        /// </summary>
        /// <param name="firstDate">统计月第一天的日期</param>
        /// <param name="allDates">统计月的所有日期集合</param>
        /// <param name="entity">考勤信息</param>
        /// <returns></returns>
        private int GetLeaveDatesType(DateTime firstDate, Dictionary<string, DayOfWeek> allDates, ATEmpAttendanceEventLog entity)
        {
            int tempType = 0;
            int days = DateTime.DaysInMonth(firstDate.Year, firstDate.Month);
            string endDate = firstDate.AddDays(days - 1).ToString("yyyy-MM-dd");
            DateTime startDateTime = DateTime.Parse(entity.StartDateTime.Value.ToString("yyyy-MM-dd"));
            DateTime endDateTime = DateTime.Parse(entity.EndDateTime.Value.ToString("yyyy-MM-dd"));
            bool fristIsInDate = false, lastIsInDate = false;
            //日期范围开始日期是否在统计月
            fristIsInDate = IsInDate(startDateTime, firstDate, firstDate.AddDays(days - 1));
            //日期范围结束日期是否在统计月
            lastIsInDate = IsInDate(endDateTime, firstDate, firstDate.AddDays(days - 1));
            //当前日期范围内在统计月的请假日期集合
            IList<string> curLeaveDates = new List<string>();
            curLeaveDates = GetAllDaysOfOneMonth(startDateTime, endDateTime, allDates);

            if (fristIsInDate == true && lastIsInDate == true)
            {
                tempType = 1;
            }
            else if (fristIsInDate == true && lastIsInDate == false && DateTime.Compare(endDateTime, DateTime.Parse(endDate)) > 0)
            {
                tempType = 2;
            }
            else if (fristIsInDate == false && lastIsInDate == true)
            {
                tempType = 3;
            }
            else if (fristIsInDate == false && lastIsInDate == false)
            {
                //统计月开始日期是否在日期范围
                fristIsInDate = IsInDate(firstDate, startDateTime, endDateTime);
                //统计月结束日期是否在日期范围
                lastIsInDate = IsInDate(firstDate.AddDays(days - 1), startDateTime, endDateTime);
                if (fristIsInDate == true && lastIsInDate == true)
                    tempType = 4;
                //ZhiFang.Common.Log.Log.Debug(entity.ApplyName + entity.ATEventSubTypeName + "fristIsInDate:" + fristIsInDate + "lastIsInDate:" + lastIsInDate);
            }
            return tempType;
        }

        /// <summary>
        /// 将小时转换为天数
        /// 大于0并小于4小时的,为0.25天;大于4并小于8小时的,为0.5天;
        /// </summary>
        /// <param name="subtractHours"></param>
        /// <returns></returns>
        private double ChangeHoursToDays(double subtractHours)
        {
            double days = 0;
            if (subtractHours >= 8)
            {
                days = Math.Round((subtractHours / 8), 2);
                //小数点处理
                double decimalDay = days - Math.Truncate(days);
                if (decimalDay >= 0.5 && decimalDay < 1)
                {
                    days = Math.Truncate(days) + 0.5;
                }
                else if (decimalDay > 0 && decimalDay < 0.5)
                {
                    days = Math.Truncate(days) + 0.25;
                }
            }
            else if (subtractHours > 4 && subtractHours < 8)
            {
                days = 0.5;
            }
            else if (subtractHours > 0 && subtractHours <= 4)
            {
                days = 0.25;
            }
            else
            {
                days = Math.Round((subtractHours / 8), 2);
            }
            return days;
        }

        #region 获取某段日期范围内的所有日期
        /// <summary> 
        /// 获取某段日期范围内的所有日期，以数组形式返回  
        /// </summary>  
        /// <param name="dt1">开始日期</param>  
        /// <param name="dt2">结束日期</param>  
        /// <returns></returns>  
        public List<DateTime> GetAllDays(DateTime dt1, DateTime dt2)
        {
            List<DateTime> listDays = new List<DateTime>();
            DateTime dtDay = new DateTime();
            for (dtDay = dt1; dtDay.CompareTo(dt2) <= 0; dtDay = dtDay.AddDays(1))
            {
                listDays.Add(dtDay);
            }
            return listDays;
        }
        /// <summary> 
        /// 获取某段日期范围内所有日期
        /// </summary>  
        /// <param name="dt1">开始日期</param>  
        /// <param name="dt2">结束日期</param>  
        /// <returns></returns>  
        public List<string> GetAllDayStr(DateTime dt1, DateTime dt2, List<string> allDates)
        {
            List<string> listDays = new List<string>();
            DateTime dtDay = new DateTime();
            for (dtDay = dt1; dtDay.CompareTo(dt2) <= 0; dtDay = dtDay.AddDays(1))
            {
                string curdate = dtDay.ToString("yyyy-MM-dd");
                if (allDates.Contains(curdate) || allDates.Count == 0)
                    listDays.Add(curdate);
            }
            return listDays;
        }
        /// <summary> 
        /// 获取某段日期范围内在统计月的所有日期
        /// </summary>  
        /// <param name="dt1">开始日期</param>  
        /// <param name="dt2">结束日期</param>  
        /// <returns></returns>  
        public List<string> GetAllDaysOfOneMonth(DateTime dt1, DateTime dt2, Dictionary<string, DayOfWeek> allDates)
        {
            List<string> listDays = new List<string>();
            DateTime dtDay = new DateTime();
            for (dtDay = dt1; dtDay.CompareTo(dt2) <= 0; dtDay = dtDay.AddDays(1))
            {
                string curdate = dtDay.ToString("yyyy-MM-dd");
                if (allDates.ContainsKey(curdate))
                    listDays.Add(curdate);
            }
            return listDays;
        }
        #endregion

        #region 判断某个日期是否在某段日期范围内
        /// <summary> 
        /// 判断某个日期是否在某段日期范围内，返回布尔值
        /// </summary> 
        /// <param name="dt">要判断的日期</param> 
        /// <param name="dtStart">开始日期</param> 
        /// <param name="dtEnd">结束日期</param> 
        /// <returns></returns>  
        public bool IsInDate(DateTime dt, DateTime dtStart, DateTime dtEnd)
        {
            return dt.CompareTo(dtStart) >= 0 && dt.CompareTo(dtEnd) <= 0;
        }
        #endregion

        #endregion
    }
}