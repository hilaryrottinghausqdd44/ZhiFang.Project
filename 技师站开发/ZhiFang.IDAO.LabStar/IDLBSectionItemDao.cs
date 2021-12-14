using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.LabStar
{
    public interface IDLBSectionItemDao : IDBaseDao<LBSectionItem, long>
    {
        IList<LBSectionItem> QueryLBSectionItemDao(string strHqlWhere);

        EntityList<LBSectionItem> QueryLBSectionItemDao(string strHqlWhere, string Order, int start, int count);

        EntityList<LBSectionItemVO> QueryLBSectionItemVODao(string strHqlWhere, string Order, int start, int count);
    }

    public interface IDLBSectionItemVODao : IDBaseDao<LBSectionItemVO, long>
    {
        EntityList<LBSectionItemVO> QueryLBSectionItemVODao(string strHqlWhere, string Order, int start, int count);

        IList<LBSectionItemVO> QueryLBSectionItemVODao(string strHqlWhere);

    }
}