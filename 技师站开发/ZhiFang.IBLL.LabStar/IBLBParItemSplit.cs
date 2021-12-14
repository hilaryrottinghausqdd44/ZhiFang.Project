using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBParItemSplit : IBGenericManager<LBParItemSplit>
    {
        /// <summary>
        /// 新增组合项目拆分时,根据组合项目ID获取到该组合项目所有子项目及子项目的所属采样组信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="parItemId">组合项目ID</param>
        /// <returns></returns>
        EntityList<LBParItemSplit> SearchAddLBParItemSplitListByParItemId(string where, string sort, int page, int limit, string parItemId);
        /// <summary>
        /// 获取某一组合项目编辑的组合项目拆分信息(HQL)
        /// </summary>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<LBParItemSplit> SearchEditLBParItemSplitByHQL(string where, string order, int page, int limit);
        /// <summary>
        /// 定制新增保存组合项目拆分
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        BaseResultDataValue AddLBParItemSplitList(IList<LBParItemSplit> entityList);

        /// <summary>
        /// 定制编辑保存组合项目拆分
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        BaseResultBool EditLBParItemSplitList(IList<LBParItemSplit> entityList);

        /// <summary>
        /// 定制按组合项目ID删除组合项目拆分关系
        /// </summary>
        /// <param name="parItemId">组合项目Id</param>
        /// <returns></returns>
        BaseResultBool DelLBParItemSplitByParItemId(long parItemId);

    }
}