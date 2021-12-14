using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.Common.Public;
using ZhiFang.WeiXin.Common;

namespace ZhiFang.BLL.RBAC
{	
    /// <summary>
    /// 
    /// </summary>
    public class BRBACUser : Base.BaseBLL<RBACUser>, IBRBACUser
    {
        public IBHREmployee IBHREmployee { get; set; }

        #region IBRBACUser 成员

        public IList<RBACUser> GetByHQL(RBACUser entity)
        {
            return ((IDRBACUserDao)base.DBDao).GetByHQL(entity);
        }
        public IList<RBACUser> SearchRBACUserByUserAccount(string strUserAccount)
        {
            return ((IDRBACUserDao)base.DBDao).SearchRBACUserByUserAccount(strUserAccount);
        }

        /// <summary>
        /// 用户登陆服务
        /// </summary>
        /// <param name="strUserAccount"></param>
        /// <param name="strPassWord"></param>
        /// <returns></returns>
        public bool RBAC_BA_Login(string strUserAccount, string strPassWord)
        {
            IList<RBACUser> tempRBACUser = SearchRBACUserByUserAccount(strUserAccount);
            if (tempRBACUser.Count == 1)
            {
                return (tempRBACUser[0].Account == strUserAccount) && (tempRBACUser[0].PWD == strPassWord);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 用户注销服务
        /// </summary>
        /// <param name="strUserAccount"></param>
        /// <returns></returns>
        public bool RBAC_BA_Logout(string strUserAccount)
        {
            return false;
        }


        public IList<RBACUser> CheckUserByEmpID(string strEmpID)
        {
            IList<RBACUser> list = ((IDRBACUserDao)base.DBDao).GetListByHQL("HREmployee.Id=" + strEmpID, 0,0).list;
            return list;
        }


        #endregion

        /// <summary>
        /// 自动生成员工账号名,根据员工姓名生成账户名,根据参数string去数据库验证是否重名,如重名则动态在账户名+“1”；
        /// </summary>
        /// <param name="strEmpID">员工ID</param>
        /// <param name="strUserAccount">帐户名</param>
        /// <returns></returns>
        public string RJ_AutoCreateUserAccount(string strEmpID, string strUserAccount)
        {
            if(strEmpID==null || strEmpID=="")
            {
                return "员工ID不能为空";
            }
            HREmployee emp = IBHREmployee.Get(Convert.ToInt64(strEmpID));
            if (emp == null)
            {
                return "数据库中不存在员工" + strEmpID;
            }
            ////查找员工是否存在账户
            //IList<RBACUser> list = CheckUserByEmpID(strEmpID);

            //RBACUser entity = new RBACUser();
            //entity.LabID = 1;
            //entity.Id = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
            //entity.HREmployee = emp;

            ////传入的帐户名不为空
            //if (strUserAccount != null && strUserAccount != "")
            //{
            //    if (list.Count > 0)
            //    {
            //        //数据库中存在此帐户，需要修改帐户名，并且不重复
            //        RBACUser entity1 = list[0];
            //        entity1.Account = CheckUserAccount(strUserAccount);
            //        this.Entity = entity1;
            //        this.Edit();
            //    }
            //    else
            //    {
            //        //数据库不存在此帐户，增加帐户，并且不重复
            //        entity.UseCode = entity.HREmployee.UseCode;
            //        entity.CName = entity.HREmployee.CName;
            //        entity.Account = CheckUserAccount(strUserAccount);
            //        this.Entity = entity;
            //        this.Add();
            //    }
            //}
            //else 
            //{            
            //    if (list.Count > 0)
            //    {
            //        //数据库中存在此员工的账户
            //        return list[0].Account;
            //    }
            //    //规则姓名第一个汉字的全拼加其他其他汉字的第一个字母
            //    entity.UseCode = entity.HREmployee.UseCode;
            //    entity.CName = entity.HREmployee.CName;
            //    entity.Account = CheckUserAccount(ZhiFang.Common.Public.StringPlus.Chinese2Spell.ChsString2Spell(entity.HREmployee.CName));
            //    this.Entity = entity;
            //    this.Add();
            
            //return this.Entity.Account;
            if (string.IsNullOrEmpty(strUserAccount))
                //return CheckUserAccount(ZhiFang.Common.Public.StringPlus.Chinese2Spell.ChsString2Spell(emp.CName));
                return CheckUserAccount(PinYinConverter.Get(emp.CName));
            else
                return CheckUserAccount(strUserAccount);
        }

        /// <summary>
        /// 是否存在账户名
        /// </summary>
        /// <param name="strUserAccount">帐户名</param>
        /// <returns>true 存在；false不存在</returns>
        public bool IsExistUserAccount(string strUserAccount)
        {
            IList<RBACUser> tempRBACUser = SearchRBACUserByUserAccount(strUserAccount);
            if (tempRBACUser.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string CheckUserAccount(string strUserAccount)
        {
            string strResult = strUserAccount;
            while (true)
            {
                IList<RBACUser> tempRBACUser = SearchRBACUserByUserAccount(strResult);
                if (tempRBACUser.Count == 0)
                {
                    break;
                }
                else
                {
                    strResult = strResult + "1";
                }
            }
            return strResult;
        }

        /// <summary>
        /// 生成随机密码,默认6位
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 账户密码重置默认6位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string RJ_ResetAccountPassword(long id)
        {
            RBACUser entity = this.Get(id);
            entity.PWD = WeiXin.Common.SecurityHelp.MD5Encrypt(RJ_GetRandomNumber(6), WeiXin.Common.SecurityHelp.PWDMD5Key);
            this.Entity = entity;
            this.Edit();

            return entity.PWD;
        }
    } 
}