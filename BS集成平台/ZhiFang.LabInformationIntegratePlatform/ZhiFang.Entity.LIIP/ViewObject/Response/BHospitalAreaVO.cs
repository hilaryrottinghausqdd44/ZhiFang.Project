using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFang.Entity.LIIP.ViewObject.Request
{
    [DataContract]
    public class BHospitalEmpLinkVO : BHospitalEmpLink
    {
        [DataMember]
        public string Account { get; set; }


       
    }
}
