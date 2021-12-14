using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodBagProcessType

	/// <summary>
	/// BloodBagProcessType object for NHibernate mapped table 'Blood_BagProcessType'.
	/// 血制品加工类型表
	/// 血制品加工类型表的信息包含存储加工类型字典基本信息及加工类型与费用项目的关系
	/// 如果加工类型与收费项目关系存在一对多关系或多对多关系，可以拆分出加工类型字典表及加工类型与收费项目关系表
	/// </summary>
	[DataContract]
    [DataDesc(CName = "加工类型表", ClassCName = "BloodBagProcessType", ShortCode = "BloodBagProcessType", Desc = "加工类型表")]
	public class BloodBagProcessType : BaseEntity
	{
		#region Member Variables
		
        protected string _cName;
        protected string _sName;
        protected string _pinYinZiTou;
        protected string _shortCode;
        protected string _hisOrderCode;
        protected bool _isUse;
        protected int _dispOrder;
		protected BloodChargeItem _bloodChargeItem;

		#endregion

		#region Constructors

		public BloodBagProcessType() { }

		public BloodBagProcessType( long labID, string cName, string sName, string pinYinZiTou, string shortCode, string hisOrderCode, bool isUse, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BloodChargeItem bloodChargeItem)
		{
			this._labID = labID;
			this._cName = cName;
			this._sName = sName;
			this._pinYinZiTou = pinYinZiTou;
			this._shortCode = shortCode;
			this._hisOrderCode = hisOrderCode;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodChargeItem = bloodChargeItem;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "加工名称", ShortCode = "CName", Desc = "加工名称", ContextType = SysDic.All, Length = 20)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 20)
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
        [DataDesc(CName = "对照码", ShortCode = "HisOrderCode", Desc = "对照码", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "是否可视", ShortCode = "IsUse", Desc = "是否可视", ContextType = SysDic.All, Length = 1)]
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
        [DataDesc(CName = "费用项目表", ShortCode = "BloodChargeItem", Desc = "费用项目表")]
		public virtual BloodChargeItem BloodChargeItem
		{
			get { return _bloodChargeItem; }
			set { _bloodChargeItem = value; }
		}

		#endregion
	}
	#endregion
}