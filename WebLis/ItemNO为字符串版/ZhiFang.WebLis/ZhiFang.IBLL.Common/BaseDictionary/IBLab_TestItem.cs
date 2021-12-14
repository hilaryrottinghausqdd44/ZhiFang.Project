using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层B_Lab_TestItem
	/// </summary>
	public interface IBLab_TestItem:IBBase<Model.Lab_TestItem>,IBDataPage<Model.Lab_TestItem>
	{
		#region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode, string LabItemNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
        int Delete(string LabCode, string LabItemNo);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        ZhiFang.Model.Lab_TestItem GetModel(string LabCode, string LabItemNo);
		/// <summary>
		/// 根据实体获取DataSet
		/// </summary>
        DataSet GetList(ZhiFang.Model.Lab_TestItem model, string flag);
        int GetTotalCount(ZhiFang.Model.Lab_TestItem model);


		/// <summary>
		/// DataTable转换为List
		/// </summary>
		/// <param name="dt">DataTable</param>
		/// <returns>List</returns>
		List<ZhiFang.Model.Lab_TestItem> DataTableToList(DataTable dt);

        List<ZhiFang.Model.UiModel.ApplyInputItemEntity> ItemEntityDataTableToList(DataTable dt);
        List<ZhiFang.Model.UiModel.ApplyInputItemEntity> ItemEntityDataTableToList(DataTable dt,string labcode);
		#endregion  成员方法

        string GetGroupItemColor(string itemno, string labcode);

        Dictionary<string, int> GetColorTotal(string itemno, string labcode);
        Dictionary<string, string> GetColor(string itemno, string labcode);

        DataSet GetLabTestItemByItemNo(string labCode, string ItemNo);
        int UpdateColor(ZhiFang.Model.Lab_TestItem model);

        DataSet GetListByPage(ZhiFang.Model.Lab_TestItem model, int nowPageNum, int nowPageSize, string sort, string order);
    } 
}