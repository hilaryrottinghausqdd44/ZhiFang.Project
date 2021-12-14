using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BCultureMedium

	/// <summary>
	/// BCultureMedium object for NHibernate mapped table 'B_CultureMedium'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "培养基字典表", ClassCName = "BCultureMedium", ShortCode = "BCultureMedium", Desc = "培养基字典表")]
	public class BCultureMedium : BaseEntity
	{
		#region Member Variables
		
        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected BCultivationCondition _bCultivationCondition;
		protected IList<BSampleTypeAndCultureMedium> _bSampleTypeAndCultureMediumList; 

		#endregion

		#region Constructors

		public BCultureMedium() { }

		public BCultureMedium( long labID, string cName, string eName, string sName, string shortcode, string pinYinZiTou, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BCultivationCondition bCultivationCondition )
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
			this._bCultivationCondition = bCultivationCondition;
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
        [DataDesc(CName = "培养条件字典表", ShortCode = "BCultivationCondition", Desc = "培养条件字典表")]
		public virtual BCultivationCondition BCultivationCondition
		{
			get { return _bCultivationCondition; }
			set { _bCultivationCondition = value; }
		}

        [DataMember]
        [DataDesc(CName = "样本类型与培养基关系表", ShortCode = "BSampleTypeAndCultureMediumList", Desc = "样本类型与培养基关系表")]
		public virtual IList<BSampleTypeAndCultureMedium> BSampleTypeAndCultureMediumList
		{
			get
			{
				if (_bSampleTypeAndCultureMediumList==null)
				{
					_bSampleTypeAndCultureMediumList = new List<BSampleTypeAndCultureMedium>();
				}
				return _bSampleTypeAndCultureMediumList;
			}
			set { _bSampleTypeAndCultureMediumList = value; }
		}

        
		#endregion
	}
	#endregion
}