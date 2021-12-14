using System;
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
    public interface IBReaBmsReqDoc : IBGenericManager<ReaBmsReqDoc>
    {

        /// <summary>
        /// 部门采购申请新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtAddList">新增的明细集合</param>
        /// <returns></returns>
        BaseResultDataValue AddReaBmsReqDocAndDt(ReaBmsReqDoc entity, IList<ReaBmsReqDtl> dtAddList, long empID, string empName);
        /// <summary>
        /// 部门采购申请更新
        /// </summary>
        /// <param name="entity">待更新的主单信息</param>
        /// <param name="dtAddList">新增的明细集合</param>
        /// <param name="dtEditList">待更新的明细集合</param>
        /// <returns></returns>
        BaseResultBool UpdateReaBmsReqDocAndDt(ReaBmsReqDoc entity, string[] tempArray, IList<ReaBmsReqDtl> dtAddList, IList<ReaBmsReqDtl> dtEditList, long empID, string empName);
        /// <summary>
        /// 部门采购申请明细更新(验证主单后只操作明细)
        /// </summary>
        /// <param name="entity">主单信息</param>
        /// <param name="dtAddList">新增的明细集合</param>
        /// <param name="dtEditList">待更新的明细集合</param>
        /// <returns></returns>
        BaseResultBool UpdateReaBmsReqDtlOfCheck(ReaBmsReqDoc entity, string[] tempArray, IList<ReaBmsReqDtl> dtAddList, IList<ReaBmsReqDtl> dtEditList, long empID, string empName);
        /// <summary>
        /// 部门采购申请审核
        /// </summary>
        /// <param name="entity">待审核的主单信息</param>
        /// <param name="dtAddList">审核前的新增的明细集合</param>
        /// <param name="dtEditList">审核前的待更新的明细集合</param>
        /// <returns></returns>
        BaseResultBool UpdateReaBmsReqDocAndDtOfCheck(ReaBmsReqDoc entity, string[] tempArray, IList<ReaBmsReqDtl> dtAddList, IList<ReaBmsReqDtl> dtEditList, long empID, string empName);
        /// <summary>
        /// 将选择的申请主单(已审核)生成客户端订单信息
        /// </summary>
        /// <param name="idStr">当前选择的申请单IDStr</param>
        /// <param name="commonIsMerge">常规申请是否按申请部门合并</param>
        /// <param name="ugentIsMerge">紧急申请是否按申请部门合并</param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <param name="deptName"></param>
        /// <param name="reaServerLabcCode">实验室所属机构平台编码</param>
        /// <returns></returns>
        BaseResultBool AddReaCenOrgReaBmsCenOrderDocOfReaBmsReqDocIDStr(string idStr, bool commonIsMerge, bool ugentIsMerge, long empID, string empName, long deptID, string deptName, string reaServerLabcCode, string labcName);
        /// <summary>
        /// 复制申请
        /// </summary>
        /// <param name="id">选择的申请总单Id</param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddReaBmsReqDocAndDtOfCopyApply(long id, long deptID, string deptName, long empID, string empName);

        /// <summary>
        /// 获取试剂耗材采购申请单PDF文件
        /// </summary>
        /// <param name="reaReportClass"></param>
        /// <param name="id"></param>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <param name="breportType"></param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream SearchPdfReportOfTypeById(string reaReportClass, long id, long labID, string labCName, string breportType, string frx, ref string fileName);
        /// <summary>
        /// 将选择的多个采购申请单按'供货商+货品编码+包装单位'合并后,生成PDF报表文件
        /// </summary>
        /// <param name="reaReportClass"></param>
        /// <param name="id"></param>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <param name="breportType"></param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream SearchReaBmsReqDocMergeReportOfPdfByIdStr(string reaReportClass, string idStr, long labID, string labCName, string breportType, string frx, ref string fileName);
        /// <summary>
        /// 获取试剂耗材采购申请单Excel文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <param name="breportType"></param>
        /// <param name="frx"></param>
        /// <param name="fileNam"></param>
        /// <returns></returns>
        Stream SearchExcelReportOfExcelById(long id, long labID, string labCName, string breportType, string frx, ref string fileNam);
    }
}