using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层B_SuperGroup
	/// </summary>
    public interface IDSuperGroup : IDataBase<Model.SuperGroup>, IDataPage<Model.SuperGroup>
	{
		#region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int SuperGroupNo);
        bool Exists(System.Collections.Hashtable ht);
        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        int AddByDataRow(DataRow dr);
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        int UpdateByDataRow(DataRow dr);
		/// <summary>
		/// 删除一条数据
		/// </summary>
        int Delete(int Id);
        int DeleteList(string Idlist);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.SuperGroup GetModel(int Id);
        
		#endregion  成员方法

        DataSet GetParentSuperGroupNolist();

       //System.Collections.Generic.List<Model.SuperGroup> GetListModel();
    } 
}