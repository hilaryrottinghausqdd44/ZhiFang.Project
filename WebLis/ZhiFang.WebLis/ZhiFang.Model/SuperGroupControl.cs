using System; 
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
namespace ZhiFang.Model
{
	//SuperGroupControl		
	[DataContract]
	public class SuperGroupControl	
	{ 
		public SuperGroupControl()
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
		/// SuperGroupControlNo
        /// </summary>
        private string _supergroupcontrolno;
        [DataMember] 
        public string SuperGroupControlNo
        {
            get{ return _supergroupcontrolno; }
            set{ _supergroupcontrolno = value; }
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
        /// SuperGroupID
        /// </summary>
        private int _supergroupid;
        [DataMember]
        public int SuperGroupID
        {
            get { return _supergroupid; }
            set { _supergroupid = value; }
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
		/// ControlSuperGroupNo
        /// </summary>
        private int _controlsupergroupno;
        [DataMember] 
        public int ControlSuperGroupNo
        {
            get{ return _controlsupergroupno; }
            set{ _controlsupergroupno = value; }
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

        private string _orderfield = "";//排序
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
        private string _labsupergroupno;
        [DataMember]
        public string LabSuperGroupNo
        {
            get { return _labsupergroupno; }
            set { _labsupergroupno = value; }
        }
        #endregion
	}
}