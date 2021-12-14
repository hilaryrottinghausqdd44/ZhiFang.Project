using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.DAO.NHB.LabStar
{
    public class LBParItemSplitDao : BaseDaoNHB<LBParItemSplit, long>, IDLBParItemSplitDao
    {
        public IList<LBParItemSplit> QueryLBParItemSplitDao(string strHqlWhere, IList<string> listEntityName = null) {
            string strFetchHQL = "";
            if (listEntityName != null && listEntityName.Count > 0)
            {
                foreach (string entityName in listEntityName)
                {
                    strFetchHQL += " left join fetch " + entityName;
                }
            }
            else
            {
                strFetchHQL = " left join fetch lbparitemsplit.LBItem lbitem " +
                              " left join fetch lbparitemsplit.ParItem paritem ";
            }
            if (strHqlWhere != null && strHqlWhere.Length > 0)
                strHqlWhere = " and " + strHqlWhere;
            strHqlWhere += " and " + BaseDataFilter.GetDataRowRoleHQLString<LBParItemSplit>();
            strHqlWhere = BaseDataFilter.FilterMacroCommand(strHqlWhere);
            string strHQL = "select lbparitemsplit from LBParItemSplit lbparitemsplit " + strFetchHQL + " where 1=1 " + strHqlWhere;
            IList<LBParItemSplit> lBParItemSplits = this.Session.CreateQuery(strHQL).List<LBParItemSplit>();
            return lBParItemSplits;


        }
    }
}