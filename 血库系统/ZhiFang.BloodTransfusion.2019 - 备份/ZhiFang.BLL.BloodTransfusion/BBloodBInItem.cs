
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.Entity.Base;

namespace ZhiFang.BLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public class BBloodBInItem : BaseBLL<BloodBInItem, string>, ZhiFang.IBLL.BloodTransfusion.IBBloodBInItem
    {
        public EntityList<BloodBInItem> SearchBloodBInItemByBReqFormID(string reqFormId, string sort, int page, int limit)
        {
            EntityList<BloodBInItem> tempEntityList = new EntityList<BloodBInItem>();

            return tempEntityList;
        }
    }
}