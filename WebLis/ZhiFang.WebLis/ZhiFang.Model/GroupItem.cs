using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.Runtime.Serialization;
namespace ZhiFang.Model{
	 	//B_GroupItem
    [DataContract]
		public class GroupItem
	{
        public GroupItem() { }
      	/// <summary>
		/// Id
        /// </summary>		
		private long _id;
        [DataMember]
        public long Id
        {
            get{ return _id; }
            set{ _id = value; }
        }        
		/// <summary>
		/// PItemNo
        /// </summary>		
		private string _pitemno;
        [DataMember]
        public string PItemNo
        {
            get{ return _pitemno; }
            set{ _pitemno = value; }
        }        
		/// <summary>
		/// ItemNo
        /// </summary>		
		private string _itemno;
        [DataMember]
        public string ItemNo
        {
            get{ return _itemno; }
            set{ _itemno = value; }
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
		private DateTime? _addtime;
        [DataMember]
        public DateTime? AddTime
        {
            get{ return _addtime; }
            set{ _addtime = value; }
        }        
		   
	}
}

