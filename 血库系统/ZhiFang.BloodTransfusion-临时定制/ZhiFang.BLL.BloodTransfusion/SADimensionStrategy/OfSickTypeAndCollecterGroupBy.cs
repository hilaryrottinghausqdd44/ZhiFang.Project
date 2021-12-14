using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.BloodTransfusion;

namespace ZhiFang.BLL.BloodTransfusion.SADimensionStrategy
{
    /// <summary>
    /// 质量指标分类类型-统计纬度按就诊类型+采样人分组
    /// </summary>
    class OfSickTypeAndCollecterGroupBy : DimensionStrategy
    {
        public override IList<SPSAQualityIndicatorType> GetSPSAQualityIndicatorTypeOfGroupBy(IList<SPSAQualityIndicatorType> nfList, string qIndicatorType)
        {
            IList<SPSAQualityIndicatorType> saList = new List<SPSAQualityIndicatorType>();
            double? allTotal = nfList.Count();
            var groupBy = nfList.GroupBy(p => new
            {
                p.SickTypeNo,
                p.CollecterID
            });
            foreach (var group in groupBy)
            {
                SPSAQualityIndicatorType entity = new SPSAQualityIndicatorType();
                entity.QIndicatorTypeId = qIndicatorType;
                entity.SickTypeNo = group.Key.SickTypeNo;
                entity.SickTypeCName = group.ElementAt(0).SickTypeCName;
                entity.CollecterID = group.Key.CollecterID;
                entity.Collecter = group.ElementAt(0).Collecter;

                var tempList = nfList.Where(p => p.SickTypeNo == group.Key.SickTypeNo && p.CollecterID == group.Key.CollecterID);
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
