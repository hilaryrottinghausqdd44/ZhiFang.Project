using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.IBLL.Common
{
    public interface IBValidUser
    {
        /// <summary>
        /// 登陆验证方法
        /// </summary>
        /// <param name="strUserName">用户名</param>
        /// <param name="strPassWord">密码</param>
        /// <returns>true/false</returns>
        bool ValidUsers(string strUserName, string strPassWord);
    }
}
