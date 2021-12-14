using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using ZhiFang.BLL.SA.SADimensionStrategy;
using ZhiFang.BLL.SA.SAQITypeOfStrategy;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.SA;

namespace ZhiFang.BLL.SA.SAQITypeOfStrategy
{
    /// <summary>
    /// 质量指标分类类型图表数据处理-标本运输温度不适当
    /// </summary>
    class OfSTransportTemperatureImproperRate : QITypeOfEChartStrategy
    {
        public override BaseResultDataValue GetSAQualityIndicatorTypeOfEChart(IList<SPSAQualityIndicatorType> saList, string qIndicatorType, string sadimension)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            JObject jresult = new JObject();
            JArray axisData = new JArray();
            JArray legendData = new JArray();
            JObject seriesData = new JObject();

            JArray failedAmountData = new JArray();
            //JArray failedTotalData = new JArray();
            JArray specimenTotalData = new JArray();
            JArray failedRateData = new JArray();

            bool isHasNull = false;
            var axisDataValue = "";
            foreach (var vo in saList)
            {
                if (sadimension == STransportTemperatureImproperRate.就诊类型.Key)
                {
                    axisDataValue = vo.SickTypeCName;
                }
                else if (sadimension == STransportTemperatureImproperRate.就诊类型送检人.Key)
                {
                    axisDataValue = vo.SickTypeCName + "_" + vo.NurseSendCarrier;
                }
                else if (sadimension == STransportTemperatureImproperRate.就诊类型样本类型.Key)
                {
                    axisDataValue = vo.SickTypeCName + "_" + vo.SampleTypeCName;
                }
                else if (sadimension == STransportTemperatureImproperRate.就诊类型科室.Key)
                {
                    axisDataValue = vo.SickTypeCName + "_" + vo.DeptCName;
                }

                else if (sadimension == STransportTemperatureImproperRate.样本类型.Key)
                {
                    axisDataValue = vo.SampleTypeCName;
                }
                else if (sadimension == STransportTemperatureImproperRate.样本类型科室.Key)
                {
                    axisDataValue = vo.SampleTypeCName + "_" + vo.DeptCName;
                }
                else if (sadimension == STransportTemperatureImproperRate.样本类型送检人.Key)
                {
                    axisDataValue = vo.SampleTypeCName + "_" + vo.NurseSendCarrier;
                }

                else if (sadimension == STransportTemperatureImproperRate.科室.Key)
                {
                    axisDataValue = vo.DeptCName;
                }
                else if (sadimension == STransportTemperatureImproperRate.科室样本类型.Key)
                {
                    axisDataValue = vo.DeptCName + "_" + vo.SampleTypeCName;
                }
                else if (sadimension == STransportTemperatureImproperRate.科室送检人.Key)
                {
                    axisDataValue = vo.DeptCName + "_" + vo.NurseSendCarrier;
                }
                else if (sadimension == STransportTemperatureImproperRate.送检人.Key)
                {
                    axisDataValue = vo.NurseSendCarrier;
                }
                else if (sadimension == STransportTemperatureImproperRate.采样人.Key)
                {
                    axisDataValue = vo.Collecter;
                }
                else if (sadimension == STransportTemperatureImproperRate.按年份.Key)
                {
                    axisDataValue = vo.Year;
                }
                else if (sadimension == STransportTemperatureImproperRate.按季度.Key)
                {
                    axisDataValue = vo.Quarter;
                }
                else if (sadimension == STransportTemperatureImproperRate.按月份.Key)
                {
                    axisDataValue = vo.Month;
                }

                if (!string.IsNullOrEmpty(axisDataValue) && !axisData.Contains(axisDataValue))
                {
                    axisData.Add(axisDataValue);
                }
                else if (isHasNull == false && !axisData.Contains("其他"))
                {
                    isHasNull = true;
                    axisData.Add("其他");
                }
                if (!vo.FailedAmount.HasValue) vo.FailedAmount = 0;
                if (!vo.SpecimenTotal.HasValue) vo.SpecimenTotal = 0;
                if (!vo.FailedTotal.HasValue) vo.FailedTotal = 0;
                if (!vo.FailedRate.HasValue) vo.FailedRate = 0;
                failedAmountData.Add(vo.FailedAmount);
                //failedTotalData.Add(vo.FailedTotal);
                specimenTotalData.Add(vo.SpecimenTotal);
                failedRateData.Add(vo.FailedRate);
            }
            jresult.Add("axisData", axisData);

