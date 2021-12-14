

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBloodBagOperation : IBGenericManager<BloodBagOperation>
	{
        /// <summary>
        /// 新增血袋接收登记
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="bloodBagOperationDtlList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddBloodBagOperationAndDtlOfHandover(BloodBagOperation entity, IList<BloodBagOperationDtl> bloodBagOperationDtlList, long empID, string empName);
        /// <summary>
        /// 获取血袋接收登记信息(包含血袋的外观,完整性)
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<BloodBagOperation> SearchBloodBagOperationAndDtlOfHandoverByHQL(string strHqlWhere, string Order, int page, int count);

        /// <summary>
        /// 根据血袋接收登记ID获取血袋接收登记信息(包含血袋的外观,完整性)
        /// </summary>
        /// <param name="longID"></param>
        /// <returns></returns>
        BloodBagHandoverVO GetBloodBagHandoverVO(long longID);
        /// <summary>
        /// 修改血袋接收登记信息(包含血袋的外观,完整性)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        BaseResultBool EditBloodBagHandoverVO(BloodBagHandoverVO entity);

        /// <summary>
        /// 新增血袋回收登记
        /// </summary>
        /// <param name="bloodBagOperationList"></param>
        /// <param name="isHasTrans">是否输血过程记录登记后才能血袋回收登记</param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddBloodBagOperationListOfRecycle(IList<BloodBagOperation> bloodBagOperationList, bool isHasTrans, long empID, string empName);

        /// <summary>
        /// (输血申请综合查询)按申请单号获取包含血袋接收及血袋回收信息
        /// </summary>
        /// <param name="reqFormId"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<BloodBagOperationVO> SearchBloodBagOperationVOOfByBReqFormID(string reqFormId, string sort, int page, int limit);

    }
}