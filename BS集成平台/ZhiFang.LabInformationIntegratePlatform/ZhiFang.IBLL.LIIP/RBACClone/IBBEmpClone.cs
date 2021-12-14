using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Entity.Base;

namespace ZhiFang.IBLL.LIIP.RBACClone
{
    public interface IBBEmpClone
    {
        BaseResultDataValue EmpClone(string DBType, string EmpId, string EmpName);
        BaseResultDataValue EmpClone_Doctor(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.HREmployee> doctorentity);
        BaseResultDataValue EmpClone_NPUser(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.HREmployee> npuserentity);
        BaseResultDataValue EmpClone_PUser(string DBType, string EmpId, string EmpName,List<ZhiFang.Entity.RBAC.HREmployee> puserentity);
        BaseResultDataValue CatchDoctorDataList();
        BaseResultDataValue CatchPuserDataList();
        BaseResultDataValue CatchNPuserDataList();

        BaseResultDataValue EmpClone_HREmployeeGoToLabStar6Table(string DBType, List<string> TableType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.HREmployee> puserentity);

        BaseResultDataValue EmpClone_HREmployeeGoToLabStar6TableByEntity(string DBType, List<string> TableType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.HREmployee> puserentity);
    }
}
