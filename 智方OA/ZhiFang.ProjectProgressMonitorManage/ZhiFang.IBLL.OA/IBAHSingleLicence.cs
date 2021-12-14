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
    public  interface IBAHSingleLicence : IBGenericManager<AHSingleLicence>
	{
        BaseResultDataValue AHSingleLicenceAdd(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction);
        BaseResultBool AHSingleLicenceStatusUpdate(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, string[] tempArray, long EmpID, string EmpName);
        /// <summary>
        /// 获取单站点授权需要特批的数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        EntityList<AHSingleLicence> SearchSpecialApprovalAHSingleLicenceByHQL(string where, int page, int limit, string sort);
        /// <summary>
        /// 根据SQH和网卡号生成授权码
        /// </summary>
        /// <param name="entity"></param>
        string GetCreateLicenceKey(AHSingleLicence entity);
        /// <summary>
        /// 单站点新申请时,授权类型为临时时获取开始日期值处理
        /// </summary>
        /// <param name="mac">网卡号</param>
        /// <param name="sqh">程序或仪器的SQH</param>
        /// <returns></returns>
        BaseResultDataValue GetStartDateValueOfApply(string mac, string sqh);
        EntityList<AHSingleLicence> SearchListByHQL_LicenceInfo(string where, string returnSortStr, int page, int limit);
        EntityList<AHSingleLicence> SearchListByHQL_LicenceInfo(string where, int page, int limit);
    }
}