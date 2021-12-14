using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.Runtime.Serialization;
namespace ZhiFang.Model
{
	//DoctorControl		
	[DataContract]
	public class DoctorControl	
	{ 
		public DoctorControl()
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
		/// DoctorControlNo
        /// </summary>
        private string _doctorcontrolno;
        [DataMember] 
        public string DoctorControlNo
        {
            get{ return _doctorcontrolno; }
            set{ _doctorcontrolno = value; }
        }  
       		      	
		/// <summary>
		/// DoctorNo
        /// </summary>
        private int? _doctorno;
        [DataMember] 
        public int? DoctorNo
        {
            get{ return _doctorno; }
            set{ _doctorno = value; }
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
		/// ControlDoctorNo
        /// </summary>
        private int _controldoctorno;
        [DataMember] 
        public int ControlDoctorNo
        {
            get{ return _controldoctorno; }
            set{ _controldoctorno = value; }
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

        private string _orderfield = "Id";//排序
        [DataMember] 
        public string OrderField
        {
            get { return _orderfield; }
            set { _orderfield = value; }
        }

        private string _searchlikekey;//模糊查询字段
        [DataMember] 
        public string SearchLikeKey
        {
            get { return _searchlikekey; }
            set { _searchlikekey = value; }
        }
        /// <summary>
        /// 对照状态
        /// </summary>
        private string _controlstate;
        [DataMember]
        public string ControlState
        {
            get { return _controlstate; }
            set { _controlstate = value; }
        }

        #region
        private string _cname;
        [DataMember]
        public string CName
        {
            get { return _cname; }
            set { _cname = value; }
        }
        private string _centercname;
        [DataMember]
        public string CenterCName
        {
            get { return _centercname; }
            set { _centercname = value; }
        }
        private string _shortcode;
        [DataMember]
        public string ShortCode
        {
            get { return _shortcode; }
            set { _shortcode = value; }
        }
        private string _labdoctorno;
        [DataMember]
        public string LabDoctorNo
        {
            get { return _labdoctorno; }
            set { _labdoctorno = value; }
        }
        #endregion
	}
}