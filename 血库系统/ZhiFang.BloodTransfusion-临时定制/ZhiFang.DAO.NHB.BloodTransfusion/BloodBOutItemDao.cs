using System.Collections.Generic;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IDAO.NHB.BloodTransfusion;

namespace ZhiFang.DAO.NHB.BloodTransfusion
{
    public class BloodBOutItemDao : BaseDaoNHBServiceByString<BloodBOutItem, string>, IDBloodBOutItemDao
    {
        #region IList
        private IList<BloodBOutItem> SearchBloodBOutItemList(string strHQL, int page, int limit)
        {
            int? start1 = null;
            int? count1 = null;
            if (page > 0)
                start1 = page;
            if (limit > 0)
                count1 = limit;
            IList<BloodBOutItem> entityList = new List<BloodBOutItem>();
            strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
            DaoNHBSearchByHqlAction<List<BloodBOutItem>, BloodBOutItem> action = new DaoNHBSearchByHqlAction<List<BloodBOutItem>, BloodBOutItem>(strHQL, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<BloodBOutItem>>(action);
            return entityList;
        }
        private int SearchBloodBOutItemCount(string strCountHQL)
        {
            strCountHQL = BaseDataFilter.FilterMacroCommand(strCountHQL);//宏命令过滤
            DaoNHBGetCountByHqlAction<BloodBOutItem> actionCount = new DaoNHBGetCountByHqlAction<BloodBOutItem>(strCountHQL);
            int count = this.HibernateTemplate.Execute<int>(actionCount);
            return count;
        }
        public EntityList<BloodBOutItem> SearchBloodBOutItemOfLeftJoinByHQL(string strHqlWhere, string sort, int page, int limit)
        {
            EntityList<BloodBOutItem> entityList = new EntityList<BloodBOutItem>();
            StringBuilder sqlHql = new StringBuilder();//bloodbreqitem,
            sqlHql.Append(" select new BloodBOutItem(bloodboutitem,bloodboutform,bloodbreqform,bloodstyle,bloodabo,bloodbunit) from BloodBOutItem bloodboutitem ");
            
            sqlHql.Append(" left join fetch bloodboutitem.BloodBOutForm bloodboutform ");
            sqlHql.Append(" left join fetch bloodboutform.BloodBReqForm bloodbreqform ");
            sqlHql.Append(" left join fetch bloodboutitem.Bloodstyle bloodstyle ");
            sqlHql.Append(" left join fetch bloodboutitem.BloodABO bloodabo ");
            sqlHql.Append(" left join fetch bloodboutitem.BloodBUnit bloodbunit ");

            //sqlHql.Append(" left join fetch bloodboutitem.BloodBReqItem bloodbreqitem ");
            string strHQL = sqlHql.ToString();

            StringBuilder countHql = new StringBuilder();
            countHql.Append(" select count(bloodboutitem.Id) from BloodBOutItem bloodboutitem ");
            countHql.Append(" left join bloodboutitem.BloodBOutForm bloodboutform ");
            countHql.Append(" left join bloodboutform.BloodBReqForm bloodbreqform ");
            countHql.Append(" left join bloodboutitem.Bloodstyle bloodstyle ");
            countHql.Append(" left join bloodboutitem.BloodABO bloodabo ");
            countHql.Append(" left join bloodboutitem.BloodBUnit bloodbunit ");

            //countHql.Append(" left join bloodboutitem.BloodBReqItem bloodbreqitem ");
            string strHQLCount = countHql.ToString();
            return this.GetListByHQL(strHqlWhere, sort, page, limit, strHQL, strHQLCount);
        }
        #endregion

        #region 血制品交接登记
        public EntityList<BloodBOutItem> SearchBloodBOutItemOfHandoverByBReqVOHQL(string strHqlWhere, string sort, int page, int limit)
        {
            EntityList<BloodBOutItem> entityList = SearchBloodBOutItemOfLeftJoinByHQL(strHqlWhere, sort, page, limit);
            return entityList;
        }
        public EntityList<BloodBOutItem> SearchBloodBOutItemOfHandoverByBBagCodeHQL(string strHqlWhere, string sort, int page, int limit)
        {
            EntityList<BloodBOutItem> entityList = SearchBloodBOutItemOfLeftJoinByHQL(strHqlWhere, sort, page, limit);
            return entityList;
        }
        #endregion

        #region 血袋回收登记
        public EntityList<BloodBOutItem> SearchBloodBOutItemOfRecycleByHQL(string strHqlWhere, string sort, int page, int limit)
        {
            EntityList<BloodBOutItem> entityList = SearchBloodBOutItemOfLeftJoinByHQL(strHqlWhere, sort, page, limit);
            return entityList;
        }
        public EntityList<BloodBOutItem> SearchBloodBOutItemOfRecycleByBBagCodeHQL(string strHqlWhere, string sort, int page, int limit)
        {
            EntityList<BloodBOutItem> entityList = SearchBloodBOutItemOfLeftJoinByHQL(strHqlWhere, sort, page, limit);
            return entityList;
        }
        #endregion

    }
}