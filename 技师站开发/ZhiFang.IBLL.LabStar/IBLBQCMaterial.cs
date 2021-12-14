using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBQCMaterial : IBGenericManager<LBQCMaterial>
    {
        EntityList<LBQCMaterial> QueryLBQCMaterial(string strHqlWhere, string Order, int start, int count);

        BaseResultTree GetEquipMaterialTree(long equipID, long matID);

        BaseResultDataValue AddCopyLBQCItemByMatID(long fromMatID, long toMatID);

        List<LBQCMaterial> SearchQCMaterialbySectionEquip(long sectionId);
    }
}