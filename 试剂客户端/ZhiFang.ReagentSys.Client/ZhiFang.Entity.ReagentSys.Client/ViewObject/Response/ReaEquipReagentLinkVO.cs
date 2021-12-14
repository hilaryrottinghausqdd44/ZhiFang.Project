using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    /// <summary>
    /// 仪器试剂信息VO,按试剂分组
    /// </summary>
    public class ReaEquipReagentLinkVO
    {
        public ReaEquipReagentLinkVO() { }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual long GoodsID { get; set; }
        public virtual string GoodsCName { get; set; }
        public virtual IList<ReaTestEquipVO> ReaTestEquipVOList { get; set; }
    }
    public class ReaTestEquipVO
    {
        public ReaTestEquipVO()
        { }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual long TestEquipID { get; set; }
        public virtual int DispOrder { get; set; }
        public virtual string TestEquipName { get; set; }
    }
}
