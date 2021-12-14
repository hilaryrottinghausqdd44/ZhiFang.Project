using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BTDModuleType

	/// <summary>
	/// BTDModuleType object for NHibernate mapped table 'BT_D_ModuleType'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "应用类型", ClassCName = "BTDModuleType", ShortCode = "BTDModuleType", Desc = "应用类型")]
	public class BTDModuleType : BaseEntity
	{
		#region Member Variables
		
        protected string _moduleTypeCode;
        protected string _cName;
        protected string _creator;
        protected string _modifier;
        protected DateTime? _dataUpdateTime;
		protected IList<BTDAppComponents> _bTDAppComponentsList; 

		#endregion

		#region Constructors

		public BTDModuleType() { }

        public BTDModuleType(long labID, string moduleTypeCode, string cName, string creator, string modifier, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
		{
			this._labID = labID;
			this._moduleTypeCode = moduleTypeCode;
			this._cName = cName;
			this._creator = creator;
			this._modifier = modifier;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "编码", ShortCode = "ModuleTypeCode", Desc = "编码", ContextType = SysDic.All, Length = 20)]
        public virtual string ModuleTypeCode
		{
			get { return _moduleTypeCode; }
			set { _moduleTypeCode = value; }
		}

        [DataMember]
        [DataDesc(CName = "中文名称", ShortCode = "CName", Desc = "中文名称", ContextType = SysDic.All, Length = 100)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "创建者", ShortCode = "Creator", Desc = "创建者", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "修改者", ShortCode = "Modifier", Desc = "修改者", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        
		#endregion
	}
	#endregion
}