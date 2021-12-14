
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.IDAO.NHB.BloodTransfusion;
using ZhiFang.Entity.Base;

namespace ZhiFang.BLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public class BBloodstyle : BaseBLL<Bloodstyle, int>, ZhiFang.IBLL.BloodTransfusion.IBBloodstyle
    {
        public IList<Bloodstyle> SearchBloodstyleListByJoinHql(string where, string bloodclassHql, string sort, int page, int limit)
        {
            IList<Bloodstyle> entityList = new List<Bloodstyle>();
            entityList = ((IDBloodstyleDao)base.DBDao).SearchBloodstyleListByJoinHql(where, bloodclassHql, sort, page, limit);
            return entityList;
        }
        public EntityList<Bloodstyle> SearchBloodstyleEntityListByJoinHql(string where, string bloodclassHql, string sort, int page, int limit)
        {
            EntityList<Bloodstyle> entityList = new EntityList<Bloodstyle>();
            entityList = ((IDBloodstyleDao)base.DBDao).SearchBloodstyleEntityListByJoinHql(where, bloodclassHql, sort, page, limit);
            return entityList;
        }
    }
}