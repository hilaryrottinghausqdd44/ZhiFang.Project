using System;
namespace ZhiFang.ReportFormQueryPrint.Model
{
	/// <summary>
	/// 实体类TestItem 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class TestItem
	{
		public TestItem()
		{}
		#region Model
		private long? _itemno;
		private string _cname;
		private string _ename;
		private string _shortname;
		private string _shortcode;
		private string _diagmethod;
		private string _unit;
		private int? _iscalc;
		private int? _visible;
		private int? _disporder;
		private int? _prec;
		private int _isprofile;
		private string _orderno;
		private string _standardcode;
		private string _itemdesc;
		private decimal? _fworkload;
		private int? _secretgrade;
		private int? _cuegrade;
		private int? _isdoctoritem;
		private int? _ischargeitem;
		private int? _hisdisporder;
		private decimal? _price;
		private int? _supergroupno;
        private int? _isnurseitem;
		/// <summary>
		/// 
		/// </summary>
		public long? ItemNo
		{
			set{ _itemno=value;}
			get{return _itemno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CName
		{
			set{ _cname=value;}
			get{return _cname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EName
		{
			set{ _ename=value;}
			get{return _ename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShortName
		{
			set{ _shortname=value;}
			get{return _shortname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShortCode
		{
			set{ _shortcode=value;}
			get{return _shortcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DiagMethod
		{
			set{ _diagmethod=value;}
			get{return _diagmethod;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Unit
		{
			set{ _unit=value;}
			get{return _unit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsCalc
		{
			set{ _iscalc=value;}
			get{return _iscalc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Visible
		{
			set{ _visible=value;}
			get{return _visible;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DispOrder
		{
			set{ _disporder=value;}
			get{return _disporder;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Prec
		{
			set{ _prec=value;}
			get{return _prec;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsProfile
		{
			set{ _isprofile=value;}
			get{return _isprofile;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrderNo
		{
			set{ _orderno=value;}
			get{return _orderno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StandardCode
		{
			set{ _standardcode=value;}
			get{return _standardcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ItemDesc
		{
			set{ _itemdesc=value;}
			get{return _itemdesc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? FWorkLoad
		{
			set{ _fworkload=value;}
			get{return _fworkload;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Secretgrade
		{
			set{ _secretgrade=value;}
			get{return _secretgrade;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Cuegrade
		{
			set{ _cuegrade=value;}
			get{return _cuegrade;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDoctorItem
		{
			set{ _isdoctoritem=value;}
			get{return _isdoctoritem;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IschargeItem
		{
			set{ _ischargeitem=value;}
			get{return _ischargeitem;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? HisDispOrder
		{
			set{ _hisdisporder=value;}
			get{return _hisdisporder;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SuperGroupNo
		{
			set{ _supergroupno=value;}
			get{return _supergroupno;}
		}
        /// <summary>
        /// 
        /// </summary>
        public int? IsNurseItem
        {
            set { _isnurseitem = value; }
            get { return _isnurseitem; }
        }
		#endregion Model

	}
}

