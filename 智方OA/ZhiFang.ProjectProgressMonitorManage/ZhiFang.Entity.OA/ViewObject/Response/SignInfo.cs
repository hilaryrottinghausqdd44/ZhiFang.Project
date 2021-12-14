using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Entity.OA.ViewObject.Response
{
    public class SignInfo:ZhiFang.Entity.RBAC.ViewObject.Response.EmpInfo
    {
        /// <summary>
        /// 签到日志ID
        /// </summary>
        public string SignInId { get; set; }
        public string SignInTime { get; set; }
        public string SignInMemo { get; set; }
        public string SignInType { get; set; }
        public bool SignInIsOffsite { get; set; }
        /// <summary>
        /// 签退日志ID
        /// </summary>
        public string SignOutId { get; set; }
        public string SignOutTime { get; set; }
        public string SignOutMemo { get; set; }
        public string SignOutType { get; set; }
        public bool SignOutIsOffsite { get; set; }
        public string WeekInfo { get; set; }
        public bool IsWorkDay { get; set; }
        public string ATEventDateCode { get; set; }
        public string OtherInfo { get; set; }
    }
}
