using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ZFReaRestful
{
    /// <summary>
    /// 给验收单的验收明细回打标志及数量主验收单VO
    /// </summary>
    [DataContract]
    public class BmsCenSaleDocConfirmVO
    {
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "主键ID", ShortCode = "Id", Desc = "主键ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long Id { get; set; }
        [DataMember]
        [DataDesc(CName = "供货验收明细单VO", ShortCode = "BmsCenSaleDtlConfirmVOList", Desc = "供货验收明细单VO")]
        public virtual IList<BmsCenSaleDtlConfirmVO> BmsCenSaleDtlConfirmVOList { get; set; }
    }
}
