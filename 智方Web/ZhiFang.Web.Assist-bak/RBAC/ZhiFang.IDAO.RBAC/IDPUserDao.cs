using ZhiFang.IDAO.Base;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.IDAO.RBAC
{
    public interface IDPUserDao : IDBaseDao<PUser, int>
    {
        int GetMaxId();
    }
}