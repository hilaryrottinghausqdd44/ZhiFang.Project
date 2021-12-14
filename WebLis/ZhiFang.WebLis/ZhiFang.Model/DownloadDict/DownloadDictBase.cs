using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Tools;

namespace ZhiFang.Model.DownloadDict
{
    [DataContract]
    public class DownloadDictBase
    {
        public DownloadDictBase()
        { }
        [DataMember]
        public string CName { get; set; }
        [DataMember]
        public string ShortCode { get; set; }
        [DataMember]
        public int? DispOrder { get; set; }
    }
}
