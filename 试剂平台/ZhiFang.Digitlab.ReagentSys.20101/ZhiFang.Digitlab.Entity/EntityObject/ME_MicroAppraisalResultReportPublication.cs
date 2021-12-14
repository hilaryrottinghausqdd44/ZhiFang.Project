using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region MEMicroAppraisalResultReportPublication

    /// <summary>
    /// MEMicroAppraisalResultReportPublication object for NHibernate mapped table 'ME_MicroAppraisalResultReportPublication'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "微生物报告鉴定结果发布记录表", ClassCName = "MEMicroAppraisalResultReportPublication", ShortCode = "MEMicroAppraisalResultReportPublication", Desc = "微生物报告鉴定结果发布记录表")]
    public class MEMicroAppraisalResultReportPublication : BaseEntity
    {
        #region Member Variables

        protected string _dataAddUser;
        protected long? _dataAddUserID;
        protected string _dataAddComputer;
        protected MEGroupSampleReportPublication _mEGroupSampleReportPublication;
        protected MEMicroAppraisalValue _mEMicroAppraisalValue;

        #endregion

        #region Constructors

        public MEMicroAppraisalResultReportPublication() { }

        public MEMicroAppraisalResultReportPublication(long labID, DateTime dataAddTime, string dataAddUser, long dataAddUserID, string dataAddComputer, byte[] dataTimeStamp, MEGroupSampleReportPublication mEGroupSampleReportPublication, MEMicroAppraisalValue mEMicroAppraisalValue)
        {
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataAddUser = dataAddUser;
            this._dataAddUserID = dataAddUserID;
            this._dataAddComputer = dataAddComputer;
            this._dataTimeStamp = dataTimeStamp;
            this._mEGroupSampleReportPublication = mEGroupSampleReportPublication;
            this._mEMicroAppraisalValue = mEMicroAppraisalValue;
        }

        #endregion

        #region Public Properties


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
        [DataDesc(CName = "小组样本单报告发布记录表", ShortCode = "MEGroupSampleReportPublication", Desc = "小组样本单报告发布记录表")]
        public virtual MEGroupSampleReportPublication MEGroupSampleReportPublication
        {
            get { return _mEGroupSampleReportPublication; }
            set { _mEGroupSampleReportPublication = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物鉴定结果", ShortCode = "MEMicroAppraisalValue", Desc = "微生物鉴定结果")]
        public virtual MEMicroAppraisalValue MEMicroAppraisalValue
        {
            get { return _mEMicroAppraisalValue; }
            set { _mEMicroAppraisalValue = value; }
        }


        #endregion
    }
    #endregion
}