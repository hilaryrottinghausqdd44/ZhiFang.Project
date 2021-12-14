using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.Runtime.Serialization;
namespace ZhiFang.Model
{
	//CLIENTELEControl		
	[DataContract]
	public class CLIENTELEControl	
	{ 
		public CLIENTELEControl()
        {}
              	      	
		/// <summary>
		/// Id
        /// </summary>
        private int _id;
		[DataMember]
        public int Id
        {
            get{ return _id; }
            set{ _id = value; }
        }  
       		      	
		/// <summary>
		/// ClIENTControlNo
        /// </summary>
        private string _clientcontrolno;
		[DataMember]
        public string ClIENTControlNo
        {
            get{ return _clientcontrolno; }
            set{ _clientcontrolno = value; }
        }  
       		      	
		/// <summary>
		/// ClIENTNO
        /// </summary>
        private long _clientno;
		[DataMember]
        public long ClIENTNO
        {
            get{ return _clientno; }
            set{ _clientno = value; }
        }  
       		      	
		/// <summary>
		/// ControlLabNo
        /// </summary>
        private string _controllabno;
		[DataMember]
        public string ControlLabNo
        {
            get{ return _controllabno; }
            set{ _controllabno = value; }
        }  
       		      	
		/// <summary>
		/// ControlClIENTNO
        /// </summary>
        private int _controlclientno;
		[DataMember]
        public int ControlClIENTNO
        {
            get{ return _controlclientno; }
            set{ _controlclientno = value; }
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
		/// UseFlag
        /// </summary>
                      
		private int? _useflag=1;
		[DataMember]
        public int? UseFlag
        {
            get{ return _useflag; }
            set{ _useflag = value; }
        }  
        		   		   		
   		/// <summary>
		/// 中心--实验室  对照关系使用
        /// </summary>
        private string _labcode;
		[DataMember]
        public string LabCode
        {
            get { return _labcode; }
            set { _labcode = value; }
        }
   		   		
	}
}