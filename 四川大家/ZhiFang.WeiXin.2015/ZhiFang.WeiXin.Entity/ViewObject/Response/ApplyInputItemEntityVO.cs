using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{
    public class ApplyInputItemEntity
    {
        public string CName { get; set; }
        public string EName { get; set; }
        public string ItemNo { get; set; }
        public string Prices { get; set; }
        public string ColorName { get; set; }
        public string ColorValue { get; set; }
        public List<SampleTypeDetail> SampleTypeDetail { get; set; }
        public string isCombiItem { get; set; }
    }
    public class SampleTypeDetail
    {
        public SampleTypeDetail() { }
        /// <summary>
        /// 样本名称
        /// </summary>
        public string CName { get; set; }
        /// <summary>
        /// 样本编号
        /// </summary>
        public string SampleTypeID { get; set; }
    }

}
