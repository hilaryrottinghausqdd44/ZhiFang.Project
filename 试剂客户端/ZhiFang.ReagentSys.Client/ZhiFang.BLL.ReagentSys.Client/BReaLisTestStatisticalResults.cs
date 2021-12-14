
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.ReagentSys.Client.Common;
using Newtonsoft.Json.Linq;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaLisTestStatisticalResults : BaseBLL<ReaLisTestStatisticalResults>, ZhiFang.IBLL.ReagentSys.Client.IBReaLisTestStatisticalResults
    {
        IDReaTestEquipLabDao IDReaTestEquipLabDao { get; set; }
        IDReaTestItemDao IDReaTestItemDao { get; set; }

        public BaseResultBool SaveReaLisTestStatisticalResults(string testType, string beginDate, string endDate, string equipIDStr, string lisEquipCodeStr, string where, string order, bool isCover)
        {
            BaseResultBool baseResultBool = new BaseResultBool();

            IList<ReaLisTestStatisticalResults> resultList = new List<ReaLisTestStatisticalResults>();
            #region 删除客户端原来已提取的检测结果
            StringBuilder hqlStrb = new StringBuilder();
            if (!string.IsNullOrEmpty(testType))
            {
                hqlStrb.Append(" realisteststatisticalresults.TestType='" + testType + "' and ");
            }
            //仪器编码
            if (!string.IsNullOrEmpty(equipIDStr))
            {
                hqlStrb.Append(" realisteststatisticalresults.TestEquipID in (" + equipIDStr + ") and ");//TestEquipCode
            }
            else if (!string.IsNullOrEmpty(lisEquipCodeStr))
            {
                hqlStrb.Append(" realisteststatisticalresults.TestEquipCode in (" + lisEquipCodeStr + ") and ");//
            }

            if (!string.IsNullOrEmpty(beginDate))
            {
                hqlStrb.Append(" realisteststatisticalresults.TestDate>='" + beginDate + "' and ");
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                hqlStrb.Append(" realisteststatisticalresults.TestDate<='" + endDate + "' and ");
            }
            char[] trimChars = new char[] { ' ', 'a', 'n', 'd' };
            string hqlWhere = " (" + hqlStrb.ToString().TrimEnd(trimChars) + ")";
            ZhiFang.Common.Log.Log.Debug("DeleteByHql.ReaLisTestStatisticalResults.hqlWhere;" + hqlWhere);
            if (isCover == true)
            {
                string delHql = " From ReaLisTestStatisticalResults realisteststatisticalresults where 1=1 and " + hqlWhere;
                int result = this.DeleteByHql(delHql);
                ZhiFang.Common.Log.Log.Debug("DeleteByHql.ReaLisTestStatisticalResults.DeleteByHql;" + delHql + ";Counts:" + result);
            }
            else
            {
                //获取符合当前条件已导入试剂客户端的检测结果
                resultList = this.SearchListByHQL(hqlWhere);
            }
            #endregion
            //string where1 = " 1=1 ";
            //if (!string.IsNullOrEmpty(lisEquipCodeStr))
            //    where1 = " and EquipNo in (" + lisEquipCodeStr + ")";
            //if (string.IsNullOrEmpty(where))
            //    where = " 1=1 ";
            //where = where +" and "+ where1;
            DataSet ds = DataAccess_SQL.CreateReaLisTestStatisticalResultsDao_SQL().SelectLisTestStatisticalResultsList(testType, beginDate, endDate, lisEquipCodeStr, where, order);
            ZhiFang.Common.Log.Log.Debug("获取Lis检测结果条件为:testType;" + testType + ",beginDate;" + beginDate + ",endDate;" + endDate + ",lisEquipCodeStr;" + lisEquipCodeStr + ",where;" + where + ",order;" + order);
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "获取Lis检测结果信息为空!";
                return baseResultBool;
            }
            ZhiFang.Common.Log.Log.Debug("获取LIS检测结果记录数.RowsCounts;" + ds.Tables[0].Rows.Count);
            DataTable dt = ds.Tables[0];
            IList<ReaTestEquipLab> equpList = IDReaTestEquipLabDao.LoadAll();
            IList<ReaTestItem> testItemList = IDReaTestItemDao.LoadAll();
            var dataRowGroupBy = from dataRow in dt.AsEnumerable()
                                 group dataRow by new
                                 {
                                     TestType = dataRow["TestType"].ToString(),
                                     ReceiveDate = dataRow["ReceiveDate"].ToString(),
                                     EquipNo = dataRow["EquipNo"].ToString(),
                                     ItemNo = dataRow["ItemNo"].ToString()
                                 } into s
                                 select s;
            foreach (var groupBy in dataRowGroupBy)
            {
                //通过Lis仪器编码找到试剂客户端对应的仪器信息
                var tempEqupList = equpList.Where(p => p.LisCode == groupBy.Key.EquipNo.ToString());
                if (tempEqupList == null || tempEqupList.Count() <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug("检测结果的仪器编码为:" + groupBy.Key.EquipNo.ToString() + ",在试剂客户端的仪器信息没有对照关系,本次结果不导入!");
                    continue;
                }
                //通过Lis项目编码找到试剂客户端对应的项目信息
                var tempTestItemList = testItemList.Where(p => p.LisCode == groupBy.Key.ItemNo.ToString());
                if (tempTestItemList == null || tempTestItemList.Count() <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug("检测结果的仪器编码为:" + groupBy.Key.ItemNo.ToString() + ",在试剂客户端的检验项目信息没有对照关系,本次结果不导入!");
                    continue;
                }

                DataRow row = groupBy.ElementAt(0);
                //ZhiFang.Common.Log.Log.Debug("检测结果的仪器编码为:" + groupBy.Key.ItemNo.ToString() + ",检测日期:" + row["ReceiveDate"].ToString() + ",检测结果为:" + groupBy.Count() + "!");
                ReaLisTestStatisticalResults entity = new ReaLisTestStatisticalResults();
                if (row["TestType"] != null && row["TestType"].ToString() != "")
                {
                    entity.TestType = row["TestType"].ToString();
                }
                if (row["ReceiveDate"] != null && row["ReceiveDate"].ToString() != "")
                {
                    entity.TestDate = DateTime.Parse(row["ReceiveDate"].ToString());
                }
                entity.TestEquipID = tempEqupList.ElementAt(0).Id;
                entity.TestEquipName = tempEqupList.ElementAt(0).CName;
                if (row["EquipNo"] != null && row["EquipNo"].ToString() != "")
                {
                    entity.TestEquipCode = row["EquipNo"].ToString();
                }
                entity.TestItemID = tempTestItemList.ElementAt(0).Id;
                entity.TestItemCName = tempTestItemList.ElementAt(0).CName;
                entity.TestItemSName = tempTestItemList.ElementAt(0).SName;
                entity.TestItemEName = tempTestItemList.ElementAt(0).EName;
                if (row["ItemNo"] != null && row["ItemNo"].ToString() != "")
                {
                    entity.TestItemCode = row["ItemNo"].ToString();
                }
                //检测量(同一仪器同一项目同一天的检测结果合并)
                entity.TestCount = groupBy.Count();
                bool isSave = true;
                //导入的检测结果不覆盖已导入的,需要判断是否当前检测结果是否已经导入到试剂客户端
                if (isCover == false)
                {
                    var tempResult = resultList.Where(p => p.TestType == entity.TestType && p.TestDate.Value.ToString("yyyy-MM-dd") == entity.TestDate.Value.ToString("yyyy-MM-dd") && p.TestEquipCode == entity.TestEquipCode && p.TestItemCode == entity.TestItemCode);
                    if (tempResult != null && tempResult.Count() > 0)
                    {
                        isSave = false;
                        ZhiFang.Common.Log.Log.Debug("仪器编码为:" + entity.TestEquipCode + ",项目编码为:" + entity.TestItemCode + ",检验类型为:" + entity.TestType + ",检验日期为:" + entity.TestDate + ",检验结果数为:" + entity.TestCount + ",已从LIS导入");
                        continue;
                    }
                }
                if (isSave == true)
                {
                    this.Entity = entity;
                    baseResultBool.success = DBDao.Save(entity);
                    if (baseResultBool.success == false)
                    {
                        ZhiFang.Common.Log.Log.Error("导入检测结果失败:仪器编码为:" + groupBy.Key.EquipNo + ",项目编码为:" + groupBy.Key.ItemNo + ",检验日期为:" + row["ReceiveDate"].ToString());
                    }
                }
            }
            //}
            return baseResultBool;
        }

        #region 统计及分组
        public IList<ReaLisTestStatisticalResults> SearchTestStatisticalResultsListByJoinHql(int groupType, string startDate, string endDate, string dtlHql, int page, int limit, string sort)
        {
            IList<ReaLisTestStatisticalResults> entityList = new List<ReaLisTestStatisticalResults>();
            EntityList<ReaLisTestStatisticalResults> entityList2 = ((IDReaLisTestStatisticalResultsDao)base.DBDao).GetListByHQL(dtlHql, sort, -1, -1);

            //按使用仪器+检测类型+项目Id分组
            IList<ReaLisTestStatisticalResults> tempDtlList = new List<ReaLisTestStatisticalResults>();
            if (groupType == 1)
            {
                tempDtlList = SearchReaLisTestStatisticalResultsBy1(entityList2.list, startDate, endDate);
            }
            else
            {
                tempDtlList = SearchReaLisTestStatisticalResultsBy1(entityList2.list, startDate, endDate);
            }
            //分页处理
            if (limit > 0 && limit < tempDtlList.Count)
            {
                int startIndex = limit * (page - 1);
                int endIndex = limit;
                var list = tempDtlList.Skip(startIndex).Take(endIndex);
                if (list != null)
                    tempDtlList = list.ToList();
            }
            entityList = tempDtlList;
            return entityList;
        }
        public EntityList<ReaLisTestStatisticalResults> SearchTestStatisticalResultsEntityListByJoinHql(int groupType, string startDate, string endDate, string dtlHql, int page, int limit, string sort)
        {
            EntityList<ReaLisTestStatisticalResults> entityList = new EntityList<ReaLisTestStatisticalResults>();
            EntityList<ReaLisTestStatisticalResults> entityList2 = ((IDReaLisTestStatisticalResultsDao)base.DBDao).GetListByHQL(dtlHql, sort, -1, -1);

            //按使用仪器+检测类型+项目Id分组
            IList<ReaLisTestStatisticalResults> tempDtlList = SearchReaLisTestStatisticalResultsBy1(entityList2.list, startDate, endDate);
            entityList.count = tempDtlList.Count();
            //分页处理
            if (limit > 0 && limit < tempDtlList.Count)
            {
                int startIndex = limit * (page - 1);
                int endIndex = limit;
                var list = tempDtlList.Skip(startIndex).Take(endIndex);
                if (list != null)
                    tempDtlList = list.ToList();
            }
            entityList.list = tempDtlList;
            return entityList;
        }
        /// <summary>
        /// 项目检测量:按使用仪器+检测类型+项目Id分组
        /// </summary>
        /// <param name="tempDtlList"></param>
        /// <returns></returns>
        private IList<ReaLisTestStatisticalResults> SearchReaLisTestStatisticalResultsBy1(IList<ReaLisTestStatisticalResults> tempDtlList, string startDate, string endDate)
        {
            IList<ReaLisTestStatisticalResults> dtlList = new List<ReaLisTestStatisticalResults>();
            if (tempDtlList != null && tempDtlList.Count > 0)
            {
                var groupBy = tempDtlList.GroupBy(p => new
                {
                    p.TestEquipID,
                    p.TestItemID,
                    p.TestType
                });
                Dictionary<long, ReaTestItem> testItemList = new Dictionary<long, ReaTestItem>();
                foreach (var model in groupBy)
                {
                    ReaLisTestStatisticalResults dtlResults = ClassMapperHelp.GetMapper<ReaLisTestStatisticalResults, ReaLisTestStatisticalResults>(model.ElementAt(0));
                    dtlResults.TestCount = model.Sum(k => k.TestCount);
                    dtlResults.SumTotal = model.Sum(k => k.SumTotal);
                    //平均价格
                    if (dtlResults.TestCount > 0)
                        dtlResults.Price = dtlResults.SumTotal / dtlResults.TestCount;
                    long testItemID = model.ElementAt(0).TestItemID.Value;
                    ReaTestItem testItem = null;
                    if (!testItemList.ContainsKey(testItemID))
                    {
                        testItem = IDReaTestItemDao.Get(testItemID);
                        if (testItem != null)
                            testItemList.Add(testItemID, testItem);
                    }
                    else
                    {
                        testItem = testItemList[testItemID];
                    }
                    if (testItem != null)
                    {
                        if (string.IsNullOrEmpty(dtlResults.TestItemSName))
                            dtlResults.TestItemSName = testItem.SName;
                        if (string.IsNullOrEmpty(dtlResults.TestItemSName))
                            dtlResults.TestItemEName = testItem.EName;
                    }
                    if (!string.IsNullOrEmpty(dtlResults.TestType))
                    {
                        dtlResults.TestTypeName = LisTestType.GetStatusDic()[dtlResults.TestType.ToString()].Name;
                    }
                    dtlResults.StartDate = startDate;
                    dtlResults.EndDate = endDate;
                    dtlList.Add(dtlResults);
                }
            }
            dtlList = dtlList.OrderBy(p => p.TestEquipID).ThenBy(p => p.TestItemID).ThenBy(p => p.TestType).ToList();
            return dtlList;
        }
        public BaseResultDataValue SearchLisResultsEChartsVOByHql(int statisticType, bool showZero, string startDate, string endDate, string dtlHql, string deptGoodsHql, string reaGoodsHql, string sort)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            IList<ReaLisTestStatisticalResults> dtlList = this.SearchListByHQL(dtlHql);

            string jkey = "TestItemCName";
            JObject jresult = new JObject();
            JObject jdataset = new JObject();
            JArray dimensionsData = new JArray();
            dimensionsData.Add(jkey);
            dimensionsData.Add("常规检测量");
            dimensionsData.Add("复检检测量");
            dimensionsData.Add("质控检测量");
            JArray sourceData = new JArray();
            //按使用仪器+项目Id分组
            var groupByList = dtlList.GroupBy(p => new
            {
                p.TestEquipID,
                p.TestItemID
            }).OrderBy(p => p.Key.TestEquipID).ThenBy(p => p.Key.TestItemID);
            Dictionary<long, ReaTestItem> testItemList = new Dictionary<long, ReaTestItem>();
            string testItemCName = "";
            double common = 0, review = 0, qcdata = 0;
            /***
             dataset: {
                 dimensions: ['product2', '2015', '2016', '2017'],
                 source: [
                     {product2: 'Matcha Latte', '2015': 43.3, '2016': 85.8, '2017': 93.7},
                     {product2: 'Milk Tea', '2015': 83.1, '2016': 73.4},
                     {product2: 'Cheese Cocoa', '2015': 86.4, '2016': 65.2, '2017': 82.5},
                     {product2: 'Walnut Brownie', '2015': 72.4, '2016': 53.9, '2017': 39.1}
                 ]
             }
              */
            foreach (var groupBy in groupByList)
            {
                testItemCName = "";
                common = 0;
                review = 0;
                qcdata = 0;
                long testItemID = groupBy.ElementAt(0).TestItemID.Value;
                ReaTestItem testItem = null;
                if (!testItemList.ContainsKey(testItemID))
                {
                    testItem = IDReaTestItemDao.Get(testItemID);
                    if (testItem != null)
                        testItemList.Add(testItemID, testItem);
                }
                else
                {
                    testItem = testItemList[testItemID];
                }
                if (testItem != null)
                    testItemCName = testItem.CName;
                else
                    testItemCName = groupBy.ElementAt(0).TestItemCName;
                //按某仪器某试剂的检测类型分组
                var groupByList2 = groupBy.GroupBy(p => new
                {
                    p.TestType
                }).OrderBy(p => p.Key.TestType);
                foreach (var groupBy2 in groupByList2)
                {
                    if (groupBy2.Key.TestType == LisTestType.常规.Key)
                    {
                        common = groupBy2.Sum(k => k.TestCount); ;
                    }
                    else if (groupBy2.Key.TestType == LisTestType.复检.Key)
                    {
                        review = groupBy2.Sum(k => k.TestCount); ;
                    }
                    else if (groupBy2.Key.TestType == LisTestType.质控.Key)
                    {
                        qcdata = groupBy2.Sum(k => k.TestCount); ;
                    }
                }
                JObject jsource = new JObject();
                jsource.Add(jkey, testItemCName);
                jsource.Add("常规检测量", Math.Round(common, 2));
                jsource.Add("复检检测量", Math.Round(review, 2));
                jsource.Add("质控检测量", Math.Round(qcdata, 2));
                sourceData.Add(jsource);
            }
            jdataset.Add("dimensions", dimensionsData);
            jdataset.Add("source", sourceData);
            jresult.Add("dataset", jdataset);
            //ZhiFang.Common.Log.Log.Debug("jresult:" + jresult.ToString());
            baseResultDataValue.ResultDataValue = jresult.ToString();
            return baseResultDataValue;
        }

        #endregion
    }
}