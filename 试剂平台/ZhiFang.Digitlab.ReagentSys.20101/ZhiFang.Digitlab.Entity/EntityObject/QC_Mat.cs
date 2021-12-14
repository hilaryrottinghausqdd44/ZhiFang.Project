using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region QCMat

	/// <summary>
	/// QCMat object for NHibernate mapped table 'QC_Mat'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "质控物", ClassCName = "", ShortCode = "ZKW", Desc = "质控物")]
    public class QCMat : BaseEntity
	{
		#region Member Variables
		
		
		protected string _matGroup;
		protected string _equipModule;
		protected string _useCode;
		protected string _standCode;
		protected string _cName;
		protected string _eName;
		protected string _sName;
		protected string _shortcode;
		protected string _pinYinZiTou;
		protected string _comment;
		protected int _matType;
		protected string _manu;
		protected string _concLevel;
		protected bool _isUse;
		protected int _dispOrder;
        protected string _markID;
		protected DateTime? _dataUpdateTime;
		protected EPBEquip _ePBEquip;
        protected BMicMicro _bMicMicro;
		protected IList<QCItem> _qCItems;
		protected IList<QCMatTime> _qCMatTimes;

		#endregion

		#region Constructors

		public QCMat() { }

		public QCMat( long labID, string matGroup, string equipModule, string useCode, string standCode, string cName, string eName, string sName, string shortcode, string pinYinZiTou, string comment, int matType, string manu, string concLevel, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, EPBEquip ePBEquip )
		{
			this._labID = labID;
			this._matGroup = matGroup;
			this._equipModule = equipModule;
			this._useCode = useCode;
			this._standCode = standCode;
			this._cName = cName;
			this._eName = eName;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._comment = comment;
			this._matType = matType;
			this._manu = manu;
			this._concLevel = concLevel;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._ePBEquip = ePBEquip;
		}

		#endregion

		#region Public Properties

        [DataMember]
        [DataDesc(CName = "质控物分组", ShortCode = "ZKWFZ", Desc = "质控物分组", ContextType = SysDic.NText, Length = 50)]
		public virtual string MatGroup
		{
			get { return _matGroup; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for MatGroup", value, value.ToString());
				_matGroup = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "模块Code", ShortCode = "MKCODE", Desc = "模块Code", ContextType = SysDic.NText, Length = 20)]
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
        [DataDesc(CName = "代码", ShortCode = "DM", Desc = "代码", ContextType = SysDic.NText, Length = 50)]
		public virtual string UseCode
		{
			get { return _useCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for UseCode", value, value.ToString());
				_useCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "标准代码", ShortCode = "BZDM", Desc = "标准代码", ContextType = SysDic.NText, Length = 50)]
		public virtual string StandCode
		{
			get { return _standCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for StandCode", value, value.ToString());
				_standCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "MC", Desc = "名称", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "英文名称", ShortCode = "YWMC", Desc = "英文名称", ContextType = SysDic.NText, Length = 50)]
		public virtual string EName
		{
			get { return _eName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
				_eName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "JC", Desc = "简称", ContextType = SysDic.NText, Length = 50)]
		public virtual string SName
		{
			get { return _sName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "KJM", Desc = "快捷码", ContextType = SysDic.NText, Length = 20)]
		public virtual string Shortcode
		{
			get { return _shortcode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
				_shortcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "汉字拼音字头", ShortCode = "HZPYZT", Desc = "汉字拼音字头", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "描述", ShortCode = "MS", Desc = "描述", ContextType = SysDic.NText)]
		public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 1000000)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "质控物类型", ShortCode = "ZKWLX", Desc = "质控物类型", ContextType = SysDic.Number, Length = 4)]
		public virtual int MatType
		{
			get { return _matType; }
			set { _matType = value; }
		}

        [DataMember]
        [DataDesc(CName = "厂家", ShortCode = "CJ", Desc = "厂家", ContextType = SysDic.NText, Length = 40)]
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
        [DataDesc(CName = "浓度水平", ShortCode = "NDSP", Desc = "浓度水平", ContextType = SysDic.NText, Length = 20)]
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
        [DataDesc(CName = "是否使用", ShortCode = "SFSY", Desc = "是否使用", ContextType = SysDic.All)]
		public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "XSCX", Desc = "显示次序", ContextType = SysDic.Number, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "对应仪器通道号", ShortCode = "MarkID", Desc = "对应仪器通道号", ContextType = SysDic.NText, Length = 20)]
        public virtual string MarkID
        {
            get { return _markID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for MarkID", value, value.ToString());
                _markID = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "SJGXSJ", Desc = "数据更新时间", ContextType = SysDic.DateTime)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "仪器", ShortCode = "YQ", Desc = "仪器", ContextType = SysDic.List)]
		public virtual EPBEquip EPBEquip
		{
			get { return _ePBEquip; }
			set { _ePBEquip = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物表", ShortCode = "BMicMicro", Desc = "微生物表")]
        public virtual BMicMicro BMicMicro
        {
            get { return _bMicMicro; }
            set { _bMicMicro = value; }
        }

        [DataMember]
        [DataDesc(CName = "质控项目列表", ShortCode = "ZKXMLB", Desc = "质控项目列表", ContextType = SysDic.List)]
		public virtual IList<QCItem> QCItemList
		{
			get
			{
				if (_qCItems==null)
				{
                    _qCItems = new List<QCItem>();
				}
				return _qCItems;
			}
			set { _qCItems = value; }
		}

        [DataMember]
        [DataDesc(CName = "质控物时效列表", ShortCode = "ZKWSXLB", Desc = "质控物时效列表", ContextType = SysDic.List)]
		public virtual IList<QCMatTime> QCMatTimeList
		{
			get
			{
				if (_qCMatTimes==null)
				{
                    _qCMatTimes = new List<QCMatTime>();
				}
				return _qCMatTimes;
			}
			set { _qCMatTimes = value; }
		}

        public virtual string PrimaryKey
        {
            get { return "QCMatID"; }
        }

		#endregion
	}
	#endregion
}