using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBItemGroup : IBGenericManager<LBItemGroup>
    {
        BaseResultDataValue AddDelLBItemGroup(IList<LBItemGroup> addEntityList, string delIDList);

        BaseResultDataValue AddCopyLBItemGroup(long fromGroupItemID, long toGroupItemID);

        IList<LBItemGroup> QueryLBItemGroup(string strHqlWhere, string fields);

        EntityList<LBItemGroup> QueryLBItemGroup(string strHqlWhere, string order, int start, int count, string fields);

    }
}