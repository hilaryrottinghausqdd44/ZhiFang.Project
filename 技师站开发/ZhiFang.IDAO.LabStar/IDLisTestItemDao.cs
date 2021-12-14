using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.LabStar
{
    public interface IDLisTestItemDao : IDBaseDao<LisTestItem, long>
    {

        IList<LisTestItem> QueryLisTestItemDao(string strHqlWhere, IList<string> listEntityName);

        EntityList<LisTestItem> QueryLisTestItemDao(string strHqlWhere, string order, int start, int count, IList<string> listEntityName);

        IList<LisTestItem> QueryLisTestItemDao(string strHqlWhere);

        EntityList<LisTestItem> QueryLisTestItemDao(string strHqlWhere, string order, int start, int count);

        EntityList<LisTestItem> QuerySectionItemResultDao(string strWhere, long sectionID, long itemID, int page, int limit);

        string QueryCommonItemByTestFormIDDao(string testFormIDList);

    }
}