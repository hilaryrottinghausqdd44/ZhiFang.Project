using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BSampleStatusType

	/// <summary>
	/// BSampleStatusType object for NHibernate mapped table 'B_SampleStatusType'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "样本状态类型表", ClassCName = "BSampleStatusType", ShortCode = "BSampleStatusType", Desc = "样本状态类型表")]
	public class BSampleStatusType : BaseEntity
	{
		#region Member Variables
		
        protected string _name;
        protected int _level;
        protected string _color;
        protected string _sName;
        protected string _shortCode;
        protected string _pinYinZiTou;
        protected string _comment;
        protected bool _isUse;
		protected IList<BSampleStatus> _bSampleStatusList;

		#endregion

		#region Constructors

		public BSampleStatusType() { }

		public BSampleStatusType( long labID, string name, int level, string color, string sName, string shortCode, string pinYinZiTou, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._name = name;
			this._level = level;
			this._color = color;
			this._sName = sName;
			this._shortCode = shortCode;
			this._pinYinZiTou = pinYinZiTou;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
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
        [DataDesc(CName = "级别", ShortCode = "Level", Desc = "级别", ContextType = SysDic.All, Length = 4)]
        public virtual int Level
		{
			get { return _level; }
			set { _level = value; }
		}

        [DataMember]
        [DataDesc(CName = "颜色", ShortCode = "Color", Desc = "颜色", ContextType = SysDic.All, Length = 40)]
        public virtual string Color
		{
			get { return _color; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Color", value, value.ToString());
				_color = value;
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
        [DataDesc(CName = "快捷码", ShortCode = "ShortCode", Desc = "快捷码", ContextType = SysDic.All, Length = 20)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
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
        [DataDesc(CName = "说明", ShortCode = "Comment", Desc = "说明", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 8000)
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
        [DataDesc(CName = "样本状态", ShortCode = "BSampleStatusList", Desc = "样本状态")]
		public virtual IList<BSampleStatus> BSampleStatusList
		{
			get
			{
				if (_bSampleStatusList==null)
				{
					_bSampleStatusList = new List<BSampleStatus>();
				}
				return _bSampleStatusList;
			}
			set { _bSampleStatusList = value; }
		}

        
		#endregion
	}
	#endregion
}