using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BTDPictureTypeCon

	/// <summary>
	/// BTDPictureTypeCon object for NHibernate mapped table 'BT_D_PictureTypeCon'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "图片类型关联", ClassCName = "", ShortCode = "TPLXGL", Desc = "图片类型关联")]
    public class BTDPictureTypeCon : BaseEntity
	{
		#region Member Variables
		
		
		protected string _creator;
		protected string _modifier;
		protected string _pinYinZiTou;
		protected DateTime? _dataUpdateTime;
		protected BTDAppPicture _bTDAppPicture;
		protected BTDPictureType _bTDPictureType;

		#endregion

		#region Constructors

		public BTDPictureTypeCon() { }

		public BTDPictureTypeCon( long labID, string creator, string modifier, string pinYinZiTou, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BTDAppPicture bTDAppPicture, BTDPictureType bTDPictureType )
		{
			this._labID = labID;
			this._creator = creator;
			this._modifier = modifier;
			this._pinYinZiTou = pinYinZiTou;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bTDAppPicture = bTDAppPicture;
			this._bTDPictureType = bTDPictureType;
		}

		#endregion

		#region Public Properties

        [DataMember]
        [DataDesc(CName = "创建者", ShortCode = "CJZ", Desc = "创建者", ContextType = SysDic.NText, Length = 20)]
		public virtual string Creator
		{
			get { return _creator; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Creator", value, value.ToString());
				_creator = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "修改者", ShortCode = "XGZ", Desc = "修改者", ContextType = SysDic.NText, Length = 20)]
		public virtual string Modifier
		{
			get { return _modifier; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Modifier", value, value.ToString());
				_modifier = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "SJGXSJ", Desc = "数据更新时间", ContextType = SysDic.DateTime)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        
        [DataMember]
        [DataDesc(CName = "应用图片", ShortCode = "YYTP", Desc = "应用图片")]
		public virtual BTDAppPicture BTDAppPicture
		{
			get { return _bTDAppPicture; }
			set { _bTDAppPicture = value; }
		}

        [DataMember]
        [DataDesc(CName = "图片类型", ShortCode = "TPLX", Desc = "图片类型")]
		public virtual BTDPictureType BTDPictureType
		{
			get { return _bTDPictureType; }
			set { _bTDPictureType = value; }
		}

		#endregion
	}
	#endregion
}