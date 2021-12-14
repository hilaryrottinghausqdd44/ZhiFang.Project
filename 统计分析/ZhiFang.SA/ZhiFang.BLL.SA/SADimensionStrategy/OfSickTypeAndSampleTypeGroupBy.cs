using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.SA;

namespace ZhiFang.BLL.SA.SADimensionStrategy
{
    /// <summary>
    /// 质量指标分类类型-统计纬度按就诊类型+样本类型分组
    /// </summary>
    class OfSickTypeAndSampleTypeGroupBy : DimensionStrategy
    {
        public override IList<SPSAQualityIndicatorType> GetSPSAQualityIndicatorTypeOfGroupBy(IList<SPSAQualityIndicatorType> nfList, string qIndicatorType)
        {
            IList<SPSAQualityIndicatorType> saList = new List<SPSAQualityIndicatorType>();
            double? allTotal = nfList.Count();
            var groupBy = nfList.GroupBy(p => new
            {
                p.SickTypeNo,
                p.SampleTypeNo
            });
            foreach (var group in groupBy)
            {
                SPSAQualityIndicatorType entity = new SPSAQualityIndicatorType();
                entity.QIndicatorTypeId = qIndicatorType;
                entity.SickTypeNo = group.Key.SickTypeNo;
                entity.SickTypeCName = group.ElementAt(0).SickTypeCName;
                entity.SampleTypeNo = group.Key.SampleTypeNo;
                entity.SampleTypeCName = group.ElementAt(0).SampleTypeCName;

                var tempList = nfList.Where(p => p.SickTypeNo == group.Key.SickTypeNo && p.SampleTypeNo == group.Key.SampleTypeNo);
                entity.FailedAmount = tempList.Where(p => p.QIndicatorTypeId == qIndicatorType).Count();
                entity.FailedTotal = tempList.Where(p => string.IsNullOrEmpty(p.QIndicatorTypeId) == false).Count();
                if (entity.FailedTotal.HasValue && entity.FailedTotal.Value > 0) entity.FailedRate = entity.FailedAmount / entity.FailedTotal * 100;
                entity.SpecimenTotal = tempList.Count();
                entity.AllTotal = allTotal;

                if (!entity.FailedAmount.HasValue) entity.FailedAmount = 0;
                entity.FailedAmount = Math.Round(entity.FailedAmount.Value, 2);
                if (!entity.FailedTotal.HasValue) entity.FailedTotal = 0;
                entity.FailedTotal = Math.Round(entity.FailedTotal.Value, 2);
                if (!entity.FailedRate.HasValue) entity.FailedRate = 0;
                entity.FailedRate = Math.Round(entity.FailedRate.Value, 2);
                if (!entity.SpecimenTotal.HasValue) entity.SpecimenTotal = 0;
                if (!entity.AllTotal.HasValue) entity.AllTotal = 0;
                saList.Add(entity);
            }
            return saList;
        }
    }
}
