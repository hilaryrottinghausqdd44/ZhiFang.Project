using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LisTestGraph

    /// <summary>
    /// LisTestGraph object for NHibernate mapped table 'Lis_TestGraph'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "检验图形", ClassCName = "LisTestGraph", ShortCode = "LisTestGraph", Desc = "检验图形")]
    public class LisTestGraph : BaseEntity
    {
        #region Member Variables

        protected DateTime? _gTestDate;
        protected int _iExamine;
        protected int _graphNo;
        protected string _graphName;
        protected string _graphType;
        protected long? _graphDataID;
        protected string _graphInfo;
        protected string _graphComment;
        protected int _mainStatusID;
        protected long? _statusID;
        protected long? _reportStatusID;
        protected bool _isReport;
        protected int _graphHeight;
        protected int _graphWidth;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected LisTestForm _lisTestForm;


        #endregion

        #region Constructors

        public LisTestGraph() { }

        public LisTestGraph(long labID, DateTime gTestDate, int iExamine, int graphNo, string graphName, string graphType, long graphDataID, string graphInfo, string graphComment, int mainStatusID, long statusID, long reportStatusID, bool isReport, int graphHeight, int graphWidth, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LisTestForm lisTestForm)
        {
            this._labID = labID;
            this._gTestDate = gTestDate;
            this._iExamine = iExamine;
            this._graphNo = graphNo;
            this._graphName = graphName;
            this._graphType = graphType;
            this._graphDataID = graphDataID;
            this._graphInfo = graphInfo;
            this._graphComment = graphComment;
            this._mainStatusID = mainStatusID;
            this._statusID = statusID;
            this._reportStatusID = reportStatusID;
            this._isReport = isReport;
            this._graphHeight = graphHeight;
            this._graphWidth = graphWidth;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lisTestForm = lisTestForm;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检验日期", ShortCode = "GTestDate", Desc = "检验日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? GTestDate
        {
            get { return _gTestDate; }
            set { _gTestDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "检查次数", ShortCode = "IExamine", Desc = "检查次数", ContextType = SysDic.All, Length = 4)]
        public virtual int IExamine
        {
            get { return _iExamine; }
            set { _iExamine = value; }
        }

        [DataMember]
        [DataDesc(CName = "图形编号", ShortCode = "GraphNo", Desc = "图形编号", ContextType = SysDic.All, Length = 4)]
        public virtual int GraphNo
        {
            get { return _graphNo; }
            set { _graphNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "图形名称", ShortCode = "GraphName", Desc = "图形名称", ContextType = SysDic.All, Length = 100)]
        public virtual string GraphName
        {
            get { return _graphName; }
            set { _graphName = value; }
        }

        [DataMember]
        [DataDesc(CName = "图形类型", ShortCode = "GraphType", Desc = "图形类型", ContextType = SysDic.All, Length = 20)]
        public virtual string GraphType
        {
            get { return _graphType; }
            set { _graphType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "图形图数据ID", ShortCode = "GraphDataID", Desc = "图形图数据ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? GraphDataID
        {
            get { return _graphDataID; }
            set { _graphDataID = value; }
        }

        [DataMember]
        [DataDesc(CName = "图形数据说明", ShortCode = "GraphInfo", Desc = "图形数据说明", ContextType = SysDic.All, Length = 16)]
        public virtual string GraphInfo
        {
            get { return _graphInfo; }
            set { _graphInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "图形备注", ShortCode = "GraphComment", Desc = "图形备注", ContextType = SysDic.All, Length = 16)]
        public virtual string GraphComment
        {
            get { return _graphComment; }
            set { _graphComment = value; }
        }

        [DataMember]
        [DataDesc(CName = "主状态", ShortCode = "MainStatusID", Desc = "主状态", ContextType = SysDic.All, Length = 4)]
        public virtual int MainStatusID
        {
            get { return _mainStatusID; }
            set { _mainStatusID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "过程状态标志", ShortCode = "StatusID", Desc = "过程状态标志", ContextType = SysDic.All, Length = 8)]
        public virtual long? StatusID
        {
            get { return _statusID; }
            set { _statusID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "结果与报告状态标志", ShortCode = "ReportStatusID", Desc = "结果与报告状态标志", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReportStatusID
        {
            get { return _reportStatusID; }
            set { _reportStatusID = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否报告", ShortCode = "IsReport", Desc = "是否报告", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsReport
        {
            get { return _isReport; }
            set { _isReport = value; }
        }

        [DataMember]
        [DataDesc(CName = "图形高度", ShortCode = "GraphHeight", Desc = "图形高度", ContextType = SysDic.All, Length = 4)]
        public virtual int GraphHeight
        {
            get { return _graphHeight; }
            set { _graphHeight = value; }
        }

        [DataMember]
        [DataDesc(CName = "图形宽度", ShortCode = "GraphWidth", Desc = "图形宽度", ContextType = SysDic.All, Length = 4)]
        public virtual int GraphWidth
        {
            get { return _graphWidth; }
            set { _graphWidth = value; }
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
        [DataDesc(CName = "DataUpdateTime", ShortCode = "DataUpdateTime", Desc = "DataUpdateTime", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "检验单", ShortCode = "LisTestForm", Desc = "检验单")]
        public virtual LisTestForm LisTestForm
        {
            get { return _lisTestForm; }
            set { _lisTestForm = value; }
        }


        #endregion
    }
    #endregion
}