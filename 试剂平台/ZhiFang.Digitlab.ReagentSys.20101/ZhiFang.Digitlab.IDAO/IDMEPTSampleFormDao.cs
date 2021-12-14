using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
	public interface IDMEPTSampleFormDao : IDBaseDao<MEPTSampleForm, long>
	{
        /// <summary>
        /// 根据where条件查询样本单(样本状态、样本操作等)
        /// </summary>
        /// <param name="strHqlWhere">where条件</param>
        /// <param name="strOrder">排序字段</param>
        /// <param name="start">页数</param>
        /// <param name="count">每页显示记录数</param>
        /// <returns>EntityList&lt;MEPTSampleForm&gt;</returns>
        EntityList<MEPTSampleForm> SearchMEPTSampleFormByHQL(string strHqlWhere, string strOrder, int start, int count);
        ///// <summary>
        ///// 根据检验小组ID、样本单执行日期查找属于该小组的样本单
        ///// </summary>
        ///// <param name="startDate">开始日期</param>
        ///// <param name="endDate">结束日期</param>
        ///// <param name="gmGroupID">检验小组ID,如果ID小于等于0,查询所有小组样本</param>
        ///// <param name="sort">排序字段</param>
        ///// <returns>IList&lt;MEPTSampleForm&gt;</returns>
        //IList<MEPTSampleForm> SearchMEPTSampleFormByGMGroupID(string startDate, string endDate, long gmGroupID, string sort);

        EntityList<MEPTSampleForm> SearchMEPTSampleFormByGMGroupID(string startDate, string endDate, long gmGroupID, int start, int count, string order);
	} 
}