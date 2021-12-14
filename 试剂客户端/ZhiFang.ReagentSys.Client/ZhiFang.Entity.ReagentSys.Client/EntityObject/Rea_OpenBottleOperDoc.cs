using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaOpenBottleOperDoc

    /// <summary>
    /// ReaOpenBottleOperDoc object for NHibernate mapped table 'Rea_OpenBottleOperDoc'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "开瓶管理记录主单", ClassCName = "ReaOpenBottleOperDoc", ShortCode = "ReaOpenBottleOperDoc", Desc = "开瓶管理记录主单")]
    public class ReaOpenBottleOperDoc : BaseEntity
    {
        #region Member Variables

        protected long? _goodsID;
        protected long? _qtyDtlID;
        protected long? _outDtlID;
        protected long? _outDocID;
        protected DateTime? _bOpenDate;
        protected DateTime? _invalidBOpenDate;
        protected bool _isUseCompleteFlag;
        protected DateTime? _useCompleteDate;
        protected bool _isObsolete;
        protected long? _obsoleteID;
        protected long? _obsoleteMemoId;
        protected string _obsoleteName;
        protected DateTime? _obsoleteTime;
        protected string _obsoleteMemo;
        protected bool _visible;
        protected int _dispOrder;
        protected long? _createrID;
        protected string _createrName;
        protected string _memo;

        #endregion

        #region Constructors

        public ReaOpenBottleOperDoc() { }

        public ReaOpenBottleOperDoc(long labID, long goodsID, long qtyDtlID, long outDtlID, long outDocID, DateTime invalidBOpenDate, bool isUseCompleteFlag, DateTime useCompleteDate, long obsoleteID, long obsoleteMemoId, string obsoleteName, DateTime obsoleteTime, string obsoleteMemo, bool visible, int dispOrder, long createrID, string createrName, string memo, DateTime dataAddTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._goodsID = goodsID;
            this._qtyDtlID = qtyDtlID;
            this._outDtlID = outDtlID;
            this._outDocID = outDocID;
            this._invalidBOpenDate = invalidBOpenDate;
            this._isUseCompleteFlag = isUseCompleteFlag;
            this._useCompleteDate = useCompleteDate;
            this._obsoleteID = obsoleteID;
            this._obsoleteMemoId = obsoleteMemoId;
            this._obsoleteName = obsoleteName;
            this._obsoleteTime = obsoleteTime;
            this._obsoleteMemo = obsoleteMemo;
            this._visible = visible;
            this._dispOrder = dispOrder;
            this._createrID = createrID;
            this._createrName = createrName;
            this._memo = memo;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货品ID", ShortCode = "GoodsID", Desc = "货品ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsID
        {
            get { return _goodsID; }
            set { _goodsID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "库存ID", ShortCode = "QtyDtlID", Desc = "库存ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? QtyDtlID
        {
            get { return _qtyDtlID; }
            set { _qtyDtlID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "出库明细单ID", ShortCode = "OutDtlID", Desc = "出库明细单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OutDtlID
        {
            get { return _outDtlID; }
            set { _outDtlID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "出库总单ID", ShortCode = "OutDocID", Desc = "出库总单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OutDocID
        {
            get { return _outDocID; }
            set { _outDocID = value; }
        }

        /// <summary>
        /// 开瓶时间取出库明细登记时间
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "开瓶时间", ShortCode = "BOpenDate", Desc = "开瓶时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BOpenDate
        {
            get { return _bOpenDate; }
            set { _bOpenDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "库存货品开瓶后最后有效期", ShortCode = "InvalidBOpenDate", Desc = "库存货品开瓶后最后有效期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? InvalidBOpenDate
        {
            get { return _invalidBOpenDate; }
            set { _invalidBOpenDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "是使用完成标志", ShortCode = "IsUseCompleteFlag", Desc = "是使用完成标志", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUseCompleteFlag
        {
            get { return _isUseCompleteFlag; }
            set { _isUseCompleteFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "使用完成时间", ShortCode = "UseCompleteDate", Desc = "使用完成时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? UseCompleteDate
        {
            get { return _useCompleteDate; }
            set { _useCompleteDate = value; }
        }
        [DataMember]
        [DataDesc(CName = "是否作废", ShortCode = "IsObsolete", Desc = "是否作废", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsObsolete
        {
            get { return _isObsolete; }
            set { _isObsolete = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "作废人Id", ShortCode = "ObsoleteID", Desc = "作废人Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? ObsoleteID
        {
            get { return _obsoleteID; }
            set { _obsoleteID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "作废备注Id", ShortCode = "ObsoleteMemoId", Desc = "作废备注Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? ObsoleteMemoId
        {
            get { return _obsoleteMemoId; }
            set { _obsoleteMemoId = value; }
        }

        [DataMember]
        [DataDesc(CName = "作废人", ShortCode = "ObsoleteName", Desc = "作废人", ContextType = SysDic.All, Length = 50)]
        public virtual string ObsoleteName
        {
            get { return _obsoleteName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ObsoleteName", value, value.ToString());
                _obsoleteName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "作废时间", ShortCode = "ObsoleteTime", Desc = "作废时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ObsoleteTime
        {
            get { return _obsoleteTime; }
            set { _obsoleteTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "作废备注", ShortCode = "ObsoleteMemo", Desc = "作废备注", ContextType = SysDic.All, Length = 200)]
        public virtual string ObsoleteMemo
        {
            get { return _obsoleteMemo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ObsoleteMemo", value, value.ToString());
                _obsoleteMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
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
        [DataDesc(CName = "创建者ID", ShortCode = "CreaterID", Desc = "创建者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreaterID
        {
            get { return _createrID; }
            set { _createrID = value; }
        }

        [DataMember]
        [DataDesc(CName = "创建者姓名", ShortCode = "CreaterName", Desc = "创建者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string CreaterName
        {
            get { return _createrName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CreaterName", value, value.ToString());
                _createrName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = int.MaxValue)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
            }
        }


        #endregion

        #region 自定义属性
        protected ReaGoods _reaGoods;
        protected ReaBmsQtyDtl _reaBmsQtyDtl;
        [DataMember]
        [DataDesc(CName = "ReaBmsQtyDtl", ShortCode = "ReaBmsQtyDtl", Desc = "ReaBmsQtyDtl")]
        public virtual ReaBmsQtyDtl ReaBmsQtyDtl
        {
            get { return _reaBmsQtyDtl; }
            set { _reaBmsQtyDtl = value; }
        }
        
        [DataMember]
        [DataDesc(CName = "ReaGoods", ShortCode = "ReaGoods", Desc = "ReaGoods")]
        public virtual ReaGoods ReaGoods
        {
            get { return _reaGoods; }
            set { _reaGoods = value; }
        }
        #endregion

    }
    #endregion
}