using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.OA
{
	#region ATHolidaySetting

	/// <summary>
	/// ATHolidaySetting object for NHibernate mapped table 'AT_HolidaySetting'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "节假日设置", ClassCName = "ATHolidaySetting", ShortCode = "ATHolidaySetting", Desc = "节假日设置")]
	public class ATHolidaySetting : BaseEntity
	{
		#region Member Variables
		
        protected int _year;
        protected int _month;
        protected int _settingType;
        protected DateTime? _dateCode;
        protected string _holidayName;
        protected string _name;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected int _dispOrder;
        protected string _comment;
        protected bool _isUse;
		

		#endregion

		#region Constructors

		public ATHolidaySetting() { }

		public ATHolidaySetting( long labID, int year, int month, int settingType, DateTime dateCode, string holidayName, string name, string sName, string shortcode, string pinYinZiTou, int dispOrder, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._year = year;
			this._month = month;
			this._settingType = settingType;
			this._dateCode = dateCode;
			this._holidayName = holidayName;
			this._name = name;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._dispOrder = dispOrder;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "Year", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Year
		{
			get { return _year; }
			set { _year = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Month", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Month
		{
			get { return _month; }
			set { _month = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SettingType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SettingType
		{
			get { return _settingType; }
			set { _settingType = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DateCode", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DateCode
		{
			get { return _dateCode; }
			set { _dateCode = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HolidayName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string HolidayName
		{
			get { return _holidayName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for HolidayName", value, value.ToString());
				_holidayName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "Name", Desc = "名称", ContextType = SysDic.All, Length = 40)]
        public virtual string Name
		{
			get { return _name; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
				_name = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 40)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 300)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

		
		#endregion
	}
	#endregion
}