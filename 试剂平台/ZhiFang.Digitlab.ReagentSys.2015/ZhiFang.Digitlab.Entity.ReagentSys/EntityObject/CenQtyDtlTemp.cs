using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
    #region CenQtyDtlTemp

    /// <summary>
    /// CenQtyDtlTemp object for NHibernate mapped table 'CenQtyDtlTemp'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "平台临时库存表", ClassCName = "CenQtyDtlTemp", ShortCode = "CenQtyDtlTemp", Desc = "平台临时库存表")]
    public class CenQtyDtlTemp : BaseEntity
    {
        #region Member Variables

        protected string _qtyDtlNo;
        protected string _labName;
        protected string _parentSerialNo;
        protected string _serialNo;
        protected string _companyName;
        protected DateTime? _inTime;
        protected string _inDtlNo;
        protected string _prodGoodsNo;
        protected string _prodOrgName;
        protected string _goodsName;
        protected string _lotNo;
        protected DateTime? _prodDate;
        protected DateTime? _invalidDate;
        protected string _goodsUnit;
        protected string _unitMemo;
        protected double _goodsQty;
        protected double _lowQty;
        protected double _heightQty;
        protected double _price;
        protected double _sumTotal;
        protected double _taxRate;
        protected int _iOFlag;
        protected int _useFlag;
        protected string _testEquipNo;
        protected string _testEquipName;
        protected string _goodsSerial;
        protected string _packSerial;
        protected string _lotSerial;
        protected string _mixSerial;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected long? _testEquipID;
        protected long? _testProdEquipID;
        protected DateTime? _dataUpdateTime;
        protected CenOrg _comp;
        protected CenOrg _lab;
        protected CenOrg _prod;
        protected Goods _goods;

        #endregion

        #region Constructors

        public CenQtyDtlTemp() { }

        public CenQtyDtlTemp(string qtyDtlNo, string labName, string parentSerialNo, string serialNo, string companyName, DateTime inTime, string inDtlNo, string prodGoodsNo, string prodOrgName, string goodsName, string lotNo, DateTime prodDate, DateTime invalidDate, string goodsUnit, string unitMemo, double goodsQty, double lowQty, double heightQty, double price, double sumTotal, double taxRate, int iOFlag, int useFlag, string testEquipNo, string testEquipName, string goodsSerial, string packSerial, string lotSerial, string mixSerial, string zX1, string zX2, string zX3, long testEquipID, long testProdEquipID, CenOrg comp, CenOrg lab, CenOrg prod, Goods goods)
        {
            this._qtyDtlNo = qtyDtlNo;
            this._labName = labName;
            this._parentSerialNo = parentSerialNo;
            this._serialNo = serialNo;
            this._companyName = companyName;
            this._inTime = inTime;
            this._inDtlNo = inDtlNo;
            this._prodGoodsNo = prodGoodsNo;
            this._prodOrgName = prodOrgName;
            this._goodsName = goodsName;
            this._lotNo = lotNo;
            this._prodDate = prodDate;
            this._invalidDate = invalidDate;
            this._goodsUnit = goodsUnit;
            this._unitMemo = unitMemo;
            this._goodsQty = goodsQty;
            this._lowQty = lowQty;
            this._heightQty = heightQty;
            this._price = price;
            this._sumTotal = sumTotal;
            this._taxRate = taxRate;
            this._iOFlag = iOFlag;
            this._useFlag = useFlag;
            this._testEquipNo = testEquipNo;
            this._testEquipName = testEquipName;
            this._goodsSerial = goodsSerial;
            this._packSerial = packSerial;
            this._lotSerial = lotSerial;
            this._mixSerial = mixSerial;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._testEquipID = testEquipID;
            this._testProdEquipID = testProdEquipID;
            this._comp = comp;
            this._lab = lab;
            this._prod = prod;
            this._goods = goods;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "库存批次编号", ShortCode = "QtyDtlNo", Desc = "库存批次编号", ContextType = SysDic.All, Length = 200)]
        public virtual string QtyDtlNo
        {
            get { return _qtyDtlNo; }
            set { _qtyDtlNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "实验室名称", ShortCode = "LabName", Desc = "实验室名称", ContextType = SysDic.All, Length = 100)]
        public virtual string LabName
        {
            get { return _labName; }
            set { _labName = value; }
        }

        [DataMember]
        [DataDesc(CName = "父条码ID", ShortCode = "ParentSerialNo", Desc = "父条码ID", ContextType = SysDic.All, Length = 50)]
        public virtual string ParentSerialNo
        {
            get { return _parentSerialNo; }
            set { _parentSerialNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "条码号", ShortCode = "SerialNo", Desc = "条码号", ContextType = SysDic.All, Length = 50)]
        public virtual string SerialNo
        {
            get { return _serialNo; }
            set { _serialNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "供应商名称", ShortCode = "CompanyName", Desc = "供应商名称", ContextType = SysDic.All, Length = 200)]
        public virtual string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "入库时间", ShortCode = "InTime", Desc = "入库时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? InTime
        {
            get { return _inTime; }
            set { _inTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "入库明细单号", ShortCode = "InDtlNo", Desc = "入库明细单号", ContextType = SysDic.All, Length = 20)]
        public virtual string InDtlNo
        {
            get { return _inDtlNo; }
            set { _inDtlNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "厂商产品编号", ShortCode = "ProdGoodsNo", Desc = "厂商产品编号", ContextType = SysDic.All, Length = 50)]
        public virtual string ProdGoodsNo
        {
            get { return _prodGoodsNo; }
            set { _prodGoodsNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "厂商名称", ShortCode = "ProdOrgName", Desc = "厂商名称", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdOrgName
        {
            get { return _prodOrgName; }
            set { _prodOrgName = value; }
        }

        [DataMember]
        [DataDesc(CName = "货品名称", ShortCode = "GoodsName", Desc = "货品名称", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsName
        {
            get { return _goodsName; }
            set { _goodsName = value; }
        }

        [DataMember]
        [DataDesc(CName = "货品批号", ShortCode = "LotNo", Desc = "货品批号", ContextType = SysDic.All, Length = 100)]
        public virtual string LotNo
        {
            get { return _lotNo; }
            set { _lotNo = value; }
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
        [DataDesc(CName = "包装单位", ShortCode = "GoodsUnit", Desc = "包装单位", ContextType = SysDic.All, Length = 10)]
        public virtual string GoodsUnit
        {
            get { return _goodsUnit; }
            set { _goodsUnit = value; }
        }

        [DataMember]
        [DataDesc(CName = "包装单位描述", ShortCode = "UnitMemo", Desc = "包装单位描述", ContextType = SysDic.All, Length = 100)]
        public virtual string UnitMemo
        {
            get { return _unitMemo; }
            set { _unitMemo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "库存数量", ShortCode = "GoodsQty", Desc = "库存数量", ContextType = SysDic.All, Length = 8)]
        public virtual double GoodsQty
        {
            get { return _goodsQty; }
            set { _goodsQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "低库存报警数量", ShortCode = "LowQty", Desc = "低库存报警数量", ContextType = SysDic.All, Length = 8)]
        public virtual double LowQty
        {
            get { return _lowQty; }
            set { _lowQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "高库存报警数量", ShortCode = "HeightQty", Desc = "高库存报警数量", ContextType = SysDic.All, Length = 8)]
        public virtual double HeightQty
        {
            get { return _heightQty; }
            set { _heightQty = value; }
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
        [DataDesc(CName = "库存总计金额", ShortCode = "SumTotal", Desc = "库存总计金额", ContextType = SysDic.All, Length = 8)]
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
        [DataDesc(CName = "数据上传标志", ShortCode = "IOFlag", Desc = "数据上传标志", ContextType = SysDic.All, Length = 4)]
        public virtual int IOFlag
        {
            get { return _iOFlag; }
            set { _iOFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "使用标志: 1-仪器使用；2-报损；3-回退供应商；4-调账", ShortCode = "UseFlag", Desc = "使用标志: 1-仪器使用；2-报损；3-回退供应商；4-调账", ContextType = SysDic.All, Length = 4)]
        public virtual int UseFlag
        {
            get { return _useFlag; }
            set { _useFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器编号", ShortCode = "TestEquipNo", Desc = "仪器编号", ContextType = SysDic.All, Length = 50)]
        public virtual string TestEquipNo
        {
            get { return _testEquipNo; }
            set { _testEquipNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器名称", ShortCode = "TestEquipName", Desc = "仪器名称", ContextType = SysDic.All, Length = 100)]
        public virtual string TestEquipName
        {
            get { return _testEquipName; }
            set { _testEquipName = value; }
        }

        [DataMember]
        [DataDesc(CName = "产品条码", ShortCode = "GoodsSerial", Desc = "产品条码", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsSerial
        {
            get { return _goodsSerial; }
            set { _goodsSerial = value; }
        }

        [DataMember]
        [DataDesc(CName = "包装条码", ShortCode = "PackSerial", Desc = "包装条码", ContextType = SysDic.All, Length = 100)]
        public virtual string PackSerial
        {
            get { return _packSerial; }
            set { _packSerial = value; }
        }

        [DataMember]
        [DataDesc(CName = "批号条码", ShortCode = "LotSerial", Desc = "批号条码", ContextType = SysDic.All, Length = 100)]
        public virtual string LotSerial
        {
            get { return _lotSerial; }
            set { _lotSerial = value; }
        }

        [DataMember]
        [DataDesc(CName = "混合条码", ShortCode = "MixSerial", Desc = "混合条码", ContextType = SysDic.All, Length = 100)]
        public virtual string MixSerial
        {
            get { return _mixSerial; }
            set { _mixSerial = value; }
        }

        [DataMember]
        [DataDesc(CName = "专项1", ShortCode = "ZX1", Desc = "专项1", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX1
        {
            get { return _zX1; }
            set { _zX1 = value; }
        }

        [DataMember]
        [DataDesc(CName = "专项2", ShortCode = "ZX2", Desc = "专项2", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX2
        {
            get { return _zX2; }
            set { _zX2 = value; }
        }

        [DataMember]
        [DataDesc(CName = "专项3", ShortCode = "ZX3", Desc = "专项3", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX3
        {
            get { return _zX3; }
            set { _zX3 = value; }
        }

        [DataMember]
        [DataDesc(CName = "实验室仪器ID(实验室上传)", ShortCode = "TestEquipID", Desc = "实验室仪器ID(实验室上传)", ContextType = SysDic.All, Length = 4)]
        public virtual long? TestEquipID
        {
            get { return _testEquipID; }
            set { _testEquipID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "厂商仪器ID(平台端维护)", ShortCode = "TestProdEquipID", Desc = "厂商仪器ID(平台端维护)", ContextType = SysDic.All, Length = 8)]
        public virtual long? TestProdEquipID
        {
            get { return _testProdEquipID; }
            set { _testProdEquipID = value; }
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
        [DataDesc(CName = "", ShortCode = "Comp", Desc = "")]
        public virtual CenOrg Comp
        {
            get { return _comp; }
            set { _comp = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Lab", Desc = "")]
        public virtual CenOrg Lab
        {
            get { return _lab; }
            set { _lab = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Prod", Desc = "")]
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


        #endregion
    }
    #endregion
}