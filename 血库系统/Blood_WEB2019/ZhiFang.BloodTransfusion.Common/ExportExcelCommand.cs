using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.BloodTransfusion.Common
{
    /// <summary>
    /// Excel填充规则公共填充数据项
    /// 前缀带"EEC_"为公共填充数据项
    /// 前缀带"SUM_"为合计填充数据项
    /// 填充格式:{T|EEC_StartDate}或{D|EEC_StartDate},{F|EEC_StartDate}
    /// </summary>
    [DataContract]
    public class ExportExcelCommand
    {
        protected string eEC_nowDate;
        protected string eEC_startDate;
        protected string eEC_endDate;
        protected string eEC_employeeName;
        protected string eEC_deptName;
        protected string eEC_labName;
        /// <summary>
        /// 当前日期
        /// </summary>
        public virtual string EEC_NowDate
        {
            get
            {
                if (string.IsNullOrEmpty(eEC_nowDate))
                    eEC_nowDate = DateTime.Now.ToString("yyyy-MM-dd");
                return eEC_nowDate;
            }
            set
            {
                eEC_nowDate = value;
            }
        }
        /// <summary>
        /// 起始日期
        /// </summary>
        public virtual string EEC_StartDate
        {
            get { return eEC_startDate; }
            set
            {
                eEC_startDate = value;
            }
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        public virtual string EEC_EndDate
        {
            get { return eEC_endDate; }
            set
            {
                eEC_endDate = value;
            }
        }
        /// <summary>
        /// 当前登录者姓名
        /// </summary>
        public virtual string EEC_EmployeeName
        {
            get
            {
                //if (string.IsNullOrEmpty(employeeName))
                //    employeeName = ZhiFang.Common.Public.Cookie.Get(DicCookieSession.UserAccount);
                return eEC_employeeName;
            }
            set
            {
                eEC_employeeName = value;
            }
        }
        /// <summary>
        /// 当前登录者所属部门名称
        /// </summary>
        public virtual string EEC_DeptName
        {
            get
            {
                //if (string.IsNullOrEmpty(deptName))
                //    deptName = ZhiFang.Common.Public.Cookie.Get(DicCookieSession.HRDeptName);
                return eEC_deptName;
            }
            set
            {
                eEC_deptName = value;
            }
        }
        /// <summary>
        /// 当前用户所属机构名称
        /// </summary>
        public virtual string EEC_LabName
        {
            get
            {
                return eEC_labName;
            }
            set
            {
                eEC_labName = value;
            }
        }
        public ExportExcelCommand() { }
    }

}
