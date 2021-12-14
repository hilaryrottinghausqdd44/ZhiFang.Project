using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.ReportFormQueryPrint.Model
{
	#region BParameter

	/// <summary>
	/// BParameter object for NHibernate mapped table 'B_Parameter'.
	/// </summary>
	[DataContract]
	public class BParameter 
	{
		#region Member Variables
		private long _parameterID;
		private long _labID;
		protected string _name;
		protected string _sName;
		protected string _paraType;
		protected string _paraNo;
		protected string _paraValue;
		protected string _site;
		protected string _paraDesc;
		protected bool _isUse;
		protected string _shortCode;
		protected string _pinYinZiTou;
		protected long _nodeID;
		protected long _groupNo;
		protected int _dispOrder;
		protected long _pDictId;
		private DateTime? _DataAddTime;
		private DateTime? _DataUpdateTime;
		private byte[] _dataTimeStamp;


		#endregion

		#region Constructors

		public BParameter() { }

		public BParameter(string name, string sName, string paraType, string paraNo, string paraValue, string site, string paraDesc, DateTime dataUpdateTime, bool isUse, string shortCode, string pinYinZiTou, long labID, long nodeID, long groupNo, DateTime dataAddTime, byte[] dataTimeStamp, int dispOrder, long pDictId)
		{
			this._name = name;
			this._sName = sName;
			this._paraType = paraType;
			this._paraNo = paraNo;
			this._paraValue = paraValue;
			this._site = site;
			this._paraDesc = paraDesc;
			this._DataUpdateTime = dataUpdateTime;
			this._isUse = isUse;
			this._shortCode = shortCode;
			this._pinYinZiTou = pinYinZiTou;
			this._labID = labID;
			this._nodeID = nodeID;
			this._groupNo = groupNo;
			this._DataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._dispOrder = dispOrder;
			this._pDictId = pDictId;
		}

		#endregion

		#region Public Properties


		[DataMember]
		public virtual string Name
		{
			get { return _name; }
			set
			{
				_name = value;
			}
		}

		[DataMember]
		public virtual string SName
		{
			get { return _sName; }
			set
			{
				_sName = value;
			}
		}

		[DataMember]
		public virtual string ParaType
		{
			get { return _paraType; }
			set
			{
				_paraType = value;
			}
		}

		[DataMember]
		public virtual string ParaNo
		{
			get { return _paraNo; }
			set
			{
				_paraNo = value;
			}
		}

		[DataMember]
		public virtual string ParaValue
		{
			get { return _paraValue; }
			set
			{
				_paraValue = value;
			}
		}

		[DataMember]
		public virtual string Site
		{
			get { return _site; }
			set
			{
				_site = value;
			}
		}

		[DataMember]
		public virtual string ParaDesc
		{
			get { return _paraDesc; }
			set
			{
				_paraDesc = value;
			}
		}

		[DataMember]
		public virtual DateTime? DataUpdateTime
		{
			get { return _DataUpdateTime; }
			set { _DataUpdateTime = value; }
		}

		[DataMember]
		public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

		[DataMember]
		public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
			}
		}

		[DataMember]
		public virtual string PinYinZiTou
		{
			get { return _pinYinZiTou; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
				_pinYinZiTou = value;
			}
		}

		[DataMember]
		public virtual long NodeID
		{
			get { return _nodeID; }
			set { _nodeID = value; }
		}

		[DataMember]
		public virtual long GroupNo
		{
			get { return _groupNo; }
			set { _groupNo = value; }
		}

		[DataMember]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

		[DataMember]
		public virtual long PDictId
		{
			get { return _pDictId; }
			set { _pDictId = value; }
		}

		[DataMember]
		public virtual long ParameterID { get => _parameterID; set => _parameterID = value; }
		[DataMember]
		public virtual long LabID { get => _labID; set => _labID = value; }
		[DataMember]
		public virtual DateTime? DataAddTime
		{
			set { _DataAddTime = value; }
			get { return _DataAddTime; }
		}
		[DataMember]
		public virtual byte[] DataTimeStamp
		{
			set { _dataTimeStamp = value; }
			get { return _dataTimeStamp; }
		}

		#endregion
	}
	#endregion
}