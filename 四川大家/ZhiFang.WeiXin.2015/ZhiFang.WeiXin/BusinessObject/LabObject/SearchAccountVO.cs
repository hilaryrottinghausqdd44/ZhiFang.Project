using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.BusinessObject;

namespace ZhiFang.WeiXin.BusinessObject.LabObject
{
    public class SearchAccountVO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id{get;set;}
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name{get;set;}
        /// <summary>
        /// 性别
        /// </summary>
        public long? SexID { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string MobileCode { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNumber { get; set; }
        /// <summary>
        /// 医保卡号
        /// </summary>
        public string MediCare { get; set; }
        /// <summary>
        /// 未读报告单个数
        /// </summary>
        public int UnReadRFCount { get; set; }
        /// <summary>
        /// 报告单索引列表
        /// </summary>
        public string RFIndexList { get; set; }
        /// <summary>
        /// SearchList
        /// </summary>
        public List<SearchAccountSearchKeyVO> SearchList{get;set;}
    }
}
