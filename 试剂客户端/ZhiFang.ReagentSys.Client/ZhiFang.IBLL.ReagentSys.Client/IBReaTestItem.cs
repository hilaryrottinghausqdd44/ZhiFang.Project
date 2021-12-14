

using System.Collections.Generic;
using System.Data;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaTestItem : IBGenericManager<ReaTestItem>
    {
        /// <summary>
        /// 试剂客户端同步LIS的检验项目信息
        /// </summary>
        /// <returns></returns>
        BaseResultBool SaveSyncReaTestItemInfo();
        bool EditVerification();
        BaseResultDataValue CheckTestItemExcelFormat(string excelFilePath, string serverPath);
        BaseResultDataValue AddReaTestItemByExcel(string excelFilePath, string serverPath);
        DataSet GetReaTestItemInfoByID(string idList, string where, string sort, string xmlPath);
    }
}