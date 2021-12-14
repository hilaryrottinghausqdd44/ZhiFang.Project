using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.OA;
using ZhiFang.Entity.OA.ViewObject.Response;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.OA
{
    /// <summary>
    ///
    /// </summary>
    public  interface IBAHServerLicence : IBGenericManager<AHServerLicence>
	{
        BaseResultDataValue UploadAHServerLicenceFile( HttpPostedFile file, long pclientID, string licenceCode);
        BaseResultDataValue AddAHAHServerLicenceAndDetails(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, ApplyAHServerLicence entity);
        /// <summary>
        /// 修改服务器授权信息及明细信息(包括手工追加的程序明细)
        /// </summary>
        /// <param name="pushWeiXinMessageAction"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        BaseResultBool UpdateAHServerLicenceAndDetails(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, ApplyAHServerLicence updateEntity, string[] tempArray, long EmpID, string EmpName);

        BaseResultBool AHServerLicenceStatusUpdate(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, string[] tempArray, long EmpID, string EmpName);
        /// <summary>
        /// 获取服务器授权需要特批的数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        EntityList<AHServerLicence> SearchSpecialApprovalAHServerLicenceByHQL(string where, int page, int limit, string sort);
        /// <summary>
        /// 获取服务器授权信息及明细信息数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       BaseResultDataValue SearchAHServerLicenceAndAndDetailsById(long id);
        /// <summary>
        /// 下载服务器授权文件
        /// </summary>
        /// <param name="entity"></param>
        FileStream DownLoadAHServerLicenceFile(long id, ref string fileName);
        /// <summary>
        /// 重新生成服务器授权返回文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResultBool RegenerateAHServerLicenceById(long id);

        EntityList<AHServerLicence> SearchListByHQL_LicenceInfo(string where, string returnSortStr, int page, int limit);
        EntityList<AHServerLicence> SearchListByHQL_LicenceInfo(string where, int page, int limit);

        EntityList<AHServerLicence> SearchListByDocAndDtlHQL_LicenceInfo(string where, string dtlWhere, string returnSortStr, int page, int limit);
        EntityList<AHServerLicence> SearchListByDocAndDtlHQL_LicenceInfo(string where, string dtlWhere, int page, int limit);
    }
}