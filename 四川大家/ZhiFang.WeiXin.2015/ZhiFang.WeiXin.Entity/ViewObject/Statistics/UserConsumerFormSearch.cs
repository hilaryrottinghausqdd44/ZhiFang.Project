using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.WeiXin.Entity.Statistics
{
    /// <summary>
    /// 项目统计报表及财务收入报表的查询实体
    /// </summary>
    public class UserConsumerFormSearch
    {
        /// <summary>
        /// 消费开始日期
        /// </summary>
        public DateTime? ConsumptionStartDate { get; set; }
        /// <summary>
        /// 消费结束日期
        /// </summary>
        public DateTime? ConsumptionEndDate { get; set; }
        /// <summary>
        /// 开单开始日期
        /// </summary>
        public DateTime? BillingStartDate { get; set; }
        /// <summary>
        /// 开单结束日期
        /// </summary>
        public DateTime? BillingEndDate { get; set; }
        /// <summary>
        /// 采样开始日期
        /// </summary>
        public DateTime? SamplingStartDate { get; set; }
        /// <summary>
        /// 采样结束日期
        /// </summary>
        public DateTime? SamplingEndDate { get; set; }
        /// <summary>
        /// 用户订单编号
        /// </summary>
        public string UOFCode { get; set; }
        /// <summary>
        /// 子订单编号(消费单编号)
        /// </summary>
        public string OSUserConsumerFormCode { get; set; }
        /// <summary>
        /// 退费单编号
        /// </summary>
        public string MRefundFormCode { get; set; }
        /// <summary>
        /// 转款单编号(结算单号)
        /// </summary>
        public string BonusFormCode { get; set; }
        /// <summary>
        /// 区域ID
        /// </summary>
        public long? AreaID { get; set; }
        /// <summary>
        /// 用户账户信息ID
        /// </summary>
        public long? UserAccountID { get; set; }
        /// <summary>
        /// 开单医生帐户Id
        /// </summary>
        public long? DoctorAccountID { get; set; }

        /// <summary>
        /// 是否转款(结算)
        /// </summary>
        public bool IsSettlement { get; set; }
        /// <summary>
        /// 是否退款
        /// </summary>
        public bool IsRefund { get; set; } 
        /// <summary>
        /// 套餐项目Id
        /// </summary>
        public long? ItemID { get; set; }
    }
}
