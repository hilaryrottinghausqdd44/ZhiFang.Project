using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Entity.OA.ViewObject.Response
{
    public class ATEmpMonthLog
    {
        /// <summary>
        /// 签到签退
        /// </summary>
        public List<SignInfo> SignList { get; set; }
        /// <summary>
        /// 员工考勤设置信息
        /// </summary>
        public ATEmpAttendanceEventParaSettings ATEmpAttendanceEventParaSettings { get; set; }
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
        /// 本月代码：2016-07
        /// </summary>
        public string MonthCode { get; set; }
        /// <summary>
        /// 月开始日期
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 月结束日期
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// 本月计划
        /// </summary>
        public string ToDayContent { get; set; }
        /// <summary>
        /// 下月计划
        /// </summary>
        public string NextDayContent { get; set; }

    }
}
