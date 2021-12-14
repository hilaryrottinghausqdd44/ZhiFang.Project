

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
	public  interface IBBAccountHospitalSearchContext : ZhiFang.IBLL.Base.IBGenericManager<BAccountHospitalSearchContext>
    {

        List<BAccountHospitalSearchContext> SearchListBySearchAccountId(long SearchAccountId);

        List<BSearchAccountReportForm> SearchRFListBySearchAccountId(long SearchAccountId, string OpenID, string Name, int page, int count);

        bool AddSearchContextByHSearchID();

        List<BSearchAccountReportForm> SearchAccountReportFormByReportFormIndexIdList(string ReportFormIndexIdList);

        BSearchAccountReportForm UpdateSearchAccountReportFormByReportFormIndexId(string ReportFormIndexId, long SearchAccountId);
    }
}