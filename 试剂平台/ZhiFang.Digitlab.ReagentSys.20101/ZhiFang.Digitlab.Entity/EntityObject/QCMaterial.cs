using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region QCMaterial

	/// <summary>
	/// QCMaterial object for NHibernate mapped table 'QCMaterial'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "QCMaterial", ShortCode = "QCMaterial", Desc = "")]
	public class QCMaterial : BaseEntity
	{
		#region Member Variables
		
        protected long? _equipNo;
        protected string _cName;
        protected string _eName;
        protected string _qCDesc;
        protected string _shortcode;
        protected string _lotNo;
        protected string _manu;
        protected string _markID;
        protected int _isUse;
        protected DateTime? _begindate;
        protected DateTime? _endDate;
        protected int _countInDay;
        protected string _equipModule;
        protected int _matType;
        protected int _microNo;
        protected DateTime? _canUseDate;
        protected DateTime? _notUseDate;
        protected string _concLevel;
        protected string _qCMType;
        protected string _canUseDateInfo;
        protected string _qCMatGroup;
		protected IList<QCItem> _qCItemList; 

		#endregion

		#region Constructors

		public QCMaterial() { }

		public QCMaterial( long equipNo, string cName, string eName, string qCDesc, string shortcode, string lotNo, string manu, string markID, int isUse, DateTime begindate, DateTime endDate, int countInDay, string equipModule, int matType, int microNo, DateTime canUseDate, DateTime notUseDate, string concLevel, string qCMType, string canUseDateInfo, string qCMatGroup )
		{
			this._equipNo = equipNo;
			this._cName = cName;
			this._eName = eName;
			this._qCDesc = qCDesc;
			this._shortcode = shortcode;
			this._lotNo = lotNo;
			this._manu = manu;
			this._markID = markID;
			this._isUse = isUse;
			this._begindate = begindate;
			this._endDate = endDate;
			this._countInDay = countInDay;
			this._equipModule = equipModule;
			this._matType = matType;
			this._microNo = microNo;
			this._canUseDate = canUseDate;
			this._notUseDate = notUseDate;
			this._concLevel = concLevel;
			this._qCMType = qCMType;
			this._canUseDateInfo = canUseDateInfo;
			this._qCMatGroup = qCMatGroup;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EquipNo", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? EquipNo
		{
			get { return _equipNo; }
			set { _equipNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string EName
		{
			get { return _eName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
				_eName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "QCDesc", Desc = "", ContextType = SysDic.All, Length = 1024)]
        public virtual string QCDesc
		{
			get { return _qCDesc; }
			set
			{
				if ( value != null && value.Length > 1024)
					throw new ArgumentOutOfRangeException("Invalid value for QCDesc", value, value.ToString());
				_qCDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Shortcode", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Shortcode
		{
			get { return _shortcode; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
				_shortcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LotNo", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string LotNo
		{
			get { return _lotNo; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for LotNo", value, value.ToString());
				_lotNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Manu", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string Manu
		{
			get { return _manu; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Manu", value, value.ToString());
				_manu = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MarkID", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string MarkID
		{
			get { return _markID; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for MarkID", value, value.ToString());
				_markID = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Begindate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? Begindate
		{
			get { return _begindate; }
			set { _begindate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EndDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? EndDate
		{
			get { return _endDate; }
			set { _endDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CountInDay", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int CountInDay
		{
			get { return _countInDay; }
			set { _countInDay = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EquipModule", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string EquipModule
		{
			get { return _equipModule; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EquipModule", value, value.ToString());
				_equipModule = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MatType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int MatType
		{
			get { return _matType; }
			set { _matType = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MicroNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int MicroNo
		{
			get { return _microNo; }
			set { _microNo = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CanUseDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CanUseDate
		{
			get { return _canUseDate; }
			set { _canUseDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "NotUseDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? NotUseDate
		{
			get { return _notUseDate; }
			set { _notUseDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ConcLevel", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ConcLevel
		{
			get { return _concLevel; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ConcLevel", value, value.ToString());
				_concLevel = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "QCMType", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string QCMType
		{
			get { return _qCMType; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for QCMType", value, value.ToString());
				_qCMType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CanUseDateInfo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CanUseDateInfo
		{
			get { return _canUseDateInfo; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CanUseDateInfo", value, value.ToString());
				_canUseDateInfo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "QCMatGroup", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string QCMatGroup
		{
			get { return _qCMatGroup; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for QCMatGroup", value, value.ToString());
				_qCMatGroup = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "QCItemList", Desc = "")]
		public virtual IList<QCItem> QCItemList
		{
			get
			{
				if (_qCItemList==null)
				{
					_qCItemList = new List<QCItem>();
				}
				return _qCItemList;
			}
			set { _qCItemList = value; }
		}

        
		#endregion
	}
	#endregion
}