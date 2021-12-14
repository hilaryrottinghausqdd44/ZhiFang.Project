using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region EPEquipItem

	/// <summary>
	/// EPEquipItem object for NHibernate mapped table 'EP_EquipItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "仪器项目关系", ClassCName = "", ShortCode = "YQXMGX", Desc = "仪器项目关系")]
    public class EPEquipItem : BaseEntity
	{
		#region Member Variables
		
		
		protected string _channel;
		protected bool _isUse;
		protected int _dispOrder;
		protected DateTime? _dataUpdateTime;
		protected EPBEquip _ePBEquip;
		protected ItemAllItem _itemAllItem;
        protected GMGroup _gMGroup;

		#endregion

		#region Constructors

		public EPEquipItem() { }

		public EPEquipItem( long labID, string channel, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, EPBEquip ePBEquip, ItemAllItem itemAllItem )
		{
			this._labID = labID;
			this._channel = channel;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._ePBEquip = ePBEquip;
			this._itemAllItem = itemAllItem;
		}

		#endregion

		#region Public Properties

        [DataMember]
        [DataDesc(CName = "仪器通道号", ShortCode = "YQTDH", Desc = "仪器通道号", ContextType = SysDic.NText, Length = 50)]
		public virtual string Channel
		{
			get { return _channel; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Channel", value, value.ToString());
				_channel = value;
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

        [DataMember]
        [DataDesc(CName = "项目", ShortCode = "XM", Desc = "项目")]
		public virtual ItemAllItem ItemAllItem
		{
			get { return _itemAllItem; }
			set { _itemAllItem = value; }
		}
       

        [DataMember]
        [DataDesc(CName = "小组表", ShortCode = "GMGroup", Desc = "小组表")]
        public virtual GMGroup GMGroup
        {
            get { return _gMGroup; }
            set { _gMGroup = value; }
        }

		#endregion
	}
	#endregion
}