using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaConfirm
{
    /// <summary>
    /// 客户端验收明细定制VO
    /// </summary>
    [DataContract]
    [DataDesc(CName = "客户端验收明细定制VO", ClassCName = "BmsCenSaleDtlConfirmVO", ShortCode = "BmsCenSaleDtlConfirmVO", Desc = "客户端验收明细定制VO")]
    public class BmsCenSaleDtlConfirmVO : BaseEntity
    {
        public BmsCenSaleDtlConfirmVO() { }

        [DataMember]
        [DataDesc(CName = "当次验货明细条码关系集合(验收新增/编辑封装用)", ShortCode = "ReaGoodsBarcodeOperationList", Desc = "当次验货明细条码关系集合")]
        public virtual IList<ReaGoodsBarcodeOperation> ReaGoodsBarcodeOperationList { get; set; }

        [DataMember]
        [DataDesc(CName = "货品中文简称", ShortCode = "ReaGoodsSName", Desc = "货品中文简称", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaGoodsSName { get; set; }
        [DataMember]
        [DataDesc(CName = "货品英文名", ShortCode = "ReaGoodsEName", Desc = "货品英文名", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaGoodsEName { get; set; }
        [DataMember]
        [DataDesc(CName = "已接收数", ShortCode = "AcceptCount", Desc = "已接收数", ContextType = SysDic.All)]
        public virtual float ReceivedCount { get; set; }
        [DataMember]
        [DataDesc(CName = "已拒收数", ShortCode = "RejectedCount", Desc = "已拒收数", ContextType = SysDic.All)]
        public virtual float RejectedCount { get; set; }
        #region 订单验收
        [DataMember]
        [DataDesc(CName = "订单购进数量", ShortCode = "OrderGoodsQty", Desc = "订单购进数量", ContextType = SysDic.All)]
        public virtual float OrderGoodsQty { get; set; }
        [DataMember]
        [DataDesc(CName = "(订单验收)可验收数", ShortCode = "ConfirmCount", Desc = "可验收数", ContextType = SysDic.All)]
        public virtual float ConfirmCount { get; set; }
        #endregion
        [DataMember]
        [DataDesc(CName = "供货验收明细单信息", ShortCode = "BmsCenSaleDtlConfirm", Desc = "供货验收明细单信息")]
        public virtual BmsCenSaleDtlConfirm BmsCenSaleDtlConfirm { get; set; }

        [DataMember]
        [DataDesc(CName = "验货已扫码记录集合(封装ReaGoodsBarcodeOperationVO)", ShortCode = "ReaBmsCenSaleDtlConfirmLinkVOListStr", Desc = "验货已扫码记录集合", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string ReaBmsCenSaleDtlConfirmLinkVOListStr { get; set; }

        [DataMember]
        [DataDesc(CName = "入库已扫码记录集合(封装ReaGoodsBarcodeOperationVO)", ShortCode = "ReaBmsInDtlLinkListStr", Desc = "入库已扫码记录集合", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string ReaBmsInDtlLinkListStr { get; set; }

        //[DataMember]
        //[DataDesc(CName = "入库明细集合Str", ShortCode = "ReaBmsInDtlListStr", Desc = "入库明细集合Str", ContextType = SysDic.All, Length = Int32.MaxValue)]
        //public virtual string ReaBmsInDtlListStr { get; set; }

        [DataMember]
        [DataDesc(CName = "当次扫码记录集合", ShortCode = "CurReaGoodsScanCodeList", Desc = "当次扫码记录集合", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string CurReaGoodsScanCodeList { get; set; }
    }
}
