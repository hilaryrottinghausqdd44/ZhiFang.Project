using System.Collections.Generic;
using System.IO;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaGoodsLot : Base.IBGenericManager<ReaGoodsLot>
    {
        BaseResultBool AddAndValid(ref ReaGoodsLot reaGoodsLot);
        BaseResultBool EditValid(ref ReaGoodsLot reaGoodsLot);
        IList<ReaGoodsLot> SearchReaGoodsLotListByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit);
        EntityList<ReaGoodsLot> SearchReaGoodsLotEntityListByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit);

        /// <summary>
        /// 获取货品批号性能验证信息,导出Excel文件
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="labCName"></param>
        /// <param name="breportType"></param>
        /// <param name="where"></param>
        /// <param name="reaGoodsHql"></param>
        /// <param name="sort"></param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream SearchReaGoodsLotOfExcelByHql(long labId, string labCName, string breportType, string where, string reaGoodsHql, string sort, string frx, ref string fileName);
        BaseResultBool UpdateVerification(ReaGoodsLot entity, string fields);
        ReaGoodsLot GetVerificationMemo(ReaGoodsLot entity);
    }
}