using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.IBLL.RBAC
{
    public interface IBRBACEmpOptions : ZhiFang.IBLL.Base.IBGenericManager<ZhiFang.Entity.RBAC.RBACEmpOptions>
	{
        IBRBACRoleModule IBRBACRoleModule { get; set; }
        /// <summary>
        /// 根据员工ID查询该人员的常用模块列表
        /// </summary>
        /// <param name="strEmpID">员工ID</param>
        /// <returns>IList&lt;RBACEmpOptions&gt;</returns>
        IList<RBACEmpOptions> SearchRBACEmpOptionsByEmpID(string strEmpID);
	} 
}