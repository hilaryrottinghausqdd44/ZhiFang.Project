using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.LabStar
{
    public interface IDLisEquipFormDao : IDBaseDao<LisEquipForm, long>
    {
        IList<LisEquipForm> QueryLisEquipFormDao(string strHqlWhere, IList<string> listEntityName);

        EntityList<LisEquipForm> QueryLisEquipFormDao(string strHqlWhere, string order, int start, int count, IList<string> listEntityName);

    }
}