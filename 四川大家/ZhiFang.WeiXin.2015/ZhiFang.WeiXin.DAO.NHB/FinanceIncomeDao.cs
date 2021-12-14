using NHibernate;
using System.Collections.Generic;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.Entity.Statistics;
using ZhiFang.WeiXin.IDAO;

namespace ZhiFang.WeiXin.DAO.NHB
{
    public class FinanceIncomeDao : BaseDaoNHB<FinanceIncome, long>, IDFinanceIncomeDao
    {
        /// <summary>
        /// 查询财务收入报表数据
        /// </summary>
        /// <param name="searchEntity"></param>
        /// <param name="strWhere">" and osmanagerrefundform.MRefundFormID is not null"</param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<FinanceIncome> SearchFinanceIncomeList(UserConsumerFormSearch searchEntity, string strWhere, int page, int count)
        {
            IList<FinanceIncome> tempEntityList = new List<FinanceIncome>();
            if (searchEntity == null)
            {
                return tempEntityList;
            }
            else if (!searchEntity.ConsumptionStartDate.HasValue || !searchEntity.ConsumptionEndDate.HasValue)
            {
                return tempEntityList;
            }

            List<string> paranamea = new List<string> { "ConsumptionStartDate", "ConsumptionEndDate", "BillingStartDate", "BillingEndDate", "SamplingStartDate", "SamplingEndDate", "UOFCode", "OSUserConsumerFormCode", "MRefundFormCode", "BonusFormCode", "AreaID", "UserAccountID", "DoctorAccountID", "OSUserConsumerFormStatus", "IsRefund", "strWhere" };
            object[] paravaluea = new string[paranamea.Count];

            for (int i = 0; i < paravaluea.Length; i++)
            {
                paravaluea[i] = "";
            }
            //消费日期范围
            if (searchEntity.ConsumptionStartDate.HasValue && searchEntity.ConsumptionEndDate.HasValue)
            {
                string startDate = searchEntity.ConsumptionStartDate.Value.ToString("yyyy-MM-dd");
                string endDate = searchEntity.ConsumptionEndDate.Value.ToString("yyyy-MM-dd");

                if (paranamea.IndexOf("ConsumptionStartDate") >= 0)
                    paravaluea[paranamea.IndexOf("ConsumptionStartDate")] = startDate;
                if (paranamea.IndexOf("ConsumptionEndDate") >= 0)
                    paravaluea[paranamea.IndexOf("ConsumptionEndDate")] = endDate + " 23:59:59";
            }
            //开单日期
            if (searchEntity.BillingStartDate.HasValue)
            {
                string startDate = searchEntity.BillingStartDate.Value.ToString("yyyy-MM-dd");
                if (paranamea.IndexOf("BillingStartDate") >= 0)
                    paravaluea[paranamea.IndexOf("BillingStartDate")] = startDate;
            }
            //开单日期
            if (searchEntity.BillingEndDate.HasValue)
            {
                string endDate = searchEntity.BillingEndDate.Value.ToString("yyyy-MM-dd");
                if (paranamea.IndexOf("BillingEndDate") >= 0)
                    paravaluea[paranamea.IndexOf("BillingEndDate")] = endDate + " 23:59:59";
            }
            //开单日期
            if (searchEntity.SamplingStartDate.HasValue)
            {
                string startDate = searchEntity.SamplingStartDate.Value.ToString("yyyy-MM-dd");
                if (paranamea.IndexOf("SamplingStartDate") >= 0)
                    paravaluea[paranamea.IndexOf("SamplingStartDate")] = startDate;
            }
            //采样结束日期
            if (searchEntity.SamplingEndDate.HasValue)
            {
                string endDate = searchEntity.SamplingEndDate.Value.ToString("yyyy-MM-dd");
                if (paranamea.IndexOf("SamplingEndDate") >= 0)
                    paravaluea[paranamea.IndexOf("SamplingEndDate")] = endDate + " 23:59:59";
            }
            if (!string.IsNullOrEmpty(searchEntity.UOFCode))
            {
                if (paranamea.IndexOf("UOFCode") >= 0)
                    paravaluea[paranamea.IndexOf("UOFCode")] = searchEntity.UOFCode;
            }
            if (!string.IsNullOrEmpty(searchEntity.OSUserConsumerFormCode))
            {
                if (paranamea.IndexOf("OSUserConsumerFormCode") >= 0)
                    paravaluea[paranamea.IndexOf("OSUserConsumerFormCode")] = searchEntity.OSUserConsumerFormCode;
            }
            if (!string.IsNullOrEmpty(searchEntity.MRefundFormCode))
            {
                if (paranamea.IndexOf("MRefundFormCode") >= 0)
                    paravaluea[paranamea.IndexOf("MRefundFormCode")] = searchEntity.MRefundFormCode;
            }
            if (!string.IsNullOrEmpty(searchEntity.BonusFormCode))
            {
                if (paranamea.IndexOf("BonusFormCode") >= 0)
                    paravaluea[paranamea.IndexOf("BonusFormCode")] = searchEntity.BonusFormCode;
            }
            if (searchEntity.AreaID.HasValue)
            {
                if (paranamea.IndexOf("AreaID") >= 0)
                    paravaluea[paranamea.IndexOf("AreaID")] = searchEntity.AreaID.Value.ToString();
            }
            if (searchEntity.UserAccountID.HasValue)
            {
                if (paranamea.IndexOf("UserAccountID") >= 0)
                    paravaluea[paranamea.IndexOf("UserAccountID")] = searchEntity.UserAccountID.Value.ToString();
            }
            if (searchEntity.DoctorAccountID.HasValue)
            {
                if (paranamea.IndexOf("DoctorAccountID") >= 0)
                    paravaluea[paranamea.IndexOf("DoctorAccountID")] = searchEntity.DoctorAccountID.Value.ToString();
            }
            //是否转款(结算)
            if (searchEntity.IsSettlement)
            {
                if (paranamea.IndexOf("OSUserConsumerFormStatus") >= 0)
                    paravaluea[paranamea.IndexOf("OSUserConsumerFormStatus")] = OSUserConsumerFormStatus.已结算.Key.ToString();
            }
            else
            {
                if (paranamea.IndexOf("OSUserConsumerFormStatus") >= 0)
                    paravaluea[paranamea.IndexOf("OSUserConsumerFormStatus")] = OSUserConsumerFormStatus.已结算.Key + "," + OSUserConsumerFormStatus.消费成功.Key;
            }
            if (searchEntity.IsRefund)
            {
                if (paranamea.IndexOf("IsRefund") >= 0)
                    paravaluea[paranamea.IndexOf("IsRefund")] = "1";
            }
            else {
                if (paranamea.IndexOf("IsRefund") >= 0)
                    paravaluea[paranamea.IndexOf("IsRefund")] = "0";
            }

            if (strWhere != null && strWhere.ToString().Trim() != "")
            {
                if (paranamea.IndexOf("strWhere") >= 0)
                {
                    paravaluea[paranamea.IndexOf("strWhere")] = " " + strWhere.ToString().Trim();
                }
            }
            tempEntityList = base.HibernateTemplate.FindByNamedQueryAndNamedParam<FinanceIncome>("SP_Report_FinanceIncome", paranamea.ToArray(), paravaluea);
            return tempEntityList;
        }

    }
}