using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBLab_SampleType	
	/// </summary>
	public interface IBLab_SampleType :IBBase<ZhiFang.Model.Lab_SampleType>,IBDataPage<ZhiFang.Model.Lab_SampleType>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode,int LabSampleTypeNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string LabCode,int LabSampleTypeNo);
				
		int DeleteList(string SampleTypeIDlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Lab_SampleType GetModel(string LabCode,int LabSampleTypeNo);

        /// <summary>
        /// DataTable转换为List
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>List</returns>
        List<ZhiFang.Model.Lab_SampleType> DataTableToList(DataTable dt);		


		/// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.Lab_SampleType model);
        DataSet GetListByLike(ZhiFang.Model.Lab_SampleType model);
		#endregion  成员方法
	} 
}