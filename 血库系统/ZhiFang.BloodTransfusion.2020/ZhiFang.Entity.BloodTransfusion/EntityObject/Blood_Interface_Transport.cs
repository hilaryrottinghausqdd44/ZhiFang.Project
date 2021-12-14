using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodInterfaceTransport

	/// <summary>
	/// BloodInterfaceTransport object for NHibernate mapped table 'Blood_Interface_Transport'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "接口传输记录表", ClassCName = "BloodInterfaceTransport", ShortCode = "BloodInterfaceTransport", Desc = "接口传输记录表")]
	public class BloodInterfaceTransport : BaseEntity
	{
		#region Member Variables
		
        protected long _iD;
        protected string _typeName;
        protected string _dockingParty;
        protected DateTime? _invokeTime;
        protected string _operatId;
        protected string _operatName;
        protected string _tableName;
        protected string _primaryKey;
        protected string _transportContent;
        protected string _returnResult;
        protected string _status;
        protected string _description;
        protected string _singleNumber;
        protected int _dispOrder;
        protected bool _visible;

		#endregion

		#region Constructors

		public BloodInterfaceTransport() { }

		public BloodInterfaceTransport( long labID, long iD, string typeName, string dockingParty, DateTime invokeTime, string operatId, string operatName, string tableName, string primaryKey, string transportContent, string returnResult, string status, string description, string singleNumber, int dispOrder, bool visible, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._iD = iD;
			this._typeName = typeName;
			this._dockingParty = dockingParty;
			this._invokeTime = invokeTime;
			this._operatId = operatId;
			this._operatName = operatName;
			this._tableName = tableName;
			this._primaryKey = primaryKey;
			this._transportContent = transportContent;
			this._returnResult = returnResult;
			this._status = status;
			this._description = description;
			this._singleNumber = singleNumber;
			this._dispOrder = dispOrder;
			this._visible = visible;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "自增编号", ShortCode = "ID", Desc = "自增编号", ContextType = SysDic.All, Length = 8)]
        public virtual long ID
		{
			get { return _iD; }
			set { _iD = value; }
		}

        [DataMember]
        [DataDesc(CName = "接口类型", ShortCode = "TypeName", Desc = "接口类型", ContextType = SysDic.All, Length = 100)]
        public virtual string TypeName
		{
			get { return _typeName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for TypeName", value, value.ToString());
				_typeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "接口对接方", ShortCode = "DockingParty", Desc = "接口对接方", ContextType = SysDic.All, Length = 100)]
        public virtual string DockingParty
		{
			get { return _dockingParty; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for DockingParty", value, value.ToString());
				_dockingParty = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "接口调用时间", ShortCode = "InvokeTime", Desc = "接口调用时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? InvokeTime
		{
			get { return _invokeTime; }
			set { _invokeTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "接口操作者编号", ShortCode = "OperatId", Desc = "接口操作者编号", ContextType = SysDic.All, Length = 20)]
        public virtual string OperatId
		{
			get { return _operatId; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for OperatId", value, value.ToString());
				_operatId = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "接口操作者姓名", ShortCode = "OperatName", Desc = "接口操作者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string OperatName
		{
			get { return _operatName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for OperatName", value, value.ToString());
				_operatName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "接口操作表", ShortCode = "TableName", Desc = "接口操作表", ContextType = SysDic.All, Length = 50)]
        public virtual string TableName
		{
			get { return _tableName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TableName", value, value.ToString());
				_tableName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "接口操作表主键", ShortCode = "PrimaryKey", Desc = "接口操作表主键", ContextType = SysDic.All, Length = 50)]
        public virtual string PrimaryKey
		{
			get { return _primaryKey; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PrimaryKey", value, value.ToString());
				_primaryKey = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "接口调用传输内容", ShortCode = "TransportContent", Desc = "接口调用传输内容", ContextType = SysDic.All, Length = 16)]
        public virtual string TransportContent
		{
			get { return _transportContent; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for TransportContent", value, value.ToString());
				_transportContent = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "返回结果", ShortCode = "ReturnResult", Desc = "返回结果", ContextType = SysDic.All, Length = 16)]
        public virtual string ReturnResult
		{
			get { return _returnResult; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for ReturnResult", value, value.ToString());
				_returnResult = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "状态", ShortCode = "Status", Desc = "状态", ContextType = SysDic.All, Length = 20)]
        public virtual string Status
		{
			get { return _status; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Status", value, value.ToString());
				_status = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "描述", ShortCode = "Description", Desc = "描述", ContextType = SysDic.All, Length = 100)]
        public virtual string Description
		{
			get { return _description; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Description", value, value.ToString());
				_description = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "返回的单号", ShortCode = "SingleNumber", Desc = "返回的单号", ContextType = SysDic.All, Length = 200)]
        public virtual string SingleNumber
		{
			get { return _singleNumber; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for SingleNumber", value, value.ToString());
				_singleNumber = value;
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

        
		#endregion
	}
	#endregion
}