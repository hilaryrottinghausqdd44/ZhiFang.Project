using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region SLog
    /// <summary>
    /// SysPara object for NHibernate mapped table 'SysPara'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "系统日志", ClassCName = "SLog", ShortCode = "SLog", Desc = "系统日志")]
    public class SLog : BaseEntity
    {
        #region Member Variables

        protected string _operateName;
        protected string _operateType;
        protected string _iP;
        protected int _infoLevel;
        protected string _comment;
        protected long _empID = 0;
        protected string _empName;

        #endregion

        #region Constructors

        public SLog() { }

        public SLog(long labID, string operateName, string operateType, string iP, int infoLevel, string comment, DateTime dataAddTime, byte[] dataTimeStamp, long empID, string empName)
        {
            this._labID = labID;
            this._operateName = operateName;
            this._operateType = operateType;
            this._iP = iP;
            this._infoLevel = infoLevel;
            this._comment = comment;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._empID = empID;
            this._empName = empName;
        }

        #endregion

        #region Public Properties



        [DataMember]
        [DataDesc(CName = "操作名称", ShortCode = "OperateName", Desc = "操作名称", ContextType = SysDic.NText, Length = 512)]
        public virtual string OperateName
        {
            get { return _operateName; }
            set
            {
                if (value != null && value.Length > 512)
                    throw new ArgumentOutOfRangeException("Invalid value for OperateName", value, value.ToString());
                _operateName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "操作类型", ShortCode = "OperateType", Desc = "操作类型", ContextType = SysDic.NText, Length = 200)]
        public virtual string OperateType
        {
            get { return _operateType; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for OperateType", value, value.ToString());
                _operateType = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "操作IP", ShortCode = "IP", Desc = "操作IP", ContextType = SysDic.NText, Length = 50)]
        public virtual string IP
        {
            get { return _iP; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for IP", value, value.ToString());
                _iP = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "信息等级", ShortCode = "InfoLevel", Desc = "信息等级", ContextType = SysDic.Number, Length = 4)]
        public virtual int InfoLevel
        {
            get { return _infoLevel; }
            set { _infoLevel = value; }
        }

        [DataMember]
        [DataDesc(CName = "描述", ShortCode = "Comment", Desc = "描述", ContextType = SysDic.NText)]
        public virtual string Comment
        {
            get { return _comment; }
            set
            {
                if (value != null && value.Length > 1000000)
                    throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
                _comment = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "员工ID", ShortCode = "EmpID", Desc = "员工ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long EmpID
        {
            get { return _empID; }
            set { _empID = value; }
        }

        [DataMember]
        [DataDesc(CName = "员工姓名", ShortCode = "EmpName", Desc = "员工姓名", ContextType = SysDic.NText, Length = 50)]
        public virtual string EmpName
        {
            get { return _empName; }
            set { _empName = value; }
        }


    }
        #endregion
    #endregion
}
