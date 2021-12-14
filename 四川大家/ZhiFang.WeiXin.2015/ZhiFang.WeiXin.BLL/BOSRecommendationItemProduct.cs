using ZhiFang.BLL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.WeiXin.Entity;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity.ViewObject.Response;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BOSRecommendationItemProduct : BaseBLL<OSRecommendationItemProduct>, ZhiFang.WeiXin.IBLL.IBOSRecommendationItemProduct
    {
        public EntityList<OSRecommendationItemProductVO> SearchOSBLabTestItemByAreaID(string AreaID, int page, int limit, string sort, string where)
        {
            if (AreaID != null && AreaID.Trim() != "")
            {
                string tmpwhere = (where != null && where.Trim() != "") ? " AreaID=" + AreaID + " and " + where : " AreaID=" + AreaID + " ";
                //添加上过滤价格(折扣价格)为0的条件
                tmpwhere += " and DiscountPrice>0 and Status in (" + OSRecommendationItemProducStatus.已审核.Key + "," + OSRecommendationItemProducStatus.上架.Key + ")";
                tmpwhere += " and StartDateTime<= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and EndDateTime>= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
                var tmplist = DBDao.GetListByHQL(tmpwhere, sort, page, limit);
                return TransVO(tmplist);
            }
            else
            {
                return null;
            }
        }
        OSRecommendationItemProductVO TransVO(OSRecommendationItemProduct entity)
        {
            OSRecommendationItemProductVO osripvo = new OSRecommendationItemProductVO();
            osripvo.Id = entity.Id;
            osripvo.CName = entity.CName;
            osripvo.ItemID = entity.ItemID;
            osripvo.ItemNo = entity.ItemNo;
            osripvo.Memo = entity.Memo;
            osripvo.DispOrder = entity.DispOrder;
            osripvo.StartDateTime = entity.StartDateTime;
            osripvo.EndDateTime = entity.EndDateTime;
            osripvo.DiscountPrice = entity.DiscountPrice;
            osripvo.MarketPrice = entity.MarketPrice;
            osripvo.GreatMasterPrice = entity.GreatMasterPrice;
            osripvo.DispOrder = entity.DispOrder;
            osripvo.Image = entity.Image;
            osripvo.BonusPercent = entity.BonusPercent;
            return osripvo;
        }
        EntityList<OSRecommendationItemProductVO> TransVO(EntityList<OSRecommendationItemProduct> entity)
        {
            EntityList<OSRecommendationItemProductVO> tmplist = new EntityList<OSRecommendationItemProductVO>();
            tmplist.list = new List<OSRecommendationItemProductVO>();
            if (entity != null && entity.list != null && entity.count > 0)
            {
                tmplist.count = entity.count;
                foreach (var e in entity.list)
                {
                    OSRecommendationItemProductVO tmp = TransVO(e);
                    tmplist.list.Add(tmp);
                }
                return tmplist;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 特推项目维护查询定制
        /// </summary>
        /// <param name="where"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="effective">是否只显示有效特推项目</param>
        /// <returns></returns>
        public EntityList<OSRecommendationItemProduct> SearchOSRecommendationItemProductOrEffective(string where, int page, int count, bool effective)
        {
            return SearchListOrEffective(ref where, "", page, count, effective);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="Order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="effective">是否只显示有效特推项目</param>
        /// <returns></returns>
        public EntityList<OSRecommendationItemProduct> SearchOSRecommendationItemProductOrEffective(string where, string Order, int page, int count, bool effective)
        {
            return SearchListOrEffective(ref where, Order, page, count, effective);
        }
        /// <summary>
        /// 特推项目维护查询定制
        /// </summary>
        /// <param name="where"></param>
        /// <param name="Order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="effective">是否只显示有效特推项目</param>
        /// <returns></returns>
        private EntityList<OSRecommendationItemProduct> SearchListOrEffective(ref string where, string Order, int page, int count, bool effective)
        {
            EntityList<OSRecommendationItemProduct> tmplist = new EntityList<OSRecommendationItemProduct>();
            if (effective)
            {
                string tmpwhere = "(Status in (" + OSRecommendationItemProducStatus.已审核.Key + "," + OSRecommendationItemProducStatus.上架.Key + ")";
                tmpwhere += " and StartDateTime<= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and EndDateTime>= '" + DateTime.Now.ToString("yyyy-MM-dd") + "') ";
                where = (where != null && where.Trim() != "") ? tmpwhere + " and " + where : tmpwhere;
            }
            tmplist = this.SearchListByHQL(where, Order, page, count);
            return tmplist;
        }
    }
}