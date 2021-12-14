using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{
    [DataContract]
    public class BTestItemControlVO : BTestItemControl
    {
        [DataMember]
        public virtual BLabTestItem BLabTestItem { get; set; }

        [DataMember]
        public virtual TestItem TestItem { get; set; }

        [DataMember]
        public string BLabTestItemCName { get; set; }

        [DataMember]
        public string TestItemCName { get; set; }
    }
}
