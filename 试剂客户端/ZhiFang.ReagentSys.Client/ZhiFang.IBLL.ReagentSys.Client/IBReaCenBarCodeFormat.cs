using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Channels;
using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaGoodsScanCode;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaCenBarCodeFormat : IBGenericManager<ReaCenBarCodeFormat>
    {
        /// <summary>
        /// 导出条码规则信息文件给离线客户端使用
        /// </summary>
        /// <param name="platformOrgNo"></param>
        /// <returns></returns>
        FileStream DownLoadReaCenBarCodeFormatOfPlatformOrgNo(string platformOrgNo, long labID);
        /// <summary>
        /// 离线客户端导入下载的条码规则附件文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        BaseResultBool UploadReaCenBarCodeFormatOfAttachment(HttpPostedFile file, long labID);
        /// <summary>
        /// 验收货品扫码
        /// </summary>
        /// <param name="reaCompID"></param>
        /// <param name="barcode"></param>
        /// <returns></returns>
        ReaGoodsScanCodeVO GetReaGoodsScanCodeVOBySanBarCode(long reaCompID, string barcode);
        BaseResultDataValue DecodingSanBarCode(string barcode, ref Dictionary<string, Dictionary<string, string>> dicMultiBarCode, ref int barCodeType);

    }
}