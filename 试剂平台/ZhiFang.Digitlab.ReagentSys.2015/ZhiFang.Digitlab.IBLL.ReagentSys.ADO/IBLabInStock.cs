using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IBLL.ReagentSys.ADO
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLabInStock
    {
        BaseResultDataValue CheckDataBaseLink(string orgNo, string orgName);

        BaseResultDataValue GetLabInStockCount(string orgNo, string orgName, string goodsID, string goodsNo, string prodGoodsNo, string goodsLotNo);

        BaseResultDataValue GetTestConsumeCountResult(string orgNo, string orgName, string beginDate, string endDate, string goodsID, string goodsNo, string prodGoodsNo, string goodsLotNo);

        }
}
