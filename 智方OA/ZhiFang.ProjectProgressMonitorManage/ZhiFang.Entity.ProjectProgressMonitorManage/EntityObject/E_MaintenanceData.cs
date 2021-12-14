using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using System;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region EMaintenanceData

    /// <summary>
    /// EMaintenanceData object for NHibernate mapped table 'E_MaintenanceData'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "仪器维护数据表", ClassCName = "EMaintenanceData", ShortCode = "EMaintenanceData", Desc = "仪器维护数据表")]
    public class EMaintenanceData : BaseEntity
    {
        #region Member Variables

        protected string _templetType;
        protected string _templetTypeCode;
        protected string _templetItem;
        protected string _templetItemCode;
        protected int _templetDataType;
        protected string _itemDataType;
        protected string _itemIsExcute;
        protected string _itemStatus;
        protected string _itemResult;
        protected string _itemMemo;
        protected string _batchNumber;
        protected string _templetBatNo;
        protected DateTime? _itemDate;
        protected string _operater;
        protected DateTime? _operateTime;
        protected DateTime? _dataUpdateTime;
        protected ETemplet _eTemplet;
        protected HREmployee _hREmployee;

        #endregion

        #region Constructors

        public EMaintenanceData() { }

        public EMaintenanceData(long labID, string templetType, string templetTypeCode, string templetItem, string templetItemCode, int templetDataType, string itemIsExcute, string itemStatus, string itemResult, string itemMemo, DateTime itemDate, string operater, DateTime operateTime, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ETemplet eTemplet)
        {
            this._labID = labID;
            this._templetType = templetType;
            this._templetTypeCode = templetTypeCode;
            this._templetItem = templetItem;
            this._templetItemCode = templetItemCode;
            this._templetDataType = templetDataType;
            this._itemIsExcute = itemIsExcute;
            this._itemStatus = itemStatus;
            this._itemResult = itemResult;
            this._itemMemo = itemMemo;
            this._itemDate = itemDate;
            this._operater = operater;
            this._operateTime = operateTime;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._eTemplet = eTemplet;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "模板类型名称", ShortCode = "TempletType", Desc = "模板类型名称", ContextType = SysDic.All, Length = 200)]
        public virtual string TempletType
        {
            get { return _templetType; }
            set { _templetType = value; }
        }

        [DataMember]
        [DataDesc(CName = "模板类型代码", ShortCode = "TempletTypeCode", Desc = "模板类型代码", ContextType = SysDic.All, Length = 100)]
        public virtual string TempletTypeCode
        {
            get { return _templetTypeCode; }
            set { _templetTypeCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "模板项目名称", ShortCode = "TempletItem", Desc = "模板项目名称", ContextType = SysDic.All, Length = 200)]
        public virtual string TempletItem
        {
            get { return _templetItem; }
            set { _templetItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "模板项目代码", ShortCode = "TempletItemCode", Desc = "模板项目代码", ContextType = SysDic.All, Length = 100)]
        public virtual string TempletItemCode
        {
            get { return _templetItemCode; }
            set { _templetItemCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "模板数据类型", ShortCode = "TempletDataType", Desc = "模板数据类型", ContextType = SysDic.All, Length = 4)]
        public virtual int TempletDataType
        {
            get { return _templetDataType; }
            set { _templetDataType = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目结果的数据类型", ShortCode = "ItemDataType", Desc = "项目结果的数据类型", ContextType = SysDic.All, Length = 10)]
        public virtual string ItemDataType
        {
            get { return _itemDataType; }
            set { _itemDataType = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目是否执行", ShortCode = "ItemIsExcute", Desc = "项目是否执行", ContextType = SysDic.All, Length = 50)]
        public virtual string ItemIsExcute
        {
            get { return _itemIsExcute; }
            set { _itemIsExcute = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目状态", ShortCode = "ItemStatus", Desc = "项目状态", ContextType = SysDic.All, Length = 50)]
        public virtual string ItemStatus
        {
            get { return _itemStatus; }
            set { _itemStatus = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目结果", ShortCode = "ItemResult", Desc = "项目结果", ContextType = SysDic.All, Length = 1000)]
        public virtual string ItemResult
        {
            get { return _itemResult; }
            set { _itemResult = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目备注", ShortCode = "ItemMemo", Desc = "项目备注", ContextType = SysDic.All, Length = 4000)]
        public virtual string ItemMemo
        {
            get { return _itemMemo; }
            set { _itemMemo = value; }
        }

        [DataMember]
        [DataDesc(CName = "存储批号", ShortCode = "BatchNumber", Desc = "存储批号", ContextType = SysDic.All, Length = 50)]
        public virtual string BatchNumber
        {
            get { return _batchNumber; }
            set { _batchNumber = value; }
        }

        [DataMember]
        [DataDesc(CName = "模板数据批次号", ShortCode = "TempletBatNo", Desc = "模板数据批次号", ContextType = SysDic.All, Length = 50)]
        public virtual string TempletBatNo
        {
            get { return _templetBatNo; }
            set { _templetBatNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目结果日期", ShortCode = "ItemDate", Desc = "项目结果日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ItemDate
        {
            get { return _itemDate; }
            set { _itemDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "操作者", ShortCode = "Operater", Desc = "操作者", ContextType = SysDic.All, Length = 50)]
        public virtual string Operater
        {
            get { return _operater; }
            set { _operater = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作时间", ShortCode = "OperateTime", Desc = "操作时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OperateTime
        {
            get { return _operateTime; }
            set { _operateTime = value; }
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