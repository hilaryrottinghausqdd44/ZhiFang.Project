using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.Runtime.Serialization;
namespace ZhiFang.Model
{
	//Lab_CLIENTELE		
	[DataContract]
	public class Lab_CLIENTELE	
	{ 
		public Lab_CLIENTELE()
        {}
              	      	
		/// <summary>
		/// ClIENTID
        /// </summary>
        private int _clientid;
        [DataMember] 
        public int ClIENTID
        {
            get{ return _clientid; }
            set{ _clientid = value; }
        }
        //是否对照
        private string _controlstatus;
        [DataMember]
        public string ControlStatus
        {
            get { return _controlstatus; }
            set { _controlstatus = value; }
        }  	
		/// <summary>
		/// LabCode
        /// </summary>
        private string _labcode;
        [DataMember] 
        public string LabCode
        {
            get{ return _labcode; }
            set{ _labcode = value; }
        }  
       		      	
		/// <summary>
		/// LabClIENTNO
        /// </summary>
        private int _labclientno;
        [DataMember] 
        public int LabClIENTNO
        {
            get{ return _labclientno; }
            set{ _labclientno = value; }
        }  
       		      	
		/// <summary>
		/// CNAME
        /// </summary>
        private string _cname;
        [DataMember] 
        public string CNAME
        {
            get{ return _cname; }
            set{ _cname = value; }
        }  
       		      	
		/// <summary>
		/// ENAME
        /// </summary>
        private string _ename;
        [DataMember] 
        public string ENAME
        {
            get{ return _ename; }
            set{ _ename = value; }
        }  
       		      	
		/// <summary>
		/// SHORTCODE
        /// </summary>
        private string _shortcode;
        [DataMember] 
        public string SHORTCODE
        {
            get{ return _shortcode; }
            set{ _shortcode = value; }
        }  
       		      	
		/// <summary>
		/// ISUSE
        /// </summary>
		private int? _isuse=1;
        [DataMember] 
        public int? ISUSE
        {
            get{ return _isuse; }
            set{ _isuse = value; }
        }  
        		      	
		/// <summary>
		/// LINKMAN
        /// </summary>
        private string _linkman;
        [DataMember] 
        public string LINKMAN
        {
            get{ return _linkman; }
            set{ _linkman = value; }
        }  
       		      	
		/// <summary>
		/// PHONENUM1
        /// </summary>
        private string _phonenum1;
        [DataMember] 
        public string PHONENUM1
        {
            get{ return _phonenum1; }
            set{ _phonenum1 = value; }
        }  
       		      	
		/// <summary>
		/// ADDRESS
        /// </summary>
        private string _address;
        [DataMember] 
        public string ADDRESS
        {
            get{ return _address; }
            set{ _address = value; }
        }  
       		      	
		/// <summary>
		/// MAILNO
        /// </summary>
        private string _mailno;
        [DataMember] 
        public string MAILNO
        {
            get{ return _mailno; }
            set{ _mailno = value; }
        }  
       		      	
		/// <summary>
		/// EMAIL
        /// </summary>
        private string _email;
        [DataMember] 
        public string EMAIL
        {
            get{ return _email; }
            set{ _email = value; }
        }  
       		      	
		/// <summary>
		/// PRINCIPAL
        /// </summary>
        private string _principal;
        [DataMember] 
        public string PRINCIPAL
        {
            get{ return _principal; }
            set{ _principal = value; }
        }  
       		      	
		/// <summary>
		/// PHONENUM2
        /// </summary>
        private string _phonenum2;
        [DataMember] 
        public string PHONENUM2
        {
            get{ return _phonenum2; }
            set{ _phonenum2 = value; }
        }  
       		      	
		/// <summary>
		/// CLIENTTYPE
        /// </summary>
		private int? _clienttype;
        [DataMember] 
        public int? CLIENTTYPE
        {
            get{ return _clienttype; }
            set{ _clienttype = value; }
        }  
        		      	
