using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
	#region TestEquipType

	/// <summary>
	/// TestEquipType object for NHibernate mapped table 'TestEquipType'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaTestEquipType", ShortCode = "ReaTestEquipType", Desc = "")]
	public class ReaTestEquipType : BaseEntity
	{
		#region Member Variables
		
        protected string _cName;
        protected string _eName;
        protected string _shortCode;
        protected string _memo;
        protected int _visible;
        protected int _dispOrder;
        protected DateTime _dataUpdateTime;
		

		#endregion

		#region Constructors

		public ReaTestEquipType() { }

		public ReaTestEquipType( string cName, string eName, string shortCode, string memo, int visible, int dispOrder, DateTime dataUpdateTime, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._cName = cName;
			this._eName = eName;
			this._shortCode = shortCode;
			this._memo = memo;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._dataUpdateTime = dataUpdateTime;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "中文名称", ShortCode = "CName", Desc = "中文名称", ContextType = SysDic.All, Length = 300)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 200)]
        public virtual string EName
		{
			get { return _eName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
				_eName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "代码", ShortCode = "ShortCode", Desc = "代码", ContextType = SysDic.All, Length = 100)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
				_memo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
		{
			get { return _visible; }
			set { _visible = value; }
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
        [DataDesc(CName = "修改时间", ShortCode = "DataUpdateTime", Desc = "修改时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		
		#endregion
	}
	#endregion
}