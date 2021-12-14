using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region BHospitalSearch

	/// <summary>
	/// BHospitalSearch object for NHibernate mapped table 'B_HospitalSearch'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "医院查询条件字典", ClassCName = "BHospitalSearch", ShortCode = "BHospitalSearch", Desc = "医院查询条件字典")]
	public class BHospitalSearch : BaseEntity
	{
		#region Member Variables
		
        protected string _hospitalCode;
        protected string _name;
        protected string _eName;
        protected string _meaning;
        protected string _code;
        protected string _fieldsName;
        protected string _fieldsEName;
        protected string _fieldsMeaning;
        protected string _fieldsCode;
        protected string _expression;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _comment;
        protected bool _isUse;
		

		#endregion

		#region Constructors

		public BHospitalSearch() { }

		public BHospitalSearch( string hospitalCode,  string name, string eName, string meaning, string code, string fieldsName, string fieldsEName, string fieldsMeaning, string fieldsCode, string expression, string sName, string shortcode, string pinYinZiTou, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._hospitalCode = hospitalCode;
			this._name = name;
			this._eName = eName;
			this._meaning = meaning;
			this._code = code;
			this._fieldsName = fieldsName;
			this._fieldsEName = fieldsEName;
			this._fieldsMeaning = fieldsMeaning;
			this._fieldsCode = fieldsCode;
			this._expression = expression;
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
        [DataDesc(CName = "", ShortCode = "HospitalCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HospitalCode
		{
			get { return _hospitalCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HospitalCode", value, value.ToString());
				_hospitalCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Name", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string Name
		{
			get { return _name; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
				_name = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EName
		{
			get { return _eName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
				_eName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Meaning", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string Meaning
		{
			get { return _meaning; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for Meaning", value, value.ToString());
				_meaning = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Code
		{
			get { return _code; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Code", value, value.ToString());
				_code = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FieldsName", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string FieldsName
		{
			get { return _fieldsName; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for FieldsName", value, value.ToString());
				_fieldsName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FieldsEName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string FieldsEName
		{
			get { return _fieldsEName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for FieldsEName", value, value.ToString());
				_fieldsEName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FieldsMeaning", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string FieldsMeaning
		{
			get { return _fieldsMeaning; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for FieldsMeaning", value, value.ToString());
				_fieldsMeaning = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FieldsCode", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string FieldsCode
		{
			get { return _fieldsCode; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for FieldsCode", value, value.ToString());
				_fieldsCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Expression", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Expression
		{
			get { return _expression; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Expression", value, value.ToString());
				_expression = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 30)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
				if ( value != null && value.Length > 30)
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