		/// <summary>
		/// BmanNo
        /// </summary>
		private int? _bmanno;
        [DataMember] 
        public int? BmanNo
        {
            get{ return _bmanno; }
            set{ _bmanno = value; }
        }  
        		      	
		/// <summary>
		/// Romark
        /// </summary>
        private string _romark;
        [DataMember] 
        public string Romark
        {
            get{ return _romark; }
            set{ _romark = value; }
        }  
       		      	
		/// <summary>
		/// TitleType
        /// </summary>
		private int? _titletype;
        [DataMember] 
        public int? TitleType
        {
            get{ return _titletype; }
            set{ _titletype = value; }
        }  
        		      	
		/// <summary>
		/// UploadType
        /// </summary>
		private int? _uploadtype=1;
        [DataMember] 
        public int? UploadType
        {
            get{ return _uploadtype; }
            set{ _uploadtype = value; }
        }  
        		      	
		/// <summary>
		/// PrintType
        /// </summary>
		private int? _printtype;
        [DataMember] 
        public int? PrintType
        {
            get{ return _printtype; }
            set{ _printtype = value; }
        }  
        		      	
		/// <summary>
		/// InputDataType
        /// </summary>
		private int? _inputdatatype=0;
        [DataMember] 
        public int? InputDataType
        {
            get{ return _inputdatatype; }
            set{ _inputdatatype = value; }
        }  
        		      	
		/// <summary>
		/// ReportPageType
        /// </summary>
		private int? _reportpagetype=0;
        [DataMember] 
        public int? ReportPageType
        {
            get{ return _reportpagetype; }
            set{ _reportpagetype = value; }
        }  
        		      	
		/// <summary>
		/// ClientArea
        /// </summary>
        private string _clientarea;
        [DataMember] 
        public string ClientArea
        {
            get{ return _clientarea; }
            set{ _clientarea = value; }
        }  
       		      	
		/// <summary>
		/// ClientStyle
        /// </summary>
        private string _clientstyle;
        [DataMember] 
        public string ClientStyle
        {
            get{ return _clientstyle; }
            set{ _clientstyle = value; }
        }  
       		      	
		/// <summary>
		/// FaxNo
        /// </summary>
        private string _faxno;
        [DataMember] 
        public string FaxNo
        {
            get{ return _faxno; }
            set{ _faxno = value; }
        }  
       		      	
		/// <summary>
		/// AutoFax
        /// </summary>
		private int? _autofax;
        [DataMember] 
        public int? AutoFax
        {
            get{ return _autofax; }
            set{ _autofax = value; }
        }  
        		      	
		/// <summary>
		/// ClientReportTitle
        /// </summary>
        private string _clientreporttitle;
        [DataMember] 
        public string ClientReportTitle
        {
            get{ return _clientreporttitle; }
            set{ _clientreporttitle = value; }
        }  
       		      	
		/// <summary>
		/// IsPrintItem
        /// </summary>
		private int? _isprintitem=1;
        [DataMember] 
        public int? IsPrintItem
        {
            get{ return _isprintitem; }
            set{ _isprintitem = value; }
        }  
        		      	
		/// <summary>
		/// CZDY1
        /// </summary>
        private string _czdy1;
        [DataMember] 
        public string CZDY1
        {
            get{ return _czdy1; }
            set{ _czdy1 = value; }
        }  
       		      	
		/// <summary>
		/// CZDY2
        /// </summary>
        private string _czdy2;
        [DataMember] 
        public string CZDY2
        {
            get{ return _czdy2; }
            set{ _czdy2 = value; }
        }  
       		      	
		/// <summary>
		/// CZDY3
        /// </summary>
        private string _czdy3;
        [DataMember] 
        public string CZDY3
        {
            get{ return _czdy3; }
            set{ _czdy3 = value; }
        }  
       		      	
		/// <summary>
		/// CZDY4
        /// </summary>
        private string _czdy4;
        [DataMember] 
        public string CZDY4
        {
            get{ return _czdy4; }
            set{ _czdy4 = value; }
        }  
       		      	
