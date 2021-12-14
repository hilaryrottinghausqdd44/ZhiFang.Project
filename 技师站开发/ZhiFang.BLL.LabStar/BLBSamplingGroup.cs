using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBSamplingGroup : BaseBLL<LBSamplingGroup>, ZhiFang.IBLL.LabStar.IBLBSamplingGroup
    {
        public EntityList<LBSamplingGroup> QueryLBSamplingGroupByFetch(string strHqlWhere, string Order, int start, int count)
        {
            return (this.DBDao as IDLBSamplingGroupDao).QueryLBSamplingGroupByFetchDao(strHqlWhere, Order, start, count);
        }

    }
}