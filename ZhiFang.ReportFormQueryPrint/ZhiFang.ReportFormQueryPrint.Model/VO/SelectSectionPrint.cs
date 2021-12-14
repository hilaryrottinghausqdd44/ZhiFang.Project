namespace ZhiFang.ReportFormQueryPrint.Model
{
    using System;

    [Serializable]
    public class SelectSectionPrint
    {
        public DateTime DataAddTime;
        public DateTime DataMigrationTime;
        //public DateTime DataTimeStamp;
        public DateTime DataUpdateTime;
        public long LabID;
        private long? _clientno;
        private string _defprinter;
        private string _formatpara;
        private int? _itemcountmax;
        private int? _itemcountmin;
        private string _pageformat;
        private int? _pnested;
        private int? _ppreview;
        private byte[] _printfile;
        private string _printformat;
        private int? _printorder;
        private string _printprogram;
        private long _sectionno;
        private long _spid;
        private long? _testitemno;
        private long? _usedefprint;
        private string _microattribate;
        private string _cname;
        private int? _sicktypeno;
        private Boolean _IsRFGraphdataPDf;

        public long? clientno
        {
            get
            {
                return this._clientno;
            }
            set
            {
                this._clientno = value;
            }
        }

        public string DefPrinter
        {
            get
            {
                return this._defprinter;
            }
            set
            {
                this._defprinter = value;
            }
        }

        public string FormatPara
        {
            get
            {
                return this._formatpara;
            }
            set
            {
                this._formatpara = value;
            }
        }

        public int? ItemCountMax
        {
            get
            {
                return this._itemcountmax;
            }
            set
            {
                this._itemcountmax = value;
            }
        }

        public int? ItemCountMin
        {
            get
            {
                return this._itemcountmin;
            }
            set
            {
                this._itemcountmin = value;
            }
        }

        public string microattribute { get; set; }
        public string nodename { get; set; }

        public string pageformat
        {
            get
            {
                return this._pageformat;
            }
            set
            {
                this._pageformat = value;
            }
        }

        public int? PNested
        {
            get
            {
                return this._pnested;
            }
            set
            {
                this._pnested = value;
            }
        }

        public int? PPreview
        {
            get
            {
                return this._ppreview;
            }
            set
            {
                this._ppreview = value;
            }
        }

        public byte[] PrintFile
        {
            get
            {
                return this._printfile;
            }
            set
            {
                this._printfile = value;
            }
        }

        public string PrintFormat
        {
            get
            {
                return this._printformat;
            }
            set
            {
                this._printformat = value;
            }
        }

        public int? PrintOrder
        {
            get
            {
                return this._printorder;
            }
            set
            {
                this._printorder = value;
            }
        }

        public string PrintProgram
        {
            get
            {
                return this._printprogram;
            }
            set
            {
                this._printprogram = value;
            }
        }

        public long SectionNo
        {
            get
            {
                return this._sectionno;
            }
            set
            {
                this._sectionno = value;
            }
        }

        public int? sicktypeno
        {
            get
            {
                return this._sicktypeno;
            }
            set
            {
                this._sicktypeno = value;
            }
        }

        public long SPID
        {
            get
            {
                return this._spid;
            }
            set
            {
                this._spid = value;
            }
        }

        public long? TestItemNo
        {
            get
            {
                return this._testitemno;
            }
            set
            {
                this._testitemno = value;
            }
        }

        public long? UseDefPrint
        {
            get
            {
                return this._usedefprint;
            }
            set
            {
                this._usedefprint = value;
            }
        }
        public string Microattribate
        {
            get
            {
                return this._microattribate;
            }
            set
            {
                this._microattribate = value;
            }
        }
        public string CName
        {
            get
            {
                return this._cname;
            }
            set
            {
                this._cname = value;
            }
        }

        public Boolean IsRFGraphdataPDf
        {
            get
            {
                return this._IsRFGraphdataPDf;
            }
            set
            {
                this._IsRFGraphdataPDf = value;
            }
        }

    }
}

