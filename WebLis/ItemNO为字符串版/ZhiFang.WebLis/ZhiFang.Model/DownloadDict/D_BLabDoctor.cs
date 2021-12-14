using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.Model.DownloadDict
{
    [DataContract]
    public class D_BLabDoctor : DownloadDictBase
    {
        public D_BLabDoctor()
        { }
        [DataMember]
        public string DoctorID { get; set; }
        [DataMember]
        public string LabDoctorNo { get; set; }
    }
}
