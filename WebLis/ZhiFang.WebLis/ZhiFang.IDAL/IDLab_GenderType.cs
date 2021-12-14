using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDLab_GenderType	
	/// </summary>
	public interface IDLab_GenderType:IDataBase<ZhiFang.Model.Lab_GenderType>,IDataPage<ZhiFang.Model.Lab_GenderType>
	{
		#region  成员方法
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode,int LabGenderNo);
        bool Exists(System.Collections.Hashtable ht);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string LabCode,int LabGenderNo);
		
				int DeleteList(string GenderIDlist );
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Lab_GenderType GetModel(string LabCode,int LabGenderNo);
				
		DataSet GetListByLike(ZhiFang.Model.Lab_GenderType model);
		
		/// <summary>
        /// 数据同步时增加数据
        /// </summary>
        int AddByDataRow(DataRow dr);
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        int UpdateByDataRow(DataRow dr);
        /// <summary>
        /// 根据实验室字典性别的名称获取实验室性别的编码
        /// </summary>
        /// <param name="LabCode">送检单位</param>
        /// <param name="LabCname">性别的名称</param>
        /// <returns>性别的编码</returns>
        DataSet GetLabCodeNo(string LabCode, List<string> LabCname);
		#endregion  成员方法
	} 
}