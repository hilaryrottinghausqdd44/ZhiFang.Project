

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public interface IBCUser : IBGenericManager<CUser>
    {
        /// <summary>
        /// 将CUser某一记录行复制到PClient中
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResultBool CopyCUserToPClientByCUserId(long id, int type, long empID, string empName);

    }
}