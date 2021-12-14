using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodBPreItem

	/// <summary>
	/// BloodBPreItem object for NHibernate mapped table 'Blood_BPreItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "配血明细表", ClassCName = "BloodBPreItem", ShortCode = "BloodBPreItem", Desc = "配血明细表")]
	public class BloodBPreItem : BaseEntity
	{
		#region Member Variables
		
        protected string _bPreItemNo;
        protected string _msResult1;
        protected string _msResult2;
        protected string _msResult3;
        protected string _msresult4;
        protected string _ssResult1;
        protected string _ssResult2;
        protected string _ssResult3;
        protected string _ssresult4;
        protected string _bPreSame;
        protected long? _bPreItemCheckID;
        protected DateTime? _bPreItemCheckTime;
        protected int _bPreItemCheckFlag;
        protected string _gbloodscan;
        protected string _bagprocess;
        protected string _memo;
        protected int _dispOrder;
        protected bool _visible;
		protected BloodABO _bloodABO;
		protected BloodBPreForm _bloodBPreForm;
		protected BDict _msWay1;
		protected BDict _msWay2;
		protected BDict _msWay3;
		protected BDict _msWay4;
		protected BloodBReqForm _bloodBReqForm;
		protected BloodBReqItem _bloodBReqItem;
		protected BloodQtyDtl _bloodQtyDtl;
		protected BloodStyle _bloodStyle;

		#endregion

		#region Constructors

		public BloodBPreItem() { }

		public BloodBPreItem( long labID, string bPreItemNo, string msResult1, string msResult2, string msResult3, string msresult4, string ssResult1, string ssResult2, string ssResult3, string ssresult4, string bPreSame, long bPreItemCheckID, DateTime bPreItemCheckTime, int bPreItemCheckFlag, string gbloodscan, string bagprocess, string memo, int dispOrder, bool visible, DateTime dataAddTime, byte[] dataTimeStamp, BloodABO bloodABO, BloodBPreForm bloodBPreForm, BDict msWay1, BDict msWay2, BDict msWay3, BDict msWay4, BloodBReqForm bloodBReqForm, BloodBReqItem bloodBReqItem, BloodQtyDtl bloodQtyDtl, BloodStyle bloodStyle )
		{
			this._labID = labID;
			this._bPreItemNo = bPreItemNo;
			this._msResult1 = msResult1;
			this._msResult2 = msResult2;
			this._msResult3 = msResult3;
			this._msresult4 = msresult4;
			this._ssResult1 = ssResult1;
			this._ssResult2 = ssResult2;
			this._ssResult3 = ssResult3;
			this._ssresult4 = ssresult4;
			this._bPreSame = bPreSame;
			this._bPreItemCheckID = bPreItemCheckID;
			this._bPreItemCheckTime = bPreItemCheckTime;
			this._bPreItemCheckFlag = bPreItemCheckFlag;
			this._gbloodscan = gbloodscan;
			this._bagprocess = bagprocess;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._visible = visible;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodABO = bloodABO;
			this._bloodBPreForm = bloodBPreForm;
			this._msWay1 = msWay1;
			this._msWay2 = msWay2;
			this._msWay3 = msWay3;
			this._msWay4 = msWay4;
			this._bloodBReqForm = bloodBReqForm;
			this._bloodBReqItem = bloodBReqItem;
			this._bloodQtyDtl = bloodQtyDtl;
			this._bloodStyle = bloodStyle;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "配血明细单号", ShortCode = "BPreItemNo", Desc = "配血明细单号", ContextType = SysDic.All, Length = 20)]
        public virtual string BPreItemNo
		{
			get { return _bPreItemNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BPreItemNo", value, value.ToString());
				_bPreItemNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "主侧配血结果1", ShortCode = "MsResult1", Desc = "主侧配血结果1", ContextType = SysDic.All, Length = 50)]
        public virtual string MsResult1
		{
			get { return _msResult1; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for MsResult1", value, value.ToString());
				_msResult1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "主侧配血结果2", ShortCode = "MsResult2", Desc = "主侧配血结果2", ContextType = SysDic.All, Length = 50)]
        public virtual string MsResult2
		{
			get { return _msResult2; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for MsResult2", value, value.ToString());
				_msResult2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "主侧配血结果3", ShortCode = "MsResult3", Desc = "主侧配血结果3", ContextType = SysDic.All, Length = 50)]
        public virtual string MsResult3
		{
			get { return _msResult3; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for MsResult3", value, value.ToString());
				_msResult3 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "主侧配血结果4", ShortCode = "Msresult4", Desc = "主侧配血结果4", ContextType = SysDic.All, Length = 50)]
        public virtual string Msresult4
		{
			get { return _msresult4; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Msresult4", value, value.ToString());
				_msresult4 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "次侧配血结果1", ShortCode = "SsResult1", Desc = "次侧配血结果1", ContextType = SysDic.All, Length = 50)]
        public virtual string SsResult1
		{
			get { return _ssResult1; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SsResult1", value, value.ToString());
				_ssResult1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "次侧配血结果2", ShortCode = "SsResult2", Desc = "次侧配血结果2", ContextType = SysDic.All, Length = 50)]
        public virtual string SsResult2
		{
			get { return _ssResult2; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SsResult2", value, value.ToString());
				_ssResult2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "次侧配血结果3", ShortCode = "SsResult3", Desc = "次侧配血结果3", ContextType = SysDic.All, Length = 50)]
        public virtual string SsResult3
		{
			get { return _ssResult3; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SsResult3", value, value.ToString());
				_ssResult3 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "次侧配血结果4", ShortCode = "Ssresult4", Desc = "次侧配血结果4", ContextType = SysDic.All, Length = 50)]
        public virtual string Ssresult4
		{
			get { return _ssresult4; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Ssresult4", value, value.ToString());
				_ssresult4 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "配血一致性", ShortCode = "BPreSame", Desc = "配血一致性", ContextType = SysDic.All, Length = 10)]
        public virtual string BPreSame
		{
			get { return _bPreSame; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for BPreSame", value, value.ToString());
				_bPreSame = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "配血审核ID", ShortCode = "BPreItemCheckID", Desc = "配血审核ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? BPreItemCheckID
		{
			get { return _bPreItemCheckID; }
			set { _bPreItemCheckID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "配血审核时间", ShortCode = "BPreItemCheckTime", Desc = "配血审核时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? BPreItemCheckTime
		{
			get { return _bPreItemCheckTime; }
			set { _bPreItemCheckTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "配血审核标志", ShortCode = "BPreItemCheckFlag", Desc = "配血审核标志", ContextType = SysDic.All, Length = 4)]
        public virtual int BPreItemCheckFlag
		{
			get { return _bPreItemCheckFlag; }
			set { _bPreItemCheckFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "扫描标志", ShortCode = "Gbloodscan", Desc = "扫描标志", ContextType = SysDic.All, Length = 20)]
        public virtual string Gbloodscan
		{
			get { return _gbloodscan; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Gbloodscan", value, value.ToString());
				_gbloodscan = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "加工内容信息", ShortCode = "Bagprocess", Desc = "加工内容信息", ContextType = SysDic.All, Length = 50)]
        public virtual string Bagprocess
		{
			get { return _bagprocess; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Bagprocess", value, value.ToString());
				_bagprocess = value;
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
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "血型表", ShortCode = "BloodABO", Desc = "血型表")]
		public virtual BloodABO BloodABO
		{
			get { return _bloodABO; }
			set { _bloodABO = value; }
		}

        [DataMember]
        [DataDesc(CName = "配血主单", ShortCode = "BloodBPreForm", Desc = "配血主单")]
		public virtual BloodBPreForm BloodBPreForm
		{
			get { return _bloodBPreForm; }
			set { _bloodBPreForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "配血方法1", ShortCode = "MsWay1", Desc = "配血方法1")]
		public virtual BDict MsWay1
		{
			get { return _msWay1; }
			set { _msWay1 = value; }
		}

        [DataMember]
        [DataDesc(CName = "配血方法2", ShortCode = "MsWay2", Desc = "配血方法2")]
		public virtual BDict MsWay2
		{
			get { return _msWay2; }
			set { _msWay2 = value; }
		}

        [DataMember]
        [DataDesc(CName = "配血方法3", ShortCode = "MsWay3", Desc = "配血方法3")]
		public virtual BDict MsWay3
		{
			get { return _msWay3; }
			set { _msWay3 = value; }
		}

        [DataMember]
        [DataDesc(CName = "配血方法4", ShortCode = "MsWay4", Desc = "配血方法4")]
		public virtual BDict MsWay4
		{
			get { return _msWay4; }
			set { _msWay4 = value; }
		}

        [DataMember]
        [DataDesc(CName = "用血申请主单表", ShortCode = "BloodBReqForm", Desc = "用血申请主单表")]
		public virtual BloodBReqForm BloodBReqForm
		{
			get { return _bloodBReqForm; }
			set { _bloodBReqForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "用血申请明细信息表", ShortCode = "BloodBReqItem", Desc = "用血申请明细信息表")]
		public virtual BloodBReqItem BloodBReqItem
		{
			get { return _bloodBReqItem; }
			set { _bloodBReqItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "库存表", ShortCode = "BloodQtyDtl", Desc = "库存表")]
		public virtual BloodQtyDtl BloodQtyDtl
		{
			get { return _bloodQtyDtl; }
			set { _bloodQtyDtl = value; }
		}

        [DataMember]
        [DataDesc(CName = "血制品字典", ShortCode = "BloodStyle", Desc = "血制品字典")]
		public virtual BloodStyle BloodStyle
		{
			get { return _bloodStyle; }
			set { _bloodStyle = value; }
		}

		#endregion
	}
	#endregion
}