using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDLab_PGroup	
	/// </summary>
	public interface IDLab_PGroup:IDataBase<ZhiFang.Model.Lab_PGroup>,IDataPage<ZhiFang.Model.Lab_PGroup>
	{
		#region  成员方法
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode,int LabSectionNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string LabCode,int LabSectionNo);
		
				int DeleteList(string SectionIDlist );
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Lab_PGroup GetModel(string LabCode,int LabSectionNo);
		
		
		DataSet GetListByLike(ZhiFang.Model.Lab_PGroup model);
		#endregion  成员方法
	} 
}