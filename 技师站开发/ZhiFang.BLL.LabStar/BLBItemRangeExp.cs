using System.Collections.Generic;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBItemRangeExp : BaseBLL<LBItemRangeExp>, ZhiFang.IBLL.LabStar.IBLBItemRangeExp
    {

        public IList<LBItemRangeExp> QueryLBItemRangeExpByItemID(long ItemID)
        {

            return this.SearchListByHQL(" lbitemrangeexp.LBItem.Id=" + ItemID);

        }
    }
}