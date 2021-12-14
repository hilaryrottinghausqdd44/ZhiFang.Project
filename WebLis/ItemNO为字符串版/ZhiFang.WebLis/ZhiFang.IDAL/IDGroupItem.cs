using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层B_GroupItem
	/// </summary>
	public interface IDGroupItem:IDataBase<Model.GroupItem>
	{
		#region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string PItemNo, string ItemNo);
        bool Exists(System.Collections.Hashtable ht);
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
        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        int AddByDataRow(DataRow dr);
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        int UpdateByDataRow(DataRow dr);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        #endregion  成员方法
    } 
}