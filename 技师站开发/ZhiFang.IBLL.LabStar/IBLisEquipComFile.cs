using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;
using ZhiFang.Entity.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLisEquipComFile : IBGenericManager<LisEquipComFile>
    {
        BaseResultDataValue AddLisEquipComFile(string equipResultType, string equipResultInfo, int equipResultCount, ClientComputerInfo computerInfo);
    }
}