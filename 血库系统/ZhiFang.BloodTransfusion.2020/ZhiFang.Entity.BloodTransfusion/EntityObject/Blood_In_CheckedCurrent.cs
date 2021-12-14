using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodInCheckedCurrent

	/// <summary>
	/// BloodInCheckedCurrent object for NHibernate mapped table 'Blood_In_CheckedCurrent'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "当前库存表", ClassCName = "BloodInCheckedCurrent", ShortCode = "BloodInCheckedCurrent", Desc = "当前库存表")]
	public class BloodInCheckedCurrent : BaseEntity
	{
		#region Member Variables
		
        protected double _bCount;
        protected string _invalidCode;
        protected string _aBORHCode;
        protected DateTime? _invalidDate;
        protected bool _checkedFlag;
        protected string _aBOType;
        protected string _rhType;
        protected int _dispOrder;
        protected bool _visible;
		protected BloodABO _bloodABO;
		protected BloodClass _bloodClass;
		protected BDict _bloodIceBox;
		protected BloodInChecked _bloodInChecked;
		protected BloodQtyDtl _bloodQtyDtl;
		protected BloodStyle _bloodStyle;
		protected BloodUnit _bloodUnit;

		#endregion

		#region Constructors

		public BloodInCheckedCurrent() { }

		public BloodInCheckedCurrent( long labID, double bCount, string invalidCode, string aBORHCode, DateTime invalidDate, bool checkedFlag, string aBOType, string rhType, int dispOrder, bool visible, DateTime dataAddTime, byte[] dataTimeStamp, BloodABO aboNo, BloodClass bloodClass, BDict iceboxNo, BloodInChecked bloodInChecked, BloodQtyDtl bloodQtyDtl, BloodStyle bloodStyle, BloodUnit bloodUnit )
		{
			this._labID = labID;
			this._bCount = bCount;
			this._invalidCode = invalidCode;
			this._aBORHCode = aBORHCode;
			this._invalidDate = invalidDate;
			this._checkedFlag = checkedFlag;
			this._aBOType = aBOType;
			this._rhType = rhType;
			this._dispOrder = dispOrder;
			this._visible = visible;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodABO = aboNo;
			this._bloodClass = bloodClass;
			this._bloodIceBox = iceboxNo;
			this._bloodInChecked = bloodInChecked;
			this._bloodQtyDtl = bloodQtyDtl;
			this._bloodStyle = bloodStyle;
			this._bloodUnit = bloodUnit;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "规格", ShortCode = "BCount", Desc = "规格", ContextType = SysDic.All, Length = 8)]
        public virtual double BCount
		{
			get { return _bCount; }
			set { _bCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "失效日期码", ShortCode = "InvalidCode", Desc = "失效日期码", ContextType = SysDic.All, Length = 20)]
        public virtual string InvalidCode
		{
			get { return _invalidCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for InvalidCode", value, value.ToString());
				_invalidCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "血型码", ShortCode = "ABORHCode", Desc = "血型码", ContextType = SysDic.All, Length = 20)]
        public virtual string ABORHCode
		{
			get { return _aBORHCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ABORHCode", value, value.ToString());
				_aBORHCode = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "失效日期", ShortCode = "InvalidDate", Desc = "失效日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? InvalidDate
		{
			get { return _invalidDate; }
			set { _invalidDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "已盘标志", ShortCode = "CheckedFlag", Desc = "已盘标志", ContextType = SysDic.All, Length = 1)]
        public virtual bool CheckedFlag
		{
			get { return _checkedFlag; }
			set { _checkedFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "ABO血型", ShortCode = "ABOType", Desc = "ABO血型", ContextType = SysDic.All, Length = 20)]
        public virtual string ABOType
		{
			get { return _aBOType; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ABOType", value, value.ToString());
				_aBOType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "RH血型", ShortCode = "RhType", Desc = "RH血型", ContextType = SysDic.All, Length = 20)]
        public virtual string RhType
		{
			get { return _rhType; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for RhType", value, value.ToString());
				_rhType = value;
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
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "血型表", ShortCode = "BloodABO", Desc = "血型表")]
		public virtual BloodABO BloodABO
		{
			get { return _bloodABO; }
			set { _bloodABO = value; }
		}

        [DataMember]
        [DataDesc(CName = "血袋分类", ShortCode = "BloodClass", Desc = "血袋分类")]
		public virtual BloodClass BloodClass
		{
			get { return _bloodClass; }
			set { _bloodClass = value; }
		}

        [DataMember]
        [DataDesc(CName = "冰箱", ShortCode = "BloodIceBox", Desc = "冰箱")]
		public virtual BDict BloodIceBox
		{
			get { return _bloodIceBox; }
			set { _bloodIceBox = value; }
		}

        [DataMember]
        [DataDesc(CName = "盘点主单表", ShortCode = "BloodInChecked", Desc = "盘点主单表")]
		public virtual BloodInChecked BloodInChecked
		{
			get { return _bloodInChecked; }
			set { _bloodInChecked = value; }
		}

        [DataMember]
        [DataDesc(CName = "库存表", ShortCode = "BloodQtyDtl", Desc = "库存表")]
		public virtual BloodQtyDtl BloodQtyDtl
		{
			get { return _bloodQtyDtl; }
			set { _bloodQtyDtl = value; }
		}

        [DataMember]
        [DataDesc(CName = "血制品字典", ShortCode = "BloodStyle", Desc = "血制品字典")]
		public virtual BloodStyle BloodStyle
		{
			get { return _bloodStyle; }
			set { _bloodStyle = value; }
		}

        [DataMember]
        [DataDesc(CName = "血制品单位", ShortCode = "BloodUnit", Desc = "血制品单位")]
		public virtual BloodUnit BloodUnit
		{
			get { return _bloodUnit; }
			set { _bloodUnit = value; }
		}

        
		#endregion
	}
	#endregion
}