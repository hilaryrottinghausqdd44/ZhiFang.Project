
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.SA;
using ZhiFang.IBLL.SA;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.SA;
using Newtonsoft.Json.Linq;
using ZhiFang.BLL.SA.SADimensionStrategy;
using ZhiFang.BLL.SA.SAQITypeOfStrategy;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.BLL.SA
{
    /// <summary>
    ///
    /// </summary>
    public class BLStatTotal : BaseBLL<LStatTotal>, ZhiFang.IBLL.SA.IBLStatTotal
    {
        #region 质量指标分类类型统计
        public BaseResultDataValue SearchFailedTotalAndSpecimenTotalOfYearAndMonth(string classificationId, string qitype, string year, string month)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(qitype) || QualityIndicatorType.GetStatusDic()[qitype] == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "未识别传入的质量指标类型所属分类ID值";
                return baseResultDataValue;
            }
            if (string.IsNullOrEmpty(year) && string.IsNullOrEmpty(month))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "指定的年或指定的月份不能为空";
                return baseResultDataValue;
            }
            JObject jresult = new JObject();
            JObject jYear = new JObject();
            JObject jMonth = new JObject();

            SPSAQualityIndicatorType searchEntity = new SPSAQualityIndicatorType();
            if (!string.IsNullOrEmpty(year))
            {
                jYear = SearchFailedTotalAndSpecimenTotalOfYear(classificationId, qitype, year);
            }
            if (!string.IsNullOrEmpty(month))
            {
                jMonth = SearchFailedTotalAndSpecimenTotalOfMonth(classificationId, qitype, month);
            }
            jresult.Add("Title", QualityIndicatorType.GetStatusDic()[qitype].Name);
            jresult.Add("Year", jYear);
            jresult.Add("Month", jMonth);
            baseResultDataValue.ResultDataValue = jresult.ToString();
            return baseResultDataValue;
        }
        private JObject SearchFailedTotalAndSpecimenTotalOfYear(string classificationId, string qitype, string year)
        {
            JObject jYear = new JObject();
            string whereHql = "";
            string order = "";
            double yFailedTotal = 0, ySpecimenTotal = 0;

            if (!string.IsNullOrEmpty(year))
            {
                ZhiFang.Common.Log.Log.Debug("SearchFailedTotalAndSpecimenTotalOfYear.开始:");
                string statDateType = LStatTotalStatDateType.本年.Key;
                string startDate = year + "-01-01 00:00:00";
                string endDate = year + "-12-31 23:59:59";
                //先按统计条件从缓存或统计结果表获取
                string lstattotalWhere = GetLStattotalWhere(classificationId, qitype, statDateType, "", startDate, endDate);
                //从缓存或统计结果获取数据 
                LStatTotal lstatTotal = GetLStatTotalOfCache(lstattotalWhere);
                if (lstatTotal != null && !string.IsNullOrEmpty(lstatTotal.StatValue))
                {
                    jYear = JObject.Parse(lstatTotal.StatValue);
                    if (jYear != null)
                        return jYear;
                }

                //从业务数据表获取
                if (lstatTotal == null)
                {
                    ZhiFang.Common.Log.Log.Debug("SearchFailedTotalAndSpecimenTotalOfYear.从业务数据表获取:查询条件" + lstattotalWhere);
                    SPSAQualityIndicatorType searchEntity = new SPSAQualityIndicatorType();
                    searchEntity.StartDate = startDate;
                    searchEntity.EndDate = endDate;
                    IList<SPSAQualityIndicatorType> nfYearList = ((IDLStatTotalDao)base.DBDao).SearchSPSAQualityIndicatorTypeList(searchEntity, whereHql, order, -1, -1);
                    var qitypeYearList = nfYearList.Where(p => p.QIndicatorTypeId == qitype);
                    yFailedTotal = qitypeYearList.Count();
                    ySpecimenTotal = nfYearList.Count();
                    //某一质量指标分类类型的某一年的不合格标本总数
                    jYear.Add("FailedTotal", Math.Round(yFailedTotal, 2));
                    //某一质量指标分类类型的某一年的标本总量
                    jYear.Add("SpecimenTotal", Math.Round(ySpecimenTotal, 2));
                    bool result = AddLStatTotal(lstattotalWhere, jYear.ToString(), classificationId, qitype, statDateType, "", startDate, endDate);
                }
            }
            return jYear;
        }
        private JObject SearchFailedTotalAndSpecimenTotalOfMonth(string classificationId, string qitype, string month)
        {
            JObject jMonth = new JObject();
            string whereHql = "";
            string order = "";
            double mFailedTotal = 0, mSpecimenTotal = 0;

            DateTime fristDate = DateTime.Parse(month + "-01 00:00:00");
            string startDate = fristDate.ToString("yyyy-MM-dd 00:00:00");
            string endDate = fristDate.AddDays(1 - fristDate.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd  23:59:59");
            if (!string.IsNullOrEmpty(month))
            {
                ZhiFang.Common.Log.Log.Debug("SearchFailedTotalAndSpecimenTotalOfMonth.开始:");
                string statDateType = LStatTotalStatDateType.月.Key;
                //先按统计条件从缓存或统计结果表获取
                string lstattotalWhere = GetLStattotalWhere(classificationId, qitype, statDateType, "", startDate, endDate);
                //从缓存或统计结果获取数据 
                LStatTotal lstatTotal = GetLStatTotalOfCache(lstattotalWhere);
                if (lstatTotal != null && !string.IsNullOrEmpty(lstatTotal.StatValue))
                {
                    jMonth = JObject.Parse(lstatTotal.StatValue);
                    if (jMonth != null)
                        return jMonth;
                }

                //从业务数据表获取
                if (lstatTotal == null)
                {
                    ZhiFang.Common.Log.Log.Debug("SearchFailedTotalAndSpecimenTotalOfMonth.从业务数据表获取:查询条件" + lstattotalWhere);
                    SPSAQualityIndicatorType searchEntity = new SPSAQualityIndicatorType();
                    searchEntity.StartDate = startDate;
                    searchEntity.EndDate = endDate;
                    IList<SPSAQualityIndicatorType> nfmonthList = ((IDLStatTotalDao)base.DBDao).SearchSPSAQualityIndicatorTypeList(searchEntity, whereHql, order, -1, -1);
                    var qitypeMonthList = nfmonthList.Where(p => p.QIndicatorTypeId == qitype);
                    mFailedTotal = qitypeMonthList.Count();
                    mSpecimenTotal = nfmonthList.Count();
                    //某一质量指标分类类型的某一月的不合格标本总数
                    jMonth.Add("FailedTotal", Math.Round(mFailedTotal, 2));
                    //某一质量指标分类类型的某一月的标本总量
                    jMonth.Add("SpecimenTotal", Math.Round(mSpecimenTotal, 2));
                    bool result = AddLStatTotal(lstattotalWhere, jMonth.ToString(), classificationId, qitype, statDateType, "", startDate, endDate);
                }
            }
            return jMonth;
        }
        public BaseResultDataValue SearchSPSAQualityIndicatorTypeOfEChart(string classificationId, string qitype, string statDateType, string sadimension, string startDate, string endDate, string where, string sort)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            IList<SPSAQualityIndicatorType> entityList = this.SearchSPSAQualityIndicatorTypeList(classificationId, qitype, statDateType, sadimension, startDate, endDate, where, sort, -1, -1).list;

            //因为每个质量指标类型对应的统计纬度可能并不一样,需要分开单独处理
            QITypeOfEChartStrategy ofEChartStrategy = null;
            if (qitype == QualityIndicatorType.标本类型错误率.Key.ToString())
            {
                ofEChartStrategy = new OfSampleTypeSADimension();
            }
            else if (qitype == QualityIndicatorType.标本容器错误率.Key.ToString())
            {
                ofEChartStrategy = new OfSTContainerErrorSADimension();
            }
            else if (qitype == QualityIndicatorType.标本采集量错误率.Key.ToString())
            {
                ofEChartStrategy = new OfSTCollectionErrorSADimension();
            }
            else if (qitype == QualityIndicatorType.血培养污染统计.Key.ToString())
            {
                ofEChartStrategy = new OfBloodCulturePollutionSADimension();
            }
            else if (qitype == QualityIndicatorType.抗凝标本凝集.Key.ToString())
            {
                ofEChartStrategy = new OfASpecimenAgglutinationSADimension();
            }
            else if (qitype == QualityIndicatorType.标本标识错误.Key.ToString())
            {
                ofEChartStrategy = new OfSIdentificationErrorSADimension();
            }
            else if (qitype == QualityIndicatorType.标本检验前储存不适当.Key.ToString())
            {
                ofEChartStrategy = new OfSStorageIsErrorSADimension();
            }
            else if (qitype == QualityIndicatorType.标本运输途中损坏.Key.ToString())
            {
                ofEChartStrategy = new OfSTransportationDamageDuring();
            }
            else if (qitype == QualityIndicatorType.标本运输温度不适当.Key.ToString())
            {
                ofEChartStrategy = new OfSTransportTemperatureImproperRate();
            }
            else if (qitype == QualityIndicatorType.标本运输时间过长.Key.ToString())
            {
                ofEChartStrategy = new OfSpecimenTransportTimeIsLong();
            }
            else if (qitype == QualityIndicatorType.标本采集时机不正确.Key.ToString())
            {
                ofEChartStrategy = new OfSpecimenCollectionTimingErrorRate();
            }
            else if (qitype == QualityIndicatorType.微生物标本污染.Key.ToString())
            {
                ofEChartStrategy = new OfMicroSpecimenContamination();
            }
            else if (qitype == QualityIndicatorType.其他类型.Key.ToString())
            {
                ofEChartStrategy = new OfQIndicatorTypeOtherTypes();
            }
            else
            {
                ofEChartStrategy = new OfQIndicatorTypeOtherTypes();
            }
            if (ofEChartStrategy != null)
            {
                SAQITypeOfEChartContext ofEChartContext = new SAQITypeOfEChartContext(entityList, qitype, sadimension, ofEChartStrategy);
                baseResultDataValue = ofEChartContext.GetSAQualityIndicatorTypeOfEChar();
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "该功能未实现!";
            }
            return baseResultDataValue;
        }
        public EntityList<SPSAQualityIndicatorType> SearchSPSAQualityIndicatorTypeList(string classificationId, string qitype, string statDateType, string sadimension, string startDate, string endDate, string where, string sort, int page, int limit)
        {
            EntityList<SPSAQualityIndicatorType> entityList = new EntityList<SPSAQualityIndicatorType>();
            entityList.list = new List<SPSAQualityIndicatorType>();

            //处理好的统计结果集合
            IList<SPSAQualityIndicatorType> saList = new List<SPSAQualityIndicatorType>();
            //先按统计条件从缓存或统计结果表获取
            string lstattotalWhere = GetLStattotalWhere(classificationId, qitype, statDateType, sadimension, startDate, endDate);
            //从缓存或统计结果获取数据 
            LStatTotal lstatTotal = GetLStatTotalOfCache(lstattotalWhere);
            if (lstatTotal != null && !string.IsNullOrEmpty(lstatTotal.StatValue))
            {
                JObject jresult = JObject.Parse(lstatTotal.StatValue);
                //Newtonsoft.Json.JsonConvert.DeserializeObject<SPSAQualityIndicatorType>(jresult.SelectToken("LStatTotalList").ToString());
                var lstatTotalList = jresult.SelectToken("LStatTotalList").ToList();
                foreach (var item in lstatTotalList)
                {
                    SPSAQualityIndicatorType model = item.ToObject<SPSAQualityIndicatorType>();
                    saList.Add(model);
                }
            }

            //从对应的业务表获取统计数据并存储到统计结果表中
            if (saList.Count <= 0)
            {
                saList = GetSPSAQualityIndicatorTypeOfNF(classificationId, qitype, statDateType, sadimension, startDate, endDate, where, sort, page, limit);
                if (saList.Count > 0)
                {
                    JObject jresult = new JObject();
                    jresult.Add("LStatTotalList", JArray.FromObject(saList));
                    ParseObjectProperty parseObjectProperty = new ParseObjectProperty("");
                    // string jresult = parseObjectProperty.GetObjectListPlanish<SPSAQualityIndicatorType>(saList);
                    bool result = AddLStatTotal(lstattotalWhere, jresult.ToString(), classificationId, qitype, statDateType, sadimension, startDate, endDate);
                }
            }

            entityList.count = saList.Count;
            //分页处理
            if (limit > 0 && limit < saList.Count)
            {
                int startIndex = limit * (page - 1);
                int endIndex = limit;
                var list = saList.Skip(startIndex).Take(endIndex);
                if (list != null)
                    saList = list.ToList();
            }
            entityList.list = saList;
            return entityList;
        }
        private LStatTotal GetLStatTotalOfCache(string lstattotalWhere)
        {
            //统计结果信息
            LStatTotal lstatTotal = null;
            //从服务器内存缓存中获取
            if (SADataCache.LStatTotalDataCache.Keys.Contains(lstattotalWhere))
            {
                ZhiFang.Common.Log.Log.Debug("GetLStatTotalOfCache.从服务器内存缓存中获取:查询条件" + lstattotalWhere);
                lstatTotal = SADataCache.LStatTotalDataCache[lstattotalWhere];
            }
            if (lstatTotal == null)
            {
                //从结果表里查询获取
                ZhiFang.Common.Log.Log.Debug("GetLStatTotalOfCache.从结果表里查询获取:查询条件" + lstattotalWhere);
                IList<LStatTotal> lsList = this.SearchListByHQL(lstattotalWhere);
                if (lsList.Count > 0)
                {
                    lstatTotal = lsList.OrderByDescending(p => p.DataAddTime).ElementAt(0);
                    if (!SADataCache.LStatTotalDataCache.Keys.Contains(lstattotalWhere))
                    {
                        SADataCache.LStatTotalDataCache.Add(lstattotalWhere, lstatTotal);
                    }
                }
            }
            return lstatTotal;
        }
        private string GetLStattotalWhere(string classificationId, string qitype, string statDateType, string sadimension, string startDate, string endDate)
        {
            string lstattotalWhere = String.Format(" lstattotal.ClassificationId={0} and lstattotal.StatType={1} and lstattotal.StatDateType={2}", classificationId, qitype, statDateType);
            if (!string.IsNullOrEmpty(sadimension))
            {
                lstattotalWhere += " and lstattotal.StatDValue=" + sadimension;
            }
            if (!string.IsNullOrEmpty(startDate))
            {
                lstattotalWhere += " and lstattotal.StatDateBegin='" + startDate + "'";
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                lstattotalWhere += " and lstattotal.StatDateEnd='" + endDate + "'";
            }

            return lstattotalWhere;
        }
        private IList<SPSAQualityIndicatorType> GetSPSAQualityIndicatorTypeOfNF(string classificationId, string qitype, string statDateType, string sadimension, string startDate, string endDate, string where, string sort, int page, int limit)
        {
            IList<SPSAQualityIndicatorType> saList = new List<SPSAQualityIndicatorType>();
            SPSAQualityIndicatorType searchEntity = new SPSAQualityIndicatorType();
            searchEntity.StartDate = startDate;
            searchEntity.EndDate = endDate;
            string whereHql = "";
            string order = "";
            //原始业务数据集合
            ZhiFang.Common.Log.Log.Debug("GetSPSAQualityIndicatorTypeOfNF.从业务数据表获取:");
            IList<SPSAQualityIndicatorType> nfList = ((IDLStatTotalDao)base.DBDao).SearchSPSAQualityIndicatorTypeList(searchEntity, whereHql, order, -1, -1);
            //因为每个质量指标类型对应的统计纬度可能并不一样,需要分开单独处理
            QITypeOfEChartStrategy ofEChartStrategy = null;
            if (qitype == QualityIndicatorType.标本类型错误率.Key.ToString())
            {
                ofEChartStrategy = new OfSampleTypeSADimension();
            }
            else if (qitype == QualityIndicatorType.标本容器错误率.Key.ToString())
            {
                ofEChartStrategy = new OfSTContainerErrorSADimension();
            }
            else if (qitype == QualityIndicatorType.标本采集量错误率.Key.ToString())
            {
                ofEChartStrategy = new OfSTCollectionErrorSADimension();
            }
            else if (qitype == QualityIndicatorType.血培养污染统计.Key.ToString())
            {
                ofEChartStrategy = new OfBloodCulturePollutionSADimension();
            }
            else if (qitype == QualityIndicatorType.抗凝标本凝集.Key.ToString())
            {
                ofEChartStrategy = new OfASpecimenAgglutinationSADimension();
            }
            else if (qitype == QualityIndicatorType.标本标识错误.Key.ToString())
            {
                ofEChartStrategy = new OfSIdentificationErrorSADimension();
            }
            else if (qitype == QualityIndicatorType.标本检验前储存不适当.Key.ToString())
            {
                ofEChartStrategy = new OfSStorageIsErrorSADimension();
            }
            else if (qitype == QualityIndicatorType.标本运输途中损坏.Key.ToString())
            {
                ofEChartStrategy = new OfSTransportationDamageDuring();
            }
            else if (qitype == QualityIndicatorType.标本运输温度不适当.Key.ToString())
            {
                ofEChartStrategy = new OfSTransportTemperatureImproperRate();
            }
            else if (qitype == QualityIndicatorType.标本运输时间过长.Key.ToString())
            {
                ofEChartStrategy = new OfSpecimenTransportTimeIsLong();
            }
            else if (qitype == QualityIndicatorType.标本采集时机不正确.Key.ToString())
            {
                ofEChartStrategy = new OfSpecimenCollectionTimingErrorRate();
            }
            else if (qitype == QualityIndicatorType.微生物标本污染.Key.ToString())
            {
                ofEChartStrategy = new OfMicroSpecimenContamination();
            }
            else if (qitype == QualityIndicatorType.其他类型.Key.ToString())
            {
                ofEChartStrategy = new OfQIndicatorTypeOtherTypes();
            }
            else
            {
                ofEChartStrategy = new OfQIndicatorTypeOtherTypes();
            }

            if (ofEChartStrategy != null)
            {
                SAQITypeOfEChartContext ofEChartContext = new SAQITypeOfEChartContext(nfList, qitype, sadimension, ofEChartStrategy);
                saList = ofEChartContext.GetSAQualityIndicatorTypeOfList(startDate, endDate);
            }
            else
            {
                // baseResultDataValue.success = false;
                //baseResultDataValue.ErrorInfo = "该功能未实现!";
            }
            return saList;
        }
        private bool AddLStatTotal(string lstattotalWhere, string jresult, string classificationId, string qitype, string statDateType, string sadimension, string startDate, string endDate)
        {
            bool result = true;
            //先判断当前统计条件是否存在正在新增保存的集合里
            if (!SADataCache.AddLStatTotalKeyList.Contains(lstattotalWhere))
            {
                SADataCache.AddLStatTotalKeyList.Add(lstattotalWhere);
                LStatTotal lstatTotal = new LStatTotal();
                lstatTotal.ClassificationId = long.Parse(classificationId);
                if (!string.IsNullOrEmpty(classificationId))
                    lstatTotal.ClassificationName = LStatTotalClassification.GetStatusDic()[classificationId].Name;
                lstatTotal.StatType = long.Parse(qitype);
                if (!string.IsNullOrEmpty(qitype))
                    lstatTotal.StatName = QualityIndicatorType.GetStatusDic()[qitype].Name;
                lstatTotal.StatDateType = long.Parse(statDateType);
                if (!string.IsNullOrEmpty(sadimension))
                    lstatTotal.StatDValue = long.Parse(sadimension);
                if (!string.IsNullOrEmpty(startDate))
                    lstatTotal.StatDateBegin = DateTime.Parse(startDate);
                if (!string.IsNullOrEmpty(endDate))
                    lstatTotal.StatDateEnd = DateTime.Parse(endDate);
                lstatTotal.StatValue = jresult.ToString();
                ZhiFang.Common.Log.Log.Debug("StatValue:" + lstatTotal.StatValue);
                this.Entity = lstatTotal;
                result = this.Add();
                //保存完成后,从正在新增保存的集合移除
                SADataCache.AddLStatTotalKeyList.Remove(lstattotalWhere);
                //将lstatTotal缓存到服务器内存中
                if (!SADataCache.LStatTotalDataCache.Keys.Contains(lstattotalWhere))
                {
                    SADataCache.LStatTotalDataCache.Add(lstattotalWhere, lstatTotal);
                }
            }
            return result;
        }
 
        #endregion

    }
}