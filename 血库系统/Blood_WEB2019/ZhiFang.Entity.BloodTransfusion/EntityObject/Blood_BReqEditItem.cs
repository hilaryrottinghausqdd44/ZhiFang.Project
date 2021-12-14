using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodBReqEditItem

    /// <summary>
    /// BloodBReqEditItem object for NHibernate mapped table 'Blood_BReqEditItem'.
    /// BtestItemNo作为主键
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodBReqEditItem", ShortCode = "BloodBReqEditItem", Desc = "")]
    public class BloodBReqEditItem : BaseEntityServiceByString
    {
        #region Member Variables

        protected string _btestItemNo;
        protected string _btestItemName;
        protected string _bEIName;
        protected int _dispOrder;
        protected bool _visible;
        protected string _lisCode;
        protected string _reqCode;
        #endregion

        #region Constructors

        public BloodBReqEditItem() { }

        public BloodBReqEditItem(string btestItemName, string bEIName, int dispOrder, long labID, DateTime dataAddTime, byte[] dataTimeStamp, bool visible)
        {
            this._btestItemName = btestItemName;
            this._bEIName = bEIName;
            this._dispOrder = dispOrder;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._visible = visible;
        }

        #endregion

        #region Public Properties

        //[DataMember]
        //[DataDesc(CName = "主键", ShortCode = "BtestItemNo", Desc = "主键", ContextType = SysDic.All, Length = 20)]
        //public virtual string BtestItemNo
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(_btestItemNo))
        //            _btestItemNo = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString();
        //        return _btestItemNo;
        //    }
        //    set
        //    {
        //        if (value != null && value.Length > 20)
        //            throw new ArgumentOutOfRangeException("Invalid value for BtestItemNo", value, value.ToString());
        //        _btestItemNo = value;
        //    }
        //}
        [DataMember]
        [DataDesc(CName = "医嘱(项目)结果对照码", ShortCode = "ReqCode", Desc = "医嘱(项目)结果对照码", ContextType = SysDic.All, Length = 20)]
        public virtual string ReqCode
        {
            get { return _reqCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ReqCode", value, value.ToString());
                _reqCode = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisCode
        {
            get { return _lisCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisCode", value, value.ToString());
                _lisCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BtestItemName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BtestItemName
        {
            get { return _btestItemName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BtestItemName", value, value.ToString());
                _btestItemName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BEIName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BEIName
        {
            get { return _bEIName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BEIName", value, value.ToString());
                _bEIName = value;
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


        #endregion
    }
    #endregion
}