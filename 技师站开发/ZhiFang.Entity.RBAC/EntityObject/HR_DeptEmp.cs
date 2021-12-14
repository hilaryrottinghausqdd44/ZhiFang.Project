using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
    #region HRDeptEmp

    /// <summary>
    /// HRDeptEmp object for NHibernate mapped table 'HR_DeptEmp'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "部门员工关系", ClassCName = "", ShortCode = "BMYGGX", Desc = "部门员工关系")]
    public class HRDeptEmp : BaseEntity
    {
        #region Member Variables


        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected HRDept _hRDept;
        protected HREmployee _hREmployee;

        #endregion

        #region Constructors

        public HRDeptEmp() { }

        public HRDeptEmp(long labID, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, HRDept hRDept, HREmployee hREmployee)
        {
            this._labID = labID;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._hRDept = hRDept;
            this._hREmployee = hREmployee;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "SFSY", Desc = "是否使用", ContextType = SysDic.All)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "XSCX", Desc = "显示次序", ContextType = SysDic.Number, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "SJGXSJ", Desc = "数据更新时间", ContextType = SysDic.DateTime)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }


        [DataMember]
        [DataDesc(CName = "部门", ShortCode = "BM", Desc = "部门")]
        public virtual HRDept HRDept
        {
            get { return _hRDept; }
            set { _hRDept = value; }
        }

        [DataMember]
        [DataDesc(CName = "员工", ShortCode = "YG", Desc = "员工")]
        public virtual HREmployee HREmployee
        {
            get { return _hREmployee; }
            set { _hREmployee = value; }
        }

        #endregion
    }
    #endregion
}