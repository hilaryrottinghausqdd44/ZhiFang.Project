using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region SCRecordPhrase

	/// <summary>
	/// SCRecordPhrase object for NHibernate mapped table 'SC_RecordPhrase'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "SCRecordPhrase", ShortCode = "SCRecordPhrase", Desc = "")]
	public class SCRecordPhrase : BaseEntity
	{
		#region Member Variables

		protected long? _bObjectId;
		protected long? _phraseType;
		protected long? _typeObjectId;
        protected string _cName;
        protected string _sName;
        protected string _shortCode;
        protected string _pinYinZiTou;
        protected bool _isUse;
        protected string _memo;
        protected int _dispOrder;

		#endregion

		#region Constructors

		public SCRecordPhrase() { }

		public SCRecordPhrase( long labID, long bObjectId, string cName, string sName, string shortCode, string pinYinZiTou, bool isUse, string memo, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._bObjectId = bObjectId;
			this._cName = cName;
			this._sName = sName;
			this._shortCode = shortCode;
			this._pinYinZiTou = pinYinZiTou;
			this._isUse = isUse;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties
		/// <summary>
		/// ��������
		/// 1:����;
		/// 2:������;
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "��������", ShortCode = "PhraseType", Desc = "PhraseType", ContextType = SysDic.All, Length = 8)]
		public virtual long? PhraseType
		{
			get { return _phraseType; }
			set { _phraseType = value; }
		}
		/// <summary>
		/// ���������Ϊ����ʱ��Ϊ��ֵ;���������Ϊ������ʱ���洢���ǿ��ҵ�Id
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "��������ҵ���Id", ShortCode = "TypeObjectId", Desc = "��������ҵ���Id", ContextType = SysDic.All, Length = 8)]
		public virtual long? TypeObjectId
		{
			get { return _typeObjectId; }
			set { _typeObjectId = value; }
		}
		/// <summary>
		/// �洢��¼�������ֵ��Id���¼���ֵ��Id
		/// </summary>
		[DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "��¼���ֵ�ID", ShortCode = "BObjectId", Desc = "��¼���ֵ�ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? BObjectId
		{
			get { return _bObjectId; }
			set { _bObjectId = value; }
		}

        [DataMember]
        [DataDesc(CName = "CName", ShortCode = "CName", Desc = "CName", ContextType = SysDic.All, Length = 80)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 80)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "SName", ShortCode = "SName", Desc = "SName", ContextType = SysDic.All, Length = 80)]
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
        [DataDesc(CName = "ShortCode", ShortCode = "ShortCode", Desc = "ShortCode", ContextType = SysDic.All, Length = 40)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "PinYinZiTou", ShortCode = "PinYinZiTou", Desc = "PinYinZiTou", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "IsUse", ShortCode = "IsUse", Desc = "IsUse", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "Memo", ShortCode = "Memo", Desc = "Memo", ContextType = SysDic.All)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				_memo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "DispOrder", ShortCode = "DispOrder", Desc = "DispOrder", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        
		#endregion
	}
	#endregion
}