using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.OA;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.OA
{ 
    ///
    /// </summary>
public  interface IBBWeiXinEmpLink : IBGenericManager<BWeiXinEmpLink>
	{
        bool AddByUserAccountOpenID(string strUserAccount, string strPassWord, string OpenID, out string ErrorInfo, out ZhiFang.Entity.RBAC.HREmployee emp);
    }
}