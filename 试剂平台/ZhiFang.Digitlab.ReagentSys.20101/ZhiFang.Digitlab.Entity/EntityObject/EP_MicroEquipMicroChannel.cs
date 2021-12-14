using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region EPMicroEquipMicroChannel

	/// <summary>
	/// EPMicroEquipMicroChannel object for NHibernate mapped table 'EP_MicroEquipMicroChannel'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物仪器微生物通道对应表", ClassCName = "EPMicroEquipMicroChannel", ShortCode = "EPMicroEquipMicroChannel", Desc = "微生物仪器微生物通道对应表")]
	public class EPMicroEquipMicroChannel : BaseEntity
	{
		#region Member Variables
		
        protected string _microChannel;
        protected int _dSTType;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected BMicro _bMicro;
		protected EPBEquip _ePBEquip;

		#endregion

		#region Constructors

		public EPMicroEquipMicroChannel() { }

		public EPMicroEquipMicroChannel( long labID, string microChannel, int dSTType, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BMicro bMicro, EPBEquip ePBEquip )
		{
			this._labID = labID;
			this._microChannel = microChannel;
			this._dSTType = dSTType;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bMicro = bMicro;
			this._ePBEquip = ePBEquip;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "MicroChannel", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string MicroChannel
		{
			get { return _microChannel; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for MicroChannel", value, value.ToString());
				_microChannel = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "默认药敏实验类型", ShortCode = "DSTType", Desc = "默认药敏实验类型", ContextType = SysDic.All, Length = 4)]
        public virtual int DSTType
		{
			get { return _dSTType; }
			set { _dSTType = value; }
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
        [DataDesc(CName = "仪器表", ShortCode = "EPBEquip", Desc = "仪器表")]
		public virtual EPBEquip EPBEquip
		{
			get { return _ePBEquip; }
			set { _ePBEquip = value; }
		}

        
		#endregion
	}
	#endregion
}