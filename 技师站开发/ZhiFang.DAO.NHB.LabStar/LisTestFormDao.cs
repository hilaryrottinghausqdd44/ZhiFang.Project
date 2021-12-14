using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.DAO.NHB.LabStar
{
    public class LisTestFormDao : BaseDaoNHB<LisTestForm, long>, IDLisTestFormDao
    {

        public IList<LisTestForm> QueryLisTestFormDao(string strHqlWhere, IList<string> listEntityName)
        {
            string strFetchHQL = "";
            foreach (string entityName in listEntityName)
            {
                strFetchHQL += " left join fetch " + entityName;
            }
            if (strHqlWhere != null && strHqlWhere.Length > 0)
                strHqlWhere = " and " + strHqlWhere;
            strHqlWhere += " and " + BaseDataFilter.GetDataRowRoleHQLString<LisTestForm>();
            strHqlWhere = BaseDataFilter.FilterMacroCommand(strHqlWhere);
            string strHQL = "select listestform from LisTestForm listestform " + strFetchHQL + " where 1=1 " + strHqlWhere;
            IList<LisTestForm> listLisTestForm = this.Session.CreateQuery(strHQL).List<LisTestForm>();
            return listLisTestForm;
        }

        public EntityList<LisTestForm> QueryLisTestFormDao(string strHqlWhere, string order, int start, int count, IList<string> listEntityName)
        {
            string strFetchHQL = "";
            string strNotFetchHQL = "";
            foreach (string entityName in listEntityName)
            {
                strFetchHQL += " left join fetch " + entityName;
                strNotFetchHQL += " left join " + entityName;
            }
            string strHQL = "select listestform from LisTestForm listestform " + strFetchHQL;
            string strHQLCount = "select count(*) from LisTestForm listestform " + strNotFetchHQL;
            EntityList<LisTestForm> entityList = this.GetListByHQL(strHqlWhere, order, start, count, strHQL, strHQLCount);
            return entityList;
        }
        public EntityPageList<LisTestForm> QueryLisTestFormCurPageDao(long id, string strHqlWhere, string order, int start, int count, IList<string> listEntityName)
        {
            string strFetchHQL = "";
            string strNotFetchHQL = "";
            foreach (string entityName in listEntityName)
            {
                strFetchHQL += " left join fetch " + entityName;
                strNotFetchHQL += " left join " + entityName;
            }
            string strHQL = "select listestform from LisTestForm listestform " + strFetchHQL;
            string strHQLCount = "select count(*) from LisTestForm listestform " + strNotFetchHQL;
            int curPage = this.GetCurPageByHQL(id, strHqlWhere, order, count);
            if (curPage > 0)
                start = curPage;
            EntityPageList<LisTestForm> entityList = this.GetListByHQL(id, strHqlWhere, order, start, count, strHQL, strHQLCount);
            return entityList;
        }

        public IList<LBMergeItemFormVO> QueryItemMergeFormInfoDao(string strHqlWhere)
        {
            if (strHqlWhere != null && strHqlWhere.Length > 0)
                strHqlWhere = " and " + strHqlWhere;
            strHqlWhere += " and " + BaseDataFilter.GetDataRowRoleHQLString<LisTestForm>();
            strHqlWhere = BaseDataFilter.FilterMacroCommand(strHqlWhere);
            string strHQL = "select new LBMergeItemFormVO(listestform.PatNo, listestform.CName, listestform.StatusID as IsMerge, count(listestitem.LBItem.Id) as ItemCount) " +
                " from LisTestItem listestitem " +
                " left join listestitem.LisTestForm listestform where 1=1 " + strHqlWhere +
                " group by listestform.PatNo, listestform.CName, listestform.StatusID";
            IList<LBMergeItemFormVO> listLBMergeItemFormVO = this.Session.CreateQuery(strHQL).List<LBMergeItemFormVO>();
            return listLBMergeItemFormVO;
        }

        public IList<LBMergeItemVO> QueryItemMergeItemInfoDao(string strHqlWhere)
        {
            if (strHqlWhere != null && strHqlWhere.Length > 0)
                strHqlWhere = " and " + strHqlWhere;
            strHqlWhere += " and " + BaseDataFilter.GetDataRowRoleHQLString<LisTestForm>();
            strHqlWhere = BaseDataFilter.FilterMacroCommand(strHqlWhere);
            string strHQL = "select new LBMergeItemVO(listestitem) " +
                " from LisTestItem listestitem " +
                " left join fetch listestitem.LisTestForm listestform " +
                " left join fetch listestitem.LBItem lbitem " +
                " where 1=1 " + strHqlWhere;
            IList<LBMergeItemVO> listLisTestItem = this.Session.CreateQuery(strHQL).List<LBMergeItemVO>();
            return listLisTestItem;
        }

    }
}