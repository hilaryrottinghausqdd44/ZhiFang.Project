using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
    #region GroupItem

    /// <summary>
    /// GroupItem object for NHibernate mapped table 'GroupItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "GroupItem", ShortCode = "GroupItem", Desc = "")]
    public class GroupItem : BaseEntityService
    {
        #region Member Variables
        protected int _pItemNo;
        protected int _itemNo;
        protected int _lowRate;

        #endregion

        #region Constructors
        #region ¶àÖ÷¼ü
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
        public GroupItem() { }

        public GroupItem(int pItemNo, int itemNo, int lowRate)
        {
            this._pItemNo = pItemNo;
            this._itemNo = itemNo;
            this._lowRate = lowRate;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "", ShortCode = "PItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PItemNo
        {
            get { return _pItemNo; }
            set { _pItemNo = value; }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ItemNo
        {
            get { return _itemNo; }
            set { _itemNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LowRate", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int LowRate
        {
            get { return _lowRate; }
            set { _lowRate = value; }
        }


        #endregion
    }
    #endregion
}