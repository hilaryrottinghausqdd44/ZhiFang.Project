using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.WeiXin.Entity
{
    /// <summary>
    /// 文件物理存储时，做一个处理：在文件名后+（.zf）,用来防止病毒文件挂在服务器直接执行
    /// </summary>
    public enum FileExt
    {
        zf
    }

    public enum UserSearchReportDataRoundType
    {
        OneMonth=1,
        SixMonth=2,
        SixMonthBefore=3
    }
    public enum TestItemSuperGroupClass
    {
        ALL = 0,
        DOCTORCOMBICHARGE = 1,
        OFTEN = 2,
        COMBI = 3,
        CHARGE = 4,
        SUPERGROUP = 5,
        COMBIITEMPROFILE = 6
    }
    public class BaseClassDicEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string FontColor { get; set; }
        public string BGColor { get; set; }
    }

    #region 退费申请单
    /// <summary>
    /// 退款申请单的退款状态
    /// </summary>
    public static class RefundFormStatus
    {
        //public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 暂存 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "暂存", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 申请 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "申请", Code = "Applyed", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 一审中 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "一审中", Code = "Reviewing", FontColor = "#ffffff", BGColor = "#71ba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 一审通过 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "一审通过", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 一审退回 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "一审退回", Code = "UnReview", FontColor = "#ffffff", BGColor = "#2aa515" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 二审中 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "二审中", Code = "TwoReviewing", FontColor = "#ffffff", BGColor = "#12abe3" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 二审通过 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "二审通过", Code = "TwoReviewed", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 二审退回 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "二审退回", Code = "UnTwoReview", FontColor = "#ffffff", BGColor = "#1195db" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 财务退回 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "财务退回", Code = "UnFinanceReview", FontColor = "#ffffff", BGColor = "#dd6572" });
        //public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 财务打款 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "财务打款", Code = "FinancePay", FontColor = "#ffffff", BGColor = "#e97f36" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 退款完成 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "退款完成", Code = "Payed", FontColor = "#ffffff", BGColor = "#568f36" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 退款异常 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "退款异常", Code = "UnPay", FontColor = "#ffffff", BGColor = "#538f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            //dic.Add(RefundStatus.暂存.Key, RefundStatus.暂存.Value);
            dic.Add(RefundFormStatus.申请.Key, RefundFormStatus.申请.Value);
            dic.Add(RefundFormStatus.一审中.Key, RefundFormStatus.一审中.Value);
            dic.Add(RefundFormStatus.一审通过.Key, RefundFormStatus.一审通过.Value);
            dic.Add(RefundFormStatus.一审退回.Key, RefundFormStatus.一审退回.Value);
            dic.Add(RefundFormStatus.二审中.Key, RefundFormStatus.二审中.Value);
            dic.Add(RefundFormStatus.二审通过.Key, RefundFormStatus.二审通过.Value);
            dic.Add(RefundFormStatus.二审退回.Key, RefundFormStatus.二审退回.Value);
            dic.Add(RefundFormStatus.财务退回.Key, RefundFormStatus.财务退回.Value);
            //dic.Add(RefundFormStatus.财务打款.Key, RefundFormStatus.财务打款.Value);
            dic.Add(RefundFormStatus.退款完成.Key, RefundFormStatus.退款完成.Value);
            dic.Add(RefundFormStatus.退款异常.Key, RefundFormStatus.退款异常.Value);
            return dic;
        }
    }
    /// <summary>
    /// 退费申请单的退款方式
    /// </summary>
    public static class OSDoctorBonusRefundType
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 微信退回 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "微信退回", Code = "WeChat", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        //public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 微信企业付款 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "微信企业付款", Code = "WeChat2", FontColor = "#ffffff", BGColor = "#71ba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 银行转账 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "银行转账", Code = "Bank", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(OSDoctorBonusRefundType.微信退回.Key, OSDoctorBonusRefundType.微信退回.Value);
            //dic.Add(OSDoctorBonusRefundType.微信企业付款.Key, OSDoctorBonusRefundType.微信企业付款.Value);
            dic.Add(OSDoctorBonusRefundType.银行转账.Key, OSDoctorBonusRefundType.银行转账.Value);
            return dic;
        }
    }
    #endregion

    /// <summary>
    /// 用户订单状态
    /// </summary>
    public static class UserOrderFormStatus
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 待缴费 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "待缴费", Code = "UnPay", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 已交费 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "已交费", Code = "Payed", FontColor = "#ffffff", BGColor = "#71ba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 使用中 = new KeyValuePair<string, BaseClassDicEntity>("31", new BaseClassDicEntity() { Id = "31", Name = "使用中", Code = "Useing", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 部分使用 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "部分使用", Code = "PartialUse", FontColor = "#ffffff", BGColor = "#7cba59" });        
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 完全使用 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "完全使用", Code = "Useed", FontColor = "#ffffff", BGColor = "#2aa515" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 取消订单 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "取消订单", Code = "CancelApply", FontColor = "#ffffff", BGColor = "#12abe3" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 取消处理中 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "取消处理中", Code = "Canceling", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 取消成功 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "取消成功", Code = "Canceled", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 退款申请 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "退款申请", Code = "RefundApply", FontColor = "#ffffff", BGColor = "#1195db" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 退款申请处理中 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "退款申请处理中", Code = "RefundApplying", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 退款申请被打回 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "退款申请被打回", Code = "RefundApplyBack", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 退款中 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "退款中", Code = "Refunding", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 退款完成 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", Name = "退款完成", Code = "Refunded", FontColor = "#ffffff", BGColor = "#e97f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(UserOrderFormStatus.待缴费.Key, UserOrderFormStatus.待缴费.Value);
            dic.Add(UserOrderFormStatus.已交费.Key, UserOrderFormStatus.已交费.Value);
            dic.Add(UserOrderFormStatus.使用中.Key, UserOrderFormStatus.使用中.Value);
            dic.Add(UserOrderFormStatus.部分使用.Key, UserOrderFormStatus.部分使用.Value);
            dic.Add(UserOrderFormStatus.完全使用.Key, UserOrderFormStatus.完全使用.Value);
            dic.Add(UserOrderFormStatus.取消订单.Key, UserOrderFormStatus.取消订单.Value);
            dic.Add(UserOrderFormStatus.取消处理中.Key, UserOrderFormStatus.取消处理中.Value);
            dic.Add(UserOrderFormStatus.取消成功.Key, UserOrderFormStatus.取消成功.Value);
            dic.Add(UserOrderFormStatus.退款申请.Key, UserOrderFormStatus.退款申请.Value);
            dic.Add(UserOrderFormStatus.退款申请处理中.Key, UserOrderFormStatus.退款申请处理中.Value);
            dic.Add(UserOrderFormStatus.退款中.Key, UserOrderFormStatus.退款中.Value);
            dic.Add(UserOrderFormStatus.退款完成.Key, UserOrderFormStatus.退款完成.Value);
            return dic;
        }
    }

    /// <summary>
    /// 医嘱单状态
    /// </summary>
    public static class DoctorOrderFormStatus
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 暂存 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "暂存", Code = "Applyed", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 下医嘱单 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "下医嘱单", Code = "Reviewing", FontColor = "#ffffff", BGColor = "#71ba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 患者已接收 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "患者已接收", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 患者取消接收 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "患者取消接收", Code = "UnReview", FontColor = "#ffffff", BGColor = "#2aa515" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 患者已下单 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "患者已下单", Code = "TwoReviewing", FontColor = "#ffffff", BGColor = "#12abe3" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 患者取消下单 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "患者取消下单", Code = "TwoReviewed", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 患者已缴费 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "患者已缴费", Code = "UnTwoReview", FontColor = "#ffffff", BGColor = "#1195db" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 患者退费申请 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "患者退费申请", Code = "UnFinanceReview", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 患者已退费 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "患者已退费", Code = "FinancePay", FontColor = "#ffffff", BGColor = "#e97f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(DoctorOrderFormStatus.暂存.Key, DoctorOrderFormStatus.暂存.Value);
            dic.Add(DoctorOrderFormStatus.下医嘱单.Key, DoctorOrderFormStatus.下医嘱单.Value);
            dic.Add(DoctorOrderFormStatus.患者已接收.Key, DoctorOrderFormStatus.患者已接收.Value);
            dic.Add(DoctorOrderFormStatus.患者取消接收.Key, DoctorOrderFormStatus.患者取消接收.Value);
            dic.Add(DoctorOrderFormStatus.患者已下单.Key, DoctorOrderFormStatus.患者已下单.Value);
            dic.Add(DoctorOrderFormStatus.患者取消下单.Key, DoctorOrderFormStatus.患者取消下单.Value);
            dic.Add(DoctorOrderFormStatus.患者已缴费.Key, DoctorOrderFormStatus.患者已缴费.Value);
            dic.Add(DoctorOrderFormStatus.患者退费申请.Key, DoctorOrderFormStatus.患者退费申请.Value);
            dic.Add(DoctorOrderFormStatus.患者已退费.Key, DoctorOrderFormStatus.患者已退费.Value);
            return dic;
        }
    }

    /// <summary>
    /// 医生奖金结算单的状态
    /// </summary>
    public static class OSDoctorBonusFormStatus
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 暂存 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "暂存", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 申请 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "申请", Code = "Applyed", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 一审中 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "一审中", Code = "Reviewing", FontColor = "#ffffff", BGColor = "#71ba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 一审通过 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "一审通过", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 一审退回 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "一审退回", Code = "UnReview", FontColor = "#ffffff", BGColor = "#2aa515" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 二审中 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "二审中", Code = "TwoReviewing", FontColor = "#ffffff", BGColor = "#12abe3" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 二审通过 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "二审通过", Code = "TwoReviewed", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 二审退回 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "二审退回", Code = "UnTwoReview", FontColor = "#ffffff", BGColor = "#1195db" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 检查并打款 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "检查并打款", Code = "FinancePay", FontColor = "#ffffff", BGColor = "#e97f36" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 检查并打款退回 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "检查并打款退回", Code = "UnFinanceReview", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 打款完成 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "打款完成", Code = "Payed", FontColor = "#ffffff", BGColor = "#568f36" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 打款异常 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", Name = "打款异常", Code = "UnPay", FontColor = "#ffffff", BGColor = "#538a36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(OSDoctorBonusFormStatus.暂存.Key, OSDoctorBonusFormStatus.暂存.Value);
            dic.Add(OSDoctorBonusFormStatus.申请.Key, OSDoctorBonusFormStatus.申请.Value);
            dic.Add(OSDoctorBonusFormStatus.一审中.Key, OSDoctorBonusFormStatus.一审中.Value);
            dic.Add(OSDoctorBonusFormStatus.一审通过.Key, OSDoctorBonusFormStatus.一审通过.Value);
            dic.Add(OSDoctorBonusFormStatus.一审退回.Key, OSDoctorBonusFormStatus.一审退回.Value);
            dic.Add(OSDoctorBonusFormStatus.二审中.Key, OSDoctorBonusFormStatus.二审中.Value);
            dic.Add(OSDoctorBonusFormStatus.二审通过.Key, OSDoctorBonusFormStatus.二审通过.Value);
            dic.Add(OSDoctorBonusFormStatus.二审退回.Key, OSDoctorBonusFormStatus.二审退回.Value);
            dic.Add(OSDoctorBonusFormStatus.检查并打款退回.Key, OSDoctorBonusFormStatus.检查并打款退回.Value);
            dic.Add(OSDoctorBonusFormStatus.检查并打款.Key, OSDoctorBonusFormStatus.检查并打款.Value);
            dic.Add(OSDoctorBonusFormStatus.打款完成.Key, OSDoctorBonusFormStatus.打款完成.Value);
            dic.Add(OSDoctorBonusFormStatus.打款异常.Key, OSDoctorBonusFormStatus.打款异常.Value);
            return dic;
        }
    }

    /// <summary>
    /// 医生奖金结算记录的发放方式
    /// </summary>
    public static class OSDoctorBonusPaymentMethod
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 微信支付 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "微信支付", Code = "WeChat", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 银行转账 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "银行转账", Code = "Bank", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(OSDoctorBonusPaymentMethod.微信支付.Key, OSDoctorBonusPaymentMethod.微信支付.Value);
            dic.Add(OSDoctorBonusPaymentMethod.银行转账.Key, OSDoctorBonusPaymentMethod.银行转账.Value);
            return dic;
        }
    }

    /// <summary>
    /// 用户消费单的状态
    /// </summary>
    public static class OSUserConsumerFormStatus
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 消费成功 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "消费成功", Code = "Success", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 消费失败 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "消费失败", Code = "Failure", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 已结算 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "已结算", Code = "Settled", FontColor = "#ffffff", BGColor = "#71ba59" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(OSUserConsumerFormStatus.消费成功.Key, OSUserConsumerFormStatus.消费成功.Value);
            dic.Add(OSUserConsumerFormStatus.消费失败.Key, OSUserConsumerFormStatus.消费失败.Value);
            dic.Add(OSUserConsumerFormStatus.已结算.Key, OSUserConsumerFormStatus.已结算.Value);
            return dic;
        }
    }

    /// <summary>
    /// 特推套餐状态
    /// </summary>
    public static class OSRecommendationItemProducStatus
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 待审核 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "待审核", Code = "UnReview", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 已审核 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "已审核", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#71ba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 上架 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "上架", Code = "Published", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 下架 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "下架", Code = "UnPublish", FontColor = "#ffffff", BGColor = "#2aa515" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(OSRecommendationItemProducStatus.待审核.Key, OSRecommendationItemProducStatus.待审核.Value);
            dic.Add(OSRecommendationItemProducStatus.已审核.Key, OSRecommendationItemProducStatus.已审核.Value);
            dic.Add(OSRecommendationItemProducStatus.上架.Key, OSRecommendationItemProducStatus.上架.Value);
            dic.Add(OSRecommendationItemProducStatus.下架.Key, OSRecommendationItemProducStatus.下架.Value);
            return dic;
        }
    }

    /// <summary>
    /// 医生类型
    /// </summary>
    public static class DoctorType
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 普通医生 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "普通医生", Code = "Doctor", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 检验技师 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "检验技师", Code = "LabTecher", FontColor = "#ffffff", BGColor = "#71ba59" });
        
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(DoctorType.普通医生.Key, DoctorType.普通医生.Value);
            dic.Add(DoctorType.检验技师.Key, DoctorType.检验技师.Value);
            return dic;
        }
    }

    /// <summary>
    /// 医嘱单类型
    /// </summary>
    public static class OSDoctorOrderFormType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 普通 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "普通", Code = "Normal", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 内部 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "内部", Code = "Internal", BGColor = "#71ba59" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(OSDoctorOrderFormType.普通.Key, OSDoctorOrderFormType.普通.Value);
            dic.Add(OSDoctorOrderFormType.内部.Key, OSDoctorOrderFormType.内部.Value);
            return dic;
        }
    }

    /// <summary>
    /// 订单类型
    /// </summary>
    public static class OSUserOrderFormType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 普通 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "普通", Code = "Normal", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 内部 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "内部", Code = "Internal", BGColor = "#71ba59" });
        public static KeyValuePair<string, BaseClassDicEntity> VIP = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "VIP", Code = "VIP", BGColor = "#71aa59" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(OSDoctorOrderFormType.普通.Key, OSDoctorOrderFormType.普通.Value);
            dic.Add(OSDoctorOrderFormType.内部.Key, OSDoctorOrderFormType.内部.Value);
            return dic;
        }
    }
    /// <summary>
    /// 各类单号的生成规则内容类型
    /// </summary>
    public static class NumberRuleType
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 退费单编号 = new KeyValuePair<string, BaseClassDicEntity>("MRefundFormCode", new BaseClassDicEntity() { Id = "MRefundFormCode", Name = "退费单编号", Code = "MRefundFormCode", FontColor = "", BGColor = "" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 用户订单编号 = new KeyValuePair<string, BaseClassDicEntity>("UOFCode", new BaseClassDicEntity() { Id = "UOFCode", Name = "用户订单编号", Code = "UOFCode", FontColor = "", BGColor = "" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 消费单编号 = new KeyValuePair<string, BaseClassDicEntity>("OSUserConsumerFormCode", new BaseClassDicEntity() { Id = "OSUserConsumerFormCode", Name = "消费单编号", Code = "OSUserConsumerFormCode", FontColor = "", BGColor = "" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 医生奖金结算单号 = new KeyValuePair<string, BaseClassDicEntity>("BonusFormCode", new BaseClassDicEntity() { Id = "BonusFormCode", Name = "医生奖金结算单号", Code = "BonusFormCode", FontColor = "", BGColor = "" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(NumberRuleType.退费单编号.Key, NumberRuleType.退费单编号.Value);
            dic.Add(NumberRuleType.用户订单编号.Key, NumberRuleType.用户订单编号.Value);
            dic.Add(NumberRuleType.消费单编号.Key, NumberRuleType.消费单编号.Value);
            dic.Add(NumberRuleType.医生奖金结算单号.Key, NumberRuleType.医生奖金结算单号.Value);
            return dic;
        }
    }
    public static class SysDicCookieSession
    {
        public static string UserOpenID = "800001";//用户OpenID
        public static string UserName = "800002";//用户名称
        public static string UserWeiXinAccountID = "800003";//用户微信账户ID
        public static string DoctorOpenID = "800004";//医生OpenID
        public static string DoctorName = "800005";//医生姓名
        public static string DoctorWeiXinAccountID = "800006";//医生微信账户ID
        public static string DoctorId = "800007";//医生ID
        public static string DoctorHospital = "800008";//医生所在医院ID        
        public static string AreaID = "800009";//区域ID
        public static string DoctorHospitalCode = "800010";//医生所在医院Code
        public static string DoctorHospitalName = "800011";//医生所在医院Name
        public static string DoctorBonusPercent = "800012";//医生咨询费比率
        public static string ReadAgreement = "800013";//用户是否阅读并统一协议
        public static string VerificationCode = "800014";//验证码
        public static string VerificationCodeDateTime = "800015";//验证码有效期
        public static string DoctorAccountType = "800016";//医生账户类型
    }
    
}
