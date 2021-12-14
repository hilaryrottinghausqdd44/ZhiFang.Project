using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.LIIP;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LIIP
{
    /// <summary>
    ///
    /// </summary>
    public interface IBWXWeiXinAccount : IBGenericManager<WXWeiXinAccount>
    {
        /// <summary>
        /// 0未注册，1已注册未绑定，2已注册已绑定
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        int CheckOpenIdAndSourceTypeID(string openid, string key);
        bool AddEntity(WXWeiXinAccount entity);
        bool AddEmpLink(string empid, string openid);
        RBACUser GetRbacUserByOpenid(string openid);
        bool CheckWeiXinAccountByWeiXinMiniOpenID(string weiXinMiniOpenID, string type="1");
    }
}