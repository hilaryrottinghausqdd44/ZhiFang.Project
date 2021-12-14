using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodSelfBlood

	/// <summary>
	/// BloodSelfBlood object for NHibernate mapped table 'Blood_SelfBlood'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "自体输血血袋登记信息表", ClassCName = "BloodSelfBlood", ShortCode = "BloodSelfBlood", Desc = "自体输血血袋登记信息表")]
	public class BloodSelfBlood : BaseEntity
	{
		#region Member Variables
		
        protected string _zdyBBagCode;
        protected string _cName;
        protected DateTime? _birthday;
        protected string _sex;
        protected string _bCount;
        protected string _collectDate;
        protected DateTime? _invalidDate;
        protected string _bBagCode;
        protected string _pCode;
        protected string _invalidCode;
        protected string _aBORHCode;
        protected long? _nurseId;
        protected string _nurseName;
        protected DateTime? _nurseDate;
        protected long? _technicianId;
        protected string _technicianName;
        protected DateTime? _technicianDate;
        protected long? _printId;
        protected string _printIName;
        protected string _printDate;
        protected bool _printFlag;
        protected long? _voidId;
        protected string _voidName;
        protected DateTime? _voidDate;
        protected bool _voidFlag;
        protected long? _warehousingID;
        protected string _warehousingName;
        protected DateTime? _warehousingDate;
        protected bool _warehousingFlag;
        protected bool _visible;
        protected int _dispOrder;
		protected BloodABO _bloodABO;
		protected BloodPatinfo _bloodPatinfo;
		protected BloodStyle _bloodStyle;

		#endregion

		#region Constructors

		public BloodSelfBlood() { }

		public BloodSelfBlood( long labID, string zdyBBagCode, string cName, DateTime birthday, string sex, string bCount, string collectDate, DateTime invalidDate, string bBagCode, string pCode, string invalidCode, string aBORHCode, long nurseId, string nurseName, DateTime nurseDate, long technicianId, string technicianName, DateTime technicianDate, long printId, string printIName, string printDate, bool printFlag, long voidId, string voidName, DateTime voidDate, bool voidFlag, long warehousingID, string warehousingName, DateTime warehousingDate, bool warehousingFlag, bool visible, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BloodABO aboNo, BloodPatinfo patId, BloodStyle bloodStyle )
		{
			this._labID = labID;
			this._zdyBBagCode = zdyBBagCode;
			this._cName = cName;
			this._birthday = birthday;
			this._sex = sex;
			this._bCount = bCount;
			this._collectDate = collectDate;
			this._invalidDate = invalidDate;
			this._bBagCode = bBagCode;
			this._pCode = pCode;
			this._invalidCode = invalidCode;
			this._aBORHCode = aBORHCode;
			this._nurseId = nurseId;
			this._nurseName = nurseName;
			this._nurseDate = nurseDate;
			this._technicianId = technicianId;
			this._technicianName = technicianName;
			this._technicianDate = technicianDate;
			this._printId = printId;
			this._printIName = printIName;
			this._printDate = printDate;
			this._printFlag = printFlag;
			this._voidId = voidId;
			this._voidName = voidName;
			this._voidDate = voidDate;
			this._voidFlag = voidFlag;
			this._warehousingID = warehousingID;
			this._warehousingName = warehousingName;
			this._warehousingDate = warehousingDate;
			this._warehousingFlag = warehousingFlag;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodABO = aboNo;
			this._bloodPatinfo = patId;
			this._bloodStyle = bloodStyle;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "自定义血袋条码", ShortCode = "ZdyBBagCode", Desc = "自定义血袋条码", ContextType = SysDic.All, Length = 50)]
        public virtual string ZdyBBagCode
		{
			get { return _zdyBBagCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZdyBBagCode", value, value.ToString());
				_zdyBBagCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "姓名", ShortCode = "CName", Desc = "姓名", ContextType = SysDic.All, Length = 50)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "出生日期", ShortCode = "Birthday", Desc = "出生日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? Birthday
		{
			get { return _birthday; }
			set { _birthday = value; }
		}

        [DataMember]
        [DataDesc(CName = "性别", ShortCode = "Sex", Desc = "性别", ContextType = SysDic.All, Length = 50)]
        public virtual string Sex
		{
			get { return _sex; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Sex", value, value.ToString());
				_sex = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "规格", ShortCode = "BCount", Desc = "规格", ContextType = SysDic.All, Length = 20)]
        public virtual string BCount
		{
			get { return _bCount; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BCount", value, value.ToString());
				_bCount = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "采集时间", ShortCode = "CollectDate", Desc = "采集时间", ContextType = SysDic.All, Length = 20)]
        public virtual string CollectDate
		{
			get { return _collectDate; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for CollectDate", value, value.ToString());
				_collectDate = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "失效时间", ShortCode = "InvalidDate", Desc = "失效时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? InvalidDate
		{
			get { return _invalidDate; }
			set { _invalidDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "血袋码", ShortCode = "BBagCode", Desc = "血袋码", ContextType = SysDic.All, Length = 50)]
        public virtual string BBagCode
		{
			get { return _bBagCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BBagCode", value, value.ToString());
				_bBagCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "产品码", ShortCode = "PCode", Desc = "产品码", ContextType = SysDic.All, Length = 20)]
        public virtual string PCode
		{
			get { return _pCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for PCode", value, value.ToString());
				_pCode = value;
			}
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
        [DataDesc(CName = "护士工号", ShortCode = "NurseId", Desc = "护士工号", ContextType = SysDic.All, Length = 8)]
		public virtual long? NurseId
		{
			get { return _nurseId; }
			set { _nurseId = value; }
		}

        [DataMember]
        [DataDesc(CName = "护士姓名", ShortCode = "NurseName", Desc = "护士姓名", ContextType = SysDic.All, Length = 20)]
        public virtual string NurseName
		{
			get { return _nurseName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for NurseName", value, value.ToString());
				_nurseName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "护士签名日期", ShortCode = "NurseDate", Desc = "护士签名日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? NurseDate
		{
			get { return _nurseDate; }
			set { _nurseDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "技师工号", ShortCode = "TechnicianId", Desc = "技师工号", ContextType = SysDic.All, Length = 8)]
		public virtual long? TechnicianId
		{
			get { return _technicianId; }
			set { _technicianId = value; }
		}

        [DataMember]
        [DataDesc(CName = "技师签名人", ShortCode = "TechnicianName", Desc = "技师签名人", ContextType = SysDic.All, Length = 50)]
        public virtual string TechnicianName
		{
			get { return _technicianName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TechnicianName", value, value.ToString());
				_technicianName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "技师签名时间", ShortCode = "TechnicianDate", Desc = "技师签名时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? TechnicianDate
		{
			get { return _technicianDate; }
			set { _technicianDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "打印者ID", ShortCode = "PrintId", Desc = "打印者ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? PrintId
		{
			get { return _printId; }
			set { _printId = value; }
		}

        [DataMember]
        [DataDesc(CName = "打印者", ShortCode = "PrintIName", Desc = "打印者", ContextType = SysDic.All, Length = 50)]
        public virtual string PrintIName
		{
			get { return _printIName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PrintIName", value, value.ToString());
				_printIName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "打印时间", ShortCode = "PrintDate", Desc = "打印时间", ContextType = SysDic.All, Length = 20)]
        public virtual string PrintDate
		{
			get { return _printDate; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for PrintDate", value, value.ToString());
				_printDate = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "打印标志", ShortCode = "PrintFlag", Desc = "打印标志", ContextType = SysDic.All, Length = 1)]
        public virtual bool PrintFlag
		{
			get { return _printFlag; }
			set { _printFlag = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "作废工号", ShortCode = "VoidId", Desc = "作废工号", ContextType = SysDic.All, Length = 8)]
		public virtual long? VoidId
		{
			get { return _voidId; }
			set { _voidId = value; }
		}

        [DataMember]
        [DataDesc(CName = "作废人", ShortCode = "VoidName", Desc = "作废人", ContextType = SysDic.All, Length = 50)]
        public virtual string VoidName
		{
			get { return _voidName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for VoidName", value, value.ToString());
				_voidName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "作废日期", ShortCode = "VoidDate", Desc = "作废日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? VoidDate
		{
			get { return _voidDate; }
			set { _voidDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "作废标志", ShortCode = "VoidFlag", Desc = "作废标志", ContextType = SysDic.All, Length = 1)]
        public virtual bool VoidFlag
		{
			get { return _voidFlag; }
			set { _voidFlag = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "入库人工号", ShortCode = "WarehousingID", Desc = "入库人工号", ContextType = SysDic.All, Length = 8)]
		public virtual long? WarehousingID
		{
			get { return _warehousingID; }
			set { _warehousingID = value; }
		}

        [DataMember]
        [DataDesc(CName = "入库人姓名", ShortCode = "WarehousingName", Desc = "入库人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string WarehousingName
		{
			get { return _warehousingName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WarehousingName", value, value.ToString());
				_warehousingName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "入库日期", ShortCode = "WarehousingDate", Desc = "入库日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? WarehousingDate
		{
			get { return _warehousingDate; }
			set { _warehousingDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "入库标志", ShortCode = "WarehousingFlag", Desc = "入库标志", ContextType = SysDic.All, Length = 1)]
        public virtual bool WarehousingFlag
		{
			get { return _warehousingFlag; }
			set { _warehousingFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "血型表", ShortCode = "BloodABO", Desc = "血型表")]
		public virtual BloodABO BloodABO
		{
			get { return _bloodABO; }
			set { _bloodABO = value; }
		}

        [DataMember]
        [DataDesc(CName = "病人就诊记录信息表", ShortCode = "BloodPatinfo", Desc = "病人就诊记录信息表")]
		public virtual BloodPatinfo BloodPatinfo
		{
			get { return _bloodPatinfo; }
			set { _bloodPatinfo = value; }
		}

        [DataMember]
        [DataDesc(CName = "血制品字典", ShortCode = "BloodStyle", Desc = "血制品字典")]
		public virtual BloodStyle BloodStyle
		{
			get { return _bloodStyle; }
			set { _bloodStyle = value; }
		}

        
		#endregion
	}
	#endregion
}