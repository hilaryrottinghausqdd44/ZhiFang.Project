using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace ZhiFang.Model{
	//Lab_StatusType		
[Serializable]
	public class Lab_StatusType	{ 
		public Lab_StatusType()
        {}
              	      	
		/// <summary>
		/// StatusID
        /// </summary>
        private int _statusid;
        public int StatusID
        {
            get{ return _statusid; }
            set{ _statusid = value; }
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
		/// LabStatusNo
        /// </summary>
        private int _labstatusno;
        public int LabStatusNo
        {
            get{ return _labstatusno; }
            set{ _labstatusno = value; }
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
		/// StatusDesc
        /// </summary>
        private string _statusdesc;
        public string StatusDesc
        {
            get{ return _statusdesc; }
            set{ _statusdesc = value; }
        }  
       		      	
		/// <summary>
		/// StatusColor
        /// </summary>
        private string _statuscolor;
        public string StatusColor
        {
            get{ return _statuscolor; }
            set{ _statuscolor = value; }
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