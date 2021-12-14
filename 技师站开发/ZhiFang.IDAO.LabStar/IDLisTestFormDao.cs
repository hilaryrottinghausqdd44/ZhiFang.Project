using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.LabStar
{
    public interface IDLisTestFormDao : IDBaseDao<LisTestForm, long>
    {
        IList<LisTestForm> QueryLisTestFormDao(string strHqlWhere, IList<string> listEntityName);

        EntityList<LisTestForm> QueryLisTestFormDao(string strHqlWhere, string order, int start, int count, IList<string> listEntityName);

        EntityPageList<LisTestForm> QueryLisTestFormCurPageDao(long id, string strHqlWhere, string order, int start, int count, IList<string> listEntityName);

        IList<LBMergeItemFormVO> QueryItemMergeFormInfoDao(string strHqlWhere);

        IList<LBMergeItemVO> QueryItemMergeItemInfoDao(string strHqlWhere);

    }
}