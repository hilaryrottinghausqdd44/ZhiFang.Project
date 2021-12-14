using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaGoodsOrgLinkDao : IDBaseDao<ReaGoodsOrgLink, long>
    {
        /// <summary>
        /// 依货品ID查找出该货品在供/订货方与货品关系表的最小单位货品信息
        /// </summary>
        /// <param name="orgType">机构类型</param>
        /// <param name="cenOrgId">机构Id</param>
        /// <param name="goodsId">业务明细所属的货品ID</param>
        /// <param name="reaGoods">业务明细所属的货品实体(冗余,可为空)</param>
        /// <param name="resultInfo"></param>
        /// <returns></returns>
        ReaGoodsOrgLink GetReaGoodsMinUnit(long orgType, long cenOrgId, long goodsId, ReaGoods reaGoods, ref string resultInfo);

        EntityList<ReaGoodsOrgLink> QueryReaGoodsOrgLinkDao(string strHqlWhere, string Order, int start, int count);
    }
}