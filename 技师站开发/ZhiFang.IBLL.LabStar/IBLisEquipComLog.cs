using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLisEquipComLog : IBGenericManager<LisEquipComLog>
    {
        BaseResultDataValue AddLisEquipComLog(int comLogType, string comLogInfo, ClientComputerInfo computerInfo);
    }
}