using System;
using System.Data;
namespace ZhiFang.IDAL
{
	/// <summary>
	/// 接口层N_News
	/// </summary>
	public interface IDNNews
    {
		#region  成员方法
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool  Add(Model.N_News model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Model.N_News model);
		/// <summary>
		/// 删除数据
		/// </summary>
		bool Delete(long FileID);
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Model.N_News GetModel(long FileID);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);
        DataSet GetList(string where, int page, int limit, string Sort);
        DataSet GetList(string where, int page, int limit, string Sort, string EmpId);
        
        int GetCount(string where);
        bool NNewsApproval(string idlist, bool approvalFlag, string Memo, string empid, string empname);
        bool NNewsPublish(string idlist, bool publishFlag, string Memo, string empid, string empname);
        #endregion  成员方法
    } 
}