using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;


namespace ZhiFang.Digitlab.IDAO
{	
	public interface IDRBACRoleModuleDao : IDBaseDao<ZhiFang.Digitlab.Entity.RBACRoleModule, long>
	{
        //返回用户角色与模块关系表
        IList<RBACRoleModule> GetRBACRoleModuleByRBACUserCodeAndModuleID(string strUserCode, long longModuleID);

        bool DeleteByRBACModuleId(long p);
    } 
}