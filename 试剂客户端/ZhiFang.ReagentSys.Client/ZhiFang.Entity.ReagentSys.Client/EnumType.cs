using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Policy;
using System.Collections;

namespace ZhiFang.Entity.ReagentSys.Client
{

    /// <summary>
    /// 订货单写入第三方系统状态-IsThirdFlag字段值 
    /// </summary>
    public enum OrderDocIsThirdFlag
    {
        同步失败 = -1,
        无 = 0,
        同步成功 = 1

    }

    public enum InterfaceType
    {
        无类型 = 0,
        供货单接口 = 1,
        订货单接口 = 2
    }

    public enum InterfaceIndex
    {
        无类型 = 0,
        四川迈克 = 1,
        北京巴瑞 = 2
    }

    public enum CodeValue
    {
        无 = 0,
        无法从Session中获取用户ID和名称 = 1001
    }

    public enum InterfaceCodeValue
    {
        //接口Code信息
        必填参数信息为空 = 2001,
        token无效访问被拒绝 = 2002,
        客户端时间和服务器时间间隔相差过大 = 2003,

        //接口订货单Code信息
        订货单创建错误 = 2100,
        订货单主单信息为空 = 2101,
        订货单产品信息为空 = 2102,
        订货单必填字段信息为空 = 2103,

        //接口供货单Code信息
        供货单创建错误 = 2200,
        供货单主单信息为空 = 2201,
        供货单产品信息为空 = 2202,
        供货单必填字段信息为空 = 2203
    }

    public enum XmlConfigType
    {
        无类型 = 0,
        货品导入配置 = 101,
        订货单导入配置 = 102,
        订货单明细导入配置 = 103,
        供货单导入配置 = 104,
        供货单明细导入配置 = 105,
        新货品导入配置 = 106
    }

