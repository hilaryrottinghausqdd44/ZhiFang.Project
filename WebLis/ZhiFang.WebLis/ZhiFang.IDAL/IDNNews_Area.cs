using System;
using System.Data;
namespace ZhiFang.IDAL
{
	/// <summary>
	/// 接口层N_News_Area
	/// </summary>
	public interface IDNNews_Area
    {
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(Model.N_News_Area model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Model.N_News_Area model);
		/// <summary>
		/// 删除数据
		/// </summary>
		bool Delete(long NewsAreaId);
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Model.N_News_Area GetModel(long NewsAreaId);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);
        DataSet GetList(string where, int page, int limit, string Sort = "DispOrder asc ");
        int GetCount(string where);

        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);
        #endregion  成员方法
    } 
}