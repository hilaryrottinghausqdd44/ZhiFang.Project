using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaBmsQtyMonthBalanceDtl

    /// <summary>
    /// ReaBmsQtyMonthBalanceDtl object for NHibernate mapped table 'Rea_BmsQtyMonthBalanceDtl'.
    /// ReaBmsQtyMonthBalanceDtlֻ������ת��ϸʵ��ʹ��,���ݲ����浽���ݿ��Rea_BmsQtyMonthBalanceDtl
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaBmsQtyMonthBalanceDtl", ShortCode = "ReaBmsQtyMonthBalanceDtl", Desc = "")]
    public class ReaBmsQtyMonthBalanceDtl : BaseEntity
    {
        #region Member Variables

        protected long? _qtyMonthBalanceDocID;
        protected long? _reaCompanyID;
        protected string _companyName;
        protected long? _orgID;
        protected string _reaServerCompCode;
        protected string _reaGoodsNo;
        protected long? _compGoodsLinkID;
        protected long? _goodsID;
        protected string _goodsName;
        protected string _lotNo;
        protected DateTime? _prodDate;
        protected string _registerNo;
        protected DateTime? _invalidDate;
        protected DateTime? _invalidWarningDate;
        protected long? _goodsUnitID;
        protected string _goodsUnit;
        protected string _unitMemo;
        protected double? _price;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected string _memo;
        protected int _dispOrder;
        protected bool _visible;
        protected long _createrID;
        protected string _createrName;
        protected DateTime _dataUpdateTime;

        protected double? _preMonthQty;
        protected double? _preMonthQtyPrice;
        protected double? _inQty;
        protected double? _inQtyPrice;
        protected double? _availabilityQty;
        protected double? _availabilityPrice;
        protected double? _comfirmInQty;
        protected double? _comfirmInPrice;
        protected double? _transferInQty;
        protected double? _transferInPrice;
        protected double? _outOfInQty;
        protected double? _outOfInPrice;
        protected double? _surplusInQty;
        protected double? _surplusInPrice;

        protected double? _equipQty;
        protected double? _equipPrice;
        protected double? _returnQty;
        protected double? _returnPrice;
        protected double? _transferOutQty;
        protected double? _transferOutPrice;
        protected double? _lossQty;
        protected double? _lossQtyPrice;
        protected double? _diskLossQty;
        protected double? _diskLossQtyPrice;
        protected double? _adjustmentOutQty;
        protected double? _adjustmentOutQtyPrice;

        protected double? _monthQty;
        protected double? _monthQtyPrice;
        protected double? _calcGoodsQty;
        protected double? _calcQtyPrice;
        #endregion

        #region Constructors

        public ReaBmsQtyMonthBalanceDtl() { }

        public ReaBmsQtyMonthBalanceDtl(long labID, long qtyMonthBalanceDocID, long reaCompanyID, string companyName, long orgID, string reaServerCompCode, string goodsNo, long _compGoodsLinkID, long goodsID, string goodsName, string lotNo, DateTime prodDate, string registerNo, DateTime invalidDate, DateTime invalidWarningDate, long goodsUnitID, string goodsUnit, string unitMemo, double price, double preMonthQty, double preMonthQtyPrice, double inQty, double inQtyPrice, double equipQty, double equipPrice, double returnQty, double returnPrice, double monthQty, double monthQtyPrice, double lossQty, double lossQtyPrice, double adjustmentOutQty, double adjustmentOutQtyPrice, string zX1, string zX2, string zX3, string memo, int dispOrder, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._qtyMonthBalanceDocID = qtyMonthBalanceDocID;
            this._reaCompanyID = reaCompanyID;
            this._companyName = companyName;
            this._orgID = orgID;
            this._reaServerCompCode = reaServerCompCode;
            this._reaGoodsNo = goodsNo;
            this._compGoodsLinkID = _compGoodsLinkID;
            this._goodsID = goodsID;
            this._goodsName = goodsName;
            this._lotNo = lotNo;
            this._prodDate = prodDate;
            this._registerNo = registerNo;
            this._invalidDate = invalidDate;
            this._invalidWarningDate = invalidWarningDate;
            this._goodsUnitID = goodsUnitID;
            this._goodsUnit = goodsUnit;
            this._unitMemo = unitMemo;
            this._price = price;
            this._preMonthQty = preMonthQty;
            this._preMonthQtyPrice = preMonthQtyPrice;
            this._inQty = inQty;
            this._inQtyPrice = inQtyPrice;
            this._equipQty = equipQty;
            this._equipPrice = equipPrice;
            this._returnQty = returnQty;
            this._returnPrice = returnPrice;
            this._monthQty = monthQty;
            this._monthQtyPrice = monthQtyPrice;
            this._lossQty = lossQty;
            this._lossQtyPrice = lossQtyPrice;
            this._adjustmentOutQty = adjustmentOutQty;
            this._adjustmentOutQtyPrice = adjustmentOutQtyPrice;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._memo = memo;
            this._dispOrder = dispOrder;
            this._visible = visible;
            this._createrID = createrID;
            this._createrName = createrName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "����ת��ID", ShortCode = "QtyMonthBalanceDocID", Desc = "����ת��ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? QtyMonthBalanceDocID
        {
            get { return _qtyMonthBalanceDocID; }
            set { _qtyMonthBalanceDocID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "������ID", ShortCode = "ReaCompanyID", Desc = "������ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReaCompanyID
        {
            get { return _reaCompanyID; }
            set { _reaCompanyID = value; }
        }

        [DataMember]
        [DataDesc(CName = "����������", ShortCode = "CompanyName", Desc = "����������", ContextType = SysDic.All, Length = 100)]
        public virtual string CompanyName
        {
            get { return _companyName; }
            set
            {
                _companyName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OrgID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrgID
        {
            get { return _orgID; }
            set { _orgID = value; }
        }

        [DataMember]
        [DataDesc(CName = "������ƽ̨����", ShortCode = "ReaServerCompCode", Desc = "������ƽ̨����", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaServerCompCode
        {
            get { return _reaServerCompCode; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ReaServerCompCode", value, value.ToString());
                _reaServerCompCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "��Ʒ�ڲ�����", ShortCode = "ReaGoodsNo", Desc = "��Ʒ�ڲ�����", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaGoodsNo
        {
            get { return _reaGoodsNo; }
            set
            {
                _reaGoodsNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "�����̻�Ʒ��ϵID", ShortCode = "CompGoodsLinkID", Desc = "�����̻�Ʒ��ϵID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CompGoodsLinkID
        {
            get { return _compGoodsLinkID; }
            set { _compGoodsLinkID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GoodsID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsID
        {
            get { return _goodsID; }
            set { _goodsID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GoodsName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsName
        {
            get { return _goodsName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for GoodsName", value, value.ToString());
                _goodsName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "��Ʒ����", ShortCode = "LotNo", Desc = "��Ʒ����", ContextType = SysDic.All, Length = 100)]
        public virtual string LotNo
        {
            get { return _lotNo; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for LotNo", value, value.ToString());
                _lotNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "��������", ShortCode = "ProdDate", Desc = "��������", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ProdDate
        {
            get { return _prodDate; }
            set { _prodDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RegisterNo", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string RegisterNo
        {
            get { return _registerNo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for RegisterNo", value, value.ToString());
                _registerNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "��Ч��", ShortCode = "InvalidDate", Desc = "��Ч��", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? InvalidDate
        {
            get { return _invalidDate; }
            set { _invalidDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "InvalidWarningDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? InvalidWarningDate
        {
            get { return _invalidWarningDate; }
            set { _invalidWarningDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GoodsUnitID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsUnitID
        {
            get { return _goodsUnitID; }
            set { _goodsUnitID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GoodsUnit", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsUnit
        {
            get { return _goodsUnit; }
            set
            {
                _goodsUnit = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UnitMemo", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string UnitMemo
        {
            get { return _unitMemo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for UnitMemo", value, value.ToString());
                _unitMemo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Price", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? Price
        {
            get { return _price; }
            set { _price = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "��ʼ�����", ShortCode = "PreMonthQty", Desc = "��ʼ�����", ContextType = SysDic.All, Length = 8)]
        public virtual double? PreMonthQty
        {
            get { return _preMonthQty; }
            set { _preMonthQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "��ʼ�����", ShortCode = "PreMonthQtyPrice", Desc = "��ʼ�����", ContextType = SysDic.All, Length = 8)]
        public virtual double? PreMonthQtyPrice
        {
            get { return _preMonthQtyPrice; }
            set { _preMonthQtyPrice = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "�������", ShortCode = "InQty", Desc = "�������", ContextType = SysDic.All, Length = 8)]
        public virtual double? InQty
        {
            get { return _inQty; }
            set { _inQty = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "����ܽ��", ShortCode = "InQtyPrice", Desc = "����ܽ��", ContextType = SysDic.All, Length = 8)]
        public virtual double? InQtyPrice
        {
            get { return _inQtyPrice; }
            set { _inQtyPrice = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "����ʼ����", ShortCode = "AvailabilityQty", Desc = "����ʼ����", ContextType = SysDic.All, Length = 8)]
        public virtual double? AvailabilityQty
        {
            get { return _availabilityQty; }
            set { _availabilityQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "����ʼ�����", ShortCode = "AvailabilityPrice", Desc = "����ʼ�����", ContextType = SysDic.All, Length = 8)]
        public virtual double? AvailabilityPrice
        {
            get { return _availabilityPrice; }
            set { _availabilityPrice = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "��������", ShortCode = "ComfirmInQty", Desc = "��������", ContextType = SysDic.All, Length = 8)]
        public virtual double? ComfirmInQty
        {
            get { return _comfirmInQty; }
            set { _comfirmInQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "��������", ShortCode = "ComfirmInPrice", Desc = "��������", ContextType = SysDic.All, Length = 8)]
        public virtual double? ComfirmInPrice
        {
            get { return _comfirmInPrice; }
            set { _comfirmInPrice = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "�ƿ������", ShortCode = "TransferOutQty", Desc = "�ƿ������", ContextType = SysDic.All, Length = 8)]
        public virtual double? TransferInQty
        {
            get { return _transferInQty; }
            set { _transferInQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "�ƿ������", ShortCode = "TransferInPrice", Desc = "�ƿ������", ContextType = SysDic.All, Length = 8)]
        public virtual double? TransferInPrice
        {
            get { return _transferInPrice; }
            set { _transferInPrice = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "�˿������", ShortCode = "OutOfInQty", Desc = "�˿������", ContextType = SysDic.All, Length = 8)]
        public virtual double? OutOfInQty
        {
            get { return _outOfInQty; }
            set { _outOfInQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "�˿������", ShortCode = "OutOfInPrice", Desc = "�˿������", ContextType = SysDic.All, Length = 8)]
        public virtual double? OutOfInPrice
        {
            get { return _outOfInPrice; }
            set { _outOfInPrice = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "��ӯ�����", ShortCode = "SurplusInQty", Desc = "��ӯ�����", ContextType = SysDic.All, Length = 8)]
        public virtual double? SurplusInQty
        {
            get { return _surplusInQty; }
            set { _surplusInQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "��ӯ�����", ShortCode = "SurplusInPrice", Desc = "��ӯ�����", ContextType = SysDic.All, Length = 8)]
        public virtual double? SurplusInPrice
        {
            get { return _surplusInPrice; }
            set { _surplusInPrice = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "����ʹ����", ShortCode = "EquipQty", Desc = "����ʹ����", ContextType = SysDic.All, Length = 8)]
        public virtual double? EquipQty
        {
            get { return _equipQty; }
            set { _equipQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "����ʹ�ý��", ShortCode = "EquipPrice", Desc = "����ʹ�ý��", ContextType = SysDic.All, Length = 8)]
        public virtual double? EquipPrice
        {
            get { return _equipPrice; }
            set { _equipPrice = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "�˹�������", ShortCode = "ReturnQty", Desc = "�˹�������", ContextType = SysDic.All, Length = 8)]
        public virtual double? ReturnQty
        {
            get { return _returnQty; }
            set { _returnQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "�˹����̽��", ShortCode = "ReturnPrice", Desc = "�˹����̽��", ContextType = SysDic.All, Length = 8)]
        public virtual double? ReturnPrice
        {
            get { return _returnPrice; }
            set { _returnPrice = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "���������", ShortCode = "LossQty", Desc = "���������", ContextType = SysDic.All, Length = 8)]
        public virtual double? LossQty
        {
            get { return _lossQty; }
            set { _lossQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "���������", ShortCode = "LossQtyPrice", Desc = "���������", ContextType = SysDic.All, Length = 8)]
        public virtual double? LossQtyPrice
        {
            get { return _lossQtyPrice; }
            set { _lossQtyPrice = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "�̿�������", ShortCode = "DiskLossQty", Desc = "�̿�������", ContextType = SysDic.All, Length = 8)]
        public virtual double? DiskLossQty
        {
            get { return _diskLossQty; }
            set { _diskLossQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "�̿�������", ShortCode = "DiskLossQtyPrice", Desc = "�̿�������", ContextType = SysDic.All, Length = 8)]
        public virtual double? DiskLossQtyPrice
        {
            get { return _diskLossQtyPrice; }
            set { _diskLossQtyPrice = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "���˳�����", ShortCode = "AdjustmentOutQty", Desc = "���˳�����", ContextType = SysDic.All, Length = 8)]
        public virtual double? AdjustmentOutQty
        {
            get { return _adjustmentOutQty; }
            set { _adjustmentOutQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "���˳�����", ShortCode = "AdjustmentOutQtyPrice", Desc = "���˳�����", ContextType = SysDic.All, Length = 8)]
        public virtual double? AdjustmentOutQtyPrice
        {
            get { return _adjustmentOutQtyPrice; }
            set { _adjustmentOutQtyPrice = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "�ƿ������", ShortCode = "TransferOutQty", Desc = "�ƿ������", ContextType = SysDic.All, Length = 8)]
        public virtual double? TransferOutQty
        {
            get { return _transferOutQty; }
            set { _transferOutQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "�ƿ������", ShortCode = "TransferOutPrice", Desc = "�ƿ������", ContextType = SysDic.All, Length = 8)]
        public virtual double? TransferOutPrice
        {
            get { return _transferOutPrice; }
            set { _transferOutPrice = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "ʣ������", ShortCode = "MonthQty", Desc = "ʣ������", ContextType = SysDic.All, Length = 8)]
        public virtual double? MonthQty
        {
            get { return _monthQty; }
            set { _monthQty = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "��������", ShortCode = "CalcGoodsQty", Desc = "��������", ContextType = SysDic.All, Length = 8)]
        public virtual double? CalcGoodsQty
        {
            get { return _calcGoodsQty; }
            set { _calcGoodsQty = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "��������", ShortCode = "CalcQtyPrice", Desc = "��������", ContextType = SysDic.All, Length = 8)]
        public virtual double? CalcQtyPrice
        {
            get { return _calcQtyPrice; }
            set { _calcQtyPrice = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "ʣ������", ShortCode = "MonthQtyPrice", Desc = "ʣ������", ContextType = SysDic.All, Length = 8)]
        public virtual double? MonthQtyPrice
        {
            get { return _monthQtyPrice; }
            set { _monthQtyPrice = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX1", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX1
        {
            get { return _zX1; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX1", value, value.ToString());
                _zX1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX2", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX2
        {
            get { return _zX2; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX2", value, value.ToString());
                _zX2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX3", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX3
        {
            get { return _zX3; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX3", value, value.ToString());
                _zX3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CreaterID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long CreaterID
        {
            get { return _createrID; }
            set { _createrID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CreaterName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CreaterName
        {
            get { return _createrName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CreaterName", value, value.ToString());
                _createrName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        #endregion

        [DataMember]
        [DataDesc(CName = "�����������", ShortCode = "InQtyMemo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string InQtyMemo { get; set; }

        [DataMember]
        [DataDesc(CName = "����ʹ��������", ShortCode = "EquipQtyMemo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string EquipQtyMemo { get; set; }

        [DataMember]
        [DataDesc(CName = "�˹�Ӧ��������", ShortCode = "ReturnQtyMemo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ReturnQtyMemo { get; set; }

        [DataMember]
        [DataDesc(CName = "��汨��������", ShortCode = "LossQtyMemo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string LossQtyMemo { get; set; }

        [DataMember]
        [DataDesc(CName = "���˳���������", ShortCode = "AdjustmentOutQtyMemo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string AdjustmentOutQtyMemo { get; set; }
    }
    #endregion
}