using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BLaboratory

	/// <summary>
	/// BLaboratory object for NHibernate mapped table 'B_Laboratory'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "实验室", ClassCName = "BLaboratory", ShortCode = "SYS", Desc = "实验室")]
    public class BLaboratory : BaseEntity
	{
		#region Member Variables
		
		protected string _cName;
		protected string _eName;
		protected string _shortCode;
        protected bool _isUse;
		protected string _linkMan;
		protected string _phoneNum1;
		protected string _address;
		protected string _mailNo;
		protected string _emall;
		protected string _principal;
		protected string _phoneNum2;
		protected int _clientType;
		protected int _bmanNo;
		protected string _romark;
		protected int _titleType;
		protected int _uploadType;
		protected int _printType;
		protected int _inputDataType;
		protected int _reportPageType;
		protected string _clientArea;
		protected string _clientStyle;
		protected string _relationName;
		protected string _webLisSourceOrgID;
		protected string _groupName;
		protected DateTime? _dataUpdateTime;		

		#endregion

		#region Constructors

		public BLaboratory() { }

		public BLaboratory( string cName, string eName, string shortCode, bool isUse, string linkMan, string phoneNum1, string address, string mailNo, string emall, string principal, string phoneNum2, int clientType, int bmanNo, string romark, int titleType, int uploadType, int printType, int inputDataType, int reportPageType, string clientArea, string clientStyle, string relationName, string webLisSourceOrgID, string groupName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._cName = cName;
			this._eName = eName;
			this._shortCode = shortCode;
			this._isUse = isUse;
			this._linkMan = linkMan;
			this._phoneNum1 = phoneNum1;
			this._address = address;
			this._mailNo = mailNo;
			this._emall = emall;
			this._principal = principal;
			this._phoneNum2 = phoneNum2;
			this._clientType = clientType;
			this._bmanNo = bmanNo;
			this._romark = romark;
			this._titleType = titleType;
			this._uploadType = uploadType;
			this._printType = printType;
			this._inputDataType = inputDataType;
			this._reportPageType = reportPageType;
			this._clientArea = clientArea;
			this._clientStyle = clientStyle;
			this._relationName = relationName;
			this._webLisSourceOrgID = webLisSourceOrgID;
			this._groupName = groupName;
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
        [DataDesc(CName = "英文名称", ShortCode = "YWMC", Desc = "英文名称", ContextType = SysDic.NText, Length = 40)]
		public virtual string EName
		{
			get { return _eName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
				_eName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "JC", Desc = "简称", ContextType = SysDic.NText, Length = 40)]
		public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
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
        [DataDesc(CName = "联系人", ShortCode = "LXR", Desc = "联系人", ContextType = SysDic.NText, Length = 40)]
		public virtual string LinkMan
		{
			get { return _linkMan; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for LinkMan", value, value.ToString());
				_linkMan = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "联系电话1", ShortCode = "LXDH1", Desc = "联系电话1", ContextType = SysDic.NText, Length = 40)]
		public virtual string PhoneNum1
		{
			get { return _phoneNum1; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for PhoneNum1", value, value.ToString());
				_phoneNum1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "地址", ShortCode = "DZ", Desc = "地址", ContextType = SysDic.NText, Length = 40)]
		public virtual string Address
		{
			get { return _address; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Address", value, value.ToString());
				_address = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "邮编", ShortCode = "YB", Desc = "邮编", ContextType = SysDic.NText, Length = 40)]
		public virtual string MailNo
		{
			get { return _mailNo; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for MailNo", value, value.ToString());
				_mailNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "电子邮件", ShortCode = "DZYJ", Desc = "电子邮件", ContextType = SysDic.NText, Length = 40)]
		public virtual string Emall
		{
			get { return _emall; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Emall", value, value.ToString());
				_emall = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "负责人", ShortCode = "FZR", Desc = "负责人", ContextType = SysDic.NText, Length = 40)]
		public virtual string Principal
		{
			get { return _principal; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Principal", value, value.ToString());
				_principal = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "联系电话2", ShortCode = "LXDH2", Desc = "联系电话2", ContextType = SysDic.NText, Length = 40)]
		public virtual string PhoneNum2
		{
			get { return _phoneNum2; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for PhoneNum2", value, value.ToString());
				_phoneNum2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "ClientType", ShortCode = "ClientType", Desc = "ClientType", ContextType = SysDic.Number, Length = 4)]
		public virtual int ClientType
		{
			get { return _clientType; }
			set { _clientType = value; }
		}

        [DataMember]
        [DataDesc(CName = "业务员编码", ShortCode = "YWYBM", Desc = "业务员编码", ContextType = SysDic.Number, Length = 4)]
		public virtual int BmanNo
		{
			get { return _bmanNo; }
			set { _bmanNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "BZ", Desc = "备注", ContextType = SysDic.NText, Length = 200)]
		public virtual string Romark
		{
			get { return _romark; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Romark", value, value.ToString());
				_romark = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "TitleType", ShortCode = "TitleType", Desc = "TitleType", ContextType = SysDic.Number, Length = 4)]
		public virtual int TitleType
		{
			get { return _titleType; }
			set { _titleType = value; }
		}

        [DataMember]
        [DataDesc(CName = "UploadType", ShortCode = "UploadType", Desc = "UploadType", ContextType = SysDic.Number, Length = 4)]
		public virtual int UploadType
		{
			get { return _uploadType; }
			set { _uploadType = value; }
		}

        [DataMember]
        [DataDesc(CName = "PrintType", ShortCode = "PrintType", Desc = "PrintType", ContextType = SysDic.Number, Length = 4)]
		public virtual int PrintType
		{
			get { return _printType; }
			set { _printType = value; }
		}

        [DataMember]
        [DataDesc(CName = "InputDataType", ShortCode = "InputDataType", Desc = "InputDataType", ContextType = SysDic.Number, Length = 4)]
		public virtual int InputDataType
		{
			get { return _inputDataType; }
			set { _inputDataType = value; }
		}

        [DataMember]
        [DataDesc(CName = "ReportPageType", ShortCode = "ReportPageType", Desc = "ReportPageType", ContextType = SysDic.Number, Length = 4)]
		public virtual int ReportPageType
		{
			get { return _reportPageType; }
			set { _reportPageType = value; }
		}

        [DataMember]
        [DataDesc(CName = "区域", ShortCode = "QY", Desc = "区域", ContextType = SysDic.NText, Length = 40)]
		public virtual string ClientArea
		{
			get { return _clientArea; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for ClientArea", value, value.ToString());
				_clientArea = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "ClientStyle", ShortCode = "ClientStyle", Desc = "ClientStyle", ContextType = SysDic.NText, Length = 40)]
		public virtual string ClientStyle
		{
			get { return _clientStyle; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for ClientStyle", value, value.ToString());
				_clientStyle = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "关联名称", ShortCode = "GLMC", Desc = "关联名称", ContextType = SysDic.NText, Length = 50)]
		public virtual string RelationName
		{
			get { return _relationName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for RelationName", value, value.ToString());
				_relationName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "区域医疗机构编码", ShortCode = "YLJGBM", Desc = "区域医疗机构编码", ContextType = SysDic.NText, Length = 10)]
		public virtual string WebLisSourceOrgID
		{
			get { return _webLisSourceOrgID; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisSourceOrgID", value, value.ToString());
				_webLisSourceOrgID = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "办事处", ShortCode = "BCC", Desc = "办事处", ContextType = SysDic.NText, Length = 50)]
		public virtual string GroupName
		{
			get { return _groupName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for GroupName", value, value.ToString());
				_groupName = value;
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

		#endregion
	}
	#endregion
}