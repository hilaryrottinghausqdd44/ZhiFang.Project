using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region MEGroupSampleGraph

    /// <summary>
    /// MEGroupSampleGraph object for NHibernate mapped table 'ME_GroupSampleGraph'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "存储小组样本图形结果", ClassCName = "MEGroupSampleGraph", ShortCode = "MEGroupSampleGraph", Desc = "存储小组样本图形结果")]
    public class MEGroupSampleGraph : BaseEntity
    {
        #region Member Variables

        protected long? _groupSampleFormID;
        protected long? _EquipSampleFormID;
        protected long? _equipID;
        protected int _graphNo;
        protected string _graphName;
        protected string _graphData;
        protected string _graphType;
        protected string _graphComment;
        protected bool _deleteFlag;
        protected int _testFlag;
        protected int _checkFlag;
        protected int _reportFlag=1;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected MEEquipSampleForm _mEEquipSampleForm;

        #endregion

        #region Constructors

        public MEGroupSampleGraph() { }

        public MEGroupSampleGraph(long labID, long groupSampleFormID, long equipID, int graphNo, string graphName, string graphData, string graphType, string graphComment, bool deleteFlag, int testFlag, int checkFlag, int reportFlag, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, MEEquipSampleForm mEEquipSampleForm)
        {
            this._labID = labID;
            this._groupSampleFormID = groupSampleFormID;
            this._equipID = equipID;
            this._graphNo = graphNo;
            this._graphName = graphName;
            this._graphData = graphData;
            this._graphType = graphType;
            this._graphComment = graphComment;
            this._deleteFlag = deleteFlag;
            this._testFlag = testFlag;
            this._checkFlag = checkFlag;
            this._reportFlag = reportFlag;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._mEEquipSampleForm = mEEquipSampleForm;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "小组样本单ID", ShortCode = "GroupSampleFormID", Desc = "小组样本单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? GroupSampleFormID
        {
            get { return _groupSampleFormID; }
            set { _groupSampleFormID = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "仪器样本单ID", ShortCode = "EquipSampleFormID", Desc = "仪器样本单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? EquipSampleFormID
        {
            get { return _EquipSampleFormID; }
            set { _EquipSampleFormID = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "仪器ID", ShortCode = "EquipID", Desc = "仪器ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? EquipID
        {
            get { return _equipID; }
            set { _equipID = value; }
        }

        [DataMember]
        [DataDesc(CName = "图形编号", ShortCode = "GraphNo", Desc = "图形编号", ContextType = SysDic.All, Length = 4)]
        public virtual int GraphNo
        {
            get { return _graphNo; }
            set { _graphNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "图形名称", ShortCode = "GraphName", Desc = "图形名称", ContextType = SysDic.All, Length = 50)]
        public virtual string GraphName
        {
            get { return _graphName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for GraphName", value, value.ToString());
                _graphName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "图形数据", ShortCode = "GraphData", Desc = "图形数据", ContextType = SysDic.All, Length = 80000)]
        public virtual string GraphData
        {
            get { return _graphData; }
            set
            {
                _graphData = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "图形类型", ShortCode = "GraphType", Desc = "图形类型", ContextType = SysDic.All, Length = 10)]
        public virtual string GraphType
        {
            get { return _graphType; }
            set
            {
                _graphType = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "图形说明", ShortCode = "GraphComment", Desc = "图形说明", ContextType = SysDic.All, Length = 8000)]
        public virtual string GraphComment
        {
            get { return _graphComment; }
            set
            {
                _graphComment = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "删除标志", ShortCode = "DeleteFlag", Desc = "删除标志", ContextType = SysDic.All, Length = 1)]
        public virtual bool DeleteFlag
        {
            get { return _deleteFlag; }
            set { _deleteFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "检验确认状态", ShortCode = "TestFlag", Desc = "检验确认状态", ContextType = SysDic.All, Length = 4)]
        public virtual int TestFlag
        {
            get { return _testFlag; }
            set { _testFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核状态", ShortCode = "CheckFlag", Desc = "审核状态", ContextType = SysDic.All, Length = 4)]
        public virtual int CheckFlag
        {
            get { return _checkFlag; }
            set { _checkFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否报告状态", ShortCode = "ReportFlag", Desc = "是否报告状态", ContextType = SysDic.All, Length = 4)]
        public virtual int ReportFlag
        {
            get { return _reportFlag; }
            set { _reportFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "图形打印次序", ShortCode = "DispOrder", Desc = "图形打印次序", ContextType = SysDic.All, Length = 4)]
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


        #endregion
    }
    #endregion
}