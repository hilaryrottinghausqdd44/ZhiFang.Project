

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.Base;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.Entity.Statistics;

namespace ZhiFang.WeiXin.IBLL
{
    /// <summary>
    ///
    /// </summary>
    public interface IBFinanceIncome : IBGenericManager<FinanceIncome>
    {
        ///// <summary>
        ///// 查询财务收入报表数据
        ///// </summary>
        ///// <param name="bonusFormRound"></param>
        ///// <returns></returns>
        EntityList<FinanceIncome> SearchFinanceIncomeList(UserConsumerFormSearch searchEntity, int page, int count);
        ///// <summary>
        ///// 获取财务收入报表Excel导出文件
        ///// </summary>
        ///// <param name="searchEntity"></param>
        ///// <param name="fileName"></param>
        ///// <returns></returns>
        FileStream GetExportExcelFinanceIncome(UserConsumerFormSearch searchEntity, ref string fileName);
        ///// <summary>
        ///// 获取财务收入报表Excel转PDF的文件
        ///// </summary>
        ///// <param name="searchEntity"></param>
        ///// <param name="fileName"></param>
        ///// <returns></returns>
        BaseResultDataValue GetFinanceIncomeExcelToPdfFile(UserConsumerFormSearch searchEntity, ref string fileName);
    }
}
