using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodBTestItem

	/// <summary>
	/// BloodBTestItem object for NHibernate mapped table 'Blood_BTestItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "检验项目表 ", ClassCName = "BloodBTestItem", ShortCode = "BloodBTestItem", Desc = "检验项目表 ")]
	public class BloodBTestItem : BaseEntity
	{
		#region Member Variables
		
        protected string _bTestItemName;
        protected string _eName;
        protected string _sName;
        protected string _pinYinZiTou;
        protected string _shortCode;
        protected bool _isGroup;
        protected string _hisOrderCode;
        protected string _reference;
        protected bool _isResultItem;
        protected bool _isPreTransEvalItem;
        protected bool _isUse;
        protected int _dispOrder;
		protected BloodBUnit _bloodBUnit;

		#endregion

		#region Constructors

		public BloodBTestItem() { }

		public BloodBTestItem( long labID, string bTestItemName, string eName, string sName, string pinYinZiTou, string shortCode, bool isGroup, string hisOrderCode, string reference, bool isResultItem, bool isPreTransEvalItem, bool isUse, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BloodBUnit bloodBUnit )
		{
			this._labID = labID;
			this._bTestItemName = bTestItemName;
			this._eName = eName;
			this._sName = sName;
			this._pinYinZiTou = pinYinZiTou;
			this._shortCode = shortCode;
			this._isGroup = isGroup;
			this._hisOrderCode = hisOrderCode;
			this._reference = reference;
			this._isResultItem = isResultItem;
			this._isPreTransEvalItem = isPreTransEvalItem;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodBUnit = bloodBUnit;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "项目名称", ShortCode = "BTestItemName", Desc = "项目名称", ContextType = SysDic.All, Length = 50)]
        public virtual string BTestItemName
		{
			get { return _bTestItemName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BTestItemName", value, value.ToString());
				_bTestItemName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "英文名", ShortCode = "EName", Desc = "英文名", ContextType = SysDic.All, Length = 20)]
        public virtual string EName
		{
			get { return _eName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
				_eName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 50)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
				if ( value != null && value.Length > 50)
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
        [DataDesc(CName = "是否组合", ShortCode = "IsGroup", Desc = "是否组合", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsGroup
		{
			get { return _isGroup; }
			set { _isGroup = value; }
		}

        [DataMember]
        [DataDesc(CName = "HIS项目对照码", ShortCode = "HisOrderCode", Desc = "HIS项目对照码", ContextType = SysDic.All, Length = 20)]
        public virtual string HisOrderCode
		{
			get { return _hisOrderCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HisOrderCode", value, value.ToString());
				_hisOrderCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "参考值", ShortCode = "Reference", Desc = "参考值", ContextType = SysDic.All, Length = 200)]
        public virtual string Reference
		{
			get { return _reference; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Reference", value, value.ToString());
				_reference = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否医嘱结果录入项", ShortCode = "IsResultItem", Desc = "是否医嘱结果录入项", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsResultItem
		{
			get { return _isResultItem; }
			set { _isResultItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否为输血前评估项", ShortCode = "IsPreTransEvalItem", Desc = "是否为输血前评估项", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsPreTransEvalItem
		{
			get { return _isPreTransEvalItem; }
			set { _isPreTransEvalItem = value; }
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
        [DataDesc(CName = "检验项目单位表", ShortCode = "BloodBUnit", Desc = "检验项目单位表")]
		public virtual BloodBUnit BloodBUnit
		{
			get { return _bloodBUnit; }
			set { _bloodBUnit = value; }
		}

		#endregion
	}
	#endregion
}