using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.IDAO.ReagentSys;
using Spring.Data.NHibernate.Generic;
using Spring.Data.NHibernate.Generic.Support;


namespace ZhiFang.Digitlab.DAO.NHB.ReagentSys
{
    public class DBStoredProcedureDao : HibernateDaoSupport, IDDBStoredProcedureDao
    {

        public IList<string> MigrationCenQtyDtlTempDao(long QtyDtlID)
        {
            string spName = "P_CenQtyDtlTemp_Migration";
            string[] paraName = new string[1] { "QtyDtlID" };
            object[] paraValue = new object[1] { QtyDtlID };
            var list = this.HibernateTemplate.FindByNamedQueryAndNamedParam<string>(spName, paraName, paraValue);
            return list;
        }

        public IList<CenQtyDtlTempHistory> StatReagentConsumeDao(string strPara, int groupByType)
        {
            string spName = "P_Stat_Reagent_Consume";
            string[] paraName = new string[2] { "StrPara", "GroupByType" };
            object[] paraValue = new object[2] { strPara, groupByType };
            var list = this.HibernateTemplate.FindByNamedQueryAndNamedParam<CenQtyDtlTempHistory>(spName, paraName, paraValue);
            return list;
        }
    }
}