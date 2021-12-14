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
    [DataDesc(CName = "打包号样本单列表VO", ClassCName = "LisPackNoBarCodeFormVo", ShortCode = "LisPackNoBarCodeFormVo", Desc = "打包号样本单列表VO")]
    public class LisPackNoBarCodeFormVo
    {
        [DataMember]
        public List<LisBarCodeForm> LisBarCodeForm { get; set; }
        [DataMember]
        public int count { get; set; }
        [DataMember]
        public int hasSignForCount { get; set; }
        [DataMember]
        public int notSignForCount { get; set; }
    }
}
