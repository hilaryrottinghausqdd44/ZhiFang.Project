
using System;

namespace ZhiFang.Entity.WebAssist.GKBarcode
{
	/// <summary>
	/// PYZd:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PYZd
	{
		public PYZd()
		{}
		#region Model
		private string _a1;
		private string _a2;
		/// <summary>
		/// 
		/// </summary>
		public string A1
		{
			set{ _a1=value;}
			get{return _a1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string A2
		{
			set{ _a2=value;}
			get{return _a2;}
		}
		#endregion Model

	}
}