    #region 字典类
    public class BaseClassDicEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string FontColor { get; set; }
        public string BGColor { get; set; }
        public string SName { get; set; }
        public string DefaultValue { get; set; }
        public string Memo { get; set; }
    }
    /// <summary>
    /// 机构类型
    /// </summary>
    public static class ReaCenOrgType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 供货方 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "供货方", Code = "Supplier", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 订货方 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "订货方", Code = "Order", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaCenOrgType.供货方.Key, ReaCenOrgType.供货方.Value);
            dic.Add(ReaCenOrgType.订货方.Key, ReaCenOrgType.订货方.Value);
            return dic;
        }
    }
    /// <summary>
    /// 订货方单据类型
    /// </summary>
    public static class ReaCenOrgNextBillType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 销售 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "销售", Code = "XS", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 共建 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "共建", Code = "GJ", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 调拨 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "调拨", Code = "DB", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaCenOrgNextBillType.销售.Key, ReaCenOrgNextBillType.销售.Value);
            dic.Add(ReaCenOrgNextBillType.共建.Key, ReaCenOrgNextBillType.共建.Value);
            dic.Add(ReaCenOrgNextBillType.调拨.Key, ReaCenOrgNextBillType.调拨.Value);
            return dic;
        }
    }
    /// <summary>
    /// 客户端申请总单状态
    /// </summary>
    public static class ReaBmsReqDocStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 暂存 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "暂存", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 已申请 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "已申请", Code = "Applyed", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 审核通过 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "审核通过", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 审核退回 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "审核退回", Code = "UnReview", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 转为订单 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "转为订单", Code = "GenerateOrders", FontColor = "#ffffff", BGColor = "#1c8f36" });//转为订单=》生成订单
        //public static KeyValuePair<string, BaseClassDicEntity> 申请作废 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "申请作废", Code = "ApplyVoid", FontColor = "#ffffff", BGColor = "red" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsReqDocStatus.暂存.Key, ReaBmsReqDocStatus.暂存.Value);
            dic.Add(ReaBmsReqDocStatus.已申请.Key, ReaBmsReqDocStatus.已申请.Value);
            dic.Add(ReaBmsReqDocStatus.审核通过.Key, ReaBmsReqDocStatus.审核通过.Value);
            dic.Add(ReaBmsReqDocStatus.审核退回.Key, ReaBmsReqDocStatus.审核退回.Value);
            dic.Add(ReaBmsReqDocStatus.转为订单.Key, ReaBmsReqDocStatus.转为订单.Value);
            //dic.Add(ReaBmsReqDocStatus.申请作废.Key, ReaBmsReqDocStatus.申请作废.Value);
            return dic;
        }
    }
    /// <summary>
    /// 客户端订单状态
    /// </summary>
    public static class ReaBmsOrderDocStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 暂存 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "暂存", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 申请 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "申请", Code = "Applyed", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 审核退回 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "审核退回", Code = "UnReview", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 审核通过 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "审核通过", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 订单上传 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "订单上传", Code = "Sending", FontColor = "#ffffff", BGColor = "#7dc5eb" });
        public static KeyValuePair<string, BaseClassDicEntity> 部分验收 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "部分验收", Code = "Accept", FontColor = "#ffffff", BGColor = "#e8989a" });
        public static KeyValuePair<string, BaseClassDicEntity> 全部验收 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "全部验收", Code = "Accept", FontColor = "#ffffff", BGColor = "#e98f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 取消上传 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "取消上传", Code = "CancelSending", FontColor = "#ffffff", BGColor = "#e98f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 供应商确认 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "供应商确认", Code = "Confirm", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 取消确认 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "取消确认", Code = "CancelConfirm", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 订单转供货 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "订单转供货", Code = "OrderToSupply", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 审批退回 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "审批退回", Code = "UnApproval", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 审批通过 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", Name = "审批通过", Code = "Approval", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsOrderDocStatus.暂存.Key, ReaBmsOrderDocStatus.暂存.Value);
            dic.Add(ReaBmsOrderDocStatus.申请.Key, ReaBmsOrderDocStatus.申请.Value);
            dic.Add(ReaBmsOrderDocStatus.审核通过.Key, ReaBmsOrderDocStatus.审核通过.Value);
            dic.Add(ReaBmsOrderDocStatus.审核退回.Key, ReaBmsOrderDocStatus.审核退回.Value);
            dic.Add(ReaBmsOrderDocStatus.审批通过.Key, ReaBmsOrderDocStatus.审批通过.Value);
            dic.Add(ReaBmsOrderDocStatus.审批退回.Key, ReaBmsOrderDocStatus.审批退回.Value);
            dic.Add(ReaBmsOrderDocStatus.订单上传.Key, ReaBmsOrderDocStatus.订单上传.Value);
            dic.Add(ReaBmsOrderDocStatus.供应商确认.Key, ReaBmsOrderDocStatus.供应商确认.Value);
            dic.Add(ReaBmsOrderDocStatus.取消确认.Key, ReaBmsOrderDocStatus.取消确认.Value);
            dic.Add(ReaBmsOrderDocStatus.订单转供货.Key, ReaBmsOrderDocStatus.订单转供货.Value);
            dic.Add(ReaBmsOrderDocStatus.取消上传.Key, ReaBmsOrderDocStatus.取消上传.Value);
            dic.Add(ReaBmsOrderDocStatus.部分验收.Key, ReaBmsOrderDocStatus.部分验收.Value);
            dic.Add(ReaBmsOrderDocStatus.全部验收.Key, ReaBmsOrderDocStatus.全部验收.Value);

            return dic;
        }
    }
    /// <summary>
    /// 订货单数据标志
    /// </summary>
    public static class ReaBmsOrderDocIOFlag
    {
        public static KeyValuePair<string, BaseClassDicEntity> 未提取 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "未提取", Code = "NotExtracted", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 已上传 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "已上传", Code = "lUpload", FontColor = "#ffffff", BGColor = "#7dc5eb" });
        public static KeyValuePair<string, BaseClassDicEntity> 取消上传 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "取消上传", Code = "CancelUpload", FontColor = "#ffffff", BGColor = "#e98f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 供应商确认 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "供应商确认", Code = "Confirm", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 取消确认 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "取消确认", Code = "UConfirm", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 订单转供货 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "订单转供货", Code = "OrderToSupply", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsOrderDocIOFlag.未提取.Key, ReaBmsOrderDocIOFlag.未提取.Value);
            dic.Add(ReaBmsOrderDocIOFlag.已上传.Key, ReaBmsOrderDocIOFlag.已上传.Value);
            dic.Add(ReaBmsOrderDocIOFlag.取消上传.Key, ReaBmsOrderDocIOFlag.取消上传.Value);
            dic.Add(ReaBmsOrderDocIOFlag.供应商确认.Key, ReaBmsOrderDocIOFlag.供应商确认.Value);
            dic.Add(ReaBmsOrderDocIOFlag.取消确认.Key, ReaBmsOrderDocIOFlag.取消确认.Value);
            dic.Add(ReaBmsOrderDocIOFlag.订单转供货.Key, ReaBmsOrderDocIOFlag.订单转供货.Value);
            return dic;
        }
    }
    /// <summary>
    /// 订单第三方接口标志
    /// </summary>
    public static class ReaBmsOrderDocThirdFlag
    {
        public static KeyValuePair<string, BaseClassDicEntity> 未同步 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "未同步", Code = "NotExtracted", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 同步成功 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "同步成功", Code = "lUpload", FontColor = "#ffffff", BGColor = "#7dc5eb" });
        public static KeyValuePair<string, BaseClassDicEntity> 同步失败 = new KeyValuePair<string, BaseClassDicEntity>("-1", new BaseClassDicEntity() { Id = "-1", Name = "同步失败", Code = "CancelUpload", FontColor = "#ffffff", BGColor = "#e98f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsOrderDocThirdFlag.未同步.Key, ReaBmsOrderDocThirdFlag.未同步.Value);
            dic.Add(ReaBmsOrderDocThirdFlag.同步成功.Key, ReaBmsOrderDocThirdFlag.同步成功.Value);
            dic.Add(ReaBmsOrderDocThirdFlag.同步失败.Key, ReaBmsOrderDocThirdFlag.同步失败.Value);
            return dic;
        }
    }
    /// <summary>
    /// 订单总单付款状态
    /// </summary>
    public static class ReaBmsOrderDocPayStaus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 未付款 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "未付款", Code = "NoPay", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 已付款 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "已付款", Code = "NoBarCode", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsOrderDocPayStaus.未付款.Key, ReaBmsOrderDocPayStaus.未付款.Value);
            dic.Add(ReaBmsOrderDocPayStaus.已付款.Key, ReaBmsOrderDocPayStaus.已付款.Value);
            return dic;
        }
    }

    /// <summary>
    /// 订单-供货状态
    /// </summary>
    public static class ReaBmsOrderDocSupplyStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 未供货 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "未供货", Code = "UnSupply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 部分供货 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "部分供货", Code = "PartSupplied", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 终止供货 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "终止供货", Code = "StopSupplied", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 全部供货 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "全部供货", Code = "AllSupplied", FontColor = "#ffffff", BGColor = "#dd6572" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsOrderDocSupplyStatus.未供货.Key, ReaBmsOrderDocSupplyStatus.未供货.Value);
            dic.Add(ReaBmsOrderDocSupplyStatus.部分供货.Key, ReaBmsOrderDocSupplyStatus.部分供货.Value);
            dic.Add(ReaBmsOrderDocSupplyStatus.终止供货.Key, ReaBmsOrderDocSupplyStatus.终止供货.Value);
            dic.Add(ReaBmsOrderDocSupplyStatus.全部供货.Key, ReaBmsOrderDocSupplyStatus.全部供货.Value);
            return dic;
        }
    }

    /// <summary>
    /// 客户端供货单及明细状态
    /// </summary>
    public static class ReaBmsCenSaleDocAndDtlStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 暂存 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "暂存", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 确认提交 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "确认提交", Code = "Applyed", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 取消提交 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "取消提交", Code = "UnApplyed", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 审核通过 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "审核通过", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 取消审核 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "取消审核", Code = "UnReview", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 供货提取 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "供货提取", Code = "Extract", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 部分验收 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "部分验收", Code = "PartAccept", FontColor = "#ffffff", BGColor = "#e8989a" });
        public static KeyValuePair<string, BaseClassDicEntity> 全部验收 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "全部验收", Code = "AllAccept", FontColor = "#ffffff", BGColor = "#e98f36" });
        //public static KeyValuePair<string, BaseClassDicEntity> 实验室已提取 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "实验室已提取", Code = "Extract", FontColor = "#ffffff", BGColor = "#e98f36" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsCenSaleDocAndDtlStatus.暂存.Key, ReaBmsCenSaleDocAndDtlStatus.暂存.Value);
            dic.Add(ReaBmsCenSaleDocAndDtlStatus.确认提交.Key, ReaBmsCenSaleDocAndDtlStatus.确认提交.Value);
            dic.Add(ReaBmsCenSaleDocAndDtlStatus.取消提交.Key, ReaBmsCenSaleDocAndDtlStatus.取消提交.Value);
            dic.Add(ReaBmsCenSaleDocAndDtlStatus.取消审核.Key, ReaBmsCenSaleDocAndDtlStatus.取消审核.Value);
            dic.Add(ReaBmsCenSaleDocAndDtlStatus.审核通过.Key, ReaBmsCenSaleDocAndDtlStatus.审核通过.Value);
            dic.Add(ReaBmsCenSaleDocAndDtlStatus.供货提取.Key, ReaBmsCenSaleDocAndDtlStatus.供货提取.Value);
            dic.Add(ReaBmsCenSaleDocAndDtlStatus.部分验收.Key, ReaBmsCenSaleDocAndDtlStatus.部分验收.Value);
            dic.Add(ReaBmsCenSaleDocAndDtlStatus.全部验收.Key, ReaBmsCenSaleDocAndDtlStatus.全部验收.Value);
            //dic.Add(ReaBmsCenSaleDocAndDtlStatus.实验室已提取.Key, ReaBmsCenSaleDocAndDtlStatus.实验室已提取.Value);
            return dic;
        }
    }
    /// <summary>
    /// 供货单数据来源
    /// </summary>
    public static class ReaBmsCenSaleDocSource
    {
        public static KeyValuePair<string, BaseClassDicEntity> 供应商 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "供应商", Code = "PComp", FontColor = "#ffffff", BGColor = "#7dc5eb" });
        public static KeyValuePair<string, BaseClassDicEntity> 实验室 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "实验室", Code = "PLab", FontColor = "#ffffff", BGColor = "#be8dbd" });
        //public static KeyValuePair<string, BaseClassDicEntity> 手持供应商 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "手持供应商", Code = "PDAComp", FontColor = "#ffffff", BGColor = "#5cb85c" });
        //public static KeyValuePair<string, BaseClassDicEntity> 手持实验室 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "手持实验室", Code = "PDALab", FontColor = "#ffffff", BGColor = "#5cb85c" });
        //public static KeyValuePair<string, BaseClassDicEntity> PC供应商 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "PC供应商", Code = "PCComp", FontColor = "#ffffff", BGColor = "#e8989a" });
        //public static KeyValuePair<string, BaseClassDicEntity> PC实验室 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "PC实验室", Code = "PCLab", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsCenSaleDocSource.供应商.Key, ReaBmsCenSaleDocSource.供应商.Value);
            dic.Add(ReaBmsCenSaleDocSource.实验室.Key, ReaBmsCenSaleDocSource.实验室.Value);
            //dic.Add(ReaBmsCenSaleDocSource.手持实验室.Key, ReaBmsCenSaleDocSource.手持实验室.Value);
            //dic.Add(ReaBmsCenSaleDocSource.手持供应商.Key, ReaBmsCenSaleDocSource.手持供应商.Value);
            //dic.Add(ReaBmsCenSaleDocSource.PC供应商.Key, ReaBmsCenSaleDocSource.PC供应商.Value);
            //dic.Add(ReaBmsCenSaleDocSource.PC实验室.Key, ReaBmsCenSaleDocSource.PC实验室.Value);

            return dic;
        }
    }
    /// <summary>
    /// 供货单数据标志
    /// </summary>
    public static class ReaBmsCenSaleDocIOFlag
    {
        public static KeyValuePair<string, BaseClassDicEntity> 未提取 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "未提取", Code = "LabNotExtracted", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 已提取 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "已提取", Code = "LabExtracted", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 部分提取 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "部分提取", Code = "LabExtracted", FontColor = "#ffffff", BGColor = "#be8dbd" });
        public static KeyValuePair<string, BaseClassDicEntity> 已上传 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "已上传", Code = "Upload", FontColor = "#ffffff", BGColor = "#e8989a" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsCenSaleDocIOFlag.未提取.Key, ReaBmsCenSaleDocIOFlag.未提取.Value);
            dic.Add(ReaBmsCenSaleDocIOFlag.已提取.Key, ReaBmsCenSaleDocIOFlag.已提取.Value);
            dic.Add(ReaBmsCenSaleDocIOFlag.部分提取.Key, ReaBmsCenSaleDocIOFlag.部分提取.Value);
            dic.Add(ReaBmsCenSaleDocIOFlag.已上传.Key, ReaBmsCenSaleDocIOFlag.已上传.Value);
            return dic;
        }
    }
    /// <summary>
    /// 客户端验货单验收双确认方式
    /// </summary>
    public static class ConfirmSecAccepterType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 本实验室 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "本实验室", Code = "Laboratory", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 供应商 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "供应商", Code = "Supplier", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 供应商或实验室 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "供应商或实验室", Code = "LabOrSup", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()

        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ConfirmSecAccepterType.本实验室.Key, ConfirmSecAccepterType.本实验室.Value);
            dic.Add(ConfirmSecAccepterType.供应商.Key, ConfirmSecAccepterType.供应商.Value);
            dic.Add(ConfirmSecAccepterType.供应商或实验室.Key, ConfirmSecAccepterType.供应商或实验室.Value);
            return dic;
        }
    }
    /// <summary>
    /// 客户端验货单数据来源类型
    /// </summary>
    public static class ReaBmsCenSaleDocConfirmSourceType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 手工验收 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "手工验收", Code = "ManualInput", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 订单验收 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "订单验收", Code = "ReaOrder", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 供货验收 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "供货验收", Code = "ReaSale", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsCenSaleDocConfirmSourceType.手工验收.Key, ReaBmsCenSaleDocConfirmSourceType.手工验收.Value);
            dic.Add(ReaBmsCenSaleDocConfirmSourceType.订单验收.Key, ReaBmsCenSaleDocConfirmSourceType.订单验收.Value);
            dic.Add(ReaBmsCenSaleDocConfirmSourceType.供货验收.Key, ReaBmsCenSaleDocConfirmSourceType.供货验收.Value);
            return dic;
        }
    }
    /// <summary>
    /// 客户端验货单状态
    /// </summary>
    public static class ReaBmsCenSaleDocConfirmStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 待继续验收 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "待继续验收", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 已验收 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "已验收", Code = "Accept", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 部分入库 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "部分入库", Code = "PartStorage", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 全部入库 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "全部入库", Code = "Storage", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 货品删除 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "货品删除", Code = "DeleteReaGoods", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsCenSaleDocConfirmStatus.待继续验收.Key, ReaBmsCenSaleDocConfirmStatus.待继续验收.Value);
            dic.Add(ReaBmsCenSaleDocConfirmStatus.已验收.Key, ReaBmsCenSaleDocConfirmStatus.已验收.Value);
            dic.Add(ReaBmsCenSaleDocConfirmStatus.部分入库.Key, ReaBmsCenSaleDocConfirmStatus.部分入库.Value);
            dic.Add(ReaBmsCenSaleDocConfirmStatus.全部入库.Key, ReaBmsCenSaleDocConfirmStatus.全部入库.Value);
            dic.Add(ReaBmsCenSaleDocConfirmStatus.货品删除.Key, ReaBmsCenSaleDocConfirmStatus.货品删除.Value);
            return dic;
        }
    }
    /// <summary>
    /// 客户端验货单明细状态
    /// </summary>
    public static class ReaBmsCenSaleDtlConfirmStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 待继续验收 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "待继续验收", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 已验收 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "已验收", Code = "Accept", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 部分入库 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "部分入库", Code = "PartStorage", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 全部入库 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "全部入库", Code = "Storage", FontColor = "#ffffff", BGColor = "#1c8f36" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsCenSaleDtlConfirmStatus.待继续验收.Key, ReaBmsCenSaleDtlConfirmStatus.待继续验收.Value);
            dic.Add(ReaBmsCenSaleDtlConfirmStatus.已验收.Key, ReaBmsCenSaleDtlConfirmStatus.已验收.Value);
            dic.Add(ReaBmsCenSaleDtlConfirmStatus.部分入库.Key, ReaBmsCenSaleDtlConfirmStatus.部分入库.Value);
            dic.Add(ReaBmsCenSaleDtlConfirmStatus.全部入库.Key, ReaBmsCenSaleDtlConfirmStatus.全部入库.Value);

            return dic;
        }
    }
    /// <summary>
    /// 入库数据来源
    /// </summary>
    public static class ReaBmsInSourceType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 验货入库 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "验货入库", Code = "ConfirmIn", FontColor = "#ffffff", BGColor = "#7dc5eb" });
        public static KeyValuePair<string, BaseClassDicEntity> 库存初始化 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "库存初始化", Code = "ManualInput", FontColor = "#ffffff", BGColor = "#be8dbd" });
        public static KeyValuePair<string, BaseClassDicEntity> 退库入库 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "退库入库", Code = "OutOfInStorage", FontColor = "#ffffff", BGColor = "#e8989a" });
        public static KeyValuePair<string, BaseClassDicEntity> 借调入库 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "借调入库", Code = "LendStorage", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 盘盈入库 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "盘盈入库", Code = "TurnOver", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 供货入库 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "供货入库", Code = "TurnOver", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsInSourceType.验货入库.Key, ReaBmsInSourceType.验货入库.Value);
            dic.Add(ReaBmsInSourceType.库存初始化.Key, ReaBmsInSourceType.库存初始化.Value);
            dic.Add(ReaBmsInSourceType.退库入库.Key, ReaBmsInSourceType.退库入库.Value);
            dic.Add(ReaBmsInSourceType.借调入库.Key, ReaBmsInSourceType.借调入库.Value);
            dic.Add(ReaBmsInSourceType.盘盈入库.Key, ReaBmsInSourceType.盘盈入库.Value);
            dic.Add(ReaBmsInSourceType.供货入库.Key, ReaBmsInSourceType.供货入库.Value);
            return dic;
        }
    }
    /// <summary>
    /// 客户端入库类型
    /// </summary>
    public static class ReaBmsInDocInType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 验货入库 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "验货入库", Code = "ConfirmIn", FontColor = "#ffffff", BGColor = "#7dc5eb" });
        public static KeyValuePair<string, BaseClassDicEntity> 库存初始化 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "库存初始化", Code = "ManualInput", FontColor = "#ffffff", BGColor = "#be8dbd" });
        public static KeyValuePair<string, BaseClassDicEntity> 退库入库 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "退库入库", Code = "OutOfInStorage", FontColor = "#ffffff", BGColor = "#e8989a" });
        public static KeyValuePair<string, BaseClassDicEntity> 借调入库 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "借调入库", Code = "LendStorage", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 盘盈入库 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "盘盈入库", Code = "TurnOver", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsInDocInType.验货入库.Key, ReaBmsInDocInType.验货入库.Value);
            dic.Add(ReaBmsInDocInType.库存初始化.Key, ReaBmsInDocInType.库存初始化.Value);
            dic.Add(ReaBmsInDocInType.退库入库.Key, ReaBmsInDocInType.退库入库.Value);
            dic.Add(ReaBmsInDocInType.借调入库.Key, ReaBmsInDocInType.借调入库.Value);
            dic.Add(ReaBmsInDocInType.盘盈入库.Key, ReaBmsInDocInType.盘盈入库.Value);
            return dic;
        }
    }
    /// <summary>
    /// 客户端入库总单状态
    /// </summary>
    public static class ReaBmsInDocStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 待继续入库 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "待继续入库", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 已入库 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "已入库", Code = "ConfirmStorage", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsInDocStatus.待继续入库.Key, ReaBmsInDocStatus.待继续入库.Value);
            dic.Add(ReaBmsInDocStatus.已入库.Key, ReaBmsInDocStatus.已入库.Value);
            return dic;
        }
    }
    /// <summary>
    /// 供应商与货品关系操作记录状态
    /// </summary>
    public static class ReaGoodsOrgLinkStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 新增货品价格 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "新增货品价格", Code = "Apply", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 编辑货品价格 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "编辑货品价格", Code = "Applyed", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaGoodsOrgLinkStatus.新增货品价格.Key, ReaGoodsOrgLinkStatus.新增货品价格.Value);
            dic.Add(ReaGoodsOrgLinkStatus.编辑货品价格.Key, ReaGoodsOrgLinkStatus.编辑货品价格.Value);
            return dic;
        }
    }

    /// <summary>
    /// 客户端货品的条码类型
    /// </summary>
    public static class ReaGoodsBarCodeType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 批条码 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "批条码", Code = "BatchBarCode", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 盒条码 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "盒条码", Code = "BoxBarCode", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 无条码 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "无条码", Code = "NoBarCode", FontColor = "#ffffff", BGColor = "#1195db" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaGoodsBarCodeType.批条码.Key, ReaGoodsBarCodeType.批条码.Value);
            dic.Add(ReaGoodsBarCodeType.盒条码.Key, ReaGoodsBarCodeType.盒条码.Value);
            dic.Add(ReaGoodsBarCodeType.无条码.Key, ReaGoodsBarCodeType.无条码.Value);
            return dic;
        }
    }
    /// <summary>
    /// 货品条码操作类型
    /// </summary>
    public static class ReaGoodsBarcodeOperType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 供货 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "供货", Code = "Availability", FontColor = "#ffffff", BGColor = "#e98f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 验货接收 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "验货接收", Code = "ConfirmAccept", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 验货拒收 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "验货拒收", Code = "ConfirmRefuse", FontColor = "#ffffff", BGColor = "#d6204b" });
        public static KeyValuePair<string, BaseClassDicEntity> 验货入库 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "验货入库", Code = "Storage", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 库存初始化 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "库存初始化", Code = "ManualInputStock", FontColor = "#ffffff", BGColor = "#1c8f36" });//手工入库
        public static KeyValuePair<string, BaseClassDicEntity> 移库入库 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "移库入库", Code = "TransferIn", FontColor = "#ffffff", BGColor = "#FFA07A" });
        public static KeyValuePair<string, BaseClassDicEntity> 使用出库 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "使用出库", Code = "OutDtl", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 退库入库 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "退库入库", Code = "Withdrawal", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 盘库 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "盘库", Code = "Stocktaking", FontColor = "#ffffff", BGColor = "#be8dbd" });
        public static KeyValuePair<string, BaseClassDicEntity> 退供应商 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "退供应商", Code = "RetreatSuppliers", FontColor = "#ffffff", BGColor = "#a4579d" });
        public static KeyValuePair<string, BaseClassDicEntity> 盘盈入库 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "盘盈入库", Code = "Surplus", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 盘亏出库 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", Name = "盘亏出库", Code = "Loss", FontColor = "#ffffff", BGColor = "#d6204b" });
        public static KeyValuePair<string, BaseClassDicEntity> 借调入库 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", Name = "借调入库", Code = "LendStorage", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 报损出库 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", Name = "报损出库", Code = "Damaged", FontColor = "#ffffff", BGColor = "#d6204b" });
        public static KeyValuePair<string, BaseClassDicEntity> 移库出库 = new KeyValuePair<string, BaseClassDicEntity>("15", new BaseClassDicEntity() { Id = "15", Name = "移库出库", Code = "TransferOut", FontColor = "#ffffff", BGColor = "#FF7F50" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaGoodsBarcodeOperType.供货.Key, ReaGoodsBarcodeOperType.供货.Value);
            dic.Add(ReaGoodsBarcodeOperType.验货接收.Key, ReaGoodsBarcodeOperType.验货接收.Value);
            dic.Add(ReaGoodsBarcodeOperType.验货拒收.Key, ReaGoodsBarcodeOperType.验货拒收.Value);
            dic.Add(ReaGoodsBarcodeOperType.验货入库.Key, ReaGoodsBarcodeOperType.验货入库.Value);
            dic.Add(ReaGoodsBarcodeOperType.库存初始化.Key, ReaGoodsBarcodeOperType.库存初始化.Value);
            dic.Add(ReaGoodsBarcodeOperType.移库入库.Key, ReaGoodsBarcodeOperType.移库入库.Value);
            dic.Add(ReaGoodsBarcodeOperType.移库出库.Key, ReaGoodsBarcodeOperType.移库出库.Value);
            dic.Add(ReaGoodsBarcodeOperType.使用出库.Key, ReaGoodsBarcodeOperType.使用出库.Value);
            dic.Add(ReaGoodsBarcodeOperType.退库入库.Key, ReaGoodsBarcodeOperType.退库入库.Value);
            dic.Add(ReaGoodsBarcodeOperType.退供应商.Key, ReaGoodsBarcodeOperType.退供应商.Value);
            dic.Add(ReaGoodsBarcodeOperType.盘库.Key, ReaGoodsBarcodeOperType.盘库.Value);
            dic.Add(ReaGoodsBarcodeOperType.盘盈入库.Key, ReaGoodsBarcodeOperType.盘盈入库.Value);
            dic.Add(ReaGoodsBarcodeOperType.盘亏出库.Key, ReaGoodsBarcodeOperType.盘亏出库.Value);
            dic.Add(ReaGoodsBarcodeOperType.借调入库.Key, ReaGoodsBarcodeOperType.借调入库.Value);
            dic.Add(ReaGoodsBarcodeOperType.报损出库.Key, ReaGoodsBarcodeOperType.报损出库.Value);
            return dic;
        }
    }
    /// <summary>
    /// 库存操作记录操作类型
    /// </summary>
    public static class ReaBmsQtyDtlOperationOperType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 库存初始化 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "库存初始化", Code = "Availability", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 验货入库 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "验货入库", Code = "ComfirmInStorage", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 退库入库 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "退库入库", Code = "OutOfInStorage", FontColor = "#ffffff", BGColor = "#2aa515" });
        public static KeyValuePair<string, BaseClassDicEntity> 借调入库 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "借调入库", Code = "LendStorage", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static KeyValuePair<string, BaseClassDicEntity> 借出出库 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "借出出库", Code = "LendingOut", FontColor = "#ffffff", BGColor = "#7dc5eb" });
        public static KeyValuePair<string, BaseClassDicEntity> 使用出库 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "使用出库", Code = "OutQtyDtl", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 归还出库 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "归还出库", Code = "ReturnOut", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 借入入库 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "借入入库", Code = "BorrowingStorage", FontColor = "#ffffff", BGColor = "#be8dbd" });
        public static KeyValuePair<string, BaseClassDicEntity> 退供应商 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "退供应商", Code = "RetreatSuppliers", FontColor = "#ffffff", BGColor = "#a4579d" });
        public static KeyValuePair<string, BaseClassDicEntity> 盘盈入库 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "盘盈入库", Code = "SurplusIn", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 盘亏出库 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "盘亏出库", Code = "LossOut", FontColor = "#ffffff", BGColor = "#88147f" });
        public static KeyValuePair<string, BaseClassDicEntity> 报损出库 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", Name = "报损出库", Code = "Damaged", FontColor = "#ffffff", BGColor = "#d6204b" });
        public static KeyValuePair<string, BaseClassDicEntity> 移库出库 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", Name = "移库出库", Code = "TransferOut", FontColor = "#ffffff", BGColor = "#FF7F50" });
        public static KeyValuePair<string, BaseClassDicEntity> 移库入库 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", Name = "移库入库", Code = "TransferIn", FontColor = "#ffffff", BGColor = "#FFA07A" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsQtyDtlOperationOperType.库存初始化.Key, ReaBmsQtyDtlOperationOperType.库存初始化.Value);
            dic.Add(ReaBmsQtyDtlOperationOperType.验货入库.Key, ReaBmsQtyDtlOperationOperType.验货入库.Value);
            dic.Add(ReaBmsQtyDtlOperationOperType.退库入库.Key, ReaBmsQtyDtlOperationOperType.退库入库.Value);
            dic.Add(ReaBmsQtyDtlOperationOperType.借调入库.Key, ReaBmsQtyDtlOperationOperType.借调入库.Value);
            dic.Add(ReaBmsQtyDtlOperationOperType.借出出库.Key, ReaBmsQtyDtlOperationOperType.借出出库.Value);
            dic.Add(ReaBmsQtyDtlOperationOperType.使用出库.Key, ReaBmsQtyDtlOperationOperType.使用出库.Value);
            dic.Add(ReaBmsQtyDtlOperationOperType.归还出库.Key, ReaBmsQtyDtlOperationOperType.归还出库.Value);
            dic.Add(ReaBmsQtyDtlOperationOperType.借入入库.Key, ReaBmsQtyDtlOperationOperType.借入入库.Value);
            dic.Add(ReaBmsQtyDtlOperationOperType.盘盈入库.Key, ReaBmsQtyDtlOperationOperType.盘盈入库.Value);
            dic.Add(ReaBmsQtyDtlOperationOperType.盘亏出库.Key, ReaBmsQtyDtlOperationOperType.盘亏出库.Value);
            dic.Add(ReaBmsQtyDtlOperationOperType.报损出库.Key, ReaBmsQtyDtlOperationOperType.报损出库.Value);
            dic.Add(ReaBmsQtyDtlOperationOperType.退供应商.Key, ReaBmsQtyDtlOperationOperType.退供应商.Value);
            dic.Add(ReaBmsQtyDtlOperationOperType.移库出库.Key, ReaBmsQtyDtlOperationOperType.移库出库.Value);
            dic.Add(ReaBmsQtyDtlOperationOperType.移库入库.Key, ReaBmsQtyDtlOperationOperType.移库入库.Value);
            return dic;
        }
    }

    /// <summary>
    /// 系统参数类型
    /// </summary>
    public static class SYSParaType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 全系统 = new KeyValuePair<string, BaseClassDicEntity>("SYS", new BaseClassDicEntity() { Id = "SYS", Name = "全系统", Code = "SYS", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 可配置参数 = new KeyValuePair<string, BaseClassDicEntity>("CONFIG", new BaseClassDicEntity() { Id = "CONFIG", Name = "可配置参数", Code = "CONFIG", FontColor = "#ffffff", BGColor = "#aad08f" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(SYSParaType.全系统.Key, SYSParaType.全系统.Value);
            dic.Add(SYSParaType.可配置参数.Key, SYSParaType.可配置参数.Value);
            return dic;
        }
    }
    /// <summary>
    /// 系统参数编码(按LabID创建)
    /// </summary>
    public static class SYSParaNo
    {
        public static KeyValuePair<string, BaseClassDicEntity> 订单上传类型 = new KeyValuePair<string, BaseClassDicEntity>("C-RBCO-UPLO-0011", new BaseClassDicEntity() { Id = "C-RBCO-UPLO-0011", Name = "订单上传类型", Code = "OrderUploadeType", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "1", Memo = "1:不上传;2:上传平台;3:上传第三方系统;4:上传平台及上传第三方系统;", SName = "订单" });
        public static KeyValuePair<string, BaseClassDicEntity> 订单审核通过同时直接订单上传 = new KeyValuePair<string, BaseClassDicEntity>("C-RBCO-CHEC-0018", new BaseClassDicEntity() { Id = "C-RBCO-CHEC-0018", Name = "订单审核通过同时直接订单上传", Code = "OrderCheckIsUploaded", FontColor = "#ffffff", BGColor = "#f4c600", DefaultValue = "2", Memo = "1:是;2:否;", SName = "订单" });
        public static KeyValuePair<string, BaseClassDicEntity> 订单审批金额 = new KeyValuePair<string, BaseClassDicEntity>("C-RBOD-APPR-0028", new BaseClassDicEntity() { Id = "C-RBOD-APPR-0028", Name = "订单审批金额", Code = "ReaBmsCenOrderDocApprovalTotalPrice", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "100000000", Memo = "判断订单是否需要审批的金额比较值,请输入有效数字", SName = "订单" });


        public static KeyValuePair<string, BaseClassDicEntity> 实验室数据升级版本 = new KeyValuePair<string, BaseClassDicEntity>("C-ULAB-DATA-0022", new BaseClassDicEntity() { Id = "C-ULAB-DATA-0022", Name = "实验室数据升级版本", Code = "LAB_DBVersion", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "1.0.0.1", Memo = "实验室数据升级版本", SName = "实验室升级版本" });
        public static KeyValuePair<string, BaseClassDicEntity> 数据库是否独立部署 = new KeyValuePair<string, BaseClassDicEntity>("C-DATA-ISDI-0017", new BaseClassDicEntity() { Id = "C-DATA-ISDI-0017", Name = "数据库是否独立部署", Code = "ReaDataBaseIsDeploy", FontColor = "#ffffff", BGColor = "#1c8f36", DefaultValue = "2", Memo = "1:是;2:否;", SName = "数据库" });


        public static KeyValuePair<string, BaseClassDicEntity> 启用用户UI配置 = new KeyValuePair<string, BaseClassDicEntity>("C-EUSE-UICF-0035", new BaseClassDicEntity() { Id = "C-EUSE-UICF-0035", Name = "启用用户UI配置", Code = "EnableUserUIConfig", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "2", Memo = "1:是;2:否;", SName = "UI" });
        public static KeyValuePair<string, BaseClassDicEntity> 列表默认分页记录数 = new KeyValuePair<string, BaseClassDicEntity>("C-LRMP-UIPA-0030", new BaseClassDicEntity() { Id = "C-LRMP-UIPA-0030", Name = "列表默认分页记录数", Code = "ReaUIDefaultPageSize", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "50", Memo = "系统默认列表的分页数为50条记录每页,用户可自行设置,设置保存后需要重新登录才生效", SName = "UI" });


        public static KeyValuePair<string, BaseClassDicEntity> 启用库存报警 = new KeyValuePair<string, BaseClassDicEntity>("C-RBQD-BQIW-0001", new BaseClassDicEntity() { Id = "C-RBQD-BQIW-0001", Name = "是否启用库存报警", Code = "IsInventoryAlarm", FontColor = "#ffffff", BGColor = "#dd6572", DefaultValue = "0", Memo = "是否启用(0:不启用;1:启用)", SName = "预警" });
        public static KeyValuePair<string, BaseClassDicEntity> 启用效期报警 = new KeyValuePair<string, BaseClassDicEntity>("C-RBQD-BQEW-0002", new BaseClassDicEntity() { Id = "C-RBQD-BQEW-0002", Name = "是否启用效期报警", Code = "IsExpirationAlarm", FontColor = "#ffffff", BGColor = "#f4c600", DefaultValue = "0", Memo = "是否启用(0:不启用;1:启用)", SName = "预警" });
        public static KeyValuePair<string, BaseClassDicEntity> 货品效期预警天数 = new KeyValuePair<string, BaseClassDicEntity>("C-RBQD-GVWD-0003", new BaseClassDicEntity() { Id = "C-RBQD-GVWD-0003", Name = "货品效期预警天数", Code = "GoodsValidityWarnDays", FontColor = "#ffffff", BGColor = "#aad08f", DefaultValue = "10", Memo = "正整数天数(1,2,3,4,5等)", SName = "预警" });
        public static KeyValuePair<string, BaseClassDicEntity> 验收货品扫码 = new KeyValuePair<string, BaseClassDicEntity>("C-RBDC-GASC-0004", new BaseClassDicEntity() { Id = "C-RBDC-GASC-0004", Name = "验收货品扫码", Code = "AcceptanceScanCode", FontColor = "#ffffff", BGColor = "#7cba59", DefaultValue = "2", Memo = "严格模式:1,混合模式：2", SName = "货品扫码" });
        public static KeyValuePair<string, BaseClassDicEntity> 入库货品扫码 = new KeyValuePair<string, BaseClassDicEntity>("C-RBID-GISC-0005", new BaseClassDicEntity() { Id = "C-RBID-GISC-0005", Name = "入库货品扫码", Code = "InScanCode", FontColor = "#ffffff", BGColor = "#2aa515", DefaultValue = "2", Memo = "严格模式:1,混合模式：2", SName = "货品扫码" });
        public static KeyValuePair<string, BaseClassDicEntity> 出库货品扫码 = new KeyValuePair<string, BaseClassDicEntity>("C-RBOD-GOSC-0006", new BaseClassDicEntity() { Id = "C-RBOD-GOSC-0006", Name = "出库货品扫码", Code = "OutScanCode", FontColor = "#ffffff", BGColor = "#7dc5eb", DefaultValue = "2", Memo = "严格模式:1,混合模式：2", SName = "货品扫码" });
        public static KeyValuePair<string, BaseClassDicEntity> 移库货品扫码 = new KeyValuePair<string, BaseClassDicEntity>("C-RBTD-GTSC-0007", new BaseClassDicEntity() { Id = "C-RBTD-GTSC-0007", Name = "移库货品扫码", Code = "TransferScanCode", FontColor = "#ffffff", BGColor = "#17abe3", DefaultValue = "2", Memo = "严格模式:1,混合模式：2", SName = "货品扫码" });
        public static KeyValuePair<string, BaseClassDicEntity> 盘库货品扫码 = new KeyValuePair<string, BaseClassDicEntity>("C-RBPD-GCSC-0008", new BaseClassDicEntity() { Id = "C-RBPD-GCSC-0008", Name = "盘库货品扫码", Code = "CheckScanCode", FontColor = "#ffffff", BGColor = "#1195db", DefaultValue = "2", Memo = "严格模式:1,混合模式：2", SName = "货品扫码" });
        public static KeyValuePair<string, BaseClassDicEntity> 访问BS平台的URL = new KeyValuePair<string, BaseClassDicEntity>("C-BSPL-PURL-0009", new BaseClassDicEntity() { Id = "C-BSPL-PURL-0009", Name = "访问BS平台的URL", Code = "BSPlatformURL", FontColor = "#ffffff", BGColor = "#be8dbd", DefaultValue = "", Memo = "客户端访问BS平台的入口", SName = "平台" });
        public static KeyValuePair<string, BaseClassDicEntity> 验收双确认方式 = new KeyValuePair<string, BaseClassDicEntity>("C-RBSC-SAAC-0010", new BaseClassDicEntity() { Id = "C-RBSC-SAAC-0010", Name = "验收双确认方式", Code = "SecAccepterAccount", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "1", Memo = "本实验室(默认):1,本实验室双人确认:2", SName = "审核" });

        public static KeyValuePair<string, BaseClassDicEntity> 出库领用人是否为登录人 = new KeyValuePair<string, BaseClassDicEntity>("C-RBOD-OBIL-0012", new BaseClassDicEntity() { Id = "C-RBOD-OBIL-0012", Name = "出库领用人是否为登录人", Code = "OutboundIsLogin", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "2", Memo = "否：1,是：2", SName = "出库" });
        public static KeyValuePair<string, BaseClassDicEntity> 盘库审核是否需要确认 = new KeyValuePair<string, BaseClassDicEntity>("C-RBCD-ISCH-0013", new BaseClassDicEntity() { Id = "C-RBCD-ISCH-0013", Name = "盘库审核是否需要确认", Code = "ReaBmsCheckDocIsCheck", FontColor = "#ffffff", BGColor = "#17abe3", DefaultValue = "1", Memo = "1:不需要;2:需要;", SName = "审核" });

        public static KeyValuePair<string, BaseClassDicEntity> 使用出库审核是否需要确认 = new KeyValuePair<string, BaseClassDicEntity>("C-RBCD-ISCH-0014", new BaseClassDicEntity() { Id = "C-RBCD-ISCH-0014", Name = "使用出库审核是否需要确认", Code = "ReaBmsOutDocUseIsCheck", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "0", Memo = "0:不需要;1:需要;", SName = "审核" });
        public static KeyValuePair<string, BaseClassDicEntity> 报损出库审核是否需要确认 = new KeyValuePair<string, BaseClassDicEntity>("C-RBCD-ISCH-0015", new BaseClassDicEntity() { Id = "C-RBCD-ISCH-0015", Name = "报损出库审核是否需要确认", Code = "ReaBmsOutDocLossIsCheck", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "0", Memo = "0:不需要;1:需要;", SName = "审核" });
        public static KeyValuePair<string, BaseClassDicEntity> 退供应商出库审核是否需要确认 = new KeyValuePair<string, BaseClassDicEntity>("C-RBCD-ISCH-0016", new BaseClassDicEntity() { Id = "C-RBCD-ISCH-0016", Name = "退供应商出库审核是否需要确认", Code = "ReaBmsOutDocRefundSIsCheck", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "0", Memo = "0:不需要;1:需要;", SName = "审核" });

        public static KeyValuePair<string, BaseClassDicEntity> 供应商确认订单时是否需要强制校验货品编码 = new KeyValuePair<string, BaseClassDicEntity>("C-RBCO-COFM-0019", new BaseClassDicEntity() { Id = "C-RBCO-COFM-0019", Name = "供应商确认订单时是否需要强制校验货品编码", Code = "OrderConfirmIsVerifyGoodsNo", FontColor = "#ffffff", BGColor = "#aad08f", DefaultValue = "2", Memo = "1:是;2:否;", SName = "订单" });
        public static KeyValuePair<string, BaseClassDicEntity> 使用出库仪器是否必填 = new KeyValuePair<string, BaseClassDicEntity>("C-RBCD-ISCH-0021", new BaseClassDicEntity() { Id = "C-RBCD-ISCH-0021", Name = "使用出库仪器是否必填", Code = "ReaBmsOutDocUseIsEquip", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "1", Memo = "1:是;2:否;", SName = "出库" });

        public static KeyValuePair<string, BaseClassDicEntity> 是否按出库人权限出库 = new KeyValuePair<string, BaseClassDicEntity>("C-RBCD-ISCH-0023", new BaseClassDicEntity() { Id = "C-RBCD-ISCH-0023", Name = "是否按出库人权限出库", Code = "ReaBmsOutDocUseIsEmpOut", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "1", Memo = "1:是;2:否;范围包含出库管理,出库查询,出库明细统计", SName = "库房权限" });
        public static KeyValuePair<string, BaseClassDicEntity> 注册证将过期预警天数 = new KeyValuePair<string, BaseClassDicEntity>("C-RRWW-WAEN-0024", new BaseClassDicEntity() { Id = "C-RRWW-WAEN-0024", Name = "注册证将过期预警天数", Code = "RegistWillexpireWarning", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "10", Memo = "正整数天数(10,15,20等)", SName = "预警" });
        public static KeyValuePair<string, BaseClassDicEntity> 启用注册证预警 = new KeyValuePair<string, BaseClassDicEntity>("C-RRWW-ISWA-0025", new BaseClassDicEntity() { Id = "C-RRWW-ISWA-0025", Name = "是否启用注册证预警", Code = "IsRegistAlarm", FontColor = "#ffffff", BGColor = "#f4c600", DefaultValue = "0", Memo = "是否启用(0:不启用;1:启用)", SName = "预警" });

        public static KeyValuePair<string, BaseClassDicEntity> 移库审核是否需要确认 = new KeyValuePair<string, BaseClassDicEntity>("C-RTCD-ISCH-0026", new BaseClassDicEntity() { Id = "C-RTCD-ISCH-0026", Name = "移库审核是否需要确认", Code = "ReaBmsTransferDocIsCheck", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "0", Memo = "0:不需要;1:需要;", SName = "审核" });
        public static KeyValuePair<string, BaseClassDicEntity> 是否按移库人权限移库 = new KeyValuePair<string, BaseClassDicEntity>("C-RBTD-ISEO-0027", new BaseClassDicEntity() { Id = "C-RBTD-ISEO-0027", Name = "是否按移库人权限移库", Code = "ReaBmsTransferDocIsUseEmpOut", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "2", Memo = "1:是;2:否;", SName = "库房权限" });

        public static KeyValuePair<string, BaseClassDicEntity> 出库确认后是否调用退库接口 = new KeyValuePair<string, BaseClassDicEntity>("C-RBOD-ISRI-0029", new BaseClassDicEntity() { Id = "C-RBOD-ISRI-0029", Name = "出库确认后是否调用退库接口", Code = "ReaBmsOutDocIsReturnInterface", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "2", Memo = "1:是;2:否;", SName = "物资接口" });

        public static KeyValuePair<string, BaseClassDicEntity> 是否启用库存库房权限 = new KeyValuePair<string, BaseClassDicEntity>("C-RBQT-ISUE-0031", new BaseClassDicEntity() { Id = "C-RBQT-ISUE-0031", Name = "是否启用库存库房权限", Code = "ReaBmsQtyDtlIsUseEmp", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "2", Memo = "1:是;2:否;权限范围包含库存查询,库存变化跟踪", SName = "库房权限" });
        public static KeyValuePair<string, BaseClassDicEntity> 库存货品是否需要性能验证后才能使用出库 = new KeyValuePair<string, BaseClassDicEntity>("C-RBOD-ISPV-0032", new BaseClassDicEntity() { Id = "C-RBOD-ISPV-0032", Name = "库存货品是否需要性能验证后才能使用出库", Code = "ReaBmsOutIsNeedPerformanceTest", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "2", Memo = "1:是;2:否;库存货品是否需要性能验证后才能使用出库", SName = "出库" });

        public static KeyValuePair<string, BaseClassDicEntity> 效期预警默认已过期天数 = new KeyValuePair<string, BaseClassDicEntity>("C-EAWE-DAYS-0033", new BaseClassDicEntity() { Id = "C-EAWE-DAYS-0033", Name = "效期预警默认已过期天数", Code = "ExpirationAlarmWillexpireDefaultDays", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "1", Memo = "正整数天数(10,15,20等)", SName = "预警" });
        public static KeyValuePair<string, BaseClassDicEntity> 注册证预警默认已过期天数 = new KeyValuePair<string, BaseClassDicEntity>("C-REWE-DDAY-0034", new BaseClassDicEntity() { Id = "C-REWE-DDAY-0034", Name = "注册证预警默认已过期天数", Code = "RegistWarnExpiredDefaultDays", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "1", Memo = "正整数天数(10,15,20等)", SName = "预警" });

        public static KeyValuePair<string, BaseClassDicEntity> 是否需要支持直接出库 = new KeyValuePair<string, BaseClassDicEntity>("C-RBOD-ISDO-0036", new BaseClassDicEntity() { Id = "C-RBOD-ISDO-0036", Name = "是否需要支持直接出库", Code = "ISNeedSupportDirectOut", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "1", Memo = "1:是(新增出库按钮显示);2:否(新增出库按钮隐藏);", SName = "出库" });

        public static KeyValuePair<string, BaseClassDicEntity> 接口数据是否需要重新生成条码 = new KeyValuePair<string, BaseClassDicEntity>("C-WTID-NTRB-0037", new BaseClassDicEntity() { Id = "C-WTID-NTRB-0037", Name = "接口数据是否需要重新生成条码", Code = "InterfaceDataISNeedCreateBarCode", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "2", Memo = "1:是;2:否;", SName = "接口" });

        public static KeyValuePair<string, BaseClassDicEntity> 移库或出库扫码是否允许从所有库房获取库存货品 = new KeyValuePair<string, BaseClassDicEntity>("C-TOBC-ISAL-0038", new BaseClassDicEntity() { Id = "C-TOBC-ISAL-0038", Name = "移库或出库扫码是否允许从所有库房获取库存货品", Code = "TranOrOutBarCodeIsAllowOfALLStorage", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "2", Memo = "1:是;2:否;", SName = "货品扫码" });

        public static KeyValuePair<string, BaseClassDicEntity> 盘库时实盘数是否取库存数 = new KeyValuePair<string, BaseClassDicEntity>("C-IVTY-ISTQ-0039", new BaseClassDicEntity() { Id = "C-IVTY-ISTQ-0039", Name = "盘库时实盘数是否取库存数", Code = "InventoryIsTakenFromQty", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "2", Memo = "1:是;2:否;", SName = "盘库" });

        public static KeyValuePair<string, BaseClassDicEntity> 是否开启近效期 = new KeyValuePair<string, BaseClassDicEntity>("C-RBOU-ISON-0040", new BaseClassDicEntity() { Id = "C-RBOU-ISON-0040", Name = "是否开启近效期", Code = "IsOpenNearEffectPeriod", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "1", Memo = "1:开启;2:不开启;3:界面选择默认开启;4:界面选择默认不开启;", SName = "出库" });
        public static KeyValuePair<string, BaseClassDicEntity> 是否强制近效期出库 = new KeyValuePair<string, BaseClassDicEntity>("C-RBOU-ISNP-0041", new BaseClassDicEntity() { Id = "C-RBOU-ISNP-0041", Name = "是否强制近效期出库", Code = " IsOutOfStockInNeartermPeriod", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "2", Memo = "1:强制;2:不强制;3:界面选择默认强制;4:界面选择默认不强制;", SName = "出库" });

        public static KeyValuePair<string, BaseClassDicEntity> 供应批次合并 = new KeyValuePair<string, BaseClassDicEntity>("C-RBOU-IIBM-0042", new BaseClassDicEntity() { Id = "C-RBOU-IIBM-0042", Name = "供应批次合并", Code = " IsInDocBatchMerge", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "1", Memo = "1后台默认合并;2后台默认不合并;3界面选择默认合并;4界面选择默认不合并;", SName = "出库" });

        public static KeyValuePair<string, BaseClassDicEntity> 出库确认后是否上传试剂平台 = new KeyValuePair<string, BaseClassDicEntity>("C-RBOD-IUPC-0045", new BaseClassDicEntity() { Id = "C-RBOD-IUPC-0045", Name = "出库确认后是否上传试剂平台", Code = "ReaBmsOutDocIsUploadCenter", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "2", Memo = "1:是;2:否;", SName = "出库" });

        //采购申请
        public static KeyValuePair<string, BaseClassDicEntity> 平均使用量计算月数 = new KeyValuePair<string, BaseClassDicEntity>("C-CGSQ-AUCM-0043", new BaseClassDicEntity() { Id = "C-CGSQ-AUCM-0043", Name = "平均使用量计算月数", Code = "AvgUsedCalMonth", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "3", Memo = "默认3，表示计算前3个月的平均使用量", SName = "采购" });
        public static KeyValuePair<string, BaseClassDicEntity> 采购系数 = new KeyValuePair<string, BaseClassDicEntity>("C-CGSQ-PUCO-0044", new BaseClassDicEntity() { Id = "C-CGSQ-PUCO-0044", Name = "采购系数", Code = "PurchaseCoefficient", FontColor = "#ffffff", BGColor = "#a4579d", DefaultValue = "2.0", Memo = "采购系数，默认2.0", SName = "采购" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(SYSParaNo.启用库存报警.Key, SYSParaNo.启用库存报警.Value);
            dic.Add(SYSParaNo.启用效期报警.Key, SYSParaNo.启用效期报警.Value);
            dic.Add(SYSParaNo.货品效期预警天数.Key, SYSParaNo.货品效期预警天数.Value);
            dic.Add(SYSParaNo.验收货品扫码.Key, SYSParaNo.验收货品扫码.Value);
            dic.Add(SYSParaNo.入库货品扫码.Key, SYSParaNo.入库货品扫码.Value);

            dic.Add(SYSParaNo.出库货品扫码.Key, SYSParaNo.出库货品扫码.Value);
            dic.Add(SYSParaNo.移库货品扫码.Key, SYSParaNo.移库货品扫码.Value);
            dic.Add(SYSParaNo.访问BS平台的URL.Key, SYSParaNo.访问BS平台的URL.Value);
            dic.Add(SYSParaNo.订单上传类型.Key, SYSParaNo.订单上传类型.Value);
            dic.Add(SYSParaNo.验收双确认方式.Key, SYSParaNo.验收双确认方式.Value);

            dic.Add(SYSParaNo.盘库审核是否需要确认.Key, SYSParaNo.盘库审核是否需要确认.Value);
            dic.Add(SYSParaNo.数据库是否独立部署.Key, SYSParaNo.数据库是否独立部署.Value);
            dic.Add(SYSParaNo.出库领用人是否为登录人.Key, SYSParaNo.出库领用人是否为登录人.Value);
            dic.Add(SYSParaNo.使用出库审核是否需要确认.Key, SYSParaNo.使用出库审核是否需要确认.Value);
            dic.Add(SYSParaNo.报损出库审核是否需要确认.Key, SYSParaNo.报损出库审核是否需要确认.Value);

            dic.Add(SYSParaNo.退供应商出库审核是否需要确认.Key, SYSParaNo.退供应商出库审核是否需要确认.Value);
            dic.Add(SYSParaNo.订单审核通过同时直接订单上传.Key, SYSParaNo.订单审核通过同时直接订单上传.Value);
            dic.Add(SYSParaNo.供应商确认订单时是否需要强制校验货品编码.Key, SYSParaNo.供应商确认订单时是否需要强制校验货品编码.Value);
            //dic.Add(SYSParaNo.业务接口URL配置.Key, SYSParaNo.业务接口URL配置.Value);
            dic.Add(SYSParaNo.使用出库仪器是否必填.Key, SYSParaNo.使用出库仪器是否必填.Value);
            dic.Add(SYSParaNo.实验室数据升级版本.Key, SYSParaNo.实验室数据升级版本.Value);

            dic.Add(SYSParaNo.是否按出库人权限出库.Key, SYSParaNo.是否按出库人权限出库.Value);
            dic.Add(SYSParaNo.注册证将过期预警天数.Key, SYSParaNo.注册证将过期预警天数.Value);
            dic.Add(SYSParaNo.启用注册证预警.Key, SYSParaNo.启用注册证预警.Value);
            dic.Add(SYSParaNo.移库审核是否需要确认.Key, SYSParaNo.移库审核是否需要确认.Value);
            dic.Add(SYSParaNo.是否按移库人权限移库.Key, SYSParaNo.是否按移库人权限移库.Value);

            dic.Add(SYSParaNo.订单审批金额.Key, SYSParaNo.订单审批金额.Value);
            dic.Add(SYSParaNo.出库确认后是否调用退库接口.Key, SYSParaNo.出库确认后是否调用退库接口.Value);
            dic.Add(SYSParaNo.列表默认分页记录数.Key, SYSParaNo.列表默认分页记录数.Value);
            dic.Add(SYSParaNo.是否启用库存库房权限.Key, SYSParaNo.是否启用库存库房权限.Value);
            dic.Add(SYSParaNo.库存货品是否需要性能验证后才能使用出库.Key, SYSParaNo.库存货品是否需要性能验证后才能使用出库.Value);
            dic.Add(SYSParaNo.效期预警默认已过期天数.Key, SYSParaNo.效期预警默认已过期天数.Value);
            dic.Add(SYSParaNo.注册证预警默认已过期天数.Key, SYSParaNo.注册证预警默认已过期天数.Value);
            dic.Add(SYSParaNo.启用用户UI配置.Key, SYSParaNo.启用用户UI配置.Value);
            dic.Add(SYSParaNo.是否需要支持直接出库.Key, SYSParaNo.是否需要支持直接出库.Value);
            dic.Add(SYSParaNo.接口数据是否需要重新生成条码.Key, SYSParaNo.接口数据是否需要重新生成条码.Value);
            dic.Add(SYSParaNo.移库或出库扫码是否允许从所有库房获取库存货品.Key, SYSParaNo.移库或出库扫码是否允许从所有库房获取库存货品.Value);

            dic.Add(SYSParaNo.盘库时实盘数是否取库存数.Key, SYSParaNo.盘库时实盘数是否取库存数.Value);
            dic.Add(SYSParaNo.是否开启近效期.Key, SYSParaNo.是否开启近效期.Value);
            dic.Add(SYSParaNo.是否强制近效期出库.Key, SYSParaNo.是否强制近效期出库.Value);

            dic.Add(SYSParaNo.供应批次合并.Key, SYSParaNo.供应批次合并.Value);
            dic.Add(SYSParaNo.出库确认后是否上传试剂平台.Key, SYSParaNo.出库确认后是否上传试剂平台.Value);

            //采购申请
            dic.Add(SYSParaNo.平均使用量计算月数.Key, SYSParaNo.平均使用量计算月数.Value);
            dic.Add(SYSParaNo.采购系数.Key, SYSParaNo.采购系数.Value);
            return dic;
        }
    }
    /// <summary>
    /// 客户端盘库单状态
    /// </summary>
    public static class ReaBmsCheckDocStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 盘库锁定 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "盘库锁定", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 确认盘库 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "确认盘库", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 差异调整中 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "差异调整中", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#e8989a" });
        public static KeyValuePair<string, BaseClassDicEntity> 差异调整完成 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "差异调整完成", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsCheckDocStatus.盘库锁定.Key, ReaBmsCheckDocStatus.盘库锁定.Value);
            dic.Add(ReaBmsCheckDocStatus.确认盘库.Key, ReaBmsCheckDocStatus.确认盘库.Value);
            dic.Add(ReaBmsCheckDocStatus.差异调整中.Key, ReaBmsCheckDocStatus.差异调整中.Value);
            dic.Add(ReaBmsCheckDocStatus.差异调整完成.Key, ReaBmsCheckDocStatus.差异调整完成.Value);
            return dic;
        }
    }
    /// <summary>
    /// 客户端盘库单锁定标志
    /// </summary>
    public static class ReaBmsCheckDocLock
    {
        public static KeyValuePair<string, BaseClassDicEntity> 已锁定 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "已锁定", Code = "Locked", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 已解锁 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "已解锁", Code = "Unlocked", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsCheckDocLock.已锁定.Key, ReaBmsCheckDocLock.已锁定.Value);
            dic.Add(ReaBmsCheckDocLock.已解锁.Key, ReaBmsCheckDocLock.已解锁.Value);
            return dic;
        }
    }
    /// <summary>
    /// 客户端盘库单盘库结果
    /// </summary>
    public static class ReaBmsCheckResult
    {
        public static KeyValuePair<string, BaseClassDicEntity> 未盘盈及未盘亏 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "未盘盈及未盘亏", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 已盘盈 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "已盘盈", Code = "Applyed", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 已盘亏 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "已盘亏", Code = "UnReview", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 已盘盈已盘亏 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "已盘盈已盘亏", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsCheckResult.未盘盈及未盘亏.Key, ReaBmsCheckResult.未盘盈及未盘亏.Value);
            dic.Add(ReaBmsCheckResult.已盘盈.Key, ReaBmsCheckResult.已盘盈.Value);
            dic.Add(ReaBmsCheckResult.已盘亏.Key, ReaBmsCheckResult.已盘亏.Value);
            dic.Add(ReaBmsCheckResult.已盘盈已盘亏.Key, ReaBmsCheckResult.已盘盈已盘亏.Value);
            return dic;
        }
    }
    /// <summary>
    /// 月结类型
    /// </summary>
    public static class ReaBmsQtyMonthBalanceDocType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 按全部 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "按全部", Code = "OfAll", FontColor = "#ffffff", BGColor = "#7dc5eb" });
        public static KeyValuePair<string, BaseClassDicEntity> 按库房 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "按库房", Code = "OfStorage", FontColor = "#ffffff", BGColor = "#be8dbd" });
        public static KeyValuePair<string, BaseClassDicEntity> 按库房货架 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "按库房货架", Code = "OfStorageAndPlace", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsQtyMonthBalanceDocType.按全部.Key, ReaBmsQtyMonthBalanceDocType.按全部.Value);
            dic.Add(ReaBmsQtyMonthBalanceDocType.按库房.Key, ReaBmsQtyMonthBalanceDocType.按库房.Value);
            dic.Add(ReaBmsQtyMonthBalanceDocType.按库房货架.Key, ReaBmsQtyMonthBalanceDocType.按库房货架.Value);
            return dic;
        }
    }
    /// <summary>
    /// 月结统计类型(月结库存货品合并方式)
    /// </summary>
    public static class ReaBmsQtyMonthBalanceDocStatisticalType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 按货品批号供应商 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "按货品+批号+供应商", Code = "OfAll", FontColor = "#ffffff", BGColor = "#7dc5eb" });
        public static KeyValuePair<string, BaseClassDicEntity> 按货品批号供应商库房 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "按货品+批号+供应商+库房", Code = "OfStorage", FontColor = "#ffffff", BGColor = "#be8dbd" });
        public static KeyValuePair<string, BaseClassDicEntity> 按货品批号供应商库房货架 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "按货品+批号+供应商+库房+货架", Code = "OfStorageAndPlace", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsQtyMonthBalanceDocStatisticalType.按货品批号供应商.Key, ReaBmsQtyMonthBalanceDocStatisticalType.按货品批号供应商.Value);
            dic.Add(ReaBmsQtyMonthBalanceDocStatisticalType.按货品批号供应商库房.Key, ReaBmsQtyMonthBalanceDocStatisticalType.按货品批号供应商库房.Value);
            dic.Add(ReaBmsQtyMonthBalanceDocStatisticalType.按货品批号供应商库房货架.Key, ReaBmsQtyMonthBalanceDocStatisticalType.按货品批号供应商库房货架.Value);
            return dic;
        }
    }
    public static class ReaGoodsBarcodeOperationSerialType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 条码生成 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "条码生成", Code = "Create", FontColor = "#ffffff", BGColor = "#7dc5eb" });
        public static KeyValuePair<string, BaseClassDicEntity> 货品扫码 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "货品扫码", Code = "GoodsScanCode", FontColor = "#ffffff", BGColor = "#be8dbd" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaGoodsBarcodeOperationSerialType.条码生成.Key, ReaGoodsBarcodeOperationSerialType.条码生成.Value);
            dic.Add(ReaGoodsBarcodeOperationSerialType.货品扫码.Key, ReaGoodsBarcodeOperationSerialType.货品扫码.Value);
            return dic;
        }
    }
    /// <summary>
    /// 模板/报表类型
    /// </summary>
    public static class ReaReportClass
    {
        public static KeyValuePair<string, BaseClassDicEntity> Frx = new KeyValuePair<string, BaseClassDicEntity>("Frx", new BaseClassDicEntity() { Id = "Frx", Name = "Frx", Code = "Frx", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> Excel = new KeyValuePair<string, BaseClassDicEntity>("Excel", new BaseClassDicEntity() { Id = "Excel", Name = "Excel", Code = "Excel", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaReportClass.Frx.Key, ReaReportClass.Frx.Value);
            dic.Add(ReaReportClass.Excel.Key, ReaReportClass.Excel.Value);
            return dic;
        }
    }
    /// <summary>
    /// 报表模板类型信息
    /// </summary>
    public static class BTemplateType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 采购申请 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "采购申请", Code = "ReaBmsReqDoc", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 订货清单 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "订货清单", Code = "ReaBmsCenOrderDoc", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 供货清单 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "供货清单", Code = "ReaBmsCenSaleDoc", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 验收清单 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "验收清单", Code = "ReaBmsCenSaleDocConfirm", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 入库清单 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "入库清单", Code = "ReaBmsInDoc", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 库存清单 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "库存清单", Code = "ReaBmsQtyDtl", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 出库清单 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "出库清单", Code = "ReaBmsOutDoc", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 移库清单 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "移库清单", Code = "ReaBmsTransferDoc", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 盘库清单 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "盘库清单", Code = "ReaBmsCheckDoc", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 月结清单 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "月结清单", Code = "ReaBmsQtyMonthBalanceDoc", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 库存预警 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "库存预警", Code = "StockWarning", FontColor = "#ffffff", BGColor = "#e8989a" });
        public static KeyValuePair<string, BaseClassDicEntity> 效期预警 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", Name = "效期预警", Code = "ValidityWarning", FontColor = "#ffffff", BGColor = "#e8989a" });
        public static KeyValuePair<string, BaseClassDicEntity> 库存结转报表 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", Name = "库存结转报表", Code = "ReaBmsQtyMonthBalanceDoc", FontColor = "#ffffff", BGColor = "#e8989a" });
        public static KeyValuePair<string, BaseClassDicEntity> 出库使用量 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", Name = "出库使用量", Code = "ReaMonthUsageStatisticsDoc", FontColor = "#ffffff", BGColor = "#e8989a" });
        public static KeyValuePair<string, BaseClassDicEntity> 订单汇总 = new KeyValuePair<string, BaseClassDicEntity>("15", new BaseClassDicEntity() { Id = "15", Name = "订单汇总", Code = "ReaBmsCenOrderDtl", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 入库汇总 = new KeyValuePair<string, BaseClassDicEntity>("16", new BaseClassDicEntity() { Id = "16", Name = "入库汇总", Code = "ReaBmsInDtl", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 移库汇总 = new KeyValuePair<string, BaseClassDicEntity>("17", new BaseClassDicEntity() { Id = "17", Name = "移库汇总", Code = "ReaBmsInDtl", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 出库汇总 = new KeyValuePair<string, BaseClassDicEntity>("18", new BaseClassDicEntity() { Id = "18", Name = "出库汇总", Code = "ReaBmsOutDtl", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 综合统计 = new KeyValuePair<string, BaseClassDicEntity>("19", new BaseClassDicEntity() { Id = "19", Name = "综合统计", Code = "ComprehensiveStatistics", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 移库及使用 = new KeyValuePair<string, BaseClassDicEntity>("20", new BaseClassDicEntity() { Id = "20", Name = "移库及使用", Code = "ReaBmsInDtl", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 批号性能验证清单 = new KeyValuePair<string, BaseClassDicEntity>("21", new BaseClassDicEntity() { Id = "21", Name = "批号性能验证清单", Code = "ReaGoodsLot", FontColor = "#ffffff", BGColor = "#17abe3" });

        public static KeyValuePair<string, BaseClassDicEntity> 出库变更台账 = new KeyValuePair<string, BaseClassDicEntity>("22", new BaseClassDicEntity() { Id = "22", Name = "出库变更台账", Code = "ReaBmsOutDtl", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(BTemplateType.采购申请.Key, BTemplateType.采购申请.Value);
            dic.Add(BTemplateType.订货清单.Key, BTemplateType.订货清单.Value);
            dic.Add(BTemplateType.供货清单.Key, BTemplateType.供货清单.Value);
            dic.Add(BTemplateType.验收清单.Key, BTemplateType.验收清单.Value);
            dic.Add(BTemplateType.入库清单.Key, BTemplateType.入库清单.Value);

            dic.Add(BTemplateType.库存清单.Key, BTemplateType.库存清单.Value);
            dic.Add(BTemplateType.出库清单.Key, BTemplateType.出库清单.Value);
            dic.Add(BTemplateType.移库清单.Key, BTemplateType.移库清单.Value);
            dic.Add(BTemplateType.盘库清单.Key, BTemplateType.盘库清单.Value);
            dic.Add(BTemplateType.月结清单.Key, BTemplateType.月结清单.Value);

            dic.Add(BTemplateType.库存预警.Key, BTemplateType.库存预警.Value);
            dic.Add(BTemplateType.效期预警.Key, BTemplateType.效期预警.Value);
            dic.Add(BTemplateType.库存结转报表.Key, BTemplateType.库存结转报表.Value);
            dic.Add(BTemplateType.出库使用量.Key, BTemplateType.出库使用量.Value);
            dic.Add(BTemplateType.订单汇总.Key, BTemplateType.订单汇总.Value);

            dic.Add(BTemplateType.入库汇总.Key, BTemplateType.入库汇总.Value);
            dic.Add(BTemplateType.移库汇总.Key, BTemplateType.移库汇总.Value);
            dic.Add(BTemplateType.出库汇总.Key, BTemplateType.出库汇总.Value);
            dic.Add(BTemplateType.综合统计.Key, BTemplateType.综合统计.Value);
            dic.Add(BTemplateType.移库及使用.Key, BTemplateType.移库及使用.Value);
            dic.Add(BTemplateType.批号性能验证清单.Key, BTemplateType.批号性能验证清单.Value);

            dic.Add(BTemplateType.出库变更台账.Key, BTemplateType.出库变更台账.Value);

            return dic;
        }
    }
    /// <summary>
    /// 报表管理类型信息
    /// </summary>
    public static class BReportType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 采购申请 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "采购申请", Code = "ReaBmsReqDoc", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 订货清单 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "订货清单", Code = "ReaBmsCenOrderDoc", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 供货清单 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "供货清单", Code = "ReaBmsCenSaleDoc", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 验收清单 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "验收清单", Code = "ReaBmsCenSaleDocConfirm", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 入库清单 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "入库清单", Code = "ReaBmsInDoc", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 库存清单 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "库存清单", Code = "ReaBmsQtyDtl", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 出库清单 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "出库清单", Code = "ReaBmsOutDoc", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 移库清单 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "移库清单", Code = "ReaBmsTransferDoc", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 盘库清单 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "盘库清单", Code = "ReaBmsCheckDoc", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 月结清单 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "月结清单", Code = "ReaBmsQtyMonthBalanceDoc", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 库存预警 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "库存预警", Code = "StockWarning", FontColor = "#ffffff", BGColor = "#e8989a" });
        public static KeyValuePair<string, BaseClassDicEntity> 效期预警 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", Name = "效期预警", Code = "ValidityWarning", FontColor = "#ffffff", BGColor = "#e8989a" });
        public static KeyValuePair<string, BaseClassDicEntity> 库存结转报表 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", Name = "库存结转报表", Code = "ReaBmsQtyMonthBalanceDoc", FontColor = "#ffffff", BGColor = "#e8989a" });
        public static KeyValuePair<string, BaseClassDicEntity> 出库使用量 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", Name = "出库使用量", Code = "ReaMonthUsageStatisticsDoc", FontColor = "#ffffff", BGColor = "#e8989a" });
        public static KeyValuePair<string, BaseClassDicEntity> 订单汇总 = new KeyValuePair<string, BaseClassDicEntity>("15", new BaseClassDicEntity() { Id = "15", Name = "订单汇总", Code = "ReaBmsCenOrderDtl", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 入库汇总 = new KeyValuePair<string, BaseClassDicEntity>("16", new BaseClassDicEntity() { Id = "16", Name = "入库汇总", Code = "ReaBmsInDtl", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 移库汇总 = new KeyValuePair<string, BaseClassDicEntity>("17", new BaseClassDicEntity() { Id = "17", Name = "移库汇总", Code = "ReaBmsInDtl", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 出库汇总 = new KeyValuePair<string, BaseClassDicEntity>("18", new BaseClassDicEntity() { Id = "18", Name = "出库汇总", Code = "ReaBmsOutDtl", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 综合统计 = new KeyValuePair<string, BaseClassDicEntity>("19", new BaseClassDicEntity() { Id = "19", Name = "综合统计", Code = "ComprehensiveStatistics", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(BReportType.采购申请.Key, BReportType.采购申请.Value);
            dic.Add(BReportType.订货清单.Key, BReportType.订货清单.Value);
            dic.Add(BReportType.供货清单.Key, BReportType.供货清单.Value);
            dic.Add(BReportType.验收清单.Key, BReportType.验收清单.Value);
            dic.Add(BReportType.入库清单.Key, BReportType.入库清单.Value);

            dic.Add(BReportType.库存清单.Key, BReportType.库存清单.Value);
            dic.Add(BReportType.出库清单.Key, BReportType.出库清单.Value);
            dic.Add(BReportType.移库清单.Key, BReportType.移库清单.Value);
            dic.Add(BReportType.盘库清单.Key, BReportType.盘库清单.Value);
            dic.Add(BReportType.月结清单.Key, BReportType.月结清单.Value);

            dic.Add(BReportType.库存预警.Key, BReportType.库存预警.Value);
            dic.Add(BReportType.效期预警.Key, BReportType.效期预警.Value);
            dic.Add(BReportType.库存结转报表.Key, BReportType.库存结转报表.Value);
            dic.Add(BReportType.出库使用量.Key, BReportType.出库使用量.Value);
            dic.Add(BReportType.订单汇总.Key, BReportType.订单汇总.Value);

            dic.Add(BReportType.入库汇总.Key, BReportType.入库汇总.Value);
            dic.Add(BReportType.移库汇总.Key, BReportType.移库汇总.Value);
            dic.Add(BReportType.出库汇总.Key, BReportType.出库汇总.Value);
            dic.Add(BReportType.综合统计.Key, BReportType.综合统计.Value);
            return dic;
        }
    }
    /// <summary>
    /// 报表管理信息状态
    /// </summary>
    public static class BReportStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 报表生成 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "报表生成", Code = "Create", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 审核通过 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "审核通过", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 审核退回 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "审核退回", Code = "UnReview", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 已发布 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "已发布", Code = "Applyed", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 报表作废 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "报表作废", Code = "Invalid", FontColor = "#ffffff", BGColor = "red" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(BReportStatus.报表生成.Key, BReportStatus.报表生成.Value);
            dic.Add(BReportStatus.审核通过.Key, BReportStatus.审核通过.Value);
            dic.Add(BReportStatus.审核通过.Key, BReportStatus.审核通过.Value);
            dic.Add(BReportStatus.审核退回.Key, BReportStatus.审核退回.Value);
            dic.Add(BReportStatus.已发布.Key, BReportStatus.已发布.Value);
            dic.Add(BReportStatus.报表作废.Key, BReportStatus.报表作废.Value);
            return dic;
        }
    }
    /// <summary>
    /// 机构操作记录类型
    /// </summary>
    public static class SServiceClientOperation
    {
        public static KeyValuePair<string, BaseClassDicEntity> 机构注册 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "机构注册", Code = "Registered", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 授权变更 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "授权变更", Code = "Ruthorization", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 审核通过 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "审核通过", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 审核退回 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "审核退回", Code = "UnReview", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 正式开通 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "正式开通", Code = "Activated", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 授权导出 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "授权导出", Code = "Activated", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 授权导入 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "授权导入", Code = "Activated", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(SServiceClientOperation.机构注册.Key, SServiceClientOperation.机构注册.Value);
            dic.Add(SServiceClientOperation.授权变更.Key, SServiceClientOperation.授权变更.Value);
            dic.Add(SServiceClientOperation.审核通过.Key, SServiceClientOperation.审核通过.Value);
            dic.Add(SServiceClientOperation.审核退回.Key, SServiceClientOperation.审核退回.Value);
            dic.Add(SServiceClientOperation.正式开通.Key, SServiceClientOperation.正式开通.Value);
            dic.Add(SServiceClientOperation.授权导出.Key, SServiceClientOperation.授权导出.Value);
            dic.Add(SServiceClientOperation.授权导入.Key, SServiceClientOperation.授权导入.Value);
            return dic;
        }
    }
    /// <summary>
    /// 条码生成类型
    /// </summary>
    public static class ReaBmsSerialType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 供货条码 = new KeyValuePair<string, BaseClassDicEntity>("ReaBmsCenSaleDtl", new BaseClassDicEntity() { Id = "ReaBmsCenSaleDtl", Name = "供货条码", Code = "ReaBmsCenSaleDtl", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 库存条码 = new KeyValuePair<string, BaseClassDicEntity>("ReaBmsQtyDtl", new BaseClassDicEntity() { Id = "ReaBmsQtyDtl", Name = "库存条码", Code = "ReaBmsQtyDtl", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsSerialType.供货条码.Key, ReaBmsSerialType.供货条码.Value);
            dic.Add(ReaBmsSerialType.库存条码.Key, ReaBmsSerialType.库存条码.Value);
            return dic;
        }
    }
    /// <summary>
    /// 业务接口类型
    /// </summary>
    public static class ReaBusinessInterfaceType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 试剂平台 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "试剂平台", Code = "ReagentPlatform", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 物资接口 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "物资接口", Code = "Material", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 供应商 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "供应商", Code = "ReaComp", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 第三方系统 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "第三方系统", Code = "Others", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBusinessInterfaceType.试剂平台.Key, ReaBusinessInterfaceType.试剂平台.Value);
            dic.Add(ReaBusinessInterfaceType.物资接口.Key, ReaBusinessInterfaceType.物资接口.Value);
            dic.Add(ReaBusinessInterfaceType.供应商.Key, ReaBusinessInterfaceType.供应商.Value);
            dic.Add(ReaBusinessInterfaceType.第三方系统.Key, ReaBusinessInterfaceType.第三方系统.Value);
            return dic;
        }
    }
    /// <summary>
    /// 业务接口的业务类型
    /// </summary>
    public static class ReaBusinessType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 订单 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "订单", Code = "OrderDoc", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 供货 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "供货", Code = "SaleDoc", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 验收 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "验收", Code = "Confirm", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 入库 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "入库", Code = "In", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 出库 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "出库", Code = "Out", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBusinessType.订单.Key, ReaBusinessType.订单.Value);
            dic.Add(ReaBusinessType.供货.Key, ReaBusinessType.供货.Value);
            dic.Add(ReaBusinessType.验收.Key, ReaBusinessType.验收.Value);
            dic.Add(ReaBusinessType.入库.Key, ReaBusinessType.入库.Value);
            dic.Add(ReaBusinessType.出库.Key, ReaBusinessType.出库.Value);
            return dic;
        }
    }
    /// <summary>
    /// 机构类型
    /// </summary>
    public static class OrgType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 供应商 = new KeyValuePair<string, BaseClassDicEntity>("5068778579665143965", new BaseClassDicEntity() { Id = "5068778579665143965", Name = "供应商", Code = "Comp", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 实验室 = new KeyValuePair<string, BaseClassDicEntity>("5070458560961965845", new BaseClassDicEntity() { Id = "5070458560961965845", Name = "实验室", Code = "Lab", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 厂商 = new KeyValuePair<string, BaseClassDicEntity>("5095132324043854891", new BaseClassDicEntity() { Id = "5095132324043854891", Name = "厂商", Code = "Fact", FontColor = "#ffffff", BGColor = "#1195db" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(OrgType.供应商.Key, OrgType.供应商.Value);
            dic.Add(OrgType.实验室.Key, OrgType.实验室.Value);
            dic.Add(OrgType.厂商.Key, OrgType.厂商.Value);
            return dic;
        }
    }
    /// <summary>
    /// 订单明细合并选择项
    /// </summary>
    public static class ReaBmsCenOrderDtlGroupType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 按部门供应商货品单位规格 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "按部门供应商货品单位规格", Code = "GroupType1", FontColor = "#ffffff", BGColor = "#e8989a" });
        public static KeyValuePair<string, BaseClassDicEntity> 按订货品明细 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "按订货品明细", Code = "OfDtl", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsCenOrderDtlGroupType.按部门供应商货品单位规格.Key, ReaBmsCenOrderDtlGroupType.按部门供应商货品单位规格.Value);
            dic.Add(ReaBmsCenOrderDtlGroupType.按订货品明细.Key, ReaBmsCenOrderDtlGroupType.按订货品明细.Value);
            return dic;
        }
    }
    /// <summary>
    /// 库存试剂合并选择项
    /// </summary>
    public static class ReaBmsStatisticalType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 按货品规格 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "按货品规格", Code = "1", FontColor = "#ffffff", BGColor = "#e8989a" });
        public static KeyValuePair<string, BaseClassDicEntity> 按货品批号 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "按货品批号", Code = "2", FontColor = "#ffffff", BGColor = "#e8989a" });
        public static KeyValuePair<string, BaseClassDicEntity> 按供应商货品 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "按供应商货品", Code = "3", FontColor = "#ffffff", BGColor = "#e8989a" });
        public static KeyValuePair<string, BaseClassDicEntity> 按供应商货品批号 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "按供应商货品批号", Code = "4", FontColor = "#ffffff", BGColor = "#e8989a" });

        public static KeyValuePair<string, BaseClassDicEntity> 按库房货品批号 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "按库房货品批号", Code = "5", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 按库房货架货品批号 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "按库房货架货品批号", Code = "6", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 按库房机构货品 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "按库房机构货品", Code = "7", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 按库房货架机构货品 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "按库房货架机构货品", Code = "8", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 按库房供应商货品批号 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "按库房供应商货品批号", Code = "ReaBmsCenSaleDoc", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 按库房货架供应商货品批号 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "按库房货架供应商货品批号", Code = "ReaBmsCenSaleDoc", FontColor = "#ffffff", BGColor = "#17abe3" });

        public static KeyValuePair<string, BaseClassDicEntity> 按库存记录 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "按库存记录", Code = "ReaBmsCenSaleDoc", FontColor = "#ffffff", BGColor = "#e8989a" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsStatisticalType.按货品规格.Key, ReaBmsStatisticalType.按货品规格.Value);
            dic.Add(ReaBmsStatisticalType.按货品批号.Key, ReaBmsStatisticalType.按货品批号.Value);

            dic.Add(ReaBmsStatisticalType.按库房货品批号.Key, ReaBmsStatisticalType.按库房货品批号.Value);
            dic.Add(ReaBmsStatisticalType.按库房机构货品.Key, ReaBmsStatisticalType.按库房机构货品.Value);
            dic.Add(ReaBmsStatisticalType.按库房货架机构货品.Key, ReaBmsStatisticalType.按库房货架机构货品.Value);
            dic.Add(ReaBmsStatisticalType.按库房货架货品批号.Key, ReaBmsStatisticalType.按库房货架货品批号.Value);
            dic.Add(ReaBmsStatisticalType.按库房供应商货品批号.Key, ReaBmsStatisticalType.按库房供应商货品批号.Value);
            dic.Add(ReaBmsStatisticalType.按库房货架供应商货品批号.Key, ReaBmsStatisticalType.按库房货架供应商货品批号.Value);

            dic.Add(ReaBmsStatisticalType.按供应商货品.Key, ReaBmsStatisticalType.按供应商货品.Value);
            dic.Add(ReaBmsStatisticalType.按供应商货品批号.Key, ReaBmsStatisticalType.按供应商货品批号.Value);
            dic.Add(ReaBmsStatisticalType.按库存记录.Key, ReaBmsStatisticalType.按库存记录.Value);
            return dic;
        }
    }
    /// <summary>
    /// 预警分类
    /// </summary>
    public static class AlertType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 低库存预警 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "低库存预警", Code = "Create", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 高库存预警 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "高库存预警", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 库存效期已过期预警 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "库存效期已过期预警", Code = "UnReview", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 库存效期将过期预警 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "库存效期将过期预警", Code = "Applyed", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 注册证已过期预警 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "注册证已过期预警", Code = "Invalid", FontColor = "#ffffff", BGColor = "red" });
        public static KeyValuePair<string, BaseClassDicEntity> 注册证将过期预警 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "注册证将过期预警", Code = "Invalid", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(AlertType.低库存预警.Key, AlertType.低库存预警.Value);
            dic.Add(AlertType.高库存预警.Key, AlertType.高库存预警.Value);
            dic.Add(AlertType.库存效期已过期预警.Key, AlertType.库存效期已过期预警.Value);
            dic.Add(AlertType.库存效期将过期预警.Key, AlertType.库存效期将过期预警.Value);
            dic.Add(AlertType.注册证已过期预警.Key, AlertType.注册证已过期预警.Value);
            dic.Add(AlertType.注册证将过期预警.Key, AlertType.注册证将过期预警.Value);
            return dic;
        }
    }
    /// <summary>
    /// LIS检测类型
    /// </summary>
    public static class LisTestType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 常规 = new KeyValuePair<string, BaseClassDicEntity>("Common", new BaseClassDicEntity() { Id = "Common", Name = "常规", Code = "Common", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 复检 = new KeyValuePair<string, BaseClassDicEntity>("Review", new BaseClassDicEntity() { Id = "Review", Name = "复检", Code = "Review", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 质控 = new KeyValuePair<string, BaseClassDicEntity>("QCData", new BaseClassDicEntity() { Id = "QCData", Name = "质控", Code = "QCData", FontColor = "#ffffff", BGColor = "#1195db" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(LisTestType.常规.Key, LisTestType.常规.Value);
            dic.Add(LisTestType.复检.Key, LisTestType.复检.Value);
            dic.Add(LisTestType.质控.Key, LisTestType.质控.Value);
            return dic;
        }
    }
    /// <summary>
    /// 入库对帐标志
    /// </summary>
    public static class ReaBmsInDocReconciliationMark
    {
        public static KeyValuePair<string, BaseClassDicEntity> 未对帐 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "未对帐", Code = "NoReconciliation", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 已对帐 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "已对帐", Code = "HasReconciliation", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 部分对帐 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "部分对帐", Code = "PartReconciliation", FontColor = "#ffffff", BGColor = "#1c8f36" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsInDocReconciliationMark.未对帐.Key, ReaBmsInDocReconciliationMark.未对帐.Value);
            dic.Add(ReaBmsInDocReconciliationMark.已对帐.Key, ReaBmsInDocReconciliationMark.已对帐.Value);
            //dic.Add(ReaBmsInDocReconciliationMark.部分对帐.Key, ReaBmsInDocReconciliationMark.部分对帐.Value);

            return dic;
        }
    }
    /// <summary>
    /// 入库对帐锁定
    /// </summary>
    public static class ReaBmsInDocLockMark
    {
        public static KeyValuePair<string, BaseClassDicEntity> 未锁定 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "未锁定", Code = "NoLockMark", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 已锁定 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "已锁定", Code = "HasLockMark", FontColor = "#ffffff", BGColor = "#1195db" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsInDocLockMark.未锁定.Key, ReaBmsInDocLockMark.未锁定.Value);
            dic.Add(ReaBmsInDocLockMark.已锁定.Key, ReaBmsInDocLockMark.已锁定.Value);

            return dic;
        }
    }
    /// <summary>
    /// 机构货品操作类型
    /// </summary>
    public static class ReaGoodsOperation
    {
        public static KeyValuePair<string, BaseClassDicEntity> 机构货品修改记录 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "机构货品修改记录", Code = "Edit", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaGoodsOperation.机构货品修改记录.Key, ReaGoodsOperation.机构货品修改记录.Value);
            return dic;
        }
    }
    /// <summary>
    /// 客户端移库总单状态
    /// </summary>
    public static class ReaBmsTransferDocStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 暂存 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "暂存", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 申请作废 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "申请作废", Code = "ApplyVoid", FontColor = "#ffffff", BGColor = "red" });
        public static KeyValuePair<string, BaseClassDicEntity> 已申请 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "已申请", Code = "Applyed", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 审核通过 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "审核通过", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 审核退回 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "审核退回", Code = "UnReview", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 移库完成 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "移库完成", Code = "TransferDoced", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsTransferDocStatus.暂存.Key, ReaBmsTransferDocStatus.暂存.Value);
            dic.Add(ReaBmsTransferDocStatus.申请作废.Key, ReaBmsTransferDocStatus.申请作废.Value);
            dic.Add(ReaBmsTransferDocStatus.已申请.Key, ReaBmsTransferDocStatus.已申请.Value);
            dic.Add(ReaBmsTransferDocStatus.审核通过.Key, ReaBmsTransferDocStatus.审核通过.Value);
            dic.Add(ReaBmsTransferDocStatus.审核退回.Key, ReaBmsTransferDocStatus.审核退回.Value);
            dic.Add(ReaBmsTransferDocStatus.移库完成.Key, ReaBmsTransferDocStatus.移库完成.Value);
            return dic;
        }
    }
    /// <summary>
    /// 客户端出库类型
    /// </summary>
    public static class ReaBmsOutDocOutType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 使用出库 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "使用出库", Code = "UseTheOut", FontColor = "#ffffff", BGColor = "#7dc5eb" });
        public static KeyValuePair<string, BaseClassDicEntity> 退库入库 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "退库入库", Code = "RefundTheOut", FontColor = "#ffffff", BGColor = "#be8dbd" });
        public static KeyValuePair<string, BaseClassDicEntity> 报损出库 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "报损出库", Code = "LosstheOut", FontColor = "#ffffff", BGColor = "#e8989a" });
        public static KeyValuePair<string, BaseClassDicEntity> 退供应商 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "退供应商", Code = "RetreatSuppliers", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 调账出库 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "调账出库", Code = "AdjustmentOut", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 盘亏出库 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "盘亏出库", Code = "DiskLoss", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsOutDocOutType.使用出库.Key, ReaBmsOutDocOutType.使用出库.Value);
            dic.Add(ReaBmsOutDocOutType.退库入库.Key, ReaBmsOutDocOutType.退库入库.Value);
            dic.Add(ReaBmsOutDocOutType.报损出库.Key, ReaBmsOutDocOutType.报损出库.Value);
            dic.Add(ReaBmsOutDocOutType.退供应商.Key, ReaBmsOutDocOutType.退供应商.Value);
            dic.Add(ReaBmsOutDocOutType.调账出库.Key, ReaBmsOutDocOutType.调账出库.Value);
            dic.Add(ReaBmsOutDocOutType.盘亏出库.Key, ReaBmsOutDocOutType.盘亏出库.Value);
            return dic;
        }
    }
    /// <summary>
    /// 客户端出库总单状态
    /// </summary>
    public static class ReaBmsOutDocStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 暂存 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "暂存", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 申请作废 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "申请作废", Code = "ApplyVoid", FontColor = "#ffffff", BGColor = "red" });
        public static KeyValuePair<string, BaseClassDicEntity> 已申请 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "已申请", Code = "Applyed", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 审核通过 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "审核通过", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 审核退回 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "审核退回", Code = "UnReview", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 审批通过 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "审批通过", Code = "FinanceCheck", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 审批退回 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "审批退回", Code = "UnFinanceCheck", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 出库完成 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "出库完成", Code = "OutDoced", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 出库单上传平台 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "出库单上传平台", Code = "OutDocUpload", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 供应商确认 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "供应商确认", Code = "SupplierConfirm", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 取消确认 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "取消确认", Code = "SupplierCancelConfirm", FontColor = "#ffffff", BGColor = "#dd6572" });


        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsOutDocStatus.暂存.Key, ReaBmsOutDocStatus.暂存.Value);
            dic.Add(ReaBmsOutDocStatus.申请作废.Key, ReaBmsOutDocStatus.申请作废.Value);
            dic.Add(ReaBmsOutDocStatus.已申请.Key, ReaBmsOutDocStatus.已申请.Value);
            dic.Add(ReaBmsOutDocStatus.审核通过.Key, ReaBmsOutDocStatus.审核通过.Value);
            dic.Add(ReaBmsOutDocStatus.审核退回.Key, ReaBmsOutDocStatus.审核退回.Value);
            dic.Add(ReaBmsOutDocStatus.审批通过.Key, ReaBmsOutDocStatus.审批通过.Value);
            dic.Add(ReaBmsOutDocStatus.审批退回.Key, ReaBmsOutDocStatus.审批退回.Value);
            dic.Add(ReaBmsOutDocStatus.出库完成.Key, ReaBmsOutDocStatus.出库完成.Value);
            dic.Add(ReaBmsOutDocStatus.出库单上传平台.Key, ReaBmsOutDocStatus.出库单上传平台.Value);
            dic.Add(ReaBmsOutDocStatus.供应商确认.Key, ReaBmsOutDocStatus.供应商确认.Value);
            dic.Add(ReaBmsOutDocStatus.取消确认.Key, ReaBmsOutDocStatus.取消确认.Value);
            return dic;
        }
    }
    /// <summary>
    /// 出库单单第三方接口标志
    /// </summary>
    public static class ReaBmsOutDocThirdFlag
    {
        public static KeyValuePair<string, BaseClassDicEntity> 未同步 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "未同步", Code = "NotExtracted", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 同步成功 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "同步成功", Code = "lUpload", FontColor = "#ffffff", BGColor = "#7dc5eb" });
        public static KeyValuePair<string, BaseClassDicEntity> 同步失败 = new KeyValuePair<string, BaseClassDicEntity>("-1", new BaseClassDicEntity() { Id = "-1", Name = "同步失败", Code = "CancelUpload", FontColor = "#ffffff", BGColor = "#e98f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsOutDocThirdFlag.未同步.Key, ReaBmsOutDocThirdFlag.未同步.Value);
            dic.Add(ReaBmsOutDocThirdFlag.同步成功.Key, ReaBmsOutDocThirdFlag.同步成功.Value);
            dic.Add(ReaBmsOutDocThirdFlag.同步失败.Key, ReaBmsOutDocThirdFlag.同步失败.Value);
            return dic;
        }
    }
    /// <summary>
    /// 出库使用量统计类型
    /// </summary>
    public static class ReaMonthUsageStatisticsDocType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 按使用量 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "按使用量", Code = "Usage", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 按使用部门 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "按使用部门", Code = "Dept", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaMonthUsageStatisticsDocType.按使用量.Key, ReaMonthUsageStatisticsDocType.按使用量.Value);
            dic.Add(ReaMonthUsageStatisticsDocType.按使用部门.Key, ReaMonthUsageStatisticsDocType.按使用部门.Value);
            return dic;
        }
    }
    /// <summary>
    /// 出库使用量统计周期类型
    /// </summary>
    public static class ReaMonthUsageStatisticsDocRoundType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 按自然月 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "按自然月", Code = "Month", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 按日期范围 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "按日期范围", Code = "DateArea", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaMonthUsageStatisticsDocRoundType.按自然月.Key, ReaMonthUsageStatisticsDocRoundType.按自然月.Value);
            dic.Add(ReaMonthUsageStatisticsDocRoundType.按日期范围.Key, ReaMonthUsageStatisticsDocRoundType.按日期范围.Value);
            return dic;
        }
    }
    /// <summary>
    /// 库存量预警比较值类型
    /// </summary>
    public static class QtyWarningComparisonValueType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 库存预设值 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "库存预设值", Code = "DefaultValue", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 理论月用量 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "理论月用量", Code = "TheoreticalMonthlyUsage", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 上月使用量 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "上月使用量", Code = "LastMonthAverageUsage", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 月用量最大值 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "月用量最大值", Code = "MaxMonthAverageUsage", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 月均使用量 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "月均使用量", Code = "AllMonthAverageUsage", FontColor = "#ffffff", BGColor = "#1c8f36" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(QtyWarningComparisonValueType.库存预设值.Key, QtyWarningComparisonValueType.库存预设值.Value);
            dic.Add(QtyWarningComparisonValueType.理论月用量.Key, QtyWarningComparisonValueType.理论月用量.Value);
            dic.Add(QtyWarningComparisonValueType.上月使用量.Key, QtyWarningComparisonValueType.上月使用量.Value);
            dic.Add(QtyWarningComparisonValueType.月用量最大值.Key, QtyWarningComparisonValueType.月用量最大值.Value);
            dic.Add(QtyWarningComparisonValueType.月均使用量.Key, QtyWarningComparisonValueType.月均使用量.Value);
            return dic;
        }
    }
    /// <summary>
    /// 接口提取数据的转换类型
    /// </summary>
    public static class InterfaceDataConvertType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 转供货单 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "转供货单", Code = "ReaBmsCenSaleDoc", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 转验收单 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "转验收单", Code = "ReaBmsCenSaleDocConfirm", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 转入库单 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "转入库单", Code = "ReaBmsInDoc", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(InterfaceDataConvertType.转供货单.Key, InterfaceDataConvertType.转供货单.Value);
            dic.Add(InterfaceDataConvertType.转验收单.Key, InterfaceDataConvertType.转验收单.Value);
            dic.Add(InterfaceDataConvertType.转入库单.Key, InterfaceDataConvertType.转入库单.Value);
            return dic;
        }
    }
    /// <summary>
    /// 货品批号的验证结果状态
    /// </summary>
    public static class ReaGoodsLotVerificationStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 未验证 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "未验证", Code = "NotVerification", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 验证通过 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "验证通过", Code = "Pass", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 验证不通过 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "验证不通过", Code = "NotPass", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaGoodsLotVerificationStatus.未验证.Key, ReaGoodsLotVerificationStatus.未验证.Value);
            dic.Add(ReaGoodsLotVerificationStatus.验证通过.Key, ReaGoodsLotVerificationStatus.验证通过.Value);
            dic.Add(ReaGoodsLotVerificationStatus.验证不通过.Key, ReaGoodsLotVerificationStatus.验证不通过.Value);
            return dic;
        }
    }
    /// <summary>
    /// 用户UI配置的各UI类型
    /// </summary>
    public static class UserUIConfigUIType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 列表配置 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "列表配置", Code = "DefaultGridPanel", FontColor = "#ffffff", BGColor = "#5cb85c" });//包括列表默认分页数,列配置,列排序
        public static KeyValuePair<string, BaseClassDicEntity> 列表默认分页数 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "列表默认分页数", Code = "DefaultPageSize", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 列表列配置 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "列表列配置", Code = "ColumnsConfig", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 列表列排序 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "列表列排序", Code = "DefaultOrderBy", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(UserUIConfigUIType.列表配置.Key, UserUIConfigUIType.列表配置.Value);
            dic.Add(UserUIConfigUIType.列表默认分页数.Key, UserUIConfigUIType.列表默认分页数.Value);
            dic.Add(UserUIConfigUIType.列表列配置.Key, UserUIConfigUIType.列表列配置.Value);
            dic.Add(UserUIConfigUIType.列表列排序.Key, UserUIConfigUIType.列表列排序.Value);
            return dic;
        }
    }
    /// <summary>
    /// 库房货架人员权限的关系类型
    /// </summary>
    public static class ReaUserStorageLinkOperType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 库房管理权限 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "库房管理权限", Code = "OfManage", FontColor = "#ffffff", BGColor = "#7dc5eb" });
        public static KeyValuePair<string, BaseClassDicEntity> 移库申请源库房 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "移库申请源库房", Code = "OfTransferApply", FontColor = "#ffffff", BGColor = "#be8dbd" });
        public static KeyValuePair<string, BaseClassDicEntity> 出库申请权限 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "出库申请权限", Code = "OfOutApply", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 直接移库源库房 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "直接移库源库房", Code = "OfDirectTransfer", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaUserStorageLinkOperType.库房管理权限.Key, ReaUserStorageLinkOperType.库房管理权限.Value);
            dic.Add(ReaUserStorageLinkOperType.移库申请源库房.Key, ReaUserStorageLinkOperType.移库申请源库房.Value);
            dic.Add(ReaUserStorageLinkOperType.直接移库源库房.Key, ReaUserStorageLinkOperType.直接移库源库房.Value);
            dic.Add(ReaUserStorageLinkOperType.出库申请权限.Key, ReaUserStorageLinkOperType.出库申请权限.Value);
            return dic;
        }
    }

    /// <summary>
    /// 库存标志
    /// </summary>
    public static class ReaBmsQtyDtlMark
    {
        public static KeyValuePair<string, BaseClassDicEntity> 无库存标志 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "无库存标志", Code = "NoMark", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 过滤库存数量为0且不是这个库房的货品 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "过滤库存数量为0且不是这个库房的货品", Code = "Mark1", FontColor = "#ffffff", BGColor = "#FF5722" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsQtyDtlMark.无库存标志.Key, ReaBmsQtyDtlMark.无库存标志.Value);
            dic.Add(ReaBmsQtyDtlMark.过滤库存数量为0且不是这个库房的货品.Key, ReaBmsQtyDtlMark.过滤库存数量为0且不是这个库房的货品.Value);

            return dic;
        }
    }
    /// <summary>
    /// 出库明细报表合并选择项
    /// </summary>
    public static class ReaBmsOutDtlStatisticalType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 按货品规格 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "按货品规格", Code = "1", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 按供应商加批号及货品 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "按供应商加批号及货品", Code = "2", FontColor = "#ffffff", BGColor = "#17abe3" });

        public static KeyValuePair<string, BaseClassDicEntity> 按出库明细常规合并 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", Name = "按出库明细常规合并", Code = "12", FontColor = "#ffffff", BGColor = "#17abe3" });
