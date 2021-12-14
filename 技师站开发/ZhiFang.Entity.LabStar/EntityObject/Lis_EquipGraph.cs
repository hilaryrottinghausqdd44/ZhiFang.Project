using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LisEquipGraph

    /// <summary>
    /// LisEquipGraph object for NHibernate mapped table 'Lis_EquipGraph'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "Lis_EquipGraph仪器图形", ClassCName = "LisEquipGraph", ShortCode = "LisEquipGraph", Desc = "Lis_EquipGraph仪器图形")]
    public class LisEquipGraph : BaseEntity
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
        protected bool _isReport;
        protected int _graphHeight;
        protected int _graphWidth;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected LisEquipForm _lisEquipForm;


        #endregion

        #region Constructors

        public LisEquipGraph() { }

        public LisEquipGraph(long labID, DateTime gTestDate, int iExamine, int graphNo, string graphName, string graphType, long graphDataID, string graphInfo, string graphComment, bool isReport, int graphHeight, int graphWidth, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LisEquipForm lisEquipForm)
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
            this._isReport = isReport;
            this._graphHeight = graphHeight;
            this._graphWidth = graphWidth;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lisEquipForm = lisEquipForm;
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
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器样本单", ShortCode = "LisEquipForm", Desc = "仪器样本单")]
        public virtual LisEquipForm LisEquipForm
        {
            get { return _lisEquipForm; }
            set { _lisEquipForm = value; }
        }


        #endregion
    }
    #endregion
}