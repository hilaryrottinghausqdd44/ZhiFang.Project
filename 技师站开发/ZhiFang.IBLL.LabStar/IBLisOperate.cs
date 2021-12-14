using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLisOperate : IBGenericManager<LisOperate>
    {

        BaseResultDataValue AddLisOperate(BaseEntity operateObject, BaseClassDicEntity operateTypeEntity, string operateMemo, SysCookieValue sysCookieValue);

        BaseResultDataValue AddLisOperate(BaseEntity operateObject, BaseClassDicEntity operateTypeEntity, string operateMemo, string dataInfo, SysCookieValue sysCookieValue);
        BaseResultDataValue AddLisOperate(BaseEntity operateObject, BaseClassDicEntity operateTypeEntity, string operateMemo, SysCookieValue sysCookieValue, string RelationUser = "",long RelationUserID = 0);

    }
}