using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.LabStar
{
    public interface IDLBQCMaterialDao : IDBaseDao<LBQCMaterial, long>
    {
        EntityList<LBQCMaterial> QueryLBQCMaterialDao(string strHqlWhere, string Order, int start, int count);
    }
}