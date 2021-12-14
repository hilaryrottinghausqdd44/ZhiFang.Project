using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region EPModule

	/// <summary>
	/// EPModule object for NHibernate mapped table 'EP_Module'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "仪器模块", ClassCName = "", ShortCode = "YQMK", Desc = "仪器模块")]
    public class EPModule : BaseEntity
	{
		#region Member Variables
		
		
		protected string _equipModuleCode;
		protected string _cName;
		protected string _eName;
		protected string _sName;
		protected string _comment;
		protected bool _isUse;
		protected int _dispOrder;
		protected DateTime? _dataUpdateTime;
		protected EPBEquip _ePBEquip;

		#endregion

		#region Constructors

		public EPModule() { }

		public EPModule( long labID, string equipModuleCode, string cName, string eName, string sName, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, EPBEquip ePBEquip )
		{
			this._labID = labID;
			this._equipModuleCode = equipModuleCode;
			this._cName = cName;
			this._eName = eName;
			this._sName = sName;
			this._comment = comment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._ePBEquip = ePBEquip;
		}

		#endregion

		#region Public Properties

        [DataMember]
        [DataDesc(CName = "模块Code", ShortCode = "MKCODE", Desc = "模块Code", ContextType = SysDic.NText, Length = 20)]
		public virtual string EquipModuleCode
		{
			get { return _equipModuleCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EquipModuleCode", value, value.ToString());
				_equipModuleCode = value;
			}
		}

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
        [DataDesc(CName = "英文名称", ShortCode = "YWMC", Desc = "英文名称", ContextType = SysDic.NText, Length = 50)]
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
        [DataDesc(CName = "简称", ShortCode = "JC", Desc = "简称", ContextType = SysDic.NText, Length = 50)]
		public virtual string SName
		{
			get { return _sName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "描述", ShortCode = "MS", Desc = "描述", ContextType = SysDic.NText, Length = 50)]
		public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 1000000)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "SFSY", Desc = "是否使用", ContextType = SysDic.All)]
		public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "XSCX", Desc = "显示次序", ContextType = SysDic.Number, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "SJGXSJ", Desc = "数据更新时间", ContextType = SysDic.DateTime)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        
        [DataMember]
        [DataDesc(CName = "仪器", ShortCode = "YQ", Desc = "仪器")]
		public virtual EPBEquip EPBEquip
		{
			get { return _ePBEquip; }
			set { _ePBEquip = value; }
		}

		#endregion
	}
	#endregion
}