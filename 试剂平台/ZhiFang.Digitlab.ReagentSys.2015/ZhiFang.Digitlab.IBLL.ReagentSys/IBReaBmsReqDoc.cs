

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.IBLL;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
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
        BaseResultBool UpdateReaBmsReqDocAndDtOfCheck(ReaBmsReqDoc entity,string [] tempArray, IList<ReaBmsReqDtl> dtAddList, IList<ReaBmsReqDtl> dtEditList, long empID, string empName);
        /// <summary>
        /// 依据客户端的申请主单(已审核)生成客户端订单信息
        /// </summary>
        /// <param name="idStr"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool AddReaCenOrgBmsCenOrderDocOfReaBmsReqDocIDStr(string idStr, long empID, string empName);

    }
}