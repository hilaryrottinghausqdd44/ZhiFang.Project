using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.Base;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.Entity.Statistics;

namespace ZhiFang.WeiXin.IDAO
{
    public interface IDFinanceIncomeDao : IDBaseDao<FinanceIncome, long>
    {
        IList<FinanceIncome> SearchFinanceIncomeList(UserConsumerFormSearch searchEntity, string strWhere, int page, int count);

    }
}