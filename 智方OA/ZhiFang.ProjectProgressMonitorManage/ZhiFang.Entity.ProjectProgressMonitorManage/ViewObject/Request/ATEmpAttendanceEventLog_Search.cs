using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Request
{
    public class ATEmpAttendanceEventLog_Search
    {
        private IList<long?> deptId;
        private IList<long?> empId;
        /// <summary>
        /// 开始日期
        /// </summary>
        private string StartDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        private string EndDate { get; set; }

        [DataMember]
        [DataDesc(CName = "部门Id", ShortCode = "EmpId", Desc = "部门Id")]
        public virtual IList<long?> DeptId
        {
            get
            {
                if (deptId == null)
                {
                    deptId = new List<long?>();
                }
                return deptId;
            }

            set
            {
                deptId = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "员工Id", ShortCode = "EmpId", Desc = "员工Id")]
        public virtual IList<long?> EmpId
        {
            get
            {
                if (empId == null)
                {
                    empId = new List<long?>();
                }
                return empId;
            }

            set
            {
                empId = value;
            }
        }
    }
}
