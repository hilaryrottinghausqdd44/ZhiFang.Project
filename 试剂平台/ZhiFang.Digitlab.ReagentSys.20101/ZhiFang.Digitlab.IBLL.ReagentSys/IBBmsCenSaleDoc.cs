using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBmsCenSaleDoc : IBGenericManager<BmsCenSaleDoc>
    {
        BaseResultDataValue ReadBmsCenSaleDocDataFormExcel(string labID, string compID, string excelFilePath, string serverPath);

        BaseResultDataValue AddBmsCenSaleDtlBarCodeList(long SaleDtlID, string SaleDtlBarCodeIDList, IList<BmsCenSaleDtlBarCode> listBmsCenSaleDtlBarCode);

        BaseResultDataValue AddBmsCenSaleDocByOrderDoc(long bmsCenOrderDocID);

        BaseResultDataValue AddSplitSaleDocByID(string docID, int splitType, int compatibleType);

        BaseResultDataValue AddConfirmSaleDocByIDList(string listID, int isSplit);

        BaseResultDataValue AddConfirmSaleDocByID(string docID, string invoiceNo, string accepterMemo, string accepterID, string accepterName, string secAccepterID, string secAccepterName, IList<BmsCenSaleDtl> listSaleDtlError, int isSplit);

        BaseResultDataValue AddConfirmSaleDocByID(string docID, string invoiceNo, string accepterMemo, string accepterID, string accepterName, string secAccepterID, string secAccepterName, IList<BmsCenSaleDtl> listSaleDtlError, IList<BmsCenSaleDtl> listSaleDtlNoBarCode, int isSplit);     

        BaseResultDataValue JudgeIsSameOrg(int secAccepterType, string docID, string secAccepterAccount, string secAccepterPwd, RBACUser rbacUser);

        BaseResultDataValue EditConfirmSaleDocCancel(string docID, string reason, string accepterAccount, long secAccepterID);

        BaseResultDataValue EditConfirmSaleDocCancelByID(long docID, string reason);

        BaseResultDataValue AddBmsCenSaleDocDataByMaiKe(string jsonData, string serverPath, CenOrg cenOrg);

        BaseResultDataValue EditSplitSaleDocCancelByID(long docID, string reason);

        BaseResultDataValue EditCheckSaleDocByID(string docID, int validateType);

        BaseResultDataValue EditConfirmSaleDocByIDAndDtlIDList(string saleDocID, string saleDtlIDList);

        BaseResultDataValue EditBmsCenSaleDocTotalPrice(long docID);

        BaseResultDataValue EditSaleDocDeleteFlagByID(string idList, int deleteFlag);
        /// <summary>
        /// 批量更新是否入帐标志
        /// </summary>
        /// <param name="saleDocIDStr"></param>
        /// <param name="isAccountInput"></param>
        /// <returns></returns>
        bool UpdateBatchIsAccountInput(string saleDocIDStr, int isAccountInput);

        BaseResultDataValue AddBmsCenSaleDocDataByBaron(string saleDocNo, string jsonData, CenOrg compCenOrg, CenOrg labCenOrg);

        EntityList<BmsCenSaleDoc> StatBmsCenSaleDocTotalPrice(string listID);

        EntityList<BmsCenSaleDtl> StatBmsCenSaleDtlGoodsQty(string beginDate, string endDate, string listStatus, long compID, long labID, long goodID, string goodLotNo, int groupbyType, string sort);

        EntityList<BmsCenSaleDtl> StatBmsCenSaleDtlGoodsQty(string beginDate, string endDate, string listStatus, long compID, long labID, long goodID, string goodLotNo, int groupbyType, int page, int limit, string sort);

    }
}