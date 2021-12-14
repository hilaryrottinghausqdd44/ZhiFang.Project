
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.IDAO.NHB.BloodTransfusion;
using ZhiFang.Entity.Base;

namespace ZhiFang.BLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public class BBloodChargeGroupItem : BaseBLL<BloodChargeGroupItem>, ZhiFang.IBLL.BloodTransfusion.IBBloodChargeGroupItem
    {
        IDBloodChargeItemDao IDBloodChargeItemDao { get; set; }

        public override bool Add()
        {
            //bloodchargegroupitem.IsUse=1 and 
            IList<BloodChargeGroupItem> tempList = ((IDBloodChargeGroupItemDao)base.DBDao).GetListByHQL("bloodchargegroupitem.GBloodChargeItem.Id=" + this.Entity.GBloodChargeItem.Id + " and bloodchargegroupitem.BloodChargeItem.Id=" + this.Entity.BloodChargeItem.Id);
            if (tempList != null && tempList.Count > 0)
            {
                ZhiFang.Common.Log.Log.Info("新增收费组合项目关系提示:组合费用项目Id为:" + this.Entity.GBloodChargeItem.Id + ",子费用项目Id为:" + this.Entity.BloodChargeItem.Id + "，已经存在！");
                return true;
            }
            else
            {
                bool a = DBDao.Save(this.Entity);
                return a;
            }
        }

        public EntityList<BloodChargeItem> SearchBloodChargeItemByChargeGItemHQL(int page, int limit, string where, string linkWhere, string sort)
        {
            EntityList<BloodChargeItem> entityList = new EntityList<BloodChargeItem>();

            IList<BloodChargeGroupItem> linkList1 = ((IDBloodChargeGroupItemDao)base.DBDao).GetListByHQL(linkWhere);
            if (linkList1 != null && linkList1.Count > 0)
            {
                IList<BloodChargeItem> entityList1 = IDBloodChargeItemDao.GetListByHQL(where, sort, -1, -1).list;
                //找出关系表里的费用项目集合信息
                var linkList = (from list2 in linkList1
                                select list2.BloodChargeItem).ToList<BloodChargeItem>();
                //比较生成两个序列的差集
                List<BloodChargeItem> entityList3 = entityList1.Except(linkList).ToList();

                entityList.count = entityList3.Count;
                //进行分页
                if (limit > 0 && limit < entityList3.Count)
                {
                    int startIndex = limit * (page - 1);
                    int endIndex = limit;
                    var list = entityList3.Skip(startIndex).Take(endIndex);
                    if (list != null)
                        entityList3 = list.ToList();
                }
                entityList.list = entityList3;
            }
            else
            {
                entityList = IDBloodChargeItemDao.GetListByHQL(where, sort, page, limit);
            }

            return entityList;
        }

    }
}