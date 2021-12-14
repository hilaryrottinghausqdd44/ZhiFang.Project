

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
	public  interface IBBSearchAccountReportForm : ZhiFang.IBLL.Base.IBGenericManager<BSearchAccountReportForm>
	{

        List<BSearchAccountReportForm> SearchRF(long BScanningBarCodeReportFormID, string Barcode, string SearchUserName);
        List<BSearchAccountReportForm> SearchRF(string Barcode, string SearchUserName);

        List<BAccountHospitalSearchContext> SendAddWeiXinBySearchAccount();
        Dictionary<string, string> SendAddWeiXinBySearchAccountDic();

        //由于未考虑附属检测账户（亲友）的报告查询，先按照消费码进行查询（暂行办法）
        //后期如果确定了查询方式
        //1、没有附属账户，则进行OPenID关联报告查询
        //2、附属账户查询，进行附属账户关联报告查询
        //以上两种查询都需要更改微信的报告接收服务并更改BSearchAccountReportForm实体。
        ZhiFang.Entity.Base.EntityList<BSearchAccountReportForm> SearchListByUserPayCodeAndDateRoundType(string UserOpenID, UserSearchReportDataRoundType userSearchReportDateRoundType, int page, int limit);
    }
}