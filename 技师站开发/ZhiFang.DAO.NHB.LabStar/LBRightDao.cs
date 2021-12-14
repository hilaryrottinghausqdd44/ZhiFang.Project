using System;
using System.Linq;
using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.DAO.NHB.LabStar
{
    public class LBRightDao : BaseDaoNHB<LBRight, long>, IDLBRightDao
    {
        public EntityList<LBSection> QueryCommoSectionByEmpIDDao(long[] arrayEmpID)
        {
            EntityList<LBSection> entityList = new EntityList<LBSection>();

            string strHQL = " and EmpID in (" + string.Join(", ", arrayEmpID) + ")";
            string strCountHQL = " where NN.Count >= " + arrayEmpID.Length;
            if (arrayEmpID.Length == 1)
            {
                strHQL = " and EmpID=" + arrayEmpID[0];
                strCountHQL = "";
            }
            string querySQL = " Select LS.* from ( Select SectionID, count(SectionID) as Count  from (" +
                              " Select SectionID, EmpID from LB_Right lbright where SectionID is not null and EmpID is not null " + strHQL + this.GetBaseDataFilter() +
                              " group by SectionID,EmpID) MM group by SectionID ) NN" +
                              " inner join LB_Section LS on NN.SectionID = LS.SectionID " + strCountHQL;

            IList<LBSection> listLBSection = this.Session.CreateSQLQuery(querySQL).AddEntity(typeof(LBSection)).List<LBSection>();

            if (listLBSection != null)
            {
                entityList.count = listLBSection.Count;
                entityList.list = listLBSection;
            }
            return entityList;
        }
    }
}