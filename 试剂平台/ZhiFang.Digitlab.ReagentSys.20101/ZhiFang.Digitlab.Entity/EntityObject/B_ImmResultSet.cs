using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BImmResultSet

	/// <summary>
	/// BImmResultSet object for NHibernate mapped table 'B_ImmResultSet'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "酶免结果信息项设置表：用于存储打印输出、发送等处理过程所需的信息项的各种配置参数", ClassCName = "BImmResultSet", ShortCode = "BImmResultSet", Desc = "酶免结果信息项设置表：用于存储打印输出、发送等处理过程所需的信息项的各种配置参数")]
	public class BImmResultSet : BaseEntity
	{
		#region Member Variables
		
        protected int _immResultSetNo;
        protected string _cName;
        protected string _eName;
        protected bool _isPrint;
        protected bool _isSendResult;
        protected int _sendResultLength;
        protected int _sendResultOrder;
        protected DateTime? _dataUpdateTime;

		#endregion

		#region Constructors

		public BImmResultSet() { }

		public BImmResultSet( long labID, int immResultSetNo, string cName, string eName, bool isPrint, bool isSendResult, int sendResultLength, int sendResultOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._immResultSetNo = immResultSetNo;
			this._cName = cName;
			this._eName = eName;
			this._isPrint = isPrint;
			this._isSendResult = isSendResult;
			this._sendResultLength = sendResultLength;
			this._sendResultOrder = sendResultOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "序号", ShortCode = "ImmResultSetNo", Desc = "序号", ContextType = SysDic.All, Length = 4)]
        public virtual int ImmResultSetNo
		{
			get { return _immResultSetNo; }
			set { _immResultSetNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "中文名称", ShortCode = "CName", Desc = "中文名称", ContextType = SysDic.All, Length = 500)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 500)]
        public virtual string EName
		{
			get { return _eName; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
				_eName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否打印 0或null-不打印 1-打印", ShortCode = "IsPrint", Desc = "是否打印 0或null-不打印 1-打印", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsPrint
		{
			get { return _isPrint; }
			set { _isPrint = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否发送结果 0或null-不发送 1-发送", ShortCode = "IsSendResult", Desc = "是否发送结果 0或null-不发送 1-发送", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsSendResult
		{
			get { return _isSendResult; }
			set { _isSendResult = value; }
		}

        [DataMember]
        [DataDesc(CName = "发送结果长度", ShortCode = "SendResultLength", Desc = "发送结果长度", ContextType = SysDic.All, Length = 4)]
        public virtual int SendResultLength
		{
			get { return _sendResultLength; }
			set { _sendResultLength = value; }
		}

        [DataMember]
        [DataDesc(CName = "发送结果次序", ShortCode = "SendResultOrder", Desc = "发送结果次序", ContextType = SysDic.All, Length = 4)]
        public virtual int SendResultOrder
		{
			get { return _sendResultOrder; }
			set { _sendResultOrder = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "更新时间", ShortCode = "DataUpdateTime", Desc = "更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        
		#endregion
	}
	#endregion
}