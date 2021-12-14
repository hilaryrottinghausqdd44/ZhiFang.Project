using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Entity.WebAssist
{
    #region GKDeptAutoCheckLink

    /// <summary>
    /// GKDeptAutoCheckLink object for NHibernate mapped table 'GK_DeptAutoCheckLink'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "科室自动核收关系", ClassCName = "GKDeptAutoCheckLink", ShortCode = "GKDeptAutoCheckLink", Desc = "科室自动核收关系")]
    public class GKDeptAutoCheckLink : BaseEntity
    {
        #region Member Variables
       
        protected int _dispOrder;
        protected bool _isUse;
        protected Department _department;

        #endregion

        #region Constructors

        public GKDeptAutoCheckLink() { }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "科室", ShortCode = "Department", Desc = "科室")]
        public virtual Department Department
        {
            get { return _department; }
            set { _department = value; }
        }

        #endregion
    }
    #endregion
}