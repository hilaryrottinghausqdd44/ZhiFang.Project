using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BCultivationCondition

	/// <summary>
	/// BCultivationCondition object for NHibernate mapped table 'B_CultivationCondition'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "培养条件字典表", ClassCName = "BCultivationCondition", ShortCode = "BCultivationCondition", Desc = "培养条件字典表")]
	public class BCultivationCondition : BaseEntity
	{
		#region Member Variables
		
        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected int _acrobicFlag;
		protected IList<BCultureMedium> _bCultureMediumList; 
		protected IList<MEMicroInoculant> _mEMicroInoculantList; 

		#endregion

		#region Constructors

		public BCultivationCondition() { }

		public BCultivationCondition( long labID, string cName, string eName, string sName, string shortcode, string pinYinZiTou, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, int acrobicFlag )
		{
			this._labID = labID;
			this._cName = cName;
			this._eName = eName;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._acrobicFlag = acrobicFlag;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 200)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "汉字拼音字头", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "需氧类型（0需氧1厌氧）", ShortCode = "AcrobicFlag", Desc = "需氧类型（0需氧1厌氧）", ContextType = SysDic.All, Length = 4)]
        public virtual int AcrobicFlag
		{
			get { return _acrobicFlag; }
			set { _acrobicFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "培养基字典表", ShortCode = "BCultureMediumList", Desc = "培养基字典表")]
		public virtual IList<BCultureMedium> BCultureMediumList
		{
			get
			{
				if (_bCultureMediumList==null)
				{
					_bCultureMediumList = new List<BCultureMedium>();
				}
				return _bCultureMediumList;
			}
			set { _bCultureMediumList = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MEMicroInoculantList", Desc = "")]
		public virtual IList<MEMicroInoculant> MEMicroInoculantList
		{
			get
			{
				if (_mEMicroInoculantList==null)
				{
					_mEMicroInoculantList = new List<MEMicroInoculant>();
				}
				return _mEMicroInoculantList;
			}
			set { _mEMicroInoculantList = value; }
		}

        
		#endregion
	}
	#endregion
}