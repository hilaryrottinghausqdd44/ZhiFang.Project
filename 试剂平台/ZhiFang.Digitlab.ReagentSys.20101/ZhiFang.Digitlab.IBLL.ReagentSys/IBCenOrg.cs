

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity.ReagentSys;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
{
	/// <summary>
	///
	/// </summary>
	public  interface IBCenOrg : IBGenericManager<CenOrg>
	{
        /// <summary>
        /// 获取机构表的最大OrgNo
        /// </summary>
        /// <returns></returns>
        int GetMaxOrgNo(long orgTypeID, int minOrgNo);

        int GetMaxOrgNo();

        bool ExcelSave(System.Data.DataTable dt, out string errorinfo);
    }
}