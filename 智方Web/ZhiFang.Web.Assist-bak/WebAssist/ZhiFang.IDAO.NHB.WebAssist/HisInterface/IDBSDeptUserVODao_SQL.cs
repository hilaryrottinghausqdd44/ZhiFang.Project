using System;
using System.Collections.Generic;
using System.Data;
using ZhiFang.Entity.WebAssist.HisInterface;

namespace ZhiFang.IDAO.NHB.WebAssist
{
	/// <summary>
	/// 百色市人民医院科室人员关系接口层
	/// </summary>
	public interface IDBSDeptUserVODao_SQL
	{
		#region  成员方法
		IList<BSDeptUserVO> GetListByHQL(string strSqlWhere);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		BSDeptUserVO GetModel();
		BSDeptUserVO DataRowToModel(DataRow row);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		DataSet GetList(int Top, string strWhere, string filedOrder);
		int GetRecordCount(string strWhere);
		DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
	}
}
