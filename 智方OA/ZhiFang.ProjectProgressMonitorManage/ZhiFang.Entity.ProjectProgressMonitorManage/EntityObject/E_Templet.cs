using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using System;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region ETemplet

    /// <summary>
    /// ETemplet object for NHibernate mapped table 'E_Templet'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "仪器模板表", ClassCName = "ETemplet", ShortCode = "ETemplet", Desc = "仪器模板表")]
    public class ETemplet : BaseEntity
    {
        #region Member Variables

        protected string _useCode;
        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _templetPath;
        protected string _templetStruct;
        protected string _templetFillStruct;
        protected string _fillStruct;
        protected DateTime? _beginDate;
        protected DateTime? _endDate;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected EEquip _eEquip;
        protected PDict _pDict;
        protected HRDept _section;
        protected IList<EMaintenanceData> _eMaintenanceDataList;
        protected IList<EReportData> _eReportDataList;
        protected IList<ETempletEmp> _eTempletEmpList;

        #endregion

        #region Constructors

        public ETemplet() { }

        public ETemplet(long labID, string useCode, string cName, string eName, string sName, string shortcode, string pinYinZiTou, string templetPath, string templetStruct, string templetFillStruct, DateTime beginDate, DateTime endDate, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, EEquip eEquip)
        {
            this._labID = labID;
            this._useCode = useCode;
            this._cName = cName;
            this._eName = eName;
            this._sName = sName;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._templetPath = templetPath;
            this._templetStruct = templetStruct;
            this._templetFillStruct = templetFillStruct;
            this._beginDate = beginDate;
            this._endDate = endDate;
            this._comment = comment;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._eEquip = eEquip;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "代码", ShortCode = "UseCode", Desc = "代码", ContextType = SysDic.All, Length = 50)]
        public virtual string UseCode
        {
            get { return _useCode; }
            set { _useCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "模板名称", ShortCode = "CName", Desc = "模板名称", ContextType = SysDic.All, Length = 200)]
        public virtual string CName
        {
            get { return _cName; }
            set { _cName = value; }
        }

        [DataMember]
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 200)]
        public virtual string EName
        {
            get { return _eName; }
            set { _eName = value; }
        }

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 100)]
        public virtual string SName
        {
            get { return _sName; }
            set { _sName = value; }
        }

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 50)]
        public virtual string Shortcode
        {
            get { return _shortcode; }
            set { _shortcode = value; }
        }

        [DataMember]
        [DataDesc(CName = "汉字拼音字头", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
        {
            get { return _pinYinZiTou; }
            set { _pinYinZiTou = value; }
        }

        [DataMember]
        [DataDesc(CName = "模板路径", ShortCode = "TempletPath", Desc = "模板路径", ContextType = SysDic.All, Length = 1000)]
        public virtual string TempletPath
        {
            get { return _templetPath; }
            set { _templetPath = value; }
        }

        [DataMember]
        [DataDesc(CName = "模板内容结构", ShortCode = "TempletStruct", Desc = "模板内容结构", ContextType = SysDic.All, Length = 4000)]
        public virtual string TempletStruct
        {
            get { return _templetStruct; }
            set { _templetStruct = value; }
        }

        [DataMember]
        [DataDesc(CName = "模板填充结构", ShortCode = "TempletFillStruct", Desc = "模板填充结构", ContextType = SysDic.All, Length = 4000)]
        public virtual string TempletFillStruct
        {
            get { return _templetFillStruct; }
            set { _templetFillStruct = value; }
        }

        [DataMember]
        [DataDesc(CName = "模板原始填充结构", ShortCode = "FillStruct", Desc = "模板原始填充结构", ContextType = SysDic.All, Length = 4000)]
        public virtual string FillStruct
        {
            get { return _fillStruct; }
            set { _fillStruct = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "开始时间", ShortCode = "BeginDate", Desc = "开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BeginDate
        {
            get { return _beginDate; }
            set { _beginDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "结束时间", ShortCode = "EndDate", Desc = "结束时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

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

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核类型", ShortCode = "CheckType", Desc = "审核类型", ContextType = SysDic.All, Length = 4)]
        public virtual int CheckType { get; set; }

        [DataMember]
        [DataDesc(CName = "填充类型", ShortCode = "FillType", Desc = "填充类型", ContextType = SysDic.All, Length = 4)]
        public virtual int FillType { get; set; }

        [DataMember]
        [DataDesc(CName = "要显示的填充项目", ShortCode = "ShowFillItem", Desc = "要显示的填充项目", ContextType = SysDic.All, Length = 500)]
        public virtual string ShowFillItem { get; set; }

        //数据库中无此对应字段
        [DataMember]
        [DataDesc(CName = "是否审核", ShortCode = "IsCheck", Desc = "是否审核", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCheck { get; set; }

        //数据库中无此对应字段
        [DataMember]
        [DataDesc(CName = "是否已填写数据", ShortCode = "IsFillData", Desc = "是否已填写数据", ContextType = SysDic.All, Length = 4)]
        public virtual int IsFillData { get; set; }

        //数据库中无此对应字段
        [DataMember]
        [DataDesc(CName = "审核意见", ShortCode = "CheckView", Desc = "审核意见", ContextType = SysDic.All, Length = 100)]
        public virtual string CheckView { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器表", ShortCode = "EEquip", Desc = "仪器表")]
        public virtual EEquip EEquip
        {
            get { return _eEquip; }
            set { _eEquip = value; }
        }

        [DataMember]
        [DataDesc(CName = "模板类型", ShortCode = "TempletType", Desc = "模板类型")]
        public virtual PDict TempletType
        {
            get { return _pDict; }
            set { _pDict = value; }
        }

        [DataMember]
        [DataDesc(CName = "检验小组", ShortCode = "Section", Desc = "检验小组")]
        public virtual HRDept Section
        {
            get { return _section; }
            set { _section = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器维护数据表", ShortCode = "EMaintenanceDataList", Desc = "仪器维护数据表")]
        public virtual IList<EMaintenanceData> EMaintenanceDataList
        {
            get
            {
                if (_eMaintenanceDataList == null)
                {
                    _eMaintenanceDataList = new List<EMaintenanceData>();
                }
                return _eMaintenanceDataList;
            }
            set { _eMaintenanceDataList = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器报表数据表", ShortCode = "EReportDataList", Desc = "仪器报表数据表")]
        public virtual IList<EReportData> EReportDataList
        {
            get
            {
                if (_eReportDataList == null)
                {
                    _eReportDataList = new List<EReportData>();
                }
                return _eReportDataList;
            }
            set { _eReportDataList = value; }
        }

        [DataMember]
        [DataDesc(CName = "模板与员工关系表", ShortCode = "ETempletEmpList", Desc = "模板与员工关系表")]
        public virtual IList<ETempletEmp> ETempletEmpList
        {
            get
            {
                if (_eTempletEmpList == null)
                {
                    _eTempletEmpList = new List<ETempletEmp>();
                }
                return _eTempletEmpList;
            }
            set { _eTempletEmpList = value; }
        }


        #endregion
    }
    #endregion
}