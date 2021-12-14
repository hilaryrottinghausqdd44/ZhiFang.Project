
using System;

namespace ZhiFang.Entity.WebAssist.GKBarcode
{
	/// <summary>
	/// TestType_Info:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TestTypeInfo
	{
		public TestTypeInfo()
		{}
		#region Model
		private int? _testtypeid;
		private string _infoname;
		private string _infotext;
		private int? _depid;
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
		public string InfoName
		{
			set{ _infoname=value;}
			get{return _infoname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string InfoText
		{
			set{ _infotext=value;}
			get{return _infotext;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DepID
		{
			set{ _depid=value;}
			get{return _depid;}
		}
		#endregion Model

	}
}

