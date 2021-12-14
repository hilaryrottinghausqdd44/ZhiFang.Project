

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
	/// <summary>
	///
	/// </summary>
	public  interface IBReaBmsSerial : IBGenericManager<ReaBmsSerial>
	{
        string GetNextBarCode(long labId,string bmsType, CenOrg cenOrg, ref long maxBarCode);
        /// <summary>
        /// 默认不添加按LabID的过滤条件获取数据
        /// </summary>
        IList<ReaBmsSerial> GetListOfNoLabIDByHql(string hqlWhere);
    }
}