using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaEquipReagentLink

    /// <summary>
    /// ReaEquipReagentLink object for NHibernate mapped table 'Rea_EquipReagentLink'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaEquipReagentLink", ShortCode = "ReaEquipReagentLink", Desc = "")]
    public class ReaEquipReagentLink : BaseEntity
    {
        #region Member Variables

        protected long? _testEquipID;
        protected long? _goodsID;
        protected string _memo;
        protected int _visible;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;


        #endregion

        #region Constructors

        public ReaEquipReagentLink() { }

        public ReaEquipReagentLink(long labID, long testEquipID, long goodsID, string memo, int visible, int dispOrder, DateTime dataUpdateTime, DateTime dataAddTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._testEquipID = testEquipID;
            this._goodsID = goodsID;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TestEquipID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? TestEquipID
        {
            get { return _testEquipID; }
            set { _testEquipID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GoodsID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsID
        {
            get { return _goodsID; }
            set { _goodsID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }


        #endregion

        #region 自定义
        //protected long? _goodsUnitID;
        //[DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        //[DataDesc(CName = "包装单位ID(CS导入BS用)", ShortCode = "GoodsUnitID", Desc = "包装单位ID(CS导入BS用)", ContextType = SysDic.All, Length = 8)]
        //public virtual long? GoodsUnitID
        //{
        //    get { return _goodsUnitID; }
        //    set { _goodsUnitID = value; }
        //}
        [DataMember]
        [DataDesc(CName = "仪器信息", ShortCode = "ReaTestEquipLab", Desc = "仪器信息")]
        public virtual ReaTestEquipLab ReaTestEquipLab { get; set; }
        [DataMember]
        [DataDesc(CName = "货品表", ShortCode = "ReaGoods", Desc = "货品表")]
        public virtual ReaGoods ReaGoods { get; set; }
        public ReaEquipReagentLink(ReaEquipReagentLink reaequipreagentlink, ReaTestEquipLab reatestequiplab, ReaGoods reagoods)
        {
            this._id = reaequipreagentlink.Id;
            this._labID = reaequipreagentlink.LabID;
            this._testEquipID = reaequipreagentlink.TestEquipID;
            this._goodsID = reaequipreagentlink.GoodsID;
            this._memo = reaequipreagentlink.Memo;
            this._visible = reaequipreagentlink.Visible;
            this._dispOrder = reaequipreagentlink.DispOrder;
            this._dataUpdateTime = reaequipreagentlink.DataUpdateTime;
            this._dataAddTime = reaequipreagentlink.DataAddTime;
            this._dataTimeStamp = reaequipreagentlink.DataTimeStamp;
            this.ReaTestEquipLab = reatestequiplab;
            this.ReaGoods = reagoods;
        }
        #endregion
    }
    #endregion

}