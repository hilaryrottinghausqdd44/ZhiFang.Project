using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region MEGroupSampleReportPublication

    /// <summary>
    /// MEGroupSampleReportPublication object for NHibernate mapped table 'ME_GroupSampleReportPublication'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "小组样本单报告发布记录表", ClassCName = "MEGroupSampleReportPublication", ShortCode = "MEGroupSampleReportPublication", Desc = "小组样本单报告发布记录表")]
    public class MEGroupSampleReportPublication : BaseEntity
    {
        #region Member Variables

        protected int _reportFlag;//0删除无效，1有效，2即时发布。
        protected int _reportType;
        protected DateTime? _receivedate;
        protected int _sectionNo;
        protected int _testTypeNo;
        protected string _sampleNo;
        protected string _dataAddUser;
        protected long? _dataAddUserID;
        protected string _dataAddComputer;
        protected DateTime? _dataRevokeTime;
        protected string _dataRevokeUser;
        protected long? _dataRevokeUserID;
        protected string _dataRevokeComputer;
        protected MEGroupSampleForm _mEGroupSampleForm;
        protected IList<MEMicroDSTResultReportPublication> _mEMicroDSTResultReportPublicationList;
        protected IList<MEMicroAppraisalResultReportPublication> _mEMicroAppraisalResultReportPublicationList;

        #endregion

        #region Constructors

        public MEGroupSampleReportPublication() { }

        public MEGroupSampleReportPublication(long labID, int reportFlag, int reportType, DateTime receivedate, int sectionNo, int testTypeNo, string sampleNo, DateTime dataAddTime, string dataAddUser, long dataAddUserID, string dataAddComputer, DateTime dataRevokeTime, string dataRevokeUser, long dataRevokeUserID, string dataRevokeComputer, byte[] dataTimeStamp, MEGroupSampleForm mEGroupSampleForm)
        {
            this._labID = labID;
            this._reportFlag = reportFlag;
            this._reportType = reportType;
            this._receivedate = receivedate;
            this._sectionNo = sectionNo;
            this._testTypeNo = testTypeNo;
            this._sampleNo = sampleNo;
            this._dataAddTime = dataAddTime;
            this._dataAddUser = dataAddUser;
            this._dataAddUserID = dataAddUserID;
            this._dataAddComputer = dataAddComputer;
            this._dataRevokeTime = dataRevokeTime;
            this._dataRevokeUser = dataRevokeUser;
            this._dataRevokeUserID = dataRevokeUserID;
            this._dataRevokeComputer = dataRevokeComputer;
            this._dataTimeStamp = dataTimeStamp;
            this._mEGroupSampleForm = mEGroupSampleForm;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "报告状态：1-有效；0-无效", ShortCode = "ReportFlag", Desc = "报告状态：1-有效；0-无效", ContextType = SysDic.All, Length = 4)]
        public virtual int ReportFlag
        {
            get { return _reportFlag; }
            set { _reportFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "报告类型：0-中间报告；1-最终报告", ShortCode = "ReportType", Desc = "报告类型：0-中间报告；1-最终报告", ContextType = SysDic.All, Length = 4)]
        public virtual int ReportType
        {
            get { return _reportType; }
            set { _reportType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "核收日期", ShortCode = "Receivedate", Desc = "核收日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? Receivedate
        {
            get { return _receivedate; }
            set { _receivedate = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组编号", ShortCode = "SectionNo", Desc = "小组编号", ContextType = SysDic.All, Length = 4)]
        public virtual int SectionNo
        {
            get { return _sectionNo; }
            set { _sectionNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "检验类型", ShortCode = "TestTypeNo", Desc = "检验类型", ContextType = SysDic.All, Length = 4)]
        public virtual int TestTypeNo
        {
            get { return _testTypeNo; }
            set { _testTypeNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本号", ShortCode = "SampleNo", Desc = "样本号", ContextType = SysDic.All, Length = 40)]
        public virtual string SampleNo
        {
            get { return _sampleNo; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for SampleNo", value, value.ToString());
                _sampleNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "发布人", ShortCode = "DataAddUser", Desc = "发布人", ContextType = SysDic.All, Length = 50)]
        public virtual string DataAddUser
        {
            get { return _dataAddUser; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for DataAddUser", value, value.ToString());
                _dataAddUser = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发布人ID", ShortCode = "DataAddUserID", Desc = "发布人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DataAddUserID
        {
            get { return _dataAddUserID; }
            set { _dataAddUserID = value; }
        }

        [DataMember]
        [DataDesc(CName = "发布计算机名", ShortCode = "DataAddComputer", Desc = "发布计算机名", ContextType = SysDic.All, Length = 40)]
        public virtual string DataAddComputer
        {
            get { return _dataAddComputer; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for DataAddComputer", value, value.ToString());
                _dataAddComputer = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "撤销时间", ShortCode = "DataRevokeTime", Desc = "撤销时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataRevokeTime
        {
            get { return _dataRevokeTime; }
            set { _dataRevokeTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "撤销人", ShortCode = "DataRevokeUser", Desc = "撤销人", ContextType = SysDic.All, Length = 50)]
        public virtual string DataRevokeUser
        {
            get { return _dataRevokeUser; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for DataRevokeUser", value, value.ToString());
                _dataRevokeUser = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "撤销人ID", ShortCode = "DataRevokeUserID", Desc = "撤销人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DataRevokeUserID
        {
            get { return _dataRevokeUserID; }
            set { _dataRevokeUserID = value; }
        }

        [DataMember]
        [DataDesc(CName = "撤销计算机名", ShortCode = "DataRevokeComputer", Desc = "撤销计算机名", ContextType = SysDic.All, Length = 40)]
        public virtual string DataRevokeComputer
        {
            get { return _dataRevokeComputer; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for DataRevokeComputer", value, value.ToString());
                _dataRevokeComputer = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "小组样本单", ShortCode = "MEGroupSampleForm", Desc = "小组样本单")]
        public virtual MEGroupSampleForm MEGroupSampleForm
        {
            get { return _mEGroupSampleForm; }
            set { _mEGroupSampleForm = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物报告药敏结果发布记录表", ShortCode = "MEMicroDSTResultReportPublicationList", Desc = "微生物报告药敏结果发布记录表")]
        public virtual IList<MEMicroDSTResultReportPublication> MEMicroDSTResultReportPublicationList
        {
            get
            {
                if (_mEMicroDSTResultReportPublicationList == null)
                {
                    _mEMicroDSTResultReportPublicationList = new List<MEMicroDSTResultReportPublication>();
                }
                return _mEMicroDSTResultReportPublicationList;
            }
            set { _mEMicroDSTResultReportPublicationList = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物报告鉴定结果发布记录表", ShortCode = "MEMicroAppraisalResultReportPublicationList", Desc = "微生物报告鉴定结果发布记录表")]
        public virtual IList<MEMicroAppraisalResultReportPublication> MEMicroAppraisalResultReportPublicationList
        {
            get
            {
                if (_mEMicroAppraisalResultReportPublicationList == null)
                {
                    _mEMicroAppraisalResultReportPublicationList = new List<MEMicroAppraisalResultReportPublication>();
                }
                return _mEMicroAppraisalResultReportPublicationList;
            }
            set { _mEMicroAppraisalResultReportPublicationList = value; }
        }


        #endregion
    }
    #endregion
}