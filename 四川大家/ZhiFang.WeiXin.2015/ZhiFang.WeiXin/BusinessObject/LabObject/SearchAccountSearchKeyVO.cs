using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.WeiXin.BusinessObject.LabObject
{
    public class SearchAccountSearchKeyVO
    {
        /// <summary>
        /// MobileCode,IDNumber,MediCare,PatNo,VisNo,TakeNo:"6项中选一项"
        /// </summary>
        public string FieldsCode { get; set; }
        /// <summary>
        /// SearchContext
        /// </summary>
        public string FieldsValue { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 顺序号
        /// </summary>
        public int DispOrder { get; set; }
    }
}
