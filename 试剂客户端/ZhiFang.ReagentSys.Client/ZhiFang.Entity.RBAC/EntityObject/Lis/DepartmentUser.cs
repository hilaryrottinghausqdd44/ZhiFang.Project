using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
    #region DepartmentUser

    /// <summary>
    /// DepartmentUser object for NHibernate mapped table 'DepartmentUser'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "DepartmentUser", ShortCode = "DepartmentUser", Desc = "")]
    public class DepartmentUser : BaseEntity
    {
        #region Member Variables

        // protected int _deptNo;
        //protected int _userNo;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected Department _department;
        protected PUser _pUser;

        #endregion

        #region Constructors

        public DepartmentUser() { }

        public DepartmentUser(long labID, Department department, PUser pUser, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._department = department;
            this._pUser = pUser;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "部门", ShortCode = "Department", Desc = "部门")]
        public virtual Department Department
        {
            get { return _department; }
            set { _department = value; }
        }
        [DataMember]
        [DataDesc(CName = "人员信息", ShortCode = "PUser", Desc = "人员信息")]
        public virtual PUser PUser
        {
            get { return _pUser; }
            set { _pUser = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }


        #endregion
    }
    #endregion
}