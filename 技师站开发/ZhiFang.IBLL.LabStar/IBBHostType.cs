using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBHostType : IBGenericManager<BHostType>
    {
        EntityList<BHostType> SearchBHostTypeNotPara(int page, int limit, string where, string sort);
    }
}