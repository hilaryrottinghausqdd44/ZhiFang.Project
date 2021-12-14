using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.OA
{
	#region BWeiXinPushMessageTemplate

	/// <summary>
	/// BWeiXinPushMessageTemplate object for NHibernate mapped table 'B_WeiXin_PushMessageTemplate'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "消息推送模版", ClassCName = "BWeiXinPushMessageTemplate", ShortCode = "BWeiXinPushMessageTemplate", Desc = "消息推送模版")]
	public class BWeiXinPushMessageTemplate : BaseEntityService
	{
		#region Member Variables
		
        protected string _pMTKey;
        protected string _name;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _comment;
        protected bool _isUse;

		#endregion

		#region Constructors

		public BWeiXinPushMessageTemplate() { }

		public BWeiXinPushMessageTemplate( long labID, string pMTKey, string name, string sName, string shortcode, string pinYinZiTou, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._pMTKey = pMTKey;
			this._name = name;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "WeiXin服务器中的模板ID", ShortCode = "PMTKey", Desc = "WeiXin服务器中的模板ID", ContextType = SysDic.All, Length = 500)]
        public virtual string PMTKey
		{
			get { return _pMTKey; }
			set
			{
				_pMTKey = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Name", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string Name
		{
			get { return _name; }
			set
			{
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
				_pinYinZiTou = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
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