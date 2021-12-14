using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BTemplate

	/// <summary>
	/// BTemplate object for NHibernate mapped table 'B_Template'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BTemplate", ShortCode = "BTemplate", Desc = "")]
	public class BTemplate : BaseEntity
	{
		#region Member Variables
		
        protected string _cName;
        protected long _typeID;
        protected string _typeName;
        protected string _filePath;
        protected string _fileExt;
        protected string _contentType;
        protected double _fileSize;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected int _dispOrder;
        protected string _comment;
        protected bool _isUse;

        protected bool _isDefault;
        protected string _fileName;
        protected string _excelRuleInfo;
        protected string _templateType;
        #endregion

        #region Constructors

        public BTemplate() { }

		public BTemplate( long labID, string cName, long typeID, string typeName, string filePath, string fileExt, string contentType, double fileSize, string sName, string shortcode, string pinYinZiTou, int dispOrder, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._cName = cName;
			this._typeID = typeID;
			this._typeName = typeName;
			this._filePath = filePath;
			this._fileExt = fileExt;
			this._contentType = contentType;
			this._fileSize = fileSize;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._dispOrder = dispOrder;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

        #endregion

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "模板类型:Excel模板,Frx模板", ShortCode = "ExcelRuleInfo", Desc = "模板类型:Excel模板,Frx模板", ContextType = SysDic.All, Length = 40)]
        public virtual string TemplateType
        {
            get { return _templateType; }
            set
            {
                _templateType = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "ExcelRuleInfo", ShortCode = "ExcelRuleInfo", Desc = "", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string ExcelRuleInfo
        {
            get { return _excelRuleInfo; }
            set
            {
                _excelRuleInfo = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "FileName", ShortCode = "FileName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				_cName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long TypeID
		{
			get { return _typeID; }
			set { _typeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string TypeName
		{
			get { return _typeName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TypeName", value, value.ToString());
				_typeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FilePath", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string FilePath
		{
			get { return _filePath; }
			set
			{
				_filePath = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FileExt", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string FileExt
		{
			get { return _fileExt; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for FileExt", value, value.ToString());
				_fileExt = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ContentType", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ContentType
		{
			get { return _contentType; }
			set
			{
				_contentType = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "FileSize", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double FileSize
		{
			get { return _fileSize; }
			set { _fileSize = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SName", Desc = "", ContextType = SysDic.All, Length = 80)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
				_sName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Shortcode", Desc = "", ContextType = SysDic.All, Length = 80)]
        public virtual string Shortcode
		{
			get { return _shortcode; }
			set
			{
				_shortcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PinYinZiTou", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Comment", Desc = "", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				_comment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}


        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsDefault", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }
        #endregion
    }
	#endregion
}