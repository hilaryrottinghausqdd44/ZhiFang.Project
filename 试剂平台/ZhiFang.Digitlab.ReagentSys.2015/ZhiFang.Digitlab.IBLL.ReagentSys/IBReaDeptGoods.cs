using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.Response;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaDeptGoods : IBGenericManager<ReaDeptGoods>
    {
        EntityList<ReaDeptGoods> SearchReaGoodsByHRDeptID(long deptID, string where, string sort, int page, int limit);
        /// <summary>
        /// 依部门Id获取该部门(包含子部门)下的所有部门的货品Id信息
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        string SearchReaDeptGoodsListByHRDeptId(long deptId);
    }
}