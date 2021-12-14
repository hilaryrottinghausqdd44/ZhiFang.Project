using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.LabStar
{
    public interface IDLBRightDao : IDBaseDao<LBRight, long>
    {
        EntityList<LBSection> QueryCommoSectionByEmpIDDao(long[] arrayEmpID);
    }
}