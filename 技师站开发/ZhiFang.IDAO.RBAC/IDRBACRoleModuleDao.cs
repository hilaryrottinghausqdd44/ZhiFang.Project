using System.Collections.Generic;
using ZhiFang.Entity.RBAC;


namespace ZhiFang.IDAO.RBAC
{
    public interface IDRBACRoleModuleDao : ZhiFang.IDAO.Base.IDBaseDao<RBACRoleModule, long>
    {
        //返回用户角色与模块关系表
        IList<RBACRoleModule> GetRBACRoleModuleByRBACUserCodeAndModuleID(string strUserCode, long longModuleID);

        bool DeleteByRBACModuleId(long p);
    }
}