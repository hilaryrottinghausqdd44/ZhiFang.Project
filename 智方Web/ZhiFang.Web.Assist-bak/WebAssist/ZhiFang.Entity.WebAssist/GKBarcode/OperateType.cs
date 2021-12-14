
using System;

namespace ZhiFang.Entity.WebAssist.GKBarcode
{
	/// <summary>
	/// OperateType:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class OperateType
	{
		public OperateType()
		{}
		#region Model
		private int _operatetypeno;
		private string _operatetext;
		private string _enabledcontrol;
		/// <summary>
		/// 
		/// </summary>
		public int OperateTypeNo
		{
			set{ _operatetypeno=value;}
			get{return _operatetypeno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperateText
		{
			set{ _operatetext=value;}
			get{return _operatetext;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EnabledControl
		{
			set{ _enabledcontrol=value;}
			get{return _enabledcontrol;}
		}
		#endregion Model

	}
}

