using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEMicroBCBottleManageInfo

	/// <summary>
	/// MEMicroBCBottleManageInfo object for NHibernate mapped table 'ME_MicroBCBottleManageInfo'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物血培养瓶管理信息表", ClassCName = "MEMicroBCBottleManageInfo", ShortCode = "MEMicroBCBottleManageInfo", Desc = "微生物血培养瓶管理信息表")]
	public class MEMicroBCBottleManageInfo : BaseEntity
	{
		#region Member Variables
		
        protected string _bottleSerialNo;
        protected string _microBCBMName;
        protected string _microBCBTName;
        protected DateTime? _periodOfValidity;
        protected int _alarmBefore;
        protected int _bottleState;
        protected long? _dataAddPersonID;
        protected string _dataAddPerson;
        protected string _dataAddComputer;
        protected string _dataAddComputerIP;
        protected DateTime? _receivingTime;
        protected long? _receivingPersonID;
        protected string _receivingPerson;
        protected long? _receivingDeptID;
        protected string _receivingDept;
        protected long? _receivingOperatorID;
        protected string _receivingOperator;
        protected string _receivingComputer;
        protected string _receivingComputerIP;
        protected string _returnSerialNo;
        protected DateTime? _returnTime;
        protected long? _returnOperatorID;
        protected string _returnOperator;
        protected string _returnComputer;
        protected string _returnComputerIP;
        protected DateTime? _dataUpdateTime;
		protected BMicroBloodCultureBottleManufacturer _bMicroBloodCultureBottleManufacturer;
		protected BMicroBloodCultureBottleType _bMicroBloodCultureBottleType;

		#endregion

		#region Constructors

		public MEMicroBCBottleManageInfo() { }

		public MEMicroBCBottleManageInfo( long labID, string bottleSerialNo, string microBCBMName, string microBCBTName, DateTime periodOfValidity, int alarmBefore, int bottleState, DateTime dataAddTime, long dataAddPersonID, string dataAddPerson, string dataAddComputer, string dataAddComputerIP, DateTime receivingTime, long receivingPersonID, string receivingPerson, long receivingDeptID, string receivingDept, long receivingOperatorID, string receivingOperator, string receivingComputer, string receivingComputerIP, string returnSerialNo, DateTime returnTime, long returnOperatorID, string returnOperator, string returnComputer, string returnComputerIP, DateTime dataUpdateTime, byte[] dataTimeStamp, BMicroBloodCultureBottleManufacturer bMicroBloodCultureBottleManufacturer, BMicroBloodCultureBottleType bMicroBloodCultureBottleType )
		{
			this._labID = labID;
			this._bottleSerialNo = bottleSerialNo;
			this._microBCBMName = microBCBMName;
			this._microBCBTName = microBCBTName;
			this._periodOfValidity = periodOfValidity;
			this._alarmBefore = alarmBefore;
			this._bottleState = bottleState;
			this._dataAddTime = dataAddTime;
			this._dataAddPersonID = dataAddPersonID;
			this._dataAddPerson = dataAddPerson;
			this._dataAddComputer = dataAddComputer;
			this._dataAddComputerIP = dataAddComputerIP;
			this._receivingTime = receivingTime;
			this._receivingPersonID = receivingPersonID;
			this._receivingPerson = receivingPerson;
			this._receivingDeptID = receivingDeptID;
			this._receivingDept = receivingDept;
			this._receivingOperatorID = receivingOperatorID;
			this._receivingOperator = receivingOperator;
			this._receivingComputer = receivingComputer;
			this._receivingComputerIP = receivingComputerIP;
			this._returnSerialNo = returnSerialNo;
			this._returnTime = returnTime;
			this._returnOperatorID = returnOperatorID;
			this._returnOperator = returnOperator;
			this._returnComputer = returnComputer;
			this._returnComputerIP = returnComputerIP;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bMicroBloodCultureBottleManufacturer = bMicroBloodCultureBottleManufacturer;
			this._bMicroBloodCultureBottleType = bMicroBloodCultureBottleType;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "培养瓶条码", ShortCode = "BottleSerialNo", Desc = "培养瓶条码", ContextType = SysDic.All, Length = 30)]
        public virtual string BottleSerialNo
		{
			get { return _bottleSerialNo; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for BottleSerialNo", value, value.ToString());
				_bottleSerialNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "血培养瓶厂商名称", ShortCode = "MicroBCBMName", Desc = "血培养瓶厂商名称", ContextType = SysDic.All, Length = 50)]
        public virtual string MicroBCBMName
		{
			get { return _microBCBMName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for MicroBCBMName", value, value.ToString());
				_microBCBMName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "血培养瓶类型名称", ShortCode = "MicroBCBTName", Desc = "血培养瓶类型名称", ContextType = SysDic.All, Length = 50)]
        public virtual string MicroBCBTName
		{
			get { return _microBCBTName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for MicroBCBTName", value, value.ToString());
				_microBCBTName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "有效期至", ShortCode = "PeriodOfValidity", Desc = "有效期至", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? PeriodOfValidity
		{
			get { return _periodOfValidity; }
			set { _periodOfValidity = value; }
		}

        [DataMember]
        [DataDesc(CName = "领用报警提前量：单位为“天”", ShortCode = "AlarmBefore", Desc = "领用报警提前量：单位为“天”", ContextType = SysDic.All, Length = 4)]
        public virtual int AlarmBefore
		{
			get { return _alarmBefore; }
			set { _alarmBefore = value; }
		}

        [DataMember]
        [DataDesc(CName = "血培养瓶状态：1-入库；2-领取；3-接收；4-失效", ShortCode = "BottleState", Desc = "血培养瓶状态：1-入库；2-领取；3-接收；4-失效", ContextType = SysDic.All, Length = 4)]
        public virtual int BottleState
		{
			get { return _bottleState; }
			set { _bottleState = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "入库登记人ID", ShortCode = "DataAddPersonID", Desc = "入库登记人ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? DataAddPersonID
		{
			get { return _dataAddPersonID; }
			set { _dataAddPersonID = value; }
		}

        [DataMember]
        [DataDesc(CName = "入库登记人姓名", ShortCode = "DataAddPerson", Desc = "入库登记人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string DataAddPerson
		{
			get { return _dataAddPerson; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DataAddPerson", value, value.ToString());
				_dataAddPerson = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "入库操作计算机名", ShortCode = "DataAddComputer", Desc = "入库操作计算机名", ContextType = SysDic.All, Length = 50)]
        public virtual string DataAddComputer
		{
			get { return _dataAddComputer; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DataAddComputer", value, value.ToString());
				_dataAddComputer = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "入库操作计算机IP", ShortCode = "DataAddComputerIP", Desc = "入库操作计算机IP", ContextType = SysDic.All, Length = 30)]
        public virtual string DataAddComputerIP
		{
			get { return _dataAddComputerIP; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for DataAddComputerIP", value, value.ToString());
				_dataAddComputerIP = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "领用时间", ShortCode = "ReceivingTime", Desc = "领用时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReceivingTime
		{
			get { return _receivingTime; }
			set { _receivingTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "领用人ID", ShortCode = "ReceivingPersonID", Desc = "领用人ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? ReceivingPersonID
		{
			get { return _receivingPersonID; }
			set { _receivingPersonID = value; }
		}

        [DataMember]
        [DataDesc(CName = "领用人姓名", ShortCode = "ReceivingPerson", Desc = "领用人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string ReceivingPerson
		{
			get { return _receivingPerson; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ReceivingPerson", value, value.ToString());
				_receivingPerson = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "领用科室ID", ShortCode = "ReceivingDeptID", Desc = "领用科室ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? ReceivingDeptID
		{
			get { return _receivingDeptID; }
			set { _receivingDeptID = value; }
		}

        [DataMember]
        [DataDesc(CName = "领用科室", ShortCode = "ReceivingDept", Desc = "领用科室", ContextType = SysDic.All, Length = 50)]
        public virtual string ReceivingDept
		{
			get { return _receivingDept; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ReceivingDept", value, value.ToString());
				_receivingDept = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "领用登记人ID", ShortCode = "ReceivingOperatorID", Desc = "领用登记人ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? ReceivingOperatorID
		{
			get { return _receivingOperatorID; }
			set { _receivingOperatorID = value; }
		}

        [DataMember]
        [DataDesc(CName = "领用登记人姓名", ShortCode = "ReceivingOperator", Desc = "领用登记人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string ReceivingOperator
		{
			get { return _receivingOperator; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ReceivingOperator", value, value.ToString());
				_receivingOperator = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "领用操作计算机名", ShortCode = "ReceivingComputer", Desc = "领用操作计算机名", ContextType = SysDic.All, Length = 50)]
        public virtual string ReceivingComputer
		{
			get { return _receivingComputer; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ReceivingComputer", value, value.ToString());
				_receivingComputer = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "领用操作计算机IP", ShortCode = "ReceivingComputerIP", Desc = "领用操作计算机IP", ContextType = SysDic.All, Length = 30)]
        public virtual string ReceivingComputerIP
		{
			get { return _receivingComputerIP; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for ReceivingComputerIP", value, value.ToString());
				_receivingComputerIP = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "返还医嘱条码", ShortCode = "ReturnSerialNo", Desc = "返还医嘱条码", ContextType = SysDic.All, Length = 30)]
        public virtual string ReturnSerialNo
		{
			get { return _returnSerialNo; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for ReturnSerialNo", value, value.ToString());
				_returnSerialNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "返还时间", ShortCode = "ReturnTime", Desc = "返还时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReturnTime
		{
			get { return _returnTime; }
			set { _returnTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "返还登记人ID", ShortCode = "ReturnOperatorID", Desc = "返还登记人ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? ReturnOperatorID
		{
			get { return _returnOperatorID; }
			set { _returnOperatorID = value; }
		}

        [DataMember]
        [DataDesc(CName = "返还登记人姓名", ShortCode = "ReturnOperator", Desc = "返还登记人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string ReturnOperator
		{
			get { return _returnOperator; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ReturnOperator", value, value.ToString());
				_returnOperator = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "返还操作计算机名", ShortCode = "ReturnComputer", Desc = "返还操作计算机名", ContextType = SysDic.All, Length = 50)]
        public virtual string ReturnComputer
		{
			get { return _returnComputer; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ReturnComputer", value, value.ToString());
				_returnComputer = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "返还操作计算机IP", ShortCode = "ReturnComputerIP", Desc = "返还操作计算机IP", ContextType = SysDic.All, Length = 30)]
        public virtual string ReturnComputerIP
		{
			get { return _returnComputerIP; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for ReturnComputerIP", value, value.ToString());
				_returnComputerIP = value;
			}
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
        [DataDesc(CName = "微生物血培养瓶厂商", ShortCode = "BMicroBloodCultureBottleManufacturer", Desc = "微生物血培养瓶厂商")]
		public virtual BMicroBloodCultureBottleManufacturer BMicroBloodCultureBottleManufacturer
		{
			get { return _bMicroBloodCultureBottleManufacturer; }
			set { _bMicroBloodCultureBottleManufacturer = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物血培养瓶类型", ShortCode = "BMicroBloodCultureBottleType", Desc = "微生物血培养瓶类型")]
		public virtual BMicroBloodCultureBottleType BMicroBloodCultureBottleType
		{
			get { return _bMicroBloodCultureBottleType; }
			set { _bMicroBloodCultureBottleType = value; }
		}

        
		#endregion
	}
	#endregion
}