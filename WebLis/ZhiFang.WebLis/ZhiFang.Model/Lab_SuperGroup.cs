using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.Runtime.Serialization;
namespace ZhiFang.Model
{
	//Lab_SuperGroup		
	[DataContract]
	public class Lab_SuperGroup	
	{ 
		public Lab_SuperGroup()
        {}
              	      	
		/// <summary>
		/// SuperGroupID
        /// </summary>
        private int _supergroupid;
        [DataMember] 
        public int SuperGroupID
        {
            get{ return _supergroupid; }
            set{ _supergroupid = value; }
        }
        //是否对照
        private string _controlstatus;
        [DataMember]
        public string ControlStatus
        {
            get { return _controlstatus; }
            set { _controlstatus = value; }
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
		/// LabSuperGroupNo
        /// </summary>
        private int _labsupergroupno;
        [DataMember] 
        public int LabSuperGroupNo
        {
            get{ return _labsupergroupno; }
            set{ _labsupergroupno = value; }
        }  
       		      	
		/// <summary>
		/// CName
        /// </summary>
        private string _cname;
        [DataMember] 
        public string CName
        {
            get{ return _cname; }
            set{ _cname = value; }
        }  
       		      	
		/// <summary>
		/// ShortName
        /// </summary>
        private string _shortname;
        [DataMember] 
        public string ShortName
        {
            get{ return _shortname; }
            set{ _shortname = value; }
        }  
       		      	
		/// <summary>
		/// ShortCode
        /// </summary>
        private string _shortcode;
        [DataMember] 
        public string ShortCode
        {
            get{ return _shortcode; }
            set{ _shortcode = value; }
        }  
       		      	
		/// <summary>
		/// Visible
        /// </summary>                      
		private int? _visible;
        [DataMember] 
        public int? Visible
        {
            get{ return _visible; }
            set{ _visible = value; }
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
		/// ParentNo
        /// </summary>                      
		private int? _parentno;
        [DataMember] 
        public int? ParentNo
        {
            get{ return _parentno; }
            set{ _parentno = value; }
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

        private string _orderfield = "SuperGroupID";//排序
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
        		   		   		
	}
}