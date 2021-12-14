using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace ZhiFang.Model{
	//StatusTypeControl		
[Serializable]
	public class StatusTypeControl	{ 
		public StatusTypeControl()
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
		/// StatusControlNo
        /// </summary>
        private string _statuscontrolno;
        public string StatusControlNo
        {
            get{ return _statuscontrolno; }
            set{ _statuscontrolno = value; }
        }  
       		      	
		/// <summary>
		/// StatusNo
        /// </summary>
        private int _statusno;
        public int StatusNo
        {
            get{ return _statusno; }
            set{ _statusno = value; }
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
		/// ControlStatusNo
        /// </summary>
        private int _controlstatusno;
        public int ControlStatusNo
        {
            get{ return _controlstatusno; }
            set{ _controlstatusno = value; }
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