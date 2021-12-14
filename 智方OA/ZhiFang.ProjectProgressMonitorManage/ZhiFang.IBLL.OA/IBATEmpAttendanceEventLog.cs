using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.OA;
using ZhiFang.Entity.OA.ViewObject.Response;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Request;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.OA
{
    /// <summary>
    ///
    /// </summary>
    public interface IBATEmpAttendanceEventLog : IBGenericManager<ATEmpAttendanceEventLog>
    {
        bool AddSignIn();

        bool AddSignOut();

        IList<ATEmpAttendanceEventLog> GetInfoBydtcode(string dtcode, string empid);

        ZhiFang.Entity.OA.ViewObject.Response.SignInfo GetSignInfoBydtcode(string dtcode, long empid);

        HREmployee GetATEmpAttendanceEventApproveByDeptId(long DeptId);

        HREmployee GetATEmpAttendanceEventApproveByEmpId(long EmpId);

        float GetATEmpAttendanceEventDayCount(string sd, string ed);

        Double GetATEmpAttendanceEventHourCount(string sd, string ed);

        /// <summary>
        /// 新增员工考勤请假事件并验证是否允许申请
        /// </summary>
        /// <param name="pushWeiXinMessageAction"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        BaseResultBool AddAndCheckATEmpAttendanceEventleaveevent(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, ATEmpAttendanceEventLog entity);
        bool AddATEmpAttendanceEventleaveevent(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, ATEmpAttendanceEventLog entity);

        /// <summary>
        /// 新增员工考勤外出事件并验证是否允许申请
        /// </summary>
        /// <param name="pushWeiXinMessageAction"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        BaseResultBool AddAndCheckATEmpAttendanceEventEgressevent(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, ATEmpAttendanceEventLog entity);
        bool AddATEmpAttendanceEventEgressevent(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, ATEmpAttendanceEventLog entity);
       
        /// <summary>
        /// 新增员工考勤出差事件并验证是否允许申请
        /// </summary>
        /// <param name="pushWeiXinMessageAction"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        BaseResultBool AddAndCheckATEmpAttendanceEventTripevent(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, ATEmpAttendanceEventLog entity);
        bool AddATEmpAttendanceEventTripevent(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, ATEmpAttendanceEventLog entity);        

        bool AddATEmpAttendanceEventOvertimeevent(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, ATEmpAttendanceEventLog entity);
        /// <summary>
        /// 新增员工考勤加班事件并验证是否允许申请
        /// </summary>
        /// <param name="pushWeiXinMessageAction"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        BaseResultBool AddAndCheckATEmpAttendanceEventOvertimeevent(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, ATEmpAttendanceEventLog entity);

        IList<Entity.OA.ViewObject.Response.SignLog> SearchATEmpSignLog(string sd, string ed, int type, long empid);
        IList<Entity.OA.ViewObject.Response.SignLog> SearchATEmpSignLogByLimit(string ed, int limit, int type, long v);
        IList<SignLogEmpList> SearchATOtherSignLogByLimit(string ed, int limit, int type, long deptid, long empid, long otherempid);
        IList<ATEmpApplyAllLog> SearchATEmpApplyAllLogByLimit(string sd, string ed, int pageindex, int limit, string apsid, int type, long empid);
        IList<ATEmpApplyAllLog> SearchATMyApprovalAllLogByEmpId(string sd, string ed, int pageindex, int limit, string apsid, string typeidlist, long empid);
        bool ApprovalATApplyEventLog(string memo, string[] eventlogids, int type, long empid);
        bool ApprovalATApplyEventLog(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, string memo, string[] eventlogids, int type, long empid);
        ATEmpMonthLog GetEmpMonthLog(long EmpId, string monthCode, long ManagerEmpId);
        List<ATEmpListWeekLog> GetATEmpListWeekLog(long type, string startDate, string endDate, long EmpId, string EmpName, long HRDeptID, string HRDeptName);
        bool AddUploadPostion();
        /// <summary>
        /// 导出并保存获取到公司所有员工的考勤统计信息为Excel文件到服务器
        /// </summary>
        /// <param name="monthCode"></param>
        /// <param name="wagesDays"></param>
        /// <param name="punch"></param>
        FileStream GetExportExcelOfAllMonthLogCount(string monthCode, int wagesDays, int punch, string fileName);
        /// <summary>
        /// 获取公司所有员工的考勤统计信息
        /// </summary>
        /// <param name="monthCode">年月</param>
        /// <param name="wagesDays">工资日总天数</param>
        /// <param name="punch">每天的打卡次数</param>
        /// <returns></returns>
        IList<ATEmpMonthLogCount> GetAllMonthLogCountList(string monthCode, int wagesDays, int punch);
        /// <summary>
        /// 员工的考勤签到或签退时的地点及时间判断是否异常处理
        /// </summary>
        /// <param name="entity">员工的考勤信息</param>
        /// <returns></returns>
        BaseResultBool AddATEmpAttendanceEventLogCheck();
        /// <summary>
        /// 员工的考勤地点是否正确或异常的判断处理
        /// </summary>
        /// <param name="empSetting">员工的考勤设置信息,可为空</param>
        /// <param name="enumType">考勤地点类型</param>
        /// <returns></returns>
        BaseResultBool ATEmpAttendanceEventLogCheckPostion(ATEmpAttendanceEventParaSettings empSetting, ATEventPostionType enumType);
        /// <summary>
        /// 处理签到时间与考勤设置上班时间是正常还是迟到
        /// 只有参数设置类型为固定时间才作是否异常判断
        /// </summary>
        /// <param name="empSetting">员工的考勤设置信息,可为</param>
        /// <returns></returns>
        BaseResultBool ATEmpAttendanceEventLogCheckSignInTime(ATEmpAttendanceEventParaSettings empSetting);
        /// <summary>
        /// 处理签退时间与考勤设置下班时间是正常还是早退
        /// </summary>
        /// <param name="empSetting">员工的考勤设置信息,可为空</param>
        /// <returns></returns>
        BaseResultBool ATEmpAttendanceEventLogCheckSignOutTime(ATEmpAttendanceEventParaSettings empSetting);
        /// <summary>
        /// 请假,外出,加班,出差申请时,不能补申请当前月一号之前(小于当前月一号 00:00:00),跨年
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        BaseResultBool AddATEmpAttendanceEventCheck(ATEmpAttendanceEventLog entity);
        /// <summary>
        /// 员工考勤上报位置并与考勤设置进行验证处理是否可以上报事件
        /// </summary>
        /// <returns></returns>
        BaseResultDataValue AddUploadPostionAndCheck();
        /// <summary>
        /// 员工考勤统计清单信息
        /// </summary>
        /// <param name="searchType">查询类型</param>
        /// <param name="attypeId">考勤事件类型Id</param>
        /// <param name="deptId">部门id</param>
        /// <param name="isGetSubDept">是否获取子部门的员工信息</param>
        /// <param name="empId">员工id字符串,如123,232,1233</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        EntityList<ATEmpApplyAllLog> GetATEmpAttendanceEventLogDetailList(long searchType, string attypeId, string deptId, bool isGetSubDept, string empId, string startDate, string endDate,long approveStatusID, int page, int limit, string sort);
        /// <summary>
        /// 导出并保存员工考勤统计清单信息为Excel文件到服务器
        /// </summary>
        /// <param name="searchType">查询类型</param>
        /// <param name="attypeId">考勤事件类型Id</param>
        /// <param name="deptId">部门id</param>
        /// <param name="isGetSubDept">是否获取子部门的员工信息</param>
        /// <param name="empId">员工id字符串,如123,232,1233</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        FileStream GetExportExcelOfATEmpAttendanceEventLogDetail(long searchType, string attypeId, string deptId, bool isGetSubDept, string empId, string startDate, string endDate,long approveStatusID,ref string fileName);

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
        EntityList<SignInfoExport> GetATEmpSignInfoDetailList(string filterType, string deptId, bool isGetSubDept, string empId, string startDate, string endDate, int page, int limit, string sort);
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
        FileStream GetExportExcelOfATEmpSignInfoDetail(string filterType, string deptId, bool isGetSubDept, string empId, string startDate, string endDate,ref string fileName);
    }
}