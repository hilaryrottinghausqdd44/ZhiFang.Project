using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.LabStar
{
    public interface IDLBSamplingItemDao : IDBaseDao<LBSamplingItem, long>
    {
        EntityList<LBSamplingItem> QueryLBSamplingItemByFetchDao(string strHqlWhere, string Order, int start, int count);

        IList<LBSamplingGroup> QuerySamplingGroupIsMultiItemDao(string strHqlWhere, bool isMulti);

        IList<LBItem> QueryItemIsMultiSamplingGroupDao(string strHqlWhere, string strSectionID, bool isMulti);

        IList<LBItem> QueryItemNoSamplingGroupDao(string strHqlWhere, string strSectionID);

    }
}