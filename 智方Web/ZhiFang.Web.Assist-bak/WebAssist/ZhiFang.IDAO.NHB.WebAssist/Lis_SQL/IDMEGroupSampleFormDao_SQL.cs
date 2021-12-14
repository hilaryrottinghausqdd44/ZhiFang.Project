using System;
using System.Data;
using ZhiFang.Entity.WebAssist;

namespace ZhiFang.IDAO.NHB.WebAssist
{
	/// <summary>
	/// 接口层ME_GroupSampleForm
	/// </summary>
	public interface IDMEGroupSampleFormDao_SQL
	{
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Insert(MEGroupSampleForm model);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool AddBySql(MEGroupSampleForm model);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool AddByParameter(MEGroupSampleForm model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(MEGroupSampleForm model);
		
	} 
}
