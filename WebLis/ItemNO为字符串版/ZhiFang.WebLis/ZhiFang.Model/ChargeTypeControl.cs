using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace ZhiFang.Model{
	//ChargeTypeControl		
[Serializable]
	public class ChargeTypeControl	{ 
		public ChargeTypeControl()
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
		/// ChargeControlNo
        /// </summary>
        private string _chargecontrolno;
        public string ChargeControlNo
        {
            get{ return _chargecontrolno; }
            set{ _chargecontrolno = value; }
        }  
       		      	
		/// <summary>
		/// ChargeNo
        /// </summary>
        private int _chargeno;
        public int ChargeNo
        {
            get{ return _chargeno; }
            set{ _chargeno = value; }
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
		/// ControlChargeNo
        /// </summary>
        private int _controlchargeno;
        public int ControlChargeNo
        {
            get{ return _controlchargeno; }
            set{ _controlchargeno = value; }
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