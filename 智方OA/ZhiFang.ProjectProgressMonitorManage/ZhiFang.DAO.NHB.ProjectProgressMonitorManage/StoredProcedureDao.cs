using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using Spring.Data.NHibernate.Generic;
using Spring.Data.NHibernate.Generic.Support;

namespace ZhiFang.DAO.NHB.ProjectProgressMonitorManage
{	
	public class StoredProcedureDao : HibernateDaoSupport, IDStoredProcedureDao
    {
        public IList<PReportData> QueryReportDataDao(int templetType, string templetID, string equipID, string empID, string beginDate, string endDate, int checkType, string otherPara)
        {
            if (templetID == null)
                templetID = "";
            if (equipID == null)
                equipID = "";
            if (otherPara == null)
                otherPara = "";
            if (beginDate == null)
                beginDate = "";
            if (endDate == null)
                endDate = "";
            if (empID == null)
                empID = "";
            string spName = "P_GetReportData" + ZhiFang.Common.Public.GetDbProcDriver.GetDbProcDriverByConfig();
            string[] paraName = new string[8] { "TempletType", "TempletID", "EquipID",  "EmpID", "BeginDate", "EndDate", "CheckType", "OtherPara" };
            object[] paraValue = new object[8] { templetType, templetID, equipID, empID, beginDate, endDate, checkType.ToString(), otherPara };
            var list = this.HibernateTemplate.FindByNamedQueryAndNamedParam<PReportData>(spName, paraName, paraValue);
            return list;
        }
    } 
}