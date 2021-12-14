using ZhiFang.IDAO.Base;
using ZhiFang.Entity.WebAssist;
using System.Data;
using ZhiFang.Entity.WebAssist.GKBarcode;
using System.Collections.Generic;

namespace ZhiFang.IDAO.NHB.WebAssist
{
	/// <summary>
	/// 接口层FeeSetUp
	/// </summary>
	public interface IFeeSetUp_SQL
	{
		#region  成员方法
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		FeeSetUp GetModel();
		FeeSetUp DataRowToModel(DataRow row);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);
		int GetRecordCount(string strWhere);
		DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
		#region  MethodEx
		IList<FeeSetUp> GetListByHQL(string strSqlWhere);
		#endregion  MethodEx
	} 
}
