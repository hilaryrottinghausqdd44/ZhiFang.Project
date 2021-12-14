using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.WeiXin;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.WeiXin
{ 
    ///
    /// </summary>
public  interface IBBWeiXinEmpLink : IBGenericManager<BWeiXinEmpLink>
	{
    bool AddByUserAccountOpenID(string strUserAccount, string strPassWord, string OpenID, out string ErrorInfo, out HREmployee emp);
    }
}