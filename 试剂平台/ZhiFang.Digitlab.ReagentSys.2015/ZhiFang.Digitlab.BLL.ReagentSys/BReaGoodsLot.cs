using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BReaGoodsLot : BaseBLL<ReaGoodsLot>, ZhiFang.Digitlab.IBLL.ReagentSys.IBReaGoodsLot
    {
        public BaseResultBool AddAndValid()
        {
            BaseResultBool baseResultBool = this.EditValid();
            baseResultBool.success = this.Add();
            return baseResultBool;
        }
        public BaseResultBool EditValid()
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(this.Entity.LotNo))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "货品批号不能为空!";
                return baseResultBool;
            }
            if (this.Entity.ReaGoods == null)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "货品信息不能为空!";
                return baseResultBool;
            }
            IList<ReaGoodsLot> tempList = this.SearchListByHQL(string.Format("reagoodslot.Id!={0} and reagoodslot.LotNo='{1}' and reagoodslot.ReaGoods.Id={2}", this.Entity.Id, this.Entity.LotNo, this.Entity.ReaGoods.Id));
            if (tempList != null && tempList.Count > 0)
            {

                baseResultBool.success = false;
                baseResultBool.ErrorInfo = string.Format("货品批号为{0},货品Id为{1},已经存在!", this.Entity.LotNo, this.Entity.ReaGoods.Id);
                return baseResultBool;
            }
            else
            {
                baseResultBool.success = true;
                return baseResultBool;
            }
        }
    }
}