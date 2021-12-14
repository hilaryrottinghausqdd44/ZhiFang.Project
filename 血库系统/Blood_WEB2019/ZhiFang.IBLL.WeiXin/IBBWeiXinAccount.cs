using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WeiXin;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.WeiXin
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBWeiXinAccount : IBGenericManager<BWeiXinAccount>
	{
        bool CheckWeiXinAccountByOpenID(string OpenID);

        bool CheckWeiXinAccountByOpenID(string OpenID, out long WeiXinAccountId, out bool LoginInputPasswordFlag,out long? EmpId);

        bool CheckWeiXinAccountByMobileCode(string MobileCode);

        bool CheckWeiXinAccountMobileCodeByOpenID(string OpenID, out long WeiXinAccountId, out bool LoginInputPasswordFlag);

        bool UpdateBWeiXinAccountMobileCodeByOpenid(string MobileCode, string Openid, out long WeiXinAccountId);

        bool ChangePwd(string OldPwd, string NewPwd, string OpenId);

        bool ChangeLoginPasswordFlag(bool Flag, string WeiXinOpenID);

        bool UpdateContent();

        IList<BWeiXinAccount> TakeAll();

        void PushWeiXinMessage(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, List<long> receiveidlist, TemplateIdObject5 entity, string syscode,string url);
        void PushWeiXinMessage(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, List<long> receiveidlist, Dictionary<string, TemplateDataObject> pdata, string syscode, string url);

    }
}