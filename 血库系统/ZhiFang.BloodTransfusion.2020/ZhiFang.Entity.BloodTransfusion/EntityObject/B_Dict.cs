using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BDict

	/// <summary>
	/// BDict object for NHibernate mapped table 'B_Dict'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "字典信息", ClassCName = "BDict", ShortCode = "BDict", Desc = "字典信息")]
	public class BDict : BaseEntity
	{
		#region Member Variables

		protected string _cName;
		protected string _sName;
		protected string _shortCode;
		protected string _pinYinZiTou;
		protected string _hisOrderCode;
		protected string _useCode;
		protected string _standCode;
		protected string _deveCode;
		protected bool _isUse;
		protected int _dispOrder;
		protected string _memo;
		protected BDictType _bDictType;

		#endregion

		#region Constructors

		public BDict() { }

		public BDict(long labID, string cName, string sName, string shortCode, string pinYinZiTou, string hisOrderCode,string useCode, string standCode, string deveCode, bool isUse, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, string memo, BDictType bDictType)
		{
			this._labID = labID;
			this._cName = cName;
			this._sName = sName;
			this._shortCode = shortCode;
			this._pinYinZiTou = pinYinZiTou;
			this._hisOrderCode = hisOrderCode;
			this._useCode = useCode;
			this._standCode = standCode;
			this._deveCode = deveCode;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._memo = memo;
			this._bDictType = bDictType;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[DataDesc(CName = "字典名称", ShortCode = "CName", Desc = "字典名称", ContextType = SysDic.All, Length = 80)]
		public virtual string CName
		{
			get { return _cName; }
			set
			{
				if (value != null && value.Length > 80)
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
				if (value != null && value.Length > 80)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "快捷码", ShortCode = "ShortCode", Desc = "快捷码", ContextType = SysDic.All, Length = 40)]
		public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if (value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "拼音字头", ShortCode = "PinYinZiTou", Desc = "拼音字头", ContextType = SysDic.All, Length = 50)]
		public virtual string PinYinZiTou
		{
			get { return _pinYinZiTou; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
				_pinYinZiTou = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "对照转换码", ShortCode = "HisOrderCode", Desc = "对照转换码", ContextType = SysDic.All, Length = 100)]
		public virtual string HisOrderCode
		{
			get { return _hisOrderCode; }
			set
			{
				if (value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for HisOrderCode", value, value.ToString());
				_hisOrderCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "业务编码", ShortCode = "UseCode", Desc = "业务编码", ContextType = SysDic.All, Length = 50)]
		public virtual string UseCode
		{
			get { return _useCode; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for UseCode", value, value.ToString());
				_useCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "标准代码", ShortCode = "StandCode", Desc = "标准代码", ContextType = SysDic.All, Length = 50)]
		public virtual string StandCode
		{
			get { return _standCode; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for StandCode", value, value.ToString());
				_standCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "开发商代码", ShortCode = "DeveCode", Desc = "开发商代码", ContextType = SysDic.All, Length = 50)]
		public virtual string DeveCode
		{
			get { return _deveCode; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DeveCode", value, value.ToString());
				_deveCode = value;
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
		[DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 214748364)]
		public virtual string Memo
		{
			get { return _memo; }
			set
			{
				_memo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "字典类型", ShortCode = "BDictType", Desc = "字典类型")]
		public virtual BDictType BDictType
		{
			get { return _bDictType; }
			set { _bDictType = value; }
		}


		#endregion
	}
	#endregion
}