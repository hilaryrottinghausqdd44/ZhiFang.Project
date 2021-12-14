using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.LabStar
{
    public interface IDLBItemCalcDao : IDBaseDao<LBItemCalc, long>
    {
        EntityList<LBItemCalc> QueryLBItemCalcDao(string strHqlWhere, string Order, int start, int count);

    }
}