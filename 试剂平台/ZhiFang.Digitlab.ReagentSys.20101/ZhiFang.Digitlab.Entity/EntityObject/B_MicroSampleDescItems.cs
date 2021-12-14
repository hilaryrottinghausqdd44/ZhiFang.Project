using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BMicroSampleDescItems

	/// <summary>
	/// BMicroSampleDescItems object for NHibernate mapped table 'B_MicroSampleDescItems'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物标本描述记录项", ClassCName = "BMicroSampleDescItems", ShortCode = "BMicroSampleDescItems", Desc = "微生物标本描述记录项")]
	public class BMicroSampleDescItems : BaseEntity
	{
		#region Member Variables
		
        protected bool _isDefault;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected BMicroTestItemInfo _bMicroTestItemInfo;
		protected BSampleType _bSampleType;
		protected IList<MEMicroSampleDescItemsResults> _mEMicroSampleDescItemsResultsList; 

		#endregion

		#region Constructors

		public BMicroSampleDescItems() { }

		public BMicroSampleDescItems( long labID, bool isDefault, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BMicroTestItemInfo bMicroTestItemInfo, BSampleType bSampleType )
		{
			this._labID = labID;
			this._isDefault = isDefault;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bMicroTestItemInfo = bMicroTestItemInfo;
			this._bSampleType = bSampleType;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "是否默认", ShortCode = "IsDefault", Desc = "是否默认", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsDefault
		{
			get { return _isDefault; }
			set { _isDefault = value; }
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
        [DataDesc(CName = "微生物检验记录项字典表", ShortCode = "BMicroTestItemInfo", Desc = "微生物检验记录项字典表")]
		public virtual BMicroTestItemInfo BMicroTestItemInfo
		{
			get { return _bMicroTestItemInfo; }
			set { _bMicroTestItemInfo = value; }
		}

        [DataMember]
        [DataDesc(CName = "样本类型", ShortCode = "BSampleType", Desc = "样本类型")]
		public virtual BSampleType BSampleType
		{
			get { return _bSampleType; }
			set { _bSampleType = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物标本描述记录项结果表", ShortCode = "MEMicroSampleDescItemsResultsList", Desc = "微生物标本描述记录项结果表")]
		public virtual IList<MEMicroSampleDescItemsResults> MEMicroSampleDescItemsResultsList
		{
			get
			{
				if (_mEMicroSampleDescItemsResultsList==null)
				{
					_mEMicroSampleDescItemsResultsList = new List<MEMicroSampleDescItemsResults>();
				}
				return _mEMicroSampleDescItemsResultsList;
			}
			set { _mEMicroSampleDescItemsResultsList = value; }
		}

        
		#endregion
	}
	#endregion
}