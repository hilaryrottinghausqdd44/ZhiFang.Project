using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEEquipmentSuppliesUseInfo

	/// <summary>
	/// MEEquipmentSuppliesUseInfo object for NHibernate mapped table 'ME_EquipmentSuppliesUseInfo'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "检验设备耗材使用记录表", ClassCName = "MEEquipmentSuppliesUseInfo", ShortCode = "MEEquipmentSuppliesUseInfo", Desc = "检验设备耗材使用记录表")]
	public class MEEquipmentSuppliesUseInfo : BaseEntity
	{
		#region Member Variables
		
        protected string _eSRFSerialNo;
        protected int _operateType;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected HREmployee _hREmployee;
		protected MEEquipmentSuppliesRegistrationform _mEEquipmentSuppliesRegistrationform;
		protected HREmployee _operator;

		#endregion

		#region Constructors

		public MEEquipmentSuppliesUseInfo() { }

		public MEEquipmentSuppliesUseInfo( long labID, string eSRFSerialNo, int operateType, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, HREmployee hREmployee, MEEquipmentSuppliesRegistrationform mEEquipmentSuppliesRegistrationform, HREmployee Operator )
		{
			this._labID = labID;
			this._eSRFSerialNo = eSRFSerialNo;
			this._operateType = operateType;
			this._comment = comment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._hREmployee = hREmployee;
			this._mEEquipmentSuppliesRegistrationform = mEEquipmentSuppliesRegistrationform;
			this._operator = Operator;
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
        [DataDesc(CName = "操作类型：1-领用；2-随医嘱送回", ShortCode = "OperateType", Desc = "操作类型：1-领用；2-随医嘱送回", ContextType = SysDic.All, Length = 4)]
        public virtual int OperateType
		{
			get { return _operateType; }
			set { _operateType = value; }
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
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "员工", ShortCode = "HREmployee", Desc = "员工")]
		public virtual HREmployee HREmployee
		{
			get { return _hREmployee; }
			set { _hREmployee = value; }
		}

        [DataMember]
        [DataDesc(CName = "检验设备耗材登记表", ShortCode = "MEEquipmentSuppliesRegistrationform", Desc = "检验设备耗材登记表")]
		public virtual MEEquipmentSuppliesRegistrationform MEEquipmentSuppliesRegistrationform
		{
			get { return _mEEquipmentSuppliesRegistrationform; }
			set { _mEEquipmentSuppliesRegistrationform = value; }
		}

        [DataMember]
        [DataDesc(CName = "员工", ShortCode = "operator", Desc = "员工")]
		public virtual HREmployee Operator
		{
			get { return _operator; }
			set { _operator = value; }
		}

        
		#endregion
	}
	#endregion
}