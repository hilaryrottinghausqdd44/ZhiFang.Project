using System;
using System.Data;
using ZhiFang.Entity.WebAssist;

namespace ZhiFang.IDAO.NHB.WebAssist
{
	/// <summary>
	/// 接口层ME_GroupSampleItem
	/// </summary>
	public interface IDMEGroupSampleItemDao_SQL
    {
        /// <summary>
		/// 增加一条数据
		/// </summary>
		bool Insert(MEGroupSampleItem model);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool AddBySql(MEGroupSampleItem model);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool AddByParameter(MEGroupSampleItem model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(MEGroupSampleItem model);
		
	} 
}
