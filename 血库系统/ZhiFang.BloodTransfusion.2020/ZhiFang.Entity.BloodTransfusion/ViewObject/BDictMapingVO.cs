using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Entity.BloodTransfusion
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
        protected BloodInterfaceMaping _bloodInterfaceMaping;
        protected BDict _bDict;
        protected BloodChargeItem _bloodChargeItem;
        protected BloodStyle _bloodStyle;
        protected BloodBTestItem _bloodBTestItem;
        protected BloodBagProcessType _bloodBagProcessType;
        protected BloodUnit _bloodUnit;
        protected BloodABO _bloodABO;
        protected PUser _pUser;
        protected Department _department;
        #endregion

        #region Constructors

        public BDictMapingVO() { }
        public BDictMapingVO(BloodInterfaceMaping bloodInterfaceMaping, BDict bDict)
        {
            this._bloodInterfaceMaping = bloodInterfaceMaping;
            this._bDict = bDict;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "对照字典关系信息", ShortCode = "BloodInterfaceMaping", Desc = "对照字典关系信息")]
        public virtual BloodInterfaceMaping BloodInterfaceMaping
        {
            get { return _bloodInterfaceMaping; }
            set { _bloodInterfaceMaping = value; }
        }
        [DataMember]
        [DataDesc(CName = "对照字典信息", ShortCode = "BDict", Desc = "对照字典信息")]
        public virtual BDict BDict
        {
            get { return _bDict; }
            set { _bDict = value; }
        }

        [DataMember]
        [DataDesc(CName = "费用项目信息", ShortCode = "BloodChargeItem", Desc = "费用项目信息")]
        public virtual BloodChargeItem BloodChargeItem
        {
            get { return _bloodChargeItem; }
            set { _bloodChargeItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "血制品信息", ShortCode = "BloodStyle", Desc = "血制品信息")]
        public virtual BloodStyle BloodStyle
        {
            get { return _bloodStyle; }
            set { _bloodStyle = value; }
        }

        [DataMember]
        [DataDesc(CName = "检验项目信息", ShortCode = "BloodBTestItem", Desc = "检验项目信息")]
        public virtual BloodBTestItem BloodBTestItem
        {
            get { return _bloodBTestItem; }
            set { _bloodBTestItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "血制品信息", ShortCode = "BloodBagProcessType", Desc = "血制品信息")]
        public virtual BloodBagProcessType BloodBagProcessType
        {
            get { return _bloodBagProcessType; }
            set { _bloodBagProcessType = value; }
        }

        [DataMember]
        [DataDesc(CName = "血制品单位信息", ShortCode = "BloodUnit", Desc = "血制品单位信息")]
        public virtual BloodUnit BloodUnit
        {
            get { return _bloodUnit; }
            set { _bloodUnit = value; }
        }
        [DataMember]
        [DataDesc(CName = "血型ABO信息", ShortCode = "BloodABO", Desc = "血型ABO信息")]
        public virtual BloodABO BloodABO
        {
            get { return _bloodABO; }
            set { _bloodABO = value; }
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
