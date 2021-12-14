using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.Response
{
    [DataContract]
    [DataDesc(CName = "BmsCenSaleDtlOV", ClassCName = "BmsCenSaleDtlOV", ShortCode = "BmsCenSaleDtlOV", Desc = "BmsCenSaleDtlOV")]
    public class BmsCenSaleDtlOV
    {
        public BmsCenSaleDtlOV() { }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "主键ID", ShortCode = "Id", Desc = "主键ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long Id { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "上级ID", ShortCode = "SJID", Desc = "上级ID", ContextType = SysDic.Number, Length = 4)]
        public virtual long PSaleDtlID { get; set; }
        [DataMember]
        public int BarCodeMgr { get; set; }

        [DataMember]
        public string GoodsName { get; set; }

        [DataMember]
        public string GoodsUnit { get; set; }

        [DataMember]
        public float DtlCount { get; set; }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        //[DataDesc(CName = "有效期", ShortCode = "InvalidDate", Desc = "有效期", ContextType = SysDic.All, Length = 20)]
        public virtual string InvalidDate { get; set; }

        [DataMember]
        public string UnitMemo { get; set; }

        [DataMember]
        public float GoodsQty { get; set; }

        [DataMember]
        [DataDesc(CName = " 混合条码", ShortCode = "MixSerial", Desc = " 混合条码", ContextType = SysDic.All, Length = 100)]
        public virtual string MixSerial { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "单价", ShortCode = "Price", Desc = "单价", ContextType = SysDic.All, Length = 8)]
        public virtual double Price { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "总计金额", ShortCode = "SumTotal", Desc = "总计金额", ContextType = SysDic.All, Length = 8)]
        public virtual double SumTotal { get; set; }

        [DataMember]
        [DataDesc(CName = "货品批号", ShortCode = "LotNo", Desc = "货品批号", ContextType = SysDic.All, Length = 100)]
        public virtual string LotNo { get; set; }

        [DataMember]
        [DataDesc(CName = "明细的同一种试剂的识别值", ShortCode = "PSaleDtlIDStr", Desc = "明细的同一种试剂的识别值")]
        public string PSaleDtlIDStr { get; set; }

        [DataMember]
        [DataDesc(CName = "验收标志(0:待验收,扫码拒收:1,扫码接收:2,已全部验收:3,已全部拒收:4;已全部入库(已验收数+已拒收数):5,某一盒条码已验收:6,某一盒条码已拒收:7)", ShortCode = "ScanCodeMark", Desc = "验收标志(0:待验收,扫码拒收:1,扫码接收:2,已全部验收:3,已全部拒收:4;已全部入库(已验收数+已拒收数):5)")]
        public int ScanCodeMark { get; set; }

        [DataMember]
        [DataDesc(CName = "同一试剂已入库总数(接收+拒收)", ShortCode = "StockSumTotal", Desc = "同一试剂已入库总数(接收+拒收)")]
        public float StockSumTotal { get; set; }
        [DataMember]
        [DataDesc(CName = "已存库的验收数量", ShortCode = "AcceptCounted", Desc = "已存库的验收数量")]
        public float AcceptCounted { get; set; }
        [DataMember]
        [DataDesc(CName = "已存库的拒收数量", ShortCode = "RefuseCounted", Desc = "已存库的拒收数量")]
        public float RefuseCounted { get; set; }
        [DataMember]
        [DataDesc(CName = "当次扫码验收数量", ShortCode = "AcceptCount", Desc = "当次扫码验收数量")]
        public float AcceptCount { get; set; }
        [DataMember]
        [DataDesc(CName = "当次扫码拒收数量", ShortCode = "RefuseCount", Desc = "当次扫码拒收数量")]
        public float RefuseCount { get; set; }
        [DataMember]
        [DataDesc(CName = "验收备注", ShortCode = "AcceptMemo", Desc = "验收备注", ContextType = SysDic.All, Length = 1000)]
        public virtual string AcceptMemo { get; set; }
    }
}
