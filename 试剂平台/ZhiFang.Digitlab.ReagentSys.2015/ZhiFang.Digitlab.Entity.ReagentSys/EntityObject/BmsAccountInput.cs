using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
    #region BmsAccountInput

    /// <summary>
    /// BmsAccountInput object for NHibernate mapped table 'BmsAccountInput'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "入账管理", ClassCName = "BmsAccountInput", ShortCode = "BmsAccountInput", Desc = "入账管理")]
    public class BmsAccountInput : BaseEntity
    {
        #region Member Variables

        protected string _labName;
        protected string _cName;
        protected string _eName;
        protected string _shortCode;
        protected string _comment;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected double _totalPrice;
        protected long? _userID;
        protected string _userName;
        protected CenOrg _lab;
        protected IList<BmsAccountSaleDoc> _bmsAccountSaleDocList;

        #endregion

        #region Constructors

        public BmsAccountInput() { }

        public BmsAccountInput(string labName, string cName, string eName, string shortCode, string comment, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, double totalPrice, long userID, string userName, CenOrg lab)
        {
            this._labName = labName;
            this._cName = cName;
            this._eName = eName;
            this._shortCode = shortCode;
            this._comment = comment;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._totalPrice = totalPrice;
            this._userID = userID;
            this._userName = userName;
            this._lab = lab;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "实验室名称", ShortCode = "LabName", Desc = "实验室名称", ContextType = SysDic.All, Length = 100)]
        public virtual string LabName
        {
            get { return _labName; }
            set { _labName = value; }
        }

        [DataMember]
        [DataDesc(CName = "中文名称", ShortCode = "CName", Desc = "中文名称", ContextType = SysDic.All, Length = 100)]
        public virtual string CName
        {
            get { return _cName; }
            set { _cName = value; }
        }

        [DataMember]
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 100)]
        public virtual string EName
        {
            get { return _eName; }
            set { _eName = value; }
        }

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "ShortCode", Desc = "简称", ContextType = SysDic.All, Length = 100)]
        public virtual string ShortCode
        {
            get { return _shortCode; }
            set { _shortCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 1000)]
        public virtual string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "总计金额", ShortCode = "TotalPrice", Desc = "总计金额", ContextType = SysDic.All, Length = 8)]
        public virtual double TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "创建人ID", ShortCode = "UserID", Desc = "创建人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        [DataMember]
        [DataDesc(CName = "创建人", ShortCode = "UserName", Desc = "创建人", ContextType = SysDic.All, Length = 50)]
        public virtual string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Lab", Desc = "")]
        public virtual CenOrg Lab
        {
            get { return _lab; }
            set { _lab = value; }
        }

        [DataMember]
        [DataDesc(CName = "入账管理", ShortCode = "BmsAccountSaleDocList", Desc = "入账管理")]
        public virtual IList<BmsAccountSaleDoc> BmsAccountSaleDocList
        {
            get
            {
                if (_bmsAccountSaleDocList == null)
                {
                    _bmsAccountSaleDocList = new List<BmsAccountSaleDoc>();
                }
                return _bmsAccountSaleDocList;
            }
            set { _bmsAccountSaleDocList = value; }
        }


        #endregion
    }
    #endregion
}