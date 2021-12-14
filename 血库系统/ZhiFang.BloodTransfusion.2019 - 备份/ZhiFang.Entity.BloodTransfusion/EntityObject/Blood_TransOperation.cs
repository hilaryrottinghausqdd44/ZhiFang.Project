using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodTransOperation

	/// <summary>
	/// BloodTransOperation object for NHibernate mapped table 'Blood_TransOperation'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "", ClassCName = "BloodTransOperation", ShortCode = "BloodTransOperation", Desc = "")]
	public class BloodTransOperation : BaseEntity
	{
		#region Member Variables

		protected int _bloodNo;
		protected int _contentTypeID;
		protected string _businessCode;
		protected string _memo;
		protected int _dispOrder;
		protected bool _isUse;
		protected long? _creatorID;
		protected string _creatorName;

		protected BloodBOutItem _bloodBOutItem;
		protected Bloodstyle _bloodstyle;
		protected BloodTransForm _bloodTransForm;
		protected BloodTransRecordType _bloodTransRecordType;
		#endregion

		#region Constructors

		public BloodTransOperation() { }

		public BloodTransOperation(BloodBOutItem bloodBOutItem, Bloodstyle bloodstyle, BloodTransForm bloodTransForm, BloodTransRecordType bloodTransRecordType,long labID, int contentTypeID, string businessCode, string memo, int dispOrder, bool isUse, long creatorID, string creatorName, DateTime dataAddTime, byte[] dataTimeStamp)
		{
			this._bloodBOutItem = bloodBOutItem;
			this._bloodTransForm = bloodTransForm;
			this._bloodTransRecordType = bloodTransRecordType;
			this._bloodstyle = bloodstyle;

			this._labID = labID;
			this._contentTypeID = contentTypeID;
			this._businessCode = businessCode;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._creatorID = creatorID;
			this._creatorName = creatorName;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties
		[DataMember]
		[DataDesc(CName = "BloodBOutItem", ShortCode = "BloodBOutItem", Desc = "BloodBOutItem")]
		public virtual BloodBOutItem BloodBOutItem
		{
			get { return _bloodBOutItem; }
			set { _bloodBOutItem = value; }
		}
		[DataMember]
		[DataDesc(CName = "Bloodstyle", ShortCode = "Bloodstyle", Desc = "")]
		public virtual Bloodstyle Bloodstyle
		{
			get { return _bloodstyle; }
			set { _bloodstyle = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "BloodTransForm", Desc = "")]
		public virtual BloodTransForm BloodTransForm
		{
			get { return _bloodTransForm; }
			set { _bloodTransForm = value; }
		}
		[DataMember]
		[DataDesc(CName = "BloodTransRecordType", ShortCode = "BloodTransRecordType", Desc = "BloodTransRecordType")]
		public virtual BloodTransRecordType BloodTransRecordType
		{
			get { return _bloodTransRecordType; }
			set { _bloodTransRecordType = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ContentTypeID", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int ContentTypeID
		{
			get { return _contentTypeID; }
			set { _contentTypeID = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "BusinessCode", Desc = "", ContextType = SysDic.All, Length = 40)]
		public virtual string BusinessCode
		{
			get { return _businessCode; }
			set
			{
				if (value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for BusinessCode", value, value.ToString());
				_businessCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = 5000)]
		public virtual string Memo
		{
			get { return _memo; }
			set
			{
				if (value != null && value.Length > 5000)
					throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
				_memo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "CreatorID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? CreatorID
		{
			get { return _creatorID; }
			set { _creatorID = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "CreatorName", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string CreatorName
		{
			get { return _creatorName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CreatorName", value, value.ToString());
				_creatorName = value;
			}
		}


		#endregion
	}
	#endregion
}