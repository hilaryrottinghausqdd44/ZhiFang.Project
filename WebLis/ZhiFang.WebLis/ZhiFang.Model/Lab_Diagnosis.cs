using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace ZhiFang.Model{
	//Lab_Diagnosis		
[Serializable]
	public class Lab_Diagnosis	{ 
		public Lab_Diagnosis()
        {}
              	      	
		/// <summary>
		/// DiagID
        /// </summary>
        private int _diagid;
        public int DiagID
        {
            get{ return _diagid; }
            set{ _diagid = value; }
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
		/// LabDiagNo
        /// </summary>
        private int _labdiagno;
        public int LabDiagNo
        {
            get{ return _labdiagno; }
            set{ _labdiagno = value; }
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
		/// DiagDesc
        /// </summary>
        private string _diagdesc;
        public string DiagDesc
        {
            get{ return _diagdesc; }
            set{ _diagdesc = value; }
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
		/// Visible
        /// </summary>
                      
		private int? _visible=1;
        public int? Visible
        {
            get{ return _visible; }
            set{ _visible = value; }
        }  
        		      	
		/// <summary>
		/// DispOrder
        /// </summary>
                      
		private int? _disporder;
        public int? DispOrder
        {
            get{ return _disporder; }
            set{ _disporder = value; }
        }  
        		      	
		/// <summary>
		/// HisOrderCode
        /// </summary>
        private string _hisordercode;
        public string HisOrderCode
        {
            get{ return _hisordercode; }
            set{ _hisordercode = value; }
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