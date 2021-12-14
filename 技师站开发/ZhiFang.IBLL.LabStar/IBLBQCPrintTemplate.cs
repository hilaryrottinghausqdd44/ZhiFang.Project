using System.Data;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBQCPrintTemplate : IBGenericManager<LBQCPrintTemplate>
    {
        DataTable GetQCTempleModuleList(string name);
    }
}