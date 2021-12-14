using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBEquip : IBGenericManager<LBEquip>
    {
        BaseResultDataValue EditLBEquipCommPara(long equipID, string jsonCommPara);

        BaseResultDataValue AddNewLBEquipByLBEquip(long equipID);

        BaseResultDataValue AddCopyLBEquipItemByLBEquip(long fromEquipID, long toEquipID);
    }
}