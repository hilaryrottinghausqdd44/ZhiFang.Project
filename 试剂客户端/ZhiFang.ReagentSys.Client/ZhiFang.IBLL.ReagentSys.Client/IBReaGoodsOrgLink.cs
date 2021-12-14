using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Response;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ZFReaRestful.BmsSaleExtract;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaGoodsScanCode;
using ZhiFang.IBLL.Base;
using System.IO;
using System.Web;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaGoodsOrgLink : IBGenericManager<ReaGoodsOrgLink>
    {
        BaseResultDataValue AddReaGoodsOrgLink(long empID, string empName);
        BaseResultBool EditReaGoodsOrgLink(long empID, string empName);
        BaseResultBool UpdateReaGoodsOrgLink(string[] tempArray, long empID, string empName);
        void AddSCOperation(ReaGoodsOrgLink entity, long empID, string empName, int status);
        /// <summary>
        /// 获取部门货品的全部货品与货品所属供应商信息(按货品进行分组,找出每组货品下的对应的供应商信息)
        /// </summary>
        /// <param name="goodIdStr"></param>
        /// <returns></returns>
        IList<ReaGoodsCenOrgVO> SearchReaCenOrgGoodsListByGoodIdStr(string goodIdStr);
        /// <summary>
        /// (可获取机构Id子孙节点)货品机构关系列表数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="isSearchChild"></param>
        /// <returns></returns>
        Entity.Base.EntityList<ReaGoodsOrgLink> SearchReaGoodsOrgLinkAndChildListByHQL(long orgId, string where, string sort, int page, int limit, bool isSearchChild, int orgType);
        /// <summary>
        /// 导入平台供货单,按本地供应商ID及平台货品编码获取对照的供应商与货品信关系的信息
        /// </summary>
        /// <param name="reaCompID"></param>
        /// <param name="goodsNoStr"></param>
        /// <returns></returns>
        IList<ReaGoodsVO> SearchReaGoodsOrgLinkByReaCompIDAndGoodsNoStr(long reaCompID, string goodsNoStr);
        /// <summary>
        /// 扫码获取货品信息
        /// </summary>
        /// <param name="barCode"></param>
        /// <param name="dicMultiKey"></param>
        /// <returns></returns>
        EntityList<ReaGoods> SearchReaGoodsByScanBarCode(string barCode, Dictionary<string, Dictionary<string, string>> dicMultiKey);
        EntityList<ReaGoodsOrgLink> SearchReaGoodsOrgLinkByScanBarCode(string barCode, Dictionary<string, Dictionary<string, string>> dicMultiKey);
        /// <summary>
        /// 扫码获取货品信息
        /// </summary>
        /// <param name="reaCompID"></param>
        /// <param name="dicKey"></param>
        /// <returns>ReaBarCodeVO</returns>
        ReaGoodsScanCodeVO SearchReaGoodsScanCodeVOByScanBarCode(long reaCompID, string barCode, Dictionary<string, string> dicKey);
        /// <summary>
        /// 依客户端某一供应商的所属机构平台编码导出供应商货品关系信息为文件
        /// </summary>
        /// <param name="reaServerCompCode"></param>
        /// <param name="reaServerLabcCode"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream GetLabDictionaryExportToComp(string reaServerCompCode, string reaServerLabcCode, ref string fileName);
        /// <summary>
        /// 平台供应商导入客户端某一供应商的字典信息
        /// </summary>
        /// <param name="file"></param>
        /// <param name="compLabID">平台供应商所属的LabID</param>
        /// <returns></returns>
        BaseResultDataValue AddUploadLabDictionaryOfCompSync(HttpPostedFile file, long compLabID);
        BaseResultData SaveReaGoodsOrgLinkByMatchInterface(IList<ReaGoods> listReaGoods, long empID, string empName);

        BaseResultData SaveReaGoodsOrgLinkByMatchInterface(IList<ReaGoods> listReaGoods, ReaCenOrg reaOrg, long empID, string empName, ref ReaGoodsOrgLink reaGoodsOrgLink);

        string QueryReaGoodsXML(string goodsNo, string lastModifyTime, string resultFieldList, string resultType);

        string QueryReaOrgGoodsXML(string goodsNo, string orgNo, string lastModifyTime, string resultFieldList, string resultType);
    }
}