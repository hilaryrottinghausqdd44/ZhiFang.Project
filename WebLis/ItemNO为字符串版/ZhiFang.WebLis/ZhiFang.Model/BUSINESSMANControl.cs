using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.Runtime.Serialization;
namespace ZhiFang.Model
{
	//BUSINESSMANControl		
	[DataContract]
	public class BUSINESSMANControl	
	{ 
		public BUSINESSMANControl()
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
		/// BMANControlNo
        /// </summary>
        private string _bmancontrolno;
		[DataMember]
        public string BMANControlNo
        {
            get{ return _bmancontrolno; }
            set{ _bmancontrolno = value; }
        }  
       		      	
		/// <summary>
		/// BMANNO
        /// </summary>
        private int _bmanno;
		[DataMember]
        public int BMANNO
        {
            get{ return _bmanno; }
            set{ _bmanno = value; }
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
		/// ControlBMANNO
        /// </summary>
        private int _controlbmanno;
		[DataMember]
        public int ControlBMANNO
        {
            get{ return _controlbmanno; }
            set{ _controlbmanno = value; }
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