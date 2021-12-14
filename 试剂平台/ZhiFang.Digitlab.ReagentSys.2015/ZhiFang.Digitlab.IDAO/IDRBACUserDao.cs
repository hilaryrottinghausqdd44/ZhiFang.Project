using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;


namespace ZhiFang.Digitlab.IDAO
{	
	public interface IDRBACUserDao : IDBaseDao<ZhiFang.Digitlab.Entity.RBACUser, long>
	{
        IList<RBACUser> GetByHQL(ZhiFang.Digitlab.Entity.RBACUser entity);
        /// <summary>
        /// 根据用户帐号查询账户信息
        /// </summary>
        /// <param name="strUserAccount">用户账号</param>
        /// <returns>IList<RBACUser></returns>
        IList<RBACUser> SearchRBACUserByUserAccount(string strUserAccount);

	} 
}