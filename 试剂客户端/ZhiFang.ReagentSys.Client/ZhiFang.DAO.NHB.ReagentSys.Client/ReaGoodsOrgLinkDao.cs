using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.Entity.Base;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaGoodsOrgLinkDao : BaseDaoNHB<ReaGoodsOrgLink, long>, IDReaGoodsOrgLinkDao
    {
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        public ReaGoodsOrgLink GetReaGoodsMinUnit(long orgType, long cenOrgId, long goodsId, ReaGoods reaGoods, ref string resultInfo)
        {
            ReaGoodsOrgLink reaGoodsOrgLink = null;
            if (reaGoods == null)
                reaGoods = IDReaGoodsDao.Get(goodsId);
            if (reaGoods == null)
            {
                resultInfo = string.Format("试剂Id为【{0}】,获取试剂信息为空,不进行货品的大小包装单位转换。", goodsId);
                return reaGoodsOrgLink;
            }

            double gonvertQty = reaGoods.GonvertQty;
            if (string.IsNullOrEmpty(reaGoods.ReaGoodsNo))
            {
                resultInfo = string.Format("试剂名称为【{0}】,产品编号为空,不进行货品的大小包装单位转换。", reaGoods.CName);
                return reaGoodsOrgLink;
            }
            else if (!string.IsNullOrEmpty(reaGoods.ReaGoodsNo) && gonvertQty <= 0)
            {
                resultInfo = string.Format("试剂名称为【{0}】,产品编号为【{1}】,没有维护换算比率!不进行货品的大小包装单位转换。", reaGoods.CName, reaGoods.ReaGoodsNo);
                return reaGoodsOrgLink;
            }
            else if (string.IsNullOrEmpty(reaGoods.ReaGoodsNo) && gonvertQty <= 0)
            {
                resultInfo = string.Format("试剂名称为【{0}】,产品编号为空,没有维护换算比率!不进行货品的大小包装单位转换。", reaGoods.CName);
                return reaGoodsOrgLink;
            }
            ReaGoods reaGoodsMinUnit = null;
            //找出该货品ID的最小包装单位货品信息and reagoods.IsMinUnit=1
            if (reaGoods.GonvertQty == 1)
            {
                reaGoodsMinUnit = reaGoods;
            }
            else
            {
                //reagoods.IsMinUnit=1
                IList<ReaGoods> reaGoodsList = IDReaGoodsDao.GetListByHQL(string.Format("reagoods.Visible=1 and reagoods.GonvertQty=1 and reagoods.ReaGoodsNo='{0}' and reagoods.Id!={1}", reaGoods.ReaGoodsNo, reaGoods.Id));
                if (reaGoodsList.Count == 1)
                {
                    reaGoodsMinUnit = reaGoodsList.ElementAt(0);
                }
                else
                {
                    if (reaGoodsList.Count() > 1)
                        resultInfo = string.Format("试剂名称为【{0}】,产品编号为【{1}】,试剂的最小单位信息维护有【{2}】条记录!不能进行货品的大小包装单位转换。", reaGoods.CName, reaGoods.ReaGoodsNo, reaGoodsList.Count());
                    else if (reaGoodsList.Count() <= 0)
                        resultInfo = string.Format("试剂名称为【{0}】,产品编号为【{1}】,没有维护试剂的最小单位信息!不进行货品的大小包装单位转换。", reaGoods.CName, reaGoods.ReaGoodsNo);
                }
            }
            if (reaGoodsMinUnit != null)
            {
                //判断最小包装单位的货品是否维护到了机构与货品关系表里
                IList<ReaGoodsOrgLink> goodsOrgLinkList = this.GetListByHQL(string.Format("reagoodsorglink.Visible=1  and reagoodsorglink.CenOrg.OrgType={0} and reagoodsorglink.CenOrg.Id={1} and reagoodsorglink.ReaGoods.Id={2}", orgType, cenOrgId, reaGoodsMinUnit.Id));
                if (goodsOrgLinkList.Count > 0)
                    reaGoodsOrgLink = goodsOrgLinkList.OrderBy(p => p.DispOrder).ElementAt(0);
                else
                    resultInfo = string.Format("试剂名称为【{0}】,产品编号为【{1}】,该试剂的最小单位货品信息(名称为【{2}】未维护到机构与货品关系表中!不进行货品的大小包装单位转换。", reaGoods.CName, reaGoods.ReaGoodsNo, reaGoodsMinUnit.CName);
            }
            return reaGoodsOrgLink;
        }

        public EntityList<ReaGoodsOrgLink> QueryReaGoodsOrgLinkDao(string strHqlWhere, string Order, int start, int count)
        {
            //EntityList<LBSectionItem> entityList = new EntityList<LBSectionItem>();  
            string strHQL = " select reagoodsorglink from ReaGoodsOrgLink reagoodsorglink " +
                            " left join fetch reagoodsorglink.CenOrg cenorg " +
                            " left join fetch reagoodsorglink.ReaCenOrg reacenorg " +
                            " left join fetch reagoodsorglink.ReaGoods reagoods ";

            return this.GetListByHQL(strHqlWhere, Order, start, count, strHQL);
            //return entityList;
        }
    }
}