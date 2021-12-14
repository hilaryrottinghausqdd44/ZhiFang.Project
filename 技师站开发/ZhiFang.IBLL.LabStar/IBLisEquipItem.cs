using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLisEquipItem : IBGenericManager<LisEquipItem>
    {
        IList<LisEquipItem> QueryLisEquipItem(string strHqlWhere, string fields);

        EntityList<LisEquipItem> QueryLisEquipItem(string strHqlWhere, string order, int start, int count, string fields);

        bool QueryIsExistEquipItemResult(long sectionID, long itemID);

    }
}