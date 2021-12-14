using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
	#region BTDAppPicture

	/// <summary>
	/// BTDAppPicture object for NHibernate mapped table 'BT_D_AppPicture'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "应用图片", ClassCName = "", ShortCode = "YYTP", Desc = "应用图片")]
    public class BTDAppPicture : BaseEntityService
    {
		#region Member Variables
		
		
		protected string _cName;
		protected int _fileSize;
		protected string _showSize;
		protected string _pictureFormat;
		protected string _pictureURL;
		protected string _creator;
		protected string _modifier;
		protected string _pinYinZiTou;
		protected DateTime? _dataUpdateTime;
		protected IList<BTDPictureTypeCon> _bTDPictureTypeCons;

		#endregion

		#region Constructors

		public BTDAppPicture() { }

		public BTDAppPicture( long labID, string cName, int fileSize, string showSize, string pictureFormat, string pictureURL, string creator, string modifier, string pinYinZiTou, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._cName = cName;
			this._fileSize = fileSize;
			this._showSize = showSize;
			this._pictureFormat = pictureFormat;
			this._pictureURL = pictureURL;
			this._creator = creator;
			this._modifier = modifier;
			this._pinYinZiTou = pinYinZiTou;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties

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
        [DataDesc(CName = "文件大小", ShortCode = "WJDX", Desc = "文件大小", ContextType = SysDic.Number, Length = 4)]
		public virtual int FileSize
		{
			get { return _fileSize; }
			set { _fileSize = value; }
		}

        [DataMember]
        [DataDesc(CName = "图片尺寸", ShortCode = "TPCC", Desc = "图片尺寸", ContextType = SysDic.NText, Length = 20)]
		public virtual string ShowSize
		{
			get { return _showSize; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ShowSize", value, value.ToString());
				_showSize = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "图片格式", ShortCode = "TPGS", Desc = "图片格式", ContextType = SysDic.NText, Length = 10)]
		public virtual string PictureFormat
		{
			get { return _pictureFormat; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for PictureFormat", value, value.ToString());
				_pictureFormat = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "图片URL", ShortCode = "TPURL", Desc = "图片URL", ContextType = SysDic.NText, Length = 100)]
		public virtual string PictureURL
		{
			get { return _pictureURL; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for PictureURL", value, value.ToString());
				_pictureURL = value;
			}
		}

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
        [DataDesc(CName = "图片类型关联列表", ShortCode = "TPLXGLLB", Desc = "图片类型关联列表", ContextType = SysDic.List)]
		public virtual IList<BTDPictureTypeCon> BTDPictureTypeConsList
		{
			get
			{
				if (_bTDPictureTypeCons==null)
				{
                    _bTDPictureTypeCons = new List<BTDPictureTypeCon>();
				}
				return _bTDPictureTypeCons;
			}
			set { _bTDPictureTypeCons = value; }
		}

		#endregion
	}
	#endregion
}