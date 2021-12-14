using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region Samplingitem

	/// <summary>
	/// Samplingitem object for NHibernate mapped table 'Samplingitem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "Samplingitem", ShortCode = "Samplingitem", Desc = "")]
	public class Samplingitem : BaseEntity
	{
		#region Member Variables
		
        protected int _dispOrder;
        protected int _isDefault;
        protected int _minItemCount;
        protected int _mustItem;
        protected double _itemCap;
        protected int _virtualItemNo;

		#endregion

		#region Constructors

		public Samplingitem() { }

		public Samplingitem( int dispOrder, int isDefault, int minItemCount, int mustItem, double itemCap, int virtualItemNo )
		{
			this._dispOrder = dispOrder;
			this._isDefault = isDefault;
			this._minItemCount = minItemCount;
			this._mustItem = mustItem;
			this._itemCap = itemCap;
			this._virtualItemNo = virtualItemNo;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsDefault", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsDefault
		{
			get { return _isDefault; }
			set { _isDefault = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MinItemCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int MinItemCount
		{
			get { return _minItemCount; }
			set { _minItemCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MustItem", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int MustItem
		{
			get { return _mustItem; }
			set { _mustItem = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ItemCap", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double ItemCap
		{
			get { return _itemCap; }
			set { _itemCap = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "VirtualItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int VirtualItemNo
		{
			get { return _virtualItemNo; }
			set { _virtualItemNo = value; }
		}

        
		#endregion
	}
	#endregion
}