using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace ZhiFang.Model{
	//Lab_Equipment		
[Serializable]
	public class Lab_Equipment	{ 
		public Lab_Equipment()
        {}
              	      	
		/// <summary>
		/// EquipID
        /// </summary>
        private int _equipid;
        public int EquipID
        {
            get{ return _equipid; }
            set{ _equipid = value; }
        }  
       		      	
		/// <summary>
		/// LabCode
        /// </summary>
        private string _labcode;
        public string LabCode
        {
            get{ return _labcode; }
            set{ _labcode = value; }
        }  
       		      	
		/// <summary>
		/// LabEquipNo
        /// </summary>
        private int _labequipno;
        public int LabEquipNo
        {
            get{ return _labequipno; }
            set{ _labequipno = value; }
        }  
       		      	
		/// <summary>
		/// CName
        /// </summary>
        private string _cname;
        public string CName
        {
            get{ return _cname; }
            set{ _cname = value; }
        }  
       		      	
		/// <summary>
		/// ShortName
        /// </summary>
        private string _shortname;
        public string ShortName
        {
            get{ return _shortname; }
            set{ _shortname = value; }
        }  
       		      	
		/// <summary>
		/// ShortCode
        /// </summary>
        private string _shortcode;
        public string ShortCode
        {
            get{ return _shortcode; }
            set{ _shortcode = value; }
        }  
       		      	
		/// <summary>
		/// SectionNo
        /// </summary>
                      
		private int? _sectionno;
        public int? SectionNo
        {
            get{ return _sectionno; }
            set{ _sectionno = value; }
        }  
        		      	
		/// <summary>
		/// Computer
        /// </summary>
        private string _computer;
        public string Computer
        {
            get{ return _computer; }
            set{ _computer = value; }
        }  
       		      	
		/// <summary>
		/// ComProgram
        /// </summary>
        private string _comprogram;
        public string ComProgram
        {
            get{ return _comprogram; }
            set{ _comprogram = value; }
        }  
       		      	
		/// <summary>
		/// ComPort
        /// </summary>
        private string _comport;
        public string ComPort
        {
            get{ return _comport; }
            set{ _comport = value; }
        }  
       		      	
		/// <summary>
		/// BaudRate
        /// </summary>
        private string _baudrate;
        public string BaudRate
        {
            get{ return _baudrate; }
            set{ _baudrate = value; }
        }  
       		      	
		/// <summary>
		/// Parity
        /// </summary>
        private string _parity;
        public string Parity
        {
            get{ return _parity; }
            set{ _parity = value; }
        }  
       		      	
		/// <summary>
		/// DataBits
        /// </summary>
        private string _databits;
        public string DataBits
        {
            get{ return _databits; }
            set{ _databits = value; }
        }  
       		      	
		/// <summary>
		/// StopBits
        /// </summary>
        private string _stopbits;
        public string StopBits
        {
            get{ return _stopbits; }
            set{ _stopbits = value; }
        }  
       		      	
		/// <summary>
		/// Visible
        /// </summary>
                      
		private int? _visible;
        public int? Visible
        {
            get{ return _visible; }
            set{ _visible = value; }
        }  
        		      	
		/// <summary>
		/// DoubleDir
        /// </summary>
                      
		private int? _doubledir;
        public int? DoubleDir
        {
            get{ return _doubledir; }
            set{ _doubledir = value; }
        }  
        		      	
		/// <summary>
		/// LicenceKey
        /// </summary>
        private string _licencekey;
        public string LicenceKey
        {
            get{ return _licencekey; }
            set{ _licencekey = value; }
        }  
       		      	
		/// <summary>
		/// LicenceType
        /// </summary>
        private string _licencetype;
        public string LicenceType
        {
            get{ return _licencetype; }
            set{ _licencetype = value; }
        }  
       		      	
		/// <summary>
		/// LicenceDate
        /// </summary>
                      
		private DateTime? _licencedate;
        public DateTime? LicenceDate
        {
            get{ return _licencedate; }
            set{ _licencedate = value; }
        }  
        		      	
		/// <summary>
		/// SQH
        /// </summary>
        private string _sqh;
        public string SQH
        {
            get{ return _sqh; }
            set{ _sqh = value; }
        }  
       		      	
		/// <summary>
		/// SNo
        /// </summary>
                      
		private int? _sno;
        public int? SNo
        {
            get{ return _sno; }
            set{ _sno = value; }
        }  
        		      	
		/// <summary>
		/// SickType
        /// </summary>
                      
		private int? _sicktype;
        public int? SickType
        {
            get{ return _sicktype; }
            set{ _sicktype = value; }
        }  
        		      	
		/// <summary>
		/// UseImmPlate
        /// </summary>
                      
		private int? _useimmplate;
        public int? UseImmPlate
        {
            get{ return _useimmplate; }
            set{ _useimmplate = value; }
        }  
        		      	
		/// <summary>
		/// ImmCalc
        /// </summary>
                      
		private int? _immcalc;
        public int? ImmCalc
        {
            get{ return _immcalc; }
            set{ _immcalc = value; }
        }  
        		      	
		/// <summary>
		/// CommPara
        /// </summary>
        private string _commpara;
        public string CommPara
        {
            get{ return _commpara; }
            set{ _commpara = value; }
        }  
       		      	
		/// <summary>
		/// ReagentPara
        /// </summary>
        private string _reagentpara;
        public string ReagentPara
        {
            get{ return _reagentpara; }
            set{ _reagentpara = value; }
        }  
       		      	
		/// <summary>
		/// DTimeStampe
        /// </summary>
                      
		private DateTime? _dtimestampe;
        public DateTime? DTimeStampe
        {
            get{ return _dtimestampe; }
            set{ _dtimestampe = value; }
        }  
        		      	
		/// <summary>
		/// AddTime
        /// </summary>
                      
		private DateTime? _addtime=DateTime.Now;
        public DateTime? AddTime
        {
            get{ return _addtime; }
            set{ _addtime = value; }
        }  
        		      	
		/// <summary>
		/// StandCode
        /// </summary>
        private string _standcode;
        public string StandCode
        {
            get{ return _standcode; }
            set{ _standcode = value; }
        }  
       		      	
		/// <summary>
		/// ZFStandCode
        /// </summary>
        private string _zfstandcode;
        public string ZFStandCode
        {
            get{ return _zfstandcode; }
            set{ _zfstandcode = value; }
        }  
       		      	
		/// <summary>
		/// UseFlag
        /// </summary>
                      
		private int? _useflag=1;
        public int? UseFlag
        {
            get{ return _useflag; }
            set{ _useflag = value; }
        }  
        		   		   		
	}
}