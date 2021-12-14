

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity;

namespace ZhiFang.WeiXin.IBLL
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBWeiXinAccount : ZhiFang.IBLL.Base.IBGenericManager<BWeiXinAccount>
	{
        bool CheckWeiXinAccountByOpenID(string OpenID);

        bool CheckWeiXinAccountByMobileCode(string MobileCode);

        bool CheckWeiXinAccountMobileCodeByOpenID(string OpenID, out long WeiXinAccountId,out bool LoginInputPasswordFlag);

        bool CheckWeiXinAccountMobileCodeByOpenID(string OpenID, out BWeiXinAccount BWeiXinAccount, out bool LoginInputPasswordFlag);

        bool UpdateBWeiXinAccountMobileCodeByOpenid(string MobileCode, string Openid, out long WeiXinAccountId);

        bool ChangePwd(string OldPwd, string NewPwd, string OpenId);

        bool ChangeLoginPasswordFlag(bool Flag, string WeiXinOpenID);

        bool UpdateContent();

        IList<BWeiXinAccount> TakeAll();
        void PushWeiXinMessage(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, List<long> receiveidlist, TemplateIdObject5 entity, string syscode, string url);
        void PushWeiXinMessage(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, List<long> receiveidlist, Dictionary<string, TemplateDataObject> pdata, string syscode, string url);
        void PushWeiXinMessage(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, List<string> receiveopenidlist, Dictionary<string, TemplateDataObject> pdata, string syscode, string url);
        BaseResultDataValue WeiXinAccountBind(string id, string accountCode, string password);
        BaseResultDataValue DoctorAccountBindWeiXinAccountChange(long id, string accountCode, string password);
        bool UserReadAgreement(string weiXinUserID);
        BaseResultDataValue WeiXinAccountPwdRest(string weiXinAccount, string empIdStr, string empName);
        EntityList<BWeiXinAccount> WXADS_SearchWeiXinAccount_User(string where, string sort, int page, int limit);
    }
}