using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBHostTypeUser : IBGenericManager<BHostTypeUser>
    {
        BaseResultDataValue copyBHostTypeUserByEmpId(long pasteuser, string Copyusers);
    }
}