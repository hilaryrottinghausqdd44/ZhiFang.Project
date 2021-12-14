using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.LabStar
{
    public interface IDLisEquipItemDao : IDBaseDao<LisEquipItem, long>
    {

        IList<LisEquipItem> QueryLisEquipItemDao(string strHqlWhere, IList<string> listEntityName);

        EntityList<LisEquipItem> QueryLisEquipItemDao(string strHqlWhere, string order, int start, int count, IList<string> listEntityName);

        EntityList<LisEquipItem> QueryEquipItemResultDao(string strWhere, long equipID, long itemID, int page, int limit);

    }
}