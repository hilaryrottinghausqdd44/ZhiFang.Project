
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.Common;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BBWeiXinAccount : ZhiFang.BLL.Base.BaseBLL<BWeiXinAccount>, ZhiFang.WeiXin.IBLL.IBBWeiXinAccount
    {
        public IDBWeiXinEmpLinkDao IDBWeiXinEmpLinkDao { get; set; }
        public IDBWeiXinPushMessageTemplateDao IDBWeiXinPushMessageTemplateDao { get; set; }
        public IDBDoctorAccountDao IDBDoctorAccountDao { get; set; }
        public bool CheckWeiXinAccountByOpenID(string OpenID)
        {
            int count = DBDao.GetListCountByHQL(" WeiXinAccount='" + OpenID + "' ");
            if (count > 0)
                return true;
            return false;
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
                string RegeditMobileFlag = ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("RegeditMobileFlag");
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

        /// <summary>
        /// 根据OpenID判断用户是否有手机号
        /// </summary>
        /// <param name="OpenID"></param>
        /// <param name="WeiXinAccountId"></param>
        /// <returns>true有，false没有</returns>
        public bool CheckWeiXinAccountMobileCodeByOpenID(string OpenID, out BWeiXinAccount BWeiXinAccount, out bool LoginInputPasswordFlag)
        {
            BWeiXinAccount = new BWeiXinAccount();
            LoginInputPasswordFlag = false;
            var objectlist = DBDao.GetListByHQL(" WeiXinAccount='" + OpenID + "' ");
            if (objectlist.Count > 0)
            {
                BWeiXinAccount = objectlist[0];
                LoginInputPasswordFlag = objectlist[0].LoginInputPasswordFlag;
                string RegeditMobileFlag = ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("RegeditMobileFlag");
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
                    List<string> receiveopenidlist = new List<string>();
                    for (int i = 0; i < bwxell.Count; i++)
                    {
                        receiveopenidlist.Add(bwxell[i].BWeiXinAccount.WeiXinAccount);
                    }
                    PushWeiXinMessage(pushWeiXinMessageAction, receiveopenidlist, pdata, syscode, url);
                }
            }
        }

        public void PushWeiXinMessage(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, List<string> receiveopenidlist, Dictionary<string, TemplateDataObject> pdata, string syscode, string url)
        {
            //ZhiFang.Common.Log.Log.Debug("PushWeiXinMessage.receiveopenidlist.Count:" + receiveopenidlist.Count);
            if (receiveopenidlist.Count > 0)
            {
                //ZhiFang.Common.Log.Log.Debug("PushWeiXinMessage.receiveopenidlist[0]:" + receiveopenidlist[0]);
                string Shortcode = "";// SysWeiXinTemplate.集成系统新消息提醒;
                Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                data = pdata;
                switch (syscode.Trim().ToLower())
                {
                    case "refunform": Shortcode = SysWeiXinTemplate.退费通知; break;
                    case "doctorbonusfinish": Shortcode = SysWeiXinTemplate.结算成功提醒; break;
                    case "orderformpush": Shortcode = SysWeiXinTemplate.医嘱提醒; break;
                    case "reportformpush": Shortcode = SysWeiXinTemplate.检验报告通知; break;
                    case "refunformsingle": Shortcode = SysWeiXinTemplate.退费通知精简; break;
                }
                //ZhiFang.Common.Log.Log.Debug("PushWeiXinMessage.HQL:" + " Shortcode='" + Shortcode + "' ");
                IList<BWeiXinPushMessageTemplate> bwxpmtl = IDBWeiXinPushMessageTemplateDao.GetListByHQL(" Shortcode='" + Shortcode + "' ");
                //ZhiFang.Common.Log.Log.Debug("PushWeiXinMessage.bwxpmtl.Count:" + bwxpmtl.Count);
                string PMTKey = bwxpmtl != null && bwxpmtl.Count > 0 ? bwxpmtl[0].PMTKey : null;
                if (PMTKey != null)
                {
                    for (int i = 0; i < receiveopenidlist.Count; i++)
                    {
                        pushWeiXinMessageAction(receiveopenidlist[i], PMTKey, "#0061b2", url, data);
                    }
                }
            }
        }

        public BaseResultDataValue WeiXinAccountBind(string id, string accountCode, string password)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            IList<BWeiXinAccount> BWeiXinAccountList = DBDao.GetListByHQL(" MobileCode='" + accountCode + "' and  PassWord ='" + SecurityHelp.MD5Encrypt(password, SecurityHelp.PWDMD5Key) + "'");
            if (BWeiXinAccountList == null || BWeiXinAccountList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "未找到待绑定账户信息！";
                ZhiFang.Common.Log.Log.Debug("WeiXinAccountBind:未找到待绑定账户信息.id=" + id + ",accountCode=" + accountCode + ",password=" + password + ",passwordmd5=" + SecurityHelp.MD5Encrypt(password, SecurityHelp.PWDMD5Key));
                return brdv;
            }
            if (BWeiXinAccountList[0].WeiXinAccount != null && BWeiXinAccountList[0].WeiXinAccount.Trim() != "" && BWeiXinAccountList[0].WeiXinAccount != accountCode)
            {
                brdv.success = false;
                brdv.ErrorInfo = "输入账户'" + accountCode + "'已绑定！";
                ZhiFang.Common.Log.Log.Debug("WeiXinAccountBind:输入账户'" + accountCode + "'已绑定！.id=" + id + ",accountCode=" + accountCode + ",password=" + password + ",passwordmd5=" + SecurityHelp.MD5Encrypt(password, SecurityHelp.PWDMD5Key));
                return brdv;
            }

            BWeiXinAccount bweiXinAccount = DBDao.Get(long.Parse(id));
            if (bweiXinAccount == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "未找到相关账户信息！";
                ZhiFang.Common.Log.Log.Debug("WeiXinAccountBind:未找到相关账户信息.id=" + id + ",accountCode=" + accountCode + ",password=" + password + ",passwordmd5=" + SecurityHelp.MD5Encrypt(password, SecurityHelp.PWDMD5Key));
                return brdv;
            }

            if (DBDao.UpdateByHql("update BWeiXinAccount set WeiXinAccount='" + bweiXinAccount.WeiXinAccount + "' ,UserName='" + bweiXinAccount.UserName + "' ,SexID=" + bweiXinAccount.SexID + ",CountryName='" + bweiXinAccount.CountryName + "',ProvinceName='" + bweiXinAccount.ProvinceName + "',CityName='" + bweiXinAccount.CityName + "',Language='" + bweiXinAccount.Language + "',HeadImgUrl='" + bweiXinAccount.HeadImgUrl + "' where Id=" + BWeiXinAccountList[0].Id) > 0)
            {
                DBDao.Delete(long.Parse(id));
                brdv.success = true;
                brdv.ResultDataValue = "true";
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "未能更新待绑定帐号信息！请重新绑定！";
            }
            return brdv;
        }

        public BaseResultDataValue DoctorAccountBindWeiXinAccountChange(long id, string accountCode, string password)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BWeiXinAccount bweiXinAccount = DBDao.Get(id);
            if (bweiXinAccount == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "未找到相关账户信息！";
                ZhiFang.Common.Log.Log.Debug("DoctorAccountBindWeiXinAccountChange:未找到相关账户信息.id=" + id + ",accountCode=" + accountCode + ",password=" + password + ",passwordmd5=" + SecurityHelp.MD5Encrypt(password, SecurityHelp.PWDMD5Key));
                return brdv;
            }
            else
            {
                if (bweiXinAccount.WeiXinAccount != bweiXinAccount.MobileCode)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "微信账户已绑定！";
                    return brdv;
                }
                else
                {
                    if (accountCode != bweiXinAccount.MobileCode && DBDao.GetListCountByHQL(" MobileCode='" + accountCode + "' and  id <>" + id) > 0)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "微信绑定帐号重复！" + accountCode;
                        return brdv;
                    }

                    if (DBDao.UpdateByHql("update BWeiXinAccount set WeiXinAccount='" + accountCode + "',UserName='" + accountCode + "', MobileCode='" + accountCode + "' ,PassWord ='" + SecurityHelp.MD5Encrypt(password, SecurityHelp.PWDMD5Key) + "' where Id=" + id) > 0)
                    {
                        brdv.success = true;
                        brdv.ResultDataValue = "true";
                    }
                    else
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "修改未成功！";
                    }
                    return brdv;

                }
            }
        }

        public bool UserReadAgreement(string weiXinUserID)
        {
            int flag = this.DBDao.UpdateByHql("update BWeiXinAccount  set ReadAgreement='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where   Id=" + weiXinUserID + "");
            if (flag > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public BaseResultDataValue WeiXinAccountPwdRest(string weiXinAccount, string empIdStr, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            IList<BWeiXinAccount> List = DBDao.GetListByHQL(" WeiXinAccount='" + weiXinAccount + "' ");
            if (List != null && List.Count > 0)
            {
                BWeiXinAccount entity = List[0];
                string pwdstr = RJ_GetRandomNumber(7);
                entity.PassWord = WeiXin.Common.SecurityHelp.MD5Encrypt(pwdstr, WeiXin.Common.SecurityHelp.PWDMD5Key);
                this.Entity = entity;
                this.Edit();
                brdv.ResultDataValue = pwdstr;
                brdv.success = true;
                ZhiFang.Common.Log.Log.Debug("WeiXinAccountPwdRest.weiXinAccount=" + weiXinAccount + ",empIdStr=" + empIdStr + ",empName=" + empName+".重置成功！");
            }
            else
            {
                brdv.ResultDataValue = "";
                brdv.success = false;
                brdv.ErrorInfo = "用户错误！";
                ZhiFang.Common.Log.Log.Debug("WeiXinAccountPwdRest.weiXinAccount=" + weiXinAccount + ",empIdStr=" + empIdStr + ",empName=" + empName + ".重置失败！");
            }
            return brdv;            
        }

        public string RJ_GetRandomNumber(int num)
        {
            string strMinValue = "100000000000000000000000000000000000000";
            string strMaxValue = "999999999999999999999999999999999999999";

            Random r = new Random();
            if (num == 0)
            {
                num = 6;
            }
            int minValue = Convert.ToInt32(strMinValue.Substring(0, num));
            int maxValue = Convert.ToInt32(strMaxValue.Substring(0, num));

            return r.Next(minValue, maxValue).ToString();
        }

        public EntityList<BWeiXinAccount> WXADS_SearchWeiXinAccount_User(string where, string sort, int page, int limit)
        {
            var doctlist=IDBDoctorAccountDao.GetListByHQL(" WeiXinUserID is not null ");
           
            if (doctlist != null && doctlist.Count > 0)
            {
                string tmpstr = "  ";
                foreach (var doct in doctlist)
                {
                    tmpstr += doct.WeiXinUserID + ",";
                }
                tmpstr=tmpstr.Remove(tmpstr.Length - 1);
                where += " and id not in (" + tmpstr + ")";
            }
            return DBDao.GetListByHQL(where, sort, page, limit);
        }
    }
}