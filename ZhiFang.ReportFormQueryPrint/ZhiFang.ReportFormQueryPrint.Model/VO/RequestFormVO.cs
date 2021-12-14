using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using ZhiFang.ReportFormQueryPrint.Common;

namespace ZhiFang.ReportFormQueryPrint.Model.VO
{
    [DataContract]
    public class RequestFormVO
    {
        private DateTime? _receivedate = null;
        private long? _sectionno;
        private string _sicktypename;
        private long? _testtypeno;
        private string _sampleno;
        private long? _statusno;
        private long? _sampletypeno;
        private string _patno;
        private string _cname;
        private string _reportformid;
        private long? _clientno;
        private long? _printtimes;
        private long? _sectiontype;
        private string _formno;
        private string _itemname;
        private string _sampletype;
        private DateTime? _receivetime;
        private DateTime? _checkdate;
        private DateTime? _checktime;
        private string pagename;
        private string pagecount;
        private string serialno;
        private string _bed;
        private DateTime? _PrintDateTime;
        private string _hserialno;
        private string _zdy1;
        private string _zdy2;
        private string _zdy3;
        private string _zdy4;
        private string _zdy5;
        private string _zdy6;
        private string _zdy7;
        private string _zdy8;
        private string _zdy9;
        private string _zdy10;
        private int? _sicktypeno;
        private int? _deptno;
        private int? _districtno;
        private string _clientprint;
        private int? _btempreport;
        private int? _supergroupno;
        private string _supergroupname;
        private int _supergroupnamenum;//检验大组的计数统计   COUNT
        private string _reportstatus;
        private int? _bRevised;
        private string _paritemname;

        [DataMember]
        public string Serialno
        {
            get { return serialno; }
            set { serialno = value; }
        }
        
        [DataMember]
        public string SickTypeName
        {
            get { return _sicktypename; }
            set { _sicktypename = value; }
        }
        [DataMember]
        public string FormNo
        {
            get { return _formno; }
            set { _formno = value; }
        }
        [DataMember]
        public long? SectionType
        {
            get { return _sectiontype; }
            set { _sectiontype = value; }
        }
        [DataMember]

        public long? PRINTTIMES
        {
            set { _printtimes = value; }
            get { return _printtimes; }
        }
        [DataMember]
        public long? CLIENTNO
        {
            set { _clientno = value; }
            get { return _clientno; }
        }

