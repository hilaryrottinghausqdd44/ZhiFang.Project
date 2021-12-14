using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.Runtime.Serialization;
namespace ZhiFang.Model{
	 	//B_Lab_GroupItem
    [DataContract]
		public class Lab_GroupItem
	{
        public Lab_GroupItem() { }
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

