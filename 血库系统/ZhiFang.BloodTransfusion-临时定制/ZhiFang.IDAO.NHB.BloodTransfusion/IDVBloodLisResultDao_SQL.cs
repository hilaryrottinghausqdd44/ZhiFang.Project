using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.NHB.BloodTransfusion
{
    public interface IDVBloodLisResultDao_SQL : IDBaseDao<VBloodLisResult, long>
    {
        IList<VBloodLisResult> SelectListByHQL(string strSqlWhere);
    }
}