        [DataMember]
        public string ReportFormID
        {
            get { return _reportformid; }
            set { _reportformid = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime? RECEIVEDATE
        {
            set { _receivedate = value; }
            get { return _receivedate; }
        }
        [DataMember]
        public long? SECTIONNO
        {
            set { _sectionno = value; }
            get { return _sectionno; }
        }
        [DataMember]
        public long? TestTypeNo
        {
            set { _testtypeno = value; }
            get { return _testtypeno; }
        }
        [DataMember]
        public string SAMPLENO
        {
            set { _sampleno = value; }
            get { return _sampleno; }
        }
        [DataMember]
        public long? StatusNo
        {
            set { _statusno = value; }
            get { return _statusno; }
        }
        [DataMember]
        public long? SampleTypeNo
        {
            set { _sampletypeno = value; }
            get { return _sampletypeno; }
        }
        [DataMember]
        public string PatNo
        {
            set { _patno = value; }
            get { return _patno; }
        }
        [DataMember]
        public string CNAME
        {
            set { _cname = value; }
            get { return _cname; }
        }

        [DataMember]
        public string ItemName
        {
            set { _itemname = value; }
            get { return _itemname; }
        }

        [DataMember]
        public string SampleType
        {
            set { _sampletype = value; }
            get { return _sampletype; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime? RECEIVETIME
        {
            set { _receivetime = value; }
            get { return _receivetime; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime? CHECKDATE
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime? CHECKTIME
        {
            set { _checktime = value; }
            get { return _checktime; }
        }

        /// <summary>
        /// 纸张大小
        /// </summary>
        [DataMember]
        public string PageName
        {
            set { pagename = value; }
            get { return pagename; }
        }

        /// <summary>
        /// PDF文件页数
        /// </summary>
        [DataMember]
        public string PageCount
        {
            set { pagecount = value; }
            get { return pagecount; }
        }

        /// <summary>
        /// 床号
        /// </summary>
        [DataMember]
        public string Bed
        {
            set { _bed = value; }
            get { return _bed; }
        }

        /// <summary>
        /// 打印时间
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime? PrintDateTime
        {
            set { _PrintDateTime = value; }
            get { return _PrintDateTime; }
        }

        /// <summary>
        /// 医院申请单号
        /// </summary>
        [DataMember]
        public string HSerialno
        {
            set { _hserialno = value; }
            get { return _hserialno; }
        }

        /// <summary>
        /// ZDY1
        /// </summary>
        [DataMember]
        public string ZDY1
        {
            set { _zdy1 = value; }
            get { return _zdy1; }
        }
        /// <summary>
        /// ZDY2
        /// </summary>
        [DataMember]
        public string ZDY2
        {
            set { _zdy2 = value; }
            get { return _zdy2; }
        }
        /// <summary>
        /// ZDY3
        /// </summary>
        [DataMember]
        public string ZDY3
        {
            set { _zdy3= value; }
            get { return _zdy3; }
        }
        /// <summary>
        /// ZDY4
        /// </summary>
        [DataMember]
        public string ZDY4
        {
            set { _zdy4 = value; }
            get { return _zdy4; }
        }
        /// <summary>
        /// ZDY5
        /// </summary>
        [DataMember]
        public string ZDY5
        {
            set { _zdy5 = value; }
            get { return _zdy5; }
        }
        /// <summary>
        /// ZDY6
        /// </summary>
        [DataMember]
        public string ZDY6
        {
            set { _zdy6 = value; }
            get { return _zdy6; }
        }
        /// <summary>
        /// ZDY7
        /// </summary>
        [DataMember]
        public string ZDY7
        {
            set { _zdy7 = value; }
            get { return _zdy7; }
        }
        /// <summary>
        /// ZDY8
        /// </summary>
        [DataMember]
        public string ZDY8
        {
            set { _zdy8 = value; }
            get { return _zdy8; }
        }
        /// <summary>
        /// ZDY9
        /// </summary>
        [DataMember]
        public string ZDY9
        {
            set { _zdy9 = value; }
            get { return _zdy9; }
        }
        /// <summary>
        /// ZDY10
        /// </summary>
        [DataMember]
        public string ZDY10
        {
            set { _zdy10 = value; }
            get { return _zdy10; }
        }
        [DataMember]
        public int? SickTypeNo
        {
            set { _sicktypeno = value; }
            get { return _sicktypeno; }
        }
        [DataMember]
        public int? DeptNo
        {
            set { _deptno = value; }
            get { return _deptno; }
        }
        [DataMember]
        public int? DistrictNo
        {
            set { _districtno = value; }
            get { return _districtno; }
        }

        [DataMember]
        public string clientprint
        {
            set { _clientprint = value; }
            get { return _clientprint; }
        }

        [DataMember]
        public int? bTempReport
        {
            set { _btempreport = value; }
            get { return _btempreport; }
        }

        [DataMember]
        public string SuperGroupName
        {
            set { _supergroupname = value; }
            get { return _supergroupname; }
        }

        [DataMember]
        public int? SuperGroupNo
        {
            set { _supergroupno = value; }
            get { return _supergroupno; }
        }

        [DataMember]
        public int SuperGroupNameNum
        {
            set { _supergroupnamenum = value; }
            get { return _supergroupnamenum; }
        }
        /// <summary>
        /// 报告状态  不属于表字段
        /// </summary>
        public string ReportStatus
        {
            get { return _reportstatus; }
            set { _reportstatus = value; }
        }

        /// <summary>
        /// 重申字段
        /// </summary>
        public int? BRevised
        {
            get { return _bRevised; }
            set { _bRevised = value; }

        }
        [DataMember]
        public string ParItemName { get => _paritemname; set => _paritemname = value; }
    }
}