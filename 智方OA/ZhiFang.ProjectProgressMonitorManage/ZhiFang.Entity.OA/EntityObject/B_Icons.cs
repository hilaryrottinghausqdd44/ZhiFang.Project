using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.OA
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
        protected long? _size;
        protected string _url;
        protected bool _isLocal;
        protected string _comment;
		protected IList<BWeiXinAccount> _bWeiXinAccountList; 

		#endregion

		#region Constructors

		public BIcons() { }

		public BIcons( long iconsTypeID, int width, int height, long size, string url, DateTime dataAddTime, byte[] dataTimeStamp, bool isLocal, string comment )
		{
			this._iconsTypeID = iconsTypeID;
			this._width = width;
			this._height = height;
			this._size = size;
			this._url = url;
			this._dataAddTime = dataAddTime;
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
		public virtual long? Size
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

        [DataMember]
        [DataDesc(CName = "微信账户", ShortCode = "BWeiXinAccountList", Desc = "微信账户")]
		public virtual IList<BWeiXinAccount> BWeiXinAccountList
		{
			get
			{
				if (_bWeiXinAccountList==null)
				{
					_bWeiXinAccountList = new List<BWeiXinAccount>();
				}
				return _bWeiXinAccountList;
			}
			set { _bWeiXinAccountList = value; }
		}

        
		#endregion
	}
	#endregion
}