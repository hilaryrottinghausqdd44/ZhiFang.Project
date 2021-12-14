using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEMicroDSTResultReportPublication

	/// <summary>
	/// MEMicroDSTResultReportPublication object for NHibernate mapped table 'ME_MicroDSTResultReportPublication'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物报告药敏结果发布记录表", ClassCName = "MEMicroDSTResultReportPublication", ShortCode = "MEMicroDSTResultReportPublication", Desc = "微生物报告药敏结果发布记录表")]
	public class MEMicroDSTResultReportPublication : BaseEntity
	{
		#region Member Variables
		
        protected string _dataAddUser;
        protected long? _dataAddUserID;
        protected string _dataAddComputer;
		protected MEGroupSampleReportPublication _mEGroupSampleReportPublication;
		protected MEMicroDSTValue _mEMicroDSTValue;

		#endregion

		#region Constructors

		public MEMicroDSTResultReportPublication() { }

		public MEMicroDSTResultReportPublication( long labID, DateTime dataAddTime, string dataAddUser, long dataAddUserID, string dataAddComputer, byte[] dataTimeStamp, MEGroupSampleReportPublication mEGroupSampleReportPublication, MEMicroDSTValue mEMicroDSTValue )
		{
			this._labID = labID;
			this._dataAddTime = dataAddTime;
			this._dataAddUser = dataAddUser;
			this._dataAddUserID = dataAddUserID;
			this._dataAddComputer = dataAddComputer;
			this._dataTimeStamp = dataTimeStamp;
			this._mEGroupSampleReportPublication = mEGroupSampleReportPublication;
			this._mEMicroDSTValue = mEMicroDSTValue;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "发布人", ShortCode = "DataAddUser", Desc = "发布人", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "发布人ID", ShortCode = "DataAddUserID", Desc = "发布人ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? DataAddUserID
		{
			get { return _dataAddUserID; }
			set { _dataAddUserID = value; }
		}

        [DataMember]
        [DataDesc(CName = "发布计算机名", ShortCode = "DataAddComputer", Desc = "发布计算机名", ContextType = SysDic.All, Length = 40)]
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
        [DataDesc(CName = "小组样本单报告发布记录表", ShortCode = "MEGroupSampleReportPublication", Desc = "小组样本单报告发布记录表")]
		public virtual MEGroupSampleReportPublication MEGroupSampleReportPublication
		{
			get { return _mEGroupSampleReportPublication; }
			set { _mEGroupSampleReportPublication = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物药敏结果drug sensitive test", ShortCode = "MEMicroDSTValue", Desc = "微生物药敏结果drug sensitive test")]
		public virtual MEMicroDSTValue MEMicroDSTValue
		{
			get { return _mEMicroDSTValue; }
			set { _mEMicroDSTValue = value; }
		}

        
		#endregion
	}
	#endregion
}