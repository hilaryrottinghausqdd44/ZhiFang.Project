using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ReagentSys.Client;

namespace ZhiFang.BLL.ReagentSys.Client.QtyListGroupBy
{
    public class QtyListGroupByHelp
    {
        public static ReaBmsQtyDtl SearchStoreUpperAndLowerValue(ReaBmsQtyDtl entity, ReaGoods goods)
        {
            if (goods == null) return entity;

            if (goods.StoreUpper.HasValue)
                entity.StoreUpper = goods.StoreUpper.Value;
            if (goods.StoreLower.HasValue)
                entity.StoreLower = goods.StoreLower;
            entity.StoreLower = goods.StoreLower;
            entity.StoreLower = goods.StoreLower;
            //库存所剩理论测试数计算:库存数量乘以基础数据中每包装单位理论测试数
            if (goods.TestCount > 0)
                entity.StocSurplusTestQty = entity.GoodsQty * goods.TestCount;
            return entity;
        }
    }
}
