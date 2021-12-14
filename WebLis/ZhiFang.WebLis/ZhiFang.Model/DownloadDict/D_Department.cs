using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.Model.DownloadDict
{
    [DataContract]
    public class D_Department
    {
        public D_Department()
        { }
        [DataMember]
        public int DeptNo { get; set; }
        [DataMember]
        public int DispOrder { get; set; }
        [DataMember]
        public int Visible { get; set; }
        [DataMember]
        public string CName { get; set; }
        [DataMember]
        public string ShortName { get; set; }
        [DataMember]
        public string ShortCode { get; set; }
        [DataMember]
        public string HisOrderCode { get; set; }
    }
}
