
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.WebAssist;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.WebAssist;

namespace ZhiFang.BLL.WebAssist
{
	/// <summary>
	///
	/// </summary>
	public  class BSCRecordItemLink : BaseBLL<SCRecordItemLink>, ZhiFang.IBLL.WebAssist.IBSCRecordItemLink
	{
        IDSCRecordTypeItemDao IDSCRecordTypeItemDao { get; set; }
        public EntityList<SCRecordTypeItem> SearchSCRecordTypeItemByLinkHQL(int page, int limit, string where, string linkWhere, string sort)
        {
            EntityList<SCRecordTypeItem> entityList = new EntityList<SCRecordTypeItem>();

            IList<SCRecordItemLink> linkList1 = ((IDSCRecordItemLinkDao)base.DBDao).GetListByHQL(linkWhere);

            if (linkList1 != null && linkList1.Count > 0)
            {
                IList<SCRecordTypeItem> entityList1 = IDSCRecordTypeItemDao.GetListByHQL(where, sort, -1, -1).list;
                //找出关系表里的费用项目集合信息
                var linkList = (from list2 in linkList1
                                select list2.SCRecordTypeItem).ToList<SCRecordTypeItem>();
                //比较生成两个序列的差集
                List<SCRecordTypeItem> entityList3 = entityList1.Except(linkList).ToList();

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
                entityList = IDSCRecordTypeItemDao.GetListByHQL(where, sort, page, limit);
            }

            return entityList;
        }

    }
}