
using System;

namespace ZhiFang.Entity.WebAssist.GKBarcode
{
	/// <summary>
	/// User:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class User
	{
		public User()
		{}
		#region Model
		private int _userid;
		private string _username;
		private string _password;
		private int? _operatetypeno;
		private string _querycode;
		private string _autoreceivetech;
		/// <summary>
		/// 
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PassWord
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? OperateTypeNo
		{
			set{ _operatetypeno=value;}
			get{return _operatetypeno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string QueryCode
		{
			set{ _querycode=value;}
			get{return _querycode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AutoReceiveTech
		{
			set{ _autoreceivetech=value;}
			get{return _autoreceivetech;}
		}
		#endregion Model

	}
}

