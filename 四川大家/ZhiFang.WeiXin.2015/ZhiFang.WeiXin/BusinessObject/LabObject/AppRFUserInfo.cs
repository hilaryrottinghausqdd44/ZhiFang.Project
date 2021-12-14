using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.WeiXin.BusinessObject.LabObject
{
    public class AppRFUserInfo
    {
        /// <summary>
        /// 出单医院
        /// </summary>
        public string HospitalName { get; set; }//:"出单医院",
        /// <summary>
        /// 报告ID,用于查看原始报告
        /// </summary>
        public string ReportId { get; set; }//:"报告ID,用于查看原始报告",
        /// <summary>
        /// 检验单号
        /// </summary>
        public string CheckListNumber { get; set; }//:"检验单号",
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }//:"患者姓名",
        /// <summary>
        /// 就诊时间
        /// </summary>
        public string VisitTime { get; set; }//:"就诊时间",
        /// <summary>
        /// 报告时间
        /// </summary>
        public string ReportTime { get; set; }//:"报告时间"

        /// <summary>
        /// 报告类型
        /// </summary>
        public string ReportType { get; set; }//:"报告类型"

        /// <summary>
        /// 报告备注
        /// </summary>
        public string ReportFORMMEMO { get; set; }//:"报告备注"

        /// <summary>
        /// 病历号
        /// </summary>
        public string PatNumber { get; set; }//:"病历号",

    }
}
