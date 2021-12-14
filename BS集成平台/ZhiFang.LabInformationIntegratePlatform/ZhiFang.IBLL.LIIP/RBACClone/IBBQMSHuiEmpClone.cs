using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFang.IBLL.LIIP.RBACClone
{
    public interface IBBQMSEmpClone
    {
        ZhiFang.Entity.Base.BaseResultDataValue HRDeptClone(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.HRDept> entity);
        ZhiFang.Entity.Base.BaseResultDataValue CatchHRDeptDataList(string DBType);
        ZhiFang.Entity.Base.BaseResultDataValue HRDeptEmpClone(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.HRDeptEmp> entity);
        ZhiFang.Entity.Base.BaseResultDataValue CatchHRDeptEmpDataList(string DBType);
        ZhiFang.Entity.Base.BaseResultDataValue HRDeptIdentityClone(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.HRDeptIdentity> entity);
        ZhiFang.Entity.Base.BaseResultDataValue CatchHRDeptIdentityDataList(string DBType);
        ZhiFang.Entity.Base.BaseResultDataValue HREmpIdentityClone(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.HREmpIdentity> entity);
        ZhiFang.Entity.Base.BaseResultDataValue CatchHREmpIdentityDataList(string DBType);
        ZhiFang.Entity.Base.BaseResultDataValue HREmployeeClone(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.HREmployee> entity);
        ZhiFang.Entity.Base.BaseResultDataValue CatchHREmployeeDataList(string DBType);
        ZhiFang.Entity.Base.BaseResultDataValue HRPositionClone(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.HRPosition> entity);
        ZhiFang.Entity.Base.BaseResultDataValue CatchHRPositionDataList(string DBType);
        ZhiFang.Entity.Base.BaseResultDataValue RBACEmpOptionsClone(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.RBACEmpOptions> entity);
        ZhiFang.Entity.Base.BaseResultDataValue CatchRBACEmpOptionsDataList(string DBType);
        ZhiFang.Entity.Base.BaseResultDataValue RBACEmpRolesClone(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.RBACEmpRoles> entity);
        ZhiFang.Entity.Base.BaseResultDataValue CatchRBACEmpRolesDataList(string DBType);
        ZhiFang.Entity.Base.BaseResultDataValue RBACModuleClone(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.RBACModule> entity);
        ZhiFang.Entity.Base.BaseResultDataValue CatchRBACModuleDataList(string DBType);
        ZhiFang.Entity.Base.BaseResultDataValue RBACModuleOperClone(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.RBACModuleOper> entity);
        ZhiFang.Entity.Base.BaseResultDataValue CatchRBACModuleOperDataList(string DBType);
        ZhiFang.Entity.Base.BaseResultDataValue RBACRoleClone(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.RBACRole> entity);
        ZhiFang.Entity.Base.BaseResultDataValue CatchRBACRoleDataList(string DBType);
        ZhiFang.Entity.Base.BaseResultDataValue RBACRoleModuleClone(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.RBACRoleModule> entity);
        ZhiFang.Entity.Base.BaseResultDataValue CatchRBACRoleModuleDataList(string DBType);
        ZhiFang.Entity.Base.BaseResultDataValue RBACRoleRightClone(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.RBACRoleRight> entity);
        ZhiFang.Entity.Base.BaseResultDataValue CatchRBACRoleRightDataList(string DBType);
        ZhiFang.Entity.Base.BaseResultDataValue RBACRowFilterClone(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.RBACRowFilter> entity);
        ZhiFang.Entity.Base.BaseResultDataValue CatchRBACRowFilterDataList(string DBType);
        ZhiFang.Entity.Base.BaseResultDataValue RBACUserClone(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.RBACUser> entity);
        ZhiFang.Entity.Base.BaseResultDataValue CatchRBACUserDataList(string DBType);
    }
}
