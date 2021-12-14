using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region MEChargeInfo

    /// <summary>
    /// MEChargeInfo object for NHibernate mapped table 'ME_ChargeInfo'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "计费操作表", ClassCName = "MEChargeInfo", ShortCode = "MEChargeInfo", Desc = "计费操作表")]
    public class MEChargeInfo : BaseEntity
    {
        #region Member Variables

        protected decimal _amountReceivable;
        protected decimal _realAmount;
        protected int _operType;
        protected string _dataAddUser;
        protected long? _dataAddUserID;
        protected string _dataAddComputer;
        protected long? _relateChargeInfoID;
        protected int _operResult;
        protected string _operErrInfo;
        protected BChargeItem _bChargeItem;
        protected MEGroupSampleForm _mEGroupSampleForm;
        protected MEPTOrderForm _mEPTOrderForm;

        #endregion

        #region Constructors

        public MEChargeInfo() { }

        public MEChargeInfo(long labID, decimal amountReceivable, decimal realAmount, int operType, DateTime dataAddTime, string dataAddUser, long dataAddUserID, string dataAddComputer, long relateChargeInfoID, byte[] dataTimeStamp, int operResult, string operErrInfo, BChargeItem bChargeItem, MEGroupSampleForm mEGroupSampleForm, MEPTOrderForm mEPTOrderForm)
        {
            this._labID = labID;
            this._amountReceivable = amountReceivable;
            this._realAmount = realAmount;
            this._operType = operType;
            this._dataAddTime = dataAddTime;
            this._dataAddUser = dataAddUser;
            this._dataAddUserID = dataAddUserID;
            this._dataAddComputer = dataAddComputer;
            this._relateChargeInfoID = relateChargeInfoID;
            this._dataTimeStamp = dataTimeStamp;
            this._operResult = operResult;
            this._operErrInfo = operErrInfo;
            this._bChargeItem = bChargeItem;
            this._mEGroupSampleForm = mEGroupSampleForm;
            this._mEPTOrderForm = mEPTOrderForm;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "应收金额", ShortCode = "AmountReceivable", Desc = "应收金额", ContextType = SysDic.All, Length = 9)]
        public virtual decimal AmountReceivable
        {
            get { return _amountReceivable; }
            set { _amountReceivable = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实收金额", ShortCode = "RealAmount", Desc = "实收金额", ContextType = SysDic.All, Length = 9)]
        public virtual decimal RealAmount
        {
            get { return _realAmount; }
            set { _realAmount = value; }
        }

        [DataMember]
        [DataDesc(CName = "操作类型：0-计费；1-退费", ShortCode = "OperType", Desc = "操作类型：0-计费；1-退费", ContextType = SysDic.All, Length = 4)]
        public virtual int OperType
        {
            get { return _operType; }
            set { _operType = value; }
        }

        [DataMember]
        [DataDesc(CName = "发布人", ShortCode = "DataAddUser", Desc = "发布人", ContextType = SysDic.All, Length = 50)]
        public virtual string DataAddUser
        {
            get { return _dataAddUser; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for DataAddUser", value, value.ToString());
                _dataAddUser = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发布人ID", ShortCode = "DataAddUserID", Desc = "发布人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DataAddUserID
        {
            get { return _dataAddUserID; }
            set { _dataAddUserID = value; }
        }

        [DataMember]
        [DataDesc(CName = "发布计算机名", ShortCode = "DataAddComputer", Desc = "发布计算机名", ContextType = SysDic.All, Length = 40)]
        public virtual string DataAddComputer
        {
            get { return _dataAddComputer; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for DataAddComputer", value, value.ToString());
                _dataAddComputer = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "相关费用操作ID：以某计费操作为基础的退费操作，需记录该计费操作ID", ShortCode = "RelateChargeInfoID", Desc = "相关费用操作ID：以某计费操作为基础的退费操作，需记录该计费操作ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? RelateChargeInfoID
        {
            get { return _relateChargeInfoID; }
            set { _relateChargeInfoID = value; }
        }

        [DataMember]
        [DataDesc(CName = "操作执行结果：1-内部（计费或退费）成功；2-HIS（计费或退费）成功；0-（计费或退费）失败", ShortCode = "OperResult", Desc = "操作执行结果：1-内部（计费或退费）成功；2-HIS（计费或退费）成功；0-（计费或退费）失败", ContextType = SysDic.All, Length = 4)]
        public virtual int OperResult
        {
            get { return _operResult; }
            set { _operResult = value; }
        }

        [DataMember]
        [DataDesc(CName = "计费失败原因", ShortCode = "OperErrInfo", Desc = "计费失败原因", ContextType = SysDic.All, Length = 300)]
        public virtual string OperErrInfo
        {
            get { return _operErrInfo; }
            set
            {
                if (value != null && value.Length > 300)
                    throw new ArgumentOutOfRangeException("Invalid value for OperErrInfo", value, value.ToString());
                _operErrInfo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "计费项目表：0-开单项目（包含医嘱项目、套餐项目）、1-检验耗材", ShortCode = "BChargeItem", Desc = "计费项目表：0-开单项目（包含医嘱项目、套餐项目）、1-检验耗材")]
        public virtual BChargeItem BChargeItem
        {
            get { return _bChargeItem; }
            set { _bChargeItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组样本单", ShortCode = "MEGroupSampleForm", Desc = "小组样本单")]
        public virtual MEGroupSampleForm MEGroupSampleForm
        {
            get { return _mEGroupSampleForm; }
            set { _mEGroupSampleForm = value; }
        }

        [DataMember]
        [DataDesc(CName = "医嘱单", ShortCode = "MEPTOrderForm", Desc = "医嘱单")]
        public virtual MEPTOrderForm MEPTOrderForm
        {
            get { return _mEPTOrderForm; }
            set { _mEPTOrderForm = value; }
        }


        #endregion
    }
    #endregion
}
