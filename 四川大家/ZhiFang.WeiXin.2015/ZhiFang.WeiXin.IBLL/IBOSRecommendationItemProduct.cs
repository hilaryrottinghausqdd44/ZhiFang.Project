using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.Entity;
using ZhiFang.IBLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity.ViewObject.Response;

namespace ZhiFang.WeiXin.IBLL
{
    /// <summary>
    ///
    /// </summary>
    public interface IBOSRecommendationItemProduct : IBGenericManager<OSRecommendationItemProduct>
    {
        EntityList<OSRecommendationItemProductVO> SearchOSBLabTestItemByAreaID(string v, int page, int limit, string sort, string where);
        /// <summary>
        /// 特推项目维护查询定制
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="effective">是否只显示有效特推项目</param>
        /// <returns></returns>
        EntityList<OSRecommendationItemProduct> SearchOSRecommendationItemProductOrEffective(string strHqlWhere, int page, int count, bool effective);
        /// <summary>
        /// 特推项目维护查询定制
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="Order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="effective">是否只显示有效特推项目</param>
        /// <returns></returns>
        EntityList<OSRecommendationItemProduct> SearchOSRecommendationItemProductOrEffective(string strHqlWhere, string Order, int page, int count, bool effective);

    }
}