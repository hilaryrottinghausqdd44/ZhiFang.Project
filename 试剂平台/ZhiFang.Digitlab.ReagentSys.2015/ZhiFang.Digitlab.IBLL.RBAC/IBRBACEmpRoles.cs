using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
namespace ZhiFang.Digitlab.IBLL.RBAC
{	
	public interface IBRBACEmpRoles : IBGenericManager<ZhiFang.Digitlab.Entity.RBACEmpRoles>
	{
        IBHREmployee IBHREmployee { get; set; }
        IBRBACRole IBRBACRole { get; set; }

        /// <summary>
        /// 根据员工ID获取角色列表
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        IList<RBACEmpRoles> SearchRBACEmpRolesByEmpId(string empId);

        /// <summary>
        /// 为单个员工增加或减少角色
        /// </summary>
        /// <param name="empIdList">员工ID</param>
        /// <param name="roleIdList">角色ID</param>
        /// <param name="flag">0增加1删除</param>
        /// <returns></returns>
        bool RJ_SetEmpRolesByEmpId(string empId, string roleId, int flag);


        /// <summary>
        /// 为多个或单个员工增加或减少，多个或单个角色
        /// </summary>
        /// <param name="empIdList">员工ID数组</param>
        /// <param name="roleIdList">角色ID数组</param>
        /// <param name="flag">0增加1删除</param>
        /// <returns></returns>
        bool RBAC_RJ_SetEmpRolesByEmpIdList(string[] empIdList, string[] roleIdList, int flag);
        
	} 
}