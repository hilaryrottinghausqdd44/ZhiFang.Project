using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.Response;
using ZhiFang.Digitlab.IDAO.ReagentSys;
using System.Text;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsQtyDtl : BaseBLL<ReaBmsQtyDtl>, ZhiFang.Digitlab.IBLL.ReagentSys.IBReaBmsQtyDtl
    {
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        IDReaGoodsOrgLinkDao IDReaGoodsOrgLinkDao { get; set; }

        /// <summary>
        /// 获取采购申请货品库存数量
        /// </summary>
        /// <param name="idStr">格式为"货品Id:供应商Id,货品Id2:供应商Id2"</param>
        /// <param name="goodIdStr">格式为"货品Id,货品Id2"</param>
        /// <returns></returns>
        public IList<ReaGoodsCurrentQtyVO> SearchReaGoodsCurrentQtyByGoodIdStr(string idStr, string goodIdStr)
        {
            IList<ReaGoodsCurrentQtyVO> tempList = new List<ReaGoodsCurrentQtyVO>();
            if (string.IsNullOrEmpty(goodIdStr)) return tempList;

            //key为货品ID,value为货品的ShortCode
            Dictionary<long, string> tempDictionary = new Dictionary<long, string>();
            string qtyDtlHQL = GetSearchGoodsQtyHQL(goodIdStr, ref tempDictionary);

            //string[] idStrArr = idStr.Split(',');
            //string qtyDtlHQL = GetSearchGoodsQtyHQL(idStrArr, ref tempDictionary);
            if (string.IsNullOrEmpty(qtyDtlHQL)) return tempList;
            //ZhiFang.Common.Log.Log.Debug("SearchReaGoodsCurrentQtyByGoodIdStr.HQL:" + qtyDtlHQL);
            qtyDtlHQL = string.Format("reabmsqtydtl.GoodsQty>0 and reabmsqtydtl.Visible=1 and ({0})", qtyDtlHQL);

            IList<ReaBmsQtyDtl> tempReaBmsQtyDtlList = this.SearchListByHQL(qtyDtlHQL);
            if (tempReaBmsQtyDtlList == null || tempReaBmsQtyDtlList.Count == 0) return tempList;

            string[] goodIdArr = goodIdStr.Split(',');
            for (int i = 0; i < goodIdArr.Length; i++)
            {
                long goodId = long.Parse(goodIdArr[i]);
                var goodQty = tempReaBmsQtyDtlList.Where(p => p.GoodsID == goodId);
                if (goodQty == null || goodQty.Count() == 0) continue;

                ReaGoodsCurrentQtyVO vo = new ReaGoodsCurrentQtyVO();
                vo.CurGoodsId = goodIdArr[i];
                vo.CurrentQty = goodQty.ElementAt(0).GoodsQty.ToString() + goodQty.ElementAt(0).GoodsUnit;
                //该货品的同系列的库存数量
                string shortCode = tempDictionary[goodId];
                if (!string.IsNullOrEmpty(shortCode))
                {
                    string otherGoodsQty = "";
                    foreach (var item in tempDictionary)
                    {
                        if (goodId != item.Key && item.Value == shortCode)
                        {
                            var otherGoodQty = tempReaBmsQtyDtlList.Where(p => p.GoodsID == item.Key);
                            if (otherGoodQty != null && otherGoodQty.Count() > 0)
                                otherGoodsQty += (otherGoodQty.ElementAt(0).GoodsQty.ToString() + otherGoodQty.ElementAt(0).GoodsUnit + ";");
                        }
                    }
                    vo.GoodsOtherQty = otherGoodsQty;
                }
                tempList.Add(vo);
            }
            return tempList;
        }
        /// <summary>
        /// 获取查询货品库存的HQL(ReaGoods)
        /// </summary>
        /// <param name="goodIdStr"></param>
        /// <param name="tempDictionary"></param>
        /// <returns></returns>
        private string GetSearchGoodsQtyHQL(string goodIdStr, ref Dictionary<long, string> tempDictionary)
        {
            StringBuilder searchHQL = new StringBuilder();
            StringBuilder idStrHql = new StringBuilder();
            IList<ReaGoods> tempReaGoodsList = IDReaGoodsDao.GetListByHQL(string.Format("reagoods.Visible=1 and reagoods.Id in ({0})", goodIdStr));

            StringBuilder shortCodeHql = new StringBuilder();
            foreach (var item in tempReaGoodsList)
            {
                //货品有货品系列码的,需要按货品系列码找出同系列的所有货品信息
                if (!string.IsNullOrEmpty(item.ShortCode))
                {
                    string tempHql = string.Format("(reagoods.Visible=1 and reagoods.ShortCode='{0}' and reagoods.Id!={1})", item.ShortCode, item.Id);
                    if (string.IsNullOrEmpty(shortCodeHql.ToString()))
                        shortCodeHql.Append(tempHql);
                    else
                        shortCodeHql.Append(string.Format(" or {0}", tempHql));
                }
            }
            //如果有同系列码,需要重新再获取所有相关货品信息
            if (!string.IsNullOrEmpty(shortCodeHql.ToString()))
            {
                IList<ReaGoods> tempReaGoodsList2 = IDReaGoodsDao.GetListByHQL(shortCodeHql.ToString());
                //合并且剔除重复项
                tempReaGoodsList = tempReaGoodsList.Union(tempReaGoodsList2).ToList();
            }
            foreach (var item in tempReaGoodsList)
            {
                if (!tempDictionary.ContainsKey(item.Id)) tempDictionary.Add(item.Id, item.ShortCode);
                string tempHql = string.Format("(reabmsqtydtl.GoodsQty>0 and reabmsqtydtl.GoodsID={0})", item.Id);
                if (string.IsNullOrEmpty(searchHQL.ToString()))
                    searchHQL.Append(tempHql);
                else
                    searchHQL.Append(string.Format(" or {0}", tempHql));
            }
            return searchHQL.ToString();
        }
        /// <summary>
        /// 获取查询货品库存的HQL(ReaGoodsOrgLink)
        /// </summary>
        /// /// <param name="idStrArr"></param>
        /// <param name="tempDictionary"></param>
        private string GetSearchGoodsQtyHQL(string[] idStrArr, ref Dictionary<long, string> tempDictionary)
        {
            StringBuilder searchHQL = new StringBuilder();
            StringBuilder idStrHql = new StringBuilder();
            //string curDate = DateTime.Now.ToString("yy-MM-dd");
            //(reagoodsorglink.BeginTime<='{0}' and (reagoodsorglink.EndTime is null or reagoodsorglink.EndTime>='{1}'))
            foreach (var idStr in idStrArr)
            {
                string[] ids = idStr.Split(':');
                string tempHql = string.Format("(reagoodsorglink.ReaGoods.Visible=1 and reagoodsorglink.ReaGoods.Id={0} and reagoodsorglink.CenOrg.Id={1})", ids[0], ids[1]);
                if (string.IsNullOrEmpty(idStrHql.ToString()))
                    idStrHql.Append(tempHql);
                else
                    idStrHql.Append(string.Format(" or {0}", tempHql));
            }
            IList<ReaGoodsOrgLink> tempReaGoodsList = IDReaGoodsOrgLinkDao.GetListByHQL(idStrHql.ToString());

            StringBuilder shortCodeHql = new StringBuilder();
            foreach (var item in tempReaGoodsList)
            {
                //货品有货品系列码的,需要按货品系列码找出同系列的所有货品信息
                if (!string.IsNullOrEmpty(item.ReaGoods.ShortCode))
                {
                    string tempHql = string.Format("(reagoodsorglink.ReaGoods.Visible=1 and reagoodsorglink.ReaGoods.ShortCode='{0}' and reagoodsorglink.CenOrg.Id={1} and reagoodsorglink.Id!={2})", item.ReaGoods.ShortCode, item.CenOrg.Id, item.Id);
                    if (string.IsNullOrEmpty(shortCodeHql.ToString()))
                        shortCodeHql.Append(tempHql);
                    else
                        shortCodeHql.Append(string.Format(" or {0}", tempHql));
                }
            }
            //如果有同系列码,需要重新再获取所有相关货品信息
            if (!string.IsNullOrEmpty(shortCodeHql.ToString()))
            {
                IList<ReaGoodsOrgLink> tempReaGoodsList2 = IDReaGoodsOrgLinkDao.GetListByHQL(shortCodeHql.ToString());
                //合并且剔除重复项
                tempReaGoodsList = tempReaGoodsList.Union(tempReaGoodsList2).ToList();
            }
            foreach (var item in tempReaGoodsList)
            {
                if (!tempDictionary.ContainsKey(item.ReaGoods.Id)) tempDictionary.Add(item.ReaGoods.Id, item.ReaGoods.ShortCode);
                string tempHql = string.Format("(reabmsqtydtl.GoodsQty>0 and reabmsqtydtl.GoodsID={0} and reabmsqtydtl.ReaCompanyID={1})", item.ReaGoods.Id, item.CenOrg.Id);
                if (string.IsNullOrEmpty(searchHQL.ToString()))
                    searchHQL.Append(tempHql);
                else
                    searchHQL.Append(string.Format(" or {0}", tempHql));
            }
            return searchHQL.ToString();
        }
    }
}