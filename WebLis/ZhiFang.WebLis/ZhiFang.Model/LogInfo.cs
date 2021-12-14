using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace ZhiFang.Model{
	 	//D_LogInfo
    [Serializable]
		public class LogInfo
        {
            public LogInfo() { }
   		     
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
		/// TableName
        /// </summary>		
		private string _tablename;
        public string TableName
        {
            get{ return _tablename; }
            set{ _tablename = value; }
        }       
		   
		/// <summary>
		/// DTimeStampe
        /// </summary>		
		private DateTime _dtimestampe;
        public DateTime DTimeStampe
        {
            get{ return _dtimestampe; }
            set{ _dtimestampe = value; }
        }
        /// <summary>
        /// 时间戳转换为int类型
        /// </summary>		
        private int? _inttimestampe;
        public int? IntTimeStampe
        {
            get { return _inttimestampe; }
            set { _inttimestampe = value; }
        }   
		/// <summary>
		/// UserID
        /// </summary>		
		private string _userid;
        public string UserID
        {
            get{ return _userid; }
            set{ _userid = value; }
        }        
		/// <summary>
		/// UserName
        /// </summary>		
		private string _username;
        public string UserName
        {
            get{ return _username; }
            set{ _username = value; }
        }        
		/// <summary>
		/// AddTime
        /// </summary>		
		private DateTime _addtime;
        public DateTime AddTime
        {
            get{ return _addtime; }
            set{ _addtime = value; }
        }        
		/// <summary>
		/// UseFlag
        /// </summary>		
		private int _useflag;
        public int UseFlag
        {
            get{ return _useflag; }
            set{ _useflag = value; }
        }        
		   
	}
}

