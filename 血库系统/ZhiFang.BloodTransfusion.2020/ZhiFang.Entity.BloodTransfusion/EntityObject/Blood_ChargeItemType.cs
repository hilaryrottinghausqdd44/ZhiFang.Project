using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodChargeItemType

	/// <summary>
	/// BloodChargeItemType object for NHibernate mapped table 'Blood_ChargeItemType'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "费用项目类型表 对收费项目进行分类描述", ClassCName = "BloodChargeItemType", ShortCode = "BloodChargeItemType", Desc = "费用项目类型表 对收费项目进行分类描述")]
	public class BloodChargeItemType : BaseEntity
	{
		#region Member Variables
		
        protected string _cName;
        protected string _sName;
        protected string _pinYinZiTou;
        protected string _shortCode;
        protected bool _isUse;
        protected int _dispOrder;

		#endregion

		#region Constructors

		public BloodChargeItemType() { }

		public BloodChargeItemType( long labID, string cName, string sName, string pinYinZiTou, string shortCode, bool isUse, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._cName = cName;
			this._sName = sName;
			this._pinYinZiTou = pinYinZiTou;
			this._shortCode = shortCode;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "中文名称", ShortCode = "CName", Desc = "中文名称", ContextType = SysDic.All, Length = 20)]
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

		#endregion
	}
	#endregion
}