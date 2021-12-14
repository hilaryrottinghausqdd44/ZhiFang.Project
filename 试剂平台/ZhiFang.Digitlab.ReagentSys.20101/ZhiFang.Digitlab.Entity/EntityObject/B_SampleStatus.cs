using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BSampleStatus

	/// <summary>
	/// BSampleStatus object for NHibernate mapped table 'B_SampleStatus'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "样本状态", ClassCName = "BSampleStatus", ShortCode = "BSampleStatus", Desc = "样本状态")]
	public class BSampleStatus : BaseEntity
	{
		#region Member Variables
		
        protected long? _operateObjectID;
        protected string _comment;
		protected BOperateObjectType _bOperateObjectType;
		protected BSampleStatusType _bSampleStatusType;
        protected int[] _bSampleList;

		#endregion

		#region Constructors

		public BSampleStatus() { }

		public BSampleStatus( long labID, long operateObjectID, string comment, DateTime dataAddTime, byte[] dataTimeStamp, BOperateObjectType bOperateObjectType, BSampleStatusType bSampleStatusType )
		{
			this._labID = labID;
			this._operateObjectID = operateObjectID;
			this._comment = comment;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bOperateObjectType = bOperateObjectType;
			this._bSampleStatusType = bSampleStatusType;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作对象ID", ShortCode = "OperateObjectID", Desc = "操作对象ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? OperateObjectID
		{
			get { return _operateObjectID; }
			set { _operateObjectID = value; }
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
        [DataDesc(CName = "操作对象类型", ShortCode = "BOperateObjectType", Desc = "操作对象类型")]
		public virtual BOperateObjectType BOperateObjectType
		{
			get { return _bOperateObjectType; }
			set { _bOperateObjectType = value; }
		}

        [DataMember]
        [DataDesc(CName = "样本状态类型表", ShortCode = "BSampleStatusType", Desc = "样本状态类型表")]
		public virtual BSampleStatusType BSampleStatusType
		{
			get { return _bSampleStatusType; }
			set { _bSampleStatusType = value; }
		}
        public virtual int[] BSampleList
        {
            get { return _bSampleList; }
            set { _bSampleList = value; }
        }
        
		#endregion
	}
	#endregion
}