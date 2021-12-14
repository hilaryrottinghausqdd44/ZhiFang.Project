using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodBReqTypeItem

    /// <summary>
    /// BloodBReqTypeItem object for NHibernate mapped table 'Blood_BReqTypeItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodBReqTypeItem", ShortCode = "BloodBReqTypeItem", Desc = "")]
    public class BloodBReqTypeItem : BaseEntityServiceByString
    {
        #region Member Variables
        protected string _bReqTypeItemID;
        protected string _bReqTypeItem;
        protected int _dispOrder;
        protected bool _visible;

        #endregion

        #region Constructors

        public BloodBReqTypeItem() { }

        public BloodBReqTypeItem(string bReqTypeItem, int dispOrder)
        {
            this._bReqTypeItem = bReqTypeItem;
            this._dispOrder = dispOrder;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "", ShortCode = "BReqTypeItemID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BReqTypeItemID
        {
            get { return _bReqTypeItemID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BReqTypeItem", value, value.ToString());
                _bReqTypeItemID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BReqTypeItem", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BReqTypeItem
        {
            get { return _bReqTypeItem; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BReqTypeItem", value, value.ToString());
                _bReqTypeItem = value;
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