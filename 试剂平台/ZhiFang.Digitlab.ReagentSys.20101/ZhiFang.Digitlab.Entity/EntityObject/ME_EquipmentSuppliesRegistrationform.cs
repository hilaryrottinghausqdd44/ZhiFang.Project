using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEEquipmentSuppliesRegistrationform

	/// <summary>
	/// MEEquipmentSuppliesRegistrationform object for NHibernate mapped table 'ME_EquipmentSuppliesRegistrationform'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "检验设备耗材登记表", ClassCName = "MEEquipmentSuppliesRegistrationform", ShortCode = "MEEquipmentSuppliesRegistrationform", Desc = "检验设备耗材登记表")]
	public class MEEquipmentSuppliesRegistrationform : BaseEntity
	{
		#region Member Variables
		
        protected string _eSRFSerialNo;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dateOfManufacture;
        protected DateTime? _validDate;
        protected DateTime? _dataUpdateTime;
		protected BEquipmentSuppliesType _bEquipmentSuppliesType;
		protected HREmployee _hREmployee;
		protected IList<MEEquipmentSuppliesUseInfo> _mEEquipmentSuppliesUseInfoList; 

		#endregion

		#region Constructors

		public MEEquipmentSuppliesRegistrationform() { }

		public MEEquipmentSuppliesRegistrationform( long labID, string eSRFSerialNo, string comment, bool isUse, int dispOrder, DateTime dateOfManufacture, DateTime validDate, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BEquipmentSuppliesType bEquipmentSuppliesType, HREmployee hREmployee )
		{
			this._labID = labID;
			this._eSRFSerialNo = eSRFSerialNo;
			this._comment = comment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dateOfManufacture = dateOfManufacture;
			this._validDate = validDate;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bEquipmentSuppliesType = bEquipmentSuppliesType;
			this._hREmployee = hREmployee;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "设备耗材条形码", ShortCode = "ESRFSerialNo", Desc = "设备耗材条形码", ContextType = SysDic.All, Length = 30)]
        public virtual string ESRFSerialNo
		{
			get { return _eSRFSerialNo; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for ESRFSerialNo", value, value.ToString());
				_eSRFSerialNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "生产日期", ShortCode = "DateOfManufacture", Desc = "生产日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DateOfManufacture
		{
			get { return _dateOfManufacture; }
			set { _dateOfManufacture = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "有效日期至", ShortCode = "ValidDate", Desc = "有效日期至", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ValidDate
		{
			get { return _validDate; }
			set { _validDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "检验设备耗材类型表", ShortCode = "BEquipmentSuppliesType", Desc = "检验设备耗材类型表")]
		public virtual BEquipmentSuppliesType BEquipmentSuppliesType
		{
			get { return _bEquipmentSuppliesType; }
			set { _bEquipmentSuppliesType = value; }
		}

        [DataMember]
        [DataDesc(CName = "员工", ShortCode = "HREmployee", Desc = "员工")]
		public virtual HREmployee HREmployee
		{
			get { return _hREmployee; }
			set { _hREmployee = value; }
		}

        [DataMember]
        [DataDesc(CName = "检验设备耗材使用记录表", ShortCode = "MEEquipmentSuppliesUseInfoList", Desc = "检验设备耗材使用记录表")]
		public virtual IList<MEEquipmentSuppliesUseInfo> MEEquipmentSuppliesUseInfoList
		{
			get
			{
				if (_mEEquipmentSuppliesUseInfoList==null)
				{
					_mEEquipmentSuppliesUseInfoList = new List<MEEquipmentSuppliesUseInfo>();
				}
				return _mEEquipmentSuppliesUseInfoList;
			}
			set { _mEEquipmentSuppliesUseInfoList = value; }
		}

        
		#endregion
	}
	#endregion
}