using System.Collections.Generic;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IDAO.NHB.BloodTransfusion;

namespace ZhiFang.DAO.NHB.BloodTransfusion
{	
	public class BloodTransRecordTypeItemDao : BaseDaoNHB<BloodTransRecordTypeItem, long>, IDBloodTransRecordTypeItemDao
	{
        #region IList
        private IList<BloodTransRecordTypeItem> SearchBloodTransRecordTypeItemList(string strHQL, int page, int limit)
        {
            int? start1 = null;
            int? count1 = null;
            if (page > 0)
                start1 = page;
            if (limit > 0)
                count1 = limit;
            IList<BloodTransRecordTypeItem> entityList = new List<BloodTransRecordTypeItem>();
            strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
            DaoNHBSearchByHqlAction<List<BloodTransRecordTypeItem>, BloodTransRecordTypeItem> action = new DaoNHBSearchByHqlAction<List<BloodTransRecordTypeItem>, BloodTransRecordTypeItem>(strHQL, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<BloodTransRecordTypeItem>>(action);
            return entityList;
        }
        private int SearchBloodTransRecordTypeItemCount(string strCountHQL)
        {
            strCountHQL = BaseDataFilter.FilterMacroCommand(strCountHQL);//宏命令过滤
            DaoNHBGetCountByHqlAction<BloodTransRecordTypeItem> actionCount = new DaoNHBGetCountByHqlAction<BloodTransRecordTypeItem>(strCountHQL);
            int count = this.HibernateTemplate.Execute<int>(actionCount);
            return count;
        }
        public EntityList<BloodTransRecordTypeItem> SearchBloodTransRecordTypeItemOfLeftJoinByHQL(string strHqlWhere, string sort, int page, int limit)
        {
            EntityList<BloodTransRecordTypeItem> entityList = new EntityList<BloodTransRecordTypeItem>();
            StringBuilder sqlHql = new StringBuilder();
            sqlHql.Append(" select bloodtransrecordtypeitem from BloodTransRecordTypeItem bloodtransrecordtypeitem ");
            sqlHql.Append(" left join fetch bloodtransrecordtypeitem.BloodTransRecordType bloodtransrecordtype ");

            sqlHql.Append(" where 1=1 ");
            if (!string.IsNullOrEmpty(strHqlWhere))
                sqlHql.Append(" and " + strHqlWhere);
            sqlHql.Append(" and " + BaseDataFilter.GetDataRowRoleHQLString());
            if (!string.IsNullOrEmpty(sort))
                sqlHql.Append(" order by " + sort);
            string strHQL = sqlHql.ToString();
            entityList.list = SearchBloodTransRecordTypeItemList(strHQL, page, limit);

            StringBuilder countHql = new StringBuilder();
            countHql.Append(" select count(bloodtransrecordtypeitem.Id) from BloodTransRecordTypeItem bloodtransrecordtypeitem ");
            countHql.Append(" left join bloodtransrecordtypeitem.BloodTransRecordType bloodtransrecordtype ");

            countHql.Append(" where 1=1 ");
            if (!string.IsNullOrEmpty(strHqlWhere))
                countHql.Append(" and " + strHqlWhere);
            countHql.Append(" and " + BaseDataFilter.GetDataRowRoleHQLString());

            string strCountHQL = countHql.ToString();
            entityList.count = SearchBloodTransRecordTypeItemCount(strCountHQL);

            return entityList;
        }
        #endregion
    }
}