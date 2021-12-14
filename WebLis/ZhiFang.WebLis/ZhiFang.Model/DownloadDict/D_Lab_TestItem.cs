using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.Model.DownloadDict
{
    [DataContract]
    public class D_Lab_TestItem : DownloadDictBase
    {
        public D_Lab_TestItem()
        { }
        [DataMember]
        public string ItemID { get; set; }
        [DataMember]
        public string ItemNo { get; set; }
        [DataMember]
        public string Color { get; set; }
        [DataMember]
        public int? isCombiItem { get; set; }
        [DataMember]
        public int? IsProfile { get; set; }
        /// <summary>
        /// 体检标记
        /// </summary>		
        [DataMember]
        public int? PhysicalFlag { get; set; }
    }
}
