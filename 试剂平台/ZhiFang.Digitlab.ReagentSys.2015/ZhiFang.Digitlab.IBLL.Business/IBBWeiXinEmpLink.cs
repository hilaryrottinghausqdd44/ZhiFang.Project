using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IBLL.Business
{ 
    ///
    /// </summary>
public  interface IBBWeiXinEmpLink : IBGenericManager<BWeiXinEmpLink>
	{
    bool AddByUserAccountOpenID(string strUserAccount, string strPassWord, string OpenID, out string ErrorInfo, out ZhiFang.Digitlab.Entity.HREmployee emp);
    }
}