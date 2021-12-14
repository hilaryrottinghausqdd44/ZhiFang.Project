
using System;

namespace ZhiFang.Entity.WebAssist.GKBarcode
{
	/// <summary>
	/// FeeSetUp:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class FeeSetUp
	{
		public FeeSetUp()
		{}
		#region Model
		private int? _testtypeid;
		private string _informationno;
		private string _informationtext;
		private string _feeset;
		/// <summary>
		/// 
		/// </summary>
		public int? TestTypeID
		{
			set{ _testtypeid=value;}
			get{return _testtypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string InformationNo
		{
			set{ _informationno=value;}
			get{return _informationno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string InformationText
		{
			set{ _informationtext=value;}
			get{return _informationtext;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FeeSet
		{
			set{ _feeset=value;}
			get{return _feeset;}
		}
		#endregion Model

	}
}

