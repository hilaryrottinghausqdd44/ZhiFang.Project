using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{
    #region OSUserOrderFormVO

    /// <summary>
    /// OSUserOrderForm object for NHibernate mapped table 'OS_UserOrderForm'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "用户订单VO", ClassCName = "VO_OSUserOrderForm", ShortCode = "VO_OSUserOrderForm", Desc = "用户订单VO")]
    public class OSUserOrderFormVO : BaseEntity
    {
        #region Public Properties

        [DataMember]
        [DataDesc(CName = "区域ID", ShortCode = "AreaID", Desc = "区域ID", ContextType = SysDic.All, Length = 8)]
        public virtual long AD { get; set; }

        [DataMember]
        [DataDesc(CName = "医院ID", ShortCode = "HospitalID", Desc = "医院ID", ContextType = SysDic.All, Length = 8)]
        public virtual long HD { get; set; }

        [DataMember]
        [DataDesc(CName = "用户订单编号", ShortCode = "UOFCode", Desc = "用户订单编号", ContextType = SysDic.All, Length = 30)]
        public virtual string UFC { get; set; }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DOFID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? DFD { get; set; }

        [DataMember]
        [DataDesc(CName = "医生账户信息ID", ShortCode = "DoctorAccountID", Desc = "医生账户信息ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DAD { get; set; }

        [DataMember]
        [DataDesc(CName = "消费单编号ID", ShortCode = "OSUserConsumerFormID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OCD { get; set; }

        [DataMember]
        [DataDesc(CName = "消费单编号", ShortCode = "OSUserConsumerFormCode", Desc = "消费单编号", ContextType = SysDic.All, Length = 30)]
        public virtual string OCC { get; set; }

        [DataMember]
        [DataDesc(CName = "医生微信ID", ShortCode = "WeiXinUserID", Desc = "医生微信ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? WXD { get; set; }

        [DataMember]
        [DataDesc(CName = "医生OpenID", ShortCode = "DoctorOpenID", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DOD { get; set; }

        [DataMember]
        [DataDesc(CName = "医生姓名", ShortCode = "DoctorName", Desc = "医生姓名", ContextType = SysDic.All, Length = 20)]
        public virtual string DN { get; set; }

        [DataMember]
        [DataDesc(CName = "用户账户信息ID", ShortCode = "UserAccountID", Desc = "用户账户信息ID", ContextType = SysDic.All, Length = 8)]
        public virtual long UID { get; set; }

        [DataMember]
        [DataDesc(CName = "用户微信ID", ShortCode = "UserWeiXinUserID", Desc = "用户微信ID", ContextType = SysDic.All, Length = 8)]
        public virtual long UWD { get; set; }

        [DataMember]
        [DataDesc(CName = "用户姓名", ShortCode = "UserName", Desc = "用户姓名", ContextType = SysDic.All, Length = 20)]
        public virtual string UN { get; set; }

        [DataMember]
        [DataDesc(CName = "用户OpenID", ShortCode = "UserOpenID", Desc = "用户OpenID", ContextType = SysDic.All, Length = 50)]
        public virtual string UOD { get; set; }

        [DataMember]
        [DataDesc(CName = "订单状态", ShortCode = "Status", Desc = "订单状态", ContextType = SysDic.All, Length = 8)]
        public virtual long SS { get; set; }

        [DataMember]
        [DataDesc(CName = "消费码", ShortCode = "PayCode", Desc = "消费码", ContextType = SysDic.All, Length = 50)]
        public virtual string PC { get; set; }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string MM { get; set; }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DO { get; set; }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IU { get; set; }

        [DataMember]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DUT { get; set; }

        [DataMember]
        [DataDesc(CName = "市场价格", ShortCode = "MarketPrice", Desc = "市场价格", ContextType = SysDic.All, Length = 8)]
        public virtual double MP { get; set; }

        [DataMember]
        [DataDesc(CName = "大家价格", ShortCode = "GreatMasterPrice", Desc = "大家价格", ContextType = SysDic.All, Length = 8)]
        public virtual double GMP { get; set; }

        [DataMember]
        [DataDesc(CName = "折扣价格", ShortCode = "DiscountPrice", Desc = "折扣价格", ContextType = SysDic.All, Length = 8)]
        public virtual double DP { get; set; }

        [DataMember]
        [DataDesc(CName = "折扣率", ShortCode = "Discount", Desc = "折扣率", ContextType = SysDic.All, Length = 8)]
        public virtual double DT { get; set; }

        [DataMember]
        [DataDesc(CName = "实际金额", ShortCode = "Price", Desc = "实际金额", ContextType = SysDic.All, Length = 8)]
        public virtual double PE { get; set; }

        [DataMember]
        [DataDesc(CName = "咨询费", ShortCode = "AdvicePrice", Desc = "咨询费", ContextType = SysDic.All, Length = 8)]
        public virtual double AP { get; set; }

        [DataMember]
        [DataDesc(CName = "退费金额", ShortCode = "RefundPrice", Desc = "退费金额", ContextType = SysDic.All, Length = 8)]
        public virtual double RP { get; set; }

        [DataMember]
        [DataDesc(CName = "缴费时间", ShortCode = "PayTime", Desc = "缴费时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PT { get; set; }

        [DataMember]
        [DataDesc(CName = "取消申请时间", ShortCode = "CancelApplyTime", Desc = "取消申请时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CAT { get; set; }

        [DataMember]
        [DataDesc(CName = "取消完成时间", ShortCode = "CancelFinishedTime", Desc = "取消完成时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CDT { get; set; }

        [DataMember]
        [DataDesc(CName = "消费开始时间", ShortCode = "ConsumerStartTime", Desc = "消费开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CST { get; set; }

        [DataMember]
        [DataDesc(CName = "消费完成时间", ShortCode = "ConsumerFinishedTime", Desc = "消费完成时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CFT { get; set; }

        [DataMember]
        [DataDesc(CName = "退费申请时间", ShortCode = "RefundApplyTime", Desc = "退费申请时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RAT { get; set; }

        [DataMember]
        [DataDesc(CName = "退款处理人", ShortCode = "RefundOneReviewManName", Desc = "退款处理人", ContextType = SysDic.All, Length = 50)]
        public virtual string RRM { get; set; }

        [DataMember]
        [DataDesc(CName = "退款处理人ID", ShortCode = "RefundOneReviewManID", Desc = "退款处理人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long RRD { get; set; }

        [DataMember]
        [DataDesc(CName = "退款处理开始时间", ShortCode = "RefundOneReviewStartTime", Desc = "退款处理开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RRT { get; set; }

        [DataMember]
        [DataDesc(CName = "退款处理完成时间", ShortCode = "RefundOneReviewFinishTime", Desc = "退款处理完成时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RFT { get; set; }

        [DataMember]
        [DataDesc(CName = "退款审批人", ShortCode = "RefundTwoReviewManName", Desc = "退款审批人", ContextType = SysDic.All, Length = 50)]
        public virtual string RTM { get; set; }

        [DataMember]
        [DataDesc(CName = "退款审批人ID", ShortCode = "RefundTwoReviewManID", Desc = "退款审批人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long RTD { get; set; }

        [DataMember]
        [DataDesc(CName = "退款审批开始时间", ShortCode = "RefundTwoReviewStartTime", Desc = "退款审批开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RTS { get; set; }

        [DataMember]
        [DataDesc(CName = "退款审批时间", ShortCode = "RefundTwoReviewFinishTime", Desc = "退款审批时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RRF { get; set; }

        [DataMember]
        [DataDesc(CName = "退款发放人", ShortCode = "RefundThreeReviewManName", Desc = "退款发放人", ContextType = SysDic.All, Length = 50)]
        public virtual string TRM { get; set; }

        [DataMember]
        [DataDesc(CName = "退款发放人ID", ShortCode = "RefundThreeReviewManID", Desc = "退款发放人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long TRD { get; set; }

        [DataMember]
        [DataDesc(CName = "退款发放开始时间", ShortCode = "RefundThreeReviewStartTime", Desc = "退款发放开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TRS { get; set; }

        [DataMember]
        [DataDesc(CName = "退款发放完成时间", ShortCode = "RefundThreeReviewFinishTime", Desc = "退款发放完成时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TRF { get; set; }

        [DataMember]
        [DataDesc(CName = "退费原因", ShortCode = "RefundReason", Desc = "退费原因", ContextType = SysDic.All, Length = 900)]
        public virtual string RR { get; set; }

        [DataMember]
        [DataDesc(CName = "退款处理说明", ShortCode = "RefundOneReviewReason", Desc = "退款处理说明", ContextType = SysDic.All, Length = 900)]
        public virtual string RRR { get; set; }

        [DataMember]
        [DataDesc(CName = "退款审批说明", ShortCode = "RefundTwoReviewReason", Desc = "退款审批说明", ContextType = SysDic.All, Length = 900)]
        public virtual string RTR { get; set; }

        [DataMember]
        [DataDesc(CName = "退款发放说明", ShortCode = "RefundThreeReviewReason", Desc = "退款发放说明", ContextType = SysDic.All, Length = 900)]
        public virtual string RERR { get; set; }

        [DataMember]
        [DataDesc(CName = "是否已预下单", ShortCode = "IsPrePay", Desc = "是否已预下单", ContextType = SysDic.All, Length = 1)]
        public virtual bool IPP { get; set; }

        [DataMember]
        [DataDesc(CName = "WeiXin统一下单编码", ShortCode = "PrePayId", Desc = "WeiXin统一下单编码", ContextType = SysDic.All, Length = 50)]
        public virtual string PPD { get; set; }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrePayTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PPT { get; set; }

        [DataMember]
        [DataDesc(CName = "预下单通信结果", ShortCode = "PrePayReturnCode", Desc = "预下单通信结果", ContextType = SysDic.All, Length = 50)]
        public virtual string PRC { get; set; }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrePayReturnMsg", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PPM { get; set; }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrePayErrCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PEC { get; set; }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrePayErrName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PPE { get; set; }

        [DataMember]
        [DataDesc(CName = "微信订单号", ShortCode = "TransactionId", Desc = "微信订单号", ContextType = SysDic.All, Length = 50)]
        public virtual string TD { get; set; }
        [DataMember]
        [DataDesc(CName = "采样标记", ShortCode = "CF", Desc = "采样标记", ContextType = SysDic.All, Length = 500)]
        public bool CF { get; set; }
        [DataMember]
        [DataDesc(CName = "采样金额", ShortCode = "CP", Desc = "采样金额", ContextType = SysDic.All, Length = 500)]
        public double CP { get; set; }
        [DataMember]
        [DataDesc(CName = "医嘱单类型", ShortCode = "TI", Desc = "医嘱单类型", ContextType = SysDic.All, Length = 500)]
        public long? TI { get; set; }
        [DataMember]
        [DataDesc(CName = "医嘱单类型名称", ShortCode = "TN", Desc = "医嘱单类型名称", ContextType = SysDic.All, Length = 500)]
        public string TN { get; set; }


        #endregion
    }
    #endregion
}
