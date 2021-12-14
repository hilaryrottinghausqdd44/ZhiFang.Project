using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDFolkType	
	/// </summary>
	public interface IDFolkType:IDataBase<ZhiFang.Model.FolkType>,IDataPage<ZhiFang.Model.FolkType>
	{
		#region  成员方法
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int FolkNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
        int Delete(int FolkNo);

        int DeleteList(string FolkIDlist);
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.FolkType GetModel(int FolkNo);
		
		
		#endregion  成员方法
	} 
}