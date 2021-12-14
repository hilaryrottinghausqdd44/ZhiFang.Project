using System;
using System.Data;
namespace ZhiFang.IDAL
{
	/// <summary>
	/// 接口层N_News_Interaction
	/// </summary>
	public interface IDNNews_Interaction
	{
		#region  成员方法
		
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(ZhiFang.Model.N_News_Interaction model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(ZhiFang.Model.N_News_Interaction model);
		/// <summary>
		/// 删除数据
		/// </summary>
		bool Delete(long InteractionID);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.N_News_Interaction GetModel(long InteractionID);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);
        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);
        DataSet GetList(string where, int page, int limit, string Sort = "DispOrder asc ");
        int GetCount(string where);
        #endregion  成员方法
    } 
}