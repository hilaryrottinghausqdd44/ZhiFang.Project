using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IBLL.Business
{
    public interface IBItemItemCon : IBGenericManager<ZhiFang.Digitlab.Entity.ItemItemCon>
    {
        /// <summary>
        /// 根据项目ID查询该项目包含的项目关系树
        /// </summary>
        /// <param name="longItemID">项目ID</param>
        /// <returns></returns>
        BaseResultTree SearchItemTreeByItemID(long longItemID);
        /// <summary>
        /// 根据组合项目ID获取其子项目
        /// 如果项目不是组合项目,则返回的列表数量为0
        /// </summary>
        /// <param name="longGroupItemID">组合项目ID</param>
        /// <returns>项目列表</returns>
        IList<ItemAllItem> SearchChildItemByGroupItemID(long longGroupItemID);
    }
}
