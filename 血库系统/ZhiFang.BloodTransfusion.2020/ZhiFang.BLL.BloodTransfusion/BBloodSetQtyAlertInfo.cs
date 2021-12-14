
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
	public  class BBloodSetQtyAlertInfo : BaseBLL<BloodSetQtyAlertInfo>, ZhiFang.IBLL.BloodTransfusion.IBBloodSetQtyAlertInfo
	{
        IDBloodStyleDao IDBloodStyleDao { get; set; }

        public EntityList<BloodStyle> SearchBloodStyleBySetQtyAlertInfoHQL(int page, int limit, string where, string linkWhere, string sort)
        {
            EntityList<BloodStyle> entityList = new EntityList<BloodStyle>();

            IList<BloodSetQtyAlertInfo> linkList1 = ((IDBloodSetQtyAlertInfoDao)base.DBDao).GetListByHQL(linkWhere);
            if (linkList1 != null && linkList1.Count > 0)
            {
                IList<BloodStyle> entityList1 = IDBloodStyleDao.GetListByHQL(where, sort, -1, -1).list;
                //找出关系表里的血制品集合信息
                var linkList = (from list2 in linkList1
                                select list2.BloodStyle).ToList<BloodStyle>();
                //比较生成两个序列的差集
                List<BloodStyle> entityList3 = entityList1.Except(linkList).ToList();

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
                entityList = IDBloodStyleDao.GetListByHQL(where, sort, page, limit);
            }

            return entityList;
        }

    }
}