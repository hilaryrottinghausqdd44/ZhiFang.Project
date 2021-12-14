using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层B_Lab_GroupItem
	/// </summary>
	public interface IDLab_GroupItem:IDataBase<Model.Lab_GroupItem>
	{
		#region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string PItemNo, string ItemNo, string LabCode);
        bool Exists(System.Collections.Hashtable ht);
		/// <summary>
		/// 删除一条数据
		/// </summary>
        int Delete(string PItemNo, string ItemNo, string LabCode);
        int Delete(ZhiFang.Model.Lab_GroupItem model, string flag);
        int DeleteAll(string LabCode);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        ZhiFang.Model.Lab_GroupItem GetModel(string PItemNo, string ItemNo, string LabCode);
        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        int AddByDataRow(DataRow dr);
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        int UpdateByDataRow(DataRow dr);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        #endregion  成员方法

        DataSet GetPitemList(ZhiFang.Model.Lab_GroupItem model);
    } 
}