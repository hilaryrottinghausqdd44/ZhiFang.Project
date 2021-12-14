using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
	#region ReaBmsTransferDoc

	/// <summary>
	/// ReaBmsTransferDoc object for NHibernate mapped table 'Rea_BmsTransferDoc'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "移库总单表", ClassCName = "ReaBmsTransferDoc", ShortCode = "ReaBmsTransferDoc", Desc = "移库总单表")]
	public class ReaBmsTransferDoc : BaseEntity
	{
		#region Member Variables
		
        protected long? _deptID;
        protected string _deptName;
        protected long _transferType;
        protected string _transferTypeName;
        protected int _status;
        protected string _statusName;
        protected long? _sStorageID;
        protected string _sStorageName;
        protected long? _dStorageID;
        protected string _dStorageName;
        protected long? _OperID;
        protected string _OperName;
        protected DateTime? _operDate;
        protected int _printTimes;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected int _dispOrder;
        protected string _memo;
        protected bool _visible;
        protected long? _createrID;
        protected string _createrName;
        protected DateTime _dataUpdateTime;
        protected string _transferDocNo;
        protected double _totalPrice;
        protected long? _takerID;
        protected string _takerName;
        protected long? _checkID;
        protected string _checkName;
        protected DateTime? _checkTime;
        protected string _checkMemo;
        #endregion

        #region Constructors

        public ReaBmsTransferDoc() { }

		public ReaBmsTransferDoc( long labID, long deptID, string deptName, long transferType, string transferTypeName, int status, string statusName, long sStorageID, string sStorageName, long dStorageID, string dStorageName, long operID, string operName, DateTime operDate, int printTimes, string zX1, string zX2, string zX3, int dispOrder, string memo, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._deptID = deptID;
			this._deptName = deptName;
			this._transferType = transferType;
			this._transferTypeName = transferTypeName;
			this._status = status;
			this._statusName = statusName;
			this._sStorageID = sStorageID;
			this._sStorageName = sStorageName;
			this._dStorageID = dStorageID;
			this._dStorageName = dStorageName;
			this._OperID = operID;
			this._OperName = operName;
			this._operDate = operDate;
			this._printTimes = printTimes;
			this._zX1 = zX1;
			this._zX2 = zX2;
			this._zX3 = zX3;
			this._dispOrder = dispOrder;
			this._memo = memo;
			this._visible = visible;
			this._createrID = createrID;
			this._createrName = createrName;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核时间", ShortCode = "CheckTime", Desc = "审核时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CheckTime
        {
            get { return _checkTime; }
            set { _checkTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核意见", ShortCode = "CheckMemo", Desc = "审核意见", ContextType = SysDic.All, Length = 8)]
        public virtual string CheckMemo
        {
            get { return _checkMemo; }
            set { _checkMemo = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "部门ID", ShortCode = "DeptID", Desc = "部门ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DeptID
		{
			get { return _deptID; }
			set { _deptID = value; }
		}

        [DataMember]
        [DataDesc(CName = "部门名称", ShortCode = "DeptName", Desc = "部门名称", ContextType = SysDic.All, Length = 50)]
        public virtual string DeptName
		{
			get { return _deptName; }
			set
			{				
				_deptName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "移库类型ID", ShortCode = "TransferType", Desc = "移库类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long TransferType
		{
			get { return _transferType; }
			set { _transferType = value; }
		}

        [DataMember]
        [DataDesc(CName = "移库类型名称", ShortCode = "TransferTypeName", Desc = "移库类型名称", ContextType = SysDic.All, Length = 50)]
        public virtual string TransferTypeName
		{
			get { return _transferTypeName; }
			set
			{				
				_transferTypeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "状态ID", ShortCode = "Status", Desc = "状态ID", ContextType = SysDic.All, Length = 4)]
        public virtual int Status
		{
			get { return _status; }
			set { _status = value; }
		}

        [DataMember]
        [DataDesc(CName = "状态名称", ShortCode = "StatusName", Desc = "状态名称", ContextType = SysDic.All, Length = 50)]
        public virtual string StatusName
		{
			get { return _statusName; }
			set
			{
				_statusName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "源库ID", ShortCode = "SStorageID", Desc = "源库ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SStorageID
		{
			get { return _sStorageID; }
			set { _sStorageID = value; }
		}

        [DataMember]
        [DataDesc(CName = "源库名称", ShortCode = "SStorageName", Desc = "源库名称", ContextType = SysDic.All, Length = 50)]
        public virtual string SStorageName
		{
			get { return _sStorageName; }
			set
			{
                _sStorageName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "目的库ID", ShortCode = "DStorageID", Desc = "目的库ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DStorageID
		{
			get { return _dStorageID; }
			set { _dStorageID = value; }
		}

        [DataMember]
        [DataDesc(CName = "目的库名称", ShortCode = "DStorageName", Desc = "目的库名称", ContextType = SysDic.All, Length = 50)]
        public virtual string DStorageName
		{
			get { return _dStorageName; }
			set
			{
				_dStorageName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作者ID", ShortCode = "OperID", Desc = "操作者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperID
        {
			get { return _OperID; }
			set { _OperID = value; }
		}

        [DataMember]
        [DataDesc(CName = "操作者名称", ShortCode = "OperName", Desc = "操作者名称", ContextType = SysDic.All, Length = 50)]
        public virtual string OperName
        {
			get { return _OperName; }
			set
			{
				_OperName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作日期", ShortCode = "OperDate", Desc = "操作日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OperDate
		{
			get { return _operDate; }
			set { _operDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "打印次数", ShortCode = "PrintTimes", Desc = "打印次数", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintTimes
		{
			get { return _printTimes; }
			set { _printTimes = value; }
		}

        [DataMember]
        [DataDesc(CName = "专项1", ShortCode = "ZX1", Desc = "专项1", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX1
		{
			get { return _zX1; }
			set
			{
				_zX1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "专项2", ShortCode = "ZX2", Desc = "专项2", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX2
		{
			get { return _zX2; }
			set
			{
				_zX2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "专项3", ShortCode = "ZX3", Desc = "专项3", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX3
		{
			get { return _zX3; }
			set
			{
				_zX3 = value;
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
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = -1)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				_memo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "创建者ID", ShortCode = "CreaterID", Desc = "创建者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreaterID
		{
			get { return _createrID; }
			set { _createrID = value; }
		}

        [DataMember]
        [DataDesc(CName = "创建者姓名", ShortCode = "CreaterName", Desc = "创建者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string CreaterName
		{
			get { return _createrName; }
			set
			{
				_createrName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TransferDocNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string TransferDocNo
        {
            get { return _transferDocNo; }
            set { _transferDocNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TotalPrice", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TakerID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? TakerID
        {
            get { return _takerID; }
            set { _takerID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TakerName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string TakerName
        {
            get { return _takerName; }
            set { _takerName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CheckID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? CheckID
        {
            get { return _checkID; }
            set { _checkID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CheckName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CheckName
        {
            get { return _checkName; }
            set { _checkName = value; }
        }


        #endregion
    }
	#endregion
}