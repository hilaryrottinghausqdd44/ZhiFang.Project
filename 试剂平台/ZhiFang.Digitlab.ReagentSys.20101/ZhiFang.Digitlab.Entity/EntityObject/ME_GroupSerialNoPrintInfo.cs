using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEGroupSerialNoPrintInfo

	/// <summary>
	/// MEGroupSerialNoPrintInfo object for NHibernate mapped table 'ME_GroupSerialNoPrintInfo'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "小组条码打印记录表", ClassCName = "MEGroupSerialNoPrintInfo", ShortCode = "MEGroupSerialNoPrintInfo", Desc = "小组条码打印记录表")]
	public class MEGroupSerialNoPrintInfo : BaseEntity
	{
		#region Member Variables
		
        protected string _serialNo;
        protected string _dataAddUser;
        protected long? _dataAddUserID;
        protected string _dataAddComputer;
		protected MEGroupSampleForm _mEGroupSampleForm;
		protected MEPTSampleForm _mEPTSampleForm;

		#endregion

		#region Constructors

		public MEGroupSerialNoPrintInfo() { }

		public MEGroupSerialNoPrintInfo( long labID, string serialNo, DateTime dataAddTime, string dataAddUser, long dataAddUserID, string dataAddComputer, byte[] dataTimeStamp, MEGroupSampleForm mEGroupSampleForm, MEPTSampleForm mEPTSampleForm )
		{
			this._labID = labID;
			this._serialNo = serialNo;
			this._dataAddTime = dataAddTime;
			this._dataAddUser = dataAddUser;
			this._dataAddUserID = dataAddUserID;
			this._dataAddComputer = dataAddComputer;
			this._dataTimeStamp = dataTimeStamp;
			this._mEGroupSampleForm = mEGroupSampleForm;
			this._mEPTSampleForm = mEPTSampleForm;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "条码", ShortCode = "SerialNo", Desc = "条码", ContextType = SysDic.All, Length = 30)]
        public virtual string SerialNo
		{
			get { return _serialNo; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for SerialNo", value, value.ToString());
				_serialNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "打印人", ShortCode = "DataAddUser", Desc = "打印人", ContextType = SysDic.All, Length = 50)]
        public virtual string DataAddUser
		{
			get { return _dataAddUser; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DataAddUser", value, value.ToString());
				_dataAddUser = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "打印人ID", ShortCode = "DataAddUserID", Desc = "打印人ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? DataAddUserID
		{
			get { return _dataAddUserID; }
			set { _dataAddUserID = value; }
		}

        [DataMember]
        [DataDesc(CName = "打印计算机名", ShortCode = "DataAddComputer", Desc = "打印计算机名", ContextType = SysDic.All, Length = 40)]
        public virtual string DataAddComputer
		{
			get { return _dataAddComputer; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for DataAddComputer", value, value.ToString());
				_dataAddComputer = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "小组样本单", ShortCode = "MEGroupSampleForm", Desc = "小组样本单")]
		public virtual MEGroupSampleForm MEGroupSampleForm
		{
			get { return _mEGroupSampleForm; }
			set { _mEGroupSampleForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "样本单", ShortCode = "MEPTSampleForm", Desc = "样本单")]
		public virtual MEPTSampleForm MEPTSampleForm
		{
			get { return _mEPTSampleForm; }
			set { _mEPTSampleForm = value; }
		}

        
		#endregion
	}
	#endregion
}