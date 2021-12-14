using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using System;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region EReportData

    /// <summary>
    /// EReportData object for NHibernate mapped table 'E_ReportData'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "仪器报表数据表", ClassCName = "EReportData", ShortCode = "EReportData", Desc = "仪器报表数据表")]
    public class EReportData : BaseEntity
    {
        #region Member Variables

        protected string _reportName;
        protected DateTime? _reportDate;
        protected string _reportFilePath;
        protected string _reportFileExt;
        protected int _isCheck;
        protected string _checker;
        protected DateTime? _checkTime;
        protected string _checkView;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected ETemplet _eTemplet;
        protected EEquip _eEquip;
        protected HREmployee _hREmployee;

        #endregion

        #region Constructors

        public EReportData() { }

        public EReportData(long labID, DateTime reportDate, string reportFilePath, string reportFileExt, string checker, DateTime checkTime, string checkView, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ETemplet eTemplet)
        {
            this._labID = labID;
            this._reportDate = reportDate;
            this._reportFilePath = reportFilePath;
            this._reportFileExt = reportFileExt;
            this._checker = checker;
            this._checkTime = checkTime;
            this._checkView = checkView;
            this._comment = comment;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._eTemplet = eTemplet;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "报表名称", ShortCode = "ReportName", Desc = "报表名称", ContextType = SysDic.All, Length = 4000)]
        public virtual string ReportName
        {
            get { return _reportName; }
            set { _reportName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "报表日期", ShortCode = "ReportDate", Desc = "报表日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReportDate
        {
            get { return _reportDate; }
            set { _reportDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "报表文件路径", ShortCode = "ReportFilePath", Desc = "报表文件路径", ContextType = SysDic.All, Length = 1000)]
        public virtual string ReportFilePath
        {
            get { return _reportFilePath; }
            set { _reportFilePath = value; }
        }

        [DataMember]
        [DataDesc(CName = "报表文件扩展名", ShortCode = "ReportFileExt", Desc = "报表文件扩展名", ContextType = SysDic.All, Length = 50)]
        public virtual string ReportFileExt
        {
            get { return _reportFileExt; }
            set { _reportFileExt = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核标志", ShortCode = "IsCheck", Desc = "审核标志", ContextType = SysDic.All, Length = 1)]
        public virtual int IsCheck
        {
            get { return _isCheck; }
            set { _isCheck = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核类型", ShortCode = "CheckType", Desc = "审核类型", ContextType = SysDic.All, Length = 4)]
        public virtual int CheckType { get; set; }

        [DataMember]
        [DataDesc(CName = "审核人", ShortCode = "Checker", Desc = "审核人", ContextType = SysDic.All, Length = 50)]
        public virtual string Checker
        {
            get { return _checker; }
            set { _checker = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核时间", ShortCode = "CheckTime", Desc = "审核时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CheckTime
        {
            get { return _checkTime; }
            set { _checkTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核意见", ShortCode = "CheckView", Desc = "审核意见", ContextType = SysDic.All, Length = 50)]
        public virtual string CheckView
        {
            get { return _checkView; }
            set { _checkView = value; }
        }

        [DataMember]
        [DataDesc(CName = "反审人ID", ShortCode = "CancelCheckerID", Desc = "反审人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long CancelCheckerID { get; set; }

        [DataMember]
        [DataDesc(CName = "反审人", ShortCode = "CancelChecker", Desc = "反审人", ContextType = SysDic.All, Length = 50)]
        public virtual string CancelChecker { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "反审时间", ShortCode = "CancelCheckTime", Desc = "反审时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CancelCheckTime { get; set; }

        [DataMember]
        [DataDesc(CName = "反审意见", ShortCode = "CancelCheckView", Desc = "反审意见", ContextType = SysDic.All, Length = 500)]
        public virtual string CancelCheckView { get; set; }

        [DataMember]
        [DataDesc(CName = "描述", ShortCode = "Comment", Desc = "描述", ContextType = SysDic.All, Length = 4000)]
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

        //此字段不对应数据中的具体字段
        [DataMember]
        [DataDesc(CName = "附件标志", ShortCode = "IsAttachment", Desc = "附件标志", ContextType = SysDic.All, Length = 1)]
        public virtual int IsAttachment { get; set; }

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
        [DataDesc(CName = "仪器表", ShortCode = "EEquip", Desc = "仪器表")]
        public virtual EEquip EEquip
        {
            get { return _eEquip; }
            set { _eEquip = value; }
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