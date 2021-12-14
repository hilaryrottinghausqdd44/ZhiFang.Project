
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Statistics;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaEquipTestItemReaGoodLink : BaseBLL<ReaEquipTestItemReaGoodLink>, ZhiFang.IBLL.ReagentSys.Client.IBReaEquipTestItemReaGoodLink
    {
        IBReaLisTestStatisticalResults IBReaLisTestStatisticalResults { get; set; }
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        IDReaTestEquipLabDao IDReaTestEquipLabDao { get; set; }
        IDReaTestItemDao IDReaTestItemDao { get; set; }
        IDReaBmsOutDtlDao IDReaBmsOutDtlDao { get; set; }

        public IList<ReaEquipTestItemReaGoodLink> SearchNewListByHQL(string where, string sort, int page, int limit)
        {
            IList<ReaEquipTestItemReaGoodLink> entityList = new List<ReaEquipTestItemReaGoodLink>();
            entityList = ((IDReaEquipTestItemReaGoodLinkDao)base.DBDao).SearchNewListByHQL(where, sort, page, limit);
            return entityList;
        }
        public EntityList<ReaEquipTestItemReaGoodLink> SearchNewEntityListByHQL(string where, string sort, int page, int limit)
        {
            EntityList<ReaEquipTestItemReaGoodLink> entityList = new EntityList<ReaEquipTestItemReaGoodLink>();
            entityList = ((IDReaEquipTestItemReaGoodLinkDao)base.DBDao).SearchNewEntityListByHQL(where, sort, page, limit);
            return entityList;
        }
        public EntityList<ConsumptionComparisonAnalysisVO> SearchConsumptionComparisonAnalysisVOListByHql(int statisticType, string startDate, string endDate, string equipIdStr, string goodsIdStr, int page, int limit, string sortType, bool isMergeOfItem)
        {
            EntityList<ConsumptionComparisonAnalysisVO> entityList = new EntityList<ConsumptionComparisonAnalysisVO>();
            if (string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
            {
                ZhiFang.Common.Log.Log.Info("消耗比对分析统计:" + ",统计仪器ID范围为:" + equipIdStr + ",统计试剂ID范围为:" + goodsIdStr + ",统计日期范围信息为空!");
                return entityList;
            }
            if (!string.IsNullOrEmpty(startDate))
                startDate = DateTime.Parse(startDate).ToString("yyyy-MM-dd 00:00:00");
            if (!string.IsNullOrEmpty(endDate))
                endDate = DateTime.Parse(endDate).ToString("yyyy-MM-dd 23:59:59");
            #region 仪器项目试剂统计信息
            StringBuilder linkHql = new StringBuilder();
            linkHql.Append(" reaequiptestitemreagoodlink.Visible=1 ");
            if (!string.IsNullOrEmpty(equipIdStr))
            {
                linkHql.Append(" and reaequiptestitemreagoodlink.TestEquipID in (");
                linkHql.Append(equipIdStr.TrimEnd(','));
                linkHql.Append(")");
            }
            if (!string.IsNullOrEmpty(goodsIdStr))
            {
                linkHql.Append(" and reaequiptestitemreagoodlink.GoodsID in (");
                linkHql.Append(goodsIdStr.TrimEnd(','));
                linkHql.Append(")");
            }
            IList<ReaEquipTestItemReaGoodLink> linkList = this.SearchListByHQL(linkHql.ToString());
            if (linkList.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Info("消耗比对分析统计:开始日期为:" + startDate + "-" + endDate + ",统计仪器ID范围为:" + equipIdStr + ",统计试剂ID范围为:" + goodsIdStr + ",仪器项目试剂维护信息为空!");
                return entityList;
            }
            #endregion
            IList<ConsumptionComparisonAnalysisVO> tempList = new List<ConsumptionComparisonAnalysisVO>();
            if (sortType == "1")//按仪器试剂项目排序
                linkList = linkList.OrderBy(p => p.TestEquipID).ThenBy(p => p.GoodsID).ThenBy(p => p.TestItemID).ToList();
            else if (sortType == "2")//按仪器项目试剂排序
                linkList = linkList.OrderBy(p => p.TestEquipID).ThenBy(p => p.TestItemID).ThenBy(p => p.GoodsID).ToList();
            //统计类型:1为理论消耗量;2为消耗比对分析;
            if (statisticType == 1)
                tempList = SearchTheoryConsumptionVOListByHql(linkList, sortType, startDate, endDate, equipIdStr, goodsIdStr, isMergeOfItem);
            else if (statisticType == 2)
                tempList = SearchConsumptionComparisonVOListByHql(linkList, sortType, startDate, endDate, equipIdStr, goodsIdStr, isMergeOfItem);

            if (tempList == null || tempList.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Info("消耗比对分析统计:开始日期为:" + startDate + "-" + endDate + ",统计仪器ID范围为:" + equipIdStr + ",统计试剂ID范围为:" + goodsIdStr + ",获取统计数据信息为空!");
                return entityList;
            }
            entityList.count = tempList.Count();
            //分页处理
            if (limit > 0 && limit < tempList.Count)
            {
                int startIndex = limit * (page - 1);
                int endIndex = limit;
                var list = tempList.Skip(startIndex).Take(endIndex);
                if (list != null)
                    tempList = list.ToList();
            }
            entityList.list = tempList;
            return entityList;
        }
        /// <summary>
        /// 理论消耗量
        /// </summary>
        /// <param name="linkList"></param>
        /// <param name="groupType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="equipIdStr"></param>
        /// <param name="goodsIdStr"></param>
        /// <param name="isMergeOfItem"></param>
        /// <returns></returns>
        private IList<ConsumptionComparisonAnalysisVO> SearchTheoryConsumptionVOListByHql(IList<ReaEquipTestItemReaGoodLink> linkList, string sortType, string startDate, string endDate, string equipIdStr, string goodsIdStr, bool isMergeOfItem)
        {
            IList<ConsumptionComparisonAnalysisVO> entityList = new List<ConsumptionComparisonAnalysisVO>();
            if (string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
            {
                ZhiFang.Common.Log.Log.Info("理论消耗量统计:" + ",统计仪器ID范围为:" + equipIdStr + ",统计试剂ID范围为:" + goodsIdStr + ",统计日期范围信息为空!");
                return entityList;
            }
            #region 统计各仪器项目实际检测量
            var groupByList = linkList.GroupBy(p => new
            {
                p.TestEquipID,
                p.TestItemID
            });
            StringBuilder lisResultsHql = new StringBuilder();
            lisResultsHql.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(startDate))
            {
                lisResultsHql.Append(" and realisteststatisticalresults.TestDate>='");
                lisResultsHql.Append(startDate);
                lisResultsHql.Append("'");
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                lisResultsHql.Append(" and realisteststatisticalresults.TestDate<='");
                lisResultsHql.Append(endDate);
                lisResultsHql.Append("'");
            }
            if (!string.IsNullOrEmpty(equipIdStr))
            {
                lisResultsHql.Append(" and realisteststatisticalresults.TestEquipID in (");
                lisResultsHql.Append(equipIdStr.TrimEnd(','));
                lisResultsHql.Append(")");
            }

            IList<string> lisListHql = new List<string>();
            foreach (var groupBy in groupByList)
            {
                lisListHql.Add(" (realisteststatisticalresults.TestEquipID=" + groupBy.Key.TestEquipID + " and realisteststatisticalresults.TestItemID=" + groupBy.Key.TestItemID + ")");
            }
            string lisHql2 = "";
            if (lisListHql.Count > 0) lisHql2 = string.Join(" or ", lisListHql);
            if (!string.IsNullOrEmpty(lisHql2))
            {
                lisResultsHql.Append(" and (");
                lisResultsHql.Append(lisHql2);
                lisResultsHql.Append(")");
            }
            IList<ReaLisTestStatisticalResults> lisResultsList = IBReaLisTestStatisticalResults.SearchTestStatisticalResultsListByJoinHql(1, "", "", lisResultsHql.ToString(), -1, -1, "");
            if (lisResultsList.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Info("理论消耗量统计:试剂ID为:" + startDate + ",结束日期为:" + endDate + ",统计仪器ID范围为:" + equipIdStr + ",统计试剂ID范围为:" + goodsIdStr + ",仪器项目实际检测量统计条件为:" + lisResultsHql.ToString() + ",获取统计各仪器项目实际检测量为空!");
                return entityList;
            }
            #endregion
            IList<ReaGoods> reaGoodsList = new List<ReaGoods>();
            IList<ReaTestItem> testItemList = new List<ReaTestItem>();
            foreach (var link in linkList)
            {
                string itemCName = "";
                #region 试剂信息
                ReaGoods reaGoods = null;
                var tempList = reaGoodsList.Where(p => p.Id == link.GoodsID.Value);
                if (tempList != null && tempList.Count() > 0)
                {
                    reaGoods = tempList.ElementAt(0);
                }
                else
                {
                    if (link.GoodsID.HasValue)
                        reaGoods = IDReaGoodsDao.Get(link.GoodsID.Value);
                    if (reaGoods != null)
                        reaGoodsList.Add(reaGoods);
                }
                if (reaGoods == null)
                {
                    ZhiFang.Common.Log.Log.Info("理论消耗量统计:试剂ID为:" + link.GoodsID.Value + ",获取试剂信息为空!");
                }
                #endregion
                #region 检验项目信息
                ReaTestItem testItem = null;
                var tempList2 = testItemList.Where(p => p.Id == link.TestItemID);
                if (tempList2 != null && tempList2.Count() > 0)
                {
                    testItem = tempList2.ElementAt(0);
                    itemCName = testItem.CName;
                }
                else
                {
                    testItem = IDReaTestItemDao.Get(link.TestItemID);
                    if (testItem != null)
                    {
                        itemCName = testItem.CName;
                        testItemList.Add(testItem);
                    }
                }
                if (testItem == null)
                {
                    ZhiFang.Common.Log.Log.Info("理论消耗量统计:检验项目ID为:" + link.TestItemID + ",获取检验项目信息为空!");
                }
                #endregion
                //找出某一仪器某一项目的所有检测结果(仪器+项目)
                IList<ReaLisTestStatisticalResults> tempLisResults = new List<ReaLisTestStatisticalResults>();
                tempLisResults = lisResultsList.Where(p => p.TestEquipID == link.TestEquipID && p.TestItemID == link.TestItemID).ToList();
                if (tempLisResults == null || tempLisResults.Count() <= 0)
                {
                    ZhiFang.Common.Log.Log.Info("理论消耗量统计:开始日期为:" + startDate + ",结束日期为:" + endDate + ",统计仪器Id为:" + link.TestEquipID + ",统计项目Id为:" + link.TestItemID + ",统计项目为:" + itemCName + ",获取统计检测结果为空!");
                    continue;
                }
                //按检测结果的检测类型生成统计结果
                var resultsOfGroupByTestType = tempLisResults.GroupBy(p => new
                {
                    p.TestType
                });
                foreach (var resultsGroupBy in resultsOfGroupByTestType)
                {
                    ConsumptionComparisonAnalysisVO vo = new ConsumptionComparisonAnalysisVO();
                    vo.TestEquipID = link.TestEquipID.ToString();
                    vo.EquipCode = resultsGroupBy.ElementAt(0).TestEquipCode;
                    vo.EquipCName = resultsGroupBy.ElementAt(0).TestEquipName;
                    vo.StartDate = startDate;
                    vo.EndDate = endDate;

                    if (reaGoods != null)
                    {
                        vo.GoodsId = reaGoods.Id.ToString();
                        vo.ReaGoodsNo = reaGoods.ReaGoodsNo;
                        vo.GoodsCName = reaGoods.CName;
                        vo.GoodsSName = reaGoods.SName;
                        vo.GoodsUnit = reaGoods.UnitName;
                        vo.UnitMemo = reaGoods.UnitMemo;
                        vo.Price = reaGoods.Price;
                    }

                    if (testItem != null)
                    {
                        vo.TestItemID = testItem.Id.ToString();
                        vo.LisTestItemCode = testItem.LisCode;
                        vo.LisTestItemSName = testItem.SName;
                        vo.LisTestItemCName = testItem.CName;
                    }
                    vo.TestType = resultsGroupBy.Key.TestType;
                    vo.TestTypeName = LisTestType.GetStatusDic()[vo.TestType].Name;
                    vo.DetectionQuantity = resultsGroupBy.Sum(p => p.TestCount);
                    vo.UnitTestCount = link.TestCount;
                    if (vo.UnitTestCount > 0)
                    {
                        vo.TheoreticalConsumption = Math.Round(vo.DetectionQuantity / vo.UnitTestCount, 3);
                        vo.TheoreticalConsumptionAmount = Math.Round(vo.Price * vo.TheoreticalConsumption / vo.UnitTestCount, 3);
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("理论消耗量统计:开始日期为:" + startDate + ",结束日期为:" + endDate + ",统计仪器ID为:" + link.TestEquipID + ",统计项目ID为:" + link.TestItemID + ",统计项目为:" + itemCName + ",统计试剂ID为:" + link.GoodsID + ",单位包装检测量未设置为合理值!");
                    }
                    entityList.Add(vo);
                }
            }

            var groupByOVList = entityList.GroupBy(p => new
            {
                p.TestEquipID,
                p.GoodsId
            });
            //理论消耗量总和,消耗占比的计算
            double theoreticalConsumptionSum = 0;
            for (int i = 0; i < entityList.Count; i++)
            {
                var tempList = entityList.Where(p => p.TestEquipID == entityList[i].TestEquipID && p.GoodsId == entityList[i].GoodsId);
                entityList[i].DetectionQuantitySum = Math.Round(tempList.Sum(k => k.DetectionQuantity), 3);
                theoreticalConsumptionSum = tempList.Sum(k => k.TheoreticalConsumption);
                entityList[i].TheoreticalConsumptionSum = Math.Round(theoreticalConsumptionSum, 3);
                if (entityList[i].UnitTestCount > 0 && theoreticalConsumptionSum > 0)
                    entityList[i].ConsumptionPercent = Math.Round(entityList[i].DetectionQuantity / entityList[i].UnitTestCount / theoreticalConsumptionSum, 4) * 100;
            }
            if (sortType == "1")
                entityList = entityList.OrderBy(p => p.TestEquipID).ThenBy(p => p.GoodsId).ThenBy(p => p.TestItemID).ToList();
            else if (sortType == "2")
                entityList = entityList.OrderBy(p => p.TestEquipID).ThenBy(p => p.GoodsId).ThenBy(p => p.TestItemID).ToList();
            return entityList;
        }
        /// <summary>
        /// 消耗比对分析
        /// </summary>
        /// <param name="linkList"></param>
        /// <param name="sortType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="equipIdStr"></param>
        /// <param name="goodsIdStr"></param>
        /// <param name="isMergeOfItem"></param>
        /// <returns></returns>
        private IList<ConsumptionComparisonAnalysisVO> SearchConsumptionComparisonVOListByHql(IList<ReaEquipTestItemReaGoodLink> linkList, string sortType, string startDate, string endDate, string equipIdStr, string goodsIdStr, bool isMergeOfItem)
        {
            IList<ConsumptionComparisonAnalysisVO> entityList = new List<ConsumptionComparisonAnalysisVO>();
            if (string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
            {
                ZhiFang.Common.Log.Log.Info("消耗比对分析统计:" + ",统计仪器ID范围为:" + equipIdStr + ",统计试剂ID范围为:" + goodsIdStr + ",统计日期范围信息为空!");
                return entityList;
            }
            #region 统计各仪器项目实际检测量
            var groupByList = linkList.GroupBy(p => new
            {
                p.TestEquipID,
                p.TestItemID
            });
            StringBuilder lisResultsHql = new StringBuilder();
            lisResultsHql.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(startDate))
            {
                lisResultsHql.Append(" and realisteststatisticalresults.TestDate>='");
                lisResultsHql.Append(startDate);
                lisResultsHql.Append("'");
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                lisResultsHql.Append(" and realisteststatisticalresults.TestDate<='");
                lisResultsHql.Append(endDate);
                lisResultsHql.Append("'");
            }
            if (!string.IsNullOrEmpty(equipIdStr))
            {
                lisResultsHql.Append(" and realisteststatisticalresults.TestEquipID in (");
                lisResultsHql.Append(equipIdStr.TrimEnd(','));
                lisResultsHql.Append(")");
            }

            IList<string> lisListHql = new List<string>();
            foreach (var groupBy in groupByList)
            {
                lisListHql.Add(" (realisteststatisticalresults.TestEquipID=" + groupBy.Key.TestEquipID + " and realisteststatisticalresults.TestItemID=" + groupBy.Key.TestItemID + ")");
            }
            string lisHql2 = "";
            if (lisListHql.Count > 0) lisHql2 = string.Join(" or ", lisListHql);
            if (!string.IsNullOrEmpty(lisHql2))
            {
                lisResultsHql.Append(" and (");
                lisResultsHql.Append(lisHql2);
                lisResultsHql.Append(")");
            }
            IList<ReaLisTestStatisticalResults> lisResultsList = IBReaLisTestStatisticalResults.SearchTestStatisticalResultsListByJoinHql(1, "", "", lisResultsHql.ToString(), -1, -1, "");
            if (lisResultsList.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Info("消耗比对分析统计:开始日期为:" + startDate + ",结束日期为:" + endDate + ",统计仪器ID范围为:" + equipIdStr + ",统计试剂ID范围为:" + goodsIdStr + ",仪器项目实际检测量统计条件为:" + lisResultsHql.ToString() + ",获取统计各仪器项目实际检测量数据为空!");
                return entityList;
            }
            #endregion
            #region 消耗比对分析的实际使用量
            IList<ReaBmsOutDtl> outDtlList = new List<ReaBmsOutDtl>();
            var groupByList2 = linkList.GroupBy(p => new
            {
                p.TestEquipID,
                p.GoodsID
            });
            StringBuilder outDtlHql = new StringBuilder();
            outDtlHql.Append(" reabmsoutdtl.TestEquipID is not null ");
            if (!string.IsNullOrEmpty(startDate))
            {
                outDtlHql.Append(" and reabmsoutdtl.DataAddTime>='");
                outDtlHql.Append(startDate);
                outDtlHql.Append("'");
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                outDtlHql.Append(" and reabmsoutdtl.DataAddTime<='");
                outDtlHql.Append(endDate);
                outDtlHql.Append("'");
            }
            if (!string.IsNullOrEmpty(equipIdStr))
            {
                outDtlHql.Append(" and reabmsoutdtl.TestEquipID in (");
                outDtlHql.Append(equipIdStr.TrimEnd(','));
                outDtlHql.Append(")");
            }
            if (!string.IsNullOrEmpty(goodsIdStr))
            {
                outDtlHql.Append(" and reabmsoutdtl.GoodsID in (");
                outDtlHql.Append(goodsIdStr.TrimEnd(','));
                outDtlHql.Append(")");
            }

            IList<string> outDtlHql2 = new List<string>();
            foreach (var groupBy in groupByList2)
            {
                outDtlHql2.Add(" (reabmsoutdtl.TestEquipID=" + groupBy.Key.TestEquipID + " and reabmsoutdtl.GoodsID=" + groupBy.Key.GoodsID + ")");
            }
            string outDtlHql3 = "";
            if (outDtlHql2.Count > 0) lisHql2 = string.Join(" or ", outDtlHql2);
            if (!string.IsNullOrEmpty(outDtlHql3))
            {
                outDtlHql.Append(" and (");
                outDtlHql.Append(outDtlHql3);
                outDtlHql.Append(")");
            }
            if (!string.IsNullOrEmpty(outDtlHql.ToString()))
            {
                string outDocHql = "reabmsoutdoc.OutType=1";
                outDtlList = IDReaBmsOutDtlDao.SearchReaBmsOutDtlListByJoinHql(outDocHql, outDtlHql.ToString(), "", "", -1, -1, "");
            }
            if (outDtlList.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Info("消耗比对分析统计:开始日期为:" + startDate + ",结束日期为:" + endDate + ",统计仪器ID范围为:" + equipIdStr + ",统计试剂ID范围为:" + goodsIdStr + ",仪器项目实际检测量统计条件为:" + outDtlHql.ToString() + ",获取统计实际使用量数据为空!");
                return entityList;
            }
            #endregion
            IList<ReaGoods> reaGoodsList = new List<ReaGoods>();
            IList<ReaTestItem> testItemList = new List<ReaTestItem>();
            foreach (var link in linkList)
            {
                #region 试剂信息
                ReaGoods reaGoods = null;
                var tempList = reaGoodsList.Where(p => p.Id == link.GoodsID.Value);
                if (tempList != null && tempList.Count() > 0)
                {
                    reaGoods = tempList.ElementAt(0);
                }
                else
                {
                    reaGoods = IDReaGoodsDao.Get(link.GoodsID.Value);
                    if (reaGoods != null)
                        reaGoodsList.Add(reaGoods);
                }
                if (reaGoods == null)
                {
                    ZhiFang.Common.Log.Log.Info("消耗比对分析统计:试剂ID为:" + link.GoodsID.Value + ",获取试剂信息为空!");
                }
                #endregion
                #region 检验项目信息
                ReaTestItem testItem = null;
                var tempList2 = testItemList.Where(p => p.Id == link.TestItemID);
                if (tempList2 != null && tempList2.Count() > 0)
                {
                    testItem = tempList2.ElementAt(0);
                }
                else
                {
                    testItem = IDReaTestItemDao.Get(link.TestItemID);
                    if (testItem != null)
                        testItemList.Add(testItem);
                }
                if (testItem == null)
                {
                    ZhiFang.Common.Log.Log.Info("消耗比对分析统计:检验项目ID为:" + link.TestItemID + ",获取检验项目信息为空!");
                }
                #endregion
                //找出某一仪器某一项目的检测结果集合(仪器+项目)
                var tempLisResults = lisResultsList.Where(p => p.TestEquipID == link.TestEquipID && p.TestItemID == link.TestItemID);
                if (tempLisResults == null || tempLisResults.Count() <= 0)
                {
                    ZhiFang.Common.Log.Log.Info("消耗比对分析统计:开始日期为:" + startDate + ",结束日期为:" + endDate + ",统计仪器ID为:" + link.TestEquipID + ",统计项目ID为:" + link.TestItemID + ",获取统计检测结果为空!");
                    continue;
                }
                //按检测结果的检测类型生成统计结果
                var resultsOfGroupByTestType = tempLisResults.GroupBy(p => new
                {
                    p.TestType
                });
                foreach (var resultsGroupBy in resultsOfGroupByTestType)
                {
                    ConsumptionComparisonAnalysisVO vo = new ConsumptionComparisonAnalysisVO();
                    vo.TestEquipID = link.TestEquipID.ToString();
                    vo.EquipCode = resultsGroupBy.ElementAt(0).TestEquipCode;
                    vo.EquipCName = resultsGroupBy.ElementAt(0).TestEquipName;
                    vo.StartDate = startDate;
                    vo.EndDate = endDate;
                    if (reaGoods != null)
                    {
                        vo.GoodsId = reaGoods.Id.ToString();
                        vo.ReaGoodsNo = reaGoods.ReaGoodsNo;
                        vo.GoodsCName = reaGoods.CName;
                        vo.GoodsSName = reaGoods.SName;
                        vo.GoodsUnit = reaGoods.UnitName;
                        vo.UnitMemo = reaGoods.UnitMemo;
                        vo.Price = reaGoods.Price;
                    }
                    var outDtlList2 = outDtlList.Where(p => p.TestEquipID == link.TestEquipID && p.GoodsID == link.GoodsID);
                    if (outDtlList2 != null)
                    {
                        vo.TestEquipOutCount = Math.Round(outDtlList2.Sum(p => p.GoodsQty), 3);
                        vo.TestEquipOutAmount = Math.Round(outDtlList2.Sum(p => p.SumTotal), 3);
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("消耗比对分析统计:开始日期为:" + startDate + ",结束日期为:" + endDate + ",统计仪器ID为:" + link.TestEquipID + ",统计试剂ID为:" + link.GoodsID + ",获取实际使用量数据为空!");
                    }
                    string itemCName = "";
                    if (testItem != null)
                    {
                        vo.TestItemID = testItem.Id.ToString();
                        vo.LisTestItemCode = testItem.LisCode;
                        vo.LisTestItemSName = testItem.SName;
                        vo.LisTestItemCName = testItem.CName;
                        vo.TestItemPrice = testItem.Price;//检验项目单价
                        itemCName = testItem.CName;
                    }
                    vo.TestType = resultsGroupBy.Key.TestType;
                    vo.UnitTestCount = link.TestCount;
                    if (!string.IsNullOrEmpty(vo.TestType))
                        vo.TestTypeName = LisTestType.GetStatusDic()[vo.TestType].Name;
                    vo.DetectionQuantity = resultsGroupBy.Sum(p => p.TestCount);

                    //项目收入= 实际检测数*项目收费单价，只取常规
                    if (vo.TestType == LisTestType.常规.Key)
                    {
                        vo.TestItemIncome = Math.Round(vo.DetectionQuantity * vo.TestItemPrice, 3);
                    }
                    if (vo.UnitTestCount > 0)
                    {
                        vo.TheoreticalConsumption = Math.Round(vo.DetectionQuantity / vo.UnitTestCount, 3);
                        vo.TheoreticalConsumptionAmount = Math.Round(vo.Price * vo.TheoreticalConsumption / vo.UnitTestCount, 3);
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("消耗比对分析统计:开始日期为:" + startDate + ",结束日期为:" + endDate + ",统计仪器ID为:" + link.TestEquipID + ",统计项目ID为:" + link.TestItemID + ",统计项目为:" + itemCName + ",统计试剂ID为:" + link.GoodsID + ",单位包装检测量未设置为合理值!");
                    }
                    entityList.Add(vo);
                }
            }
            #region 理论消耗量总和,消耗占比及消耗比的计算
            //同一仪器相同试剂的不同项目
            double theoreticalConsumptionSum = 0;
            for (int i = 0; i < entityList.Count; i++)
            {
                var tempList = entityList.Where(p => p.TestEquipID == entityList[i].TestEquipID && p.GoodsId == entityList[i].GoodsId);
                entityList[i].DetectionQuantitySum = Math.Round(tempList.Sum(k => k.DetectionQuantity), 3);
                theoreticalConsumptionSum = tempList.Sum(k => k.TheoreticalConsumption);
                entityList[i].TheoreticalConsumptionSum = Math.Round(theoreticalConsumptionSum, 3);

                if (entityList[i].TheoreticalConsumptionSum > 0)
                    entityList[i].ConsumptionPercentSum = Math.Round(entityList[i].TestEquipOutCount / entityList[i].TheoreticalConsumptionSum * 100, 3);

                if (entityList[i].UnitTestCount > 0 && entityList[i].TheoreticalConsumptionSum > 0)
                    entityList[i].ConsumptionPercent = Math.Round(entityList[i].DetectionQuantity / entityList[i].UnitTestCount / entityList[i].TheoreticalConsumptionSum, 4) * 100;

                //项目总收入：同一仪器相同试剂的不同项目的项目收入之和
                entityList[i].TestItemIncomeSum = Math.Round(tempList.Sum(k => k.TestItemIncome), 3);

                //成本占比=成本/项目总收入*100%，即试剂成本比=试剂成本/项目总收入*100%
                if (entityList[i].TestItemIncomeSum > 0)
                    entityList[i].CostRatio = Math.Round((entityList[i].TestEquipOutAmount / entityList[i].TestItemIncomeSum * 100), 3);

                //试剂成本利润率=（项目总收入-试剂成本）/试剂成本*100%
                if (entityList[i].TestEquipOutAmount > 0)
                    entityList[i].CostMargin = Math.Round(((entityList[i].TestItemIncomeSum - entityList[i].TestEquipOutAmount) / entityList[i].TestEquipOutAmount * 100), 3);

                //(项目)毛利率= 毛利/收入*100%=（项目总收入-试剂成本）/项目总收入*100%
                if (entityList[i].TestItemIncomeSum > 0)
                    entityList[i].GrossProfitMargin = Math.Round(((entityList[i].TestItemIncomeSum - entityList[i].TestEquipOutAmount) / entityList[i].TestItemIncomeSum * 100), 3);

                //额外消耗=实际消耗量-理论消耗总量（同仪器相同试剂不同项目的理论消耗量之和）
                //if (entityList[i].TestEquipOutCount > entityList[i].TheoreticalConsumptionSum)
                entityList[i].ExtraConsumption = entityList[i].TestEquipOutCount - entityList[i].TheoreticalConsumptionSum;

                //额外消耗比=(实际消耗量-理论消耗总量 )/理论消耗总量*100% 也就是额外消耗比=消耗比-1
                if (entityList[i].TheoreticalConsumptionSum > 0)
                    entityList[i].ExtraConsumptionRatio = Math.Round(entityList[i].ExtraConsumption / entityList[i].TheoreticalConsumptionSum, 3) * 100;

            }
            #endregion
            return entityList;
        }
        public BaseResultDataValue SearChconsumeTheoryEChartsVOByHql(int statisticType, bool showZero, string startDate, string endDate, string dtlHql, string equipIdStr, string goodsIdStr, bool isMergeOfItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ConsumptionComparisonAnalysisVO> entityList = SearchConsumptionComparisonAnalysisVOListByHql(1, startDate, endDate, equipIdStr, goodsIdStr, -1, -1, "", isMergeOfItem);
            if (entityList.count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取理论消耗量统计信息为空!";
                return baseResultDataValue;
            }
            IList<ConsumptionComparisonAnalysisVO> dtlList = entityList.list;
            string jkey = "GoodsCName";
            if (statisticType == 2)
                jkey = "TestItemCName";
            JArray dimensionsData = new JArray();
            dimensionsData.Add(jkey);
            dimensionsData.Add("常规消耗");
            dimensionsData.Add("复检消耗");
            dimensionsData.Add("质控消耗");
            dimensionsData.Add("常规金额");
            dimensionsData.Add("复检金额");
            dimensionsData.Add("质控金额");
            if (statisticType == 1)
                baseResultDataValue = SearChconsumeTheoryEChartsVOByHqlOfGoodsCName(dtlList, jkey, dimensionsData);
            else
                baseResultDataValue = SearChconsumeTheoryEChartsVOByHqlOfTestItemCName(dtlList, jkey, dimensionsData);
            return baseResultDataValue;
        }
        /// <summary>
        /// 理论消耗量:按仪器试剂显示(相同仪器同一试剂的不同项目合并)
        /// </summary>
        /// <param name="dtlList"></param>
        /// <param name="jkey"></param>
        /// <param name="dimensionsData"></param>
        /// <returns></returns>
        private BaseResultDataValue SearChconsumeTheoryEChartsVOByHqlOfGoodsCName(IList<ConsumptionComparisonAnalysisVO> dtlList, string jkey, JArray dimensionsData)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            JObject jresult = new JObject();
            JObject jdataset = new JObject();
            JArray sourceData = new JArray();
            var groupByList = dtlList.GroupBy(p => new
            {
                p.TestEquipID,
                p.GoodsId
            });
            Dictionary<long, ReaTestItem> testItemList = new Dictionary<long, ReaTestItem>();
            string jkeyValue = "";
            double commonCount = 0, reviewCount = 0, qcdataCount = 0, commonAmount = 0, reviewAmount = 0, qcdataAmount = 0;
            foreach (var groupBy in groupByList)
            {
                commonCount = 0;
                reviewCount = 0;
                qcdataCount = 0;
                commonAmount = 0;
                reviewAmount = 0;
                qcdataAmount = 0;
                jkeyValue = "";

                //按某仪器某试剂的检测类型再分组
                var groupByList2 = groupBy.GroupBy(p => new
                {
                    p.TestType
                });
                foreach (var groupBy2 in groupByList2)
                {
                    if (groupBy2.Key.TestType == LisTestType.常规.Key)
                    {
                        commonCount = groupBy2.Sum(k => k.TheoreticalConsumption);
                        commonAmount = groupBy2.Sum(k => k.TheoreticalConsumptionAmount);
                    }
                    else if (groupBy2.Key.TestType == LisTestType.复检.Key)
                    {
                        reviewCount = groupBy2.Sum(k => k.TheoreticalConsumption);
                        reviewAmount = groupBy2.Sum(k => k.TheoreticalConsumptionAmount);
                    }
                    else if (groupBy2.Key.TestType == LisTestType.质控.Key)
                    {
                        qcdataCount = groupBy2.Sum(k => k.TheoreticalConsumption);
                        qcdataAmount = groupBy2.Sum(k => k.TheoreticalConsumptionAmount);
                    }
                }

                jkeyValue = groupBy.ElementAt(0).GoodsCName;
                JObject jsource = new JObject();
                jsource.Add(jkey, jkeyValue);
                jsource.Add("常规消耗", Math.Round(commonCount, 2).ToString());
                jsource.Add("复检消耗", Math.Round(reviewCount, 2).ToString());
                jsource.Add("质控消耗", Math.Round(qcdataCount, 2).ToString());
                jsource.Add("常规金额", Math.Round(commonAmount, 2).ToString());
                jsource.Add("复检金额", Math.Round(reviewAmount, 2).ToString());
                jsource.Add("质控金额", Math.Round(qcdataAmount, 2).ToString());
                sourceData.Add(jsource);
            }
            jdataset.Add("dimensions", dimensionsData);
            jdataset.Add("source", sourceData);
            jresult.Add("dataset", jdataset);
            //ZhiFang.Common.Log.Log.Debug("jresult:" + jresult.ToString());
            baseResultDataValue.ResultDataValue = jresult.ToString();
            return baseResultDataValue;
        }
        /// <summary>
        /// 试剂理论消耗量:按仪器项目试剂显示
        /// </summary>
        /// <param name="dtlList"></param>
        /// <param name="jkey"></param>
        /// <param name="dimensionsData"></param>
        /// <returns></returns>
        private BaseResultDataValue SearChconsumeTheoryEChartsVOByHqlOfTestItemCName(IList<ConsumptionComparisonAnalysisVO> dtlList, string jkey, JArray dimensionsData)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            JObject jresult = new JObject();
            JObject jdataset = new JObject();
            JArray sourceData = new JArray();
            var groupByList = dtlList.GroupBy(p => new
            {
                p.TestEquipID,
                p.TestItemID,
                p.GoodsId
            });
            Dictionary<long, ReaTestItem> testItemList = new Dictionary<long, ReaTestItem>();
            string jkeyValue = "";
            double commonCount = 0, reviewCount = 0, qcdataCount = 0, commonAmount = 0, reviewAmount = 0, qcdataAmount = 0;
            foreach (var groupBy in groupByList)
            {
                commonCount = 0;
                reviewCount = 0;
                qcdataCount = 0;
                commonAmount = 0;
                reviewAmount = 0;
                qcdataAmount = 0;
                jkeyValue = "";

                //按某仪器项目试剂的检测类型再分组
                var groupByList2 = groupBy.GroupBy(p => new
                {
                    p.TestType
                });
                foreach (var groupBy2 in groupByList2)
                {
                    if (groupBy2.Key.TestType == LisTestType.常规.Key)
                    {
                        commonCount = groupBy2.Sum(k => k.TheoreticalConsumption);
                        commonAmount = groupBy2.Sum(k => k.TheoreticalConsumptionAmount);
                    }
                    else if (groupBy2.Key.TestType == LisTestType.复检.Key)
                    {
                        reviewCount = groupBy2.Sum(k => k.TheoreticalConsumption);
                        reviewAmount = groupBy2.Sum(k => k.TheoreticalConsumptionAmount);
                    }
                    else if (groupBy2.Key.TestType == LisTestType.质控.Key)
                    {
                        qcdataCount = groupBy2.Sum(k => k.TheoreticalConsumption);
                        qcdataAmount = groupBy2.Sum(k => k.TheoreticalConsumptionAmount);
                    }
                }

                jkeyValue = groupBy.ElementAt(0).LisTestItemCName + "-" + groupBy.ElementAt(0).GoodsCName;
                JObject jsource = new JObject();
                jsource.Add(jkey, jkeyValue);
                jsource.Add("常规消耗", Math.Round(commonCount, 2).ToString());
                jsource.Add("复检消耗", Math.Round(reviewCount, 2).ToString());
                jsource.Add("质控消耗", Math.Round(qcdataCount, 2).ToString());
                jsource.Add("常规金额", Math.Round(commonAmount, 2).ToString());
                jsource.Add("复检金额", Math.Round(reviewAmount, 2).ToString());
                jsource.Add("质控金额", Math.Round(qcdataAmount, 2).ToString());
                //jsource.Add("金额", 0);
                sourceData.Add(jsource);
            }
            jdataset.Add("dimensions", dimensionsData);
            jdataset.Add("source", sourceData);
            jresult.Add("dataset", jdataset);
            //ZhiFang.Common.Log.Log.Debug("jresult:" + jresult.ToString());
            baseResultDataValue.ResultDataValue = jresult.ToString();
            return baseResultDataValue;
        }
        public BaseResultDataValue SearChconsumeComparisonEChartsVOByHql(int statisticType, bool showZero, string startDate, string endDate, string dtlHql, string equipIdStr, string goodsIdStr, bool isMergeOfItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ConsumptionComparisonAnalysisVO> entityList = SearchConsumptionComparisonAnalysisVOListByHql(2, startDate, endDate, equipIdStr, goodsIdStr, -1, -1, "", isMergeOfItem);
            if (entityList.count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取消耗比对分析统计信息为空!";
                return baseResultDataValue;
            }
            IList<ConsumptionComparisonAnalysisVO> dtlList = entityList.list;
            //string jkey = "GoodsCName";
            JArray legendData = new JArray();
            // legendData.Add(jkey);
            legendData.Add("实际使用量");
            legendData.Add("试剂成本");
            legendData.Add("常规理论消耗");
            legendData.Add("质控理论消耗");
            legendData.Add("复检理论消耗");
            legendData.Add("消耗比");//拆线
            //统计类型:1为理论消耗量;2为消耗比对分析;
            if (statisticType == 2)
            {
                legendData.Add("项目收入");//柱状图
                legendData.Add("成本利润率");//拆线
                legendData.Add("成本占比");//拆线
                legendData.Add("毛利率");//拆线
                legendData.Add("额外消耗比");//拆线               
            }
            //if (statisticType == 1)
            baseResultDataValue = SearChconsumeComparisonEChartsVOByHqlOfGoodsCName(statisticType, dtlList, legendData);

            return baseResultDataValue;
        }
        /// <summary>
        /// 消耗比对分析:按仪器试剂显示(相同仪器同一试剂的不同项目合并)
        /// </summary>
        /// <param name="dtlList"></param>
        /// <param name="jkey"></param>
        /// <param name="legendData"></param>
        /// <returns></returns>
        private BaseResultDataValue SearChconsumeComparisonEChartsVOByHqlOfGoodsCName(int statisticType, IList<ConsumptionComparisonAnalysisVO> dtlList, JArray legendData)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            JObject jresult = new JObject();
            JObject jLegend = new JObject();
            JObject jxAxis = new JObject();
            JArray xAxisData = new JArray();
            JObject jSeries = new JObject();
            JArray seriesData = new JArray();

            jLegend.Add("data", legendData);
            jresult.Add("legend", jLegend);

            var groupByList = dtlList.GroupBy(p => new
            {
                p.TestEquipID,
                p.GoodsId
            });
            //实际使用量series
            JObject jSeriesOutCount = new JObject();
            jSeriesOutCount.Add("name", "实际使用量");
            jSeriesOutCount.Add("type", "bar");
            JArray seriesDataOutCount = new JArray();

            //试剂成本series
            JObject jSeriesOutAmount = new JObject();
            jSeriesOutAmount.Add("name", "试剂成本");
            jSeriesOutAmount.Add("type", "bar");
            jSeriesOutAmount.Add("yAxisIndex", 2);
            JArray seriesDataOutAmount = new JArray();

            //消耗比series
            JObject jSeriesPercent = new JObject();
            jSeriesPercent.Add("name", "消耗比");
            jSeriesPercent.Add("type", "line");
            jSeriesPercent.Add("yAxisIndex", 1);
            JArray seriesDataPercent = new JArray();

            //常规理论消耗series
            JObject jSeriesCommonCount = new JObject();
            jSeriesCommonCount.Add("name", "常规理论消耗");
            jSeriesCommonCount.Add("type", "bar");
            jSeriesCommonCount.Add("stack", "理论消耗");
            JArray seriesDataCommonCount = new JArray();

            //复检理论消耗series
            JObject jSeriesReviewCount = new JObject();
            jSeriesReviewCount.Add("name", "复检理论消耗");
            jSeriesReviewCount.Add("type", "bar");
            jSeriesReviewCount.Add("stack", "理论消耗");
            JArray seriesDataReviewCount = new JArray();

            //质控理论消耗series
            JObject jSeriesQCdataCount = new JObject();
            jSeriesQCdataCount.Add("name", "质控理论消耗");
            jSeriesQCdataCount.Add("type", "bar");
            jSeriesQCdataCount.Add("stack", "理论消耗");
            JArray seriesDataQCdataCount = new JArray();

            #region 消耗比对分析才有
            //成本占比series
            JObject jSeriesCostRatio = new JObject();
            jSeriesCostRatio.Add("name", "成本占比");
            jSeriesCostRatio.Add("type", "line");
            jSeriesCostRatio.Add("yAxisIndex", 3);
            JArray seriesDataCostRatio = new JArray();

            //成本利润率series
            JObject jSeriesCostMargin = new JObject();
            jSeriesCostMargin.Add("name", "成本利润率");
            jSeriesCostMargin.Add("type", "line");
            jSeriesCostMargin.Add("yAxisIndex", 3);
            JArray seriesDataCostMargin = new JArray();

            //毛利率series
            JObject jSeriesGrossProfitMargin = new JObject();
            jSeriesGrossProfitMargin.Add("name", "毛利率");
            jSeriesGrossProfitMargin.Add("type", "line");
            jSeriesGrossProfitMargin.Add("yAxisIndex", 3);
            JArray seriesDataGrossProfitMargin = new JArray();

            //额外消耗比series
            JObject jSeriesExtraConsumptionRatio = new JObject();
            jSeriesExtraConsumptionRatio.Add("name", "额外消耗比");
            jSeriesExtraConsumptionRatio.Add("type", "line");
            jSeriesExtraConsumptionRatio.Add("yAxisIndex", 3);
            JArray seriesDataExtraConsumptionRatio = new JArray();

            //项目收入series 柱状图(不是堆叠,只按常规检测计算)
            JObject jSeriesTestItemIncome = new JObject();
            jSeriesTestItemIncome.Add("name", "项目收入");
            jSeriesTestItemIncome.Add("type", "bar");
            jSeriesTestItemIncome.Add("yAxisIndex", 2);
            JArray seriesDataTestItemIncome = new JArray();
            #endregion

            foreach (var groupBy in groupByList)
            {
                //仪器试剂名称
                xAxisData.Add(groupBy.ElementAt(0).GoodsCName);
                //实际使用量data
                var testEquipOutCount = groupBy.Sum(k => k.TestEquipOutCount);
                seriesDataOutCount.Add(Math.Round(testEquipOutCount, 2));
                //试剂成本data
                var testEquipOutAmount = groupBy.Sum(k => k.TestEquipOutAmount);
                seriesDataOutAmount.Add(Math.Round(testEquipOutAmount, 2));

                //消耗比data
                var consumptionPercent = groupBy.ElementAt(0).ConsumptionPercentSum;
                seriesDataPercent.Add(Math.Round(consumptionPercent, 3));
                if (statisticType == 2)
                {
                    //项目（总）收入
                    seriesDataTestItemIncome.Add(Math.Round(groupBy.ElementAt(0).TestItemIncomeSum, 3));
                    //成本占比
                    seriesDataCostRatio.Add(Math.Round(groupBy.ElementAt(0).CostRatio, 3));
                    //成本利润率
                    seriesDataCostMargin.Add(Math.Round(groupBy.ElementAt(0).CostMargin, 3));
                    //毛利率
                    seriesDataGrossProfitMargin.Add(Math.Round(groupBy.ElementAt(0).GrossProfitMargin, 3));
                    //额外消耗比
                    seriesDataExtraConsumptionRatio.Add(Math.Round(groupBy.ElementAt(0).ExtraConsumptionRatio, 3));
                }
                var groupByList2 = groupBy.GroupBy(p => new
                {
                    p.TestType
                });
                #region 按检测类型获取理论消耗
                double commonCount = 0, reviewCount = 0, qcdataCount = 0;
                foreach (var groupBy2 in groupByList2)
                {
                    if (groupBy2.Key.TestType == LisTestType.常规.Key)
                    {
                        commonCount += groupBy2.Sum(k => k.TheoreticalConsumption);
                        //testItemIncome = groupBy2.Sum(k => k.TestItemIncome);
                    }
                    else if (groupBy2.Key.TestType == LisTestType.复检.Key)
                    {
                        reviewCount += groupBy2.Sum(k => k.TheoreticalConsumption);
                    }
                    else if (groupBy2.Key.TestType == LisTestType.质控.Key)
                    {
                        qcdataCount += groupBy2.Sum(k => k.TheoreticalConsumption);
                    }
                }

                seriesDataCommonCount.Add(Math.Round(commonCount, 3));
                seriesDataReviewCount.Add(Math.Round(reviewCount, 3));
                seriesDataQCdataCount.Add(Math.Round(qcdataCount, 3));
                #endregion
            }

            jSeriesOutCount.Add("data", seriesDataOutCount);
            seriesData.Add(jSeriesOutCount);

            jSeriesOutAmount.Add("data", seriesDataOutAmount);
            seriesData.Add(jSeriesOutAmount);

            jSeriesPercent.Add("data", seriesDataPercent);
            seriesData.Add(jSeriesPercent);

            jSeriesCommonCount.Add("data", seriesDataCommonCount);
            seriesData.Add(jSeriesCommonCount);

            jSeriesReviewCount.Add("data", seriesDataReviewCount);
            seriesData.Add(jSeriesReviewCount);

            jSeriesQCdataCount.Add("data", seriesDataQCdataCount);
            seriesData.Add(jSeriesQCdataCount);
            if (statisticType == 2)
            {
                jSeriesCostRatio.Add("data", seriesDataCostRatio);
                seriesData.Add(jSeriesCostRatio);

                jSeriesCostMargin.Add("data", seriesDataCostMargin);
                seriesData.Add(jSeriesCostMargin);

                jSeriesGrossProfitMargin.Add("data", seriesDataGrossProfitMargin);
                seriesData.Add(jSeriesGrossProfitMargin);

                jSeriesExtraConsumptionRatio.Add("data", seriesDataExtraConsumptionRatio);
                seriesData.Add(jSeriesExtraConsumptionRatio);

                jSeriesTestItemIncome.Add("data", seriesDataTestItemIncome);
                seriesData.Add(jSeriesTestItemIncome);
            }
            jxAxis.Add("data", xAxisData);
            jresult.Add("xAxis", jxAxis);
            jresult.Add("series", seriesData);

            //ZhiFang.Common.Log.Log.Debug("jresult:" + jresult.ToString());
            baseResultDataValue.ResultDataValue = jresult.ToString();
            return baseResultDataValue;
        }

    }
}