using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Policy;
using System.Collections;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
    /// <summary>
    /// 订货单状态-Status字段值
    /// </summary>
    public enum OrderDocStatus
    {
        无 = 0,
        已提交 = 1,//或已审核
        已确认 = 2,
        已出货 = 3,
        已验收 = 4,
        已删除 = 999,
    }

    /// <summary>
    /// 订货单写入第三方系统状态-IsThirdFlag字段值 
    /// </summary>
    public enum OrderDocIsThirdFlag
    {
        同步失败 = -1,
        无 = 0,
        同步成功 = 1

    }

    /// <summary>
    /// 供货单状态-Status字段值
    /// </summary>
    public enum SaleDocStatus
    {
        无 = 0,
        已验收 = 1,
        已审核 = 2
    }

    /// <summary>
    /// 供货单拆分状态-IsSplit字段值
    /// </summary>
    public enum SaleDocIsSplit
    {
        无 = 0,
        已拆分 = 1
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
    /// 客户端申请总单状态
    /// </summary>
    public static class ReaBmsReqDocStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 暂存 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "暂存", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 已申请 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "已申请", Code = "Applyed", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 审核通过 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "审核通过", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#1195db" });
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
        public static KeyValuePair<string, BaseClassDicEntity> 审核通过 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "审核通过", Code = "Reviewed", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 已上传 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "已上传", Code = "Upload", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 部分验收 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "部分验收", Code = "Accept", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static KeyValuePair<string, BaseClassDicEntity> 全部验收 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "全部验收", Code = "Accept", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaBmsOrderDocStatus.暂存.Key, ReaBmsOrderDocStatus.暂存.Value);
            dic.Add(ReaBmsOrderDocStatus.申请.Key, ReaBmsOrderDocStatus.申请.Value);
            dic.Add(ReaBmsOrderDocStatus.审核退回.Key, ReaBmsOrderDocStatus.审核退回.Value);
            dic.Add(ReaBmsOrderDocStatus.审核通过.Key, ReaBmsOrderDocStatus.审核通过.Value);
            dic.Add(ReaBmsOrderDocStatus.已上传.Key, ReaBmsOrderDocStatus.已上传.Value);
            dic.Add(ReaBmsOrderDocStatus.部分验收.Key, ReaBmsOrderDocStatus.部分验收.Value);
            dic.Add(ReaBmsOrderDocStatus.全部验收.Key, ReaBmsOrderDocStatus.全部验收.Value);
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
    public static class BmsCenSaleDocConfirmSourceType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 手工验收 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "手工验收", Code = "ManualInput", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 订单验收 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "订单验收", Code = "ReaOrder", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 供货验收 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "供货验收", Code = "ReaSale", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(BmsCenSaleDocConfirmSourceType.手工验收.Key, BmsCenSaleDocConfirmSourceType.手工验收.Value);
            dic.Add(BmsCenSaleDocConfirmSourceType.订单验收.Key, BmsCenSaleDocConfirmSourceType.订单验收.Value);
            dic.Add(BmsCenSaleDocConfirmSourceType.供货验收.Key, BmsCenSaleDocConfirmSourceType.供货验收.Value);
            return dic;
        }
    }
    /// <summary>
    /// 客户端验货单状态
    /// </summary>
    public static class BmsCenSaleDocConfirmStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 待继续验收 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "待继续验收", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 已验收 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "已验收", Code = "Accept", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 部分入库 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "部分入库", Code = "PartStorage", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 全部入库 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "全部入库", Code = "Storage", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(BmsCenSaleDocConfirmStatus.待继续验收.Key, BmsCenSaleDocConfirmStatus.待继续验收.Value);
            dic.Add(BmsCenSaleDocConfirmStatus.已验收.Key, BmsCenSaleDocConfirmStatus.已验收.Value);
            dic.Add(BmsCenSaleDocConfirmStatus.部分入库.Key, BmsCenSaleDocConfirmStatus.部分入库.Value);
            dic.Add(BmsCenSaleDocConfirmStatus.全部入库.Key, BmsCenSaleDocConfirmStatus.全部入库.Value);
            return dic;
        }
    }
    /// <summary>
    /// 客户端验货单明细状态
    /// </summary>
    public static class BmsCenSaleDtlConfirmStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 待继续验收 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "待继续验收", Code = "Apply", FontColor = "#ffffff", BGColor = "#bfbfbf" });
        public static KeyValuePair<string, BaseClassDicEntity> 已验收 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "已验收", Code = "Accept", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 部分入库 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "部分入库", Code = "PartStorage", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 全部入库 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "全部入库", Code = "Storage", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(BmsCenSaleDtlConfirmStatus.待继续验收.Key, BmsCenSaleDtlConfirmStatus.待继续验收.Value);
            dic.Add(BmsCenSaleDtlConfirmStatus.已验收.Key, BmsCenSaleDtlConfirmStatus.已验收.Value);
            dic.Add(BmsCenSaleDtlConfirmStatus.部分入库.Key, BmsCenSaleDtlConfirmStatus.部分入库.Value);
            dic.Add(BmsCenSaleDtlConfirmStatus.全部入库.Key, BmsCenSaleDtlConfirmStatus.全部入库.Value);
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
    public static class ReaGoodsBarCodeMgr
    {
        public static KeyValuePair<string, BaseClassDicEntity> 批条码 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "批条码", Code = "BatchBarCode", FontColor = "#ffffff", BGColor = "#1c8f36" });
        public static KeyValuePair<string, BaseClassDicEntity> 盒条码 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "盒条码", Code = "BoxBarCode", FontColor = "#ffffff", BGColor = "#5cb85c" });
        public static KeyValuePair<string, BaseClassDicEntity> 无条码 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "无条码", Code = "NoBarCode", FontColor = "#ffffff", BGColor = "#1195db" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaGoodsBarCodeMgr.批条码.Key, ReaGoodsBarCodeMgr.批条码.Value);
            dic.Add(ReaGoodsBarCodeMgr.盒条码.Key, ReaGoodsBarCodeMgr.盒条码.Value);
            dic.Add(ReaGoodsBarCodeMgr.无条码.Key, ReaGoodsBarCodeMgr.无条码.Value);
            return dic;
        }
    }
    /// <summary>
    /// 货品条码操作类型
    /// </summary>
    public static class ReaGoodsBarcodeOperType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 供货 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "供货", Code = "Availability", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 验货接收 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "验货接收", Code = "ConfirmAccept", FontColor = "#ffffff", BGColor = "#aad08f" });
        public static KeyValuePair<string, BaseClassDicEntity> 验货拒收 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "2", Name = "验货拒收", Code = "ConfirmRefuse", FontColor = "#ffffff", BGColor = "#2c2c2c" });
        public static KeyValuePair<string, BaseClassDicEntity> 验货入库 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "验货入库", Code = "Storage", FontColor = "#ffffff", BGColor = "#aad08f" });
        public static KeyValuePair<string, BaseClassDicEntity> 库存 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "库存", Code = "Stock", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static KeyValuePair<string, BaseClassDicEntity> 移库 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "移库", Code = "Transfer", FontColor = "#ffffff", BGColor = "#2aa515" });
        public static KeyValuePair<string, BaseClassDicEntity> 出库 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "出库", Code = "OutDtl", FontColor = "#ffffff", BGColor = "#7dc5eb" });
        public static KeyValuePair<string, BaseClassDicEntity> 退库 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "退库", Code = "Withdrawal", FontColor = "#ffffff", BGColor = "#1195db" });
        public static KeyValuePair<string, BaseClassDicEntity> 盘库 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "盘库", Code = "Stocktaking", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static KeyValuePair<string, BaseClassDicEntity> 退供应商 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "退供应商", Code = "RetreatSuppliers", FontColor = "#ffffff", BGColor = "#88147f" });
        public static KeyValuePair<string, BaseClassDicEntity> 盘盈 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "盘盈", Code = "Surplus", FontColor = "#ffffff", BGColor = "#e8989a" });
        public static KeyValuePair<string, BaseClassDicEntity> 盘亏 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", Name = "盘亏", Code = "Loss", FontColor = "#ffffff", BGColor = "#2c2c2c" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ReaGoodsBarcodeOperType.供货.Key, ReaGoodsBarcodeOperType.供货.Value);
            dic.Add(ReaGoodsBarcodeOperType.验货接收.Key, ReaGoodsBarcodeOperType.验货接收.Value);
            dic.Add(ReaGoodsBarcodeOperType.验货拒收.Key, ReaGoodsBarcodeOperType.验货拒收.Value);
            dic.Add(ReaGoodsBarcodeOperType.验货入库.Key, ReaGoodsBarcodeOperType.验货入库.Value);
            dic.Add(ReaGoodsBarcodeOperType.库存.Key, ReaGoodsBarcodeOperType.库存.Value);
            dic.Add(ReaGoodsBarcodeOperType.移库.Key, ReaGoodsBarcodeOperType.移库.Value);

            dic.Add(ReaGoodsBarcodeOperType.出库.Key, ReaGoodsBarcodeOperType.出库.Value);
            dic.Add(ReaGoodsBarcodeOperType.退库.Key, ReaGoodsBarcodeOperType.退库.Value);
            dic.Add(ReaGoodsBarcodeOperType.盘库.Key, ReaGoodsBarcodeOperType.盘库.Value);
            dic.Add(ReaGoodsBarcodeOperType.盘盈.Key, ReaGoodsBarcodeOperType.盘盈.Value);
            dic.Add(ReaGoodsBarcodeOperType.盘亏.Key, ReaGoodsBarcodeOperType.盘亏.Value);
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