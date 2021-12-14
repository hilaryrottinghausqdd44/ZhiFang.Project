using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BAntiClass

	/// <summary>
	/// BAntiClass object for NHibernate mapped table 'B_AntiClass'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "抗生素类型", ClassCName = "BAntiClass", ShortCode = "BAntiClass", Desc = "抗生素类型")]
	public class BAntiClass : BaseEntity
	{
		#region Member Variables
		
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected BAnti _bAnti;
		protected BAntiClassType _bAntiClassType;

		#endregion

		#region Constructors

		public BAntiClass() { }

		public BAntiClass( long labID, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BAnti bAnti, BAntiClassType bAntiClassType )
		{
			this._labID = labID;
			this._comment = comment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bAnti = bAnti;
			this._bAntiClassType = bAntiClassType;
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
        [DataDesc(CName = "抗生素", ShortCode = "BAnti", Desc = "抗生素")]
		public virtual BAnti BAnti
		{
			get { return _bAnti; }
			set { _bAnti = value; }
		}

        [DataMember]
        [DataDesc(CName = "抗生素分类类型", ShortCode = "BAntiClassType", Desc = "抗生素分类类型")]
		public virtual BAntiClassType BAntiClassType
		{
			get { return _bAntiClassType; }
			set { _bAntiClassType = value; }
		}

        
		#endregion
	}
	#endregion
}