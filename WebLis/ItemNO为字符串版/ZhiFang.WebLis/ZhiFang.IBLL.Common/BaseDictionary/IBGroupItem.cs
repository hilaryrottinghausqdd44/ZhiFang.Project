using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层B_GroupItem
	/// </summary>
	public interface IBGroupItem:IBBase<Model.GroupItem>
	{
		#region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string PItemNo, string ItemNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
        int Delete(string PItemNo, string ItemNo);
        int Delete(ZhiFang.Model.GroupItem model, string flag);
        int DeleteAll();
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        ZhiFang.Model.GroupItem GetModel(string PItemNo, string ItemNo);
		
		#endregion  成员方法
        DataSet GetGroupItemList(string p);
        /// <summary>
        /// 获取最底层检验项目No和CName
        /// </summary>
        /// <param name="p">组套项目No</param>
        /// <returns></returns>
        string GetSubItemList_No_CName(string p);
        /// <summary>
        /// 获取检验项目No
        /// </summary>
        /// <param name="p">组套项目No</param>
        /// <returns></returns>
        string GetSubItemList_No(string p);

        List<ZhiFang.Model.GroupItem> DataTableToList(DataTable dt);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
    } 
}