using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDrdType	
	/// </summary>
    public interface IDWardType : IDataBase<ZhiFang.Model.WardType>, IDataPage<ZhiFang.Model.WardType>
	{
		#region  成员方法
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int DistrictNo,int WardNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(int DistrictNo,int WardNo);
		
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.WardType GetModel(int DistrictNo,int WardNo);

        DataSet GetListLike(ZhiFang.Model.WardType model);
      
		#endregion  成员方法
	} 
}