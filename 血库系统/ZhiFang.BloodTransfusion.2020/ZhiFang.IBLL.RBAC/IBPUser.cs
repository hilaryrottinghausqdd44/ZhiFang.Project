using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.RBAC
{
    /// <summary>
    ///
    /// </summary>
    public interface IBPUser : IBGenericManager<PUser>
    {
        bool RBAC_BA_Logout(string strUserAccount);
        /// <summary>
        ///加密
        /// </summary>
        /// <param name="astr"></param>
        /// <returns></returns>
        string CovertPassWord(string astr);
        string UnCovertPassWord(string pwd);
        BaseResultDataValue GetUserLogin(string account, string pwd);
        /// <summary>
        /// his调用时验证及获取人员信息入口
        /// </summary>
        /// <param name="hisWardCode"></param>
        /// <param name="hisDeptCode"></param>
        /// <param name="hisDoctorCode"></param>
        /// <param name="isAutoAddDepartmentUser"></param>
        /// <returns></returns>
        BaseResultDataValue GetUserLoginByHisCode(string hisWardCode, string hisDeptCode, string hisDoctorCode, bool isAutoAddDepartmentUser);

        /// <summary>
        /// 获取封装处理后的PUser信息
        /// </summary>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<RBACUser> SearchRBACUserOfPUserByHQL(string where, string sort, int page, int limit);

        /// <summary>
        /// 通过指定字段获取PUser
        /// </summary>
        /// <param name="fieldKey"></param>
        /// <param name="fieldVal"></param>
        /// <returns></returns>
        PUser SearchPUserByFieldKey(string fieldKey, string fieldVal);

        /// <summary>
        /// 通过指定字段获取PUser
        /// </summary>
        /// <param name="fieldKey"></param>
        /// <param name="fieldVal"></param>
        /// <returns></returns>
        RBACUser SearchRBACUserByFieldKey(string fieldKey, string fieldVal);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverPUser"></param>
        /// <param name="arrFields"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        void AddSCOperation(PUser serverPUser, string[] arrFields, long empID, string empName);

        /// <summary>
		/// 获取6.6数据库的人员同步信息
		/// </summary>
		/// <returns></returns>
		Dictionary<JObject, JObject> GetSyncPUserList();
    }
}