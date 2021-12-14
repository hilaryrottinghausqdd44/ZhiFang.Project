using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region EPMicroEquipAntiChannel

	/// <summary>
	/// EPMicroEquipAntiChannel object for NHibernate mapped table 'EP_MicroEquipAntiChannel'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物仪器抗生素通道对应表", ClassCName = "EPMicroEquipAntiChannel", ShortCode = "EPMicroEquipAntiChannel", Desc = "微生物仪器抗生素通道对应表")]
	public class EPMicroEquipAntiChannel : BaseEntity
	{
		#region Member Variables
		
        protected string _antiChannel;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected BAnti _bAnti;
		protected EPBEquip _ePBEquip;

		#endregion

		#region Constructors

		public EPMicroEquipAntiChannel() { }

		public EPMicroEquipAntiChannel( long labID, string antiChannel, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BAnti bAnti, EPBEquip ePBEquip )
		{
			this._labID = labID;
			this._antiChannel = antiChannel;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bAnti = bAnti;
			this._ePBEquip = ePBEquip;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "抗生素通道号", ShortCode = "AntiChannel", Desc = "抗生素通道号", ContextType = SysDic.All, Length = 50)]
        public virtual string AntiChannel
		{
			get { return _antiChannel; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for AntiChannel", value, value.ToString());
				_antiChannel = value;
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