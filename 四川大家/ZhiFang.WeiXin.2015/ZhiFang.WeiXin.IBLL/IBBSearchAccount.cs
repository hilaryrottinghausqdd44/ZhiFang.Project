

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.Entity;

namespace ZhiFang.WeiXin.IBLL
{
	/// <summary>
	///
	/// </summary>
    public interface IBBSearchAccount : ZhiFang.IBLL.Base.IBGenericManager<BSearchAccount>
    {
        bool Add(List<BAccountHospitalSearchContext> bahscl);

        bool Update(List<BAccountHospitalSearchContext> bahscl);

        List<BSearchAccount> SearchSearchAccountVOListByHQL(long SearchAccountId, string OpenID);

        
    }
}