using System;
using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLisQCData : IBGenericManager<LisQCData>
    {
        BaseResultDataValue GetCalcTargetByQCData(long qcItemID, string beginDate, string endDate);

        BaseResultDataValue GetCalcSDByQCData(long qcItemID, string beginDate, string endDate);

        BaseResultDataValue GetCalcTargetSDByQCData(string listQCItemID, string beginDate, string endDate);
        List<LisQCDataMonthVO> getQCMothsData(long QCItemID, bool buse, DateTime startDate, DateTime endDate);
        bool AddLisQCData(LisQCData LisQCData);
        void EditLisQCData(LisQCData entity);
        QCMFigureLJ QCMFigureLJ(long qCItemId, DateTime startDate, DateTime endDate);
        QCMFigureZ QCMFigureZ(long equipId, long QCMId, long itemId, DateTime startDate, DateTime endDate);
        QCMFigureValueRange QCMFigureValueRange(long qCItemId, DateTime startDate, DateTime endDate);
        QCMFigureQualitative QCMFigureQualitative(long qCItemId, DateTime startDate, DateTime endDate);
        QCMFigureValueMonica QCMFigureValueMonica(long qCItemId, DateTime startDate, DateTime endDate);
        QCMFigureYouden QCMFigureYouden(List<long> qCItemIds, DateTime startDate, DateTime endDate);
        QCMFigureNormalDistribution QCMFigureNormalDistribution(List<long> qCItemIds, DateTime startDate, DateTime endDate);
        QCMFigureCumulativeSumGraph QCMFigureCumulativeSumGraph(long qCItemId, double target, double sD, DateTime startDate, DateTime endDate);
        QCMFigureFrequencyDistribution QCMFigureFrequencyDistribution(long qCItemId, double target, double sD, DateTime startDate, DateTime endDate);
    }
}