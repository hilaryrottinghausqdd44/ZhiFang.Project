using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodBagOperation

    /// <summary>
    /// BloodBagOperation object for NHibernate mapped table 'Blood_BagOperation'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodBagOperation", ShortCode = "BloodBagOperation", Desc = "")]
    public class BloodBagOperation : BaseEntity
    {
        #region Member Variables

        protected string _bagOperationNo;
        //protected string _bOutFormID;
        //protected string _bOutItemID;
        protected long? _bagOperTypeID;
        protected long? _bagOperResultID;
        protected long? _deptID;
        protected string _deptCName;
        protected string _bBagCode;
        protected string _pCode;
        protected long? _bagOperID;
        protected string _bagOper;
        protected DateTime? _bagOperTime;
        protected long? _carrierID;
        protected string _carrier;
        protected bool _isVisible;
        protected DateTime? _dataUpdateTime;
        protected BloodBReqForm _bloodBReqForm;
        protected BloodBOutForm _bloodBOutForm;
        protected BloodBOutItem _bloodBOutItem;
        protected Bloodstyle _bloodstyle;

        #endregion

        #region Constructors

        public BloodBagOperation() { }

        public BloodBagOperation(long labID, string bagOperationNo, long bagOperTypeID, long bagOperResultID, long deptID, string deptCName, string bBagCode, string pCode, long bagOperID, string bagOper, DateTime bagOperTime, long carrierID, string carrier, bool isVisible, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BloodBReqForm bloodBReqForm, BloodBOutForm bloodBOutForm, BloodBOutItem bloodBOutItem, Bloodstyle bloodstyle)
        {
            this._labID = labID;
            this._bagOperationNo = bagOperationNo;
            this._bloodBOutForm = bloodBOutForm;
            this._bloodBOutItem = bloodBOutItem;
            this._bagOperTypeID = bagOperTypeID;
            this._bagOperResultID = bagOperResultID;
            this._deptID = deptID;
            this._deptCName = deptCName;
            this._bBagCode = bBagCode;
            this._pCode = pCode;
            this._bagOperID = bagOperID;
            this._bagOper = bagOper;
            this._bagOperTime = bagOperTime;
            this._carrierID = carrierID;
            this._carrier = carrier;
            this._isVisible = isVisible;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._bloodBReqForm = bloodBReqForm;
            this._bloodstyle = bloodstyle;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "BagOperationNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BagOperationNo
        {
            get { return _bagOperationNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BagOperationNo", value, value.ToString());
                _bagOperationNo = value;
            }
        }

        //      [DataMember]
        //      [DataDesc(CName = "", ShortCode = "BOutFormID", Desc = "", ContextType = SysDic.All, Length = 40)]
        //      public virtual string BOutFormID
        //{
        //	get { return _bOutFormID; }
        //	set
        //	{
        //		if ( value != null && value.Length > 40)
        //			throw new ArgumentOutOfRangeException("Invalid value for BOutFormID", value, value.ToString());
        //		_bOutFormID = value;
        //	}
        //}

        //      [DataMember]
        //      [DataDesc(CName = "", ShortCode = "BOutItemID", Desc = "", ContextType = SysDic.All, Length = 20)]
        //      public virtual string BOutItemID
        //{
        //	get { return _bOutItemID; }
        //	set
        //	{
        //		if ( value != null && value.Length > 20)
        //			throw new ArgumentOutOfRangeException("Invalid value for BOutItemID", value, value.ToString());
        //		_bOutItemID = value;
        //	}
        //}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BagOperTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? BagOperTypeID
        {
            get { return _bagOperTypeID; }
            set { _bagOperTypeID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BagOperResultID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? BagOperResultID
        {
            get { return _bagOperResultID; }
            set { _bagOperResultID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DeptID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeptCName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DeptCName
        {
            get { return _deptCName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for DeptCName", value, value.ToString());
                _deptCName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BBagCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BBagCode
        {
            get { return _bBagCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BBagCode", value, value.ToString());
                _bBagCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PCode", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string PCode
        {
            get { return _pCode; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for PCode", value, value.ToString());
                _pCode = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BagOperID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? BagOperID
        {
            get { return _bagOperID; }
            set { _bagOperID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BagOper", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BagOper
        {
            get { return _bagOper; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BagOper", value, value.ToString());
                _bagOper = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "登记时间/操作时间", ShortCode = "BagOperTime", Desc = "登记时间/操作时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BagOperTime
        {
            get { return _bagOperTime; }
            set { _bagOperTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CarrierID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? CarrierID
        {
            get { return _carrierID; }
            set { _carrierID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Carrier", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Carrier
        {
            get { return _carrier; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Carrier", value, value.ToString());
                _carrier = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsVisible", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodBReqForm", Desc = "")]
        public virtual BloodBReqForm BloodBReqForm
        {
            get { return _bloodBReqForm; }
            set { _bloodBReqForm = value; }
        }
        [DataMember]
        [DataDesc(CName = "BloodBOutForm", ShortCode = "BloodBOutForm", Desc = "BloodBOutForm")]
        public virtual BloodBOutForm BloodBOutForm
        {
            get { return _bloodBOutForm; }
            set { _bloodBOutForm = value; }
        }
        [DataMember]
        [DataDesc(CName = "BloodBOutItem", ShortCode = "BloodBOutItem", Desc = "BloodBOutItem")]
        public virtual BloodBOutItem BloodBOutItem
        {
            get { return _bloodBOutItem; }
            set { _bloodBOutItem = value; }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "Bloodstyle", Desc = "")]
        public virtual Bloodstyle Bloodstyle
        {
            get { return _bloodstyle; }
            set { _bloodstyle = value; }
        }

        #endregion

        #region 自定义属性

        [DataMember]
        [DataDesc(CName = "血袋外观信息", ShortCode = "BloodAppearance", Desc = "血袋外观信息")]
        public virtual BloodBagOperationDtl BloodAppearance { get; set; }

        [DataMember]
        [DataDesc(CName = "血袋完整性", ShortCode = "BloodIntegrity", Desc = "血袋完整性")]
        public virtual BloodBagOperationDtl BloodIntegrity { get; set; }

        #endregion
    }
    #endregion
}