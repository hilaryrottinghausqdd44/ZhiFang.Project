
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Entity.RBAC;
using ZhiFang.BLL.Base;
using ZhiFang.IBLL.RBAC;

namespace ZhiFang.BLL.RBAC
{
    /// <summary>
    ///
    /// </summary>
    public class BDepartmentUser : BaseBLL<DepartmentUser>, IBDepartmentUser
    {
        IDDepartmentDao IDDepartmentDao { get; set; }
        IDPUserDao IDPUserDao { get; set; }

        public override bool Add()
        {
            if (this.Entity.Department == null)
            {
                return false;
            }
            if (this.Entity.PUser == null)
            {
                return false;
            }
            Department department = IDDepartmentDao.Get(this.Entity.Department.Id);
            if (department == null)
            {
                return false;
            }
            PUser puser = IDPUserDao.Get(this.Entity.PUser.Id);
            if (puser == null)
            {
                return false;
            }
            return base.Add();
        }
    }
}