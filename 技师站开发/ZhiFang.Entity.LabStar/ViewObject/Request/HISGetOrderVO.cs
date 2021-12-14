using System;
using System.Runtime.Serialization;

namespace ZhiFang.Entity.LabStar.ViewObject.Request
{
    [DataContract]
    public class HISGetOrderVO
    {
        [DataMember]
        public string InputType { get; set; }

        [DataMember]
        public string InputValue { get; set; }

        [DataMember]
        public string StartDate { get; set; }

        [DataMember]
        public string EndDate { get; set; }

        [DataMember]
        public string SickTypeName { get; set; }
        [DataMember]
        public string HospNo { get; set; }
        [DataMember]
        public string OrgNo { get; set; }
    }
}