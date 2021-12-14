using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.WeiXin.BusinessObject.LabObject
{
    public class AppMicro
    {
        /// <summary>
        /// 微生物名称
        /// </summary>
        public string MicroName { get; set; }
        /// <summary>
        /// 微生物编码
        /// </summary>
        public string MicroNo { get; set; }
        /// <summary>
        /// 微生物描述
        /// </summary>
        public string MicroDesc { get; set; }
        /// <summary>
        /// 抗生素列表
        /// </summary>
        public List<AppAnit> AnitList { get; set; }
    }
}
