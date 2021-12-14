using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.BLL.Base;

namespace ZhiFang.BLL.RBAC
{	
	public class BRBACEmpRoles : BaseBLL<ZhiFang.Entity.RBAC.RBACEmpRoles>, IBRBACEmpRoles
    {
        public IBHREmployee IBHREmployee { get; set; }
        public IBRBACRole IBRBACRole { get; set; }

        public IList<RBACEmpRoles> SearchRBACEmpRolesByEmpId(string empId)
        {
            //RBACEmpRoles r = new RBACEmpRoles();
            //r.HREmployee.Id
            return ((IDRBACEmpRolesDao)DBDao).GetListByHQL("HREmployee.Id=" + empId, 0, 0).list;
        }
        /// <summary>
        /// 为单个员工增加或减少角色
        /// </summary>
        /// <param name="empIdList">员工ID</param>
        /// <param name="roleIdList">角色ID</param>
        /// <param name="flag">0增加1删除</param>
        /// <returns></returns>
        public bool RJ_SetEmpRolesByEmpId(string empId, string roleId, int flag)
        {
            bool r = true;

            HREmployee emp=IBHREmployee.Get(Convert.ToInt64(empId));
            if (emp == null)
            {
                return false;
            }
            RBACRole role =  IBRBACRole.Get(Convert.ToInt64(roleId));
            if (role == null)
            {
                return false;
            }
            
            IList<RBACEmpRoles> list = SearchRBACEmpRolesByEmpId(empId);
            list = list.Where(p => p.RBACRole.Id == Convert.ToInt64(roleId)).ToList();

            if (flag == 0)
            {
                if (list.Count == 0)
                {
                    //r = false;
                    //return r;
                    RBACEmpRoles empRole = new RBACEmpRoles();
                    empRole.LabID = 1;
                    empRole.Id = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
                    empRole.HREmployee = emp;
                    empRole.RBACRole = role;
                    this.Entity = empRole;
                    this.Add();
                }
            }
            else if (flag == 1)
            {        
               if (list.Count > 0)
               {
                   this.Remove(list[0].Id);
               }        
            }
            
            return r;
        }

        /// <summary>
        /// 为多个或单个员工增加或减少，多个或单个角色
        /// </summary>
        /// <param name="empIdList">员工ID</param>
        /// <param name="roleIdList">角色ID</param>
        /// <param name="flag">0增加1删除</param>
        /// <returns></returns>
        public bool RBAC_RJ_SetEmpRolesByEmpIdList(string[] empIdList, string[] roleIdList, int flag)
        {
            bool r = true;
            //string[] empList = empIdList.Split(';');
            //string[] roleList = roleIdList.Split(';');

            if (empIdList.Length < 1)
            {
                return false;
            }

            if (roleIdList.Length < 1)
            {
                return false;
            }

            foreach (string empid in empIdList)
            {
                foreach (string roleid in roleIdList)
                {
                    r = RJ_SetEmpRolesByEmpId(empid, roleid, flag);
                    if (!r)
                    {
                        return r;
                    }
                }
            }
            return r;

        }
    } 
}