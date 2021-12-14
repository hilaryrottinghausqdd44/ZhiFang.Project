using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.Runtime.Serialization;
namespace ZhiFang.Model
{
	//Lab_BUSINESSMAN		
	[DataContract]
	public class Lab_BUSINESSMAN	
	{ 
		public Lab_BUSINESSMAN()
        {}
              	      	
		/// <summary>
		/// BNANID
        /// </summary>
        private int _bnanid;
        [DataMember] 
        public int BNANID
        {
            get{ return _bnanid; }
            set{ _bnanid = value; }
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
		/// LabBMANNO
        /// </summary>
        private int _labbmanno;
        [DataMember] 
        public int LabBMANNO
        {
            get{ return _labbmanno; }
            set{ _labbmanno = value; }
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
		/// IDCODE
        /// </summary>
        private string _idcode;
        [DataMember] 
        public string IDCODE
        {
            get{ return _idcode; }
            set{ _idcode = value; }
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
		/// PHONENUM
        /// </summary>
        private string _phonenum;
        [DataMember] 
        public string PHONENUM
        {
            get{ return _phonenum; }
            set{ _phonenum = value; }
        }  
       		      	
		/// <summary>
		/// ROMARK
        /// </summary>
        private string _romark;
        [DataMember] 
        public string ROMARK
        {
            get{ return _romark; }
            set{ _romark = value; }
        }  
       		      	
		/// <summary>
		/// DispOrder
        /// </summary>
		private int? _disporder;
        [DataMember] 
        public int? DispOrder
        {
            get{ return _disporder; }
            set{ _disporder = value; }
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