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
    public interface IBReaGoodsOrgLink : IBGenericManager<ReaGoodsOrgLink>
    {
        /// <summary>
        /// 获取部门货品的全部货品与货品所属供应商信息(按货品进行分组,找出每组货品下的对应的供应商信息)
        /// </summary>
        /// <param name="goodIdStr"></param>
        /// <returns></returns>
        IList<ReaGoodsCenOrgVO> SearchReaCenOrgGoodsListByGoodIdStr(string goodIdStr);
        void AddReaReqOperation(ReaGoodsOrgLink entity, long empID, string empName, int status);
    }
}