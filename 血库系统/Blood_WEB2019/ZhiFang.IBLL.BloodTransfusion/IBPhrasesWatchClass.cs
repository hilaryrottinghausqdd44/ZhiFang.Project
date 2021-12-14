

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public interface IBPhrasesWatchClass : IBGenericManager<PhrasesWatchClass>
    {
        /// <summary>
        /// 根据PhrasesWatchClassID查询机构信息单列树
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>BaseResultTree</returns>
        BaseResultTree SearchPhrasesWatchClassTreeById(long id);
        /// <summary>
        /// 根据PhrasesWatchClassID查询机构信息列表树
        /// </summary>
        /// <param name="id">机构ID</param>
        /// 机构ID等于0时 查询所有机构信息
        /// <returns>BaseResultTree</returns>
        BaseResultTree<PhrasesWatchClass> SearchPhrasesWatchClassListTreeById(long id);
        /// <summary>
        /// (可获取机构子孙节点)机构信息列表数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="isSearchChild"></param>
        /// <returns></returns>
        EntityList<PhrasesWatchClass> SearchPhrasesWatchClassAndChildListByHQL(string where, string sort, int page, int limit, bool isSearchChild);
    }
}