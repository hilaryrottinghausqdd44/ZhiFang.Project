using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBSampleItem : IBGenericManager<LBSampleItem>
    {
        /// <summary>
        /// 按选择的样本类型或按选择的检验项目,新增LB_SampleItem
        /// </summary>
        /// <param name="entityList">包括已经存在的关系及需要新增保存的关系</param>
        /// <param name="ofType">按样本类型设置:of_sampletype;按检验项目设置:of_testitem;</param>
        /// <param name="isHasDel">是否需要删除处理不存在当前集合的关系</param>
        /// <returns></returns>
        BaseResultDataValue AddLBSampleItemList(IList<LBSampleItem> entityList, string ofType, bool isHasDel);

    }
}