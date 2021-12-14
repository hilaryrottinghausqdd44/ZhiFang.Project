using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.OA;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.OA
{
    /// <summary>
    ///
    /// </summary>
    public  interface IBAHServerProgramLicence : IBGenericManager<AHServerProgramLicence>
	{
        int DeleteByStrId(string strId);
        /// <summary>
        /// 获取上一次的授权程序申请信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="Order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<AHServerProgramLicence> SearchPreListByHQL(string strHqlWhere, string Order, int page, int count);

    }
}