using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region PReportData

    [DataContract]
    [DataDesc(CName = "仪器报表数据表", ClassCName = "PReportData", ShortCode = "PReportData", Desc = "仪器报表数据表")]
    public class PReportData : BaseEntity
    {
        #region Member Variables

        protected long _templetID;
        protected long _equipID;
        protected long _reportDataID;
        protected string _reportName;
        protected string _equipName;
        protected string _equipTypeName;
        protected DateTime? _reportDate;
        protected string _reportFilePath;
        protected string _reportFileExt;
        protected int _isAttachment;
        protected int _isCheck;
        protected string _checker;
        protected DateTime? _checkTime;
        protected string _checkView;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "模板ID", ShortCode = "TempletID", Desc = "模板ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long TempletID
        {
            get { return _templetID; }
            set { _templetID = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器ID", ShortCode = "EquipID", Desc = "仪器ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long EquipID
        {
            get { return _equipID; }
            set { _equipID = value; }
        }


        [DataMember]
        [DataDesc(CName = "质量审核记录ID", ShortCode = "ReportDataID", Desc = "质量审核记录ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long ReportDataID
        {
            get { return _reportDataID; }
            set { _reportDataID = value; }
        }

        [DataMember]
        [DataDesc(CName = "模板代码", ShortCode = "TempletTypeName", Desc = "模板代码", ContextType = SysDic.All, Length = 200)]
        public virtual string TempletTypeName { get; set; }

        [DataMember]
        [DataDesc(CName = "模板类型", ShortCode = "TempletCode", Desc = "模板类型", ContextType = SysDic.All, Length = 200)]
        public virtual string TempletCode { get; set; }

        [DataMember]
        [DataDesc(CName = "报表名称", ShortCode = "ReportName", Desc = "报表名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ReportName
        {
            get { return _reportName; }
            set { _reportName = value; }
        }

        [DataMember]
        [DataDesc(CName = "报表简称", ShortCode = "ReportSName", Desc = "报表简称", ContextType = SysDic.All, Length = 200)]
        public virtual string ReportSName { get; set; }

        [DataMember]
        [DataDesc(CName = "仪器名称", ShortCode = "EquipName", Desc = "仪器名称", ContextType = SysDic.All, Length = 200)]
        public virtual string EquipName
        {
            get { return _equipName; }
            set { _equipName = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器类型名称", ShortCode = "EquipTypeName", Desc = "仪器类型名称", ContextType = SysDic.All, Length = 200)]
        public virtual string EquipTypeName
        {
            get { return _equipTypeName; }
            set { _equipTypeName = value; }
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
        [DataDesc(CName = "附件标志", ShortCode = "IsAttachment", Desc = "附件标志", ContextType = SysDic.All, Length = 1)]
        public virtual int IsAttachment
        {
            get { return _isAttachment; }
            set { _isAttachment = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核标志", ShortCode = "IsCheck", Desc = "审核标志", ContextType = SysDic.All, Length = 1)]
        public virtual int IsCheck
        {
            get { return _isCheck; }
            set { _isCheck = value; }
        }

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
        [DataDesc(CName = "描述", ShortCode = "Comment", Desc = "描述", ContextType = SysDic.All, Length = 4000)]
        public virtual string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组名称", ShortCode = "SectionName", Desc = "小组名称", ContextType = SysDic.All, Length = 100)]
        public virtual string SectionName { get; set; }

        [DataMember]
        [DataDesc(CName = "小组代码", ShortCode = "UseCode", Desc = "小组代码", ContextType = SysDic.All, Length = 100)]
        public virtual string SectionUseCode { get; set; }

        [DataMember]
        [DataDesc(CName = "小组快捷码", ShortCode = "Shortcode", Desc = "小组快捷码", ContextType = SysDic.All, Length = 100)]
        public virtual string SectionShortcode { get; set; }

        [DataMember]
        [DataDesc(CName = "小组标准代码", ShortCode = "StandCode", Desc = "小组标准代码", ContextType = SysDic.All, Length = 100)]
        public virtual string SectionStandCode { get; set; }

        [DataMember]
        [DataDesc(CName = "小组英文名称", ShortCode = "EName", Desc = "小组英文名称", ContextType = SysDic.All, Length = 100)]
        public virtual string SectionEName { get; set; }

        [DataMember]
        [DataDesc(CName = "仪器代码", ShortCode = "UseCode", Desc = "小组代码", ContextType = SysDic.All, Length = 100)]
        public virtual string EquipUseCode { get; set; }

        [DataMember]
        [DataDesc(CName = "仪器快捷码", ShortCode = "Shortcode", Desc = "小组快捷码", ContextType = SysDic.All, Length = 100)]
        public virtual string EquipShortcode { get; set; }

        [DataMember]
        [DataDesc(CName = "仪器英文名称", ShortCode = "EName", Desc = "小组英文名称", ContextType = SysDic.All, Length = 100)]
        public virtual string EquipEName { get; set; }

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
        [DataDesc(CName = "是否保存质量记录数据", ShortCode = "IsContainData", Desc = "是否保存质量记录数据", ContextType = SysDic.All, Length = 4)]
        public virtual string IsContainData { get; set; }

        [DataMember]
        [DataDesc(CName = "模板数据批次号", ShortCode = "TempletBatNo", Desc = "模板数据批次号", ContextType = SysDic.All, Length = 50)]
        public virtual string TempletBatNo { get; set; }

            [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        #endregion
    }

    #endregion
}