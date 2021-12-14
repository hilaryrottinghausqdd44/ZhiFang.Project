using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.Entity.LabStar.ViewObject.Response
{
    public class LBSamplingItemVO : LBSamplingItem
    {
        [DataMember]
        public string ItemCName { get; set; }
    }
}
