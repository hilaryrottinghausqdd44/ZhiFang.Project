using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.RBAC
{
    /// <summary>
    ///
    /// </summary>
    public interface IBDoctor : IBGenericManager<Doctor, int>
    {
        /// <summary>
        /// HIS调用时,依传入HIS医生ID,获取到的医生信息(包含医生审核等级信息)
        /// </summary>
        /// <param name="hisDoctorCode"></param>
        /// <param name="hisDeptCode"></param>
        /// <param name="isAutoAddDepartmentUser">是否自动建立科室人员关系信息</param>
        /// <returns></returns>
        BaseResultDataValue GetSysCurDoctorInfoByHisCode(string hisDoctorCode, string hisDeptCode, bool isAutoAddDepartmentUser);
        /// <summary>
        /// BS依PUser登录帐号及密码,获取医生信息(包含医生审核等级信息)
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        BaseResultDataValue GetSysCurDoctorInfoByAccount(string account, string pwd);

        void AddSCOperation(Doctor serverPUser, string[] arrFields, long empID, string empName);
    }
}