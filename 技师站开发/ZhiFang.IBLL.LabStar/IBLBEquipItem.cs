using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBEquipItem : IBGenericManager<LBEquipItem>
    {
        BaseResultDataValue AddDelLBEquipItem(IList<LBEquipItem> addEntityList, bool isCheckEntityExist, string delIDList);

        BaseResultDataValue DeleteLBEquipItem(long sectionItemID);

        BaseResultDataValue DeleteLBEquipItem(string sectionItemIDList);

        EntityList<LBEquipItem> QueryLBEquipItem(string strHqlWhere, string Order, int start, int count);

        EntityList<LBEquipItemVO> QueryLBEquipItemVO(string strHqlWhere, string Order, int start, int count);

        IList<LBEquipItem> QueryIsExistSectionItem(long sectionID, long itemID);

    }
}