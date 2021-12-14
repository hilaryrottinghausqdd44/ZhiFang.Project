using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    public class S_RequestVItem
    {
        
        #region Model
        private DateTime _receivedate;
        private int _sectionno;
        private int _testtypeno;
        private string _sampleno;
        private int _paritemno;
        private int _itemno;
        private decimal? _orgvalue;
        private decimal? _reportvalue;
        private string _orgdesc;
        private string _reportdesc;
        private string _reporttext;
        private byte[] _reportimage;
        private string _refrange;
        private int? _equipno;
        private int? _modified;
        private DateTime? _itemdate;
        private DateTime? _itemtime;
        private string _resultstatus;
        private int? _isprint;
        private int? _printorder;
        private string _graphfile;
        private int? _isfile;
        private string _graphfilename;
        private DateTime? _graphfiletime;
        private int? _isfiletoserver;
        /// <summary>
        /// 
        /// </summary>
        public DateTime ReceiveDate
        {
            set { _receivedate = value; }
            get { return _receivedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SectionNo
        {
            set { _sectionno = value; }
            get { return _sectionno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TestTypeNo
        {
            set { _testtypeno = value; }
            get { return _testtypeno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleNo
        {
            set { _sampleno = value; }
            get { return _sampleno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ParItemNo
        {
            set { _paritemno = value; }
            get { return _paritemno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ItemNo
        {
            set { _itemno = value; }
            get { return _itemno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? OrgValue
        {
            set { _orgvalue = value; }
            get { return _orgvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ReportValue
        {
            set { _reportvalue = value; }
            get { return _reportvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrgDesc
        {
            set { _orgdesc = value; }
            get { return _orgdesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReportDesc
        {
            set { _reportdesc = value; }
            get { return _reportdesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReportText
        {
            set { _reporttext = value; }
            get { return _reporttext; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] ReportImage
        {
            set { _reportimage = value; }
            get { return _reportimage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RefRange
        {
            set { _refrange = value; }
            get { return _refrange; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? EquipNo
        {
            set { _equipno = value; }
            get { return _equipno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Modified
        {
            set { _modified = value; }
            get { return _modified; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ItemDate
        {
            set { _itemdate = value; }
            get { return _itemdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ItemTime
        {
            set { _itemtime = value; }
            get { return _itemtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ResultStatus
        {
            set { _resultstatus = value; }
            get { return _resultstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsPrint
        {
            set { _isprint = value; }
            get { return _isprint; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PrintOrder
        {
            set { _printorder = value; }
            get { return _printorder; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GraphFile
        {
            set { _graphfile = value; }
            get { return _graphfile; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsFile
        {
            set { _isfile = value; }
            get { return _isfile; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GraphFileName
        {
            set { _graphfilename = value; }
            get { return _graphfilename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? GraphFileTime
        {
            set { _graphfiletime = value; }
            get { return _graphfiletime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? isFileToServer
        {
            set { _isfiletoserver = value; }
            get { return _isfiletoserver; }
        }
        #endregion Model
    }
}
