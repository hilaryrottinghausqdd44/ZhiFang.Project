

using System.Collections.Generic;
using System.IO;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.WebAssist
{
    /// <summary>
    ///
    /// </summary>
    public interface IBGKSampleRequestForm : IBGenericManager<GKSampleRequestForm>
    {
        BaseResultDataValue AddGKSampleRequestFormAndDtl(GKSampleRequestForm entity, IList<SCRecordDtl> dtlEntityList, long empID, string empName);

        BaseResultBool EditGKSampleRequestFormAndDtl(GKSampleRequestForm entity, string[] tempArray, IList<SCRecordDtl> dtlEntityList, long empID, string empName);

        /// <summary>
        /// 院感申请单更新处理
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tempArray"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool EditGKSampleRequestForm(GKSampleRequestForm entity, string[] tempArray, long empID, string empName);

        /// <summary>
        /// 根据条件查询院感登记Id获取样本信息及记录项明细信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<GKSampleRequestForm> SearchGKSampleRequestFormAndDtlByHQL(string strHqlWhere, string Order, int page, int count, bool isGetTestItem);

        /// <summary>
        /// 按院感登记Id获取样本信息及记录项明细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GKSampleRequestForm GetGKSampleRequestFormAndDtlById(long id);

        /// <summary>
        /// 获院感登记报表PDF文件
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="labCName"></param>
        /// <param name="breportType"></param>
        /// <param name="groupType"></param>
        /// <param name="docVO"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream SearchGKSampleRequestFormOfPdfByHQL(long labId, string labCName, string breportType, string groupType, string docVO, string where, string sort, string frx, ref string fileName);

        /// <summary>
        /// 获院感登记报表,导出Excel文件
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="labCName"></param>
        /// <param name="breportType"></param>
        /// <param name="groupType"></param>
        /// <param name="docVO"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream SearchGKSampleRequestFormOfExcelByHql(long labId, string labCName, string breportType, string groupType, string docVO, string where, string sort, string frx, ref string fileName);

        /// <summary>
        /// 院感统计--按科室统计表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<GKOfDeptEvaluation> SearchGKListByHQLInfectionOfDept(string docVO, string where, string sort, int page, int count);

        /// <summary>
        /// 院感统计--按季度统计表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<GKOfDeptEvaluation> SearchGKListByHQLInfectionOfQuarterly(string docVO, string where, string sort, int page, int count);

        /// <summary>
        /// 院感统计--评价报告表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<GKOfDeptEvaluation> SearchGKListByHQLInfectionOfEvaluation(string docVO, string where, string sort, int page, int count);

        /// <summary>
        /// 登录成功后,获取库存预警,效期预警,注册证预警提示信息
        /// </summary>
        /// <returns></returns>
        BaseResultDataValue GetGKWarningAlertInfo(long empID, string empName);

    }
}