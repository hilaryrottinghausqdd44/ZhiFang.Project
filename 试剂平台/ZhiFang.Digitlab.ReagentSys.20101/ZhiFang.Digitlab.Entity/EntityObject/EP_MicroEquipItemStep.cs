using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region EPMicroEquipItemStep

	/// <summary>
	/// EPMicroEquipItemStep object for NHibernate mapped table 'EP_MicroEquipItemStep'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物仪器项目默认检验步骤", ClassCName = "EPMicroEquipItemStep", ShortCode = "EPMicroEquipItemStep", Desc = "微生物仪器项目默认检验步骤")]
	public class EPMicroEquipItemStep : BaseEntity
	{
		#region Member Variables
		
        protected int _equipResultType;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected BMicroItemTestStep _bMicroItemTestStep;
		protected EPEquipItem _ePEquipItem;

		#endregion

		#region Constructors

		public EPMicroEquipItemStep() { }

		public EPMicroEquipItemStep( long labID, int equipResultType, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BMicroItemTestStep bMicroItemTestStep, EPEquipItem ePEquipItem )
		{
			this._labID = labID;
			this._equipResultType = equipResultType;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bMicroItemTestStep = bMicroItemTestStep;
			this._ePEquipItem = ePEquipItem;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "仪器结果类型:0-阴性结果；1-阳性结果", ShortCode = "EquipResultType", Desc = "仪器结果类型:0-阴性结果；1-阳性结果", ContextType = SysDic.All, Length = 4)]
        public virtual int EquipResultType
		{
			get { return _equipResultType; }
			set { _equipResultType = value; }
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
        [DataDesc(CName = "项目微生物检验步骤关系", ShortCode = "BMicroItemTestStep", Desc = "项目微生物检验步骤关系")]
		public virtual BMicroItemTestStep BMicroItemTestStep
		{
			get { return _bMicroItemTestStep; }
			set { _bMicroItemTestStep = value; }
		}

        [DataMember]
        [DataDesc(CName = "仪器项目关系表", ShortCode = "EPEquipItem", Desc = "仪器项目关系表")]
		public virtual EPEquipItem EPEquipItem
		{
			get { return _ePEquipItem; }
			set { _ePEquipItem = value; }
		}

        
		#endregion
	}
	#endregion
}