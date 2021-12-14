using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Entity.WebAssist
{
    #region BDictMapingVO

    /// <summary>
    /// BDictMapingVO object for NHibernate mapped table 'Blood_Interface_Maping'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "接口映射(对照)关系表", ClassCName = "BDictMapingVO", ShortCode = "BDictMapingVO", Desc = "接口映射(对照)关系表")]
    public class BDictMapingVO
    {
        #region Member Variables
        protected SCInterfaceMaping _scInterfaceMaping;
        protected BDict _bDict;
        protected PUser _pUser;
        protected Department _department;
        #endregion

        #region Constructors

        public BDictMapingVO() { }
        public BDictMapingVO(SCInterfaceMaping scInterfaceMaping, BDict bDict)
        {
            this._scInterfaceMaping = scInterfaceMaping;
            this._bDict = bDict;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "对照字典关系信息", ShortCode = "SCInterfaceMaping", Desc = "对照字典关系信息")]
        public virtual SCInterfaceMaping SCInterfaceMaping
        {
            get { return _scInterfaceMaping; }
            set { _scInterfaceMaping = value; }
        }
        [DataMember]
        [DataDesc(CName = "对照字典信息", ShortCode = "BDict", Desc = "对照字典信息")]
        public virtual BDict BDict
        {
            get { return _bDict; }
            set { _bDict = value; }
        }

        [DataMember]
        [DataDesc(CName = "用户信息", ShortCode = "PUser", Desc = "用户信息")]
        public virtual PUser PUser
        {
            get { return _pUser; }
            set { _pUser = value; }
        }
        [DataMember]
        [DataDesc(CName = "部门信息", ShortCode = "Department", Desc = "部门信息")]
        public virtual Department Department
        {
            get { return _department; }
            set { _department = value; }
        }
        #endregion
    }
    #endregion
}
