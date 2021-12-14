using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodTransRecordTypeItem

	/// <summary>
	/// BloodTransRecordTypeItem object for NHibernate mapped table 'Blood_TransRecordTypeItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodTransRecordTypeItem", ShortCode = "BloodTransRecordTypeItem", Desc = "")]
	public class BloodTransRecordTypeItem : BaseEntity
	{
		#region Member Variables
		
        protected string _transItemCode;
        protected string _cName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _transItemEditInfo;
        protected int _dispOrder;
        protected bool _isVisible;
        protected DateTime? _dataUpdateTime;
		protected BloodTransRecordType _bloodTransRecordType;
		
		#endregion

		#region Constructors

		public BloodTransRecordTypeItem() { }

		public BloodTransRecordTypeItem( long labID, string transItemCode, string transItemName, string sName, string shortcode, string pinYinZiTou, string transItemEditInfo, int dispOrder, bool isVisible, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BloodTransRecordType bloodTransRecordType )
		{
			this._labID = labID;
			this._transItemCode = transItemCode;
			this._cName = transItemName;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._transItemEditInfo = transItemEditInfo;
			this._dispOrder = dispOrder;
			this._isVisible = isVisible;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodTransRecordType = bloodTransRecordType;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "��¼�����", ShortCode = "TransItemCode", Desc = "��¼�����", ContextType = SysDic.All, Length = 50)]
        public virtual string TransItemCode
		{
			get { return _transItemCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TransItemCode", value, value.ToString());
				_transItemCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "��¼������", ShortCode = "CName", Desc = "��¼������", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "���", ShortCode = "SName", Desc = "��¼������", ContextType = SysDic.All, Length = 80)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
				if ( value != null && value.Length > 80)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "�����", ShortCode = "Shortcode", Desc = "�����", ContextType = SysDic.All, Length = 50)]
        public virtual string Shortcode
		{
			get { return _shortcode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
				_shortcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "ƴ����ͷ", ShortCode = "PinYinZiTou", Desc = "ƴ����ͷ", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
		{
			get { return _pinYinZiTou; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
				_pinYinZiTou = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "������Ϣ", ShortCode = "TransItemEditInfo", Desc = "������Ϣ", ContextType = SysDic.All)]
        public virtual string TransItemEditInfo
		{
			get { return _transItemEditInfo; }
			set
			{
				_transItemEditInfo = value;
			}
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "����ʱ��", ShortCode = "DataUpdateTime", Desc = "����ʱ��", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "�������ݷ���", ShortCode = "BloodTransRecordType", Desc = "�������ݷ���")]
		public virtual BloodTransRecordType BloodTransRecordType
		{
			get { return _bloodTransRecordType; }
			set { _bloodTransRecordType = value; }
		}

		#endregion
	}
	#endregion
}