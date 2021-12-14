using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region PMEGroupSampleFormSampleQuantityStatistics小组样本单样本量统计
    /// <summary>
    /// PMEGroupSampleFormSampleQuantityStatistics
    /// </summary>
    [DataContract]
    [DataDesc(CName = "小组样本单样本量统计", ClassCName = "PMEGroupSampleFormSampleQuantityStatistics", ShortCode = "PMEGroupSampleFormSampleQuantityStatistics", Desc = "小组样本单样本量统计")]
    public class PMEGroupSampleFormSampleQuantityStatistics
    {
        #region Member Variables
        protected long _id;
        protected long? _GroupID;
        protected long? _SampleFormID;
        protected long? _SampleStatusID;
        protected long? _GSampleTypeID;
        protected string _SampleTypeCName;
        private string _GBarCode;
        private int _MainState;
        private int _PositiveFlag;
        private string _GSampleInfo;
        private string _TestComment;
        private string _FormMemo;
        private string _ESampleNo;
        private string _GSampleNo;
        private long? _OrderFormID;
        private long? _DeptID;
        private long? _SickTypeID;
        private string _DeptCName;
        private string _SickTypeCName;
        private string _GroupCName;
        private string _DataAddDate;
        private long _CountNumber;

        #endregion

        #region Constructors

        public PMEGroupSampleFormSampleQuantityStatistics() { }


        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "主键ID", ShortCode = "Id", Desc = "主键ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long Id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "检验小组ID", ShortCode = "GroupID", Desc = "检验小组ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long? GroupID
        {
            get { return _GroupID; }
            set { _GroupID = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本单ID", ShortCode = "SampleFormID", Desc = "样本单ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long? SampleFormID
        {
            get { return _SampleFormID; }
            set { _SampleFormID = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本状态记录ID", ShortCode = "SampleStatusID", Desc = "样本状态记录ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long? SampleStatusID
        {
            get { return _SampleStatusID; }
            set { _SampleStatusID = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本类型ID", ShortCode = "GSampleTypeID", Desc = "样本类型ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long? GSampleTypeID
        {
            get { return _GSampleTypeID; }
            set { _GSampleTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本类型名称", ShortCode = "SampleTypeCName", Desc = "样本类型名称", ContextType = SysDic.Number, Length = 8)]
        public virtual string SampleTypeCName
        {
            get { return _SampleTypeCName; }
            set { _SampleTypeCName = value; }
        }

        [DataMember]
        [DataDesc(CName = "条码号", ShortCode = "GBarCode", Desc = "条码号", ContextType = SysDic.Number, Length = 8)]
        public virtual string GBarCode
        {
            get { return _GBarCode; }
            set { _GBarCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组样本单主状态", ShortCode = "MainState", Desc = "小组样本单主状态", ContextType = SysDic.Number, Length = 8)]
        public virtual int MainState
        {
            get { return _MainState; }
            set { _MainState = value; }
        }

        [DataMember]
        [DataDesc(CName = "阳性标识", ShortCode = "PositiveFlag", Desc = "阳性标识", ContextType = SysDic.Number, Length = 8)]
        public virtual int PositiveFlag
        {
            get { return _PositiveFlag; }
            set { _PositiveFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组样本描述", ShortCode = "GSampleInfo", Desc = "小组样本描述", ContextType = SysDic.Text, Length = 500)]
        public virtual string GSampleInfo
        {
            get { return _GSampleInfo; }
            set { _GSampleInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "检验备注", ShortCode = "TestComment", Desc = "检验备注", ContextType = SysDic.Text, Length = 500)]
        public virtual string TestComment
        {
            get { return _TestComment; }
            set { _TestComment = value; }
        }

        [DataMember]
        [DataDesc(CName = "检验样本备注", ShortCode = "FormMemo", Desc = "检验样本备注", ContextType = SysDic.Text, Length = 500)]
        public virtual string FormMemo
        {
            get { return _FormMemo; }
            set { _FormMemo = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器样本号", ShortCode = "ESampleNo", Desc = "仪器样本号", ContextType = SysDic.Text, Length = 500)]
        public virtual string ESampleNo
        {
            get { return _ESampleNo; }
            set { _ESampleNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组样本号", ShortCode = "GSampleNo", Desc = "小组样本号", ContextType = SysDic.Text, Length = 500)]
        public virtual string GSampleNo
        {
            get { return _GSampleNo; }
            set { _GSampleNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "医嘱申请单ID", ShortCode = "OrderFormID", Desc = "医嘱申请单ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long? OrderFormID
        {
            get { return _OrderFormID; }
            set { _OrderFormID = value; }
        }

        [DataMember]
        [DataDesc(CName = "医嘱科室ID", ShortCode = "DeptID", Desc = "医嘱科室ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long? DeptID
        {
            get { return _DeptID; }
            set { _DeptID = value; }
        }

        [DataMember]
        [DataDesc(CName = "就诊类型ID", ShortCode = "SampleTypeCName", Desc = "就诊类型ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long? SickTypeID
        {
            get { return _SickTypeID; }
            set { _SickTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "医嘱科室名称", ShortCode = "DeptCName", Desc = "医嘱科室名称", ContextType = SysDic.Text, Length = 50)]
        public virtual string DeptCName
        {
            get { return _DeptCName; }
            set { _DeptCName = value; }
        }

        [DataMember]
        [DataDesc(CName = "就诊类型名称", ShortCode = "SickTypeCName", Desc = "就诊类型名称", ContextType = SysDic.Text, Length = 50)]
        public virtual string SickTypeCName
        {
            get { return _SickTypeCName; }
            set { _SickTypeCName = value; }
        }


        [DataMember]
        [DataDesc(CName = "检验小组名称", ShortCode = "GroupName", Desc = "检验小组名称", ContextType = SysDic.Text, Length = 50)]
        public virtual string GroupCName
        {
            get { return _GroupCName; }
            set { _GroupCName = value; }
        }

        [DataMember]
        [DataDesc(CName = "数据新增时间", ShortCode = "DataAddTime", Desc = "数据新增时间", ContextType = SysDic.Text, Length = 50)]
        public virtual string DataAddDate
        {
            get { return _DataAddDate; }
            set { _DataAddDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "合计", ShortCode = "CountNumber", Desc = "合计", ContextType = SysDic.Text, Length = 50)]
        public virtual long CountNumber
        {
            get { return _CountNumber; }
            set { _CountNumber = value; }
        }
        #endregion
    }
    #endregion
}
