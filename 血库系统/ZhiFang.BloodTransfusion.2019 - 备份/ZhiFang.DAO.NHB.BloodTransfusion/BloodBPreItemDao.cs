using System.Collections.Generic;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IDAO.NHB.BloodTransfusion;

namespace ZhiFang.DAO.NHB.BloodTransfusion
{
    public class BloodBPreItemDao : BaseDaoNHBServiceByString<BloodBPreItem, string>, IDBloodBPreItemDao
    {
        #region 公共
        private IList<BloodBReqForm> SearchBloodBReqFormList(string strHQL, int page, int limit)
        {
            int? start1 = null;
            int? count1 = null;
            if (page > 0)
                start1 = page;
            if (limit > 0)
                count1 = limit;
            IList<BloodBReqForm> entityList = new List<BloodBReqForm>();
            strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
            DaoNHBSearchByHqlAction<List<BloodBReqForm>, BloodBReqForm> action = new DaoNHBSearchByHqlAction<List<BloodBReqForm>, BloodBReqForm>(strHQL, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<BloodBReqForm>>(action);
            return entityList;
        }
        private int SearchBloodBReqFormCount(string strCountHQL)
        {
            strCountHQL = BaseDataFilter.FilterMacroCommand(strCountHQL);//宏命令过滤
            DaoNHBGetCountByHqlAction<BloodBReqForm> actionCount = new DaoNHBGetCountByHqlAction<BloodBReqForm>(strCountHQL);
            int count = this.HibernateTemplate.Execute<int>(actionCount);
            return count;
        }
        #endregion

        #region 护士站
        public EntityList<BloodBReqForm> SearchBloodBReqFormListByBBagCodeAndHql(string strHqlWhere, string scanCodeField, string bbagCode, string sort, int page, int limit)
        {
            EntityList<BloodBReqForm> entityList = new EntityList<BloodBReqForm>();

            StringBuilder sqlHql = new StringBuilder();
            if (string.IsNullOrEmpty(strHqlWhere)) strHqlWhere = " 1=1 ";
            if (!string.IsNullOrEmpty(bbagCode))
            {
                if (scanCodeField == "BBagCode")
                {
                    strHqlWhere = strHqlWhere + " and bloodbpreitem.BBagCode='" + bbagCode + "'";
                }
                else
                {
                    strHqlWhere = strHqlWhere + " and bloodbpreitem.B3Code='" + bbagCode + "'";
                }
            }
            sqlHql.Append(" select DISTINCT bloodbreqform from BloodBPreItem bloodbpreitem ");
            sqlHql.Append(" left join bloodbpreitem.BloodBReqForm bloodbreqform where  1=1 ");//fetch
            if (!string.IsNullOrEmpty(strHqlWhere))
            {
                sqlHql.Append(" and " + strHqlWhere);
            }
            sqlHql.Append(" and bloodbreqform.Id is not null ");
            if (!string.IsNullOrEmpty(sort))
                sqlHql.Append(" order by " + sort);
            string strHQL = sqlHql.ToString();

            var list = SearchBloodBReqFormList(sqlHql.ToString(), page, limit);
            if (list == null || list.Count <= 0)
            {
                return entityList;
            }
            entityList.list = list;

            StringBuilder countHql = new StringBuilder();
            countHql.Append(" select count(DISTINCT bloodbreqform.Id) from BloodBPreItem bloodbpreitem ");
            countHql.Append(" left join bloodbpreitem.BloodBReqForm bloodbreqform where  1=1 ");
            if (!string.IsNullOrEmpty(strHqlWhere))
            {
                countHql.Append(" and " + strHqlWhere);
            }
            countHql.Append(" and bloodbreqform.Id is not null ");

            string strHQLCount = countHql.ToString();
            int counts = SearchBloodBReqFormCount(countHql.ToString());
            entityList.count = counts;
            return entityList;

        }
        #endregion
    }
}