using System;
using System.Runtime.Serialization;

namespace ZhiFang.Entity.LabStar.ViewObject.Request
{
    [DataContract]
    public class LisQCDataVO
    {
        [DataMember]
        public long QCItemId { get; set; }

        [DataMember]
        public string ReportValue { get; set; }

        [DataMember]
        public DateTime ReceiveTime { get; set; }

        [DataMember]
        public bool BUse { get; set; }

        [DataMember]
        public string QCValueMemo { get; set; }

    }
}