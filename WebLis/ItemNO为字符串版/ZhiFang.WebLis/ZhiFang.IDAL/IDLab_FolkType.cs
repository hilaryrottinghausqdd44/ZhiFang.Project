using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDLab_FolkType	
	/// </summary>
	public interface IDLab_FolkType:IDataBase<ZhiFang.Model.Lab_FolkType>,IDataPage<ZhiFang.Model.Lab_FolkType>
	{
		#region  成员方法
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode,int LabFolkNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string LabCode,int LabFolkNo);
		
				int DeleteList(string FolkIDlist );
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Lab_FolkType GetModel(string LabCode,int LabFolkNo);
		
		
		DataSet GetListByLike(ZhiFang.Model.Lab_FolkType model);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        #endregion  成员方法
    } 
}