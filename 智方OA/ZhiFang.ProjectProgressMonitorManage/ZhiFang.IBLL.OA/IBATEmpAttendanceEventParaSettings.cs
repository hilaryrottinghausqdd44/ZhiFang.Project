using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.OA;
using ZhiFang.Entity.OA.ViewObject.Response;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.OA
{
    /// <summary>
    ///
    /// </summary>
    public interface IBATEmpAttendanceEventParaSettings : IBGenericManager<ATEmpAttendanceEventParaSettings>
    {
        IList<ATEmpAttendanceEventParaSettings> SearchATEmpAttendanceEventParaSettingsByDeptId(long deptid, bool isincludesubdept);
        ATEmpAttendanceEventParaSettings SearchATEmpAttendanceEventParaSettingsByEmpId(long empId);
    }
}