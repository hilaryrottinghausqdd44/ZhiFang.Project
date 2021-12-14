using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;
using ZhiFang.LabStar.Common;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLisEquipForm : IBGenericManager<LisEquipForm>
    {
        IList<LisEquipForm> QueryLisEquipForm(string strHqlWhere, string fields);

        EntityList<LisEquipForm> QueryLisEquipForm(string strHqlWhere, string order, int start, int count, string fields);

        //BaseResultDataValue AppendLisEquipItemResultByUpLoadInfo(long labID, string equipResultType, string equipResultInfo, SendCommDataDelegate sendCommDataMsg);

        BaseResultDataValue AddLisItemResultByEquipResult(long testFormID, long equipFormID, string equipItemID, bool changeSampleNo, bool changeTestFormID, bool isDelAuotAddItem, long? empID, string empName, string reCheckMemoInfo, ClientComputerInfo computerInfo);

        BaseResultDataValue AddBatchExtractEquipResult(string testFormIDList, string equipFormIDList, bool isChangeSampleNo, bool isDelAuotAddItem, long? empID, string empName);

        BaseResultDataValue AddLisEquipItemResult_Common(long labID, IList<EquipResult> listEquipResult, ClientComputerInfo computerInfo, SendSysMessageDelegate sendCommDataMsg);

        BaseResultDataValue AddLisEquipItemResult_Memo(long comFileID, long labID, IList<EquipMemoResult> listEquipResult, ClientComputerInfo computerInfo, SendSysMessageDelegate sendCommDataMsg);

        BaseResultDataValue AddLisEquipItemResult_QC(long comFileID, long labID, IList<EquipQCResult> listEquipQCResult, ClientComputerInfo computerInfo, SendSysMessageDelegate sendCommDataMsg);

        BaseResultDataValue AppendLisEquipItemResult_Graph(long comFileID, long labID, IList<EquipGraphResult> listEquipGraphResult, ClientComputerInfo computerInfo, SendSysMessageDelegate sendCommDataMsg);

    }
}