using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace ZhiFang.IBLL.Report
{	/// <summary>
	/// 业务逻辑类TestItem 的摘要说明。
	/// </summary>
    public interface IBTestItem : IBLLBase<Model.TestItem>
	{        
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string ItemNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string ItemNo);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.TestItem GetModel(string ItemNo);

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        Model.TestItem GetModelByCache(string ItemNo);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetListLike(Model.TestItem model);
	}
}

