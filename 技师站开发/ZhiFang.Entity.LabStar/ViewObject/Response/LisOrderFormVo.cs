using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar.ViewObject.Response
{
    [DataContract]
    [DataDesc(CName = "医嘱单列表VO", ClassCName = "LisOrderFormVo", ShortCode = "LisOrderFormVo", Desc = "医嘱单列表VO")]
    public class LisOrderFormVo
    {
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "主键ID", ShortCode = "Id", Desc = "主键ID", ContextType = SysDic.Number, Length = 8)]
        public long OrderFormID { get; set; }
        [DataMember]
        public string OrderFormNo { get; set; }
        [DataMember]
        public DateTime? OrderTime { get; set; }
        [DataMember]
        public int IsAffirm { get; set; }
        [DataMember]
        public string CName { get; set; }
        [DataMember]
        public string DeptName { get; set; }
        [DataMember]
        public string Bed { get; set; }
        [DataMember]
        public string PatNo { get; set; }
        [DataMember]
        public string ItemName { get; set; }
    }
}
