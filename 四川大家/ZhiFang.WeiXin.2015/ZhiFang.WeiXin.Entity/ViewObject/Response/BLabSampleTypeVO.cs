using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{
    [DataContract]
    public class BLabSampleTypeVO : BLabSampleType
    {
        [DataMember]
        public string isContrast { get; set; }

        [DataMember]
        public string sampleTypeId { get; set; }

        [DataMember]
        public string sampleTypeCname { get; set; }
    }
}
