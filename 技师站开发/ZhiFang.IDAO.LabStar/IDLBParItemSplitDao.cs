using System.Collections.Generic;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.LabStar
{
    public interface IDLBParItemSplitDao : IDBaseDao<LBParItemSplit, long>
    {
        IList<LBParItemSplit> QueryLBParItemSplitDao(string strHqlWhere, IList<string> listEntityName );
    }
}