		/// <summary>
		/// CZDY5
        /// </summary>
        private string _czdy5;
        [DataMember] 
        public string CZDY5
        {
            get{ return _czdy5; }
            set{ _czdy5 = value; }
        }  
       		      	
		/// <summary>
		/// CZDY6
        /// </summary>
        private string _czdy6;
        [DataMember] 
        public string CZDY6
        {
            get{ return _czdy6; }
            set{ _czdy6 = value; }
        }  
       		      	
		/// <summary>
		/// 联系人职务
        /// </summary>
        private string _linkmanposition;
        [DataMember] 
        public string LinkManPosition
        {
            get{ return _linkmanposition; }
            set{ _linkmanposition = value; }
        }  
       		      	
		/// <summary>
		/// 联系人2
        /// </summary>
        private string _linkman1;
        [DataMember] 
        public string LinkMan1
        {
            get{ return _linkman1; }
            set{ _linkman1 = value; }
        }  
       		      	
		/// <summary>
		/// 联系人职务2
        /// </summary>
        private string _linkmanposition1;
        [DataMember] 
        public string LinkManPosition1
        {
            get{ return _linkmanposition1; }
            set{ _linkmanposition1 = value; }
        }  
       		      	
		/// <summary>
		/// ClientCode
        /// </summary>
        private string _clientcode;
        [DataMember] 
        public string ClientCode
        {
            get{ return _clientcode; }
            set{ _clientcode = value; }
        }  
       		      	
		/// <summary>
		/// CWAccountDate
        /// </summary>
        private string _cwaccountdate;
        [DataMember] 
        public string CWAccountDate
        {
            get{ return _cwaccountdate; }
            set{ _cwaccountdate = value; }
        }  
       		      	
		/// <summary>
		/// NFClientType
        /// </summary>
        private string _nfclienttype;
        [DataMember] 
        public string NFClientType
        {
            get{ return _nfclienttype; }
            set{ _nfclienttype = value; }
        }  
       		      	
		/// <summary>
		/// RelationName
        /// </summary>
        private string _relationname;
        [DataMember] 
        public string RelationName
        {
            get{ return _relationname; }
            set{ _relationname = value; }
        }  
       		      	
		/// <summary>
		/// WebLisSourceOrgID
        /// </summary>
        private string _weblissourceorgid;
        [DataMember] 
        public string WebLisSourceOrgID
        {
            get{ return _weblissourceorgid; }
            set{ _weblissourceorgid = value; }
        }  
       		      	
		/// <summary>
		/// 办事处
        /// </summary>
        private string _groupname;
        [DataMember] 
        public string GroupName
        {
            get{ return _groupname; }
            set{ _groupname = value; }
        }  
       		      	
		/// <summary>
		/// DTimeStampe
        /// </summary>
		private DateTime? _dtimestampe;
        [DataMember] 
        public DateTime? DTimeStampe
        {
            get{ return _dtimestampe; }
            set{ _dtimestampe = value; }
        }  
        		      	
		/// <summary>
		/// AddTime
        /// </summary>
		private DateTime? _addtime=DateTime.Now;
        [DataMember] 
        public DateTime? AddTime
        {
            get{ return _addtime; }
            set{ _addtime = value; }
        }  
        		      	
		/// <summary>
		/// StandCode
        /// </summary>
        private string _standcode;
        [DataMember] 
        public string StandCode
        {
            get{ return _standcode; }
            set{ _standcode = value; }
        }  
       		      	
		/// <summary>
		/// ZFStandCode
        /// </summary>
        private string _zfstandcode;
        [DataMember] 
        public string ZFStandCode
        {
            get{ return _zfstandcode; }
            set{ _zfstandcode = value; }
        }  
       		      	
		/// <summary>
		/// UseFlag
        /// </summary>
		private int? _useflag=1;
        [DataMember] 
        public int? UseFlag
        {
            get{ return _useflag; }
            set{ _useflag = value; }
        }  
        		   		   		
	}
}