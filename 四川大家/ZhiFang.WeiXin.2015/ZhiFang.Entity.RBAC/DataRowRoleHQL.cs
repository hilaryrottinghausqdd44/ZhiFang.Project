using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Entity.RBAC
{
    public class DataRowRoleHQL
    {
        private string hql;
        public string Hql {
            get { return hql; }
            set { this.hql = value; }
        }
    }
}
