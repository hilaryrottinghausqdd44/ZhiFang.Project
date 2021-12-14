using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
    #region Goods

    /// <summary>
    /// Goods object for NHibernate mapped table 'Goods'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "产品表", ClassCName = "Goods", ShortCode = "Goods", Desc = "")]
    public class Goods : BaseEntity
    {
        #region Member Variables

        protected string _goodsNo;
        protected string _compGoodsNo;
        protected string _prodGoodsNo;
        protected string _cName;
        protected string _eName;
        protected string _shortCode;
        protected string _goodsClass;
        protected string _goodsClassType;
        protected string _unitName;
        protected string _unitMemo;
        protected string _storageType;
        protected string _goodsDesc;
        protected string _approveDocNo;
        protected string _standard;
        protected string _registNo;
        protected DateTime? _registDate;
        protected DateTime? _registNoInvalidDate;
        protected string _purpose;
        protected string _constitute;
        protected byte[] _license;
        protected byte[] _goodsPicture;
        protected int _dispOrder;
        protected int _visible;
        protected double _price;
        protected string _biddingNo;
        protected string _prodEara;
        protected string _prodOrgName;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected string _equipment;
        protected string _suitableType;
        protected int _cenOrgConfirm;
        protected int _compConfirm;
        protected int _barCodeMgr;
        protected int _isPrintBarCode;
        protected int _isRegister;
        protected DateTime? _dataUpdateTime;
        protected CenOrg _cenOrg;
        protected CenOrg _comp;
        protected CenOrg _prod;

        #endregion

        #region Constructors

        public Goods() { }

        public Goods(string goodsNo, string compGoodsNo, string prodGoodsNo, string cName, string eName, string shortCode, string goodsClass, string goodsClassType, string unitName, string unitMemo, string storageType, string goodsDesc, string approveDocNo, string standard, string registNo, DateTime registDate, DateTime registNoInvalidDate, string purpose, string constitute, byte[] license, byte[] goodsPicture, int dispOrder, int visible, double price, string biddingNo, string zX1, string zX2, string zX3, CenOrg cenOrg, CenOrg comp, CenOrg prod)
        {
            this._goodsNo = goodsNo;
            this._compGoodsNo = compGoodsNo;
            this._prodGoodsNo = prodGoodsNo;
            this._cName = cName;
            this._eName = eName;
            this._shortCode = shortCode;
            this._goodsClass = goodsClass;
            this._goodsClassType = goodsClassType;
            this._unitName = unitName;
            this._unitMemo = unitMemo;
            this._storageType = storageType;
            this._goodsDesc = goodsDesc;
            this._approveDocNo = approveDocNo;
            this._standard = standard;
            this._registNo = registNo;
            this._registDate = registDate;
            this._registNoInvalidDate = registNoInvalidDate;
            this._purpose = purpose;
            this._constitute = constitute;
            this._license = license;
            this._goodsPicture = goodsPicture;
            this._dispOrder = dispOrder;
            this._visible = visible;
            this._price = price;
            this._biddingNo = biddingNo;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._cenOrg = cenOrg;
            this._comp = comp;
            this._prod = prod;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "产品编号", ShortCode = "GoodsNo", Desc = "产品编号", ContextType = SysDic.All, Length = 50)]
        public virtual string GoodsNo
        {
            get { return _goodsNo; }
            set
            {
                _goodsNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "供应商产品编号", ShortCode = "CompGoodsNo", Desc = "供应商产品编号", ContextType = SysDic.All, Length = 50)]
        public virtual string CompGoodsNo
        {
            get { return _compGoodsNo; }
            set
            {
                _compGoodsNo = value;
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
        [DataDesc(CName = "中文名", ShortCode = "CName", Desc = "中文名", ContextType = SysDic.All, Length = 200)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "英文名", ShortCode = "EName", Desc = "英文名", ContextType = SysDic.All, Length = 200)]
        public virtual string EName
        {
            get { return _eName; }
            set
            {
                _eName = value;
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
        [DataDesc(CName = "一级分类", ShortCode = "GoodsClass", Desc = "一级分类", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsClass
        {
            get { return _goodsClass; }
            set
            {
                _goodsClass = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "二级分类", ShortCode = "GoodsClassType", Desc = "二级分类", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsClassType
        {
            get { return _goodsClassType; }
            set
            {
                _goodsClassType = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "单位", ShortCode = "UnitName", Desc = "单位", ContextType = SysDic.All, Length = 10)]
        public virtual string UnitName
        {
            get { return _unitName; }
            set
            {
                _unitName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "单位描述", ShortCode = "UnitMemo", Desc = "单位描述", ContextType = SysDic.All, Length = 100)]
        public virtual string UnitMemo
        {
            get { return _unitMemo; }
            set
            {
                _unitMemo = value;
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

        [DataMember]
        [DataDesc(CName = "货品描述", ShortCode = "GoodsDesc", Desc = "货品描述", ContextType = SysDic.All, Length = 500)]
        public virtual string GoodsDesc
        {
            get { return _goodsDesc; }
            set
            {
                _goodsDesc = value;
            }
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
        [DataDesc(CName = "国标", ShortCode = "Standard", Desc = "国标", ContextType = SysDic.All, Length = 200)]
        public virtual string Standard
        {
            get { return _standard; }
            set
            {
                _standard = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "注册号", ShortCode = "RegistNo", Desc = "注册号", ContextType = SysDic.All, Length = 200)]
        public virtual string RegistNo
        {
            get { return _registNo; }
            set {
            
                _registNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "注册日期", ShortCode = "RegistDate", Desc = "注册日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RegistDate
        {
            get { return _registDate; }
            set { _registDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "注册证有效期", ShortCode = "RegistNoInvalidDate", Desc = "注册证有效期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RegistNoInvalidDate
        {
            get { return _registNoInvalidDate; }
            set { _registNoInvalidDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "用途", ShortCode = "Purpose", Desc = "用途", ContextType = SysDic.All, Length = 500)]
        public virtual string Purpose
        {
            get { return _purpose; }
            set
            {
       
                _purpose = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "结构组成", ShortCode = "Constitute", Desc = "结构组成", ContextType = SysDic.All, Length = 500)]
        public virtual string Constitute
        {
            get { return _constitute; }
            set
            {
                _constitute = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "证书内容", ShortCode = "License", Desc = "证书内容", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] License
        {
            get { return _license; }
            set { _license = value; }
        }

        [DataMember]
        [DataDesc(CName = "外观照片", ShortCode = "GoodsPicture", Desc = "外观照片", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] GoodsPicture
        {
            get { return _goodsPicture; }
            set { _goodsPicture = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
        {
            get { return _visible; }
            set { _visible = value; }
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
        [DataDesc(CName = "是否盒条码", ShortCode = "BarCodeMgr", Desc = "是否盒条码", ContextType = SysDic.All, Length = 4)]
        public virtual int BarCodeMgr
        {
            get { return _barCodeMgr; }
            set { _barCodeMgr = value; }
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
        [DataDesc(CName = "产地", ShortCode = "ProdEara", Desc = "产地", ContextType = SysDic.All, Length = 50)]
        public virtual string ProdEara
        {
            get { return _prodEara; }
            set
            {
                _prodEara = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "生成厂家", ShortCode = "ProdOrgName", Desc = "生成厂家", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdOrgName
        {
            get { return _prodOrgName; }
            set
            {
                _prodOrgName = value;
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
        [DataDesc(CName = "专项3", ShortCode = "ZX3", Desc = "专项3", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX3
        {
            get { return _zX3; }
            set
            {
                _zX3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "仪器", ShortCode = "Equipment", Desc = "仪器", ContextType = SysDic.All, Length = 100)]
        public virtual string Equipment
        {
            get { return _equipment; }
            set
            {
                _equipment = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "适用机型", ShortCode = "SuitableType", Desc = "适用机型", ContextType = SysDic.All, Length = 100)]
        public virtual string SuitableType
        {
            get { return _suitableType; }
            set
            {
                _suitableType = value;
            }
        }
        
        [DataMember]
        [DataDesc(CName = "本机构确认", ShortCode = "CenOrgConfirm", Desc = "本机构确认", ContextType = SysDic.All, Length = 4)]
        public virtual int CenOrgConfirm
        {
            get { return _cenOrgConfirm; }
            set { _cenOrgConfirm = value; }
        }

        [DataMember]
        [DataDesc(CName = "供应商确认", ShortCode = "CompConfirm", Desc = "供应商确认", ContextType = SysDic.All, Length = 4)]
        public virtual int CompConfirm
        {
            get { return _compConfirm; }
            set { _compConfirm = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否有注册证", ShortCode = "IsRegister", Desc = "是否有注册证", ContextType = SysDic.All, Length = 4)]
        public virtual int IsRegister
        {
            get { return _isRegister; }
            set { _isRegister = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否打印条码", ShortCode = "IsPrintBarCode", Desc = "是否打印条码", ContextType = SysDic.All, Length = 4)]
        public virtual int IsPrintBarCode
        {
            get { return _isPrintBarCode; }
            set { _isPrintBarCode = value; }
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
        [DataDesc(CName = "机构", ShortCode = "CenOrg", Desc = "")]
        public virtual CenOrg CenOrg
        {
            get { return _cenOrg; }
            set { _cenOrg = value; }
        }

        [DataMember]
        [DataDesc(CName = "供应商", ShortCode = "Comp", Desc = "")]
        public virtual CenOrg Comp
        {
            get { return _comp; }
            set { _comp = value; }
        }

        [DataMember]
        [DataDesc(CName = "厂商", ShortCode = "Prod", Desc = "")]
        public virtual CenOrg Prod
        {
            get { return _prod; }
            set { _prod = value; }
        }

        //区别平台产品和其他厂商产品
        [DataMember]
        [DataDesc(CName = "产品来源", ShortCode = "GoodsSource", Desc = "产品来源", ContextType = SysDic.All, Length = 4)]
        public virtual int GoodsSource{ get;  set; }

        #endregion
    }
    #endregion
}