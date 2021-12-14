using System.Collections.Generic;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IDAO.NHB.BloodTransfusion;

namespace ZhiFang.DAO.NHB.BloodTransfusion
{
    public class BloodInterfaceMapingDao : BaseDaoNHB<BloodInterfaceMaping, long>, IDBloodInterfaceMapingDao
    {
        #region 公共
        private IList<BDictMapingVO> SearchBDictMapingVOList(string strHQL, int page, int limit)
        {
            int? start1 = null;
            int? count1 = null;
            if (page > 0)
                start1 = page;
            if (limit > 0)
                count1 = limit;
            IList<BDictMapingVO> entityList = new List<BDictMapingVO>();
            strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
            DaoNHBSearchByHqlAction<List<BDictMapingVO>, BDictMapingVO> action = new DaoNHBSearchByHqlAction<List<BDictMapingVO>, BDictMapingVO>(strHQL, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<BDictMapingVO>>(action);
            return entityList;
        }
        private int SearchBDictMapingVOCount(string strCountHQL)
        {
            strCountHQL = BaseDataFilter.FilterMacroCommand(strCountHQL);//宏命令过滤
            DaoNHBGetCountByHqlAction<BloodBReqForm> actionCount = new DaoNHBGetCountByHqlAction<BloodBReqForm>(strCountHQL);
            int count = this.HibernateTemplate.Execute<int>(actionCount);
            return count;
        }
        #endregion

        public EntityList<BDictMapingVO> SearchInterfaceMapingJoinBDictByHql(string where, string sort, int page, int limit)
        {
            EntityList<BDictMapingVO> entityList = new EntityList<BDictMapingVO>();
            entityList.list = new List<BDictMapingVO>();

            IList<BDictMapingVO> list = new List<BDictMapingVO>();
            StringBuilder sqlHql = new StringBuilder();

            sqlHql.Append(" from BDictMapingVO bdictmapingvo left join bdictmapingvo.BDict where 1=1 ");
            if (!string.IsNullOrEmpty(where))
            {
                sqlHql.Append(" and " + where);
            }
            if (!string.IsNullOrEmpty(sort))
                sqlHql.Append(" order by " + sort);

            list = SearchBDictMapingVOList(sqlHql.ToString(), page, limit);

            if (list == null || list.Count <= 0)
            {
                return entityList;
            }
            entityList.list = list;

            StringBuilder strCountHQL = new StringBuilder();
            strCountHQL.Append(" select count(bdictmapingvo.BDict.Id) from BDictMapingVO bdictmapingvo left join bdictmapingvo.BDict where 1=1 ");

            if (!string.IsNullOrEmpty(where))
            {
                strCountHQL.Append(" and " + where);
            }
            int counts = SearchBDictMapingVOCount(strCountHQL.ToString());
            entityList.count = counts;
            return entityList;
        }
    }
}