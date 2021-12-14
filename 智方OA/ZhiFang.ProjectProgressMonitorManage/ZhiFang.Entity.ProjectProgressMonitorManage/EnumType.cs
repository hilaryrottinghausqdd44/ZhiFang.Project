using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region 字典类
    /// <summary>
    /// 任务状态实体
    /// </summary>
    public class BaseClassDicEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string FontColor { get; set; }
        public string BGColor { get; set; }
    }

    /// <summary>
    /// 任务状态
    /// </summary>
    public static class PTaskStatus
    {
        public static PTaskStatusEntity 暂存 = new PTaskStatusEntity() { Id = "5588205522535239744", Name = "暂存", Code = "TmpApply", FontColor = "#ffffff", BGColor = "#bfbfbf" };
        public static PTaskStatusEntity 申请 = new PTaskStatusEntity() { Id = "4649883000533627142", Name = "申请", Code = "Applyed", FontColor = "#ffffff", BGColor = "#f4c600" };

        public static PTaskStatusEntity 一审中 = new PTaskStatusEntity() { Id = "5097561283722347016", Name = "一审中", Code = "OneAuditing", FontColor = "#ffffff", BGColor = "#aad08f" };
        public static PTaskStatusEntity 一审通过 = new PTaskStatusEntity() { Id = "5682026874588216259", Name = "一审通过", Code = "OneAudited", FontColor = "#ffffff", BGColor = "#7cba59" };
        public static PTaskStatusEntity 一审退回 = new PTaskStatusEntity() { Id = "5150044114524428979", Name = "一审退回", Code = "OneAuditBack", FontColor = "#ffffff", BGColor = "#2aa515" };

        public static PTaskStatusEntity 二审中 = new PTaskStatusEntity() { Id = "5221836557183432811", Name = "二审中", Code = "TwoAuditing", FontColor = "#ffffff", BGColor = "#7dc5eb" };
        public static PTaskStatusEntity 二审通过 = new PTaskStatusEntity() { Id = "5434763342795916000", Name = "二审通过", Code = "TwoAudited", FontColor = "#ffffff", BGColor = "#17abe3" };
        public static PTaskStatusEntity 二审退回 = new PTaskStatusEntity() { Id = "5324460690574315774", Name = "二审退回", Code = "TwoAuditBack", FontColor = "#ffffff", BGColor = "#1195db" };

        public static PTaskStatusEntity 分配中 = new PTaskStatusEntity() { Id = "5181783138154880332", Name = "分配中", Code = "Publishing", FontColor = "#ffffff", BGColor = "#be8dbd" };
        public static PTaskStatusEntity 分配完成 = new PTaskStatusEntity() { Id = "4967258396876635439", Name = "分配完成", Code = "Published", FontColor = "#ffffff", BGColor = "#a4579d" };
        public static PTaskStatusEntity 分配退回 = new PTaskStatusEntity() { Id = "5741016721777107562", Name = "分配退回", Code = "PublishBack", FontColor = "#ffffff", BGColor = "#88147f" };

        public static PTaskStatusEntity 执行中 = new PTaskStatusEntity() { Id = "4621621720762238176", Name = "执行中", Code = "Executing", FontColor = "#ffffff", BGColor = "#e8989a" };
        public static PTaskStatusEntity 执行完成 = new PTaskStatusEntity() { Id = "5391772382920326538", Name = "执行完成", Code = "Executed", FontColor = "#ffffff", BGColor = "#dd6572" };
        public static PTaskStatusEntity 不执行 = new PTaskStatusEntity() { Id = "5001555516032423353", Name = "不执行", Code = "ExecutStop", FontColor = "#ffffff", BGColor = "#d6204b" };

        public static PTaskStatusEntity 验收中 = new PTaskStatusEntity() { Id = "4890928177429564879", Name = "验收中", Code = "Checking", FontColor = "#ffffff", BGColor = "#eeb173" };
        public static PTaskStatusEntity 已验收 = new PTaskStatusEntity() { Id = "5518558271903118484", Name = "已验收", Code = "Checked", FontColor = "#ffffff", BGColor = "#e98f36" };
        public static PTaskStatusEntity 验收退回 = new PTaskStatusEntity() { Id = "5490921315541028028", Name = "验收退回", Code = "CheckBack", FontColor = "#ffffff", BGColor = "#e0620d" };

        public static PTaskStatusEntity 已终止 = new PTaskStatusEntity() { Id = "5484216973649314900", Name = "已终止", Code = "Stoped", FontColor = "#ffffff", BGColor = "#2c2c2c" };
    }

    /// <summary>
    /// 任务状态实体
    /// </summary>
    public class PTaskStatusEntity : BaseClassDicEntity
    {
    }

    /// <summary>
    /// 合同状态
    /// </summary>
    public static class PContractStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 暂存 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "暂存", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 申请 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "申请", Code = "Applyed", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 技术已评 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "技术已评", Code = "TechnicalReviewed", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 商务已评 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "商务已评", Code = "BusinessReviewed", FontColor = "#ffffff", BGColor = "#aad08f" });
        public static KeyValuePair<string, BaseClassDicEntity> 评审未通过 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "评审未通过", Code = "UnReview", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static KeyValuePair<string, BaseClassDicEntity> 评审通过 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "评审通过", Code = "UnReview", FontColor = "#ffffff", BGColor = "#7cca59" });
        public static KeyValuePair<string, BaseClassDicEntity> 正式签署 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "正式签署", Code = "FormalSignature", FontColor = "#ffffff", BGColor = "#2aa515" });
        public static KeyValuePair<string, BaseClassDicEntity> 已验收 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "已验收", Code = "Checked", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 变更 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "变更", Code = "Change", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 已邮寄 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "已邮寄", Code = "Change", FontColor = "#ffffff", BGColor = "#dd6572" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(PContractStatus.暂存.Key, PContractStatus.暂存.Value);
            dic.Add(PContractStatus.申请.Key, PContractStatus.申请.Value);
            dic.Add(PContractStatus.技术已评.Key, PContractStatus.技术已评.Value);
            dic.Add(PContractStatus.商务已评.Key, PContractStatus.商务已评.Value);
            dic.Add(PContractStatus.评审未通过.Key, PContractStatus.评审未通过.Value);
            dic.Add(PContractStatus.正式签署.Key, PContractStatus.正式签署.Value);
            dic.Add(PContractStatus.已验收.Key, PContractStatus.已验收.Value);
            dic.Add(PContractStatus.已邮寄.Key, PContractStatus.已邮寄.Value);
            return dic;
        }

        //public static BaseClassDicEntity 暂存 = new BaseClassDicEntity() { Id = "1", Name = "暂存", Code = "TmpApply", FontColor = "#ffffff", BGColor = "#bfbfbf" };
        //public static BaseClassDicEntity 申请 = new BaseClassDicEntity() { Id = "2", Name = "申请", Code = "Applyed", FontColor = "#ffffff", BGColor = "#bfbfbf" };

        //public static BaseClassDicEntity 技术已评商务未评 = new BaseClassDicEntity() { Id = "3", Name = "技术已评商务未评", Code = "TechnicalReviewed", FontColor = "#ffffff", BGColor = "#f4c600" };

        //public static BaseClassDicEntity 商务已评技术未评 = new BaseClassDicEntity() { Id = "4", Name = "商务已评技术未评", Code = "BusinessReviewed", FontColor = "#ffffff", BGColor = "#aad08f" };

        //public static BaseClassDicEntity 评审未通过 = new BaseClassDicEntity() { Id = "5", Name = "评审未通过", Code = "UnReview", FontColor = "#ffffff", BGColor = "#7cba59" };

        //public static BaseClassDicEntity 正式签署 = new BaseClassDicEntity() { Id = "6", Name = "正式签署", Code = "FormalSignature", FontColor = "#ffffff", BGColor = "#2aa515" };

        //public static BaseClassDicEntity 已验收 = new BaseClassDicEntity() { Id = "7", Name = "已验收", Code = "Checked", FontColor = "#ffffff", BGColor = "#bfbfbf" };
    }

    /// <summary>
    /// 收款计划状态
    /// </summary>
    public static class PReceivePlanStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 暂存 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "暂存", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        //public static  KeyValuePair<string, BaseClassDicEntity> 申请 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "申请", Code = "Applyed", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 执行中 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "执行中", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#f4c600" });
        //public static  KeyValuePair<string, BaseClassDicEntity> 审核退回 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "审核退回", Code = "UnReview", FontColor = "#ffffff", BGColor = "#aad08f" });
        public static KeyValuePair<string, BaseClassDicEntity> 变更申请 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "变更申请", Code = "ChangeApply", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static KeyValuePair<string, BaseClassDicEntity> 已变更 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "已变更", Code = "ReviewChange", FontColor = "#ffffff", BGColor = "#7cca59" });
        public static KeyValuePair<string, BaseClassDicEntity> 变更退回 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "变更退回", Code = "UnReviewChange", FontColor = "#ffffff", BGColor = "#2aa515" });
        public static KeyValuePair<string, BaseClassDicEntity> 收款完成 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "收款完成", Code = "Finish", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(PReceivePlanStatus.暂存.Key, PReceivePlanStatus.暂存.Value);
            //dic.Add(PReceivePlanStatus.申请.Key, PReceivePlanStatus.申请.Value);
            dic.Add(PReceivePlanStatus.执行中.Key, PReceivePlanStatus.执行中.Value);
            //dic.Add(PReceivePlanStatus.审核退回.Key, PReceivePlanStatus.审核退回.Value);
            dic.Add(PReceivePlanStatus.变更申请.Key, PReceivePlanStatus.变更申请.Value);
            dic.Add(PReceivePlanStatus.已变更.Key, PReceivePlanStatus.已变更.Value);
            dic.Add(PReceivePlanStatus.变更退回.Key, PReceivePlanStatus.变更退回.Value);
            dic.Add(PReceivePlanStatus.收款完成.Key, PReceivePlanStatus.收款完成.Value);
            return dic;
        }
    }

    /// <summary>
    /// 发票状态
    /// </summary>
    public static class PInvoiceStatus
    {

        public static KeyValuePair<string, BaseClassDicEntity> 暂存 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "暂存", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 申请 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "申请", Code = "Applyed", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 一审通过 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "一审通过", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static KeyValuePair<string, BaseClassDicEntity> 一审退回 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "一审退回", Code = "UnReview", FontColor = "#ffffff", BGColor = "#2ee515" });
        public static KeyValuePair<string, BaseClassDicEntity> 二审通过 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "二审通过", Code = "TwoReviewed", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 二审退回 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "二审退回", Code = "UnTwoReview", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 已开票 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "已开票", Code = "Invoiceed", FontColor = "#ffffff", BGColor = "#a4579d" });
        public static KeyValuePair<string, BaseClassDicEntity> 已邮寄 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "已邮寄", Code = "Posted", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 已签收 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "已签收", Code = "Receiveed", FontColor = "#ffffff", BGColor = "#e98f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(PInvoiceStatus.暂存.Key, PInvoiceStatus.暂存.Value);
            dic.Add(PInvoiceStatus.申请.Key, PInvoiceStatus.申请.Value);
            dic.Add(PInvoiceStatus.一审通过.Key, PInvoiceStatus.一审通过.Value);
            dic.Add(PInvoiceStatus.一审退回.Key, PInvoiceStatus.一审退回.Value);
            dic.Add(PInvoiceStatus.二审通过.Key, PInvoiceStatus.二审通过.Value);
            dic.Add(PInvoiceStatus.二审退回.Key, PInvoiceStatus.二审退回.Value);
            dic.Add(PInvoiceStatus.已开票.Key, PInvoiceStatus.已开票.Value);
            dic.Add(PInvoiceStatus.已邮寄.Key, PInvoiceStatus.已邮寄.Value);
            dic.Add(PInvoiceStatus.已签收.Key, PInvoiceStatus.已签收.Value);
            return dic;
        }
    }

    /// <summary>
    /// 借款状态
    /// </summary>
    public static class PLoanBillStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 暂存 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "暂存", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 申请 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "申请", Code = "Applyed", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 一审通过 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "一审通过", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static KeyValuePair<string, BaseClassDicEntity> 一审退回 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "一审退回", Code = "UnReview", FontColor = "#ffffff", BGColor = "#2aa515" });
        public static KeyValuePair<string, BaseClassDicEntity> 二审通过 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "二审通过", Code = "TwoReviewed", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 二审退回 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "二审退回", Code = "UnTwoReview", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 三审通过 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "三审通过", Code = "ThreeReviewed", FontColor = "#ffffff", BGColor = "#a4579d" });
        public static KeyValuePair<string, BaseClassDicEntity> 三审退回 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "三审退回", Code = "UnThreeReview", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 四审通过 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "四审通过", Code = "FourReviewed", FontColor = "#ffffff", BGColor = "#e97f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 四审退回 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "四审退回", Code = "UnFourReview", FontColor = "#ffffff", BGColor = "#e99f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 打款 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "打款", Code = "pay", FontColor = "#ffffff", BGColor = "#568f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 领款确认 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", Name = "领款确认", Code = "Receiveed", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(PLoanBillStatus.暂存.Key, PLoanBillStatus.暂存.Value);
            dic.Add(PLoanBillStatus.申请.Key, PLoanBillStatus.申请.Value);
            dic.Add(PLoanBillStatus.一审通过.Key, PLoanBillStatus.一审通过.Value);
            dic.Add(PLoanBillStatus.一审退回.Key, PLoanBillStatus.一审退回.Value);
            dic.Add(PLoanBillStatus.二审通过.Key, PLoanBillStatus.二审通过.Value);
            dic.Add(PLoanBillStatus.二审退回.Key, PLoanBillStatus.二审退回.Value);
            dic.Add(PLoanBillStatus.三审通过.Key, PLoanBillStatus.三审通过.Value);
            dic.Add(PLoanBillStatus.三审退回.Key, PLoanBillStatus.三审退回.Value);
            dic.Add(PLoanBillStatus.四审通过.Key, PLoanBillStatus.四审通过.Value);
            dic.Add(PLoanBillStatus.四审退回.Key, PLoanBillStatus.四审退回.Value);
            dic.Add(PLoanBillStatus.打款.Key, PLoanBillStatus.打款.Value);
            dic.Add(PLoanBillStatus.领款确认.Key, PLoanBillStatus.领款确认.Value);
            return dic;
        }
    }

    /// <summary>
    /// 报销状态
    /// </summary>
    public static class PExpenseAccountStatus
    {

        public static KeyValuePair<string, BaseClassDicEntity> 暂存 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "暂存", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 申请 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "申请", Code = "Applyed", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 一审通过 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "一审通过", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static KeyValuePair<string, BaseClassDicEntity> 一审退回 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "一审退回", Code = "UnReview", FontColor = "#ffffff", BGColor = "#2aa515" });
        public static KeyValuePair<string, BaseClassDicEntity> 二审通过 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "二审通过", Code = "TwoReviewed", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 二审退回 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "二审退回", Code = "UnTwoReview", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 三审通过 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "三审通过", Code = "ThreeReviewed", FontColor = "#ffffff", BGColor = "#a4579d" });
        public static KeyValuePair<string, BaseClassDicEntity> 三审退回 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "三审退回", Code = "UnThreeReview", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 四审通过 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "四审通过", Code = "FourReviewed", FontColor = "#ffffff", BGColor = "#e97f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 四审退回 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "四审退回", Code = "UnFourReview", FontColor = "#ffffff", BGColor = "#e99f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 打款 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "打款", Code = "pay", FontColor = "#ffffff", BGColor = "#e68f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 领款确认 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", Name = "领款确认", Code = "Receiveed", FontColor = "#ffffff", BGColor = "#e78f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(PExpenseAccountStatus.暂存.Key, PExpenseAccountStatus.暂存.Value);
            dic.Add(PExpenseAccountStatus.申请.Key, PExpenseAccountStatus.申请.Value);
            dic.Add(PExpenseAccountStatus.一审通过.Key, PExpenseAccountStatus.一审通过.Value);
            dic.Add(PExpenseAccountStatus.一审退回.Key, PExpenseAccountStatus.一审退回.Value);
            dic.Add(PExpenseAccountStatus.二审通过.Key, PExpenseAccountStatus.二审通过.Value);
            dic.Add(PExpenseAccountStatus.二审退回.Key, PExpenseAccountStatus.二审退回.Value);
            dic.Add(PExpenseAccountStatus.三审通过.Key, PExpenseAccountStatus.三审通过.Value);
            dic.Add(PExpenseAccountStatus.三审退回.Key, PExpenseAccountStatus.三审退回.Value);
            dic.Add(PExpenseAccountStatus.四审通过.Key, PExpenseAccountStatus.四审通过.Value);
            dic.Add(PExpenseAccountStatus.四审退回.Key, PExpenseAccountStatus.四审退回.Value);
            dic.Add(PExpenseAccountStatus.打款.Key, PExpenseAccountStatus.打款.Value);
            dic.Add(PExpenseAccountStatus.领款确认.Key, PExpenseAccountStatus.领款确认.Value);
            return dic;
        }
    }

    /// <summary>
    /// 服务状态
    /// </summary>
    public static class PCustomerServiceStatus
    {

        public static KeyValuePair<string, BaseClassDicEntity> 未处理 = new KeyValuePair<string, BaseClassDicEntity>("5306931351097424780", new BaseClassDicEntity() { Id = "5306931351097424780", Name = "未处理", Code = "Unhandle", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 处理中 = new KeyValuePair<string, BaseClassDicEntity>("4913864673941805116", new BaseClassDicEntity() { Id = "4913864673941805116", Name = " 处理中", Code = "Handling", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 已完成 = new KeyValuePair<string, BaseClassDicEntity>("4868388252853122720", new BaseClassDicEntity() { Id = "4868388252853122720", Name = "已完成", Code = "Complete", FontColor = "#ffffff", BGColor = "#7cba59" });

        public static KeyValuePair<string, BaseClassDicEntity> 有遗留 = new KeyValuePair<string, BaseClassDicEntity>("5118676910026877787", new BaseClassDicEntity() { Id = "5118676910026877787", Name = "有遗留", Code = "LeftBehind", FontColor = "#ffffff", BGColor = "#FF0000" });
        public static KeyValuePair<string, BaseClassDicEntity> 不好处理 = new KeyValuePair<string, BaseClassDicEntity>("5476597464546245138", new BaseClassDicEntity() { Id = "5476597464546245138", Name = "不好处理", Code = "HardToDeal", FontColor = "#ffffff", BGColor = "#FF0000" });
        public static KeyValuePair<string, BaseClassDicEntity> 可不处理 = new KeyValuePair<string, BaseClassDicEntity>("5313840044079446861", new BaseClassDicEntity() { Id = "5313840044079446861", Name = "可不处理", Code = "NoToDeal", FontColor = "#ffffff", BGColor = "#FF0000" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(PCustomerServiceStatus.未处理.Key, PCustomerServiceStatus.未处理.Value);
            dic.Add(PCustomerServiceStatus.处理中.Key, PCustomerServiceStatus.处理中.Value);
            dic.Add(PCustomerServiceStatus.已完成.Key, PCustomerServiceStatus.已完成.Value);
            dic.Add(PCustomerServiceStatus.有遗留.Key, PCustomerServiceStatus.有遗留.Value);
            dic.Add(PCustomerServiceStatus.不好处理.Key, PCustomerServiceStatus.不好处理.Value);
            dic.Add(PCustomerServiceStatus.可不处理.Key, PCustomerServiceStatus.可不处理.Value);
            return dic;
        }
    }

    /// <summary>
    /// 还款状态
    /// </summary>
    public static class PRepaymentStatus
    {

        public static KeyValuePair<string, BaseClassDicEntity> 暂存 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "暂存", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 申请 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "申请", Code = "Applyed", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 打回 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "打回", Code = "Back", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static KeyValuePair<string, BaseClassDicEntity> 还款确认 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "还款确认", Code = "Receive", FontColor = "#ffffff", BGColor = "#2aa515" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(PRepaymentStatus.暂存.Key, PRepaymentStatus.暂存.Value);
            dic.Add(PRepaymentStatus.申请.Key, PRepaymentStatus.申请.Value);
            dic.Add(PRepaymentStatus.打回.Key, PRepaymentStatus.打回.Value);
            dic.Add(PRepaymentStatus.还款确认.Key, PRepaymentStatus.还款确认.Value);
            return dic;
        }
    }

    /// <summary>
    /// 角色字典设置
    /// </summary>
    public static class RoleList
    {
        #region 商务
        public static KeyValuePair<string, BaseClassDicEntity> 商务助理 = new KeyValuePair<string, BaseClassDicEntity>("5599855028764712867", new BaseClassDicEntity() { Id = "5599855028764712867", Name = "商务助理", Code = "R1004", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 商务经理 = new KeyValuePair<string, BaseClassDicEntity>("5027619732489863317", new BaseClassDicEntity() { Id = "5027619732489863317", Name = "商务经理", Code = "R0018", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 出纳 = new KeyValuePair<string, BaseClassDicEntity>("4998265022165656715", new BaseClassDicEntity() { Id = "4998265022165656715", Name = "出纳", Code = "R0012", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 会计 = new KeyValuePair<string, BaseClassDicEntity>("4752109693339716076", new BaseClassDicEntity() { Id = "4752109693339716076", Name = "会计", Code = "R0013", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        #endregion

        public static KeyValuePair<string, BaseClassDicEntity> 总经理 = new KeyValuePair<string, BaseClassDicEntity>("5153823459927439789", new BaseClassDicEntity() { Id = "5153823459927439789", Name = "总经理", Code = "R0005", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 副总经理 = new KeyValuePair<string, BaseClassDicEntity>("5579125521166282740", new BaseClassDicEntity() { Id = "5579125521166282740", Name = "副总经理", Code = "R0006", FontColor = "#ffffff", BGColor = "#bfbfbf" });

        #region 服务
        public static KeyValuePair<string, BaseClassDicEntity> 服务监管 = new KeyValuePair<string, BaseClassDicEntity>("4910810038581646191", new BaseClassDicEntity() { Id = "4910810038581646191", Name = "服务监管", Code = "R0099", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 服务处理 = new KeyValuePair<string, BaseClassDicEntity>("4770491677694155044", new BaseClassDicEntity() { Id = "4770491677694155044", Name = "服务处理", Code = "R0091", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 服务受理 = new KeyValuePair<string, BaseClassDicEntity>("4644927123883559104", new BaseClassDicEntity() { Id = "4644927123883559104", Name = "服务受理", Code = "R0090", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        #endregion

        #region 合同
        public static KeyValuePair<string, BaseClassDicEntity> 合同商务评审 = new KeyValuePair<string, BaseClassDicEntity>("5193926283521345263", new BaseClassDicEntity() { Id = "5193926283521345263", Name = "合同商务评审", Code = "R0021", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 合同技术评审 = new KeyValuePair<string, BaseClassDicEntity>("5037391688897795989", new BaseClassDicEntity() { Id = "5037391688897795989", Name = "合同技术评审", Code = "R0022", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        #endregion

        #region 授权
        public static KeyValuePair<string, BaseClassDicEntity> 授权审核 = new KeyValuePair<string, BaseClassDicEntity>("5714505728673896978", new BaseClassDicEntity() { Id = "5714505728673896978", Name = "授权审核", Code = "R0031", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 授权审批 = new KeyValuePair<string, BaseClassDicEntity>("5720150253623434816", new BaseClassDicEntity() { Id = "5720150253623434816", Name = "授权审批", Code = "R0032", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        #endregion
    }
    /// <summary>
    /// 授权流程状态
    /// </summary>
    public static class LicenceStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 暂存 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "暂存", Code = "TmpApply", FontColor = "#ffffff", BGColor = "#bfbfbf" });

        public static KeyValuePair<string, BaseClassDicEntity> 申请 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "申请", Code = "Applyed", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 授权中 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "授权中", Code = "Licenceing", FontColor = "#ffffff", BGColor = "#aad08f" });
        public static KeyValuePair<string, BaseClassDicEntity> 商务授权通过 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "商务授权通过", Code = "LicencePass", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static KeyValuePair<string, BaseClassDicEntity> 商务授权退回 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "商务授权退回", Code = "LicenceBack", FontColor = "#ffffff", BGColor = "#f4c600" });

        public static KeyValuePair<string, BaseClassDicEntity> 特批授权中 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "特批授权中", Code = "SpecialApprovalLicenceing", FontColor = "#ffffff", BGColor = "#7dc5eb" });
        public static KeyValuePair<string, BaseClassDicEntity> 特批授权通过 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "特批授权通过", Code = "SpecialApprovalLicencePass", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 特批授权退回 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "特批授权退回", Code = "SpecialApprovalLicenceBack", FontColor = "#ffffff", BGColor = "#e98f36" });

        public static KeyValuePair<string, BaseClassDicEntity> 授权完成 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "授权完成", Code = "Complete", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(LicenceStatus.暂存.Key, LicenceStatus.暂存.Value);
            dic.Add(LicenceStatus.申请.Key, LicenceStatus.申请.Value);
            dic.Add(LicenceStatus.授权中.Key, LicenceStatus.授权中.Value);
            dic.Add(LicenceStatus.商务授权通过.Key, LicenceStatus.商务授权通过.Value);
            dic.Add(LicenceStatus.商务授权退回.Key, LicenceStatus.商务授权退回.Value);
            dic.Add(LicenceStatus.特批授权中.Key, LicenceStatus.特批授权中.Value);
            dic.Add(LicenceStatus.特批授权通过.Key, LicenceStatus.特批授权通过.Value);
            dic.Add(LicenceStatus.特批授权退回.Key, LicenceStatus.特批授权退回.Value);
            dic.Add(LicenceStatus.授权完成.Key, LicenceStatus.授权完成.Value);
            return dic;
        }
    }
    /// <summary>
    /// 授权类型
    /// </summary>
    public static class LicenceType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 商业 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "商业", Code = "Business", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 临时 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "临时", Code = "Temp", FontColor = "#ffffff", BGColor = "#e98f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 评估 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "评估", Code = "Assess", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 测试 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "测试", Code = "Test", FontColor = "#ffffff", BGColor = "#f4c600" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(LicenceType.商业.Key, LicenceType.商业.Value);
            dic.Add(LicenceType.评估.Key, LicenceType.评估.Value);
            dic.Add(LicenceType.测试.Key, LicenceType.测试.Value);
            dic.Add(LicenceType.临时.Key, LicenceType.临时.Value);
            return dic;
        }
    }
    /// <summary>
    /// 截止日期(有效期)状态
    /// </summary>
    public static class LicenceDateStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 有效 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "有效", Code = "Effective", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 十天内到期 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "十天内到期", Code = "TenDays", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 失效 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "失效", Code = "Invalid", FontColor = "#ffffff", BGColor = "red" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(LicenceDateStatus.有效.Key, LicenceDateStatus.有效.Value);
            dic.Add(LicenceDateStatus.十天内到期.Key, LicenceDateStatus.十天内到期.Value);
            dic.Add(LicenceDateStatus.失效.Key, LicenceDateStatus.失效.Value);
            return dic;
        }
    }
    /// <summary>
    /// 文档状态
    /// </summary>
    public static class FFileStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 暂存 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "暂存", Code = "TmpApply", FontColor = "#ffffff", BGColor = "#2c2c2c" });
        public static KeyValuePair<string, BaseClassDicEntity> 已提交 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "已提交", Code = "Submit", FontColor = "#ffffff", BGColor = "#f4c600" });

        public static KeyValuePair<string, BaseClassDicEntity> 已审核 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "已审核", Code = "Checked", FontColor = "#ffffff", BGColor = "#aad08f" });
        public static KeyValuePair<string, BaseClassDicEntity> 已批准 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "已批准", Code = "Approval", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static KeyValuePair<string, BaseClassDicEntity> 发布 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "发布", Code = "Release", FontColor = "#ffffff", BGColor = "#2aa515" });
        public static KeyValuePair<string, BaseClassDicEntity> 作废 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "作废", Code = "Invalid", FontColor = "#ffffff", BGColor = "#FF000F" });

        public static KeyValuePair<string, BaseClassDicEntity> 撤消提交 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "撤消提交", Code = "UndoSubmit", FontColor = "#ffffff", BGColor = "#e98f36" });

        public static KeyValuePair<string, BaseClassDicEntity> 审核退回 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "审核退回", Code = "AuditBack", FontColor = "#ffffff", BGColor = "#7dc5eb" });
        public static KeyValuePair<string, BaseClassDicEntity> 审批退回 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "审批退回", Code = "TwoAuditBack", FontColor = "#ffffff", BGColor = "#1195db" });
        // public static KeyValuePair<string, BaseClassDicEntity> 撤消发布 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "撤消发布", Code = "PublishBack", FontColor = "#ffffff", BGColor = "#88147f" });

        //public static KeyValuePair<string, BaseClassDicEntity> 禁用 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", Name = "禁用", Code = "Disabled", FontColor = "#ffffff", BGColor = "#e8989a" });
        //public static KeyValuePair<string, BaseClassDicEntity> 撤消禁用 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", Name = "撤消禁用", Code = "UndoDisabled", FontColor = "#ffffff", BGColor = "#0D0D0D" });
        public static KeyValuePair<string, BaseClassDicEntity> 打回起草人 = new KeyValuePair<string, BaseClassDicEntity>("15", new BaseClassDicEntity() { Id = "15", Name = "打回起草人", Code = "DraftedBack", FontColor = "#ffffff", BGColor = "#2c2c2c" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(FFileStatus.暂存.Key, FFileStatus.暂存.Value);
            dic.Add(FFileStatus.已提交.Key, FFileStatus.已提交.Value);
            dic.Add(FFileStatus.已审核.Key, FFileStatus.已审核.Value);
            dic.Add(FFileStatus.已批准.Key, FFileStatus.已批准.Value);
            dic.Add(FFileStatus.发布.Key, FFileStatus.发布.Value);
            dic.Add(FFileStatus.作废.Key, FFileStatus.作废.Value);
            dic.Add(FFileStatus.撤消提交.Key, FFileStatus.撤消提交.Value);
            dic.Add(FFileStatus.审核退回.Key, FFileStatus.审核退回.Value);
            dic.Add(FFileStatus.审批退回.Key, FFileStatus.审批退回.Value);
            //dic.Add(FFileStatus.撤消发布.Key, FFileStatus.撤消发布.Value);
            dic.Add(FFileStatus.打回起草人.Key, FFileStatus.打回起草人.Value);
            return dic;
        }
    }

    #endregion

    #region 字典枚举
    /// <summary>
    /// 日志可见级别
    /// </summary>
    public enum WorkLogExportLevel
    {
        仅自己和直接主管可见 = 0,
        所属部门可见 = 1,
        全公司可见 = 2
    }
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum WorkLogType
    {
        WorkLogDay,
        WorkLogWeek,
        WorkLogMonth
    }
    /// <summary>
    /// 日志发布类型
    /// </summary>
    public enum WorkLogSendType
    {
        ALL,
        COPYFORME,
        SENDFORME,
        MEOWN
    }
    /// <summary>
    /// 文档操作记录的操作类型
    /// </summary>
    public enum FFileOperationType
    {
        起草 = 1,
        修订 = 2,
        审核 = 3,
        批准 = 4,
        发布 = 5,
        浏览 = 6,
        作废 = 7,
        撤消提交 = 8,
        审核退回 = 9,
        批准退回 = 10,
        撤消发布 = 11,
        修订文档 = 12,
        禁用 = 13,
        撤消禁用 = 14,
        打回起草人 = 15,
        置顶 = 16,
        撤消置顶 = 17,
        更新文档类型 = 18,
        编辑更新 = 19//新闻管理/文档管理时的文档内容(不更新文档状态/阅读对象信息)的编辑更新操作
    }

    /// <summary>
    /// 文档应用类型
    /// </summary>
    public enum FFileType
    {
        文档应用 = 1,
        新闻应用 = 2,
        知识库应用 = 3,
        修订文档应用 = 4
    }

    /// <summary>
    /// 文件物理存储时，做一个处理：在文件名后+（.zf）,用来防止病毒文件挂在服务器直接执行
    /// </summary>
    public enum FileExt
    {
        zf
    }

    /// <summary>
    /// 文档抄送对象/文档阅读对象的对象类型
    /// </summary>
    public enum FFileObjectType
    {
        无 = -1,
        全部人员 = 1,
        科室 = 2,
        角色 = 3,
        人员 = 4
    }

    /// <summary>
    /// 模版关键字
    /// </summary>
    public enum TempletType
    {

        无 = 0,
        MD = 1,//月日，月多少天
        MW = 2,//月周，月多少周，指定周几
        MM = 3,//月，指定每月几号
        YD = 4,//年日，年多少天
        YW = 5,//年周，年多少周，指定周几
        YM = 6,//年月，年多少月，指定每月几号
        YP = 7,//年季，指定各季度的几号
        YH = 8,//半年，指定上下半年的几号
        YY = 9,//年，指定每年的几号
        SP = 10, //特殊类型
    }
    /// <summary>
    /// 公共操作记录表应用类型
    /// </summary>
    public enum SCOperationType
    {
        新增仪器信息 = 1,
        修改仪器信息 = 2,
        程序暂存 = 3,
        程序直接发布 = 4,
        程序修改暂存 = 5,
        程序修改发布 = 6,
        程序禁用 = 12,
        程序启用 = 13
        //    ,
        //系统参数添加=7,
        //系统参数修改 =8,
        //系统参数禁用 = 9,
        //发票申请 = 10,
        //修改发票申请=11
    }
    /// <summary>
    /// 程序状态
    /// </summary>
    public enum PGMProgramStatus
    {
        暂存 = 1,//暂存
        //待审核 = 2,
        发布 = 3
    }
    /// <summary>
    /// 考勤地点类型
    /// </summary>
    public enum ATEventPostionType
    {
        ATEventPostion = 0,//固定考勤地点
        TimingOnePostion = 1,
        TimingTwoPostion = 2,
        TimingThreePostion = 3,
        TimingFourPostion = 4,
        TimingFivePostion = 5
    }

    #endregion

}
