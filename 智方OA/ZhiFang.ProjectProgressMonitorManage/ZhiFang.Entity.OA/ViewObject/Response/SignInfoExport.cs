using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Entity.OA.ViewObject.Response
{
    /// <summary>
    /// 员工打卡清单统计及导出
    /// </summary>
    public class SignInfoExport
    {
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        /// <summary>
        /// 申请人工号(登录帐号)
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 申请人所属部门
        /// </summary>
        public string HRDeptCName { get; set; }
        /// <summary>
        /// 申请人职务
        /// </summary>
        public string HRPositionCName { get; set; }
        /// <summary>
        /// 签到事件位置坐标名称
        /// </summary>
        public string SigninATEventLogPostionName { get; set; }
        /// <summary>
        /// 签退事件位置坐标名称
        /// </summary>
        public string SignoutATEventLogPostionName { get; set; }
        /// <summary>
        /// 签到日志ID
        /// </summary>
        public string SignInId { get; set; }
        public string SignInTime { get; set; }
        public string SignInMemo { get; set; }
        public string SignInType { get; set; }
        public long SignInSubTypeID { get; set; }
        //public bool SignInIsOffsite { get; set; }
        /// <summary>
        /// 签退日志ID
        /// </summary>
        public string SignOutId { get; set; }
        public string SignOutTime { get; set; }
        public string SignOutMemo { get; set; }
        public string SignOutType { get; set; }
        public long SignOutSubTypeID { get; set; }
        //public bool SignOutIsOffsite { get; set; }
        public string WeekInfo { get; set; }
        //public bool IsWorkDay { get; set; }
        public string ATEventDateCode { get; set; }
        //public string OtherInfo { get; set; }
    }
}
