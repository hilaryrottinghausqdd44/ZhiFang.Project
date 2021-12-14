using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    [DataContract]
    public class SiteOperationRecords
    {
        #region Member Variables
        private long labID = 0;
        private long siteOperationID;
        private string siteIP;//站点ip
        private string siteHostName;//站点电脑名称
        private string serviceName;//调用的服务名称
        private long empID;
        private string empName;
        private DateTime? dataUpdateTime;//记录更新时间
        private DateTime? _dataAddTime;//记录添加时间
        private byte[] _dataTimeStamp;




        #endregion

        #region Constructors
        public SiteOperationRecords() { }
        public SiteOperationRecords(string siteIP, string siteHostName, string serviceName)
        {
            this.siteIP = siteIP;
            this.siteHostName = siteHostName;
            this.serviceName = serviceName;
        }

        #endregion
        #region Public Properties
        [DataMember]
        public long LabID { get => labID; set => labID = value; }
        [DataMember]
        public long SiteOperationID { get => siteOperationID; set => siteOperationID = value; }
        [DataMember]
        public string SiteIP { get => siteIP; set => siteIP = value; }
        [DataMember]
        public string SiteHostName { get => siteHostName; set => siteHostName = value; }
        [DataMember]
        public string ServiceName { get => serviceName; set => serviceName = value; }
        [DataMember]
        public long EmpID { get => empID; set => empID = value; }
        [DataMember]
        public string EmpName { get => empName; set => empName = value; }
        [DataMember]
        public DateTime? DataUpdateTime { get => dataUpdateTime; set => dataUpdateTime = value; }
        [DataMember]
        public DateTime? DataAddTime { get => _dataAddTime; set => _dataAddTime = value; }
        [DataMember]
        public byte[] DataTimeStamp { get => _dataTimeStamp; set => _dataTimeStamp = value; }
        #endregion

    }
}
