using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodChargeMoney

    /// <summary>
    /// BloodChargeMoney object for NHibernate mapped table 'Blood_ChargeMoney'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "收费管理", ClassCName = "BloodChargeMoney", ShortCode = "BloodChargeMoney", Desc = "收费管理")]
    public class BloodChargeMoney : BaseEntity
    {
        #region Member Variables

        protected long? _pChargeId;
        protected long? _operTypeId;
        protected string _cMNo;
        protected long? _typeId;
        protected long _bobjectID;
        protected long _bobjectDtlID;
        protected long? _bloodDeptNo;
        protected string _hisChargeItemNo;
        protected string _ordItemId;
        protected int _isCharge;
        protected int _hisisCharge;
        protected DateTime? _chargeDate;
        protected DateTime? _hisChargeDate;
        protected double _modulus;
        protected double _totalPrice;
        protected double _reductionAmount;
        protected double _actuallyAmount;

        protected long? _operateId;
        protected string _operateName;
        protected DateTime? _operateDate;
        protected string _operateMemo;
        protected string _hisDemo;
        protected bool _visible;
        protected int _dispOrder;
        protected BloodBagProcessType _bloodBagProcessType;
        protected BloodBReqForm _bloodBReqForm;
        protected BloodBReqItem _bloodBReqItem;
        protected BloodChargeItem _bloodChargeItem;
        protected BloodChargeItemLink _bloodChargeItemLink;
        protected BloodChargeItemType _bloodChargeItemType;
        protected BDict _chargeTypeNo;
        protected BloodPatinfo _patId;
        protected BloodUnit _bloodUnit;
        protected BDict _bCE;
        protected BloodChargeItem _g;

        #endregion

        #region Constructors

        public BloodChargeMoney() { }

        public BloodChargeMoney(long labID, long pChargeId, long operTypeId, string cMNo, long typeId, long bobjectID, long bobjectDtlID, long bloodDeptNo, string hisChargeItemNo, string ordItemId, int isCharge, int hisisCharge, DateTime chargeDate, DateTime hisChargeDate, double modulus, double totalPrice, double reductionAmount, double actuallyAmount, long operateId, string operateName, DateTime operateDate, string operateMemo, string hisDemo, bool visible, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BloodBagProcessType bloodBagProcessType, BloodBReqForm bloodBReqForm, BloodBReqItem bloodBReqItem, BloodChargeItem bloodChargeItem, BloodChargeItemLink bloodChargeItemLink, BloodChargeItemType bloodChargeItemType, BDict chargeTypeNo, BloodPatinfo patId, BloodUnit bloodUnit, BDict bCE, BloodChargeItem g)
        {
            this._labID = labID;
            this._pChargeId = pChargeId;
            this._operTypeId = operTypeId;
            this._cMNo = cMNo;
            this._typeId = typeId;
            this._bobjectID = bobjectID;
            this._bobjectDtlID = bobjectDtlID;
            this._bloodDeptNo = bloodDeptNo;
            this._hisChargeItemNo = hisChargeItemNo;
            this._ordItemId = ordItemId;
            this._isCharge = isCharge;
            this._hisisCharge = hisisCharge;
            this._chargeDate = chargeDate;
            this._hisChargeDate = hisChargeDate;
            this._modulus = modulus;
            this._totalPrice = totalPrice;
            this._reductionAmount = reductionAmount;
            this._actuallyAmount = actuallyAmount;

            this._operateId = operateId;
            this._operateName = operateName;
            this._operateDate = operateDate;
            this._operateMemo = operateMemo;
            this._hisDemo = hisDemo;
            this._visible = visible;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._bloodBagProcessType = bloodBagProcessType;
            this._bloodBReqForm = bloodBReqForm;
            this._bloodBReqItem = bloodBReqItem;
            this._bloodChargeItem = bloodChargeItem;
            this._bloodChargeItemLink = bloodChargeItemLink;
            this._bloodChargeItemType = bloodChargeItemType;
            this._chargeTypeNo = chargeTypeNo;
            this._patId = patId;
            this._bloodUnit = bloodUnit;
            this._bCE = bCE;
            this._g = g;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "原收费记录Id,退费时记录", ShortCode = "PChargeId", Desc = "原收费记录Id,退费时记录", ContextType = SysDic.All, Length = 8)]
        public virtual long? PChargeId
        {
            get { return _pChargeId; }
            set { _pChargeId = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "变动类型", ShortCode = "OperTypeId", Desc = "变动类型", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperTypeId
        {
            get { return _operTypeId; }
            set { _operTypeId = value; }
        }

        [DataMember]
        [DataDesc(CName = "收费流水号", ShortCode = "CMNo", Desc = "收费流水号", ContextType = SysDic.All, Length = 50)]
        public virtual string CMNo
        {
            get { return _cMNo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CMNo", value, value.ToString());
                _cMNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "来源类型Id", ShortCode = "TypeId", Desc = "来源类型Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? TypeId
        {
            get { return _typeId; }
            set { _typeId = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "业务主单对象ID", ShortCode = "BobjectID", Desc = "业务主单对象ID", ContextType = SysDic.All, Length = 8)]
        public virtual long BobjectID
        {
            get { return _bobjectID; }
            set { _bobjectID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "业务明细对象ID", ShortCode = "BobjectDtlID", Desc = "业务明细对象ID", ContextType = SysDic.All, Length = 8)]
        public virtual long BobjectDtlID
        {
            get { return _bobjectDtlID; }
            set { _bobjectDtlID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医技科室编号", ShortCode = "BloodDeptNo", Desc = "医技科室编号", ContextType = SysDic.All, Length = 8)]
        public virtual long? BloodDeptNo
        {
            get { return _bloodDeptNo; }
            set { _bloodDeptNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "His收费编号", ShortCode = "HisChargeItemNo", Desc = "His收费编号", ContextType = SysDic.All, Length = 50)]
        public virtual string HisChargeItemNo
        {
            get { return _hisChargeItemNo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for HisChargeItemNo", value, value.ToString());
                _hisChargeItemNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "HIS返填的记录单号", ShortCode = "OrdItemId", Desc = "HIS返填的记录单号", ContextType = SysDic.All, Length = 50)]
        public virtual string OrdItemId
        {
            get { return _ordItemId; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for OrdItemId", value, value.ToString());
                _ordItemId = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否需要收费", ShortCode = "IsCharge", Desc = "是否需要收费", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCharge
        {
            get { return _isCharge; }
            set { _isCharge = value; }
        }

        [DataMember]
        [DataDesc(CName = "His是否收费", ShortCode = "HisisCharge", Desc = "His是否收费", ContextType = SysDic.All, Length = 4)]
        public virtual int HisisCharge
        {
            get { return _hisisCharge; }
            set { _hisisCharge = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "计费日期", ShortCode = "ChargeDate", Desc = "计费日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ChargeDate
        {
            get { return _chargeDate; }
            set { _chargeDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "His收费时间", ShortCode = "HisChargeDate", Desc = "His收费时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? HisChargeDate
        {
            get { return _hisChargeDate; }
            set { _hisChargeDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "收费系数", ShortCode = "Modulus", Desc = "收费系数", ContextType = SysDic.All, Length = 8)]
        public virtual double Modulus
        {
            get { return _modulus; }
            set { _modulus = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "费用金额", ShortCode = "TotalPrice", Desc = "费用金额", ContextType = SysDic.All, Length = 8)]
        public virtual double TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "减免金额", ShortCode = "ReductionAmount", Desc = "减免金额", ContextType = SysDic.All, Length = 8)]
        public virtual double ReductionAmount
        {
            get { return _reductionAmount; }
            set { _reductionAmount = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实付金额", ShortCode = "ActuallyAmount", Desc = "实付金额", ContextType = SysDic.All, Length = 8)]
        public virtual double ActuallyAmount
        {
            get { return _actuallyAmount; }
            set { _actuallyAmount = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作用户编码", ShortCode = "OperateId", Desc = "操作用户编码", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperateId
        {
            get { return _operateId; }
            set { _operateId = value; }
        }

        [DataMember]
        [DataDesc(CName = "操作用户名称", ShortCode = "OperateName", Desc = "操作用户名称", ContextType = SysDic.All, Length = 50)]
        public virtual string OperateName
        {
            get { return _operateName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for OperateName", value, value.ToString());
                _operateName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作日期", ShortCode = "OperateDate", Desc = "操作日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OperateDate
        {
            get { return _operateDate; }
            set { _operateDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "费用备注", ShortCode = "OperateMemo", Desc = "费用备注", ContextType = SysDic.All, Length = 200)]
        public virtual string OperateMemo
        {
            get { return _operateMemo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for OperateMemo", value, value.ToString());
                _operateMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "退费HIS备注", ShortCode = "HisDemo", Desc = "退费HIS备注", ContextType = SysDic.All, Length = 200)]
        public virtual string HisDemo
        {
            get { return _hisDemo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for HisDemo", value, value.ToString());
                _hisDemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "加工类型表", ShortCode = "BloodBagProcessType", Desc = "加工类型表")]
        public virtual BloodBagProcessType BloodBagProcessType
        {
            get { return _bloodBagProcessType; }
            set { _bloodBagProcessType = value; }
        }

        [DataMember]
        [DataDesc(CName = "用血申请主单表", ShortCode = "BloodBReqForm", Desc = "用血申请主单表")]
        public virtual BloodBReqForm BloodBReqForm
        {
            get { return _bloodBReqForm; }
            set { _bloodBReqForm = value; }
        }

        [DataMember]
        [DataDesc(CName = "用血申请明细信息表", ShortCode = "BloodBReqItem", Desc = "用血申请明细信息表")]
        public virtual BloodBReqItem BloodBReqItem
        {
            get { return _bloodBReqItem; }
            set { _bloodBReqItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "费用项目表", ShortCode = "BloodChargeItem", Desc = "费用项目表")]
        public virtual BloodChargeItem BloodChargeItem
        {
            get { return _bloodChargeItem; }
            set { _bloodChargeItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "组合项目费用关系表", ShortCode = "BloodChargeItemLink", Desc = "组合项目费用关系表")]
        public virtual BloodChargeItemLink BloodChargeItemLink
        {
            get { return _bloodChargeItemLink; }
            set { _bloodChargeItemLink = value; }
        }

        [DataMember]
        [DataDesc(CName = "费用项目类型表 对收费项目进行分类描述", ShortCode = "BloodChargeItemType", Desc = "费用项目类型表 对收费项目进行分类描述")]
        public virtual BloodChargeItemType BloodChargeItemType
        {
            get { return _bloodChargeItemType; }
            set { _bloodChargeItemType = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典信息", ShortCode = "ChargeTypeNo", Desc = "字典信息")]
        public virtual BDict ChargeType
        {
            get { return _chargeTypeNo; }
            set { _chargeTypeNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "病人就诊记录信息表", ShortCode = "PatId", Desc = "病人就诊记录信息表")]
        public virtual BloodPatinfo BloodPatinfo
        {
            get { return _patId; }
            set { _patId = value; }
        }

        [DataMember]
        [DataDesc(CName = "血制品单位", ShortCode = "BloodUnit", Desc = "血制品单位")]
        public virtual BloodUnit BloodUnit
        {
            get { return _bloodUnit; }
            set { _bloodUnit = value; }
        }

        [DataMember]
        [DataDesc(CName = "费用异常说明字典", ShortCode = "BloodChargeException", Desc = "费用异常说明字典")]
        public virtual BDict BloodChargeException
        {
            get { return _bCE; }
            set { _bCE = value; }
        }

        [DataMember]
        [DataDesc(CName = "费用项目表(组合项目)", ShortCode = "GBloodChargeItem", Desc = "费用项目表(组合项目)")]
        public virtual BloodChargeItem GBloodChargeItem
        {
            get { return _g; }
            set { _g = value; }
        }


        #endregion
    }
    #endregion
}