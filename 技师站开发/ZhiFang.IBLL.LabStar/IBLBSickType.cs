using System.Data;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBSickType : IBGenericManager<LBSickType>
    {
        DataTable LS_UDTO_SearchLBSickTypeDicContrastNumByHQL(long SectionID, int GroupType, string CName);
        DataTable LS_UDTO_SearchLBSickTypeOtherDicContrastNum(string type);
    }
}