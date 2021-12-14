using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTDefaultItemMode

	/// <summary>
	/// MEPTDefaultItemMode object for NHibernate mapped table 'MEPT_DefaultItemMode'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "默认项目模板", ClassCName = "MEPTDefaultItemMode", ShortCode = "MEPTDefaultItemMode", Desc = "默认项目模板")]
	public class MEPTDefaultItemMode : BaseEntity
	{
		#region Member Variables
		
        protected int _modeType;
        protected string _name;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _comment;
        protected bool _isUse;
        protected HRDept _hRDept;
		protected HREmployee _hREmployee;
		protected IList<MEPTDefaultItemModeRelation> _mEPTDefaultItemModeRelationList;

		#endregion

		#region Constructors

		public MEPTDefaultItemMode() { }

		public MEPTDefaultItemMode( int modeType, string name, string sName, string shortcode, string pinYinZiTou, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, HRDept hRDept, HREmployee hREmployee, long LabID )
		{
			this._modeType = modeType;
			this._name = name;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._hRDept = hRDept;
			this._hREmployee = hREmployee;
            this._labID = LabID;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "模板类型", ShortCode = "ModeType", Desc = "模板类型", ContextType = SysDic.All, Length = 4)]
        public virtual int ModeType
		{
			get { return _modeType; }
			set { _modeType = value; }
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

        [DataMember]
        [DataDesc(CName = "部门", ShortCode = "HRDept", Desc = "部门", ContextType = SysDic.All)]
		public virtual HRDept HRDept
		{
			get { return _hRDept; }
			set { _hRDept = value; }
		}

        [DataMember]
        [DataDesc(CName = "员工", ShortCode = "HREmployee", Desc = "员工", ContextType = SysDic.All)]
		public virtual HREmployee HREmployee
		{
			get { return _hREmployee; }
			set { _hREmployee = value; }
		}
        [DataMember]
        [DataDesc(CName = "默认模板项目关系", ShortCode = "MEPTDefaultItemModeRelationList", Desc = "默认模板项目关系", ContextType = SysDic.All)]
		public virtual IList<MEPTDefaultItemModeRelation> MEPTDefaultItemModeRelationList
		{
			get
			{
				if (_mEPTDefaultItemModeRelationList==null)
				{
					_mEPTDefaultItemModeRelationList = new List<MEPTDefaultItemModeRelation>();
				}
				return _mEPTDefaultItemModeRelationList;
			}
			set { _mEPTDefaultItemModeRelationList = value; }
		}

		#endregion
	}
	#endregion
}