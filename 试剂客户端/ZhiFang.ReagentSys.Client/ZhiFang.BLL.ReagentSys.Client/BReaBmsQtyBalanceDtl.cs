
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsQtyBalanceDtl : BaseBLL<ReaBmsQtyBalanceDtl>, ZhiFang.IBLL.ReagentSys.Client.IBReaBmsQtyBalanceDtl
    {
        public BaseResultDataValue AddReaBmsQtyBalanceDtl(IList<ReaBmsQtyBalanceDtl> dtAddList, long empID, string empName)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            foreach (var model in dtAddList)
            {
                this.Entity = model;
                tempBaseResultDataValue.success = this.Add();
                if (tempBaseResultDataValue.success == false)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "保存库存结账明细信息失败!";
                }
                if (tempBaseResultDataValue.success == false) break;

            }
            return tempBaseResultDataValue;
        }
    }
}