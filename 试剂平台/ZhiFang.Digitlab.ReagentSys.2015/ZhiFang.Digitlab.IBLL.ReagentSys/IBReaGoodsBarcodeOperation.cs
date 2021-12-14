using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaGoodsScanCode;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaGoodsBarcodeOperation : IBGenericManager<ReaGoodsBarcodeOperation>
    {
        BaseResultBool AddBarcodeOperationOfList(IList<ReaGoodsBarcodeOperation> dtAddList, long operTypeID, long empID, string empName);
        /// <summary>
        /// 定制客户端订单验收货品扫码
        /// </summary>
        /// <param name="reaCompID"></param>
        /// <param name="serialNo"></param>
        /// <param name="scanCodeType">扫码类型</param>
        /// <returns></returns>
        ReaGoodsScanCodeVO DecodingReaGoodsScanCodeVOOfConfirmByCompIDAndSerialNo(long reaCompID, string serialNo, string scanCodeType, long orderId);
        /// <summary>
        /// 客户端入库货品扫码
        /// </summary>
        /// <param name="reaCompID">供应商Id</param>
        /// <param name="serialNo"></param>
        /// <param name="docConfirmID">验收单Id</param>
        /// <returns></returns>
        ReaGoodsScanCodeVO DecodingReaGoodsScanCodeVOOfReaBmsInByCompIDAndSerialNo(long reaCompID, string serialNo, long docConfirmID);
    }
}