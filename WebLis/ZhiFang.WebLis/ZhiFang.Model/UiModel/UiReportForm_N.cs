using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Tools;

namespace ZhiFang.Model.UiModel
{
    public class UiReportForm_N
    {
        private DateTime? _receivedate = null;
        private string _sectionno;
        private string _testtypeno;
        private string _sampleno;
        private int? _statusno;
        private int? _sampletypeno;
        private string _patno;
        private string _cname;
        private string _reportformid;
        private string _clientno;
        private int? _printtimes;
        private string _sectiontype;
        private int formno;

        public int FormNo
        {
            get { return formno; }
            set { formno = value; }
        }

        public string SectionType
        {
            get { return _sectiontype; }
            set { _sectiontype = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? PRINTTIMES
        {
            set { _printtimes = value; }
            get { return _printtimes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CLIENTNO
        {
            set { _clientno = value; }
            get { return _clientno; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ReportFormID
        {
            get { return _reportformid; }
            set { _reportformid = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime? RECEIVEDATE
        {
            set { _receivedate = value; }
            get { return _receivedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SECTIONNO
        {
            set { _sectionno = value; }
            get { return _sectionno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TestTypeNo
        {
            set { _testtypeno = value; }
            get { return _testtypeno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SAMPLENO
        {
            set { _sampleno = value; }
            get { return _sampleno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? StatusNo
        {
            set { _statusno = value; }
            get { return _statusno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SampleTypeNo
        {
            set { _sampletypeno = value; }
            get { return _sampletypeno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PatNo
        {
            set { _patno = value; }
            get { return _patno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CNAME
        {
            set { _cname = value; }
            get { return _cname; }
        }
    }
}
