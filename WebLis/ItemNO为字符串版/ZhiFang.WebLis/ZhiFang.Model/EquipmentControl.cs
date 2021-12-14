using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace ZhiFang.Model{
	//EquipmentControl		
[Serializable]
	public class EquipmentControl	{ 
		public EquipmentControl()
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
		/// EquipControlNo
        /// </summary>
        private string _equipcontrolno;
        public string EquipControlNo
        {
            get{ return _equipcontrolno; }
            set{ _equipcontrolno = value; }
        }  
       		      	
		/// <summary>
		/// EquipNo
        /// </summary>
        private int _equipno;
        public int EquipNo
        {
            get{ return _equipno; }
            set{ _equipno = value; }
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
		/// ControlEquipNo
        /// </summary>
        private int _controlequipno;
        public int ControlEquipNo
        {
            get{ return _controlequipno; }
            set{ _controlequipno = value; }
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