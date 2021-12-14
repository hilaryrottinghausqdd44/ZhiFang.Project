using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.WebAssist;
using ZhiFang.IDAO.NHB.WebAssist;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.BLL.WebAssist
{
	/// <summary>
	///
	/// </summary>
	public  class BGKDeptAutoCheckLink : BaseBLL<GKDeptAutoCheckLink>, IBGKDeptAutoCheckLink
	{
        IDDepartmentDao IDDepartmentDao { get; set; }

        public EntityList<Department> SearchDepartmentByLinkHQL(int page, int limit, string where, string linkWhere, string sort)
        {
            EntityList<Department> entityList = new EntityList<Department>();

            IList<GKDeptAutoCheckLink> linkList1 = ((IDGKDeptAutoCheckLinkDao)base.DBDao).GetListByHQL(linkWhere);

            if (linkList1 != null && linkList1.Count > 0)
            {
                IList<Department> entityList1 = IDDepartmentDao.GetListByHQL(where, sort, -1, -1).list;
                //找出关系表里的科室集合信息
                var linkList = (from list2 in linkList1
                                select list2.Department).ToList<Department>();
                //比较生成两个序列的差集
                List<Department> entityList3 = entityList1.Except(linkList).ToList();

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
                entityList = IDDepartmentDao.GetListByHQL(where, sort, page, limit);
            }

            return entityList;
        }

    }
}
