using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Model;

namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBLab_SickType	
	/// </summary>
	public interface IBLab_SickType :IBBase<ZhiFang.Model.Lab_SickType>,IBDataPage<ZhiFang.Model.Lab_SickType>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode,int LabSickTypeNo);
        bool Exists(System.Collections.Hashtable ht);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string LabCode,int LabSickTypeNo);


        /// <summary>
        /// DataTable转换为List
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>List</returns>
        List<ZhiFang.Model.Lab_SickType > DataTableToList(DataTable dt);
		int DeleteList(string SickTypeIDlist );
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Lab_SickType GetModel(string LabCode,int LabSickTypeNo);
		
        DataSet GetListByLike(ZhiFang.Model.Lab_SickType model);
        
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
        BaseResult CopyToLab(List<string> itemNos, List<string> labCodeNo, string fromLabCodeNo);
        #endregion  成员方法
    } 
}