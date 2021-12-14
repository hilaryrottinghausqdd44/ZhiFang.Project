using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaSale;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaBmsCenSaleDoc : Base.IBGenericManager<ReaBmsCenSaleDoc>
    {
        /// <summary>
        /// 删除一条供货明细后,供货金额更新处理
        /// </summary>
        /// <param name="saleDocID"></param>
        /// <param name="delDtlId"></param>
        /// <returns></returns>
        BaseResultBool EditBmsCenSaleDocTotalPrice(long saleDocID, long delDtlId);
        /// <summary>
        /// 新增供货单及供货明细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtAddList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        ///<param name="isValid">是否需要验证明细</param>
        /// <returns></returns>
        BaseResultDataValue AddReaBmsCenSaleDocAndDtl(ReaBmsCenSaleDoc entity, IList<ReaBmsCenSaleDtl> dtAddList, long empID, string empName, bool isValid);
        /// <summary>
        /// 新增订单转供单,实验室订单与平台(供货商)同在一数据库
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddReaBmsCenSaleDocOfOrderToSupply(long orderId, long labID, long empID, string empName);
        /// <summary>
        /// 修改供货单及供货明细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tempArray"></param>
        /// <param name="dtAddList"></param>
        /// <param name="dtEditList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool UpdateReaBmsCenSaleDocAndDt(ReaBmsCenSaleDoc entity, string[] tempArray, IList<ReaBmsCenSaleDtl> dtAddList, IList<ReaBmsCenSaleDtl> dtEditList, string dtlFields, long empID, string empName);
        /// <summary>
        /// 新增保存实验室从平台提取的供货信息
        /// </summary>
        /// <param name="dtAddList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddReaBmsCenSaleDocAndDtl(IList<ReaBmsCenSaleDtlVO> dtAddList, long empID, string empName);
        /// <summary>
        /// 供货验收更新供货信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool EditReaBmsCenSaleDocAndDtlOfConfirm(ReaBmsCenSaleDoc entity, long empID, string empName);
        /// <summary>
        /// 供货验收更新供货信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="editDtlSaleList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool EditReaBmsCenSaleDocAndDtlOfConfirm(ReaBmsCenSaleDoc entity, IList<ReaBmsCenSaleDtl> editDtlSaleList, long empID, string empName);

        BaseResultDataValue AddReaBmsCenSaleDocByInterface(string saleDocXML, CenOrg cenOrg, HREmployee emp, ref ReaBmsCenSaleDoc reaSaleDoc, ref IList<ReaBmsCenSaleDtl> reaSaleDtlList);

        BaseResultDataValue AddReaBmsCenSaleDocByInterface(IList<XElement> saleDocList, IList<XElement> saleDtlList, CenOrg cenOrg, HREmployee emp, ref ReaBmsCenSaleDoc reaSaleDoc, ref IList<ReaBmsCenSaleDtl> reaSaleDtlList, bool isSave);

        BaseResultDataValue AddReaBmsCenSaleDocAndDtl(ReaBmsCenSaleDoc saleDoc, IList<ReaBmsCenSaleDtl> listSaleDtl, long empID, string empName);

        BaseResultDataValue AddReaBmsCenSaleDtlMerge(ref ReaBmsCenSaleDoc reaSaleDoc, ref IList<ReaBmsCenSaleDtl> reaSaleDtlList);

        BaseResultData AddReaBmsCenSaleDocByInterface(DataSet dsSaleDoc, DataSet dsSaleDtl, ref ReaBmsCenSaleDoc reaSaleDoc, ref IList<ReaBmsCenSaleDtl> reaSaleDtlList);
        /// <summary>
        /// 获取供货单PDF文件
        /// </summary>
        /// <param name="reaReportClass"></param>
        /// <param name="id"></param>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <param name="breportType">BTemplateType的Name</param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream SearchPdfReportOfTypeById(string reaReportClass, long id, long labID, string labCName, string breportType, string frx, ref string fileName);
        /// <summary>
        /// 获取供货单Excel文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <param name="breportType">BTemplateType的Name</param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream SearchExcelReportOfExcelById(long id, long labID, string labCName, string breportType, string frx, ref string fileName);

        #region 实验室与供应商同一数据库
        /// <summary>
        /// 实验室按选择的供货单或选择的供应商+供货单号提取供货单
        /// </summary>
        /// <param name="reaCompID">选择供货单的ID</param>
        /// <param name="reaServerCompCode">选择供应商所属机构平台编码</param>
        /// <param name="saleDocNo">供货单号</param>
        /// <param name="reaServerLabcCode">实验室的所属机构平台编码</param>
        /// <returns></returns>
        BaseResultBool EditReaBmsCenSaleDocOfExtract(long saleDocId, string reaServerCompCode, string saleDocNo, string reaServerLabcCode, long empID, string empName);
        /// <summary>
        /// 通过选择供货方+供货单号,获取本地待验收供货单信息
        /// </summary>
        /// <param name="reaServerCompCode"></param>
        /// <param name="saleDocNo"></param>
        /// <param name="reaServerLabcCode"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool SearchLocalReaSaleDocOfConfirmBySaleDocNo(string reaServerCompCode, string saleDocNo, string reaServerLabcCode, long empID, string empName);
        #endregion

        #region 客户端与平台不在同一数据库--客户端部分
        /// <summary>
        /// 智方客户端新增保存提取平台供货商的供货信息
        /// </summary>
        /// <param name="labcCode"></param>
        /// <param name="compCode"></param>
        /// <param name="saleDocId"></param>
        /// <param name="saleDocNo"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <param name="jresult">平台供货商的供货信息</param>
        /// <returns></returns>
        BaseResultDataValue AddSaleDocAndDtlOfPlatformExtract(string labcCode, string compCode, long saleDocId, string saleDocNo, long empID, string empName, JObject jresult);
        #endregion

        #region 客户端与平台不在同一数据库--平台部分
        /// <summary>
        /// 客户端从平台供货商提取供货信息
        /// </summary>
        /// <param name="labcCode"></param>
        /// <param name="compCode"></param>
        /// <param name="saleDocId"></param>
        /// <param name="saleDocNo"></param>
        /// <returns></returns>
        BaseResultDataValue GetPlatformSaleDocAndDtlToClient(string labcCode, string compCode, long saleDocId, string saleDocNo, ref JObject jresult);
        /// <summary>
        /// 客户端提取平台供货数据成功后,更新平台供货单的提取标志及状态(总单+明细)
        /// </summary>
        /// <param name="labcCode"></param>
        /// <param name="compCode"></param>
        /// <param name="saleDocId"></param>
        /// <param name="saleDocNo"></param>
        /// <returns></returns>
        BaseResultDataValue UpdatePlatformSaleDocAndDtlToClient(string labcCode, string compCode, long saleDocId, string saleDocNo);

        #endregion
    }
}