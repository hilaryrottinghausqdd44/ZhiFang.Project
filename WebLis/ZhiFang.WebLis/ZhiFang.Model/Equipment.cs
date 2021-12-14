using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.Model
{
    //Equipment		
    [Serializable]
    public class Equipment
    {
        public Equipment()
        { }
        private int _equipid;
        private int _equipno;
        private string _cname;
        private string _shortname;
        private string _shortcode;
        private int? _sectionno;
        private string _computer;
        private string _comprogram;
        private string _comport;
        private string _baudrate;
        private string _parity;
        private string _databits;
        private string _stopbits;
        private int? _visible;
        private int? _doubledir;
        private string _licencekey;
        private string _licencetype;
        private DateTime? _licencedate;
        private string _sqh;
        private int? _sno;
        private int? _sicktype;
        private int? _useimmplate;
        private int? _immcalc;
        private string _commpara;
        private string _reagentpara;
        private DateTime? _dtimestampe;
        private DateTime? _addtime;
        private string _standcode;
        private string _zfstandcode;
        private int? _useflag;

        /// <summary>
        /// EquipID
        /// </summary>
        public int EquipID
        {
            get { return _equipid; }
            set { _equipid = value; }
        }

        /// <summary>
        /// EquipNo
        /// </summary>
        public int EquipNo
        {
            get { return _equipno; }
            set { _equipno = value; }
        }

        /// <summary>
        /// CName
        /// </summary>
        public string CName
        {
            get { return _cname; }
            set { _cname = value; }
        }

        /// <summary>
        /// ShortName
        /// </summary>
        public string ShortName
        {
            get { return _shortname; }
            set { _shortname = value; }
        }

        /// <summary>
        /// ShortCode
        /// </summary>
        public string ShortCode
        {
            get { return _shortcode; }
            set { _shortcode = value; }
        }

        /// <summary>
        /// SectionNo
        /// </summary>
        public int? SectionNo
        {
            get { return _sectionno; }
            set { _sectionno = value; }
        }

        /// <summary>
        /// Computer
        /// </summary>
        public string Computer
        {
            get { return _computer; }
            set { _computer = value; }
        }

        /// <summary>
        /// ComProgram
        /// </summary>
        public string ComProgram
        {
            get { return _comprogram; }
            set { _comprogram = value; }
        }

        /// <summary>
        /// ComPort
        /// </summary>
        public string ComPort
        {
            get { return _comport; }
            set { _comport = value; }
        }

        /// <summary>
        /// BaudRate
        /// </summary>
        public string BaudRate
        {
            get { return _baudrate; }
            set { _baudrate = value; }
        }

        /// <summary>
        /// Parity
        /// </summary>
        public string Parity
        {
            get { return _parity; }
            set { _parity = value; }
        }

        /// <summary>
        /// DataBits
        /// </summary>
        public string DataBits
        {
            get { return _databits; }
            set { _databits = value; }
        }

        /// <summary>
        /// StopBits
        /// </summary>
        public string StopBits
        {
            get { return _stopbits; }
            set { _stopbits = value; }
        }

        /// <summary>
        /// Visible
        /// </summary>
        public int? Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        /// <summary>
        /// DoubleDir
        /// </summary>
        public int? DoubleDir
        {
            get { return _doubledir; }
            set { _doubledir = value; }
        }

        /// <summary>
        /// LicenceKey
        /// </summary>
        public string LicenceKey
        {
            get { return _licencekey; }
            set { _licencekey = value; }
        }

        /// <summary>
        /// LicenceType
        /// </summary>
        public string LicenceType
        {
            get { return _licencetype; }
            set { _licencetype = value; }
        }

        /// <summary>
        /// LicenceDate
        /// </summary>
        public DateTime? LicenceDate
        {
            get { return _licencedate; }
            set { _licencedate = value; }
        }

        /// <summary>
        /// SQH
        /// </summary>
        public string SQH
        {
            get { return _sqh; }
            set { _sqh = value; }
        }

        /// <summary>
        /// SNo
        /// </summary>
        public int? SNo
        {
            get { return _sno; }
            set { _sno = value; }
        }

        /// <summary>
        /// SickType
        /// </summary>
        public int? SickType
        {
            get { return _sicktype; }
            set { _sicktype = value; }
        }

        /// <summary>
        /// UseImmPlate
        /// </summary>
        public int? UseImmPlate
        {
            get { return _useimmplate; }
            set { _useimmplate = value; }
        }

        /// <summary>
        /// ImmCalc
        /// </summary>
        public int? ImmCalc
        {
            get { return _immcalc; }
            set { _immcalc = value; }
        }

        /// <summary>
        /// CommPara
        /// </summary>
        public string CommPara
        {
            get { return _commpara; }
            set { _commpara = value; }
        }

        /// <summary>
        /// ReagentPara
        /// </summary>
        public string ReagentPara
        {
            get { return _reagentpara; }
            set { _reagentpara = value; }
        }

        /// <summary>
        /// DTimeStampe
        /// </summary>
        public DateTime? DTimeStampe
        {
            get { return _dtimestampe; }
            set { _dtimestampe = value; }
        }

        /// <summary>
        /// AddTime
        /// </summary>
        public DateTime? AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }

        /// <summary>
        /// StandCode
        /// </summary>
        public string StandCode
        {
            get { return _standcode; }
            set { _standcode = value; }
        }

        /// <summary>
        /// ZFStandCode
        /// </summary>
        public string ZFStandCode
        {
            get { return _zfstandcode; }
            set { _zfstandcode = value; }
        }

        /// <summary>
        /// UseFlag
        /// </summary>
        public int? UseFlag
        {
            get { return _useflag; }
            set { _useflag = value; }
        }

        /// <summary>
        /// 中心--实验室  对照关系使用
        /// </summary>
        private string _labcode;
        public string LabCode
        {
            get { return _labcode; }
            set { _labcode = value; }
        }

    }
}