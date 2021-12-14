using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.Model.DownloadDict
{
    [DataContract]
    public class D_BLabSickType: DownloadDictBase
    {
        public D_BLabSickType()
        { }

        [DataMember]
        public string SickTypeID { get; set; }
        [DataMember]
        public string LabSickTypeNo { get; set; }
    }
}
