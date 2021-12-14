using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Entity.OA.ViewObject.Response
{
    public class ATEmpListWeekLog
    {
        public string DeptName { get; set; }
        public long DeptId { get; set; }
        public string EmpName { get; set; }
        public long EmpId { get; set; }
        public ATEmpDayLog Monday { get; set; }
        public ATEmpDayLog Tuesday { get; set; }
        public ATEmpDayLog Wednesday { get; set; }
        public ATEmpDayLog Thursday { get; set; }
        public ATEmpDayLog Friday { get; set; }
        public ATEmpDayLog Saturday { get; set; }
        public ATEmpDayLog Sunday { get; set; }
        /// <summary>
        /// 员工考勤设置信息
        /// </summary>
        public ATEmpAttendanceEventParaSettings ATEmpAttendanceEventParaSettings { get; set; }
        /// <summary>
        /// 本周代码：2016-07
        /// </summary>
        public string WeekCode { get; set; }
        /// <summary>
        /// 周开始日期
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 周结束日期
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// 本周计划
        /// </summary>
        public string ToDayContent { get; set; }
        /// <summary>
        /// 下周计划
        /// </summary>
        public string NextDayContent { get; set; }

    }

    public class ATEmpDayLog
    {
        /// <summary>
        /// 签到签退
        /// </summary>
        public SignLog SignList { get; set; }
        /// <summary>
        /// 请假列表
        /// </summary>
        public List<ATEmpApplyAllLog> LeaveList { get; set; }
        /// <summary>
        /// 外出列表
        /// </summary>
        public List<ATEmpApplyAllLog> EgressList { get; set; }
        /// <summary>
        /// 出差列表
        /// </summary>
        public List<ATEmpApplyAllLog> TripList { get; set; }
        /// <summary>
        /// 加班列表
        /// </summary>
        public List<ATEmpApplyAllLog> OvertimeList { get; set; }
        /// <summary>
        /// 本日代码：2016-07-23
        /// </summary>
        public string DayCode { get; set; }
        /// <summary>
        /// 本日计划
        /// </summary>
        public string ToDayContent { get; set; }
        /// <summary>
        /// 次日计划
        /// </summary>
        public string NextDayContent { get; set; }
    }
}
