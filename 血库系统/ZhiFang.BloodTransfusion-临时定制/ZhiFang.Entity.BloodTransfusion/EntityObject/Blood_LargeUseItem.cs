using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodLargeUseItem

    /// <summary>
    /// BloodLargeUseItem object for NHibernate mapped table 'Blood_LargeUseItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodLargeUseItem", ShortCode = "BloodLargeUseItem", Desc = "")]
    public class BloodLargeUseItem : BaseEntityService
    {
        #region Member Variables

        protected string _lUFID;
        protected string _bReqFormID;
        protected string _lUIMemo;
        protected int _dispOrder;
        protected bool _visible;
        protected long _largeDocId;


        #endregion

        #region Constructors

        public BloodLargeUseItem() { }

        public BloodLargeUseItem(string lUFID, string bReqFormID, string lUIMemo, long labID, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, bool visible, long largeDocId)
        {
            this._lUFID = lUFID;
            this._bReqFormID = bReqFormID;
            this._lUIMemo = lUIMemo;
            this._labID = labID;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._visible = visible;
            this._largeDocId = largeDocId;
        }

        #endregion

        #region Public Properties

        //审批批次号等于当时进行医嘱申请确认提交的医嘱申请单号(医嘱申请单Id)
        [DataMember]
        [DataDesc(CName = "审批批次号", ShortCode = "LUFID", Desc = "审批批次号", ContextType = SysDic.All, Length = 20)]
        public virtual string LUFID
        {
            get { return _lUFID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LUFID", value, value.ToString());
                _lUFID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "某一医嘱申请单号", ShortCode = "BReqFormID", Desc = "某一医嘱申请单号", ContextType = SysDic.All, Length = 40)]
        public virtual string BReqFormID
        {
            get { return _bReqFormID; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for BReqFormID", value, value.ToString());
                _bReqFormID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LUIMemo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string LUIMemo
        {
            get { return _lUIMemo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for LUIMemo", value, value.ToString());
                _lUIMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LargeDocId", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long LargeDocId
        {
            get { return _largeDocId; }
            set { _largeDocId = value; }
        }


        #endregion
    }
    #endregion
}