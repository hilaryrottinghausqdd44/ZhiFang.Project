using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBItemCalc : IBGenericManager<LBItemCalc>
    {
        BaseResultDataValue AddDelLBItemCalc(IList<LBItemCalc> addEntityList, string delIDList);
        EntityList<LBItemCalc> QueryLBItemCalc(string strHqlWhere, string Order, int start, int count);
    }
}