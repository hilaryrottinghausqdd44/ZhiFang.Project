using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.Runtime.Serialization;
namespace ZhiFang.Model
{
	//Lab_PGroup		
	[DataContract]
	public class Lab_PGroup	
	{ 
		public Lab_PGroup()
        {}
              	      	
		/// <summary>
		/// SectionID
        /// </summary>
        private int _sectionid;
        [DataMember] 
        public int SectionID
        {
            get{ return _sectionid; }
            set{ _sectionid = value; }
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
		/// LabSectionNo
        /// </summary>
        private int _labsectionno;
        [DataMember] 
        public int LabSectionNo
        {
            get{ return _labsectionno; }
            set{ _labsectionno = value; }
        }  
       		      	
		/// <summary>
		/// SuperGroupNo
        /// </summary>
                      
		private int? _supergroupno;
        [DataMember] 
        public int? SuperGroupNo
        {
            get{ return _supergroupno; }
            set{ _supergroupno = value; }
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
		/// SectionDesc
        /// </summary>
        private string _sectiondesc;
        [DataMember] 
        public string SectionDesc
        {
            get{ return _sectiondesc; }
            set{ _sectiondesc = value; }
        }  
       		      	
		/// <summary>
		/// SectionType
        /// </summary>                      
		private int? _sectiontype;
        [DataMember] 
        public int? SectionType
        {
            get{ return _sectiontype; }
            set{ _sectiontype = value; }
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
		/// OnlineTime
        /// </summary>                      
		private int? _onlinetime;
        [DataMember] 
        public int? OnlineTime
        {
            get{ return _onlinetime; }
            set{ _onlinetime = value; }
        }  
        		      	
		/// <summary>
		/// KeyDispOrder
        /// </summary>                      
		private int? _keydisporder;
        [DataMember] 
        public int? KeyDispOrder
        {
            get{ return _keydisporder; }
            set{ _keydisporder = value; }
        }  
        		      	
		/// <summary>
		/// DummyGroup
        /// </summary>                      
		private int? _dummygroup;
        [DataMember] 
        public int? DummyGroup
        {
            get{ return _dummygroup; }
            set{ _dummygroup = value; }
        }  
        		      	
		/// <summary>
		/// UnionType
        /// </summary>                      
		private int? _uniontype;
        [DataMember] 
        public int? UnionType
        {
            get{ return _uniontype; }
            set{ _uniontype = value; }
        }  
        		      	
		/// <summary>
		/// SectorTypeNo
        /// </summary>                      
		private int? _sectortypeno;
        [DataMember] 
        public int? SectorTypeNo
        {
            get{ return _sectortypeno; }
            set{ _sectortypeno = value; }
        }  
        		      	
		/// <summary>
		/// SampleRule
        /// </summary>                      
		private int? _samplerule;
        [DataMember] 
        public int? SampleRule
        {
            get{ return _samplerule; }
            set{ _samplerule = value; }
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
		private string _orderfield = "SectionId";//排序

		[DataMember] 
        public string Orderfield
		{
			get { return _orderfield; }
			set { _orderfield = value; }
		}
		private string _searchlikekey;//模糊查询字段

		[DataMember] 
        public string Searchlikekey
		{
			get { return _searchlikekey; }
			set { _searchlikekey = value; }
		}
        		   		   		
	}
}