using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BAntiUse

	/// <summary>
	/// BAntiUse object for NHibernate mapped table 'B_AntiUse'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "抗生素用药方法", ClassCName = "BAntiUse", ShortCode = "BAntiUse", Desc = "抗生素用药方法")]
	public class BAntiUse : BaseEntity
	{
		#region Member Variables
		
        protected string _cName;
        protected string _comment;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected BAnti _bAnti;

		#endregion

		#region Constructors

		public BAntiUse() { }

		public BAntiUse( long labID, string cName, string comment, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BAnti bAnti )
		{
			this._labID = labID;
			this._cName = cName;
			this._comment = comment;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bAnti = bAnti;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 50)]
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

        
		#endregion
	}
	#endregion
}