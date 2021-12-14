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
    public interface IBReaBmsTransferDoc : IBGenericManager<ReaBmsTransferDoc>
    {
        /// <summary>
        /// 移库查询定制
        /// 一.移库申请 默认数据过滤条件:申请人等于登录人
        /// 二.直接移库管理默认数据过滤条件:分配给登录人所属的全部库房权限的移库, 库房条件为源库房
        /// 三.移库管理(申请) 默认数据过滤条件:分配给登录人所属的全部库房权限的移库,库房条件为源库房
        /// 四.移库管理(全部)默认数据过滤条件:分配给登录人所属的全部库房权限的移库,库房条件为源库房
        /// </summary>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="isUseEmpOut">是否按移库人权限移库</param>
        /// <param name="type"></param>
        /// <param name="empId"></param>
        /// <returns></returns>
        EntityList<ReaBmsTransferDoc> SearchListByHQL(string strHqlWhere, string order, int page, int count, string isUseEmpOut, string type, long empId);

        /// <summary>
        /// 移库申请时,获取某一申请货品的已申请总数及当前库存总数
        /// </summary>
        /// <param name="qtyHql"></param>
        /// <param name="dtlHql"></param>
        /// <returns></returns>
        BaseResultDataValue SearchSumReqGoodsQtyAndCurrentQtyByHQL(string qtyHql, string dtlHql, string goodsId);
        /// <summary>
        /// 新增直接移库完成并修改移库对应的库存记录
        /// </summary>
        /// <param name="transferDoc"></param>
        /// <param name="transferDtlList"></param>
        /// <param name="isEmpTransfer">是否按移库人权限移库</param>
        /// <returns></returns>
        BaseResultDataValue AddTransferDocAndDtlListOfComp(ReaBmsTransferDoc transferDoc, IList<ReaBmsTransferDtl> transferDtlList, bool isEmpTransfer, long empID, string empName);
        /// <summary>
        /// 移库申请单查询定制(数据范围限制申请人所属部门及子部门)
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<ReaBmsTransferDoc> SearchReaBmsTransferDocByReqDeptHQL(long deptId, string strHqlWhere, string Order, int page, int count);
        /// <summary>
        /// 新增移库申请
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtlAddList"></param>
        /// <param name="isEmpTransfer">是否按移库人权限移库</param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddTransferDocAndDtlOfApply(ReaBmsTransferDoc entity, IList<ReaBmsTransferDtl> dtlAddList, bool isEmpTransfer, long empID, string empName);
        /// <summary>
        /// 编辑保存移库申请
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtlAddList"></param>
        ///<param name="dtlEditList"></param>
        /// <param name="isEmpTransfer">是否按移库人权限移库</param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool UpdateTransferDocAndDtl(ReaBmsTransferDoc entity, IList<ReaBmsTransferDtl> dtlAddList, IList<ReaBmsTransferDtl> dtlEditList, bool isEmpTransfer, long empID, string empName);
        /// <summary>
        /// 获取移库单PDF文件
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
        /// 获取移库单Excel文件
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
        /// 新增移库入库,将选择的入库单和库存货品进行移库
        /// </summary>
        /// <param name="inDoc"></param>
        /// <param name="transferDoc"></param>
        /// <param name="transferDtlList"></param>
        /// <param name="isEmpTransfer"></param>
        /// <returns></returns>
        BaseResultDataValue AddTransferDocOfInDoc(ReaBmsInDoc inDoc, ReaBmsTransferDoc transferDoc, IList<ReaBmsTransferDtl> transferDtlList, bool isEmpTransfer, long empID, string empName);
    }
}