using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
    public interface IDMEGroupSampleFormDao : IDBaseDao<MEGroupSampleForm, long>
    {
        IList<MEGroupSampleForm> SearchMEGroupSampleFormAndMEMicroSmearValueListByHQL(string strHqlWhere, int page, int count);
        IList<MEGroupSampleForm> SearchMEGroupSampleFormAndMEMicroInoculantListByHQL(string strHqlWhere, int page, int count);
        IList<MEGroupSampleForm> SearchMEGroupSampleFormAndMEGroupSampleItemListByHQL(string strHqlWhere, int page, int count);
        IList<MEGroupSampleForm> SearchMEGroupSampleFormByHQL(string strHqlWhere, int page, int count);
        /// <summary>
        /// 根据where条件查询小组样本单(样本状态、样本操作等)
        /// </summary>
        /// <param name="strHqlWhere">where条件</param>
        /// <param name="strOrder">排序字段</param>
        /// <param name="start">页数</param>
        /// <param name="count">每页显示记录数</param>
        /// <returns>EntityList&lt;MEGroupSampleForm&gt;</returns>
        EntityList<MEGroupSampleForm> SearchMEGroupSampleFormByHQL(string strHqlWhere, string strOrder, int start, int count);

        /// <summary>
        /// 根据where条件查询检验单的操作记录
        /// </summary>
        /// <param name="strHqlWhere">HQL条件</param>
        /// <param name="strOrder">排序</param>
        /// <returns></returns>
        IList<MEGroupSampleForm> SearchMEGroupSampleFormOperateInfoByHQL(string strHqlWhere, string strOrder);

        /// <summary>
        /// 查询某一个检验小组下的所有小于当天的为检验中状态的检验单总数信息
        /// </summary>
        /// <param name="gmgroupId">检验小组Id</param>
        /// <returns></returns>
        int GetMEGroupSampleFormCountsByGMGroupId(string gmgroupId);

        /// <summary>
        /// 普通专业样本量统计
        /// </summary>
        /// <param name="DateStartDate"></param>
        /// <param name="DateEndDate"></param>
        /// <param name="GroupList"></param>
        /// <param name="ItemList"></param>
        /// <param name="DeptList"></param>
        /// <param name="SampleTypeList"></param>
        /// <param name="MainStateList"></param>
        /// <param name="GroupField"></param>
        /// <returns></returns>
        IList<PMEGroupSampleFormSampleQuantityStatistics> SearchNormalSampleQuantityStatistics(string DateStartDate, string DateEndDate, string GroupList, string ItemList, string DeptList, string SampleTypeList, string MainStateList, string GroupField);

        /// <summary>
        /// 普通专业工作量统计
        /// </summary>
        /// <param name="DateStartDate"></param>
        /// <param name="DateEndDate"></param>
        /// <param name="GroupList"></param>
        /// <param name="ItemList"></param>
        /// <param name="DeptList"></param>
        /// <param name="SampleTypeList"></param>
        /// <param name="MainStateList"></param>
        /// <param name="GroupField"></param>
        /// <returns></returns>
        IList<PMEGroupSampleFormWorkQuantityStatistics> SearchNormalWorkQuantityStatistics(string DateStartDate, string DateEndDate, string GroupList, string ItemList, string DeptList, string SampleTypeList, string MainStateList, string GroupField);

        /// <summary>
        /// 微生物样本量统计
        /// </summary>
        /// <param name="DateStartDate"></param>
        /// <param name="DateEndDate"></param>
        /// <param name="GroupList"></param>
        /// <param name="ItemList"></param>
        /// <param name="DeptList"></param>
        /// <param name="SampleTypeList"></param>
        /// <param name="PositiveFlag"></param>
        /// <param name="MainStateList"></param>
        /// <param name="GroupField"></param>
        /// <returns></returns>
        IList<PMEGroupSampleFormSampleQuantityStatistics> SearchMicroSampleQuantityStatistics(string DateStartDate, string DateEndDate, string GroupList, string ItemList, string DeptList, string SampleTypeList, string PositiveFlag, string MainStateList, string GroupField);

        /// <summary>
        /// 微生物工作量统计
        /// </summary>
        /// <param name="DateStartDate"></param>
        /// <param name="DateEndDate"></param>
        /// <param name="GroupList"></param>
        /// <param name="ItemList"></param>
        /// <param name="DeptList"></param>
        /// <param name="SampleTypeList"></param>
        /// <param name="PositiveFlag"></param>
        /// <param name="MainStateList"></param>
        /// <param name="GroupField"></param>
        /// <returns></returns>
        IList<PMEGroupSampleFormWorkQuantityStatistics> SearchMicroWorkQuantityStatistics(string DateStartDate, string DateEndDate, string GroupList, string ItemList, string DeptList, string SampleTypeList, string PositiveFlag, string MainStateList, string GroupField);

        /// <summary>
        /// 撤消核收:清空原检验单项目及检验单项目的医嘱及样本信息,物理删除及删除样本单及医嘱单信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateMEGroupSampleFormOfRevokeDistribution(MEGroupSampleForm model);

    }
}