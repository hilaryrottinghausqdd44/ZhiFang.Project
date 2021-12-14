using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDBUSINESSMAN	
	/// </summary>
	public interface IDBUSINESSMAN:IDataBase<ZhiFang.Model.BUSINESSMAN>,IDataPage<ZhiFang.Model.BUSINESSMAN>
	{
		#region  成员方法
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int BMANNO);
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
		int Delete(int BMANNO);
		
		int DeleteList(string BNANIDlist );
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.BUSINESSMAN GetModel(int BMANNO);

        DataSet GetListLike(ZhiFang.Model.BUSINESSMAN model);   
		
		#endregion  成员方法
	} 
}