using System.Collections.Generic;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.DAO.NHB.LabStar
{
    public class LBItemDao : BaseDaoNHB<LBItem, long>, IDLBItemDao
    {

        public void TestDao(long id, string strHqlWhere, string Order, int limit)
        {

            this.GetCurPageByHQL(id, strHqlWhere, Order, limit);
        }

        private IList<LBItem> SearchLBItemList(string strHQL, int page, int limit)
        {
            int? start1 = null;
            int? count1 = null;
            if (page > 0)
                start1 = page;
            if (limit > 0)
                count1 = limit;
            IList<LBItem> entityList = new List<LBItem>();
            strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
            DaoNHBSearchByHqlAction<List<LBItem>, LBItem> action = new DaoNHBSearchByHqlAction<List<LBItem>, LBItem>(strHQL, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<LBItem>>(action);
            return entityList;
        }
        private int SearchLBItemCount(string strCountHQL)
        {
            strCountHQL = BaseDataFilter.FilterMacroCommand(strCountHQL);//宏命令过滤
            DaoNHBGetCountByHqlAction<LBItem> actionCount = new DaoNHBGetCountByHqlAction<LBItem>(strCountHQL);
            int count = this.HibernateTemplate.Execute<int>(actionCount);
            return count;
        }
        public EntityList<LBItem> SearchLBItemEntityListByLBSectionItemHQL(string strHqlWhere, string sort, int page, int limit)
        {
            EntityList<LBItem> entityList = new EntityList<LBItem>();
            StringBuilder sqlHql = new StringBuilder();
            sqlHql.Append(" select lbsectionitem.LBItem from LBSectionItem lbsectionitem left join  lbsectionitem.LBSection lbsection left join  lbsectionitem.LBItem lbitem where 1=1");//fetch
            if (!string.IsNullOrEmpty(strHqlWhere))
                sqlHql.Append(" and " + strHqlWhere);
            sqlHql.Append(" and " + BaseDataFilter.GetDataRowRoleHQLString<LBItem>());
            if (!string.IsNullOrEmpty(sort))
                sqlHql.Append(" order by " + sort);
            string strHQL = sqlHql.ToString();
            entityList.list = SearchLBItemList(strHQL, page, limit);

            StringBuilder countHql = new StringBuilder();
            countHql.Append(" select count(lbsectionitem.Id) from LBSectionItem lbsectionitem left join  lbsectionitem.LBSection lbsection left join  lbsectionitem.LBItem lbitem where 1=1");//fetch
            if (!string.IsNullOrEmpty(strHqlWhere))
                countHql.Append(" and " + strHqlWhere);
            countHql.Append(" and " + BaseDataFilter.GetDataRowRoleHQLString<LBItem>());
            string strCountHQL = countHql.ToString();
            entityList.count = SearchLBItemCount(strCountHQL);
            return entityList;
        }
        public EntityList<LBItem> SearchNotLBParItemSplitPLBItemListByHQL(string strHqlWhere, string sort, int page, int limit)
        {
            EntityList<LBItem> entityList = new EntityList<LBItem>();
            StringBuilder sqlHql = new StringBuilder();
            sqlHql.Append("select lbitem from LBParItemSplit lbparitemsplit left join  lbparitemsplit.ParItem paritem right join lbparitemsplit.ParItem lbitem where lbparitemsplit.ParItem is null");
            if (!strHqlWhere.Contains("lbitem.GroupType=1"))
                sqlHql.Append(" and lbitem.GroupType=1 ");
            if (!string.IsNullOrEmpty(strHqlWhere))
                sqlHql.Append(" and " + strHqlWhere);
            sqlHql.Append(" and " + BaseDataFilter.GetDataRowRoleHQLString<LBItem>());
            if (!string.IsNullOrEmpty(sort))
                sqlHql.Append(" order by " + sort);
            string strHQL = sqlHql.ToString();
            entityList.list = SearchLBItemList(strHQL, page, limit);

            StringBuilder countHql = new StringBuilder();
            countHql.Append(" select count(lbitem.Id) from LBParItemSplit lbparitemsplit left join  lbparitemsplit.ParItem paritem right join lbparitemsplit.ParItem lbitem where lbparitemsplit.ParItem is null ");
            if (!strHqlWhere.Contains("lbitem.GroupType=1"))
                countHql.Append(" and lbitem.GroupType=1 ");
            if (!string.IsNullOrEmpty(strHqlWhere))
                countHql.Append(" and " + strHqlWhere);
            countHql.Append(" and " + BaseDataFilter.GetDataRowRoleHQLString<LBItem>());
            string strCountHQL = countHql.ToString();
            entityList.count = SearchLBItemCount(strCountHQL);
            return entityList;
        }
        public EntityList<LBItem> SearchAlreadyLBParItemSplitPLBItemListByHQL(string strHqlWhere, string sort, int page, int limit)
        {
            EntityList<LBItem> entityList = new EntityList<LBItem>();
            StringBuilder sqlHql = new StringBuilder();
            sqlHql.Append(" select lbitem from LBItem lbitem where lbitem.Id in (");
            //子查询信息
            sqlHql.Append(" select paritem.Id from LBParItemSplit lbparitemsplit left join lbparitemsplit.ParItem paritem where 1=1");
            sqlHql.Append(" group by paritem.Id) ");
            if (!string.IsNullOrEmpty(strHqlWhere))
                sqlHql.Append(" and " + strHqlWhere);
            sqlHql.Append(" and " + BaseDataFilter.GetDataRowRoleHQLString<LBItem>());
            if (!string.IsNullOrEmpty(sort))
                sqlHql.Append(" order by " + sort);
            string strHQL = sqlHql.ToString();
            entityList.list = SearchLBItemList(strHQL, page, limit);

            StringBuilder countHql = new StringBuilder();
            countHql.Append(" select count(lbitem.Id)  from LBItem lbitem where lbitem.Id in (");//fetch
            countHql.Append(" select paritem.Id from LBParItemSplit lbparitemsplit left join lbparitemsplit.ParItem paritem where 1=1 ");
            countHql.Append(" group by paritem.Id) ");
            if (!string.IsNullOrEmpty(strHqlWhere))
                countHql.Append(" and " + strHqlWhere);
            countHql.Append(" and " + BaseDataFilter.GetDataRowRoleHQLString<LBItem>());

            string strCountHQL = countHql.ToString();
            entityList.count = SearchLBItemCount(strCountHQL);
            return entityList;
        }

    }
}