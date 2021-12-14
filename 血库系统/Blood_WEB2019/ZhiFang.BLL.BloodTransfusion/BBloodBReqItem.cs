
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.BloodTransfusion;

namespace ZhiFang.BLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public class BBloodBReqItem : BaseBLL<BloodBReqItem>, ZhiFang.IBLL.BloodTransfusion.IBBloodBReqItem
    {
        IDBloodstyleDao IDBloodstyleDao { get; set; }

        public IList<BloodBReqItem> SearchBloodBReqItemListByJoinHql(string where, string docHql, string bloodstyleHql, string sort, int page, int limit)
        {
            IList<BloodBReqItem> entityList = new List<BloodBReqItem>();
            entityList = ((IDBloodBReqItemDao)base.DBDao).SearchBloodBReqItemListByJoinHql(where, docHql, bloodstyleHql, sort, page, limit);
            return entityList;
        }
        public EntityList<BloodBReqItem> SearchBloodBReqItemEntityListByJoinHql(string where, string docHql, string bloodstyleHql, string sort, int page, int limit)
        {
            EntityList<BloodBReqItem> entityList = new EntityList<BloodBReqItem>();
            entityList = ((IDBloodBReqItemDao)base.DBDao).SearchBloodBReqItemEntityListByJoinHql(where, docHql, bloodstyleHql, sort, page, limit);
            return entityList;
        }
        public BaseResultDataValue AddBReqItemList(BloodBReqForm reqForm, IList<BloodBReqItem> addBreqItemList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int bloodOrder = 1;
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            foreach (BloodBReqItem entity in addBreqItemList)
            {
                if (brdv.success == false)
                {
                    brdv.ErrorInfo = "新增医嘱申请明细失败!";
                    break;
                }
                entity.BReqFormID = reqForm.Id;
                if (reqForm.DataTimeStamp == null)
                {
                    reqForm.DataTimeStamp = dataTimeStamp;
                }
                entity.BloodBReqForm = reqForm;
                entity.BloodBReqForm.DataTimeStamp = dataTimeStamp;
                entity.Bloodstyle = IDBloodstyleDao.Get(entity.BloodNo.Value);

                if (!entity.BloodOrder.HasValue)
                    entity.BloodOrder = bloodOrder;
                else
                    bloodOrder = entity.BloodOrder.Value;
                if (!entity.DispOrder.HasValue)
                    entity.DispOrder = bloodOrder;
                else
                    bloodOrder = entity.DispOrder.Value;
                entity.Visible = true;
                this.Entity = entity;
                brdv.success = this.Add();
                bloodOrder += 1;
            }

            return brdv;
        }
        public BaseResultDataValue EditBReqItemList(BloodBReqForm reqForm, IList<BloodBReqItem> editBreqItemList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            foreach (BloodBReqItem entity in editBreqItemList)
            {
                if (brdv.success == false)
                {
                    brdv.ErrorInfo = "更新医嘱申请明细失败!";
                    break;
                }
                List<string> tmpa = new List<string>();
                tmpa.Add("Id=" + entity.Id + " ");
                tmpa.Add("BReqCount=" + entity.BReqCount + " ");
                this.Entity = entity;
                brdv.success = this.Update(tmpa.ToArray());
            }

            return brdv;
        }

    }
}