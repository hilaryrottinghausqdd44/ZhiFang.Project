using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
{
    public interface IBDBStoredProcedure
    {
        EntityList<string> MigrationCenQtyDtlTemp(long QtyDtlID);

        EntityList<CenQtyDtlTempHistory> StatReagentConsume(string strPara, int groupByType);
    }
}