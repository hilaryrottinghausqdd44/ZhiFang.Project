using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model.UiModel.WeiXin
{
    public class AppRMicroResult
    {
        /// <summary>
        /// 微生物项目名称
        /// </summary>
        public string MicroItemName { get; set; }
        /// <summary>
        /// 微生物项目编码
        /// </summary>
        public string MicroItemNo { get; set; }
        /// <summary>
        /// 微生物列表
        /// </summary>
        public List<AppMicro> MicroList { get; set; }
        /// <summary>
        /// 项目描述
        /// </summary>
        public string MicroItemDesc { get; set; }
        /// <summary>
        /// 培养描述
        /// </summary>
        public List<string> DescList { get; set; }
        /// <summary>
        /// 简码（ShortCode）
        /// </summary>
        public string ShortCode { get; set; }
    }
}
