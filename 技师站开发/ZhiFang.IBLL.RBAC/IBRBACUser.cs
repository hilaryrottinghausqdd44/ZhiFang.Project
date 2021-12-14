using System.Collections.Generic;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.IBLL.RBAC
{
    public interface IBRBACUser : ZhiFang.IBLL.Base.IBGenericManager<RBACUser>
    {
        IBHREmployee IBHREmployee { get; set; }

        IList<RBACUser> GetByHQL(RBACUser entity);
        IList<RBACUser> SearchRBACUserByUserAccount(string strUserAccount);

        /// <summary>
        /// 用户登陆服务
        /// </summary>
        /// <param name="strUserAccount"></param>
        /// <param name="strPassWord"></param>
        /// <returns></returns>
        bool RBAC_BA_Login(string strUserAccount, string strPassWord);

        /// <summary>
        /// 用户注销服务
        /// </summary>
        /// <param name="strUserAccount"></param>
        /// <returns></returns>
        bool RBAC_BA_Logout(string strUserAccount);

        /// <summary>
        /// 自动生成员工账号名,根据员工姓名生成账户名,根据参数string去数据库验证是否重名,如重名则动态在账户名+“1”；
        /// </summary>
        /// <param name="strEmpID">员工ID</param>
        /// <param name="strUserAccount">帐户名</param>
        /// <returns></returns>
        string RJ_AutoCreateUserAccount(string strEmpID, string strUserAccount);

        /// <summary>
        /// 生成随机密码,默认6位
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        string RJ_GetRandomNumber(int num);

        /// <summary>
        /// 账户密码重置默认6位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string RJ_ResetAccountPassword(long id);

        /// <summary>
        /// 是否存在账户名
        /// </summary>
        /// <param name="strUserAccount">帐户名</param>
        /// <returns>true 存在；false不存在</returns>
        bool IsExistUserAccount(string strUserAccount);

        bool AddRBACUser(HREmployee emp, string strUserAccount, string pwd);

    }
}