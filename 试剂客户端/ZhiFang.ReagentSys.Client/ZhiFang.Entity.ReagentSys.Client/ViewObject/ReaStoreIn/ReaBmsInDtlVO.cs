using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaStoreIn
{
    /// <summary>
    /// 客户端入库明细封装定制VO
    /// </summary>
    [DataContract]
    [DataDesc(CName = "客户端入库明细封装定制VO", ClassCName = "ReaBmsInDtlVO", ShortCode = "ReaBmsInDtlVO", Desc = "客户端入库明细封装定制VO")]
    public class ReaBmsInDtlVO : BaseEntity
    {
        public ReaBmsInDtlVO() { }

        [DataMember]
        [DataDesc(CName = "入库明细表", ShortCode = "ReaBmsInDtl", Desc = "入库明细表")]
        public virtual ReaBmsInDtl ReaBmsInDtl { get; set; }
        [DataMember]
        [DataDesc(CName = "条码类型字段0无条码1盒条码2批条码", ShortCode = "BarCodeType", Desc = "条码类型字段0无条码1盒条码2批条码", ContextType = SysDic.All, Length = 4)]
        public virtual int BarCodeType { get; set; }
        [DataMember]
        [DataDesc(CName = "新增的入库条码扫码操作集合", ShortCode = "ReaBmsInDtlLinkList", Desc = "新增的入库条码扫码操作集合")]
        public virtual IList<ReaGoodsBarcodeOperation> ReaBmsInDtlLinkList { get; set; }

        [DataMember]
        [DataDesc(CName = "入库已扫码条码操作集合", ShortCode = "EditReaBmsInDtlLinkList", Desc = "入库已扫码条码操作集合")]
        public virtual IList<ReaGoodsBarcodeOperation> EditReaBmsInDtlLinkList { get; set; }

        [DataMember]
        [DataDesc(CName = "当次入库条码扫码操作集合Str", ShortCode = "CurReaBmsInDtlLinkListStr", Desc = "当次入库条码扫码操作集合Str")]
        public virtual string CurReaBmsInDtlLinkListStr { get; set; }
    }
}
