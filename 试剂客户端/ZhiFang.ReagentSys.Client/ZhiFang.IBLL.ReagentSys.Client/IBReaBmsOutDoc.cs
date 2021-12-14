using Newtonsoft.Json.Linq;
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
    public interface IBReaBmsOutDoc : IBGenericManager<ReaBmsOutDoc>
    {
        /// <summary>
        /// 出库查询定制
        /// 一.出库申请 默认数据过滤条件:申请人等于登录人
        /// 二.直接出库管理默认数据过滤条件:分配给登录人所属的全部库房权限的出库, 库房条件为库房
        /// 三.出库管理(申请) 默认数据过滤条件:分配给登录人所属的全部库房权限的出库,库房条件为库房
        /// 四.出库管理(全部)默认数据过滤条件:分配给登录人所属的全部库房权限的出库,库房条件为库房
        /// </summary>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="isUseEmpOut">是否按移库人权限出库</param>
        /// <param name="type">1:出库申请;2:直接出库管理;3:出库管理(申请);4:出库管理(全部)</param>
        /// <param name="empId"></param>
        /// <returns></returns>
        EntityList<ReaBmsOutDoc> SearchListByHQL(string where, string order, int page, int count, string isUseEmpOut, string type, long empId);
        /// <summary>
        /// 出库申请时,获取某一申请货品的已申请总数及当前库存总数
        /// </summary>
        /// <param name="qtyHql"></param>
        /// <param name="dtlHql"></param>
        /// <returns></returns>
        BaseResultDataValue SearchSumReqGoodsQtyAndCurrentQtyByHQL(string qtyHql, string dtlHql, string goodsId);
        /// <summary>
        /// 新增直接出库(完成)并修改出库明细对应的库存记录信息(带库存条码扫码操作记录处理)
        /// </summary>
        /// <param name="outDoc">出库总单</param>
        /// <param name="dtlAddList">出库明细单</param>
        /// <param name="isEmpOut">是否按出库人权限出库</param>
        /// <param name="isNeedPerformanceTest">库存货品是否需要性能验证后才能使用出库</param>
        /// <returns></returns>
        BaseResultDataValue AddOutDocAndOutDtlListOfComp(ref ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> dtlAddList, bool isEmpOut, bool isNeedPerformanceTest, long empID, string empName);
        /// <summary>
        /// 获取出库单PDF文件
        /// </summary>
        /// <param name="reaReportClass"></param>
        /// <param name="id"></param>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <param name="breportType">BTemplateType的Name</param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream SearchPdfReportOfTypeById(string reaReportClass, long id, long labID, string labCName, string breportType, string frx, ref string fileName);
        /// <summary>
        /// 获取出库单Excel文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <param name="breportType">BTemplateType的Name</param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream SearchExcelReportOfExcelById(long id, long labID, string labCName, string breportType, string frx, ref string fileName);
        /// <summary>
        /// 出库申请单查询定制(数据范围限制申请人所属部门及子部门)
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<ReaBmsOutDoc> SearchReaBmsOutDocByReqDeptHQL(long deptId, string strHqlWhere, string Order, int page, int count);
        /// <summary>
        /// 新增出库申请单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtlAddList"></param>
        /// <param name="isEmpOut">是否按出库人权限出库</param>
        /// <param name="isNeedPerformanceTest">库存货品是否需要性能验证后才能使用出库</param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddReaBmsOutDocAndDtlOfApply(ReaBmsOutDoc entity, IList<ReaBmsOutDtl> dtlAddList, bool isEmpOut, bool isNeedPerformanceTest, long empID, string empName);
        /// <summary>
        /// 编辑保存出库申请单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtlAddList"></param>
        ///<param name="dtlEditList"></param>
        /// <param name="isEmpOut">是否按出库人权限出库</param>
        /// <param name="isNeedPerformanceTest">库存货品是否需要性能验证后才能使用出库</param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool UpdateReaBmsOutDocAndDtl(ReaBmsOutDoc entity, IList<ReaBmsOutDtl> dtlAddList, IList<ReaBmsOutDtl> dtlEditList, bool isEmpOut, bool isNeedPerformanceTest, long empID, string empName);
        /// <summary>
        /// 出库申请单的审核/审批
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tempArray"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool UpdateReaBmsOutDocByCheck(ReaBmsOutDoc entity, string[] tempArray, long empID, string empName);
        /// <summary>
        /// 对出库申请单进行出库完成处理
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtlAddList"></param>
        /// <param name="dtlEditList"></param>
        /// <param name="isEmpOut">是否按出库人权限出库</param>
        /// <param name="isNeedPerformanceTest">库存货品是否需要性能验证后才能使用出库</param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool UpdateReaBmsOutDocAndDtlOfComp(ReaBmsOutDoc entity, IList<ReaBmsOutDtl> dtlAddList, IList<ReaBmsOutDtl> dtlEditList, bool isEmpOut, bool isNeedPerformanceTest, long empID, string empName);

        #region 出库单上传至智方试剂平台--不在同一数据库--客户端部分
        BaseResultDataValue GetUploadPlatformOutDocAndDtl(long outDocId, string reaServerLabcCode, ref JObject jPostData, ref ReaBmsOutDoc outDoc);
        #endregion

        #region 出库单上传至智方试剂平台--不在同一数据库--平台部分
        BaseResultDataValue SaveClientOutDocAndDtl(ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> outDtlList, long empID, string empName);
        #endregion

        /// <summary>
        /// 更新出库单同步到第三方系统标志，1为已经同步到第三方
        /// </summary>
        BaseResultDataValue EditReaBmsOutDocThirdFlag(long id, int isThirdFlag);

        /// <summary>
        /// 智方试剂平台使用
        /// 查询 状态=出库单上传平台 且 订货方类型=调拨 的出库单
        /// </summary>
        EntityList<ReaBmsOutDoc> GetPlatformOutDocListByDBClient(string strHqlWhere, string sort, int page, int limit);
    }
}