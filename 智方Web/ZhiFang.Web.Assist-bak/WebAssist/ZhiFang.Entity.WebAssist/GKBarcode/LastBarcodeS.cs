
using System;

namespace ZhiFang.Entity.WebAssist.GKBarcode
{
	/// <summary>
	/// LastBarcodeS:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class LastBarcodeS
	{
		public LastBarcodeS()
		{}
		#region Model
		private int _lastbarcodesId;
		private DateTime? _date;
		private DateTime? _time;
		private string _department;
		/// <summary>
		/// 
		/// </summary>
		public int LastBarcodeSId
		{
			set{ _lastbarcodesId=value;}
			get{return _lastbarcodesId; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Date
		{
			set{ _date=value;}
			get{return _date;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Time
		{
			set{ _time=value;}
			get{return _time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Department
		{
			set{ _department=value;}
			get{return _department;}
		}
		#endregion Model

	}
}

