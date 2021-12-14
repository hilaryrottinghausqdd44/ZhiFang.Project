using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZhiFang.Model.UiModel
{
    //[DataContract]
    public class SampleItemInfo
    {
        public string CName { get; set; }
        public string ItemNo { get; set; }
        public string SampleTypeName { get; set; }
        public string CheckMethodName { get; set; }
        public string Price { get; set; }
        public string ItemUnit { get; set; }
    }
}
