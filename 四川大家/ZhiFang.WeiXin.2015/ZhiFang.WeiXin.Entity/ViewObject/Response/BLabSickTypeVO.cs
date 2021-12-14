using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{
    [DataContract]
    public class BLabSickTypeVO :BLabSickType
    {
        [DataMember]
        public string isContrast { get; set; }

        [DataMember]
        public string sickTypeId { get; set; }

        [DataMember]
        public string sickTypeCname { get; set; }
    }
}
