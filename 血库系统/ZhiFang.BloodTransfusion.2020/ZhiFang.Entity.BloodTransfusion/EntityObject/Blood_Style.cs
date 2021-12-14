using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodStyle

	/// <summary>
	/// BloodStyle object for NHibernate mapped table 'Blood_Style'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "血制品字典", ClassCName = "BloodStyle", ShortCode = "BloodStyle", Desc = "血制品字典")]
	public class BloodStyle : BaseEntity
	{
		#region Member Variables

		protected string _cName;
		protected string _sName;
		protected string _shortCode;
		protected string _pinYinZiTou;
		protected bool _isMakeBlood;
		protected string _hisDispCode;
		protected string _bisDispCode;
		protected double _storeDays;
		protected double _hemolysisTime;
		protected string _hisOrderCode;
		protected int _dispOrder;
		protected bool _isUse;
		protected BloodClass _bloodClass;
		protected BloodUnit _bloodUnit;
		protected BDict _bloodStoreCond;

		#endregion

		#region Constructors

		public BloodStyle() { }

		public BloodStyle(long labID, string bloodName, string sName, string shortCode, string pinYinZiTou, bool isMakeBlood, string hisDispCode, string bisDispCode, double storeDays, double hemolysisTime, string hisOrderCode, int dispOrder, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, BloodClass bloodClass, BloodUnit bloodUnit, BDict storeCondNo)
		{
			this._labID = labID;
			this._cName = bloodName;
			this._sName = sName;
			this._shortCode = shortCode;
			this._pinYinZiTou = pinYinZiTou;
			this._isMakeBlood = isMakeBlood;
			this._hisDispCode = hisDispCode;
			this._bisDispCode = bisDispCode;
			this._storeDays = storeDays;
			this._hemolysisTime = hemolysisTime;
			this._hisOrderCode = hisOrderCode;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodClass = bloodClass;
			this._bloodUnit = bloodUnit;
			this._bloodStoreCond = storeCondNo;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 30)]
		public virtual string CName
		{
			get { return _cName; }
			set
			{
				if (value != null && value.Length > 30)
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
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "简码", ShortCode = "ShortCode", Desc = "简码", ContextType = SysDic.All, Length = 50)]
		public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if (value != null && value.Length > 50)
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
		[DataDesc(CName = "是否做交叉配血", ShortCode = "IsMakeBlood", Desc = "是否做交叉配血", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsMakeBlood
		{
			get { return _isMakeBlood; }
			set { _isMakeBlood = value; }
		}

		[DataMember]
		[DataDesc(CName = "HIS对照码", ShortCode = "HisDispCode", Desc = "HIS对照码", ContextType = SysDic.All, Length = 50)]
		public virtual string HisDispCode
		{
			get { return _hisDispCode; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for HisDispCode", value, value.ToString());
				_hisDispCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "血站对照码", ShortCode = "BisDispCode", Desc = "血站对照码", ContextType = SysDic.All, Length = 50)]
		public virtual string BisDispCode
		{
			get { return _bisDispCode; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BisDispCode", value, value.ToString());
				_bisDispCode = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "贮存时长（天）单位统一按天计算", ShortCode = "StoreDays", Desc = "贮存时长（天）单位统一按天计算", ContextType = SysDic.All, Length = 8)]
		public virtual double StoreDays
		{
			get { return _storeDays; }
			set { _storeDays = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "溶血时间（分）", ShortCode = "HemolysisTime", Desc = "溶血时间（分）", ContextType = SysDic.All, Length = 8)]
		public virtual double HemolysisTime
		{
			get { return _hemolysisTime; }
			set { _hemolysisTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "HIS对照码", ShortCode = "HisOrderCode", Desc = "HIS对照码", ContextType = SysDic.All, Length = 50)]
		public virtual string HisOrderCode
		{
			get { return _hisOrderCode; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for HisOrderCode", value, value.ToString());
				_hisOrderCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
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

		[DataMember]
		[DataDesc(CName = "血袋分类", ShortCode = "BloodClass", Desc = "血袋分类")]
		public virtual BloodClass BloodClass
		{
			get { return _bloodClass; }
			set { _bloodClass = value; }
		}

		[DataMember]
		[DataDesc(CName = "血制品单位", ShortCode = "BloodUnit", Desc = "血制品单位")]
		public virtual BloodUnit BloodUnit
		{
			get { return _bloodUnit; }
			set { _bloodUnit = value; }
		}

		[DataMember]
		[DataDesc(CName = "贮存温度单位", ShortCode = "BloodStoreCond", Desc = "贮存温度单位")]
		public virtual BDict BloodStoreCond
		{
			get { return _bloodStoreCond; }
			set { _bloodStoreCond = value; }
		}


		#endregion
	}
	#endregion
}