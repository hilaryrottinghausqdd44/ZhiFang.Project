using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Response;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaGoodsDao : IDBaseDao<ReaGoods, long>
    {
        /// <summary>
        /// 获取机构货品实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hasLabId">是否按机构LabId过滤</param>
        /// <returns></returns>
        ReaGoods Get(long id, bool hasLabId);
        IList<ReaGoodsClassVO> SearchGoodsClassListByClassTypeAndHQL(string classType, bool hasNull, string whereHql, string sort, int page, int limit);
        EntityList<ReaGoodsClassVO> SearchGoodsClassEntityListByClassTypeAndHQL(string classType, bool hasNull, string whereHql, string sort, int page, int limit);
        /// <summary>
        /// 获取产品编号最大值(不能添加LabId作过滤条件)
        /// </summary>
        /// <returns></returns>
        int GetMaxGoodsSort();

        /// <summary>
        /// 获取产品编号最大值
        /// </summary>
        /// <returns></returns>
        long GetMaxGoodsId(long labID);
        /// <summary>
        /// 获取最大的时间戳，接口同步货品使用
        /// </summary>
        /// <returns></returns>
        string GetMaxTS();

        /// <summary>
        /// 货品表结合库存表，查询二级分类并返回
        /// </summary>
        /// <returns></returns>
        EntityList<ReaGoodsClassVO> SearchGoodsClassTypeJoinQtyDtl(string where, string sort, int page, int limit);
    }
}