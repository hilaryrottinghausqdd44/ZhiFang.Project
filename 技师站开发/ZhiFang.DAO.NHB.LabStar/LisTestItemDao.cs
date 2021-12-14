using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.DAO.NHB.LabStar
{
    public class LisTestItemDao : BaseDaoNHB<LisTestItem, long>, IDLisTestItemDao
    {

        public IList<LisTestItem> QueryLisTestItemDao(string strHqlWhere, IList<string> listEntityName)
        {
            string strFetchHQL = "";
            foreach (string entityName in listEntityName)
            {
                strFetchHQL += " left join fetch " + entityName;
            }
            if (strHqlWhere != null && strHqlWhere.Length > 0)
                strHqlWhere = " and " + strHqlWhere;
            strHqlWhere += " and " + BaseDataFilter.GetDataRowRoleHQLString<LisTestItem>();
            strHqlWhere = BaseDataFilter.FilterMacroCommand(strHqlWhere);
            string strHQL = "select listestitem from LisTestItem listestitem " + strFetchHQL + " where 1=1 " + strHqlWhere;
            IList<LisTestItem> listLisTestItem = this.Session.CreateQuery(strHQL).List<LisTestItem>();
            return listLisTestItem;
        }

        public EntityList<LisTestItem> QueryLisTestItemDao(string strHqlWhere, string order, int start, int count, IList<string> listEntityName)
        {
            string strFetchHQL = "";
            string strNotFetchHQL = "";
            foreach (string entityName in listEntityName)
            {
                strFetchHQL += " left join fetch " + entityName;
                strNotFetchHQL += " left join " + entityName;
            }
            string strHQL = "select listestitem from LisTestItem listestitem " + strFetchHQL;
            string strHQLCount = "select count(*) from LisTestItem listestitem " + strNotFetchHQL;
            EntityList<LisTestItem> entityList = this.GetListByHQL(strHqlWhere, order, start, count, strHQL, strHQLCount);
            return entityList;
        }

        public IList<LisTestItem> QueryLisTestItemDao(string strHqlWhere)
        {
            if (strHqlWhere != null && strHqlWhere.Length > 0)
                strHqlWhere = " and " + strHqlWhere;
            strHqlWhere += " and " + BaseDataFilter.GetDataRowRoleHQLString<LisTestItem>();
            strHqlWhere = BaseDataFilter.FilterMacroCommand(strHqlWhere);
            string strHQL = "select listestitem from LisTestItem listestitem " +
                " left join fetch listestitem.LisTestForm listestform " +
                " left join fetch listestitem.LBItem lbitem " +
                " where 1=1 " + strHqlWhere;
            IList<LisTestItem> listLisTestItem = this.Session.CreateQuery(strHQL).List<LisTestItem>();
            return listLisTestItem;
        }

        public EntityList<LisTestItem> QueryLisTestItemDao(string strHqlWhere, string order, int start, int count)
        {
            string strHQL = "select listestitem from LisTestItem listestitem " +
                " left join fetch listestitem.LisTestForm listestform " +
                " left join fetch listestitem.LBItem lbitem ";
            string strHQLCount = "select count(*) from LisTestItem listestitem " +
                " left join listestitem.LisTestForm listestform " +
                " left join listestitem.LBItem lbitem ";
            EntityList<LisTestItem> entityList = this.GetListByHQL(strHqlWhere, order, start, count, strHQL, strHQLCount);
            return entityList;
        }

        public EntityList<LisTestItem> QuerySectionItemResultDao(string strWhere, long sectionID, long itemID, int page, int limit)
        {
            //EntityList<LisTestItem> entityList = new EntityList<LisTestItem>();  
            string strHQL = " select listestitem from LisTestItem listestitem " +
                            " left join fetch listestitem.LBItem lbitem " +
                            " left join listestitem.LisTestForm.LBSection lbsection ";
            //" where lbsection.Id="+ sectionID + " and lbitem.Id=" + itemID;
            string strHQLWhere = " listestitem.LisTestForm.LBSection.Id=" + sectionID +
                                 " and listestitem.LBItem.Id=" + itemID + strWhere;
            EntityList<LisTestItem> entityList = this.GetListByHQL(strHQLWhere, page, limit, strHQL);
            return entityList;
        }

        public string QueryCommonItemByTestFormIDDao(string testFormIDList)
        {
            string itemIDList = "";
            string[] formIDArray = testFormIDList.Split(',');
            string strSQL = "select ItemID from Lis_TestItem where TestFormID={0} and MainStatusID=0 ";
            string querySQL = "";
            foreach (string formID in formIDArray)
            {
                querySQL = querySQL == "" ? string.Format(strSQL, formID) : querySQL + " intersect " + string.Format(strSQL, formID);
            }
            IList<long> listItemID = this.Session.CreateSQLQuery(querySQL).List<long>();
            foreach (long itemID in listItemID)
                itemIDList = itemIDList == "" ? itemID.ToString() : itemIDList + "," + itemID;
            return itemIDList;
        }
    }
}