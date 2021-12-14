using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region MEMicroCultureMessage

    /// <summary>
    /// MEMicroCultureMessage object for NHibernate mapped table 'ME_MicroCultureMessage'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "微生物培养随笔记录", ClassCName = "MEMicroCultureMessage", ShortCode = "MEMicroCultureMessage", Desc = "微生物培养随笔记录")]
    public class MEMicroCultureMessage : BaseEntity
    {
        #region Member Variables

        protected string _messageInfo;
        protected string _empName;
        protected DateTime? _dataUpdateTime;
        protected HREmployee _hREmployee;
        protected MEMicroInoculant _mEMicroInoculant;

        #endregion

        #region Constructors

        public MEMicroCultureMessage() { }

        public MEMicroCultureMessage(long labID, string messageInfo, string empName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, HREmployee hREmployee, MEMicroInoculant mEMicroInoculant)
        {
            this._labID = labID;
            this._messageInfo = messageInfo;
            this._empName = empName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._hREmployee = hREmployee;
            this._mEMicroInoculant = mEMicroInoculant;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "Message记录", ShortCode = "MessageInfo", Desc = "Message记录", ContextType = SysDic.All, Length = 500)]
        public virtual string MessageInfo
        {
            get { return _messageInfo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for MessageInfo", value, value.ToString());
                _messageInfo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "记录人姓名", ShortCode = "EmpName", Desc = "记录人姓名", ContextType = SysDic.All, Length = 60)]
        public virtual string EmpName
        {
            get { return _empName; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for EmpName", value, value.ToString());
                _empName = value;
            }
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
        [DataDesc(CName = "员工", ShortCode = "HREmployee", Desc = "员工")]
        public virtual HREmployee HREmployee
        {
            get { return _hREmployee; }
            set { _hREmployee = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物接种记录", ShortCode = "MEMicroInoculant", Desc = "微生物接种记录")]
        public virtual MEMicroInoculant MEMicroInoculant
        {
            get { return _mEMicroInoculant; }
            set { _mEMicroInoculant = value; }
        }


        #endregion
    }
    #endregion
}