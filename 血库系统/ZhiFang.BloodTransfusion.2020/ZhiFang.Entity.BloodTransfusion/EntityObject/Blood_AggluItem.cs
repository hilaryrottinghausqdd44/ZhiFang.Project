using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodAggluItem

	/// <summary>
	/// BloodAggluItem object for NHibernate mapped table 'Blood_AggluItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "凝集规则明细字典", ClassCName = "BloodAggluItem", ShortCode = "BloodAggluItem", Desc = "凝集规则明细字典")]
	public class BloodAggluItem : BaseEntity
	{
		#region Member Variables
		
        protected string _aggluItemName;
        protected string _cName;
        protected string _sName;
        protected string _pinYinZiTou;
        protected string _shortCode;
        protected string _rhPriority;
        protected bool _isUse;
        protected int _dispOrder;
		protected BDict _bloodAgglu;
		protected BloodClass _bloodClass;

		#endregion

		#region Constructors

		public BloodAggluItem() { }

		public BloodAggluItem( long labID, string aggluItemName, string aggluName, string sName, string pinYinZiTou, string shortCode, string rhPriority, bool isUse, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BDict aggluNo, BloodClass bloodClass )
		{
			this._labID = labID;
			this._aggluItemName = aggluItemName;
			this._cName = aggluName;
			this._sName = sName;
			this._pinYinZiTou = pinYinZiTou;
			this._shortCode = shortCode;
			this._rhPriority = rhPriority;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodAgglu = aggluNo;
			this._bloodClass = bloodClass;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "可配rH小因子", ShortCode = "AggluItemName", Desc = "可配rH小因子", ContextType = SysDic.All, Length = 50)]
        public virtual string AggluItemName
		{
			get { return _aggluItemName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for AggluItemName", value, value.ToString());
				_aggluItemName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "凝集名称", ShortCode = "CName", Desc = "凝集名称", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 80)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
				if ( value != null && value.Length > 80)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "拼音字头", ShortCode = "PinYinZiTou", Desc = "拼音字头", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
		{
			get { return _pinYinZiTou; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
				_pinYinZiTou = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "ShortCode", Desc = "快捷码", ContextType = SysDic.All, Length = 20)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "RH小因子优先级", ShortCode = "RhPriority", Desc = "RH小因子优先级", ContextType = SysDic.All, Length = 20)]
        public virtual string RhPriority
		{
			get { return _rhPriority; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for RhPriority", value, value.ToString());
				_rhPriority = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "字典信息", ShortCode = "BloodAgglu", Desc = "字典信息")]
		public virtual BDict BloodAgglu
		{
			get { return _bloodAgglu; }
			set { _bloodAgglu = value; }
		}

        [DataMember]
        [DataDesc(CName = "血袋分类", ShortCode = "BloodClass", Desc = "血袋分类")]
		public virtual BloodClass BloodClass
		{
			get { return _bloodClass; }
			set { _bloodClass = value; }
		}

        
		#endregion
	}
	#endregion
}