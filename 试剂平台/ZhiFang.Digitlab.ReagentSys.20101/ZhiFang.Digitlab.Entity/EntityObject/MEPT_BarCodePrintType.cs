using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTBarCodePrintType

	/// <summary>
	/// MEPTBarCodePrintType object for NHibernate mapped table 'MEPT_BarCodePrintType'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "条码打印类型", ClassCName = "MEPTBarCodePrintType", ShortCode = "MEPTBarCodePrintType", Desc = "条码打印类型")]
    public class MEPTBarCodePrintType : BaseEntity
	{
		#region Member Variables
		
        
        protected string _name;
        protected string _sName;
        protected string _content;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _comment;
        protected bool _isUse;
        

		#endregion

		#region Constructors

		public MEPTBarCodePrintType() { }

		public MEPTBarCodePrintType( long labID, string name, string sName, string content, string shortcode, string pinYinZiTou, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._name = name;
			this._sName = sName;
			this._content = content;
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
        [DataDesc(CName = "打印参数", ShortCode = "Content", Desc = "打印参数", ContextType = SysDic.NText, Length = 500)]
        public virtual string Content
        {
            get { return _content; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for Content", value, value.ToString());
                _content = value;
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
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 16)]
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

		#endregion
	}
	#endregion
}