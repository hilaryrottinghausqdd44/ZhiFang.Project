using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar.ViewObject.Response
{
    [DataContract]

    public class ModuleGridConifg
    {
        [DataMember]
        public BModuleGridList BModuleGridList { get; set; }
        [DataMember]
        public List<BModuleGridControlList> BModuleGridControlList { get; set; }

    }
}
