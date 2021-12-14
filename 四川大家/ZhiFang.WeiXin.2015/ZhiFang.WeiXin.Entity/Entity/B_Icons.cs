using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region BIcons

	/// <summary>
	/// BIcons object for NHibernate mapped table 'B_Icons'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "图标头像", ClassCName = "BIcons", ShortCode = "BIcons", Desc = "图标头像")]
	public class BIcons : BaseEntity
	{
		#region Member Variables
		
        protected long? _iconsTypeID;
        protected int _width;
        protected int _height;
        protected long _size;
        protected string _url;
        protected DateTime _addTime;
        protected bool _isLocal;
        protected string _comment;
		

		#endregion

		#region Constructors

		public BIcons() { }

		public BIcons( long iconsTypeID, int width, int height, long size, string url, DateTime addTime, byte[] dataTimeStamp, bool isLocal, string comment )
		{
			this._iconsTypeID = iconsTypeID;
			this._width = width;
			this._height = height;
			this._size = size;
			this._url = url;
			this._addTime = addTime;
			this._dataTimeStamp = dataTimeStamp;
			this._isLocal = isLocal;
			this._comment = comment;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "IconsTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? IconsTypeID
		{
			get { return _iconsTypeID; }
			set { _iconsTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Width", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Width
		{
			get { return _width; }
			set { _width = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Height", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Height
		{
			get { return _height; }
			set { _height = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Size", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long Size
		{
			get { return _size; }
			set { _size = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Url", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Url
		{
			get { return _url; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Url", value, value.ToString());
				_url = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "AddTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime AddTime
		{
			get { return _addTime; }
			set { _addTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsLocal", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsLocal
		{
			get { return _isLocal; }
			set { _isLocal = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Comment", Desc = "", ContextType = SysDic.All, Length = 300)]
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

		
		#endregion
	}
	#endregion
}