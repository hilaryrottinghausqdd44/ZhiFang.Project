using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBItemCalc

    /// <summary>
    /// LBItemCalc object for NHibernate mapped table 'LB_ItemCalc'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBItemCalc", ShortCode = "LBItemCalc", Desc = "")]
    public class LBItemCalc : BaseEntity
    {
        #region Member Variables

        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected LBItem _calc;
        protected LBItem _lBItem;

        #endregion

        #region Constructors

        public LBItemCalc() { }

        public LBItemCalc(int dispOrder, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBItem calc, LBItem lBItem)
        {
            this._dispOrder = dispOrder;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._calc = calc;
            this._lBItem = lBItem;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Calc", Desc = "")]
        public virtual LBItem LBCalcItem
        {
            get { return _calc; }
            set { _calc = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBItem", Desc = "")]
        public virtual LBItem LBItem
        {
            get { return _lBItem; }
            set { _lBItem = value; }
        }


        #endregion
    }
    #endregion
}