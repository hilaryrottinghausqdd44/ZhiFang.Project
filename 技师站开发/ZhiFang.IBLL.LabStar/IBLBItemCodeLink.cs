using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBItemCodeLink : IBGenericManager<LBItemCodeLink>
    {
        EntityList<LBItemCodeLinkVO> LS_UDTO_SearchLBItemAndLBItemCodeLink(long SectionID, int GroupType, long SickTypeID, string ItemCName, string sort, int page, int limit);
        List<LBItem> LS_UDTO_SearchLBItemAndLBItemBySickTypeID(long SectionID, int GroupType, long SickTypeID, string ItemCName);
        BaseResultDataValue AddCopyLBItemCodeLinkContrast(string sickTypeIds, string thisSickTypeId);
    }
}