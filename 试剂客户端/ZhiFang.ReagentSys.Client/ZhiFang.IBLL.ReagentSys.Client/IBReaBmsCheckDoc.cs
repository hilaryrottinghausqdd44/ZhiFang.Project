
using System.Collections.Generic;
using System.IO;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaStoreIn;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaBmsCheckDoc : IBGenericManager<ReaBmsCheckDoc>
    {
        /// <summary>
        /// 新增盘库(无机构货品盘库条件)
        /// </summary>
        /// <param name="mergeType">盘库的合并方式 1:货品ID+批号+库房+货架;</param>
        /// <returns></returns>
        BaseResultDataValue AddReaBmsCheckDoc(int mergeType, long empID, string empName);
        /// <summary>
        /// 编辑盘库总单及盘库明细信息
        /// </summary>
        /// <param name="dtEditList"></param>
        /// <param name="fieldsDtl"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool EditReaBmsCheckDocAndDtl(string[] tempArray, IList<ReaBmsCheckDtl> dtEditList, string fieldsDtl, long empID, string empName);
        /// <summary>
        /// 取消盘库,物理删除盘库总单及盘库明细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResultBool DelReaBmsCheckDoc(long id);
        /// <summary>
        /// 获取新增盘库明细货品信息
        /// </summary>
        /// <param name="checkDoc">盘库实体条件</param>
        /// <param name="reaGoodHql">机构货品条件</param>
        /// <param name="days">相同盘库条件的最近几天内未盘库的库存货品</param>
        /// <param name="zeroQtyDays">入库时间范围作为盘库数据过滤条件：包含最近___天内库存数为0的货品（生效的前提是盘库条件的“包括库存数为0的试剂勾选上”）;</param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <param name="baseResultDataValue"></param>
        /// <returns></returns>
        EntityList<ReaBmsCheckDtl> SearchAddReaBmsCheckDtlByHQL(ReaBmsCheckDoc checkDoc, string reaGoodHql, int days, int zeroQtyDays, string sort, int page, int limit, long empID, string empName, ref BaseResultDataValue baseResultDataValue);
        /// <summary>
        /// 新增盘库主单及盘库明细(有机构货品盘库条件)
        /// </summary>
        /// <param name="checkDoc"></param>
        /// <param name="dtAddList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddReaBmsCheckDocAndDtlList(ReaBmsCheckDoc checkDoc, IList<ReaBmsCheckDtl> dtAddList, long empID, string empName,bool isTakenFromQty);
        /// <summary>
        /// 依盘库Id获取盘库差异调整的盘盈入库信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue SearchReaBmsInDocOfCheckDocID(long id, bool isPlanish, string fields, long empID, string empName);
        /// <summary>
        /// 依盘库Id获取盘库差异调整的盘盈入库明细信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue SearchReaBmsInDtlListOfCheckDocID(long id, bool isPlanish, string fields, long empID, string empName);
        /// <summary>
        /// 依盘库Id获取盘库差异调整的盘亏出库信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue SearchReaBmsOutDocOfCheckDocID(long id, bool isPlanish, string fields, long deptID, string deptName, long empID, string empName);
        /// <summary>
        /// 依盘库Id获取盘库差异调整的盘亏出库明细信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue SearchReaBmsOutDtlListOfCheckDocID(long id, bool isPlanish, string fields, long empID, string empName);
        /// <summary>
        /// 依盘库Id保存盘库差异调整的盘盈入库及入库明细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResultDataValue AddReaBmsInDocAndDtlOfCheckDocID(long checkDocID, ReaBmsInDoc inDoc, IList<ReaBmsInDtlVO> dtAddList, string codeScanningMode, long empID, string empName);
        /// <summary>
        /// 依盘库Id保存盘库差异调整的盘亏出库及出库明细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResultDataValue AddReaBmsOutDocAndDtlOfCheckDocID(long checkDocID, ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> dtAddList, string codeScanningMode, long empID, string empName);
        /// <summary>
        /// 获取生成PDF格式的盘库单
        /// </summary>
        /// <param name="id">盘点单Id</param>
        /// <param name="frx">盘点单模板名称</param>
        /// <returns></returns>
        Stream GetReaBmsCheckDocAndDtlOfPdf(long id, string sort, string frx);
    }
}