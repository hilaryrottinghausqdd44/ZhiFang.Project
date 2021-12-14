using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodABO

	/// <summary>
	/// BloodABO object for NHibernate mapped table 'Blood_ABO'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "血型表", ClassCName = "BloodABO", ShortCode = "BloodABO", Desc = "血型表")]
	public class BloodABO : BaseEntity
	{
		#region Member Variables

		protected string _cName;
		protected string _rhEName;
		protected string _aBOCode;
		protected string _aBOType;
		protected string _rHType;
		protected string _shortCode;
		protected string _hisOrderCode;
		protected string _memo;
		protected int _dispOrder;
		protected bool _isUse;

		#endregion

		#region Constructors

		public BloodABO() { }

		public BloodABO(long labID, string aBORhName, string rhEName, string aBOCode, string aBOType, string rHType, string shortCode, string hisOrderCode, string memo, int dispOrder, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp)
		{
			this._labID = labID;
			this._cName = aBORhName;
			this._rhEName = rhEName;
			this._aBOCode = aBOCode;
			this._aBOType = aBOType;
			this._rHType = rHType;
			this._shortCode = shortCode;
			this._hisOrderCode = hisOrderCode;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[DataDesc(CName = "血型名称", ShortCode = "CName", Desc = "血型名称", ContextType = SysDic.All, Length = 20)]
		public virtual string CName
		{
			get { return _cName; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "Rh英文名", ShortCode = "RhEName", Desc = "Rh英文名", ContextType = SysDic.All, Length = 50)]
		public virtual string RhEName
		{
			get { return _rhEName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for RhEName", value, value.ToString());
				_rhEName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "血型条码，来源于血站血型", ShortCode = "ABOCode", Desc = "血型条码，来源于血站血型", ContextType = SysDic.All, Length = 20)]
		public virtual string ABOCode
		{
			get { return _aBOCode; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ABOCode", value, value.ToString());
				_aBOCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "ABO血型", ShortCode = "ABOType", Desc = "ABO血型", ContextType = SysDic.All, Length = 10)]
		public virtual string ABOType
		{
			get { return _aBOType; }
			set
			{
				if (value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for ABOType", value, value.ToString());
				_aBOType = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "h血型", ShortCode = "RHType", Desc = "h血型", ContextType = SysDic.All, Length = 10)]
		public virtual string RHType
		{
			get { return _rHType; }
			set
			{
				if (value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for RHType", value, value.ToString());
				_rHType = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "简码", ShortCode = "ShortCode", Desc = "简码", ContextType = SysDic.All, Length = 10)]
		public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if (value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "对照转换码,血型码，来源于血站", ShortCode = "HisOrderCode", Desc = "对照转换码,血型码，来源于血站", ContextType = SysDic.All, Length = 100)]
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
		[DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 200)]
		public virtual string Memo
		{
			get { return _memo; }
			set
			{
				if (value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
				_memo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "显示顺序", ShortCode = "DispOrder", Desc = "显示顺序", ContextType = SysDic.All, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

		[DataMember]
		[DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

		#endregion
	}
	#endregion
}