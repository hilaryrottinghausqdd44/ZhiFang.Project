using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.Model.DownloadDict
{
    [DataContract]
    public class D_BPhysicalExamType : DownloadDictBase
    {
        public D_BPhysicalExamType() { }
        [DataMember]
        public string Id { get; set; }
    }
}
