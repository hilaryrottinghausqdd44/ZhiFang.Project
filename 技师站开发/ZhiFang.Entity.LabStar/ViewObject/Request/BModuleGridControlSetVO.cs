using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFang.Entity.LabStar.ViewObject.Request
{
    [DataContract]
    public class BModuleGridControlSetVO
    {
        [DataMember]
        public BModuleGridControlSet BModuleGridControlSet { get; set; }
        [DataMember]
        public string fields { get; set; }
    }
}
