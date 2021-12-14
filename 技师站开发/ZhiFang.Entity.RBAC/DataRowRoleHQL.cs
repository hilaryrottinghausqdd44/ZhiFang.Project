using System.Collections.Generic;

namespace ZhiFang.Entity.RBAC
{
    public class DataRowRoleHQL
    {
        private string hql;
        public string Hql
        {
            get { return hql; }
            set { this.hql = value; }
        }
        /// <summary>
        /// 行数据条件拦截处理的行数据条件集合
        /// 包含有单表及预置条件的行数据条件
        /// </summary>
        private Dictionary<string, string> dicPreconditionsHQL;
        public Dictionary<string, string> DicPreconditionsHQL
        {
            get
            {
                //if (dicPreconditionsHQL == null) dicPreconditionsHQL = new Dictionary<string, string>();
                return dicPreconditionsHQL;
            }
            set
            {
                dicPreconditionsHQL = value;
            }
        }

    }
}