            legendData.Add("错误数");
            legendData.Add("总数");
            legendData.Add("不合格占比");
            jresult.Add("legendData", legendData);

            seriesData.Add("failedAmount", failedAmountData);
            //seriesData.Add("failedTotal", failedTotalData);
            seriesData.Add("specimenTotal", specimenTotalData);
            seriesData.Add("failedRate", failedRateData);
            jresult.Add("seriesData", seriesData);
            baseResultDataValue.ResultDataValue = jresult.ToString();
            return baseResultDataValue;
        }
        public override IList<SPSAQualityIndicatorType> GetSAQualityIndicatorTypeOfList(IList<SPSAQualityIndicatorType> nfList, string qitype, string sadimension, string startDate, string endDate)
        {
            IList<SPSAQualityIndicatorType> saList = new List<SPSAQualityIndicatorType>();

            DimensionStrategy dimensionStrategy = null;
            if (sadimension == STransportTemperatureImproperRate.就诊类型.Key)
            {
                dimensionStrategy = new OfSickTypeGroupBy();
            }
            else if (sadimension == STransportTemperatureImproperRate.就诊类型送检人.Key)
            {
                dimensionStrategy = new OfSickTypeAndInspectorGroupBy();
            }
            else if (sadimension == STransportTemperatureImproperRate.就诊类型样本类型.Key)
            {
                dimensionStrategy = new OfSickTypeAndSampleTypeGroupBy();
            }
            else if (sadimension == STransportTemperatureImproperRate.就诊类型科室.Key)
            {
                dimensionStrategy = new OfSickTypeAndDeptGroupBy();
            }

            else if (sadimension == STransportTemperatureImproperRate.样本类型.Key)
            {
                dimensionStrategy = new OfSampleTypeGroupBy();
            }
            else if (sadimension == STransportTemperatureImproperRate.样本类型科室.Key)
            {
                dimensionStrategy = new OfSampleTypeAndDeptGroupBy();
            }
            else if (sadimension == STransportTemperatureImproperRate.样本类型送检人.Key)
            {
                dimensionStrategy = new OfSampleTypeAndInspectorGroupBy();
            }

            else if (sadimension == STransportTemperatureImproperRate.科室.Key)
            {
                dimensionStrategy = new OfDeptGroupBy();
            }
            else if (sadimension == STransportTemperatureImproperRate.科室样本类型.Key)
            {
                dimensionStrategy = new OfDeptAndSampleTypeGroupBy();
            }
            else if (sadimension == STransportTemperatureImproperRate.科室送检人.Key)
            {
                dimensionStrategy = new OfDeptAndInspectorGroupBy();
            }
            else if (sadimension == STransportTemperatureImproperRate.送检人.Key)
            {
                dimensionStrategy = new OfInspectorGroupBy();
            }

            else if (sadimension == STransportTemperatureImproperRate.采样人.Key)
            {
                dimensionStrategy = new OfCollecterGroupBy();
            }
            else if (sadimension == STransportTemperatureImproperRate.按年份.Key)
            {
                dimensionStrategy = new OfYearGroupBy();
            }
            else if (sadimension == STransportTemperatureImproperRate.按季度.Key)
            {
                dimensionStrategy = new OfQuarterGroupBy();
            }
            else if (sadimension == STransportTemperatureImproperRate.按月份.Key)
            {
                dimensionStrategy = new OfMonthGroupBy();
            }
            if (dimensionStrategy != null)
            {
                DimensionContext dimensionContext = new DimensionContext(nfList, qitype, dimensionStrategy);
                saList = dimensionContext.GetSPSAQualityIndicatorTypeOfGroupBy();
            }
            return saList;
        }
    }
}
