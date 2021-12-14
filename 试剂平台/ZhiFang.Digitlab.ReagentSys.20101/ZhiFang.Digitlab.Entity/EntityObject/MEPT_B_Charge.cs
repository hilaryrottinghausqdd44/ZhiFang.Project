using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTBCharge

	/// <summary>
	/// MEPTBCharge object for NHibernate mapped table 'MEPT_B_Charge'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "费用表", ClassCName = "MEPTBCharge", ShortCode = "MEPTBCharge", Desc = "费用表")]
    public class MEPTBCharge : BaseEntity
	{
		#region Member Variables
		
        
        protected long? _chargeTypeID;
        protected string _hisCode;
        protected decimal _chargeValue;
        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _shortcode;
        protected int _dispOrder;
        protected string _comment1;
        protected string _comment2;
        protected string _comment3;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        

		#endregion

		#region Constructors

		public MEPTBCharge() { }

		public MEPTBCharge( long labID, long chargeTypeID, string hisCode, decimal chargeValue, string cName, string eName, string sName, string shortcode, int dispOrder, string comment1, string comment2, string comment3, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._chargeTypeID = chargeTypeID;
			this._hisCode = hisCode;
			this._chargeValue = chargeValue;
			this._cName = cName;
			this._eName = eName;
			this._sName = sName;
			this._shortcode = shortcode;
			this._dispOrder = dispOrder;
			this._comment1 = comment1;
			this._comment2 = comment2;
			this._comment3 = comment3;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "费用类型ID", ShortCode = "ChargeTypeID", Desc = "费用类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ChargeTypeID
		{
			get { return _chargeTypeID; }
			set { _chargeTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "HIS对照码", ShortCode = "HisCode", Desc = "HIS对照码", ContextType = SysDic.All, Length = 50)]
        public virtual string HisCode
		{
			get { return _hisCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for HisCode", value, value.ToString());
				_hisCode = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "费用", ShortCode = "ChargeValue", Desc = "费用", ContextType = SysDic.All, Length = 8)]
        public virtual decimal ChargeValue
		{
			get { return _chargeValue; }
			set { _chargeValue = value; }
		}

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 50)]
        public virtual string EName
		{
			get { return _eName; }
			set
			{
				if ( value != null && value.Length > 50)
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
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 20)]
        public virtual string Shortcode
		{
			get { return _shortcode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
				_shortcode = value;
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
        [DataDesc(CName = "备注1", ShortCode = "Comment1", Desc = "备注1", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment1
		{
			get { return _comment1; }
			set
			{
				_comment1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注2", ShortCode = "Comment2", Desc = "备注2", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment2
		{
			get { return _comment2; }
			set
			{
				_comment2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注3", ShortCode = "Comment3", Desc = "备注3", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment3
		{
			get { return _comment3; }
			set
			{
				_comment3 = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		#endregion
	}
	#endregion
}