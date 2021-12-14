using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBSamplingGroup : IBGenericManager<LBSamplingGroup>
    {
        EntityList<LBSamplingGroup> QueryLBSamplingGroupByFetch(string strHqlWhere, string Order, int start, int count);

    }
}