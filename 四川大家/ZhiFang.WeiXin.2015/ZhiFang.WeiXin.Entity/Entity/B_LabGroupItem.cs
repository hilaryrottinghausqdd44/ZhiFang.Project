using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
    #region BLabGroupItem

    /// <summary>
    /// BLabGroupItem object for NHibernate mapped table 'B_Lab_GroupItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "BLabGroupItem", ClassCName = "BLabGroupItem", ShortCode = "BLabGroupItem", Desc = "")]
    public class BLabGroupItem : BaseEntity
    {
        #region Member Variables
        protected string _labCode;
        protected string _pItemNo;
        protected string _itemNo;
        protected double? _Price;

        #endregion

        #region Constructors

        public BLabGroupItem() { }
        public BLabGroupItem(string labCode, string pItemNo, string itemNo, DateTime addTime, byte[] dataTimeStamp)
        {
            this._labCode = labCode;
            this._pItemNo = pItemNo;
            this._itemNo = itemNo;
            this._dataTimeStamp = dataTimeStamp;
        }
        #endregion

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "", ShortCode = "LabCode", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string LabCode
        {
            get { return _labCode; }
            set
            {
                _labCode = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "PItemNo", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string PItemNo
        {
            get { return _pItemNo; }
            set
            {
                _pItemNo = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemNo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ItemNo
        {
            get { return _itemNo; }
            set
            {
                _itemNo = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "组套内执行价格", ShortCode = "Price", Desc = "组套内执行价格", ContextType = SysDic.All, Length = 100)]
        public virtual double? Price
        {
            get { return _Price; }
            set
            {
                _Price = value;
            }
        }

        #endregion
    }
    #endregion
}