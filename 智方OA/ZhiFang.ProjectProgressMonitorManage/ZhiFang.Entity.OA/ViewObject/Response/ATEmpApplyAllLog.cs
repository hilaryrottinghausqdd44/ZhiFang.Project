using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.RBAC.ViewObject.Response;

namespace ZhiFang.Entity.OA.ViewObject.Response
{
    public class ATEmpApplyAllLog
    {
        public string ATEmpAttendanceEventLogId { get; set; }
        public string DataAddTime { get; set; }
        /// <summary>
        /// 申请人姓名
        /// </summary>
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
        /// 事件日期编码(如外出日期,加班日期)
        /// </summary>
        public string ATEventDateCode { get; set; }
        /// <summary>
        /// 签到日志ID
        /// </summary>
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        
        public string Memo { get; set; }
        public Double EvenLength { get; set; }
        public string EvenLengthUnit { get; set; }
        public string ATEventTypeID { get; set; }
        public string ATEventTypeName { get; set; }
        public string ATEventSubTypeID { get; set; }
        public string ATEventSubTypeName { get; set; }
        /// <summary>
        /// 审批状态ID
        /// </summary>
        public long ApproveStatusID { get; set; }
        /// <summary>
        /// 审批状态名称
        /// </summary>
        public string ApproveStatusName { get; set; }
        /// <summary>
        /// 审批人ID
        /// </summary>
        public string ApproveID { get; set; }
        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string ApproveName { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public string ApproveDateTime { get; set; }
        /// <summary>
        /// 审批备注
        /// </summary>
        public string ApproveMemo { get; set; }
        /// <summary>
        /// 申请人信息
        /// </summary>
        public EmpInfo ApplyEmp { get; set;}
        
        /// <summary>
        /// 始发地
        /// </summary>
        public string EventStatPostion { get; set; }
        /// <summary>
        /// 目的地
        /// </summary>
        public string EventDestinationPostion { get; set; }
        /// <summary>
        /// 交通工具
        /// </summary>
        /// 
        public string TransportationName { get; set; }
        
    }
}
