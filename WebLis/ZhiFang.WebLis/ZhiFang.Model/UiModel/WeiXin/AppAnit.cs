using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model.UiModel.WeiXin
{
    public class AppAnit
    {
        /// <summary>
        /// 抗生素名称
        /// </summary>
        public string AnitName { get; set; }
        /// <summary>
        /// 康生物编码
        /// </summary>
        public string AnitNo { get; set; }
        /// <summary>
        /// 检测方法学
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public string SusQuan { get; set; }
        /// <summary>
        /// 参考值
        /// </summary>
        public string RefRange { get; set; }
        /// <summary>
        /// 耐药结果
        /// </summary>
        public string Suscept { get; set; }
    }
}
