using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
	public interface IDMEGroupSampleItemDao : IDBaseDao<MEGroupSampleItem, long>
	{
        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IList<MEGroupSampleItem> SearchMEGroupSampleItemByHQL(string strHqlWhere, int page, int count);
	} 
}