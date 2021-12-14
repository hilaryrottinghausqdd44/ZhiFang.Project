using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.LabStar
{
    public interface IDLBQCItemDao : IDBaseDao<LBQCItem, long>
    {
        EntityList<LBQCItem> QueryLBQCItemDao(string strHqlWhere, string Order, int start, int count);

    }
}