<<<<<<< .mine
||||||| .r2673
        
=======
        public static KeyValuePair<string, BaseClassDicEntity> 按出库单汇总 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", Name = "按出库单汇总", Code = "13", FontColor = "#ffffff", BGColor = "#17abe3" });
>>>>>>> .r2783


        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsOutDtlStatisticalType.按货品规格.Key, ReaBmsOutDtlStatisticalType.按货品规格.Value);
            dic.Add(ReaBmsOutDtlStatisticalType.按供应商加批号及货品.Key, ReaBmsOutDtlStatisticalType.按供应商加批号及货品.Value);
            dic.Add(ReaBmsOutDtlStatisticalType.按出库明细常规合并.Key, ReaBmsOutDtlStatisticalType.按出库明细常规合并.Value);
            dic.Add(ReaBmsOutDtlStatisticalType.按出库单汇总.Key, ReaBmsOutDtlStatisticalType.按出库单汇总.Value);
            return dic;
        }
    }

    /// <summary>
    /// 入库明细报表合并选择项
    /// </summary>
    public static class ReaBmsInDtlStatisticalType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 按货品规格 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "按货品规格", Code = "1", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 按供应商加批号及货品 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "按供应商加批号及货品", Code = "2", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 按入库明细常规合并 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "按入库明细常规合并", Code = "3", FontColor = "#ffffff", BGColor = "#17abe3" });
