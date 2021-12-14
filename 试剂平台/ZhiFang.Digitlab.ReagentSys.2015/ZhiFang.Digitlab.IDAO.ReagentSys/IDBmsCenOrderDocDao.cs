using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO.ReagentSys
{
    public interface IDBmsCenOrderDocDao : IDBaseDao<BmsCenOrderDoc, long>
    {
        EntityList<BmsCenOrderDoc> SearchBmsCenOrderDocDao(string orderDocWhere, string orderDtlWhere, string sort, int page, int limit);
    }
}