using System;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.LIIP;
using ZhiFang.Entity.LIIP;
using ZhiFang.BLL.Base;
using ZhiFang.IBLL.LIIP;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.BLL.LIIP
{
	/// <summary>
	///
	/// </summary>
	public class BWXWeiXinAccount : BaseBLL<WXWeiXinAccount>, IBWXWeiXinAccount
	{
		IDAO.LIIP.IDWXWeiXinEmpLinkDao IDWXWeiXinEmpLinkDao { get; set; }

		IDAO.RBAC.IDRBACUserDao IDRBACUserDao { get; set; }

		public bool AddEmpLink(string empid, string openid)
		{
			var tmplist = DBDao.GetListByHQL($" OpenID='{openid}' AND SourceTypeID={WXSourceType.小程序.Key} ");
			if (tmplist == null || tmplist.Count <= 0)
			{
				ZhiFang.Common.Log.Log.Debug($" 未能找到相应的微信账户!OpenID={openid},SourceTypeID={WXSourceType.小程序.Value.Name} ");
				throw new Exception("未能找到相应的微信账户！");
			}
			WXWeiXinEmpLink weixinemplink = new WXWeiXinEmpLink();
			weixinemplink.EmpID = long.Parse(empid);
			weixinemplink.WeiXinAccountID = tmplist.First().Id;
			weixinemplink.DataAddTime = DateTime.Now;
			return IDWXWeiXinEmpLinkDao.Save(weixinemplink);
		}

		public bool AddEntity(WXWeiXinAccount entity)
		{
			return DBDao.Save(entity);
		}

		public int CheckOpenIdAndSourceTypeID(string openid, string key)
		{
			var tmplist=DBDao.GetListByHQL($" OpenID='{openid}' AND SourceTypeID={key} ");
			if (tmplist == null || tmplist.Count <= 0)
			{
				return 0;
			}
			var linklist = IDWXWeiXinEmpLinkDao.GetListByHQL($" WeiXinAccountID={tmplist.First().Id}  ");
			if (linklist == null || linklist.Count <= 0)
			{
				return 1;
			}
			else
			{
				return 2;
			}
		}

		public bool CheckWeiXinAccountByWeiXinMiniOpenID(string weiXinMiniOpenID,string type="1")
		{
			var tmplist = DBDao.GetListByHQL($" OpenID='{weiXinMiniOpenID}' and SourceTypeID={type} ");
			return tmplist != null && tmplist.Count > 0;
		}

		public RBACUser GetRbacUserByOpenid(string openid)
		{
			var tmplist = DBDao.GetListByHQL($" OpenID='{openid}' AND SourceTypeID={WXSourceType.小程序.Key} ");
			if (tmplist == null || tmplist.Count <= 0)
			{
				ZhiFang.Common.Log.Log.Debug($" 未能找到相应的微信账户!OpenID={openid},SourceTypeID={WXSourceType.小程序.Value.Name} ");
				throw new Exception("未能找到相应的微信账户！");
			}
			var weixinemplinklist= IDWXWeiXinEmpLinkDao.GetListByHQL($" WeiXinAccountID={tmplist.First().Id} ");
			if (weixinemplinklist == null || weixinemplinklist.Count <= 0)
			{
				ZhiFang.Common.Log.Log.Error($"微信账户没有关联关系！ OpenID='{openid}'");
				throw new Exception("微信账户没有关联关系！ ");
			}
			WXWeiXinEmpLink weixinemplink = weixinemplinklist.First();
			var rbacuserlist = IDRBACUserDao.GetListByHQL(" EmpID=" + weixinemplink.EmpID);
			if (weixinemplinklist == null || weixinemplinklist.Count <= 0)
			{
				ZhiFang.Common.Log.Log.Error($"员工没有相关账户！ EmpID='{weixinemplink.EmpID}'");
				throw new Exception("账户异常！ ");
			}
			return rbacuserlist.First();
		}
	}
}