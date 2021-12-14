using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaGoods : IBGenericManager<ReaGoods>
    {
        BaseResultDataValue CheckGoodsExcelFormat(string excelFilePath, string serverPath);

        BaseResultDataValue AddGoodsDataFormExcel(string labID, string compID, string prodID, string excelFilePath, string serverPath);

        int GetMaxGoodsSort();
    }
}