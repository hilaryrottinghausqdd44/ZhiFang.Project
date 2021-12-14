using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaGoodsBarcodeOperationDao :BaseDaoNHBService<ReaGoodsBarcodeOperation, long>, IDReaGoodsBarcodeOperationDao
    {
        public BaseResultBool UpdatePrintCount(IList<long> boxList)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (boxList == null || boxList.Count <= 0)
                return tempBaseResultBool;

            foreach (var id in boxList)
            {
                string hql = "update ReaGoodsBarcodeOperation reagoodsbarcodeoperation set reagoodsbarcodeoperation.PrintCount=(reagoodsbarcodeoperation.PrintCount+1) where reagoodsbarcodeoperation.Id=" + id;
                int counts = this.UpdateByHql(hql);
                if (counts > 0)
                    tempBaseResultBool.success = true;
                else
                    tempBaseResultBool.success = false;
            }
            return tempBaseResultBool;
        }
    }
}