using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZhiFang.Model.UiModel
{
    //[DataContract]
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
