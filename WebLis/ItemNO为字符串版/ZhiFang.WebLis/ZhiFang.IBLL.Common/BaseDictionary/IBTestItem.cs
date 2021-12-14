using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层B_TestItem
	/// </summary>
    public interface IBTestItem :IBBase<Model.TestItem>,IBDataPage<Model.TestItem>
	{
		#region  成员方法

        bool Exists(string ItemNo);        
		/// <summary>
		/// 删除一条数据
		/// </summary>
        int Delete(string ItemNo);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.TestItem GetModel(string ItemNo);
		/// <summary>
		/// 根据实体获取DataSet
		/// </summary>
        DataSet GetList(ZhiFang.Model.TestItem model, string flag);
        int GetTotalCount();

        /// <summary>
        /// DataTable转换为List
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>List</returns>
        List<ZhiFang.Model.TestItem> DataTableToList(DataTable dt);

        List<ZhiFang.Model.UiModel.ApplyInputItemEntity> ItemEntityDataTableToList(DataTable dt);
		#endregion  成员方法

        DataSet GetListLike(Model.TestItem ti_m);        

        DataSet GetList(int p, int PageIndex, Model.TestItem testItem);

        int GetListCount(global::ZhiFang.Common.Dictionary.TestItemSuperGroupClass globalZhiFangCommonDictionaryTestItemSuperGroupClass);

        int GetListCount(Model.TestItem testItem);
        DataSet getItemCName(string ItemNo);

        //DataSet getTestItemByCombiItem(string superGroup);

        /// <summary>
        /// 根据检验项目ID查询检验明细
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        //DataSet getItemDetailByItemId(string itemId);

        int Add(List<ZhiFang.Model.TestItem> modelList);
        int Update(List<Model.TestItem> modelList);
    } 
}