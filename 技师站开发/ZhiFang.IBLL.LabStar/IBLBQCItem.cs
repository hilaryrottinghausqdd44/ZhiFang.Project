using System;
using System.Collections.Generic;
using System.Data;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBQCItem : IBGenericManager<LBQCItem>
    {
        EntityList<LBQCItem> QueryLBQCItem(string strHqlWhere, string Order, int start, int count);
        List<LBQCItemVO> GetQCDays(long equipID, long qCMatID, DateTime dateTime, int page, int limit);
        List<MultipleConcentrationQCM> GetMultipleConcentrationQCM(long EquipId, long itemId, string qCMModule, string qCMGroup, DateTime startDate, DateTime endDate);
        BaseResultTree<MultipleConcentrationQCMTree> GetMultipleConcentrationQCMTree();
        BaseResultTree GetQCMothTree();
        IList<LBQCItem> SearchLBQCItemByHQL(IList<LBQCItem> list);

        DataTable GetMultipleConcentrationQCMCompareInfo(string QCItemIds, DateTime startDate, DateTime endDate);
        List<MultipleConcentrationQCM> GetQCMothItem(long QCItemId, DateTime startDate, DateTime endDate);
        List<MultipleConcentrationQCMInfoFull> GetMultipleConcentrationQCMInfoFull(string QCItemIds, DateTime startDate, DateTime endDate);
        DataTable SearchEquipDayQCM();
        DataTable SearchEquipDayQCMList(long EquipId, string EquipModel, string EquipGroup, DateTime startDate, DateTime endDate);
        List<LisQCDataMonthVO> SearchEquipDayQCData(long qCMId, DateTime startDate, DateTime endDate);
        BaseResultTree GetOutControlEQ_QCMTree();
        BaseResultTree GetOutControlEQ_ITEMTree();
        BaseResultTree GetOutControlITEM_QC_QCMTree();
        List<MultipleConcentrationQCMInfoFull> SearchEquipDayQCMFull(string QCMID, DateTime startDate, DateTime endDate);
        string QCMReportFormPrint(Dictionary<string, string> keyValuePairs, DateTime StartDate, DateTime EndDate, string printType, string tempath, SortedList<string, System.IO.Stream> streams);
        List<LBQCMaterial> SearchEquipDayQCM(long EquipId, string EquipModel, string EquipGroup);
        string FindTemplate(int QCMCount, LBQCItem lBQCItem, string printType, string QCDataType);
    }
}