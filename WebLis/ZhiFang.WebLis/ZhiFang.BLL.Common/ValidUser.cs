using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IBLL.Common;

namespace ZhiFang.BLL.Common
{
    public class ValidUser : ZhiFang.IBLL.Common.IBValidUser
    {
        #region IValidUser 成员

        /// <summary>
        /// 登陆验证方法
        /// </summary>
        /// <param name="strUserName">用户名</param>
        /// <param name="strPassWord">密码</param>
        /// <returns>true/false</returns>
        public bool ValidUsers(string strUserName, string strPassWord)
        {
            return true;
        }

        #endregion
    }
}
