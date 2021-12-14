using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using System;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region ETempletEmp

    /// <summary>
    /// ETempletEmp object for NHibernate mapped table 'E_TempletEmp'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "模板与员工关系表", ClassCName = "ETempletEmp", ShortCode = "ETempletEmp", Desc = "模板与员工关系表")]
    public class ETempletEmp : BaseEntity
    {
        #region Member Variables

        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected ETemplet _eTemplet;
        protected HREmployee _hREmployee;

        #endregion

        #region Constructors

        public ETempletEmp() { }

        public ETempletEmp(long labID, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ETemplet eTemplet, HREmployee hREmployee)
        {
            this._labID = labID;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._eTemplet = eTemplet;
            this._hREmployee = hREmployee;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
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
        [DataDesc(CName = "仪器模板表", ShortCode = "ETemplet", Desc = "仪器模板表")]
        public virtual ETemplet ETemplet
        {
            get { return _eTemplet; }
            set { _eTemplet = value; }
        }

        [DataMember]
        [DataDesc(CName = "员工", ShortCode = "HREmployee", Desc = "员工")]
        public virtual HREmployee HREmployee
        {
            get { return _hREmployee; }
            set { _hREmployee = value; }
        }


        #endregion
    }
    #endregion
}