using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBSectionItem : IBGenericManager<LBSectionItem>
    {
        BaseResultDataValue AddDelLBSectionItem(IList<LBSectionItem> addEntityList, bool isCheckEntityExist, string delIDList);

        BaseResultDataValue DeleteLBSectionItem(long sectionItemID);

        BaseResultDataValue DeleteLBSectionItem(string sectionItemIDList);

        BaseResultDataValue AddCopyLBSectionItem(long fromSectionID, long toSectionID);

        EntityList<LBSectionItemVO> QueryLBSectionItemVO(string strHqlWhere, string Order, int start, int count);

        EntityList<LBSectionSingleItemVO> QueryLBSectionDefultSingleItemVO(long sectionID);

    }
}