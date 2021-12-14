using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WeiXin;
using ZhiFang.IDAO.OA;

namespace ZhiFang.BLL.WeiXin
{
    /// <summary>
    ///
    /// </summary>
    public class BBWeiXinAccount : BaseBLL<BWeiXinAccount>, ZhiFang.IBLL.WeiXin.IBBWeiXinAccount
	{
        public IDBWeiXinEmpLinkDao IDBWeiXinEmpLinkDao { get; set; }
        public IDBWeiXinPushMessageTemplateDao IDBWeiXinPushMessageTemplateDao { get; set; }
        public bool CheckWeiXinAccountByOpenID(string OpenID)
        {
            int count = DBDao.GetListCountByHQL(" WeiXinAccount='" + OpenID + "' ");
            if (count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// 根据OpenID判断用户是否有注册
        /// </summary>
        /// <param name="OpenID"></param>
        /// <returns>true有，false没有</returns>
        public bool CheckWeiXinAccountByOpenID(string OpenID, out long WeiXinAccountId, out bool LoginInputPasswordFlag,out long? EmpID)
        {
            WeiXinAccountId = 0;
            LoginInputPasswordFlag = false;
            EmpID = null;
           // var objectlist = DBDao.GetListByHQL(" WeiXinAccount='" + OpenID + "' ");
            IList<BWeiXinAccount> BWeiXinAccountlist = DBDao.GetListByHQL(" WeiXinAccount='" + OpenID + "' ");
            if (BWeiXinAccountlist != null && BWeiXinAccountlist.Count > 0)
            {
                if (BWeiXinAccountlist[0].BWeiXinEmpLinkList != null && BWeiXinAccountlist[0].BWeiXinEmpLinkList.Count > 0)
                {
                    WeiXinAccountId = BWeiXinAccountlist[0].Id;
                    LoginInputPasswordFlag = BWeiXinAccountlist[0].LoginInputPasswordFlag;
                    EmpID = BWeiXinAccountlist[0].BWeiXinEmpLinkList[0].EmpID;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool CheckWeiXinAccountByMobileCode(string MobileCode)
        {
            int count = DBDao.GetListCountByHQL(" MobileCode='" + MobileCode + "' ");
            if (count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// 根据OpenID判断用户是否有手机号
        /// </summary>
        /// <param name="OpenID"></param>
        /// <param name="WeiXinAccountId"></param>
        /// <returns>true有，false没有</returns>
        public bool CheckWeiXinAccountMobileCodeByOpenID(string OpenID, out long WeiXinAccountId, out bool LoginInputPasswordFlag)
        {
            WeiXinAccountId = 0;
            LoginInputPasswordFlag = false;
            var objectlist = DBDao.GetListByHQL(" WeiXinAccount='" + OpenID + "' ");
            if (objectlist.Count > 0)
            {
                WeiXinAccountId = objectlist[0].Id;
                LoginInputPasswordFlag = objectlist[0].LoginInputPasswordFlag;
                string RegeditMobileFlag = ZhiFang.Common.Public.ConfigHelper.GetConfigString("RegeditMobileFlag");
                if (RegeditMobileFlag != null && RegeditMobileFlag != "" && RegeditMobileFlag == "1")
                {
                    if (objectlist[0].MobileCode != null && objectlist[0].MobileCode.Trim() != "")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }       

        public bool UpdateBWeiXinAccountMobileCodeByOpenid(string MobileCode, string Openid, out long WeiXinAccountId)
        {
            bool LoginInputPasswordFlag;
            if (!CheckWeiXinAccountMobileCodeByOpenID(Openid, out WeiXinAccountId, out LoginInputPasswordFlag))
            {
                int i = DBDao.UpdateByHql("update BWeiXinAccount as bwxa set bwxa.MobileCode='" + MobileCode.ToString() + "' where bwxa.WeiXinAccount='" + Openid + "' ");
                if (i > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ChangePwd(string OldPwd, string NewPwd, string OpenId)
        {
            string opwd = SecurityHelp.MD5Encrypt(OldPwd, SecurityHelp.PWDMD5Key);
            List<BWeiXinAccount> ac = this.SearchListByHQL(" PassWord='" + opwd + "' and WeiXinAccount='" + OpenId + "'").ToList();
            if (ac != null && ac.Count > 0)
            {
                string npwd = SecurityHelp.MD5Encrypt(NewPwd, SecurityHelp.PWDMD5Key);
                this.DBDao.UpdateByHql("update BWeiXinAccount as bwxa set bwxa.PassWord='" + npwd + "' where   bwxa.WeiXinAccount='" + OpenId + "'");
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ChangeLoginPasswordFlag(bool Flag, string WeiXinOpenID)
        {
            int flag = this.DBDao.UpdateByHql("update BWeiXinAccount as bwxa set bwxa.LoginInputPasswordFlag='" + Flag.ToString() + "' where   bwxa.WeiXinAccount='" + WeiXinOpenID + "'");
            if (flag > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateContent()
        {
            int flag = this.DBDao.UpdateByHql("update BWeiXinAccount as bwxa set bwxa.UserName='" + this.Entity.UserName + "',bwxa.AddTime='" + this.Entity.AddTime + "',bwxa.SexID=" + this.Entity.SexID + " ,bwxa.ProvinceName='" + this.Entity.ProvinceName + "' ,bwxa.CityName='" + this.Entity.CityName + "' ,bwxa.CountryName='" + this.Entity.CountryName + "',bwxa.Language='" + this.Entity.Language + "' ,bwxa.HeadImgUrl='" + this.Entity.HeadImgUrl + "' where   bwxa.WeiXinAccount='" + this.Entity.WeiXinAccount + "'");
            if (flag > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IList<BWeiXinAccount> TakeAll()
        {
            return base.LoadAll();
        }

        public void PushWeiXinMessage(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, List<long> receiveidlist, TemplateIdObject5 entity, string syscode, string url)
        {
            if (receiveidlist.Count > 0)
            {               
                Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                data.Add("first", entity.first);
                data.Add("keyword1", entity.keyword1);
                data.Add("keyword2", entity.keyword2);
                data.Add("keyword3", entity.keyword3);
                data.Add("keyword4", entity.keyword4);
                data.Add("keyword5", entity.keyword5);
                data.Add("remark", entity.remark);
                PushWeiXinMessage(pushWeiXinMessageAction, receiveidlist, data, syscode, url);
            }
        }

        public void PushWeiXinMessage(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, List<long> receiveidlist, Dictionary<string, TemplateDataObject> pdata, string syscode, string url)
        {
            if (receiveidlist.Count > 0)
            {
                IList<BWeiXinEmpLink> bwxell = IDBWeiXinEmpLinkDao.GetListByHQL(" EmpID in (" + string.Join(",", receiveidlist.ToArray()) + ") ");
                if (bwxell != null && bwxell.Count > 0)
                {
                    string Shortcode = SysWeiXinTemplate.新订单提醒;
                    Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                    data = pdata;
                    switch (syscode.Trim().ToLower())
                    {
                        case "addReaBmsCenOrderDoc": Shortcode = SysWeiXinTemplate.新订单提醒; break;
                        case "bmscenstatuschange": Shortcode = SysWeiXinTemplate.订单状态通知; break;
                        case "addReaBmsCenOrderDoctocomp": Shortcode = SysWeiXinTemplate.新订单提醒公司; break;
                    }

                    IList<ZhiFang.Entity.WeiXin.BWeiXinPushMessageTemplate> bwxpmtl = IDBWeiXinPushMessageTemplateDao.GetListByHQL(" Shortcode='" + Shortcode + "' ");

                    string PMTKey = bwxpmtl.Count > 0 ? bwxpmtl[0].PMTKey : null;
                    if (PMTKey != null)
                    {
                        for (int i = 0; i < bwxell.Count; i++)
                        {
                            pushWeiXinMessageAction(bwxell[i].BWeiXinAccount.WeiXinAccount, PMTKey, "#0061b2", url, data);
                        }
                    }
                }
            }
        }
    }
}