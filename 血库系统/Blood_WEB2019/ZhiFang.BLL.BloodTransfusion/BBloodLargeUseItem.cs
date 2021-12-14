
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
    public class BBloodLargeUseItem : BaseBLL<BloodLargeUseItem>, ZhiFang.IBLL.BloodTransfusion.IBBloodLargeUseItem
    {
        public BaseResultDataValue AddBloodLargeUseItemList(BloodBReqForm reqForm, IList<BloodLargeUseItem> addUseItemList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //先删除医嘱申请旧的24小时内的用血申请记录
            //int counts = this.DeleteByHql("From BloodLargeUseItem bloodlargeuseitem where bloodlargeuseitem.LUFID='" + reqForm.Id + "'");
            //ZhiFang.Common.Log.Log.Warn("删除医嘱申请旧的24小时内的用血申请记录数有:" + counts);

            IList<BloodLargeUseItem> oldList = this.SearchListByHQL("bloodlargeuseitem.LUFID='" + reqForm.Id + "'");
            //foreach (var item in oldList)
            //{
            //    this.Remove(item.Id);
            //}

            int dispOrder = 1;
            foreach (BloodLargeUseItem entity in addUseItemList)
            {
                var tempList2 = oldList.Where(p => p.LUFID == entity.LUFID && p.BReqFormID == entity.BReqFormID);
                if (tempList2.Count() > 0) continue;

                if (brdv.success == false)
                {
                    brdv.ErrorInfo = "新增医嘱申请的24小时内的用血申请记录失败!";
                    break;
                }
                entity.DispOrder = dispOrder;
                entity.Visible = true;

                this.Entity = entity;
                brdv.success = this.Add();
                dispOrder += 1;
            }

            return brdv;
        }
    }
}