using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{
    [DataContract]
    public class BLabDoctorVO : BLabDoctor
    {
        [DataMember]
        public string isContrast { get; set; }

        [DataMember]
        public string doctorId { get; set; }

        [DataMember]
        public string doctorCname { get; set; }
    }
}
