using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBmsCenSaleDoc : IBGenericManager<BmsCenSaleDoc>
    {

        BaseResultData AddBmsCenSaleDoc(string jsonEntity);

        BaseResultData AddBmsCenSaleDoc(string jsonEntity, string saleDocNo, string labNo, string compNo);

        BaseResultDataValue ReadBmsCenSaleDocDataFormExcel(string labID, string compID, string excelFilePath, string serverPath);

        BaseResultDataValue AddBmsCenSaleDtlBarCodeList(long SaleDtlID, string SaleDtlBarCodeIDList, IList<BmsCenSaleDtlBarCode> listBmsCenSaleDtlBarCode);

        BaseResultDataValue AddBmsCenSaleDocByOrderDoc(long bmsCenOrderDocID);

        BaseResultDataValue AddSplitSaleDocByID(string docID, int splitType, int compatibleType);

        BaseResultDataValue AddConfirmSaleDocByIDList(string listID, int isSplit, int isTemp);

        BaseResultDataValue AddConfirmSaleDocByID(string docID, string invoiceNo, string accepterMemo, string accepterID, string accepterName, string secAccepterID, string secAccepterName, IList<BmsCenSaleDtl> listNoConfirm, int isSplit, int isTemp);

        BaseResultDataValue AddConfirmSaleDocByID(string docID, string invoiceNo, string accepterMemo, string accepterID, string accepterName, string secAccepterID, string secAccepterName, IList<BmsCenSaleDtl> listNoConfirm, IList<BmsCenSaleDtl> listBatchBarCodeConfirm, int isSplit, int isTemp);     

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

        BaseResultDataValue AddBmsCenSaleDocDataByBaron(string saleDocNo, string jsonData, CenOrg compCenOrg, CenOrg labCenOrg, string otherOrderDocNo);

        EntityList<BmsCenSaleDoc> StatBmsCenSaleDocTotalPrice(string listID);

        EntityList<BmsCenSaleDtl> StatBmsCenSaleDtlGoodsQty(string beginDate, string endDate, string listStatus, string compID, string labID, string goodID, string goodLotNo, string groupbyType, string sort);

        EntityList<BmsCenSaleDtl> StatBmsCenSaleDtlGoodsQty(string beginDate, string endDate, string listStatus, string compID, string labID, string goodID, string goodLotNo, string groupbyType, int page, int limit, string sort);

        BaseResultDataValue EidtSaleDocAcceptFlag(long saleDocID);

        DataSet GetBmsCenSaleDtlInfoByID(string idList, string where, string sort, string xmlPath);

        BaseResultDataValue AddBmsCenSaleDocCopyBySaleDocID(long saleDocID, IList<BmsCenSaleDtl> listSaleDtl);

        BaseResultData EditExtractFlagSaleDocByIDAndDtlIDList(string saleDocID, string saleDtlIDList);

    }
}