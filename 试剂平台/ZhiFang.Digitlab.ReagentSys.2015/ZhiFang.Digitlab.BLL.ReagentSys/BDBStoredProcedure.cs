using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using ZhiFang.Digitlab.IDAO.ReagentSys;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Common.Public;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    public class BDBStoredProcedure : ZhiFang.Digitlab.IBLL.ReagentSys.IBDBStoredProcedure
    {
        public IDDBStoredProcedureDao IDDBStoredProcedureDao { get; set; }

        public EntityList<string> MigrationCenQtyDtlTemp(long QtyDtlID)
        {
            EntityList<string> tempEntityList = new EntityList<string>();
            tempEntityList.list = IDDBStoredProcedureDao.MigrationCenQtyDtlTempDao(QtyDtlID);
            if (tempEntityList.list != null)
                tempEntityList.count = tempEntityList.list.Count;
            return tempEntityList;
        }

        public EntityList<CenQtyDtlTempHistory> StatReagentConsume(string strPara, int groupByType)
        {
            EntityList<CenQtyDtlTempHistory> tempEntityList = new EntityList<CenQtyDtlTempHistory>();
            tempEntityList.list = IDDBStoredProcedureDao.StatReagentConsumeDao(strPara, groupByType);
            if (tempEntityList.list != null)
                tempEntityList.count = tempEntityList.list.Count;
            return tempEntityList;
        }
    }
}