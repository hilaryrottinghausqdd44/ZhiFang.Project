using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodTransRecordType

	/// <summary>
	/// BloodTransRecordType object for NHibernate mapped table 'Blood_TransRecordType'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodTransRecordType", ShortCode = "BloodTransRecordType", Desc = "")]
	public class BloodTransRecordType : BaseEntity
	{
		#region Member Variables
		
        protected int _contentTypeID;
        protected string _cName;
        protected string _typeCode;
        protected int _dispOrder;
        protected bool _isVisible;
        protected string _memo;
        protected DateTime? _dataUpdateTime;
		protected long? _transTypeId;
		#endregion

		#region Constructors

		public BloodTransRecordType() { }

		public BloodTransRecordType( long labID, int contentTypeID, long? transTypeId, string cName, string typeCode, int dispOrder, bool isVisible, string memo, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._contentTypeID = contentTypeID;
			this._transTypeId = transTypeId;
			this._cName = cName;
			this._typeCode = typeCode;
			this._dispOrder = dispOrder;
			this._isVisible = isVisible;
			this._memo = memo;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "��Ѫ��¼����", ShortCode = "TransTypeId", Desc = "��Ѫ��¼����", ContextType = SysDic.Number, Length = 8)]
		public virtual long? TransTypeId
		{
			get
			{
				return _transTypeId;
			}
			set { _transTypeId = value; }
		}
		[DataMember]
        [DataDesc(CName = "���ݷ���", ShortCode = "ContentTypeID", Desc = "���ݷ���", ContextType = SysDic.All, Length = 4)]
        public virtual int ContentTypeID
		{
			get { return _contentTypeID; }
			set { _contentTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "���ݷ�������", ShortCode = "CName", Desc = "���ݷ�������", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
        {
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "���ݷ������", ShortCode = "TypeCode", Desc = "���ݷ������", ContextType = SysDic.All, Length = 50)]
        public virtual string TypeCode
		{
			get { return _typeCode; }
			set { _typeCode = value; }
		}

        [DataMember]
        [DataDesc(CName = "��ʾ����", ShortCode = "DispOrder", Desc = "��ʾ����", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "�Ƿ�ʹ��", ShortCode = "IsVisible", Desc = "�Ƿ�ʹ��", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsVisible
		{
			get { return _isVisible; }
			set { _isVisible = value; }
		}

        [DataMember]
        [DataDesc(CName = "��ע", ShortCode = "Memo", Desc = "��ע", ContextType = SysDic.All, Length = 16)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
				_memo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		#endregion
	}
	#endregion
}