using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
    #region BmsCenSaleDtl

    /// <summary>
    /// BmsCenSaleDtl object for NHibernate mapped table 'BmsCenSaleDtl'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "平台供货明细表", ClassCName = "BmsCenSaleDtl", ShortCode = "BmsCenSaleDtl", Desc = "平台供货明细表")]
    public class BmsCenSaleDtl : BaseEntity
    {
        #region Member Variables

        protected long _pSaleDtlID;
        protected string _saleDtlNo;
        protected string _saleDocNo;
        protected string _prodGoodsNo;
        protected string _prodOrgName;
        protected string _goodsName;
        protected string _goodsUnit;
        protected string _unitMemo;
        protected double _goodsQty;
        protected double _iogoodsQty;
        protected double _price;
        protected double _sumTotal;
        protected double _taxRate;
        protected string _lotNo;
        protected DateTime? _prodDate;
        protected DateTime? _invalidDate;
        protected string _biddingNo;
        protected int _iOFlag;
        protected string _goodsSerial;
        protected string _packSerial;
        protected string _lotSerial;
        protected string _mixSerial;
        protected string _SysLotSerial;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected string _shortCode;
        protected DateTime? _dataUpdateTime;
        protected int _dispOrder;
        protected double _dtlCount;
        protected double _acceptCount;
        protected double _refuseCount;
        protected int _acceptFlag;
        protected int _barCodeMgr;
        protected string _accepterErrorMsg;
        protected string _registerNo;
        protected string _storageType;
        protected string _approveDocNo;
        protected DateTime? _registerInvalidDate;
        protected string _tempRange;
        protected int _deleteFlag;
        protected BmsCenSaleDoc _bmsCenSaleDoc;
        protected CenOrg _prod;
        protected Goods _goods;
        protected long? _reaGoodsID;
        protected string _reaGoodsName;
        protected string _goodsNo;
        protected long? _orderGoodsID;
        protected long? _Status;
        protected string _StatusName;
        protected string _memo;

        #endregion

        #region Constructors

        public BmsCenSaleDtl() { }

        public BmsCenSaleDtl(string saleDtlNo, string saleDocNo, string prodGoodsNo, string prodOrgName, string goodsName, string goodsUnit, string unitMemo, double goodsQty, double iogoodsQty, double price, double sumTotal, double taxRate, string lotNo, DateTime prodDate, DateTime invalidDate, string biddingNo, int iOFlag, string goodsSerial, string packSerial, string lotSerial, string mixSerial, string zX1, string zX2, string zX3, int dispOrder, BmsCenSaleDoc bmsCenSaleDoc, CenOrg prod, Goods goods)
        {
            this._saleDtlNo = saleDtlNo;
            this._saleDocNo = saleDocNo;
            this._prodGoodsNo = prodGoodsNo;
            this._prodOrgName = prodOrgName;
            this._goodsName = goodsName;
            this._goodsUnit = goodsUnit;
            this._unitMemo = unitMemo;
            this._goodsQty = goodsQty;
            this._iogoodsQty = iogoodsQty;
            this._price = price;
            this._sumTotal = sumTotal;
            this._taxRate = taxRate;
            this._lotNo = lotNo;
            this._prodDate = prodDate;
            this._invalidDate = invalidDate;
            this._biddingNo = biddingNo;
            this._iOFlag = iOFlag;
            this._goodsSerial = goodsSerial;
            this._packSerial = packSerial;
            this._lotSerial = lotSerial;
            this._mixSerial = mixSerial;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._dispOrder = dispOrder;
            this._bmsCenSaleDoc = bmsCenSaleDoc;
            this._prod = prod;
            this._goods = goods;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "上级ID", ShortCode = "SJID", Desc = "上级ID", ContextType = SysDic.Number, Length = 4)]
        public virtual long PSaleDtlID
        {
            get { return _pSaleDtlID; }
            set { _pSaleDtlID = value; }
        }

        [DataMember]
        [DataDesc(CName = "供货单号", ShortCode = "SaleDtlNo", Desc = "供货单号", ContextType = SysDic.All, Length = 50)]
        public virtual string SaleDtlNo
        {
            get { return _saleDtlNo; }
            set
            {
                _saleDtlNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "供货单号", ShortCode = "SaleDocNo", Desc = "供货单号", ContextType = SysDic.All, Length = 50)]
        public virtual string SaleDocNo
        {
            get { return _saleDocNo; }
            set
            {
                _saleDocNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "厂商产品编号", ShortCode = "ProdGoodsNo", Desc = "厂商产品编号", ContextType = SysDic.All, Length = 50)]
        public virtual string ProdGoodsNo
        {
            get { return _prodGoodsNo; }
            set
            {
                _prodGoodsNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "厂商名称", ShortCode = "ProdOrgName", Desc = "厂商名称", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdOrgName
        {
            get { return _prodOrgName; }
            set
            {
                _prodOrgName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "货品名称", ShortCode = "GoodsName", Desc = "货品名称", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsName
        {
            get { return _goodsName; }
            set
            {
                _goodsName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "包装单位", ShortCode = "GoodsUnit", Desc = "包装单位", ContextType = SysDic.All, Length = 10)]
        public virtual string GoodsUnit
        {
            get { return _goodsUnit; }
            set
            {
                _goodsUnit = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "包装单位描述", ShortCode = "UnitMemo", Desc = "包装单位描述", ContextType = SysDic.All, Length = 100)]
        public virtual string UnitMemo
        {
            get { return _unitMemo; }
            set
            {
                _unitMemo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "购进数量", ShortCode = "GoodsQty", Desc = "购进数量", ContextType = SysDic.All, Length = 8)]
        public virtual double GoodsQty
        {
            get { return _goodsQty; }
            set { _goodsQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实验室入库数量", ShortCode = "IOGoodsQty", Desc = "购进数量", ContextType = SysDic.All, Length = 8)]
        public virtual double IOGoodsQty
        {
            get { return _iogoodsQty; }
            set { _iogoodsQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "单价", ShortCode = "Price", Desc = "单价", ContextType = SysDic.All, Length = 8)]
        public virtual double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "总计金额", ShortCode = "SumTotal", Desc = "总计金额", ContextType = SysDic.All, Length = 8)]
        public virtual double SumTotal
        {
            get { return _sumTotal; }
            set { _sumTotal = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "税率", ShortCode = "TaxRate", Desc = "税率", ContextType = SysDic.All, Length = 8)]
        public virtual double TaxRate
        {
            get { return _taxRate; }
            set { _taxRate = value; }
        }

        [DataMember]
        [DataDesc(CName = "货品批号", ShortCode = "LotNo", Desc = "货品批号", ContextType = SysDic.All, Length = 100)]
        public virtual string LotNo
        {
            get { return _lotNo; }
            set
            {
                _lotNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "生产日期", ShortCode = "ProdDate", Desc = "生产日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ProdDate
        {
            get { return _prodDate; }
            set { _prodDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "有效期", ShortCode = "InvalidDate", Desc = "有效期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? InvalidDate
        {
            get { return _invalidDate; }
            set { _invalidDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "招标号", ShortCode = "BiddingNo", Desc = "招标号", ContextType = SysDic.All, Length = 100)]
        public virtual string BiddingNo
        {
            get { return _biddingNo; }
            set
            {
                _biddingNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "数据上传标志", ShortCode = "IOFlag", Desc = "数据上传标志", ContextType = SysDic.All, Length = 4)]
        public virtual int IOFlag
        {
            get { return _iOFlag; }
            set { _iOFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "产品条码", ShortCode = "GoodsSerial", Desc = "产品条码", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsSerial
        {
            get { return _goodsSerial; }
            set
            {
                _goodsSerial = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "包装单位条码", ShortCode = "PackSerial", Desc = "包装单位条码", ContextType = SysDic.All, Length = 100)]
        public virtual string PackSerial
        {
            get { return _packSerial; }
            set
            {
                _packSerial = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "批号条码或自定义条码", ShortCode = "LotSerial", Desc = "批号条码或自定义条码", ContextType = SysDic.All, Length = 100)]
        public virtual string LotSerial
        {
            get { return _lotSerial; }
            set
            {
                _lotSerial = value;
            }
        }

        [DataMember]
        [DataDesc(CName = " 混合条码", ShortCode = "MixSerial", Desc = " 混合条码", ContextType = SysDic.All, Length = 100)]
        public virtual string MixSerial
        {
            get { return _mixSerial; }
            set
            {
                _mixSerial = value;
            }
        }

        [DataMember]
        [DataDesc(CName = " 系统内部批号条码", ShortCode = "SysLotSerial", Desc = " 系统内部批号条码", ContextType = SysDic.All, Length = 100)]
        public virtual string SysLotSerial
        {
            get { return _SysLotSerial; }
            set
            {
                _SysLotSerial = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "代码", ShortCode = "ShortCode", Desc = "代码", ContextType = SysDic.All, Length = 100)]
        public virtual string ShortCode
        {
            get { return _shortCode; }
            set
            {
                _shortCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "专项1", ShortCode = "ZX1", Desc = "专项1", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX1
        {
            get { return _zX1; }
            set
            {
                _zX1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "专项2", ShortCode = "ZX2", Desc = "专项2", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX2
        {
            get { return _zX2; }
            set
            {
                _zX2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "专项", ShortCode = "ZX3", Desc = "专项", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX3
        {
            get { return _zX3; }
            set
            {
                _zX3 = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否盒条码", ShortCode = "BarCodeMgr", Desc = "是否盒条码", ContextType = SysDic.All, Length = 4)]
        public virtual int BarCodeMgr
        {
            get { return _barCodeMgr; }
            set { _barCodeMgr = value; }
        }

        [DataMember]
        [DataDesc(CName = "明细总数", ShortCode = "DtlCount", Desc = "明细总数", ContextType = SysDic.All, Length = 4)]
        public virtual double DtlCount
        {
            get { return _dtlCount; }
            set { _dtlCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "验收数量", ShortCode = "AcceptCount", Desc = "验收数量", ContextType = SysDic.All, Length = 4)]
        public virtual double AcceptCount
        {
            get { return _acceptCount; }
            set { _acceptCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "拒收数量", ShortCode = "RefuseCount", Desc = "拒收数量", ContextType = SysDic.All, Length = 4)]
        public virtual double RefuseCount
        {
            get { return _refuseCount; }
            set { _refuseCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "验收标志", ShortCode = "AcceptFlag", Desc = "验收标志", ContextType = SysDic.All, Length = 4)]
        public virtual int AcceptFlag
        {
            get { return _acceptFlag; }
            set { _acceptFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "验收异常信息", ShortCode = "AccepterErrorMsg", Desc = "验收异常信息", ContextType = SysDic.All, Length = 1000)]
        public virtual string AccepterErrorMsg
        {
            get { return _accepterErrorMsg; }
            set { _accepterErrorMsg = value; }
        }

        [DataMember]
        [DataDesc(CName = "注册证编号", ShortCode = "RegisterNo", Desc = "注册证编号", ContextType = SysDic.All, Length = 200)]
        public virtual string RegisterNo
        {
            get { return _registerNo; }
            set { _registerNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "注册证有效期", ShortCode = "RegisterInvalidDate", Desc = "注册证有效期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RegisterInvalidDate
        {
            get { return _registerInvalidDate; }
            set { _registerInvalidDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "温度范围", ShortCode = "TempRange", Desc = "温度范围", ContextType = SysDic.All, Length = 100)]
        public virtual string TempRange
        {
            get { return _tempRange; }
            set { _tempRange = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "删除标记", ShortCode = "DeleteFlag", Desc = "删除标记", ContextType = SysDic.All, Length = 8)]
        public virtual int DeleteFlag
        {
            get { return _deleteFlag; }
            set { _deleteFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "批准文号", ShortCode = "ApproveDocNo", Desc = "批准文号", ContextType = SysDic.All, Length = 200)]
        public virtual string ApproveDocNo
        {
            get { return _approveDocNo; }
            set
            {
                _approveDocNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "储藏条件", ShortCode = "StorageType", Desc = "储藏条件", ContextType = SysDic.All, Length = 200)]
        public virtual string StorageType
        {
            get { return _storageType; }
            set
            {
                _storageType = value;
            }
        }

        //预留属性,接口序列化使用，数据库中不存在此字段
        [DataMember]
        [DataDesc(CName = "产品编号", ShortCode = "GoodsNo", Desc = "产品编号", ContextType = SysDic.All, Length = 50)]
        public virtual string GoodsNo
        {
            get { return _goodsNo; }
            set { _goodsNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "平台供货总单表", ShortCode = "BmsCenSaleDoc", Desc = "平台供货总单表")]
        public virtual BmsCenSaleDoc BmsCenSaleDoc
        {
            get { return _bmsCenSaleDoc; }
            set { _bmsCenSaleDoc = value; }
        }

        [DataMember]
        [DataDesc(CName = "平台厂商", ShortCode = "Prod", Desc = "")]
        public virtual CenOrg Prod
        {
            get { return _prod; }
            set { _prod = value; }
        }

        [DataMember]
        [DataDesc(CName = "平台产品表", ShortCode = "Goods", Desc = "平台产品表")]
        public virtual Goods Goods
        {
            get { return _goods; }
            set { _goods = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "本地货品ID", ShortCode = "ReaGoodsID", Desc = "本地货品ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReaGoodsID
        {
            get { return _reaGoodsID; }
            set { _reaGoodsID = value; }
        }

        [DataMember]
        [DataDesc(CName = "本地货品名称", ShortCode = "ReaGoodsName", Desc = "本地货品名称", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaGoodsName
        {
            get { return _reaGoodsName; }
            set
            {
                _reaGoodsName = value;
            }
        }


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货品机构关系ID", ShortCode = "OrderGoodsID", Desc = "货品机构关系ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrderGoodsID
        {
            get { return _orderGoodsID; }
            set { _orderGoodsID = value; }

        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "单据状态", ShortCode = "Status", Desc = "单据状态", ContextType = SysDic.All, Length = 8)]
        public virtual long? Status
        {
            get { return _Status; }
            set { _Status = value; }

        }

        [DataMember]
        [DataDesc(CName = "单据状态描述", ShortCode = "StatusName", Desc = "单据状态描述", ContextType = SysDic.All, Length = 100)]
        public virtual string StatusName
        {
            get { return _StatusName; }
            set
            {
                _StatusName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
            }
        }
        #endregion
    }
    #endregion
}
