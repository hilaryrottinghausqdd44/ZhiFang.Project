using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.RBAC;


namespace ZhiFang.IDAO.RBAC
{	
	public interface IDRBACUserDao : ZhiFang.IDAO.Base.IDBaseDao<RBACUser, long>
	{
        IList<RBACUser> GetByHQL(RBACUser entity);
        /// <summary>
        /// 根据用户帐号查询账户信息
        /// </summary>
        /// <param name="strUserAccount">用户账号</param>
        /// <returns>IList<RBACUser></returns>
        IList<RBACUser> SearchRBACUserByUserAccount(string strUserAccount);

	} 
}