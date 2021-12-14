using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
    #region BmsAccountSaleDoc

    /// <summary>
    /// BmsAccountSaleDoc object for NHibernate mapped table 'BmsAccountSaleDoc'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "入账管理关系表", ClassCName = "BmsAccountSaleDoc", ShortCode = "BmsAccountSaleDoc", Desc = "入账管理关系表")]
    public class BmsAccountSaleDoc : BaseEntity
    {
        #region Member Variables

        protected string _labName;
        protected long? _userID;
        protected string _userName;
        protected BmsAccountInput _bmsAccountInput;
        protected BmsCenSaleDoc _bmsCenSaleDoc;
        protected CenOrg _lab;

        #endregion

        #region Constructors

        public BmsAccountSaleDoc() { }

        public BmsAccountSaleDoc(string labName, long userID, string userName, DateTime dataAddTime, BmsAccountInput bmsAccountInput, BmsCenSaleDoc bmsCenSaleDoc, CenOrg lab)
        {
            this._labName = labName;
            this._userID = userID;
            this._userName = userName;
            this._dataAddTime = dataAddTime;
            this._bmsAccountInput = bmsAccountInput;
            this._bmsCenSaleDoc = bmsCenSaleDoc;
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
        [DataDesc(CName = "入账管理", ShortCode = "BmsAccountInput", Desc = "入账管理")]
        public virtual BmsAccountInput BmsAccountInput
        {
            get { return _bmsAccountInput; }
            set { _bmsAccountInput = value; }
        }

        [DataMember]
        [DataDesc(CName = "平台供货总单表", ShortCode = "BmsCenSaleDoc", Desc = "平台供货总单表")]
        public virtual BmsCenSaleDoc BmsCenSaleDoc
        {
            get { return _bmsCenSaleDoc; }
            set { _bmsCenSaleDoc = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Lab", Desc = "")]
        public virtual CenOrg Lab
        {
            get { return _lab; }
            set { _lab = value; }
        }


        #endregion
    }
    #endregion
}