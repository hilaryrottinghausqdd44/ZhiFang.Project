using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BMicroClass

	/// <summary>
	/// BMicroClass object for NHibernate mapped table 'B_MicroClass'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物类型", ClassCName = "BMicroClass", ShortCode = "BMicroClass", Desc = "微生物类型")]
	public class BMicroClass : BaseEntity
	{
		#region Member Variables
		
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected BMicro _bMicro;
		protected BMicroClassType _bMicroClassType;

		#endregion

		#region Constructors

		public BMicroClass() { }

		public BMicroClass( long labID, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BMicro bMicro, BMicroClassType bMicroClassType )
		{
			this._labID = labID;
			this._comment = comment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bMicro = bMicro;
			this._bMicroClassType = bMicroClassType;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "描述", ShortCode = "Comment", Desc = "描述", ContextType = SysDic.All, Length = 16)]
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

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物", ShortCode = "BMicro", Desc = "微生物")]
		public virtual BMicro BMicro
		{
			get { return _bMicro; }
			set { _bMicro = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物分类", ShortCode = "BMicroClassType", Desc = "微生物分类")]
		public virtual BMicroClassType BMicroClassType
		{
			get { return _bMicroClassType; }
			set { _bMicroClassType = value; }
		}

        
		#endregion
	}
	#endregion
}