<<<<<<< .mine

||||||| .r2673
        
=======
        public static KeyValuePair<string, BaseClassDicEntity> 按入库总单号汇总 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "按入库总单号汇总", Code = "4", FontColor = "#ffffff", BGColor = "#17abe3" });

>>>>>>> .r2783
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsInDtlStatisticalType.按货品规格.Key, ReaBmsInDtlStatisticalType.按货品规格.Value);
            dic.Add(ReaBmsInDtlStatisticalType.按供应商加批号及货品.Key, ReaBmsInDtlStatisticalType.按供应商加批号及货品.Value);
            dic.Add(ReaBmsInDtlStatisticalType.按入库明细常规合并.Key, ReaBmsInDtlStatisticalType.按入库明细常规合并.Value);
            dic.Add(ReaBmsInDtlStatisticalType.按入库总单号汇总.Key, ReaBmsInDtlStatisticalType.按入库总单号汇总.Value);
            return dic;
        }
    }

    /// <summary>
    /// 入库数据接口标志
    /// </summary>
    public static class ReaBmsInDocIOFlag
    {
        public static KeyValuePair<string, BaseClassDicEntity> 未处理 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "未处理", Code = "NotExtracted", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 退库成功 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "退库成功", Code = "lUpload", FontColor = "#ffffff", BGColor = "#7dc5eb" });
        public static KeyValuePair<string, BaseClassDicEntity> 退库失败 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "退库失败", Code = "CancelUpload", FontColor = "#ffffff", BGColor = "#e98f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsInDocIOFlag.未处理.Key, ReaBmsInDocIOFlag.未处理.Value);
            dic.Add(ReaBmsInDocIOFlag.退库成功.Key, ReaBmsInDocIOFlag.退库成功.Value);
            dic.Add(ReaBmsInDocIOFlag.退库失败.Key, ReaBmsInDocIOFlag.退库失败.Value);
            return dic;
        }
    }

    /// <summary>
    /// 系统配置类型
    /// </summary>
    public static class SysConfig
    {
        public static KeyValuePair<string, BaseClassDicEntity> HISSYS = new KeyValuePair<string, BaseClassDicEntity>("HISSYS", new BaseClassDicEntity() { Id = "HISSYS", Name = "HISSYS", Code = "HISSYS", FontColor = "#ffffff", BGColor = "green" });

        public static KeyValuePair<string, BaseClassDicEntity> LISSYS = new KeyValuePair<string, BaseClassDicEntity>("LISSYS", new BaseClassDicEntity() { Id = "LISSYS", Name = "LISSYS", Code = "LISSYS", FontColor = "#ffffff", BGColor = "green" });
        public static KeyValuePair<string, BaseClassDicEntity> GKSYS = new KeyValuePair<string, BaseClassDicEntity>("GKSYS", new BaseClassDicEntity() { Id = "GKSYS", Name = "GKSYS", Code = "GKSYS", FontColor = "#ffffff", BGColor = "orange" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(SysConfig.HISSYS.Key, SysConfig.HISSYS.Value);
            dic.Add(SysConfig.LISSYS.Key, SysConfig.LISSYS.Value);
            dic.Add(SysConfig.GKSYS.Key, SysConfig.GKSYS.Value);

            return dic;
        }
    }

    /// <summary>
    /// 出库数据接口标志
    /// </summary>
    public static class ReaBmsOutDocIOFlag
    {
        public static KeyValuePair<string, BaseClassDicEntity> 未处理 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "未处理", Code = "NotExtracted", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 退库成功 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "退库成功", Code = "lUpload", FontColor = "#ffffff", BGColor = "#7dc5eb" });
        public static KeyValuePair<string, BaseClassDicEntity> 退库失败 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "退库失败", Code = "CancelUpload", FontColor = "#ffffff", BGColor = "#e98f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 已上传 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "已上传", Code = "CancelUpload", FontColor = "#ffffff", BGColor = "#7dc5eb" });
        public static KeyValuePair<string, BaseClassDicEntity> 取消上传 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "取消上传", Code = "CancelUpload", FontColor = "#ffffff", BGColor = "#e98f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 供应商确认 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "供应商确认", Code = "Confirm", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 取消确认 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "取消确认", Code = "UConfirm", FontColor = "#ffffff", BGColor = "#dd6572" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsOutDocIOFlag.未处理.Key, ReaBmsOutDocIOFlag.未处理.Value);
            dic.Add(ReaBmsOutDocIOFlag.退库成功.Key, ReaBmsOutDocIOFlag.退库成功.Value);
            dic.Add(ReaBmsOutDocIOFlag.退库失败.Key, ReaBmsOutDocIOFlag.退库失败.Value);
            dic.Add(ReaBmsOutDocIOFlag.已上传.Key, ReaBmsOutDocIOFlag.已上传.Value);//在客户端和智方试剂平台接口使用
            dic.Add(ReaBmsOutDocIOFlag.取消上传.Key, ReaBmsOutDocIOFlag.取消上传.Value);
            dic.Add(ReaBmsOutDocIOFlag.供应商确认.Key, ReaBmsOutDocIOFlag.供应商确认.Value);
            dic.Add(ReaBmsOutDocIOFlag.取消确认.Key, ReaBmsOutDocIOFlag.取消确认.Value);
            return dic;
        }
    }
    /// <summary>
    /// 统计-订单明细汇总，报表选择项
    /// </summary>
    public static class ReaBmsCenOrderDtlStatisticalType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 订单明细汇总 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "订单明细汇总", Code = "1", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 订单明细 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "订单明细", Code = "2", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 订单号汇总 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "订单号汇总", Code = "3", FontColor = "#ffffff", BGColor = "#17abe3" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsCenOrderDtlStatisticalType.订单明细汇总.Key, ReaBmsCenOrderDtlStatisticalType.订单明细汇总.Value);
            dic.Add(ReaBmsCenOrderDtlStatisticalType.订单明细.Key, ReaBmsCenOrderDtlStatisticalType.订单明细.Value);
            dic.Add(ReaBmsCenOrderDtlStatisticalType.订单号汇总.Key, ReaBmsCenOrderDtlStatisticalType.订单号汇总.Value);
            return dic;
        }
    }

    /// <summary>
    /// 文件物理存储时，做一个处理：在文件名后+（.zf）,用来防止病毒文件挂在服务器直接执行
    /// </summary>
    public enum FileExt
    {
        zf
    }
    #endregion
}