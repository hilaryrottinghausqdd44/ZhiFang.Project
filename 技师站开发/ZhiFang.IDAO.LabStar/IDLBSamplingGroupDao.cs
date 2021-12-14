using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.LabStar
{
    public interface IDLBSamplingGroupDao : IDBaseDao<LBSamplingGroup, long>
    {
        EntityList<LBSamplingGroup> QueryLBSamplingGroupByFetchDao(string strHqlWhere, string Order, int start, int count);

    }
}