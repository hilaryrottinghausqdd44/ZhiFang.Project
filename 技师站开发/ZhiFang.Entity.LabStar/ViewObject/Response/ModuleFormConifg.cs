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

    public class ModuleFormConifg
    {
        [DataMember]
        public BModuleFormList BModuleFormList { get; set; }
        [DataMember]
        public List<BModuleFormControlList> BModuleFormControlList { get; set; }

    }
}
