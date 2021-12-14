using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaStoreIn
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
        [DataDesc(CName = "是否盒条码", ShortCode = "BarCodeMgr", Desc = "是否盒条码", ContextType = SysDic.All, Length = 4)]
        public virtual int BarCodeMgr { get; set; }
        [DataMember]
        [DataDesc(CName = "当次入库条码扫码操作集合", ShortCode = "ReaBmsInDtlLinkList", Desc = "当次入库条码扫码操作集合")]
        public virtual IList<ReaGoodsBarcodeOperation> ReaBmsInDtlLinkList { get; set; }
    }
}
