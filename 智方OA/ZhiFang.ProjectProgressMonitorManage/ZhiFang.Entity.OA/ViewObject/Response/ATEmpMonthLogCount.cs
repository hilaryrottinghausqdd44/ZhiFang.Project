using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Entity.OA.ViewObject.Response
{
   
    /// <summary>
    /// 员工考勤月统计信息
    /// </summary>
    public class ATEmpMonthLogCount
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        public long? EmpID { get; set; }
        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmpNo { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmpName { get; set; }
        /// <summary>
        /// 员工所属部门
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 是否全勤
        /// </summary>
        public string IsFullAttendance { get; set; }
        /// <summary>
        /// 实际签到天数
        /// </summary>
        public double SignInDays { get; set; }

        /// <summary>
        /// 实际签退天数
        /// </summary>
        public double SignOutDays { get; set; }
        /// <summary>
        /// 补签打卡天数
        /// </summary>
        public double FillCardsDays { get; set; }
        /// <summary>
        /// 实际签到总次数
        /// </summary>
        public int SignInCount { get; set; }
        /// <summary>
        /// 迟到总次数
        /// </summary>
        public int LateCount { get; set; }

        /// <summary>
        /// 实际签退总次数
        /// </summary>
        public int SignOutCount{ get; set; }

        /// <summary>
        /// 早退次数
        /// </summary>
        public double LeaveEarlyCount { get; set; }

        /// <summary>
        /// 事假
        /// </summary>
        public double JobLeaveDays { get; set; }

        /// <summary>
        /// 旷工天数=工资日-事假-病假-婚假-产假-护理假-丧假-调休-年假-出差
        /// </summary>
        public double AbsenteeismDays { get; set; }
        /// <summary>
        /// 入职缺勤
        /// </summary>
        public double EntryAbsenceDays { get; set; }

        /// <summary>
        /// 离职缺勤
        /// </summary>
        public int LeavingAbsenceDays { get; set; }
        /// <summary>
        /// 病假
        /// </summary>
        public double SickLeaveDays { get; set; }
        /// <summary>
        /// 婚假
        /// </summary>
        public double MarriageLeaveDays { get; set; }
        /// <summary>
        /// 产假
        /// </summary>
        public double MaternityLeaveDays { get; set; }
        /// <summary>
        /// 护理假
        /// </summary>
        public double CareLeaveDays { get; set; }

        /// <summary>
        /// 丧假
        /// </summary>
        public double BereavementLeaveDays { get; set; }

        /// <summary>
        /// 调休
        /// </summary>
        public double AdjustTheBreakDays { get; set; }
        /// <summary>
        /// 当月使用了的年假天数
        /// </summary>
        public double AnnualLeaveDays { get; set; }

        /// <summary>
        /// 外出
        /// </summary>
        public double EgressDays { get; set; }
        /// <summary>
        /// 出差
        /// </summary>
        public double TripDays { get; set; }

        /// <summary>
        /// 加班
        /// </summary>
        public double OvertimeDays { get; set; }
        /// <summary>
        /// 出差存休,不用统计,导出后再处理
        /// </summary>
        public double TravelHoliday { get; set; }

        /// <summary>
        /// 未打卡次数(一天打卡几次暂由前台传回)
        /// </summary>
        public double NotPunch { get; set; }
        /// <summary>
        /// 缺勤天数
        /// </summary>
        public double DaysOfAbsence { get; set; }
        /// <summary>
        /// 工资日,暂由前台传回
        /// </summary>
        public double WagesDays { get; set; }
        /// <summary>
        /// 公司日(当月在公司上班的天数)=工资日-事假-旷工-病假-婚假-产假-护理假-丧假-调休-年假-出差
        /// </summary>
        public double CompanyDays { get; set; }

        ///// <summary>
        ///// 本月代码：2016-07
        ///// </summary>
        //public string MonthCode { get; set; }
        ///// <summary>
        ///// 月开始日期
        ///// </summary>
        //public string StartDate { get; set; }
        ///// <summary>
        ///// 月结束日期
        ///// </summary>
        //public string EndDate { get; set; }

    }
}
