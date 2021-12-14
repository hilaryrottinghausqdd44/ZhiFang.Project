using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace ZhiFang.Model{
	//DiagnosisControl		
[Serializable]
	public class DiagnosisControl	{ 
		public DiagnosisControl()
        {}
              	      	
		/// <summary>
		/// Id
        /// </summary>
        private int _id;
        public int Id
        {
            get{ return _id; }
            set{ _id = value; }
        }  
       		      	
		/// <summary>
		/// DiagControlNo
        /// </summary>
        private string _diagcontrolno;
        public string DiagControlNo
        {
            get{ return _diagcontrolno; }
            set{ _diagcontrolno = value; }
        }  
       		      	
		/// <summary>
		/// DiagNo
        /// </summary>
        private int _diagno;
        public int DiagNo
        {
            get{ return _diagno; }
            set{ _diagno = value; }
        }  
       		      	
		/// <summary>
		/// ControlLabNo
        /// </summary>
        private string _controllabno;
        public string ControlLabNo
        {
            get{ return _controllabno; }
            set{ _controllabno = value; }
        }  
       		      	
		/// <summary>
		/// ControlDiagNo
        /// </summary>
        private int _controldiagno;
        public int ControlDiagNo
        {
            get{ return _controldiagno; }
            set{ _controldiagno = value; }
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
		/// UseFlag
        /// </summary>
                      
		private int? _useflag=1;
        public int? UseFlag
        {
            get{ return _useflag; }
            set{ _useflag = value; }
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