using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client.ViewObject.Response
{
    /// <summary>
    /// 供应商货品信息VO
    /// </summary>
    //[DataContract]
    public class ReaGoodsCenOrgVO
    {
        public ReaGoodsCenOrgVO() { }
        public virtual string GoodsId { get; set; }
        public virtual string GoodsCName { get; set; }

        public virtual IList<ReaCenOrgVO> ReaCenOrgVOList { get; set; }
    }
    /// <summary>
    /// 供应商货品信息VO:每组货品下的对应的供应商信息
    /// </summary>
    //[DataContract]
    public class ReaCenOrgVO
    {
        public ReaCenOrgVO() { }
        /// <summary>
        /// 供应商与货品关系主键Id
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 供应商Id
        /// </summary>
        public virtual string CenOrgId { get; set; }
        public virtual string CenOrgCName { get; set; }
        public virtual int DispOrder { get; set; }
        public virtual string ReaGoodsNo { get; set; }
        public virtual string ProdGoodsNo { get; set; }
        public virtual string CenOrgGoodsNo { get; set; }
        public virtual string GoodsNo { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? Price { get; set; }
    }
}
