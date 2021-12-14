using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BSoftWare

	/// <summary>
	/// BSoftWare object for NHibernate mapped table 'B_SoftWare'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "软件字典", ClassCName = "BSoftWare", ShortCode = "BSoftWare", Desc = "软件字典")]
	public class BSoftWare : BaseEntity
	{
		#region Member Variables
		
        protected string _name;
        protected string _sName;
        protected string _code;
        protected string _pinYinZiTou;
        protected string _comment;
        protected bool _isUse;
        protected string _iCO;
		protected IList<BSoftWareVersionManager> _bSoftWareVersionManagerList; 

		#endregion

		#region Constructors

		public BSoftWare() { }

        public BSoftWare(long labID, string name, string sName, string code, string pinYinZiTou, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, string iCO)
		{
			this._labID = labID;
			this._name = name;
			this._sName = sName;
			this._code = code;
			this._pinYinZiTou = pinYinZiTou;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
            this._iCO = iCO;
		}

		#endregion

		#region Public Properties


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
        [DataDesc(CName = "软件代码", ShortCode = "Code", Desc = "软件代码", ContextType = SysDic.All, Length = 40)]
        public virtual string Code
		{
			get { return _code; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Code", value, value.ToString());
				_code = value;
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
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 500)
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

        [DataMember]
        [DataDesc(CName = "图片Base64", ShortCode = "ICO", Desc = "图片Base64", ContextType = SysDic.All, Length = 16)]
        public virtual string ICO
        {
            get { return _iCO; }
            set { _iCO = value; }
        }

        [DataMember]
        [DataDesc(CName = "软件版本管理", ShortCode = "BSoftWareVersionManagerList", Desc = "软件版本管理")]
		public virtual IList<BSoftWareVersionManager> BSoftWareVersionManagerList
		{
			get
			{
				if (_bSoftWareVersionManagerList==null)
				{
					_bSoftWareVersionManagerList = new List<BSoftWareVersionManager>();
				}
				return _bSoftWareVersionManagerList;
			}
			set { _bSoftWareVersionManagerList = value; }
		}

        
		#endregion
	}
	#endregion
}