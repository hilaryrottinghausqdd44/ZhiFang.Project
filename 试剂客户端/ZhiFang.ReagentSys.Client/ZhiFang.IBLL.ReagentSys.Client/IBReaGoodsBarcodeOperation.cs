using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaGoodsScanCode;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaGoodsBarcodeOperation : IBGenericManager<ReaGoodsBarcodeOperation>
    {
        BaseResultBool AddBarcodeOperationOfList(IList<ReaGoodsBarcodeOperation> dtAddList, long operTypeID, long empID, string empName, long labID);
        BaseResultBool AddBarcodeOperation(ReaGoodsBarcodeOperation operation, long operTypeID, long empID, string empName, long labID);
        /// <summary>
        /// 验收货品扫码
        /// </summary>
        /// <param name="reaCompID"></param>
        /// <param name="serialNo"></param>
        /// <param name="scanCodeType">验收方式,手工,订单,供货</param>
        /// <param name="bobjectID">业务对象ID(订单总单Id/供货总单Id)</param>
        /// <returns></returns>
        ReaGoodsScanCodeVO DecodingReaGoodsScanCodeVOOfConfirm(long reaCompID, string serialNo, string scanCodeType, long bobjectID);
        /// <summary>
        /// 客户端入库货品扫码
        /// </summary>
        /// <param name="reaCompID">供应商Id</param>
        /// <param name="serialNo"></param>
        /// <param name="docConfirmID">验收单Id</param>
        /// <returns></returns>
        ReaGoodsScanCodeVO DecodingReaGoodsScanCodeVOOfReaBmsInByCompIDAndSerialNo(long reaCompID, string serialNo, long docConfirmID);
        /// <summary>
        /// 依入库单ID获取(生成)货品条码信息
        /// </summary>
        /// <param name="inDocId"></param>
        /// <returns></returns>
        EntityList<ReaGoodsPrintBarCodeVO> SearchReaGoodsPrintBarCodeVOListByInDocId(long inDocId, string dtlIdStr, string boxHql, int page, int limit);
        /// <summary>
        /// 依入库明细ID获取(生成)货品条码信息
        /// </summary>
        /// <param name="inDtlId"></param>
        /// <returns></returns>
        EntityList<ReaGoodsPrintBarCodeVO> SearchReaGoodsPrintBarCodeVOListByInDtlId(long inDtlId, string boxHql, int page, int limit);
        /// <summary>
        /// 依供货单ID获取(生成)货品条码信息
        /// </summary>
        /// <param name="saledocId">供货单ID</param>
        /// <param name="dtlIdStr">供货明细IDStr</param>
        /// <returns></returns>
        EntityList<ReaGoodsPrintBarCodeVO> SearchReaGoodsPrintBarCodeVOListBySaledocId(long saledocId, string dtlIdStr, int page, int limit);
        /// <summary>
        /// 依供货明细ID获取(生成)货品条码生成信息
        /// </summary>
        /// <param name="saleDtlId"></param>
        /// <returns></returns>
        EntityList<ReaGoodsPrintBarCodeVO> SearchReaGoodsPrintBarCodeVOListBySaleDtlId(long saleDtlId, int page, int limit);
        /// <summary>
        /// 条码打印后根据选中的条码数据更新条码打印次数
        /// </summary>
        /// <param name="lotList">批条码的业务表的ID值集合</param>
        /// <param name="lotType">批条码的业务表类型(供货明细表;入库明细表)</param>
        /// <param name="boxList">合条码的条码操作记录表的ID值集合</param>
        /// <returns></returns>
        BaseResultBool UpdatePrintCount(IList<long> lotList, string lotType, IList<long> boxList);
        /// <summary>
        /// 出库,移库根据货品条码获取机构货品关系相关信息
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        EntityList<ReaGoodsOrgLink> SearchReaGoodsOrgLinkBySanBarCode(string barcode);
        /// <summary>
        /// 根据货品条码获取货品相关信息
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        EntityList<ReaGoods> SearchReaGoodsBySanBarCode(string barcode);
        double SearchOverageQty(string barcode, IList<ReaGoodsBarcodeOperation> barcodeAllList);
        IList<ReaGoodsBarcodeOperation> GetListByHQL(string strHqlWhere);
        /// <summary>
        /// 获取库存货品的剩余条码信息
        /// </summary>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaGoodsBarcodeOperation> SearchOverReaGoodsBarcodeOperationByHQL(string where, string sort, int page, int limit);
        /// <summary>
        /// 新增客户端提取平台供货商的供货明细货品的盒条码信息
        /// </summary>
        /// <param name="saleDtlList"></param>
        /// <param name="barcodeOperationList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddBarcodeOperationListOfPlatformExtract(IList<ReaBmsCenSaleDtl> saleDtlList,IList<ReaGoodsBarcodeOperation> barcodeOperationList, long empID, string empName);

        #region 赣南医学院附属第一医院，通过入库接口写入后，将对方的条码信息写入到条码操作表

        /// <summary>
        /// 货品属性，是否打印条码=否，不使用智方的规则生成条码
        /// 将HRP的条码信息保存到条码操作表
        /// 盒条码、不存在大小包装单位转换
        /// </summary>
        /// <returns></returns>
        BaseResultDataValue AddReaGoodsBarcodeOperationByHRPInterface(IList<ReaBmsInDtl> inDtlList, long empID, string empName);

        #endregion
    }
}