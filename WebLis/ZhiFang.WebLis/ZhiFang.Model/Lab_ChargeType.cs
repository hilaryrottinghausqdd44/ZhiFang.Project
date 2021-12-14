using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace ZhiFang.Model{
	//Lab_ChargeType		
[Serializable]
	public class Lab_ChargeType	{ 
		public Lab_ChargeType()
        {}
              	      	
		/// <summary>
		/// ChargeID
        /// </summary>
        private int _chargeid;
        public int ChargeID
        {
            get{ return _chargeid; }
            set{ _chargeid = value; }
        }  
       		      	
		/// <summary>
		/// LabCode
        /// </summary>
        private string _labcode;
        public string LabCode
        {
            get{ return _labcode; }
            set{ _labcode = value; }
        }  
       		      	
		/// <summary>
		/// LabChargeNo
        /// </summary>
        private int _labchargeno;
        public int LabChargeNo
        {
            get{ return _labchargeno; }
            set{ _labchargeno = value; }
        }  
       		      	
		/// <summary>
		/// CName
        /// </summary>
        private string _cname;
        public string CName
        {
            get{ return _cname; }
            set{ _cname = value; }
        }  
       		      	
		/// <summary>
		/// ChargeTypeDesc
        /// </summary>
        private string _chargetypedesc;
        public string ChargeTypeDesc
        {
            get{ return _chargetypedesc; }
            set{ _chargetypedesc = value; }
        }  
       		      	
		/// <summary>
		/// Discount
        /// </summary>
                      
		private decimal? _discount;
        public decimal? Discount
        {
            get{ return _discount; }
            set{ _discount = value; }
        }  
        		      	
		/// <summary>
		/// Append
        /// </summary>
                      
		private decimal? _append;
        public decimal? Append
        {
            get{ return _append; }
            set{ _append = value; }
        }  
        		      	
		/// <summary>
		/// ShortCode
        /// </summary>
        private string _shortcode;
        public string ShortCode
        {
            get{ return _shortcode; }
            set{ _shortcode = value; }
        }  
       		      	
		/// <summary>
		/// Visible
        /// </summary>
                      
		private int? _visible=1;
        public int? Visible
        {
            get{ return _visible; }
            set{ _visible = value; }
        }  
        		      	
		/// <summary>
		/// DispOrder
        /// </summary>
                      
		private int? _disporder;
        public int? DispOrder
        {
            get{ return _disporder; }
            set{ _disporder = value; }
        }  
        		      	
		/// <summary>
		/// HisOrderCode
        /// </summary>
        private string _hisordercode;
        public string HisOrderCode
        {
            get{ return _hisordercode; }
            set{ _hisordercode = value; }
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
                      
		private DateTime? _addtime;
        public DateTime? AddTime
        {
            get{ return _addtime; }
            set{ _addtime = value; }
        }  
        		      	
		/// <summary>
		/// StandCode
        /// </summary>
        private string _standcode;
        public string StandCode
        {
            get{ return _standcode; }
            set{ _standcode = value; }
        }  
       		      	
		/// <summary>
		/// ZFStandCode
        /// </summary>
        private string _zfstandcode;
        public string ZFStandCode
        {
            get{ return _zfstandcode; }
            set{ _zfstandcode = value; }
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
        		   		   		
	}
}