using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.BLL.BloodTransfusion
{
    public class CacheDictClass
    {
        public static IList<BloodBReqType> BloodBReqTypeList;
        public static IList<BloodUseType> BloodUseTypeList;
        public static IList<Department> DepartmentList;
        public static IList<Doctor> DoctorList;
    }
}
