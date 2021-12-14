using System;
using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IDAO.NHB.BloodTransfusion;

namespace ZhiFang.DAO.NHB.BloodTransfusion
{
    public class LStatTotalDao : BaseDaoNHBService<LStatTotal, long>, IDLStatTotalDao
    {
        /// <summary>
        /// 获取质量指标类型统计原始数据源
        /// </summary>
        /// <param name="searchEntity"></param>
        /// <param name="strWhere">""</param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<SPSAQualityIndicatorType> SearchSPSAQualityIndicatorTypeList(SPSAQualityIndicatorType searchEntity, string where, string order, int page, int count)
        {
            IList<SPSAQualityIndicatorType> tempEntityList = new List<SPSAQualityIndicatorType>();
            if (searchEntity == null)
            {
                return tempEntityList;
            }
            else if (string.IsNullOrEmpty(searchEntity.StartDate) && string.IsNullOrEmpty(searchEntity.EndDate))
            {
                return tempEntityList;
            }

            List<string> paranamea = new List<string> { "StartDate", "EndDate", "QIndicatorTypeId", "PhrasesWatchClassID", "RefuseID", "Where", "Order" };
            object[] paravaluea = new string[paranamea.Count];

            for (int i = 0; i < paravaluea.Length; i++)
            {
                paravaluea[i] = "";
            }
            //日期范围
            if (!string.IsNullOrEmpty(searchEntity.StartDate))
            {
                string startDate = DateTime.Parse(searchEntity.StartDate).ToString("yyyy-MM-dd 00:00:00");
                if (paranamea.IndexOf("StartDate") >= 0)
                    paravaluea[paranamea.IndexOf("StartDate")] = startDate;
            }
            if (!string.IsNullOrEmpty(searchEntity.EndDate))
            {
                string endDate = DateTime.Parse(searchEntity.EndDate).ToString("yyyy-MM-dd 23:59:59");//.AddDays(1)
                if (paranamea.IndexOf("EndDate") >= 0)
                    paravaluea[paranamea.IndexOf("EndDate")] = endDate;// 23:59:59
            }
            if (!string.IsNullOrEmpty(searchEntity.QIndicatorTypeId))
            {
                if (paranamea.IndexOf("QIndicatorTypeId") >= 0)
                    paravaluea[paranamea.IndexOf("QIndicatorTypeId")] = searchEntity.QIndicatorTypeId;
            }
            if (!string.IsNullOrEmpty(searchEntity.PhrasesWatchClassID))
            {
                if (paranamea.IndexOf("PhrasesWatchClassID") >= 0)
                    paravaluea[paranamea.IndexOf("PhrasesWatchClassID")] = searchEntity.PhrasesWatchClassID;
            }
            if (!string.IsNullOrEmpty(searchEntity.RefuseID))
            {
                if (paranamea.IndexOf("RefuseID") >= 0)
                    paravaluea[paranamea.IndexOf("RefuseID")] = searchEntity.RefuseID;
            }
            if (!string.IsNullOrEmpty(where))
            {
                if (paranamea.IndexOf("Where") >= 0)
                    paravaluea[paranamea.IndexOf("Where")] = where.Trim();
            }
            if (!string.IsNullOrEmpty(order))
            {
                if (paranamea.IndexOf("Order") >= 0)
                    paravaluea[paranamea.IndexOf("Order")] = order.Trim();
            }
            tempEntityList = base.HibernateTemplate.FindByNamedQueryAndNamedParam<SPSAQualityIndicatorType>("SP_SA_QualityIndicatorType", paranamea.ToArray(), paravaluea);
            return tempEntityList;
        }
    }
}