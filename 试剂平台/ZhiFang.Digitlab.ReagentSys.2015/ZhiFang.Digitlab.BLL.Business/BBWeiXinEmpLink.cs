using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;
using ZhiFang.IDAO.OA;
using ZhiFang.Common.Public;

namespace ZhiFang.Digitlab.BLL.Business
{
    /// <summary>
    ///
    /// </summary>
    public class BBWeiXinEmpLink : BaseBLL<BWeiXinEmpLink>, ZhiFang.Digitlab.IBLL.Business.IBBWeiXinEmpLink
    {
        public IDRBACUserDao IDRBACUserDao { get; set; }
        public IDBWeiXinAccountDao IDBWeiXinAccountDao { get; set; }
        public bool AddByUserAccountOpenID(string strUserAccount, string strPassWord, string OpenID, out string ErrorInfo,out HREmployee emp)
        {
            emp = null;
            ErrorInfo = "";
            IList<RBACUser> tempRBACUser = IDRBACUserDao.SearchRBACUserByUserAccount(strUserAccount);
            if (tempRBACUser.Count == 1)
            {
                strPassWord = SecurityHelp.MD5Encrypt(strPassWord, SecurityHelp.PWDMD5Key);
                if (tempRBACUser[0].IsUse)
                {
                    if (tempRBACUser[0].HREmployee.IsUse && tempRBACUser[0].HREmployee.IsEnabled == 1)
                    {
                        bool tempBool = (tempRBACUser[0].Account == strUserAccount) && (tempRBACUser[0].PWD == strPassWord) && (!tempRBACUser[0].AccLock);
                        if (tempBool)
                        {
                            BWeiXinEmpLink bxel = new BWeiXinEmpLink();
                            IList<BWeiXinAccount> BWeiXinAccountList= IDBWeiXinAccountDao.GetListByHQL(" WeiXinAccount='" + OpenID + "' ");
                            if (BWeiXinAccountList != null && BWeiXinAccountList.Count > 0)
                            {
                                bxel.BWeiXinAccount = BWeiXinAccountList[0];
                            }
                            else
                            {
                                ErrorInfo = "未找到OpenID对应的微信账户！";
                                ZhiFang.Common.Log.Log.Debug("未找到OpenID对应的微信账户！");
                                return false;
                            }
                            bxel.EmpID = tempRBACUser[0].HREmployee.Id;
                            bxel.EmpName = tempRBACUser[0].HREmployee.CName;
                            bxel.LabID = tempRBACUser[0].LabID;
                            this.Entity = bxel;
                            emp = tempRBACUser[0].HREmployee;
                            return this.Add();
                        }
                        else
                        {
                            ErrorInfo = "用户名或密码错误！";
                            ZhiFang.Common.Log.Log.Debug("用户名或密码错误！");
                            return false;
                        }
                    }
                    else
                    {
                        ErrorInfo = "员工被禁用或者逻辑删除！";
                        ZhiFang.Common.Log.Log.Debug("员工被禁用或者逻辑删除！");
                        return false;
                    }
                }
                else
                {
                    ErrorInfo = "员工帐号被逻辑删除！";
                    ZhiFang.Common.Log.Log.Debug("员工帐号被逻辑删除！");
                    return false;
                }
            }
            else
            {
                ErrorInfo = "员工帐号不存在！";
                ZhiFang.Common.Log.Log.Debug("员工帐号不存在！");
                return false;
            }
            return false;
        }
    }
}