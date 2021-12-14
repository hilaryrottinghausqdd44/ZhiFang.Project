using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region OtherAttachment

	/// <summary>
	/// OtherAttachment object for NHibernate mapped table 'Other_Attachment'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "附件", ClassCName = "OtherAttachment", ShortCode = "OtherAttachment", Desc = "附件")]
    public class OtherAttachment : BaseEntity
	{
		#region Member Variables
		
		
		protected long _recordIndex;
		protected string _name;
		protected string _url;
        protected long? _fileTypeID;
		protected string _creator;
		protected string _modifier;
		protected DateTime? _dataUpdateTime;
		protected BTDAppComponents _bTDAppComponent;
		protected long? _creatorID;
		protected long? _modifierID;

		#endregion

		#region Constructors

		public OtherAttachment() { }

        public OtherAttachment(long labID, long recordIndex, string name, string url, long fileTypeID, string creator, DateTime dataAddTime, string modifier, DateTime dataUpdateTime, byte[] dataTimeStamp, BTDAppComponents bTDAppComponent, long creatorID, long modifierID)
		{
			this._labID = labID;
			this._recordIndex = recordIndex;
			this._name = name;
			this._url = url;
			this._fileTypeID = fileTypeID;
			this._creator = creator;
			this._dataAddTime = dataAddTime;
			this._modifier = modifier;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bTDAppComponent = bTDAppComponent;
            this._creatorID = creatorID;
            this._modifierID = modifierID;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "记录索引", ShortCode = "RecordIndex", Desc = "记录索引", ContextType = SysDic.Number, Length = 8)]
		public virtual long RecordIndex
		{
			get { return _recordIndex; }
			set { _recordIndex = value; }
		}

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "Name", Desc = "名称", ContextType = SysDic.NText, Length = 128)]
		public virtual string Name
		{
			get { return _name; }
			set
			{
				if ( value != null && value.Length > 128)
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
				_name = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "路径", ShortCode = "Url", Desc = "路径", ContextType = SysDic.NText, Length = 500)]
        public virtual string Url
        {
            get { return _url; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for Url", value, value.ToString());
                _url = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "附件类型ID", ShortCode = "FileTypeID", Desc = "附件类型ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long? FileTypeID
        {
            get { return _fileTypeID; }
            set { _fileTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "创建者", ShortCode = "Creator", Desc = "创建者", ContextType = SysDic.NText, Length = 20)]
		public virtual string Creator
		{
			get { return _creator; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Creator", value, value.ToString());
				_creator = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "修改者", ShortCode = "Modifier", Desc = "修改者", ContextType = SysDic.NText, Length = 20)]
		public virtual string Modifier
		{
			get { return _modifier; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Modifier", value, value.ToString());
				_modifier = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.DateTime)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        
        [DataMember]
        [DataDesc(CName = "应用组件", ShortCode = "BTDAppComponent", Desc = "应用组件")]
		public virtual BTDAppComponents BTDAppComponent
		{
			get { return _bTDAppComponent; }
			set { _bTDAppComponent = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "创建者ID", ShortCode = "CreatorID", Desc = "创建者ID", ContextType = SysDic.Number, Length = 8)]
		public virtual long? CreatorID
		{
			get { return _creatorID; }
			set { _creatorID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "修改者ID", ShortCode = "ModifierID", Desc = "修改者ID", ContextType = SysDic.Number, Length = 8)]
		public virtual long? ModifierID
		{
			get { return _modifierID; }
			set { _modifierID = value; }
		}

		#endregion
	}
	#endregion
}